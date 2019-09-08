using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
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
            await ReplyAsync($"Pong! Latency: {Context.Client.Latency}ms");
        }
    }
}
