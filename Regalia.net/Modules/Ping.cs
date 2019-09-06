using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        [Command("ping")]
        [Summary("Returns the bot's latency relative to the Discord API connection(It does not reflect on it's ping on an action like reading and processing a message).")]
        public async Task ShowPing()
        {
            _client = new DiscordSocketClient();
            await ReplyAsync($"{_client.Latency}ms");
        }
    }
}
