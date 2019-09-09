using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Discord;
using System.Linq;
using Regalia.net.Entities;

namespace Regalia.net.Modules
{
    public class Recommend : ModuleBase<SocketCommandContext>
    {
        [Command("recommend")]
        [Summary("Returns a list of animes based on the informed name from the MyAnimeList unnofficial API.")]
        public async Task GetAnimeRecommendationAsync([Remainder]string animeName)
        {
            if (string.IsNullOrEmpty(animeName) || animeName == "")
            {
                await ReplyAsync($"You must inform an anime for me to find recommendations!");
                return;
            }
            var builder = new EmbedBuilder()
            {
                Color = Color.Green,
                Description = "These are the animes I found based on what you told me:"
            };
            AnimeSearch searchList = new AnimeSearch();

            string search = animeName.Replace(" ", "%20");
            var firstClient = new RestClient($"https://api.jikan.moe/v3/search/anime?q={search}");
            var requestedAnime = firstClient.Execute<AnimeSearch>(new RestRequest());

            if (requestedAnime.StatusCode != System.Net.HttpStatusCode.OK)
            {
                await ReplyAsync("I didn't find any recommendations. Did you type the anime's name correctly?");
                return;
            }

            foreach (var anime in requestedAnime.Data.Results)
            {
                searchList.Results.Add(anime);
            }

            AnimeList AnimeList = new AnimeList();
            var secondClient = new RestClient($"https://api.jikan.moe/v3/anime/{searchList.Results.First().MalID}/recommendations");
            var secondResponse = secondClient.Execute<AnimeList>(new RestRequest());
            foreach (var anime in secondResponse.Data.Recommendations)
            {
                //Limits the size of the list to 25 so Discord can render the embed list.
                if (AnimeList.Recommendations.Count < 25)
                AnimeList.Recommendations.Add(anime);
            }

            foreach (var recommendation in AnimeList.Recommendations)
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
    }
}
