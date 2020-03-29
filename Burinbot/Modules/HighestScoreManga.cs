using Burinbot.Entities;
using Burinbot.Utils;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class HighestScoreManga : ModuleBase<SocketCommandContext>
    {
        [Command("highestscoremangas")]
        [Summary("Returns a list with 25 of the highest scored animes!")]
        public async Task GetHighScoreMangasAsync()
        {
            var builder = BurinbotUtils.CreateDiscordEmbedMessage("Highest rated mangas!", Color.Green, "These are the mangas I found based on your request!");
            var mangas = new MangaSearch();

            try
            {
                var response = new RestClient($"https://api.jikan.moe/v3/search/manga?order_by=score").Execute<MangaSearch>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode));
                    return;
                }

                if (response.Data.Results.Count == 0)
                {
                    await ReplyAsync("I didn't find any mangas. That's weird. :thinking:");
                    return;
                }

                Parallel.ForEach(response.Data.Results, manga =>
                {
                    if (mangas.Results.Count < 25)
                        mangas.Results.Add(manga);
                });

                Parallel.ForEach(mangas.Results, manga =>
                {
                    builder.AddField(x =>
                    {
                        x.Name = $"{manga.Title ?? manga.Name}";
                        x.Value = $"Link: {manga.URL}\nScore: {manga.Score}";
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