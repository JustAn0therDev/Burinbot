using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class ConnectionStatus : ModuleBase<SocketCommandContext>
    {
        [Command("mystatus")]
        [Summary("Returns the current connection status of the user that requested the command.")]
        public async Task GetConnectionStateAsync()
        {
            await ReplyAsync(Context.User.Status.ToString());
        }
    }
}
