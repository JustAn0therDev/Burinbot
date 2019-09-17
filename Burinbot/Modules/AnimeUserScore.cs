using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Burinbot.Entities;
using System.Linq;
using System;

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
                var Client = new RestClient($"https://api.jikan.moe/v3/user/{user}/animelist?search={animeName.Replace(" ", "%20")}");
                var RequestedAnimes = Client.Execute<UserAnimeList>(new RestRequest());

                if (RequestedAnimes.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest) || RequestedAnimes.StatusCode.Equals(System.Net.HttpStatusCode.InternalServerError))
                {
                    await ReplyAsync("There was an error on the API request. Please call my dad. I need an adult.");
                    return;
                }

                if (RequestedAnimes.Data.UserAnimes.Count == 0)
                {
                    await ReplyAsync("I didn't find any animes based on the user and anime name you informed me. Did you type its name correctly?");
                    return;
                }

                UserAnime anime = RequestedAnimes.Data.UserAnimes.First();

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
