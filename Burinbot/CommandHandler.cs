using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;

namespace Burinbot
{
    public class CommandHandler
    {
        #region Private Props

        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        #endregion

        #region Constructors

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        #endregion

        #region Public Methods

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: null);
        }

        #endregion

        #region Private Methods
        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            if (!(messageParam is SocketUserMessage message)) return;

            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
        #endregion
    }
}
