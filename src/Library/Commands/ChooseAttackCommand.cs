using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'attack' del bot. Este comando ordena
/// a un Pokémon a atacar según el ataque seleccionado por el jugador.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ChooseAttackCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'attack'. Este comando selecciona un ataque para
    /// el Pokémon activo del jugador y ordena que lo realice. Si el ataque
    /// seleccionado no es válido, el ataque no se ejecutará.
    /// </summary>
    /// <param name="attackOption">La opción de ataque seleccionada por el jugador. Si no se proporciona, se solicita un ataque válido.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asincrónica de ejecutar el comando.</returns>
    [Command("attack")]
    [Summary(
        """
        Ordena al Pokémon activo del Entrenador a atacar; si el 
        ataque no es el correcto, no lo realizará.
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Opción de ataque que el jugador desea que el Pokémon realice.")]
        string? attackOption = null)
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        

        string result;
        if (attackOption != null)
        {
            result = Facade.Instance.AttackPokemon(displayName, attackOption);
        }
        else
        {
            result = $"Favor de ingresar un ataque valido.";
        }

        await ReplyAsync(result);
    }
}