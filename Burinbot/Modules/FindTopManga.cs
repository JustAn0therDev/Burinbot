using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class FindTopManga : BaseDiscordCommand
    {
        private TopMangas TopAnimes { get; set; }
        private IRestResponse<TopMangas> Response { get; set; }

        [Command("topmangas")]
        [Summary("Returns a list of the top rated mangas in MAL!")]
        public async Task GetTopAnimesAsync()
        {
            try
            {
                CreateDiscordEmbedMessage("Top mangas!", Color.Green, "Here are the top mangas I found!");

                ExecuteRestRequest();

                PopulateErrorMessageBasedOnHttpStatusCode(Response);

                await VerifyResponseToSendMessage();
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }

        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{Endpoint}/top/manga");
            Response = RestClient.Execute<TopMangas>(new RestRequest());
            TopAnimes = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            int countNumberOfAddedMangas = 0;

            if (TopAnimes == null || TopAnimes.Top.Count == 0)
                throw new ArgumentNullException("Nothing was found inside the TopMangas list.");

            while (EmbedMessage.Fields.Count < LimitOfFieldsPerEmbedMessage)
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = TopAnimes.Top[countNumberOfAddedMangas].Title
                    ?? TopAnimes.Top[countNumberOfAddedMangas].Name;
                    x.Value = $"Rank: {TopAnimes.Top[countNumberOfAddedMangas].Rank}";
                    x.IsInline = false;
                });

                ++countNumberOfAddedMangas;
            }

            await ReplyAsync("", false, EmbedMessage.Build());
        }
    }
}
