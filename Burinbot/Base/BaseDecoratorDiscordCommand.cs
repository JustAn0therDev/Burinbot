using System;
using System.Net;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using RestSharp;

namespace Burinbot.Base
{
    public abstract class BaseDecoratorDiscordCommand : ModuleBase<SocketCommandContext>
    {
        #region Constant Members

        protected const string ENDPOINT = "https://api.jikan.moe/v3";
        protected const string EXCEPTION_MESSAGE = "Something bad happened in the code! Error: ";
        protected const int LIMIT_OF_FIELDS_PER_EMBED_MESSAGE = 25;

        #endregion

        #region Protected Members

        protected virtual EmbedBuilder EmbedMessage { get; set; }
        protected RestClient RestClient { get; set; }
        protected RestRequest Request { get; set; } = new RestRequest(Method.GET);

        #endregion

        #region Public Methods

        public void PopulateErrorMessageByVerifyingHttpStatusCode(IRestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(CreateErrorMessageBasedOnHttpStatusCode(response.StatusCode));
        }

        #endregion

        #region Protected Methods

        protected string CreateErrorMessageBasedOnHttpStatusCode(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    return "A BadRequest error returned while I tried to complete your request. Did you specify the parameters correctly?";

                case HttpStatusCode.NotFound:
                    return "A NotFound status code returned to me while I was completing your request. Did you specify the parameters correctly?";

                case HttpStatusCode.InternalServerError:
                    return "Something happened on the API end. Contact my creator if you need more details or any help!";

                default:
                    return "Something happened and I couldn't complete your request. Please try again soon or contact my creator!";
            }
        }

        protected virtual void ExecuteRestRequest() 
            => throw new NotImplementedException("An HTTP request for this command has not been implemented."); 

        protected virtual async Task VerifyResponseToSendMessage() 
            => await Task.CompletedTask;

        protected virtual void CreateDiscordEmbedMessage(string title, Color color, string description)
        {
            EmbedMessage = new EmbedBuilder()
            {
                Title = title,
                Color = color,
                Description = description
            };
        }

        protected async Task SendExceptionMessageInDiscordChat(Exception exception)
            => await ReplyAsync(EXCEPTION_MESSAGE + exception.Message ?? exception.InnerException.Message);

        #endregion
    }
}
