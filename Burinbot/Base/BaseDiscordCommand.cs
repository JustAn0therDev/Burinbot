using System;
using System.Threading.Tasks;
using Burinbot.Utils;
using Discord;
using Discord.Commands;
using RestSharp;

namespace Burinbot.Base
{
    public abstract class BaseDiscordCommand : ModuleBase<SocketCommandContext>
    {
        protected string Endpoint { get; } = "https://api.jikan.moe/v3";
        protected EmbedBuilder EmbedMessage { get; set; }
        protected RestClient RestClient { get; set; }
        protected RestRequest Request { get; set; } = new RestRequest(Method.GET);
        protected string ErrorMessage { get; set; } = string.Empty;

        public void PopulateErrorMessageBasedOnHttpStatusCode(IRestResponse response)
        {
            if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                ErrorMessage = BurinbotUtils.CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode);
            }
        }

        protected virtual void ExecuteRestRequest() { }
        protected virtual async Task VerifyResponseToSendMessage() => await Task.CompletedTask;
    }
}
