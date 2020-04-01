using System;
using System.Threading.Tasks;

using Discord.Commands;

using Burinbot.Base;

namespace Burinbot.Modules
{
    public class FindLove : BaseDecoratorDiscordCommand
    {
        [Command("findlove")]
        [Summary("Ah, I see you're a man of culture as well.")]
        public async Task FindLoveAsync()
        {
            var randomlyGeneratedNumber = new Random().Next(1, 290000);
            await ReplyAsync($"{Context.User.Mention} ah, I see you're a man of culture as well: {randomlyGeneratedNumber}");
        }
    }
}
