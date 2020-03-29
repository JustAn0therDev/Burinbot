using System;
using System.Net;
using System.Diagnostics;
using Discord;

namespace Burinbot.Utils
{
    public class BurinbotUtils
    {
        #region Private Props

        private static readonly Stopwatch _stopwatch = new Stopwatch();

        #endregion

        #region Constructors

        public BurinbotUtils() { }

        #endregion

        #region Public Methods

        public static EmbedBuilder CreateDiscordEmbedMessage(string title, Color color, string description)
        {
            return new EmbedBuilder()
            {
                Title = title,
                Color = color,
                Description = description
            };
        }

        public static string CreateErrorMessageBasedOnHttpStatusCode(HttpStatusCode statusCode)
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

        public void StartPerformanceTest()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public void EndAndOutputPerformanceTestInConsole()
        {
            _stopwatch.Stop();
            Console.WriteLine($"Performance monitoring ended. Elapsed time until the end of command execution: {_stopwatch.ElapsedMilliseconds}ms");
        }

        #endregion
    }
}
