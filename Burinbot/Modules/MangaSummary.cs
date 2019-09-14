using Burinbot.Entities;
using Discord.Commands;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class MangaSummary : ModuleBase<SocketCommandContext>
    {
        [Command("mangasummary")]
        [Summary("Gets the summary and some more information about the requested manga!")]
        public async Task GetMangaSummaryAsync([Remainder]string mangaName)
        {
            string search = mangaName.Replace(" ", "%20");
            var firstClient = new RestClient($"https://api.jikan.moe/v3/search/manga?q={search}");
            var requestedManga = firstClient.Execute<MangaSearch>(new RestRequest());

            if (requestedManga.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest) || requestedManga.StatusCode.Equals(System.Net.HttpStatusCode.InternalServerError))
            {
                await ReplyAsync("There was an error on the API request. Please call my dad. I need an adult.");
                return;
            }

            if (requestedManga.Data == null || requestedManga.Data.Results.Count == 0)
            {
                await ReplyAsync("I couldn't find any information about the informed manga. Did you type it's name correctly?");
                return;
            }

            Manga MangaResult = requestedManga.Data.Results.First();

            await ReplyAsync(
                $"{MangaResult.Title}\nMore Info: {MangaResult.URL}\nSynopsis: {MangaResult.Synopsis}\nChapters: {MangaResult.Chapters}\nScore: {MangaResult.Score}"
                );
        }
    }
}
