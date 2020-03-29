using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System;
using Burinbot.Utils;
using System.Diagnostics;
using System.Text;

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
            BurinbotUtils burinbotUtils = new BurinbotUtils();

            try
            {
                burinbotUtils.StartPerformanceTest();

                var builder = BurinbotUtils.CreateDiscordEmbedMessage("Currently Available Commands!", Color.Green, "These are the available commands:");

                Parallel.ForEach(_service.Modules, module =>
                {
                    var description = new StringBuilder();

                    Parallel.ForEach(module.Commands, async command =>
                    {
                        var result = await command.CheckPreconditionsAsync(Context);
                        if (result.IsSuccess)
                            description.AppendLine($"!{command.Aliases[0]}\n{command.Summary}");
                    });

                    if (description != null)
                    {
                        builder.AddField(x =>
                        {
                            x.Name = module.Name;
                            x.Value = description;
                            x.IsInline = false;
                        });
                    }

                });

                await ReplyAsync("", false, builder.Build());

                burinbotUtils.EndAndOutputPerformanceTestInConsole();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}

