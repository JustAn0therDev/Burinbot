using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class RecommendManga : BaseDiscordCommand
    {
        private string EncodedMangaName { get; set; }
        private MangaSearch SearchList { get; set; }
        private MangaList RecommendationList { get; set; }
        private IRestResponse<MangaSearch> Response { get; set; }
        private IRestResponse<MangaList> FinalResponse { get; set; }
        private int MalID { get; set; }

        [Command("recommendmanga")]
        [Summary("Returns a list of mangas based on the informed name from MyAnimeList.")]
        public async Task GetMangaRecommendationsAsync([Remainder]string mangaName)
        {
            try
            {
                CreateDiscordEmbedMessage("Manga recommendations!", Color.Green, "These are the mangas I found based on what you told me:");

                EncodedMangaName = Uri.EscapeDataString(mangaName);

                ExecuteRestRequest();

                PopulateErrorMessageByVerifyingHttpStatusCode(Response);

                await GetFirstMALIdFromSearchList();
                ExecuteFinalRestRequestToGetListOfRecommendations();

                await VerifyResponseToSendMessage();
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }

        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{ENDPOINT}/search/manga?q={EncodedMangaName}");
            Response = RestClient.Execute<MangaSearch>(Request);
            SearchList = Response.Data;
        }

        private async Task GetFirstMALIdFromSearchList()
        {
            if (SearchList == null || SearchList.Results.Count == 0)
            {
                await ReplyAsync("I didn't find any recommendations. Did you insert the correct manga name?");
                return;
            }

            MalID = SearchList.Results[0].MalID;
        }

        private void ExecuteFinalRestRequestToGetListOfRecommendations()
        {
            RestClient = new RestClient($"{ENDPOINT}/manga/{MalID}/recommendations");
            FinalResponse = RestClient.Execute<MangaList>(Request);
            RecommendationList = FinalResponse.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            int counterForNumberOfItemsInAList = 0;

            if (RecommendationList == null || RecommendationList.Recommendations.Count == 0)
            {
                await ReplyAsync("The manga was found but there are no recommendations like it.");
                return;
            }

            while (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE
                && EmbedMessage.Fields.Count < RecommendationList.Recommendations.Count)
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = RecommendationList.Recommendations[counterForNumberOfItemsInAList].Title;
                    x.Value = RecommendationList.Recommendations[counterForNumberOfItemsInAList].URL;
                    x.IsInline = false;
                });

                ++counterForNumberOfItemsInAList;
            }

            await ReplyAsync("", false, EmbedMessage.Build());
        }
    }
}
