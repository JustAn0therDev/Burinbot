using Discord.Net;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Discord.Commands;

namespace Regalia.net
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            DiscordSocketClient _client = new DiscordSocketClient();

            string token = "NTc4NzM2MTMwNjY5OTM2NjQw.XWrOAg._ap6Oq-FSo1n1gMu5mibq2SLJQ4";

            await _client.LoginAsync(Discord.TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);

            Console.ReadKey();
        }
    }
}
