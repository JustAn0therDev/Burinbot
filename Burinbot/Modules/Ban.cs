using System;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

using Burinbot.Base;

namespace Burinbot.Modules
{
    public class Ban : BaseDiscordCommand
    {
        [Command("ban")]
        [Alias("banmember")]
        [Summary("Bans a member of the server. Must have enough permission on the server to do so.")]
        [RequireUserPermission(Discord.GuildPermission.BanMembers)]
        [RequireBotPermission(Discord.GuildPermission.BanMembers)]
        public async Task BanASync(SocketGuildUser user)
        {
            try
            {
                await Context.Guild.AddBanAsync(user);
                await ReplyAsync($"{Context.User.Mention}, the requested user has been banned from this server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}