﻿using Burinbot.Entities;
using Burinbot.Utils;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Burinbot.Modules
{
    public class AnimeSchedule : ModuleBase<SocketCommandContext>
    {
        [Command("animeschedule")]
        [Summary("Returns the scheduled animes for the specified day! It takes a day of the week as a parameter.")]
        public async Task GetScheduledAnimesAsync([Remainder]string dayOfTheWeek)
        {
            var builder = BurinbotUtils.GenerateDiscordEmbedMessage($"Scheduled animes for {dayOfTheWeek}:", Color.Green, $"These are the scheduled animes for {dayOfTheWeek}");
            var burinbotUtils = new BurinbotUtils(new Stopwatch());
            ScheduledAnime Animes = new ScheduledAnime();

            try
            {
                burinbotUtils.StartPerformanceTest();

                var response = new RestClient($"https://api.jikan.moe/v3/schedule/{dayOfTheWeek}").Execute<ScheduledAnime>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                    burinbotUtils.EndAndLogPerformanceTest();
                    return;
                }

                foreach (var list in response.Data.GetType().GetProperties())
                {
                    if (list == null && list.Name.ToLower() == dayOfTheWeek.ToLower())
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {dayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                        return;
                    }
                }

                switch (dayOfTheWeek.ToLower())
                {
                    case "monday":
                        Parallel.ForEach(response.Data.Monday, anime =>
                        {
                            if (Animes.Monday.Count < 25)
                                Animes.Monday.Add(anime);
                        }
                        );

                        Parallel.ForEach(response.Data.Monday, anime =>
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        });
                        break;

                    case "tuesday":
                        Parallel.ForEach(response.Data.Tuesday, anime =>
                        {
                            if (Animes.Tuesday.Count < 25)
                                Animes.Tuesday.Add(anime);
                        }
                        );

                        Parallel.ForEach(response.Data.Tuesday, anime =>
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        });
                        break;

                    case "wednesday":
                        Parallel.ForEach(response.Data.Wednesday, anime =>
                        {
                            if (Animes.Wednesday.Count < 25)
                                Animes.Wednesday.Add(anime);
                        }
                        );

                        Parallel.ForEach(response.Data.Wednesday, anime =>
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        });
                        break;

                    case "thursday":
                        Parallel.ForEach(response.Data.Thursday, anime =>
                        {
                            if (Animes.Thursday.Count < 25)
                                Animes.Thursday.Add(anime);
                        }
                        );

                        Parallel.ForEach(response.Data.Thursday, anime =>
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        });
                        break;

                    case "friday":
                        Parallel.ForEach(response.Data.Friday, anime =>
                        {
                            if (Animes.Friday.Count < 25)
                                Animes.Friday.Add(anime);
                        }
                        );

                        Parallel.ForEach(response.Data.Friday, anime =>
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        });
                        break;

                    case "saturday":
                        Parallel.ForEach(response.Data.Saturday, anime =>
                        {
                            if (Animes.Saturday.Count < 25)
                                Animes.Saturday.Add(anime);
                        }
                        );

                        Parallel.ForEach(response.Data.Saturday, anime =>
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        });

                        break;

                    case "sunday":
                        Parallel.ForEach(response.Data.Sunday, anime =>
                        {
                            if (Animes.Sunday.Count < 25)
                                Animes.Sunday.Add(anime);
                        }
                        );

                        Parallel.ForEach(response.Data.Sunday, anime =>
                        {
                            builder.AddField(x =>
                            {
                                x.Name = $"{anime.Title ?? anime.Name}";
                                x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                                x.IsInline = false;
                            });
                        });
                        break;
                }

                await ReplyAsync("", false, builder.Build());
                burinbotUtils.EndAndLogPerformanceTest();
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
