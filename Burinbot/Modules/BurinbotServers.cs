using Burinbot.Base;
using Discord;
using Discord.Commands;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class BurinbotServers : BaseDiscordCommand
    {
        #region Private Members

        private StringBuilder Description { get; set; }

        #endregion

        [Command("servers")]
        [Alias("servers")]
        [Summary("Returns a list of servers in which Burinbot is currently in.")]
        public async Task GetBurinbotServers()
        {
            try
            {
                var discordSocketClient = Context.Client;
                Parallel.ForEach(discordSocketClient.CurrentUser.MutualGuilds, server => Description.AppendLine(server.Name));

                EmbedMessage.WithTitle($"Burinbot is currently in {discordSocketClient.CurrentUser.MutualGuilds.Count} servers!")
                    .WithDescription(Description.ToString())
                    .WithColor(Color.Green);

                await ReplyAsync("", false, EmbedMessage.Build());
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.Message, false, null);
            }
        }
    }
}
