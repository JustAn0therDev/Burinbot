using Discord.Commands;
using RestSharp;
using System.Threading.Tasks;
using Burinbot.Entities;
using System.Linq;
using System;
using Burinbot.Utils;
using Burinbot.Base;

namespace Burinbot.Modules
{
    public class MangaUserScore : BaseDiscordCommand
    {
        [Command("mangauserscore")]
        [Alias("Manga User Score")]
        [Summary("Returns the score given to a manga by an specific user. It takes the username and manga name as parameters")]
        public async Task GetUserScoreAsync(string user, [Remainder]string mangaName)
        {
            try
            {
                mangaName = mangaName.Replace(" ", "%20");
                var response = new RestClient($"https://api.jikan.moe/v3/user/{user}/mangalist?search={mangaName}").Execute<UserMangaList>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode));
                    return;
                }

                if (response.Data.UserMangas.Count == 0)
                {
                    await ReplyAsync("I didn't find any mangas based on the user and anime name you informed me. Did you type its name correctly?");
                    return;
                }

                var manga = response.Data.UserMangas[0];

                if (manga != null)
                    await ReplyAsync($"{Context.User.Mention}, the user scored this manga with {manga.Score}");
                else
                    await ReplyAsync($"I didn't find a manga with that name for the specified user. Maybe he/she hasn't gave it a score yet!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
