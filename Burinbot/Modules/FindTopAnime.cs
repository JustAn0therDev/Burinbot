using Discord.Commands;
using System.Threading.Tasks;
using RestSharp;
using Discord;
using Burinbot.Entities;
using System.Collections.Generic;

namespace Burinbot.Modules
{
    public class FindTopAnime : ModuleBase<SocketCommandContext>
    {
        [Command("topanimes")]
        [Summary("Returns a list of the top rated animes in MAL!")]
        public async Task GetTopAnimesAsync()
        {
            var builder = new EmbedBuilder()
            {
                Title = "Top animes!",
                Description = "Here are the top animes I found!",
                Color = Color.Green
            };

            var Client = new RestClient("https://api.jikan.moe/v3/top/anime");
            var Response = Client.Execute<TopAnimes>(new RestRequest());

            TopAnimes topAnimes = new TopAnimes();

            foreach (var anime in Response.Data.Top)
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
    }
}
