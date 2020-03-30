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
