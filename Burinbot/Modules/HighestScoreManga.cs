using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class HighestScoreManga : BaseDiscordCommand
    {
        private MangaSearch MangaSearch { get; set; }
        private IRestResponse<MangaSearch> Response { get; set; }

        [Command("highestscoremangas")]
        [Summary("Returns a list with 25 of the highest scored mangas!")]
        public async Task GetHighestScoreAnimesAsync()
        {
            try
            {
                CreateDiscordEmbedMessage(
                    "Highest rated mangas!",
                    Color.Green,
                    "These are the mangas I found based on your request!");

                ExecuteRestRequest();

                PopulateErrorMessageByVerifyingHttpStatusCode(Response);

                await VerifyResponseToSendMessage();

            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }

        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{ENDPOINT}/search/manga?order_by=score");
            Response = RestClient.Execute<MangaSearch>(new RestRequest());
            MangaSearch = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            if (MangaSearch == null || MangaSearch.Results.Count == 0)
            {
                await ReplyAsync("I didn't find any mangas. That's weird. :thinking:");
                return;
            }

            PopulateEmbedMessageFieldsWithHighestScoreMangas();

            await ReplyAsync("", false, EmbedMessage.Build());
        }

        private void PopulateEmbedMessageFieldsWithHighestScoreMangas()
        {
            int counterForCurrentFieldInTheEmbedMessage = 0;

            while (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE)
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{MangaSearch.Results[counterForCurrentFieldInTheEmbedMessage].Title ?? MangaSearch.Results[counterForCurrentFieldInTheEmbedMessage].Name}";
                    x.Value = $"Link: {MangaSearch.Results[counterForCurrentFieldInTheEmbedMessage].URL}\nScore: {MangaSearch.Results[counterForCurrentFieldInTheEmbedMessage].Score}";
                    x.IsInline = false;
                });

                ++counterForCurrentFieldInTheEmbedMessage;
            }
        }
    }
}