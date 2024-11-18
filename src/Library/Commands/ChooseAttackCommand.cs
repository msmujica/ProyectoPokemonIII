using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'attack' del bot. Este comando ordena
/// a un pokemon a atacar segun el ataque, el ataque se selecciona segun el
/// parametro que nos pasa el usuario
/// </summary>
// ReSharper disable once UnusedType.Global
public class ChooseAttackCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'attack'. Este commando selecciona
    /// un ataque y ordena atacar.
    /// </summary>
    [Command("attack")]
    [Summary(
        """
        Ordena al pokemon activo de el Entrenador a atacar; si el 
        ataque no es el correcto no lo realizara.
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Opcion de ataque")]
        string? attackOption = null)
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        

        string result;
        if (attackOption != null)
        {
            result = Facade.Instance.AttackPokemon(displayName, attackOption);
        }
        else
        {
            result = $"Favor de ingresar un ataque valido.";
        }

        await ReplyAsync(result);
    }
}