using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'surrender' del bot. 
/// Este comando permite a un jugador rendirse durante una batalla actual, 
/// eliminándose de la misma y permitiéndole participar en otra batalla o continuar con el juego.
/// </summary>
// ReSharper disable once UnusedType.Global
public class SurrenderCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'surrender'. Este comando permite al jugador que lo ejecuta 
    /// rendirse en la batalla actual, removiéndose de la lista de jugadores en la batalla 
    /// y dejándole libre para participar en otro combate o seguir con el juego.
    /// </summary>
    /// 
    [Command("surrender")]
    [Summary("Permite al jugador rendirse en la batalla actual y abandonar la misma.")]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        string result = Facade.Instance.Surrender(displayName);
        await ReplyAsync(result);
    }
}