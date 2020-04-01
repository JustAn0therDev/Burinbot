using Burinbot.Base;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class Ping : BaseDecoratorDiscordCommand
    {
        [Command("oinc")]
        [Alias("ping")]
        [Summary("Returns Burinbot's latency relative to the Discord API's heartbeat!")]
        public async Task ShowPing()
        {
            try
            {
                await ReplyAsync($"{Context.User.Mention} ronc! Latency: {Context.Client.Latency}ms");
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
