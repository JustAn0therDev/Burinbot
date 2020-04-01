﻿using Burinbot.Base;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class ConnectionStatus : BaseDecoratorDiscordCommand
    {
        [Command("mystatus")]
        [Alias("mystatus")]
        [Summary("Returns the current connection status of the user that requested the command.")]
        public async Task GetConnectionStateAsync()
        {
            try
            {
                await ReplyAsync($"{Context.User.Mention}, your status is now: {Context.User.Status}");
            }
            catch(Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }
    }
}
