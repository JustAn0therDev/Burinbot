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

        private string _dayOfTheWeek { get; set; }

        #endregion

        #region Protected Members

        protected AnimeScheduleEmbedMessageBuilder AnimeScheduleEmbedMessageBuilder { get; set; } = new AnimeScheduleEmbedMessageBuilder();
        protected ScheduledAnime Animes { get; set; }
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
            _dayOfTheWeek = dayOfTheWeek;
            CreateInheritableEmbedMessage($"Scheduled animes for {_dayOfTheWeek}:", Color.Green, $"These are the scheduled animes for {_dayOfTheWeek}");
        }

        protected void CreateInheritableEmbedMessage(string title, Color color, string description)
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
            RestClient = new RestClient($"{ENDPOINT}/schedule/{_dayOfTheWeek}");
            Response = RestClient.Execute<ScheduledAnime>(new RestRequest());
            Animes = Response.Data;
        }

        private async Task<bool> AnimesPropertyIsNull() => Animes == null || await CheckIfPropertiesInsideTheAnimesListAreNull();

        private async Task<bool> CheckIfPropertiesInsideTheAnimesListAreNull()
        {
            bool listOfAnimesForTheDayIsNull = false;
            Dictionary<string, List<Anime>> dictionaryOfDaysInAWeek = Animes.GetDictionaryOfAllObjectProperties<List<Anime>>();

            List<Anime> listForTheRequestedDay = dictionaryOfDaysInAWeek?
                .Where(w => w.Key.ToLower() == _dayOfTheWeek.ToLower())?
                .Select(s => s.Value).FirstOrDefault();

            if (listForTheRequestedDay == null)
                throw new ArgumentException("Couldn't find a day for the week with the day you provided me.");
            
            if (listForTheRequestedDay.Count == 0)
            {
                await ReplyAsync($"I didn't find any animes scheduled for {_dayOfTheWeek}. Is there a day where animes just don't come out? :thinking:");
                listOfAnimesForTheDayIsNull = true;
            }

            return listOfAnimesForTheDayIsNull;
        }

        private async Task BuildEmbedMessageWithResponseData()
        {
            switch (_dayOfTheWeek.ToUpper())
            {
                case "MONDAY":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForMonday();
                    break;
                case "TUESDAY":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForTuesday();
                    break;
                case "WEDNESDAY":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForWednesday();
                    break;
                case "THURSDAY":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForThursday();
                    break;
                case "FRIDAY":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForFriday();
                    break;
                case "SATURDAY":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForSaturday();
                    break;
                case "SUNDAY":
                    await AnimeScheduleEmbedMessageBuilder.BuildEmbedMessageWithAnimeScheduledForSunday();
                    break;
            }
        }

        protected override async Task VerifyResponseToSendMessage() => await ReplyAsync("", false, EmbedMessage.Build());
    }
}
