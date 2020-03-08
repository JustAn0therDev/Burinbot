using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Burinbot.Entities;
using System;
using Burinbot.Utils;
using System.Linq;

namespace Burinbot.Modules
{
    public class AnimeUserScore : ModuleBase<SocketCommandContext>
    {
        [Command("animeuserscore")]
        [Alias("Anime User Score")]
        [Summary("Returns the score given to an anime by an specific user. It takes the username and anime name as parameters")]
        public async Task GetUserScoreAsync(string user, [Remainder]string animeName)
        {
            try
            {
                var client = new RestClient($"https://api.jikan.moe/v3/user/{user}/animelist?search={animeName.Replace(" ", "%20")}");
                var request = new RestRequest(Method.GET);
                var response = client.Execute<UserAnimeList>(request);

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                    return;
                }

                if (response.Data.UserAnimes.Count == 0)
                {
                    await ReplyAsync("I didn't find any animes based on the user and anime name you informed me. Did you mean something else?");
                    return;
                }

                var anime = response.Data.UserAnimes[0];

                if (anime != null)
                    await ReplyAsync($"{Context.User.Mention}, the user scored this anime with {anime.Score}");
                else
                    await ReplyAsync($"I didn't find an anime with that name for the specified user. Maybe he/she hasn't given it a score yet!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
