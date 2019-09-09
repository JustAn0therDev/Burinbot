using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class Kick : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Summary("Kicks a member of the server. Regalia must have permission to kick somebody.")]
        [RequireUserPermission(Discord.GuildPermission.KickMembers)]
        [RequireBotPermission(Discord.GuildPermission.KickMembers)]
        public async Task KickAsync(SocketGuildUser user)
        {
            await user.KickAsync();
            await ReplyAsync($"{Context.User.Mention}, the requested user has been kicked from the server.");
        }
    }
}
