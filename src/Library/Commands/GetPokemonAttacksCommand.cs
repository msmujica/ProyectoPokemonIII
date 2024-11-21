using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Comando para mostrar los ataques de un Pokémon en el equipo de un entrenador.
/// </summary>
public class GetPokemonAttacksCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Muestra los ataques del Pokémon especificado en el equipo de un entrenador.
    /// </summary>
    /// <param name="trainerDisplayName">El nombre del entrenador que tiene el Pokémon.</param>
    /// <param name="pokemonName">El nombre del Pokémon del cual se desean ver los ataques.</param>
    [Command("getAttacks")]
    [Summary("Muestra los ataques de un Pokémon en el equipo de un entrenador.")]
    public async Task ExecuteAsync(){
        string displayName = CommandHelper.GetDisplayName(Context);
        // Construir el mensaje con los ataques del Pokémon
        string result = $"{Facade.Instance.GetPokemonAtacks(displayName)}";
        // Enviar el mensaje al canal
        await ReplyAsync(result.TrimEnd()); // Elimina cualquier salto de línea adicional al final
    }
}