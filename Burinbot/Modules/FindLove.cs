using Discord.Commands;
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
            Random rnd = new Random();
            try
            {
                await ReplyAsync(rnd.Next(1, 290000).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
