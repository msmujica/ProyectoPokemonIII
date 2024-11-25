using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'showpokemon' del bot. 
/// Este comando retorna la lista de Pokémon disponibles del entrenador especificado.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ShowEnemiesPokemonCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'showpokemon', que muestra los Pokémon disponibles 
    /// en el equipo de un entrenador enemigo.
    /// </summary>
    /// <param name="trainerDisplayName">El nombre del entrenador cuyo equipo de Pokémon se desea mostrar.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asincrónica del comando.</returns>
    /// 
    [Command("showPokemons")]
    [Summary("Devuelve una lista de todos los Pokémon del enemigo disponibles en su equipo.")]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync([Remainder][Summary("El nombre del entrenador del cual se desea ver el equipo de Pokémon.")]
        string trainerDisplayName)
    {
        string result = Facade.Instance.ShowEnemiesPokemon(trainerDisplayName);
        await ReplyAsync(result);
    }
}