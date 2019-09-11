using Burinbot.Entities;
using Discord;
using Discord.Commands;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class AnimeSummary : ModuleBase<SocketCommandContext>
    {
        [Command("summary")]
        [Summary("Gets the summary and some more information about the requested anime!")]
        public async Task GetAnimeSummaryAsync([Remainder]string animeName)
        {
            string search = animeName.Replace(" ", "%20");
            var firstClient = new RestClient($"https://api.jikan.moe/v3/search/anime?q={search}");
            var requestedAnime = firstClient.Execute<AnimeSearch>(new RestRequest());

            if (requestedAnime.StatusCode != System.Net.HttpStatusCode.OK)
            {
                await ReplyAsync("There was an error on the API request. Please call my dad. I need an adult.");
                return;
            }

            if (requestedAnime.Data == null || requestedAnime.Data.Results.Count == 0)
            {
                await ReplyAsync("I couldn't find any information about the informed anime. Did you type it's name correctly?");
                return;
            }

            Anime AnimeResult = new Anime();
            AnimeResult = requestedAnime.Data.Results.First();

            await ReplyAsync(
                $"{AnimeResult.Title}\nMore Info: {AnimeResult.URL}\nSynopsis: {AnimeResult.Synopsis}\nEpisodes: {AnimeResult.Episodes}\nScore: {AnimeResult.Score}"
                );
        }
    }
}