using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class RegaliaServers : ModuleBase<SocketCommandContext>
    {
        [Command("servers")]
        [Alias("servers")]
        [Summary("Returns a list of servers in which Regalia is currently in.")]
        public async Task GetRegaliaServers()
        {
            DiscordSocketClient discordSocketClient = Context.Client;
            EmbedBuilder builder = new EmbedBuilder();
            var description = "";
            foreach (SocketGuild guild in discordSocketClient.CurrentUser.MutualGuilds)
            {
                description += $"{guild.Name}\n";
            }
            builder.WithTitle($"Regalia is currently in { discordSocketClient.CurrentUser.MutualGuilds.Count} servers!")
                .WithDescription(description).WithColor(Color.Green);
            //Arguments being passed to ReplyAsync correspond to message, IsTTS (text to speech) and an Embed Message.
            await ReplyAsync("", false, builder.Build());
        }
    }
}
