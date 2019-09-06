using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Regalia.net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class RegaliaServers : ModuleBase<SocketCommandContext>
    {
        private string _command = "servers";
        private string _description = "Returns a list of servers in which the bot is currently in.";

        [Command("servers")]
        [Summary("Returns a list of servers in which the bot is currently in.")]
        public async Task GetRegaliaServers()
        {
            DiscordSocketClient discordSocketClient = Context.Client;
            EmbedBuilder builder = new EmbedBuilder();
            StringBuilder sb = new StringBuilder();
            foreach (SocketGuild guild in discordSocketClient.CurrentUser.MutualGuilds)
            {
                sb.AppendLine($"{guild.Name}");
            }
            builder.WithTitle($"Regalia is currently in { discordSocketClient.CurrentUser.MutualGuilds.Count} servers!")
                .WithDescription(sb.ToString()).WithColor(Color.Green);
            //Arguments being passed to ReplyAsync correspond to message, IsTTS (text to speech) and an Embed Message.
            await ReplyAsync("", false, builder.Build());
        }
    }
}
