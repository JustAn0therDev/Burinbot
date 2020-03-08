using System;
using Discord;
using System.Diagnostics;

namespace Burinbot.Utils
{
    public class BurinbotUtils
    {
        #region Private Props

        private readonly Stopwatch _stopwatch;

        #endregion

        #region Constructors

        public BurinbotUtils(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;
        }

        #endregion

        #region Public Methods

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
                    return "Something happened on the API end. Contact my creator if you need more details or any help!";

                default:
                    return "Something happened and I couldn't complete your request. Please try again soon or contact my creator!";
            }
        }

        public void StartPerformanceTest()
        {
            _stopwatch.Start();
        }

        public void EndAndLogPerformanceTest()
        {
            _stopwatch.Stop();
            Console.WriteLine($"Performance monitoring ended. Elapsed time until the end of command execution: {_stopwatch.ElapsedMilliseconds}ms");
        }

        #endregion
    }
}
