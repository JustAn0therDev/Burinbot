using Discord.Commands;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Alias("ping")]
        [Summary("Returns Regalia's latency relative to the Discord API's heartbeat!")]
        public async Task ShowPing()
        {
            await ReplyAsync($"{Context.User.Mention} pong! Latency: {Context.Client.Latency}ms");
        }
    }
}
