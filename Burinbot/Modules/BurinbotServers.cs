using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class BurinbotServers : ModuleBase<SocketCommandContext>
    {
        [Command("servers")]
        [Alias("servers")]
        [Summary("Returns a list of servers in which Burinbot is currently in.")]
        public async Task GetRegaliaServers()
        {
            DiscordSocketClient discordSocketClient = Context.Client;
            EmbedBuilder builder = new EmbedBuilder();
            var description = "";
            try
            {
                foreach (SocketGuild guild in discordSocketClient.CurrentUser.MutualGuilds)
                    description += $"{guild.Name}\n";

                builder.WithTitle($"Burinbot is currently in {discordSocketClient.CurrentUser.MutualGuilds.Count} servers!")
                    .WithDescription(description)
                    .WithColor(Color.Green);

                //Arguments being passed to ReplyAsync correspond to message, IsTTS (text to speech) and an Embed Message with the build method.
                await ReplyAsync("", false, builder.Build());
            }
            catch (ArgumentException aex)
            {
                Console.WriteLine(aex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
