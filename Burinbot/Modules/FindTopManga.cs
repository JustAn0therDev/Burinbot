using Discord.Commands;
using System.Threading.Tasks;
using RestSharp;
using Discord;
using Burinbot.Entities;
using Burinbot.Utils;
using System;
using Burinbot.Base;

namespace Burinbot.Modules
{
    public class FindTopManga : BaseDiscordCommand
    {
        [Command("topmangas")]
        [Summary("Returns a list of the top rated mangas in MAL!")]
        public async Task GetTopMangasAsync()
        {
            try
            {
                EmbedBuilder builder = BurinbotUtils.CreateDiscordEmbedMessage("Top mangas!", Color.Green, "Here are the top mangas I found!");
                var topMangas = new TopMangas();
                var response = new RestClient("https://api.jikan.moe/v3/top/manga").Execute<TopMangas>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode));
                    return;
                }

                foreach (var anime in response.Data.Top)
                {
                    if (topMangas.Top.Count < 25)
                        topMangas.Top.Add(anime);
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
