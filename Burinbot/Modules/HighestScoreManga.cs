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
            try
            {
                EmbedBuilder builder = BurinbotUtils.GenerateDiscordEmbedMessage("Highest rated mangas!", Color.Green, "These are the mangas I found based on your request!");

                var response = new RestClient($"https://api.jikan.moe/v3/search/manga?order_by=score").Execute<MangaSearch>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                    return;
                }

                if (response.Data.Results.Count == 0)
                {
                    await ReplyAsync("I didn't find any mangas. That's weird. :thinking:");
                    return;
                }

                MangaSearch mangas = new MangaSearch();

                foreach (Manga item in response.Data.Results)
                {
                    if (mangas.Results.Count < 25)
                        mangas.Results.Add(item);
                }

                foreach (Manga manga in mangas.Results)
                {
                    builder.AddField(x =>
                    {
                        x.Name = $"{manga.Title ?? manga.Name}";
                        x.Value = $"Link: {manga.URL}\nScore: {manga.Score}";
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