using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Discord;
using System.Linq;
using Burinbot.Entities;
using System;

namespace Burinbot.Modules
{
    public class RecommendAnime : ModuleBase<SocketCommandContext>
    {
        [Command("recommendanime")]
        [Summary("Returns a list of animes based on the informed name from MyAnimeList.")]
        public async Task GetAnimeRecommendationsAsync([Remainder]string animeName)
        {
            try
            {
                var builder = new EmbedBuilder()
                {
                    Color = Color.Green,
                    Description = "These are the animes I found based on what you told me:"
                };

                AnimeSearch searchList = new AnimeSearch();

                //This first search is made because the API does not have a route that accepts a string as an argument to get recommendations.
                string search = animeName.Replace(" ", "%20");
                var firstClient = new RestClient($"https://api.jikan.moe/v3/search/anime?q={search}");
                var requestedAnime = firstClient.Execute<AnimeSearch>(new RestRequest());

                if (requestedAnime.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    await ReplyAsync("There was an error on the API request. Please call my dad. I need an adult.");
                    return;
                }

                if (requestedAnime.Data == null || requestedAnime.Data.Results.Count == 0)
                {
                    await ReplyAsync("I didn't find any recommendations. Did you type the anime's name correctly?");
                    return;
                }

                foreach (var anime in requestedAnime.Data.Results)
                {
                    searchList.Results.Add(anime);
                }

                AnimeList RecommendationList = new AnimeList();

                //Selects the first item of the list to search.
                int malID = searchList.Results.First().MalID;

                var finalClient = new RestClient($"https://api.jikan.moe/v3/anime/{malID}/recommendations");
                var finalResponse = finalClient.Execute<AnimeList>(new RestRequest());
                foreach (var anime in finalResponse.Data.Recommendations)
                {
                    //Limits the size of the list to 25 so Discord can render the embed list.
                    if (RecommendationList.Recommendations.Count < 25)
                        RecommendationList.Recommendations.Add(anime);
                }

                foreach (var anime in RecommendationList.Recommendations)
                {
                    builder.AddField(x =>
                    {
                        x.Name = anime.Title;
                        x.Value = anime.URL;
                        x.IsInline = false;
                    });
                }
                await ReplyAsync("", false, builder.Build());
            }
            catch(ArgumentException aex)
            {
                Console.WriteLine(aex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
