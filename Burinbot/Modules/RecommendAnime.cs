using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Discord;
using Burinbot.Entities;
using Burinbot.Utils;
using System;

namespace Burinbot.Modules
{
    public class RecommendAnime : ModuleBase<SocketCommandContext>
    {
        [Command("recommendanime")]
        [Summary("Returns a list of animes based on the informed name from MyAnimeList.")]
        public async Task GetAnimeRecommendationsAsync([Remainder]string animeName)
        {
            var builder = BurinbotUtils.CreateDiscordEmbedMessage("Anime recommendations!", Color.Green, "These are the animes I found based on what you told me:");
            var searchList = new AnimeSearch();
            var RecommendationList = new AnimeList();

            try
            {
                //This first search is made because the API does not have a route that accepts a string as an argument to get recommendations.
                var search = animeName.Replace(" ", "%20");
                var response = new RestClient($"https://api.jikan.moe/v3/search/anime?q={search}").Execute<AnimeSearch>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode));
                    return;
                }

                if (response.Data == null || response.Data.Results.Count == 0)
                {
                    await ReplyAsync("I didn't find any recommendations. Did you type the anime's name correctly?");
                    return;
                }

                foreach (var anime in response.Data.Results)
                    searchList.Results.Add(anime);

                //Selects the first item of the list to search.
                int malID = searchList.Results[0].MalID;

                var finalResponse = new RestClient($"https://api.jikan.moe/v3/anime/{malID}/recommendations").Execute<AnimeList>(new RestRequest());

                if (finalResponse.Data.Recommendations.Count > 0)
                {
                    Parallel.ForEach(finalResponse.Data.Recommendations, anime =>
                    {
                        //Limits the size of the list to 25 so Discord can render the embed list.
                        if (RecommendationList.Recommendations.Count < 25)
                            RecommendationList.Recommendations.Add(anime);
                    });
                }
                else
                {
                    await ReplyAsync("The anime was found but there are no recommendations like it.");
                    return;
                }

                Parallel.ForEach(RecommendationList.Recommendations, anime =>
                {
                    builder.AddField(x =>
                    {
                        x.Name = anime.Title;
                        x.Value = anime.URL;
                        x.IsInline = false;
                    });
                });

                await ReplyAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
