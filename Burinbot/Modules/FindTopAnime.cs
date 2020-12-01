using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class FindTopAnime : BaseDecoratorDiscordCommand
    {
        private TopAnimes TopAnimes { get; set; }
        private IRestResponse<TopAnimes> Response { get; set; }

        [Command("topanimes")]
        [Summary("Returns a list of the top rated animes in MAL!")]
        public async Task GetTopAnimesAsync()
        {
            try
            {
                CreateDiscordEmbedMessage("Top animes!", Color.Green, "Here are the top animes I found!");

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
            RestClient = new RestClient($"{ENDPOINT}/top/anime");
            Response = RestClient.Execute<TopAnimes>(new RestRequest());
            TopAnimes = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            int countNumberOfAddedAnimes = 0;

            if (TopAnimes == null || TopAnimes.Top.Count == 0)
                throw new ArgumentNullException("Nothing was found inside the TopAnimes list.");

            while (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE)
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = TopAnimes.Top[countNumberOfAddedAnimes].Title
                    ?? TopAnimes.Top[countNumberOfAddedAnimes].Name;
                    x.Value = $"Rank: {TopAnimes.Top[countNumberOfAddedAnimes].Rank}";
                    x.IsInline = false;
                });

                ++countNumberOfAddedAnimes;
            }

            await ReplyAsync("", false, EmbedMessage.Build());
        }
    }
}
