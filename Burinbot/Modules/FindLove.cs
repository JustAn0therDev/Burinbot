﻿using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class FindLove : ModuleBase<SocketCommandContext>
    {
        [Command("findlove")]
        [Summary("Ah, I see you're a man of culture as well.")]
        public async Task FindLoveAsync()
        {
            var randomGeneratedNumber = new Random().Next(1, 290000);
            await ReplyAsync($"{Context.User.Mention} ah, I see you're a man of culture as well: {randomGeneratedNumber}");
        }
    }
}
