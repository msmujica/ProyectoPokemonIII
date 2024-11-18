using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'showpokemon' del bot.
/// Este comando retorna la lista de Pokémon disponibles.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ShowEnemiesPokemonCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'showpokemon', que muestra los Pokémon disponibles.
    /// </summary>
    [Command("showPokemons")]
    [Summary("Devuelve una lista de todos los Pokémon del enemigo disponibles.")]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync([Remainder][Summary("El nombre del entrenador del cual se desea ver el equipo de Pokémon.")]
        string trainerDisplayName)
    {
        string result = Facade.Instance.ShowEnemiesPokemon(trainerDisplayName);
        await ReplyAsync(result);
    }
}