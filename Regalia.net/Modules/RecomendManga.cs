using Burinbot.Entities;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class RecommendManga : ModuleBase<SocketCommandContext>
    {
        [Command("recommendmanga")]
        [Alias("manga")]
        [Summary("Returns a list of mangas based on the informed name from MyAnimeList.")]
        public async Task GetMangaRecommendationsAsync([Remainder]string mangaName)
        {
            try
            {
                if (string.IsNullOrEmpty(mangaName) || mangaName == "")
                {
                    await ReplyAsync($"You must inform an anime for me to find recommendations!");
                    return;
                }
                var builder = new EmbedBuilder()
                {
                    Color = Discord.Color.Green,
                    Description = "These are the animes I found based on what you told me:"
                };
                RecommendationSearch searchList = new RecommendationSearch();

                string search = mangaName.Replace(" ", "%20");
                var firstClient = new RestClient($"https://api.jikan.moe/v3/search/manga?q={search}");
                var requestedManga = firstClient.Execute<RecommendationSearch>(new RestRequest());

                if (requestedManga.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    await ReplyAsync("I didn't find any recommendations. Did you type the anime's name correctly?");
                    return;
                }

                foreach (var manga in requestedManga.Data.Results)
                {
                    searchList.Results.Add(manga);
                }

                RecommendationList RecommendationList = new RecommendationList();
                var finalClient = new RestClient($"https://api.jikan.moe/v3/manga/{searchList.Results.First().MalID}/recommendations");
                var finalResponse = finalClient.Execute<RecommendationList>(new RestRequest());
                foreach (var manga in finalResponse.Data.Recommendations)
                {
                    //Limits the size of the list to 25 so Discord can render the embed list.
                    if (RecommendationList.Recommendations.Count < 25)
                        RecommendationList.Recommendations.Add(manga);
                }

                foreach (var recommendation in RecommendationList.Recommendations)
                {
                    builder.AddField(x =>
                    {
                        x.Name = recommendation.Title;
                        x.Value = recommendation.URL;
                        x.IsInline = false;
                    });
                }

                await ReplyAsync("", false, builder.Build());
            }
            catch (ArgumentException aex)
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
