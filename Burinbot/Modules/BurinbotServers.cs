using Discord;
using Discord.Commands;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class BurinbotServers : ModuleBase<SocketCommandContext>
    {
        [Command("servers")]
        [Alias("servers")]
        [Summary("Returns a list of servers in which Burinbot is currently in.")]
        public async Task GetBurinbotServers()
        {
            var discordSocketClient = Context.Client;
            var builder = new EmbedBuilder();
            var description = new StringBuilder();

            try
            {
                Parallel.ForEach(discordSocketClient.CurrentUser.MutualGuilds, server => description.AppendLine(server.Name));

                builder.WithTitle($"Burinbot is currently in {discordSocketClient.CurrentUser.MutualGuilds.Count} servers!")
                    .WithDescription(description.ToString())
                    .WithColor(Color.Green);

                //Arguments being passed to ReplyAsync correspond to message, IsTTS (text to speech) and an Embed Message with the build method.
                await ReplyAsync("", false, builder.Build());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
