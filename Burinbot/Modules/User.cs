using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class User : BaseDecoratorDiscordCommand
    {
        private MALUser MALUser { get; set; }
        private Dictionary<string, EmbedBuilder> DictionaryOfEmbedMessages { get; set; } = new Dictionary<string, EmbedBuilder>();
        private string UserReceivedFromCommand { get; set; }
        private IRestResponse<MALUser> Response { get; set; }

        [Command("user")]
        [Alias("user")]
        [Summary("Returns some info about the requested user. Just type in a MAL username after the command! e.g.: !user Kuraa")]
        public async Task GetUserAsync([Remainder]string user)
        {
            try
            {
                UserReceivedFromCommand = user.Replace(" ", "%20");

                InitializeDictionaryOfEmbedMessages();

                ExecuteRestRequest();
                PopulateErrorMessageByVerifyingHttpStatusCode(Response);

                await VerifyResponseToSendMessage();
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }

        private void InitializeDictionaryOfEmbedMessages()
        {
            DictionaryOfEmbedMessages.Add("Burinbot", CreateThenReturnDiscordEmbedMessage("Information about the user!", Color.Green, "This is the information I found for the user requested:"));
            DictionaryOfEmbedMessages.Add("ListOfAnimesInEmbedMessage", CreateThenReturnDiscordEmbedMessage("", Color.Green, "List of favorite animes!"));
            DictionaryOfEmbedMessages.Add("ListOfMangasInEmbedMessage", CreateThenReturnDiscordEmbedMessage("", Color.Green, "List of favorite mangas!"));
            DictionaryOfEmbedMessages.Add("ListOfCharactersInEmbedMessage", CreateThenReturnDiscordEmbedMessage("", Color.Green, "List of favorite characters!"));
        }

        private EmbedBuilder CreateThenReturnDiscordEmbedMessage(string title, Color color, string description)
        {
            return new EmbedBuilder
            {
                Title = title,
                Color = color,
                Description = description
            };
        }

        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{ENDPOINT}/user/{UserReceivedFromCommand}");
            Response = RestClient.Execute<MALUser>(Request);
            MALUser = Response.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            int counterForNumberOfItemsInAList = 0;

            DictionaryOfEmbedMessages["Burinbot"].AddField(x =>
            {
                x.Name = "General Stats";
                x.Value = $"Username: {MALUser.Username}\nLink to profile: {MALUser.URL}";
                x.IsInline = false;
            });

            DictionaryOfEmbedMessages["ListOfAnimesInEmbedMessage"].AddField(x =>
            {
                x.Name = "Anime Stats";
                x.Value =
                $"Total Watched Anime Episodes: {MALUser.AnimeStats.EpisodesWatched}\n" +
                $"Currently watching: {MALUser.AnimeStats.Watching}\n" +
                $"Total days spent watching anime: {MALUser.AnimeStats.DaysWatched}\n" +
                $"Dropped animes: {MALUser.AnimeStats.Dropped}\n" +
                $"Animes that he/she plans to watch: {MALUser.AnimeStats.PlanToWatch}\n" +
                $"Rewatched animes: {MALUser.AnimeStats.ReWatched}\n";
                x.IsInline = false;
            });

            DictionaryOfEmbedMessages["ListOfMangasInEmbedMessage"].AddField(x =>
            {
                x.Name = "Manga Stats";
                x.Value =
                $"Total Read Manga Chapters: {MALUser.MangaStats.ChaptersRead}\n" +
                $"Currently reading: {MALUser.MangaStats.Reading}\n" +
                $"Total days spent reading: {MALUser.MangaStats.DaysRead}\n" +
                $"Dropped mangas: {MALUser.MangaStats.Dropped}\n" +
                $"Mangas that he/she plans to read: {MALUser.MangaStats.PlanToRead}\n" +
                $"Reread mangas: {MALUser.MangaStats.ReRead}\n";
                x.IsInline = false;
            });

            DictionaryOfEmbedMessages["ListOfCharactersInEmbedMessage"].AddField(x =>
            {
                x.Name = "Characters Stats";
                x.Value = $"Total of favorite characters: {MALUser.Favorites.Characters.Count}";
                x.IsInline = false;
            });

            if (MALUser == null || MALUser.Favorites.Animes.Count > 0)
            {
                while (DictionaryOfEmbedMessages["ListOfAnimesInEmbedMessage"].Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE
                    && DictionaryOfEmbedMessages["ListOfAnimesInEmbedMessage"].Fields.Count <= MALUser.Favorites.Animes.Count)
                {
                    DictionaryOfEmbedMessages["ListOfAnimesInEmbedMessage"].AddField(x =>
                    {
                        x.Name = MALUser.Favorites.Animes[counterForNumberOfItemsInAList].Name;
                        x.Value = MALUser.Favorites.Animes[counterForNumberOfItemsInAList].URL;
                        x.IsInline = false;
                    });

                    ++counterForNumberOfItemsInAList;
                }
            }

            counterForNumberOfItemsInAList = 0;

            if (MALUser == null || MALUser.Favorites.Mangas.Count > 0)
            {
                while (DictionaryOfEmbedMessages["ListOfMangasInEmbedMessage"].Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE
                    && DictionaryOfEmbedMessages["ListOfMangasInEmbedMessage"].Fields.Count <= MALUser.Favorites.Mangas.Count)
                {
                    DictionaryOfEmbedMessages["ListOfMangasInEmbedMessage"].AddField(x =>
                    {
                        x.Name = MALUser.Favorites.Mangas[counterForNumberOfItemsInAList].Name;
                        x.Value = MALUser.Favorites.Mangas[counterForNumberOfItemsInAList].URL;
                        x.IsInline = false;
                    });

                    ++counterForNumberOfItemsInAList;
                }
            }

            counterForNumberOfItemsInAList = 0;

            if (MALUser == null || MALUser.Favorites.Characters.Count > 0)
            {
                while (DictionaryOfEmbedMessages["ListOfCharactersInEmbedMessage"].Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE
                    && DictionaryOfEmbedMessages["ListOfCharactersInEmbedMessage"].Fields.Count <= MALUser.Favorites.Characters.Count)
                {
                    DictionaryOfEmbedMessages["ListOfCharactersInEmbedMessage"].AddField(x =>
                    {
                        x.Name = MALUser.Favorites.Characters[counterForNumberOfItemsInAList].Name;
                        x.Value = MALUser.Favorites.Characters[counterForNumberOfItemsInAList].URL;
                        x.IsInline = false;
                    });

                    ++counterForNumberOfItemsInAList;
                }
            }

            await ReplyAsync("", false, DictionaryOfEmbedMessages["Burinbot"].Build());
            await ReplyAsync("", false, DictionaryOfEmbedMessages["ListOfAnimesInEmbedMessage"].Build());
            await ReplyAsync("", false, DictionaryOfEmbedMessages["ListOfMangasInEmbedMessage"].Build());
            await ReplyAsync("", false, DictionaryOfEmbedMessages["ListOfCharactersInEmbedMessage"].Build());
        }
    }
}
