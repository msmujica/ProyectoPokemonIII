using System.Collections;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'item' del bot. Este comando permite
/// al jugador usar un ítem disponible en su inventario para su Pokémon.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ChooseItemCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'item'. Este comando permite seleccionar un
    /// ítem de la lista de opciones y usarlo con un Pokémon especificado.
    /// </summary>
    /// <param name="optionList">Una cadena que contiene los detalles del ítem y el Pokémon al que se le usará el ítem, separados por coma. Ejemplo: "poción, Pikachu".</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asincrónica de ejecutar el comando.</returns>
    [Command("item")]
    [Summary(
        """
        Ordena al Pokémon activo del Entrenador a usar un ítem del inventario; si el 
        ítem o Pokémon no son válidos, no se realizará la acción.
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Lista de ítem y Pokémon a usar, separados por coma. Ejemplo: 'poción,Pikachu'")]
        string? optionList = null)
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        string[] options = optionList.Split(",");

        string result;
        if (options != null)
        {
            result = Facade.Instance.UseItem(displayName, Int32.Parse(options[1]), options[0]);
        }
        else
        {
            result = $"Favor de ingresar un pokemon u item valido.";
        }

        await ReplyAsync(result);
    }
}