using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Burinbot.Entities;
using System.Linq;
using System;
using Burinbot.Utils;

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
                var response = new RestClient($"https://api.jikan.moe/v3/user/{user}/animelist?search={animeName.Replace(" ", "%20")}").Execute<UserAnimeList>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                    return;
                }

                if (response.Data.UserAnimes.Count == 0)
                {
                    await ReplyAsync("I didn't find any animes based on the user and anime name you informed me. Did you type its name correctly?");
                    return;
                }

                UserAnime anime = response.Data.UserAnimes.First();

                if (anime != null)
                    await ReplyAsync($"{Context.User.Mention}, the user scored this anime with {anime.Score}");
                else
                    await ReplyAsync($"I didn't find an anime with that name for the specified user. Maybe he/she hasn't given it a score yet!");
            }
            catch (ArgumentNullException aex)
            {
                Console.WriteLine(aex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
