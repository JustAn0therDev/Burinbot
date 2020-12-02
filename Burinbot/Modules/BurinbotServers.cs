using System;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using Burinbot.Base;

namespace Burinbot.Modules
{
    public class BurinbotServers : BaseDiscordCommand
    {
        #region Private Members

        private StringBuilder Description { get; set; } = new StringBuilder();

        #endregion

        [Command("servers")]
        [Alias("servers")]
        [Summary("Returns a list of servers in which Burinbot is currently in.")]
        public async Task GetBurinbotServers()
        {
            try
            {
                foreach (var server in Context.Client.CurrentUser.MutualGuilds)
                    Description.AppendLine($"{server.Name}");

                CreateDiscordEmbedMessage(
                    $"Burinbot is currently in {Context.Client.CurrentUser.MutualGuilds.Count} servers!", 
                    Color.Green, 
                    Description.ToString());

                await ReplyAsync("", false, EmbedMessage.Build());
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }
    }
}
