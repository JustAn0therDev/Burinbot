using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System;
using Burinbot.Utils;
using System.Diagnostics;

namespace Burinbot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        #region Private Props

        private readonly CommandService _service;

        #endregion

        #region Constructors
        public Help(CommandService service)
        {
            _service = service;
        }
        #endregion

        #region Public Methods

        [Command("help")]
        [Alias("help")]
        [Summary("Returns a list containing all of Burinbot's commands.")]
        public async Task GetHelpAsync()
        {
            BurinbotUtils burinbotUtils = new BurinbotUtils(new Stopwatch());

            try
            {
                burinbotUtils.StartPerformanceTest();

                EmbedBuilder builder = BurinbotUtils.GenerateDiscordEmbedMessage("Currently Available Commands!", Color.Green, "These are the available commands:");

                foreach (var module in _service.Modules)
                {
                    string description = "";
                    foreach (var cmd in module.Commands)
                    {
                        var result = await cmd.CheckPreconditionsAsync(Context);
                        if (result.IsSuccess)
                            description += $"!{cmd.Aliases[0]}\n{cmd.Summary}";
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

                burinbotUtils.EndAndLogPerformanceTest();
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

        #endregion
    }
}

