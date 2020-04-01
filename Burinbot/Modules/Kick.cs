using Burinbot.Base;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class Kick : BaseDiscordCommand
    {
        [Command("kick")]
        [Summary("Kicks a member of the server. Burinbot must have permission to kick somebody and you should as well.")]
        [RequireUserPermission(Discord.GuildPermission.KickMembers)]
        [RequireBotPermission(Discord.GuildPermission.KickMembers)]
        public async Task KickAsync(SocketGuildUser requestedUser)
        {
            try
            {
                await requestedUser.KickAsync();
                await ReplyAsync($"{Context.User.Mention}, the requested user has been kicked from this server.");
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }
    }
}
