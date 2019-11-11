using Discord;

namespace Burinbot.Utils
{
    public static class BurinbotUtils
    {
        public static EmbedBuilder GenerateDiscordEmbedMessage(string title, Color color, string description)
        {
            return new EmbedBuilder()
            {
                Title = title,
                Color = color,
                Description = description
            };
        }

        public static string CheckForHttpStatusCodes(System.Net.HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    return "A BadRequest error returned while I tried to complete your request. Did you specify the parameters correctly?";

                case System.Net.HttpStatusCode.NotFound:
                    return "A NotFound status code returned to me while I was completing your request. Did you specify the parameters correctly?";

                case System.Net.HttpStatusCode.InternalServerError:
                    return "Something happened on the API end. Call my dad if you need more details or any help!";

                default:
                    return "Something happened and I couldn't complete your request. Please try again!";
            }
        }
    }
}
