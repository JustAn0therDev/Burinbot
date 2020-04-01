using Burinbot.Base;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class BurinbotServers : BaseDecoratorDiscordCommand
    {
        #region Private Members

        private DiscordSocketClient DiscordSocketClient { get; set; }
        private StringBuilder Description { get; set; } = new StringBuilder();

        #endregion

        [Command("servers")]
        [Alias("servers")]
        [Summary("Returns a list of servers in which Burinbot is currently in.")]
        public async Task GetBurinbotServers()
        {
            try
            {
                DiscordSocketClient = Context.Client;

                BuildStringBuilderWithServersList();
                CreateDiscordEmbedMessage($"Burinbot is currently in {DiscordSocketClient.CurrentUser.MutualGuilds.Count} servers!", Color.Green, Description.ToString());

                await ReplyAsync("", false, EmbedMessage.Build());
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }

        private void BuildStringBuilderWithServersList()
        {
            foreach (var server in DiscordSocketClient.CurrentUser.MutualGuilds)
            {
                Description.AppendLine($"{server.Name}");
            }
        }
    }
}
