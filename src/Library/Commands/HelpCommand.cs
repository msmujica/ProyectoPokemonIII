using Discord.Commands;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Comando para mostrar los ataques de un Pokémon en el equipo de un entrenador.
/// </summary>
public class HelpCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Muestra los ataques del Pokémon especificado en el equipo de un entrenador.
    /// </summary>
    /// <param name="trainerDisplayName">El nombre del entrenador que tiene el Pokémon.</param>
    /// <param name="pokemonName">El nombre del Pokémon del cual se desean ver los ataques.</param>
    [Command("Help")]
    [Summary("Muestra posibles comandos.")]
    public async Task ExecuteAsync(){
        // Construir el mensaje con los ataques del Pokémon
        string result = $"\n*!Join - Para entrar a una lista de espera.\n" +
                        $"*!who - Para ver los aprticipantes.\n" +
                        $"*!Battle <Oponente> - Para entrar a una batalla, poner el nombre del oponente.\n" +
                        $"*!Choose <Pokemon> - Agrear un pokemon a tu equipo, se debe de poner el numero del pokemon. \n" +
                        $"*!Attack <Ataque> - Para atacar a un pokemon, poner el nombre del ataque. \n" +
                        $"*!Item <Pokemon>, <Item> - Para usar un item se debe de agregar el numero de pokemon y el nombre del item a usar. \n" +
                        $"*!pokemonsAvaliables - Muestra los pokemones disponibles para elegir en tu equipo. \n" +
                        $"*!showPokemons <Player> - Muestra el pokemon del nombre de jugador que pusiste. \n" +
                        $"*!Surrender - Te rendis y puedes jugador otra batalla.\n" +
                        $"*!change <Pokemon> - Cambia el pokemon activo, para cambiarlo ingresa el indice de tu equipo. \n" +
                        $"*!getattacks -  Muestra los ataques del pokemon activo.";
        // Enviar el mensaje al canal
        await ReplyAsync($"Comandos disponibles:" + result.TrimEnd()); // Elimina cualquier salto de línea adicional al final
    }
}