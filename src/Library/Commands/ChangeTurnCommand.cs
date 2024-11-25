using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'ChangeTurn' del bot. Este comando permite
/// que un jugador cambie su turno en el juego una vez que ejecute el comando en Discord.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ChangeTurnCommand : ModuleBase<SocketCommandContext>
{    
    /// <summary>
    /// Implementa el comando 'ChangeTurn'. Este comando permite que el jugador
    /// cambie su turno en el juego. El jugador ejecuta el comando en Discord para
    /// que su turno sea cambiado.
    /// </summary>
    [Command("ChangeTurn")]
    [Summary(
        """
        Cambio el turno del jugador una vez el propio jugador ejecuta el comando en Discord.
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