﻿using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class ChangeNickname : ModuleBase<SocketCommandContext>
    {
        [Command("changenick")]
        [Summary("Changes the nickname of the mentioned user. It requires permission to change the nickname. It takes a user and the desired nickname as parameters.")]
        [RequireUserPermission(Discord.GuildPermission.ChangeNickname)]
        [RequireBotPermission(Discord.GuildPermission.ChangeNickname)]
        public async Task ChangeNicknameAsync(SocketGuildUser user, [Remainder]string nickname)
        {
            await user.ModifyAsync(x => x.Nickname = nickname);
            await ReplyAsync($"{Context.User.Mention}, the mentioned user's nickname has been changed as you requested.");
        }
    }
}