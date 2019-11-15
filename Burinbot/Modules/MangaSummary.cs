using Burinbot.Entities;
using Burinbot.Utils;
using Discord.Commands;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Burinbot.Modules
{
    public class MangaSummary : ModuleBase<SocketCommandContext>
    {
        [Command("mangasummary")]
        [Summary("Gets the summary and some more information about the requested manga!")]
        public async Task GetMangaSummaryAsync([Remainder]string mangaName)
        {
            try
            {
                string search = mangaName.Replace(" ", "%20");
                var response = new RestClient($"https://api.jikan.moe/v3/search/manga?q={search}").Execute<MangaSearch>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                    return;
                }

                if (response.Data == null || response.Data.Results.Count == 0)
                {
                    await ReplyAsync("I couldn't find any information about the informed manga. Did you type its name correctly?");
                    return;
                }

                Manga MangaResult = response.Data.Results[0];

                await ReplyAsync(
                    $"{MangaResult.Title}\nMore Info: {MangaResult.URL}\nSynopsis: {MangaResult.Synopsis}\nChapters: {MangaResult.Chapters}\nScore: {MangaResult.Score}"
                    );
            }
            catch (ArgumentNullException aex)
            {
                Console.WriteLine(aex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
