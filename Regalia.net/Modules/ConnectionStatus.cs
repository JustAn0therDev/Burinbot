using Discord;
using Discord.Commands;
using Regalia.net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class ConnectionStatus : ModuleBase<SocketCommandContext>, IDescription
    {
        private string _command = "mystatus";
        private string _description = "Returns the current connection status of the user that requested the command.";
        [Command("mystatus")]
        [Summary("Returns the current connection status of the user that requested the command.")]
        public async Task GetConnectionStateAsync()
        {
            await ReplyAsync(Context.User.Status.ToString());
        }

        public string Description()
        {
            return $"{_command}: {_description}";
        }
    }
}
