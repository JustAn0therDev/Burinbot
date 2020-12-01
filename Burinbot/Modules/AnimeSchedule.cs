using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;
using Burinbot.Modules.SubClassesForModules;
using Burinbot.Utils;

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
            CreateDiscordEmbedMessageThatCanBeInherited($"Scheduled animes for {DayOfTheWeek}:", Color.Green, $"These are the scheduled animes for {DayOfTheWeek}");
        }

        protected void CreateDiscordEmbedMessageThatCanBeInherited(string title, Color color, string description)
        {
            EmbedMessage = new EmbedBuilder()
            {
                Title = title,
                Color = color,
                Description = description
            };
        }

        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{ENDPOINT}/schedule/{DayOfTheWeek}");
            Response = RestClient.Execute<ScheduledAnime>(new RestRequest());
            Animes = Response.Data;
        }

        private async Task<bool> AnimesPropertyIsNull() => Animes == null || await CheckIfPropertiesInsideTheAnimesListAreNull();

        private async Task<bool> CheckIfPropertiesInsideTheAnimesListAreNull()
        {
            bool listOfAnimesForTheDayIsNull = false;
            Dictionary<string, List<Anime>> dictionaryOfDaysInAWeek = Animes.ReturnDictionaryOfAllObjectProperties<List<Anime>>();

            List<Anime> listForTheRequestedDay = dictionaryOfDaysInAWeek?
                .Where(w => w.Key.ToLower() == DayOfTheWeek.ToLower())?
                .Select(s => s.Value).FirstOrDefault();

            if (listForTheRequestedDay == null)
                throw new ArgumentException("Couldn't find a day for the week with the day you provided me.");
            
            if (listForTheRequestedDay.Count == 0)
            {
                await ReplyAsync($"I didn't find any animes scheduled for {DayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                listOfAnimesForTheDayIsNull = true;
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

        protected override async Task VerifyResponseToSendMessage() => await ReplyAsync("", false, EmbedMessage.Build());
    }
}
