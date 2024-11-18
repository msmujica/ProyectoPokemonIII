using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

/// <summary>
/// Esta clase implementa el comando 'choose' del bot. Este comando nos sirve para que el
/// jugador eliga su equipo.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ChoosePokemonCommand : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Implementa el comando 'battle'. Este comando une al jugador que envía el
    /// mensaje a la lista de jugadores esperando para jugar.
    /// </summary>
    [Command("choose")]
    [Summary(
        """
        Un jugador que elije un pokemon para agregar a su propio equipo.;
        """)]
    // ReSharper disable once UnusedMember.Global
    public async Task ExecuteAsync(
        [Remainder]
        [Summary("Display del id del pokemon")]
        int pokemonInt)
    {
        string displayName = CommandHelper.GetDisplayName(Context);

        if (pokemonInt != null)
        {
            string result = Facade.Instance.ChooseTeam(displayName, pokemonInt);
            await ReplyAsync(result);
        }
        else
        {
            await ReplyAsync("Favor de proporcionar un ID valido.");
        }
    }
}