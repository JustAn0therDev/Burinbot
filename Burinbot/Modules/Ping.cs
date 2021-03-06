﻿using Burinbot.Base;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class Ping : BaseDiscordCommand
    {
        [Command("oinc")]
        [Alias("ping")]
        [Summary("Returns Burinbot's latency relative to the Discord API's heartbeat!")]
        public async Task ShowPing()
        {
            try
            {
                await ReplyAsync($"{Context.User.Mention} ronc! Latency: {Context.Client.Latency.ToString("F2")}ms");
            } 
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }
    }
}
