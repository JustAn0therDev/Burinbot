using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Burinbot.Entities;
using System.Linq;
using System;

namespace Burinbot.Modules
{
    public class MangaUserScore : ModuleBase<SocketCommandContext>
    {
        [Command("mangauserscore")]
        [Alias("Manga User Score")]
        [Summary("Returns the score given to a manga by an specific user. It takes the username and manga name as parameters")]
        public async Task GetUserScoreAsync(string user, [Remainder]string mangaName)
        {
            try
            {
                mangaName = mangaName.Replace(" ", "%20");
                var Client = new RestClient($"https://api.jikan.moe/v3/user/{user}/mangalist?search={mangaName}");
                var RequestedManga = Client.Execute<UserMangaList>(new RestRequest());

                if (RequestedManga.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest) || RequestedManga.StatusCode.Equals(System.Net.HttpStatusCode.InternalServerError))
                {
                    await ReplyAsync("There was an error on the API request. Please call my dad. I need an adult.");
                    return;
                }

                if (RequestedManga.Data.UserMangas.Count == 0)
                {
                    await ReplyAsync("I didn't find any mangas based on the user and anime name you informed me. Did you type its name correctly?");
                    return;
                }

                UserManga manga = RequestedManga.Data.UserMangas.First();

                if (manga != null)
                    await ReplyAsync($"{Context.User.Mention}, the user scored this manga with {manga.Score}");
                else
                    await ReplyAsync($"I didn't find a manga with that name for the specified user. Maybe he/she hasn't gave it a score yet!");
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
