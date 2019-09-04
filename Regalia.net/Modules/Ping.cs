using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Just a fucking test.")]
        public async Task ShowPing() => await ReplyAsync("Finally");
    }
}
