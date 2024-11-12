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
    [Command("battle")]
    [Summary(
        """
        Ordena al pokemon activo de el Entrenador a atacar; si el 
        ataque no es el correcto no lo realizara.
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Opcion de ataque")]
        string? itemOption, int pokemonOption)
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        

        string result;
        if (itemOption != null && pokemonOption >= 0 && pokemonOption <= 5)
        {
            result = Facade.Instance.UseItem(displayName, pokemonOption, itemOption);
            await Context.Message.Author.SendMessageAsync(result);
        }
        else
        {
            result = $"Favor de ingresar un pokemon u item valido.";
        }

        await ReplyAsync(result);
    }
}