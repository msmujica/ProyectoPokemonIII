using System.Collections;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'item' del bot. Este comando usa uno
/// de los items disponible del entrenador.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ChooseItemCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'item'. Este commando selecciona
    /// un item y un pokemon, luego lo usa.
    /// </summary>
    [Command("item")]
    [Summary(
        """
        Ordena al pokemon activo de el Entrenador a atacar; si el 
        ataque no es el correcto no lo realizara.
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Opcion de ataque")]
        string? optionList = null)
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        string[] options = optionList.Split(",");

        string result;
        if (options == null)
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