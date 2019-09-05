using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class RegaliaServers : ModuleBase<SocketCommandContext>
    {
        [Command("servers")]
        [Summary("Returns a list of servers in which the bot is currently in.")]
        public async Task GetRegaliaServers()
        {
            DiscordSocketClient discordSocketClient = Context.Client;
            EmbedBuilder builder = new EmbedBuilder();
            //TODO: Use StringBuilder to show the list of servers in which Regalia is currently in.
            builder.WithTitle("Regalia servers!").WithDescription($"Regalia is currently in {discordSocketClient.CurrentUser.MutualGuilds.Count} servers!").WithColor(Color.Green);
            //Arguments being passed to ReplyAsync correspond to message, IsTTS (text to speech) and an Embed Message.
            await ReplyAsync("", false, builder.Build());
        } 
    }
}
