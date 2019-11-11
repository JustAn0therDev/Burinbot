using Burinbot.Entities;
using Burinbot.Utils;
using Discord;
using Discord.Commands;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class AnimeSchedule : ModuleBase<SocketCommandContext>
    {
        [Command("animeschedule")]
        [Summary("Returns the scheduled animes for the specified day! It takes a day of the week as a parameter.")]
        public async Task GetScheduledAnimesAsync([Remainder]string dayOfTheWeek)
        {
            try
            {
                EmbedBuilder builder = BurinbotUtils.GenerateDiscordEmbedMessage($"Scheduled animes for {dayOfTheWeek}:", Color.Green, $"These are the scheduled animes for {dayOfTheWeek}");

                var response = new RestClient($"https://api.jikan.moe/v3/schedule/{dayOfTheWeek}").Execute<ScheduledAnime>(new RestRequest());

                if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    await ReplyAsync(BurinbotUtils.CheckForHttpStatusCodes(response.StatusCode));
                    return;
                }

                ScheduledAnime Animes = new ScheduledAnime();

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
                        foreach (Anime anime in response.Data.Monday)
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
                        foreach (Anime anime in response.Data.Tuesday)
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
                        foreach (Anime anime in response.Data.Wednesday)
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
                        foreach (Anime anime in response.Data.Thursday)
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
                        foreach (Anime anime in response.Data.Friday)
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
                        foreach (Anime anime in response.Data.Saturday)
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
                        foreach (Anime anime in response.Data.Sunday)
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
