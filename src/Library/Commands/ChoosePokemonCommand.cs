using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Implementa el comando 'choose'. Este comando permite que un jugador
/// elija un Pokémon utilizando su ID para agregarlo a su equipo.
/// </summary>
/// <param name="pokemonInt">El ID del Pokémon que el jugador desea agregar a su equipo.</param>
/// <returns>Un <see cref="Task"/> que representa la operación asincrónica de ejecutar el comando.</returns>
// ReSharper disable once UnusedType.Global
public class ChoosePokemonCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'battle'. Este comando une al jugador que envía el
    /// mensaje a la lista de jugadores esperando para jugar.
    /// </summary>
    [Command("choose")]
    [Summary(
        """
        Permite al jugador elegir un Pokémon utilizando su ID para agregarlo a su equipo.
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("El ID del Pokémon a elegir.")]
        int pokemonInt)
    {
        string displayName = CommandHelper.GetDisplayName(Context);

        if (pokemonInt != null)
        {
            string result = Facade.Instance.ChooseTeam(displayName, pokemonInt);
            await ReplyAsync(result);
        }
        else
        {
            await ReplyAsync("Favor de proporcionar un ID valido.");
        }
    }
}