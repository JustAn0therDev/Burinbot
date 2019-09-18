using Burinbot.Entities;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class AnimeSchedule : ModuleBase<SocketCommandContext>
    {
        [Command("animeschedule")]
        [Summary("Returns the scheduled animes for the specified day!")]
        public async Task GetScheduledAnimesAsync([Remainder]string dayOfTheWeek)
        {
            try
            {
                var builder = new EmbedBuilder()
                {
                    Title = $"Scheduled animes for {dayOfTheWeek}: ",
                    Color = Color.Green,
                    Description = $"These are the scheduled animes for {dayOfTheWeek}"
                };

                var Client = new RestClient($"https://api.jikan.moe/v3/schedule/{dayOfTheWeek}");
                var Response = Client.Execute<ScheduledAnime>(new RestRequest());

                ScheduledAnime Animes = new ScheduledAnime();

                foreach (var list in Response.Data.GetType().GetProperties())
                {
                    if (list == null && list.Name.ToLower() == dayOfTheWeek)
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {dayOfTheWeek}.");
                        return;
                    }
                }

                switch (dayOfTheWeek.ToLower())
                {
                    case "monday":
                        foreach (Anime anime in Response.Data.Monday)
                        {
                            if (Animes.Monday.Count < 25)
                                Animes.Monday.Add(anime);
                        }
                        foreach (Anime anime in Animes.Monday)
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        }

                        break;

                    case "tuesday":
                        foreach (Anime anime in Response.Data.Tuesday)
                        {
                            if (Animes.Tuesday.Count < 25)
                                Animes.Tuesday.Add(anime);
                        }
                        foreach (Anime anime in Animes.Tuesday)
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        }

                        break;

                    case "wednesday":
                        foreach (Anime anime in Response.Data.Wednesday)
                        {
                            if (Animes.Wednesday.Count < 25)
                                Animes.Wednesday.Add(anime);
                        }
                        foreach (Anime anime in Animes.Wednesday)
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        }

                        break;

                    case "thursday":
                        foreach (Anime anime in Response.Data.Thursday)
                        {
                            if (Animes.Thursday.Count < 25)
                                Animes.Thursday.Add(anime);
                        }
                        foreach (Anime anime in Animes.Thursday)
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        }

                        break;

                    case "friday":
                        foreach (Anime anime in Response.Data.Friday)
                        {
                            if (Animes.Friday.Count < 25)
                                Animes.Friday.Add(anime);
                        }
                        foreach (Anime anime in Animes.Friday)
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        }

                        break;

                    case "saturday":
                        foreach (Anime anime in Response.Data.Saturday)
                        {
                            if (Animes.Saturday.Count < 25)
                                Animes.Saturday.Add(anime);
                        }
                        foreach (Anime anime in Animes.Saturday)
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        }

                        break;

                    case "sunday":
                        foreach (Anime anime in Response.Data.Sunday)
                        {
                            if (Animes.Sunday.Count < 25)
                                Animes.Sunday.Add(anime);
                        }
                        foreach (Anime anime in Animes.Sunday)
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        }

                        break;
                }

                await ReplyAsync("", false, builder.Build());
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
