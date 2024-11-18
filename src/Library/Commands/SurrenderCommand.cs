using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'surrender' del bot. Este comando remueve el
/// batalla para poder jugar otra en caso de que el otro player nunca juege o se quiera rendir.
/// </summary>
// ReSharper disable once UnusedType.Global
public class SurrenderCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'leave' del bot. Este comando remueve el jugador
    /// que envía el mensaje de la lista de jugadores esperando para jugar.
    /// </summary>
    [Command("surrender")]
    [Summary("Remueve la batalla y efectua un surrender")]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync()
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        string result = Facade.Instance.Surrender(displayName);
        await ReplyAsync(result);
    }
}