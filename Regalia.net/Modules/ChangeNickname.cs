using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Burinbot.Modules
{
    public class ChangeNickname : ModuleBase<SocketCommandContext>
    {
        public readonly CommandService _service;
        public readonly DiscordSocketClient _client;
        public ChangeNickname(CommandService service, DiscordSocketClient client)
        {
            //Constructor of the class is made just so we don't bump into an ArgumentNullException.
            service = _service;
            client = _client;
        }
        [Command("changenick")]
        [Summary("Changes the nickname of the mentioned user. It requires permission to change the nickname. It takes a user and the desired nickname as parameters.")]
        [RequireUserPermission(Discord.GuildPermission.ChangeNickname)]
        [RequireBotPermission(Discord.GuildPermission.ChangeNickname)]
        public async Task ChangeNicknameAsync(SocketGuildUser user, [Remainder]string nickname)
        {
            await user.ModifyAsync(x => x.Nickname = nickname);
            await ReplyAsync($"{Context.User.Mention}, the mentioned user's nickname has been changed as you requested.");
        }
    }
}
