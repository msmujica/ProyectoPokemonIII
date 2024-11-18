using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'change' del bot. Este comando nos sirve para que el
/// jugador cambie su pokemon activo.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ChangePokemonCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'change'. Este comando une al jugador que envía el
    /// id del pokemon para cambiarlo.
    /// </summary>
    [Command("change")]
    [Summary(
        """
        Un jugador que elije un id de pokemon para cambiar su activo;
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Display del id del pokemon")]
        int pokemonInt)
    {
        string displayName = CommandHelper.GetDisplayName(Context);

        if (pokemonInt != null)
        {
            string result = Facade.Instance.ChangePokemon(displayName, pokemonInt);
            ReplyAsync(result);
        }
        ReplyAsync("Favor de proporcionar un ID valido.");
    }
}