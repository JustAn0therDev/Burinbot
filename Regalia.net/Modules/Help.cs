using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regalia.net.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Returns a list with all of the available commands.")]
        public async Task GetHelpAsync()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();
            StringBuilder sb = new StringBuilder();
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use"
            };
            await ReplyAsync(embedBuilder.WithTitle("These are the available commands:").WithDescription("").ToString());
        }
    }
}
