using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class Kick : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Summary("Kicks a member of the server. Burinbot must have permission to kick somebody.")]
        [RequireUserPermission(Discord.GuildPermission.KickMembers)]
        [RequireBotPermission(Discord.GuildPermission.KickMembers)]
        public async Task KickAsync(SocketGuildUser user)
        {
            try
            {
                await user.KickAsync();
                await ReplyAsync($"{Context.User.Mention}, the requested user has been kicked from the server.");
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
