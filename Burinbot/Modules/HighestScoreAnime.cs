using Burinbot.Entities;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Threading.Tasks;

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
                var builder = new EmbedBuilder()
                {
                    Title = "Highest rated animes!",
                    Color = Color.Green,
                    Description = "These are the animes I found based on your request!"
                };

                var Client = new RestClient($"https://api.jikan.moe/v3/search/manga?order_by=score");
                var Response = Client.Execute<AnimeSearch>(new RestRequest());

                if (Response.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest) || Response.StatusCode.Equals(System.Net.HttpStatusCode.InternalServerError))
                {
                    await ReplyAsync("There was an error on the API request. Please call my dad. I need an adult.");
                    return;
                }

                if (Response.Data.Results.Count == 0)
                {
                    await ReplyAsync("I didn't find any animes. That's weird. :thinking:");
                    return;
                }

                AnimeSearch animes = new AnimeSearch();

                foreach (Anime item in Response.Data.Results)
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
