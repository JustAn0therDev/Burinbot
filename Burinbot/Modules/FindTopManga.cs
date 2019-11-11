using Discord.Commands;
using System.Threading.Tasks;
using RestSharp;
using Discord;
using Burinbot.Entities;
using Burinbot.Utils;
using System;

namespace Burinbot.Modules
{
    public class FindTopManga : ModuleBase<SocketCommandContext>
    {
        [Command("topmangas")]
        [Summary("Returns a list of the top rated mangas in MAL!")]
        public async Task GetTopMangasAsync()
        {
            try
            {
                EmbedBuilder builder = BurinbotUtils.GenerateDiscordEmbedMessage("Top mangas!", Color.Green, "Here are the top mangas I found!");

                var response = new RestClient("https://api.jikan.moe/v3/top/manga").Execute<TopMangas>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                    return;
                }

                TopMangas topMangas = new TopMangas();

                foreach (var manga in response.Data.Top)
                {
                    if (topMangas.Top.Count < 25)
                        topMangas.Top.Add(manga);
                }

                foreach (var manga in topMangas.Top)
                {
                    builder.AddField(x =>
                    {
                        x.Name = manga.Title ?? manga.Name;
                        x.Value = $"Rank: {manga.Rank}";
                        x.IsInline = false;
                    });
                }

                await ReplyAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
