using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System;
using Burinbot.Base;

namespace Burinbot.Modules
{
    public class Help : BaseDecoratorDiscordCommand
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
            try
            {
                CreateDiscordEmbedMessage("Currently Available Commands!", Color.Green, "These are the available commands:");

                Parallel.ForEach(_service.Modules, async module =>
                {
                    foreach (var command in module.Commands)
                    {
                        var result = await command.CheckPreconditionsAsync(Context);

                        if (result.IsSuccess)
                            EmbedMessage.AddField(x =>
                            {
                                x.Name = $"{module.Name} | !{command.Aliases[0]}";
                                x.Value = $"{command.Summary}";
                                x.IsInline = false;
                            });
                    }
                });

                await ReplyAsync("", false, EmbedMessage.Build());
            }
            catch (Exception ex)
            {
                await SendExceptionMessageInDiscordChat(ex);
            }
        }

        #endregion
    }
}

