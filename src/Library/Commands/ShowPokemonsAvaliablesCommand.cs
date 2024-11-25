using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'pokemonsAvailable' del bot.
/// Este comando muestra una lista de todos los Pokémon disponibles para ser seleccionados por los entrenadores.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ShowPokemonCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'pokemonsAvailable', que muestra una lista de todos los Pokémon disponibles
    /// que los entrenadores pueden seleccionar para su equipo.
    /// </summary>
    /// <returns>Un <see cref="Task"/> que representa la operación asincrónica del comando.</returns>
    [Command("pokemonsAvaliables")]
    [Summary("Devuelve una lista de todos los Pokémon disponibles para ser seleccionados por los entrenadores.")]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync()
    {
        string result = Facade.Instance.ShowPokémonAvailable();
        await ReplyAsync(result);
    }
}