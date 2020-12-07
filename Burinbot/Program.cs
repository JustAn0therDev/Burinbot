using System;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Burinbot
{
    class Program
    {
        public static void Main() => new Program().RunBotAsync().GetAwaiter().GetResult();

        #region Private Props

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        #endregion

        #region Private Methods

        private async Task AnnounceUserJoined(SocketGuildUser user)
            => await user.Guild.DefaultChannel.SendMessageAsync($"{user.Mention} has joined the server! Welcome, oinc!");

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            int argPos;
            SocketUserMessage message = (SocketUserMessage)arg;

            if (message == null || message.Author.IsBot) return;

            argPos = 0;

            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos) || !_client.CurrentUser.IsBot)
            {
                var result = await
                    _commands.ExecuteAsync(new SocketCommandContext(_client, message),
                                            argPos,
                                            _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }

        #endregion

        #region Public Methods

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        public async Task RunBotAsync()
        {
            string token = File.ReadAllText("token.txt");

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("You must have a token to connect to discord");

            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;
            await RegisterCommandsAsync();

            await _client.SetGameAsync("Type !help to see all of the available commands!");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        #endregion
    }
}
