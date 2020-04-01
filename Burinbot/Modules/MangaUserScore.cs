using System;
using System.Threading.Tasks;

using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class MangaUserScore : BaseDecoratorDiscordCommand
    {
        private string User { get; set; }
        private string MangaName { get; set; }
        private UserMangaList UserMangaList { get; set; }
        private IRestResponse<UserMangaList> Response { get; set; }

        [Command("mangauserscore")]
        [Alias("Manga User Score")]
        [Summary("Returns the score given to a manga by an specific user. It takes the username and manga name as parameters")]
        public async Task GetUserScoreAsync(string user, [Remainder]string mangaName)
        {
            try
            {
                await PopulateQueryParameters(user, mangaName);

                ExecuteRestRequest();

                PopulateErrorMessageBasedOnHttpStatusCode(Response);

                await VerifyResponseToSendMessage();
            }
            catch (Exception ex)
            {
                await ReplyAsync($"Something bad happened in the code! Error: {ex.Message}");
            }
        }

        private Task PopulateQueryParameters(string user, string mangaName)
        {
            User = user;
            MangaName = mangaName.Replace(" ", "%20");

            return Task.CompletedTask;
        }

        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{Endpoint}/user/{User}/mangalist?search={MangaName}");
            Response = RestClient.Execute<UserMangaList>(Request);
            UserMangaList = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                await ReplyAsync(ErrorMessage);
                return;
            }

            if (UserMangaList == null || UserMangaList.UserMangas.Count == 0)
            {
                await ReplyAsync("I didn't find any mangas based on the user and anime name you informed me. Did you type its name correctly?");
                return;
            }

            if (UserMangaList.UserMangas[0] == null)
                await ReplyAsync($"I didn't find a manga with that name for the specified user. Maybe he/she hasn't gave it a score yet!");
            else
                await ReplyAsync($"{Context.User.Mention}, {User} scored this manga with {UserMangaList.UserMangas[0].Score}");
        }
    }
}
