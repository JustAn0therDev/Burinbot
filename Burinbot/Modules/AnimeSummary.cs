using Burinbot.Entities;
using Discord.Commands;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;
using Burinbot.Utils;

namespace Burinbot.Modules
{
    public class AnimeSummary : ModuleBase<SocketCommandContext>
    {
        [Command("animesummary")]
        [Summary("Gets the summary and some more information about the requested anime!")]
        public async Task GetAnimeSummaryAsync([Remainder]string animeName)
        {
            string search = animeName.Replace(" ", "%20");
            var response = new RestClient($"https://api.jikan.moe/v3/search/anime?q={search}").Execute<AnimeSearch>(new RestRequest());

            if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                return;
            }

            if (response.Data == null || response.Data.Results.Count == 0)
            {
                await ReplyAsync("I couldn't find any information about the informed anime. Did you type it's name correctly?");
                return;
            }

            Anime AnimeResult = response.Data.Results.First();

            await ReplyAsync(
                $"{AnimeResult.Title}\nMore Info: {AnimeResult.URL}\nSynopsis: {AnimeResult.Synopsis}\nEpisodes: {AnimeResult.Episodes}\nScore: {AnimeResult.Score}"
                );
        }
    }
}