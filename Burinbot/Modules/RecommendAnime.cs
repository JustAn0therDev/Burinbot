﻿using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using RestSharp;

using Burinbot.Base;
using Burinbot.Entities;

namespace Burinbot.Modules
{
    public class RecommendAnime : BaseDiscordCommand
    {
        private string EncodedAnimeName { get; set; }
        private AnimeSearch SearchList { get; set; }
        private AnimeList RecommendationList { get; set; }
        private IRestResponse<AnimeSearch> Response { get; set; }
        private IRestResponse<AnimeList> FinalResponse { get; set; }
        private int MalID { get; set; }

        [Command("recommendanime")]
        [Summary("Returns a list of animes based on the informed name from MyAnimeList.")]
        public async Task GetAnimeRecommendationsAsync([Remainder]string animeName)
        {
            try
            {
                CreateDiscordEmbedMessage("Anime recommendations!", Color.Green, "These are the animes I found based on what you told me:");

                EncodedAnimeName = Uri.EscapeDataString(animeName);

                ExecuteRestRequest();

                PopulateErrorMessageByVerifyingHttpStatusCode(Response);

                await GetFirstMALIdFromSearchList();
                ExecuteFinalRestRequestToGetListOfRecommendations();

                await VerifyResponseToSendMessage();
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }


        protected override void ExecuteRestRequest()
        {
            RestClient = new RestClient($"{ENDPOINT}/search/anime?q={EncodedAnimeName}");
            Response = RestClient.Execute<AnimeSearch>(Request);
            SearchList = Response.Data;
        }

        private async Task GetFirstMALIdFromSearchList()
        {
            if (SearchList == null || SearchList.Results.Count == 0)
            {
                await ReplyAsync("I didn't find any recommendations. Did you insert the correct anime name?");
                return;
            }

            MalID = SearchList.Results[0].MalID;
        }

        private void ExecuteFinalRestRequestToGetListOfRecommendations()
        {
            RestClient = new RestClient($"{ENDPOINT}/anime/{MalID}/recommendations");
            FinalResponse = RestClient.Execute<AnimeList>(Request);
            RecommendationList = FinalResponse.Data;
        }

        protected override async Task VerifyResponseToSendMessage()
        {
            int counterForNumberOfItemsInAList = 0;

            if (RecommendationList == null || RecommendationList.Recommendations.Count == 0)
            {
                await ReplyAsync("The anime was found but there are no recommendations like it.");
                return;
            }

            while (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE
                && EmbedMessage.Fields.Count < RecommendationList.Recommendations.Count)
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = RecommendationList.Recommendations[counterForNumberOfItemsInAList].Title;
                    x.Value = RecommendationList.Recommendations[counterForNumberOfItemsInAList].URL;
                    x.IsInline = false;
                });

                ++counterForNumberOfItemsInAList;
            }

            await ReplyAsync("", false, EmbedMessage.Build());
        }
    }
}
