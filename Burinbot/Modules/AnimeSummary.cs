using System;
using System.Threading.Tasks;

using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class AnimeSummary : BaseDecoratorDiscordCommand
    {
        private string AnimeNameWithEncodedSpace { get; set; }
        private AnimeSearch AnimeSearch { get; set; }
        private IRestResponse<AnimeSearch> Response { get; set; }

        [Command("animesummary")]
        [Summary("Gets the summary and some more information about the requested anime!")]
        public async Task GetAnimeSummaryAsync([Remainder]string animeName)
        {
            try
            {
                AnimeNameWithEncodedSpace = animeName.Replace(" ", "%20");
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
            RestClient = new RestClient($"{ENDPOINT}/search/anime?q={AnimeNameWithEncodedSpace}");
            Response = RestClient.Execute<AnimeSearch>(new RestRequest());
            AnimeSearch = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            if (AnimeSearch == null || AnimeSearch.Results.Count == 0)
            {
                await ReplyAsync("I couldn't find any information about the informed anime. Did you type it's name correctly?");
                return;
            }

            await ReplyAsync(
                $"{AnimeSearch.Results[0].Title}\nMore Info: {AnimeSearch.Results[0].URL}\nSynopsis: {AnimeSearch.Results[0].Synopsis}\nEpisodes: {AnimeSearch.Results[0].Episodes}\nScore: {AnimeSearch.Results[0].Score}"
                );
        }
    }
}