﻿using Burinbot.Entities;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Burinbot.Utils;

namespace Burinbot.Modules
{
    public class RecommendManga : ModuleBase<SocketCommandContext>
    {
        [Command("recommendmanga")]
        [Alias("manga")]
        [Summary("Returns a list of mangas based on the informed name from MyAnimeList.")]
        public async Task GetMangaRecommendationsAsync([Remainder]string mangaName)
        {
            EmbedBuilder builder = BurinbotUtils.GenerateDiscordEmbedMessage("Manga recommendations!", Color.Green, "These are the mangas I found based on what you told me:");
            MangaSearch searchList = new MangaSearch();
            MangaList RecommendationList = new MangaList();

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
                    await ReplyAsync("I didn't find any recommendations. Did you type the manga's name correctly?");
                    return;
                }

                foreach (var manga in response.Data.Results)
                    searchList.Results.Add(manga);

                var finalResponse = new RestClient($"https://api.jikan.moe/v3/manga/{searchList.Results[0].MalID}/recommendations").Execute<MangaList>(new RestRequest());

                if (finalResponse.Data.Recommendations.Count > 0)
                    foreach (var manga in finalResponse.Data.Recommendations)
                    {
                        //Limits the size of the list to 25 so Discord can render the embed list.
                        if (RecommendationList.Recommendations.Count < 25)
                            RecommendationList.Recommendations.Add(manga);
                    }
                else
                {
                    await ReplyAsync("The manga was found but there are no recommendations like it.");
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
