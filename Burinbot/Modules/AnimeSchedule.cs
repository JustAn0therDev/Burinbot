using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;
using Burinbot.Modules.SubClassesForModules;

namespace Burinbot.Modules
{
    public class AnimeSchedule : BaseDiscordCommand
    {
        #region Private Members

        private string DayOfTheWeek { get; set; }

        #endregion

        #region Protected Members

        protected new static EmbedBuilder EmbedMessage { get; set; }
        protected AnimeScheduleEmbedMessageBuilder AnimeScheduleEmbedMessageBuilder { get; set; }
        protected static ScheduledAnime Animes { get; set; }
        protected IRestResponse<ScheduledAnime> Response { get; set; }

        #endregion

        [Command("animeschedule")]
        [Summary("Returns the scheduled animes for the specified day! It takes a day of the week as a parameter.")]
        public async Task GetScheduledAnimesAsync([Remainder]string dayOfTheWeek)
        {
            try
            {
                InitializeClassPropertiesWithReceivedDayOfTheWeek(dayOfTheWeek);

                ExecuteRestRequest();

                if (await AnimesPropertyIsNull())
                    return;

                await BuildEmbedMessageWithResponseData();

                await VerifyResponseToSendMessage();
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }

        private void InitializeClassPropertiesWithReceivedDayOfTheWeek(string dayOfTheWeek)
        {
            AnimeScheduleEmbedMessageBuilder = new AnimeScheduleEmbedMessageBuilder();
            DayOfTheWeek = dayOfTheWeek;
            CreateDiscordEmbedMessage($"Scheduled animes for {DayOfTheWeek}:", Color.Green, $"These are the scheduled animes for {DayOfTheWeek}");
        }

        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{Endpoint}/schedule/{DayOfTheWeek}");
            Response = RestClient.Execute<ScheduledAnime>(new RestRequest());
            Animes = Response.Data;
        }

        private async Task<bool> AnimesPropertyIsNull()
        {
            if (Animes == null)
                return true;
            else
                return await CheckIfPropertiesInsideTheAnimesListAreNull();
        }

        private async Task<bool> CheckIfPropertiesInsideTheAnimesListAreNull()
        {
            bool listOfAnimesForTheDayIsNull = false;
            switch (DayOfTheWeek.ToLower())
            {
                case "monday":
                    if (Animes.Monday.Count == 0)
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {DayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                        listOfAnimesForTheDayIsNull = true;
                    }
                    break;

                case "tuesday":
                    if (Animes.Tuesday.Count == 0)
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {DayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                        listOfAnimesForTheDayIsNull = true;
                    }
                    break;

                case "wednesday":
                    if (Animes.Wednesday.Count == 0)
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {DayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                        listOfAnimesForTheDayIsNull = true;
                    }
                    break;

                case "thursday":
                    if (Animes.Thursday.Count == 0)
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {DayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                        listOfAnimesForTheDayIsNull = true;
                    }
                    break;

                case "friday":
                    if (Animes.Friday.Count == 0)
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {DayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                        listOfAnimesForTheDayIsNull = true;
                    }
                    break;

                case "saturday":
                    if (Animes.Saturday.Count == 0)
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {DayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                        listOfAnimesForTheDayIsNull = true;
                    }
                    break;

                case "sunday":
                    if (Animes.Sunday.Count == 0)
                    {
                        await ReplyAsync($"I didn't find any animes scheduled for {DayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                        listOfAnimesForTheDayIsNull = true;
                    }
                    break;
                default:
                    throw new NullReferenceException("There no day of the week with this name, dumbass.");
            }
            return listOfAnimesForTheDayIsNull;
        }

        private async Task BuildEmbedMessageWithResponseData()
        {
            switch (DayOfTheWeek.ToLower())
            {
                case "monday":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForMonday();
                    break;

                case "tuesday":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForTuesday();
                    break;

                case "wednesday":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForWednesday();
                    break;

                case "thursday":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForThursday();
                    break;

                case "friday":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForFriday();
                    break;

                case "saturday":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForSaturday();
                    break;

                case "sunday":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForSunday();
                    break;
            }
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            await ReplyAsync("", false, EmbedMessage.Build());
        }
    }
}
