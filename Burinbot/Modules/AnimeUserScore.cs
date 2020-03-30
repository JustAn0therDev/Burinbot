﻿using System;
using System.Threading.Tasks;

using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class AnimeUserScore : BaseDiscordCommand
    {
        private string AnimeName { get; set; }
        private string User { get; set; }
        private UserAnimeList UserAnimeList { get; set; }
        private IRestResponse<UserAnimeList> Response { get; set; }

        [Command("animeuserscore")]
        [Alias("Anime User Score")]
        [Summary("Returns the score given to an anime by an specific user. It takes the username and anime name as parameters")]
        public async Task GetUserScoreAsync(string user, [Remainder]string animeName)
        {
            try
            {
                ExecuteRestRequest();
                PopulateErrorMessageBasedOnHttpStatusCode(Response);

                await VerifyResponseToSendMessage();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{Endpoint}/user/{User}/animelist?search={AnimeName.Replace(" ", "%20")}");
            Response = RestClient.Execute<UserAnimeList>(Request);
            UserAnimeList = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                await ReplyAsync(ErrorMessage);
                return;
            }
            else
            {
                if (UserAnimeList != null && UserAnimeList.UserAnimes.Count == 0)
                {
                    await ReplyAsync("I didn't find any animes based on the user and anime name you informed me. Did you mean something else?");
                    return;
                }

                if (UserAnimeList.UserAnimes == null)
                    await ReplyAsync("I didn't find an anime with that name for the specified user. Maybe he/she hasn't given it a score yet!");
                else
                    await ReplyAsync($"{Context.User.Mention}, the user scored this anime with {UserAnimeList.UserAnimes[0].Score}");
            }
        }
    }
}
