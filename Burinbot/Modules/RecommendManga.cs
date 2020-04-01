using Burinbot.Entities;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Burinbot.Utils;
using Burinbot.Base;

namespace Burinbot.Modules
{
    public class RecommendManga : BaseDecoratorDiscordCommand
    {
        [Command("recommendmanga")]
        [Alias("manga")]
        [Summary("Returns a list of mangas based on the informed name from MyAnimeList.")]
        public async Task GetMangaRecommendationsAsync([Remainder]string mangaName)
        {
            var builder = BurinbotUtils.CreateDiscordEmbedMessage("Manga recommendations!", Color.Green, "These are the mangas I found based on what you told me:");
            var searchList = new MangaSearch();
            var RecommendationList = new MangaList();

            try
            {
                var search = mangaName.Replace(" ", "%20");
                var response = new RestClient($"https://api.jikan.moe/v3/search/manga?q={search}").Execute<MangaSearch>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode));
                    return;
                }

                if (response.Data == null || response.Data.Results.Count == 0)
                {
                    await ReplyAsync("I didn't find any recommendations. Did you type the manga's name correctly?");
                    return;
                }

                Parallel.ForEach(response.Data.Results, manga => searchList.Results.Add(manga));

                var finalResponse = new RestClient($"https://api.jikan.moe/v3/manga/{searchList.Results[0].MalID}/recommendations").Execute<MangaList>(new RestRequest());

                if (finalResponse.Data.Recommendations.Count > 0)
                    Parallel.ForEach(finalResponse.Data.Recommendations, manga =>
                    {
                        if (RecommendationList.Recommendations.Count < 25)
                            RecommendationList.Recommendations.Add(manga);
                    });
                else
                {
                    await ReplyAsync("The manga was found but there are no recommendations like it.");
                    return;
                }

                Parallel.ForEach(RecommendationList.Recommendations, recommendation =>
                {
                    builder.AddField(x =>
                    {
                        x.Name = recommendation.Title;
                        x.Value = recommendation.URL;
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
