using System;
using System.Threading.Tasks;

using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class MangaSummary : BaseDecoratorDiscordCommand
    {
        private string MangaNameWithEncodedSpace { get; set; }
        private MangaSearch MangaSearch { get; set; }
        private IRestResponse<MangaSearch> Response { get; set; }

        [Command("mangasummary")]
        [Summary("Gets the summary and some more information about the requested manga!")]
        public async Task GetMangaSummaryAsync([Remainder]string mangaName)
        {
            try
            {
                MangaNameWithEncodedSpace = mangaName.Replace(" ", "%20");
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
            RestClient = new RestClient($"{ENDPOINT}/search/manga?q={MangaNameWithEncodedSpace}");
            Response = RestClient.Execute<MangaSearch>(new RestRequest());
            MangaSearch = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            if (MangaSearch == null || MangaSearch.Results.Count == 0)
            {
                await ReplyAsync("I couldn't find any information about the informed manga. Did you type it's name correctly?");
                return;
            }

            await ReplyAsync(
                $"{MangaSearch.Results[0].Title}\nMore Info: {MangaSearch.Results[0].URL}\nSynopsis: {MangaSearch.Results[0].Synopsis}\nChapters: {MangaSearch.Results[0].Chapters}\nScore: {MangaSearch.Results[0].Score}"
                );
        }
    }
}
