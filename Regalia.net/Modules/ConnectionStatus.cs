using Discord.Commands;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class ConnectionStatus : ModuleBase<SocketCommandContext>
    {
        [Command("mystatus")]
        [Alias("mystatus")]
        [Summary("Returns the current connection status of the user that requested the command.")]
        public async Task GetConnectionStateAsync()
        {
            await ReplyAsync($"{Context.User.Mention}, your status is now: {Context.User.Status.ToString()}");
        }
    }
}
