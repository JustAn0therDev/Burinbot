using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class HighestScoreAnime : BaseDiscordCommand
    {
        private AnimeSearch AnimeSearch { get; set; }
        private IRestResponse<AnimeSearch> Response { get; set; }

        [Command("highestscoreanimes")]
        [Summary("Returns a list with 25 of the highest scored animes!")]
        public async Task GetHighestScoreAnimesAsync()
        {

            try
            {
                CreateDiscordEmbedMessage(
                    "Highest rated animes!", 
                    Color.Green,
                    "These are the animes I found based on your request!");

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
            RestClient = new RestClient($"{ENDPOINT}/search/anime?order_by=score");
            Response = RestClient.Execute<AnimeSearch>(new RestRequest());
            AnimeSearch = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            if (AnimeSearch == null || AnimeSearch.Results.Count == 0)
            {
                await ReplyAsync("I didn't find any animes. That's weird. :thinking:");
                return;
            }

            PopulateEmbedMessageFieldsWithHighestScoreAnimes();

            await ReplyAsync("", false, EmbedMessage.Build());
        }

        private void PopulateEmbedMessageFieldsWithHighestScoreAnimes()
        {
            int counterForCurrentFieldInTheEmbedMessage = 0;

            while (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE)
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{AnimeSearch.Results[counterForCurrentFieldInTheEmbedMessage].Title ?? AnimeSearch.Results[counterForCurrentFieldInTheEmbedMessage].Name}";
                    x.Value = $"Link: {AnimeSearch.Results[counterForCurrentFieldInTheEmbedMessage].URL}\nScore: {AnimeSearch.Results[counterForCurrentFieldInTheEmbedMessage].Score}";
                    x.IsInline = false;
                });

                ++counterForCurrentFieldInTheEmbedMessage;
            }
        }
    }
}
