using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Linq;
using System;
using Burinbot.Utils;

namespace Burinbot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;

        public Help(CommandService service)
        {
            _service = service;
        }

        [Command("help")]
        [Alias("help")]
        [Summary("Returns a list containing all of Burinbot's commands.")]
        public async Task GetHelpAsync()
        {
            try
            {
                EmbedBuilder builder = BurinbotUtils.GenerateDiscordEmbedMessage("Currently Available Commands!", Color.Green, "These are the available commands:");

                foreach (var module in _service.Modules)
                {
                    string description = "";
                    foreach (var cmd in module.Commands)
                    {
                        var result = await cmd.CheckPreconditionsAsync(Context);
                        if (result.IsSuccess)
                            description += $"!{cmd.Aliases.First()}\n{cmd.Summary}";
                    }

                    if (!string.IsNullOrWhiteSpace(description))
                    {
                        builder.AddField(x =>
                        {
                            x.Name = module.Name;
                            x.Value = description;
                            x.IsInline = false;
                        });
                    }
                }
                await ReplyAsync("", false, builder.Build());
            }
            catch (ArgumentException aex)
            {
                Console.WriteLine(aex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

