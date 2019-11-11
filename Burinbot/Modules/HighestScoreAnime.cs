﻿using Burinbot.Entities;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Threading.Tasks;
using Burinbot.Utils;

namespace Burinbot.Modules
{
    public class HighestScoreAnime : ModuleBase<SocketCommandContext>
    {
        [Command("highestscoreanimes")]
        [Summary("Returns a list with 25 of the highest scored animes!")]
        public async Task GetHighScoreAnimesAsync()
        {
            try
            {
                EmbedBuilder builder = BurinbotUtils.GenerateDiscordEmbedMessage("Highest rated animes!", Color.Green, "These are the animes I found based on your request!");

                var response = new RestClient($"https://api.jikan.moe/v3/search/anime?order_by=score").Execute<AnimeSearch>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                    return;
                }

                if (response.Data.Results.Count == 0)
                {
                    await ReplyAsync("I didn't find any animes. That's weird. :thinking:");
                    return;
                }

                AnimeSearch animes = new AnimeSearch();

                foreach (Anime item in response.Data.Results)
                {
                    if (animes.Results.Count < 25)
                        animes.Results.Add(item);
                }

                foreach (Anime anime in animes.Results)
                {
                    builder.AddField(x =>
                    {
                        x.Name = $"{anime.Title ?? anime.Name}";
                        x.Value = $"Link: {anime.URL}\nScore: {anime.Score}";
                        x.IsInline = false;
                    });
                }

                await ReplyAsync("", false, builder.Build());
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
