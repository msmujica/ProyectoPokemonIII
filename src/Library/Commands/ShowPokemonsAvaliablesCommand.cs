using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'showpokemon' del bot.
/// Este comando retorna la lista de Pokémon disponibles.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ShowPokemonCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'showpokemon', que muestra los Pokémon disponibles.
    /// </summary>
    [Command("pokemonsAvaliables")]
    [Summary("Devuelve una lista de todos los Pokémon disponibles.")]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync()
    {
        string result = Facade.Instance.ShowPokémonAvailable();
        await ReplyAsync(result);
    }
}