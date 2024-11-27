using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/* 
Buenas Dias!
Si bien funciona y esta correctamente hecho no es exactamente respetando algunos principios.
A decir verdad no se me ocurrió otra forma de realizarlo a no se de esta hasta las 11AM.
En ese horario me di cuenta que para poder respetar muchos principios y de mas y que sea un buen uso de código tuve que realizar una clase llamada reglas que se comunicara con battle, una ves de ahi a cada accion desde el propio facade u accion que realzara se debería de realizar la validación previa. Esta clase también permite agregar nuevas reglas sin modificar todo el código provisto desde el inicio.

Tome la decision de no realizar esta opción ya que era muy tarde y pensé que es preferible entregar el código funcionando pero no respetando el principio OCP entre otros a que entregar todo a medio hacer.
 */

/// <summary>
/// Esta clase implementa el comando 'battle' del bot. Este comando une al
/// jugador que envía el mensaje con el oponente que se recibe como parámetro,
/// si lo hubiera, en una batalla; si no se recibe un oponente, lo une con
/// cualquiera que esté esperando para jugar.
/// </summary>
public class BattleCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'battle'. Este comando une al jugador que envía el
    /// mensaje a la lista de jugadores esperando para jugar.
    /// </summary>
    [Command("battle")]
    [Summary(
        """
        Une al jugador que envía el mensaje con el oponente que se recibe
        como parámetro, si lo hubiera, en una batalla; si no se recibe un
        oponente, lo une con cualquiera que esté esperando para jugar.
        """)]

    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Display name del oponente, opcional")]
        string? battleSince)
    {
        string displayName = CommandHelper.GetDisplayName(Context);
        
        string[] options = battleSince.Split(",");
        
        SocketGuildUser? opponentUser = CommandHelper.GetUser(
            Context, options[0]);

        string rTipos = options[1];
        string rPokemon = options[2];
        string rItems = options[3];
        
        string result;
        if (opponentUser != null)
        {
            result = Facade.Instance.StartBattle(displayName, opponentUser.DisplayName, rTipos, rPokemon, rItems);
        }
        else
        {
            result = $"No hay un usuario {opponentUser}";
        }
        
        await ReplyAsync(result);
    }
}