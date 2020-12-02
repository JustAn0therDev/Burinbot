using System;
using System.Threading.Tasks;

using Discord.Commands;

using Burinbot.Base;

namespace Burinbot.Modules
{
    public class FindLove : BaseDiscordCommand
    {
        [Command("findlove")]
        [Summary("Ah, I see you're a man of culture as well.")]
        public async Task FindLoveAsync()
            => await ReplyAsync(
                    $"{Context.User.Mention} ah, I see you're a man of culture as well: {new Random().Next(1, 290000)}"
                    );
    }
}
