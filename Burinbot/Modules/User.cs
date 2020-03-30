﻿using Discord.Commands;
using RestSharp;
using System;
using System.Threading.Tasks;
using Burinbot.Entities;
using Discord;
using Burinbot.Utils;
using Burinbot.Base;

namespace Burinbot.Modules
{
    public class User : BaseDiscordCommand
    {
        [Command("user")]
        [Alias("user")]
        [Summary("Returns some info about the requested user. The parameter is a MAL username!")]
        public async Task GetUserAsync([Remainder]string user)
        {
            var builder = BurinbotUtils.CreateDiscordEmbedMessage("Information about the user!", Color.Green, "This is the information I found for the user requested:");
            var animeList = BurinbotUtils.CreateDiscordEmbedMessage("", Color.Green, "List of favorite animes!");
            var mangaList = BurinbotUtils.CreateDiscordEmbedMessage("", Color.Green, "List of favorite mangas!");
            var characterList = BurinbotUtils.CreateDiscordEmbedMessage("", Color.Green, "List of favorite characters!");

            try
            {
                var MALUser = user.Replace(" ", "%20");
                var response = new RestClient($"https://api.jikan.moe/v3/user/{MALUser}").Execute<MALUser>(new RestRequest());

                if (!response.StatusCode.Equals((System.Net.HttpStatusCode.OK)))
                {
                    await ReplyAsync(CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode));
                    return;
                }

                var User = response.Data;

                builder.AddField(x =>
                {
                    x.Name = "General Stats";
                    x.Value = $"Username: {User.Username}\nLink to profile: {User.URL}";
                    x.IsInline = false;
                });

                builder.AddField(x =>
                {
                    x.Name = "Anime Stats";
                    x.Value =
                    $"Total Watched Anime Episodes: {User.AnimeStats.EpisodesWatched}\n" +
                    $"Currently watching: {User.AnimeStats.Watching}\n" +
                    $"Total days spent watching anime: {User.AnimeStats.DaysWatched}\n" +
                    $"Dropped animes: {User.AnimeStats.Dropped}\n" +
                    $"Animes that he/she plans to watch: {User.AnimeStats.PlanToWatch}\n" +
                    $"Rewatched animes: {User.AnimeStats.ReWatched}\n";
                    x.IsInline = false;
                });

                builder.AddField(x =>
                {
                    x.Name = "Manga Stats";
                    x.Value =
                    $"Total Read Manga Chapters: {User.MangaStats.ChaptersRead}\n" +
                    $"Currently reading: {User.MangaStats.Reading}\n" +
                    $"Total days spent reading: {User.MangaStats.DaysRead}\n" +
                    $"Dropped mangas: {User.MangaStats.Dropped}\n" +
                    $"Mangas that he/she plans to read: {User.MangaStats.PlanToRead}\n" +
                    $"Reread mangas: {User.MangaStats.ReRead}\n";
                    x.IsInline = false;
                });

                if (User.Favorites.Animes.Count > 0)
                {
                    Parallel.ForEach(User.Favorites.Animes, anime =>
                    {
                        if (User.Favorites.Animes.Count < 25)
                            animeList.AddField(x =>
                            {
                                //Since the anime or manga object this time doesn't have a title, but has a name, we use the two inside the same class for simplicity's sake.
                                x.Name = anime.Name;
                                x.Value = anime.URL;
                                x.IsInline = false;
                            });
                    });
                }

                if (User.Favorites.Mangas.Count > 0)
                {
                    Parallel.ForEach(User.Favorites.Mangas, manga =>
                    {
                        if (User.Favorites.Animes.Count < 25)
                            animeList.AddField(x =>
                            {
                                //Since the anime or manga object this time doesn't have a title, but has a name, we use the two inside the same class for simplicity's sake.
                                x.Name = manga.Name;
                                x.Value = manga.URL;
                                x.IsInline = false;
                            });
                    });
                }

                if(User.Favorites.Characters.Count > 0)
                {
                    Parallel.ForEach(User.Favorites.Characters, character =>
                    {
                        if (User.Favorites.Characters.Count < 25)
                            characterList.AddField(x =>
                            {
                                x.Name = character.Name;
                                x.Value = $"{character.URL}";
                                x.IsInline = false;
                            });
                    });
                }

                await ReplyAsync("", false, builder.Build());

                if (User.Favorites.Animes.Count > 0)
                    await ReplyAsync("", false, animeList.Build());

                if (User.Favorites.Mangas.Count > 0)
                    await ReplyAsync("", false, mangaList.Build());

                if (User.Favorites.Characters.Count > 0)
                    await ReplyAsync("", false, characterList.Build());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
