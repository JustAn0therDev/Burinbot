using Discord.Commands;
using System.Threading.Tasks;
using RestSharp;
using Discord;
using Burinbot.Entities;
using Burinbot.Utils;
using System;
using System.Linq;

namespace Burinbot.Modules
{
    public class FindTopAnime : ModuleBase<SocketCommandContext>
    {
        [Command("topanimes")]
        [Summary("Returns a list of the top rated animes in MAL!")]
        public async Task GetTopAnimesAsync()
        {
            var builder = BurinbotUtils.CreateDiscordEmbedMessage("Top animes!", Color.Green, "Here are the top animes I found!");
            var topAnimes = new TopAnimes();

            try
            {
                var response = new RestClient("https://api.jikan.moe/v3/top/anime").Execute<TopAnimes>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode));
                }

                foreach (var anime in response.Data.Top)
                {
                    if (topAnimes.Top.Count < 25)
                        topAnimes.Top.Add(anime);
                }

                foreach (var anime in topAnimes.Top)
                {
                    builder.AddField(x =>
                    {
                        x.Name = anime.Title ?? anime.Name;
                        x.Value = $"Rank: {anime.Rank}";
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
