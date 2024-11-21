using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class ChangeTurnCommand : ModuleBase<SocketCommandContext>
{    
    [Command("ChangeTurn")]
    [Summary(
        """
        Cambia el turno.
        """)] 

    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        

        string result;
        result = Facade.Instance.ChangeTurn(displayName);
        await ReplyAsync(result);
    }
}