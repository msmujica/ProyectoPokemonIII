using Library;

namespace Ucu.Poo.DiscordBot.Domain;

/// <summary>
/// Esta clase representa la lista de batallas en curso.
/// </summary>
public class BattlesList
{
    private List<Battle> battles = new List<Battle>();

    /// <summary>
    /// Crea una nueva batalla entre dos jugadores y la agrega a la lista de batallas.
    /// </summary>
    /// <param name="player1">El primer jugador (entrenador).</param>
    /// <param name="player2">El segundo jugador (oponente).</param>
    /// <returns>La nueva batalla creada.</returns>
    public Battle AddBattle(Entrenador player1, Entrenador player2)
    {
        var battle = new Battle(player1, player2);
        battles.Add(battle);
        return battle;
    }
    
    /// <summary>
    /// Busca un entrenador en todas las batallas por su nombre de pantalla (display name).
    /// </summary>
    /// <param name="displayName">El nombre de pantalla del entrenador a buscar.</param>
    /// <returns>El entrenador encontrado o <c>null</c> si no se encuentra en ninguna batalla.</returns>
    public Entrenador? FindTrainerByDisplayName(string displayName)
    {
        foreach (Battle batlle in battles)
        {
            if (batlle.Player1.Nombre == displayName)
            {
                return batlle.Player1;
            }

            if (batlle.Player2.Nombre == displayName)
            {
                return batlle.Player2;
            }
        }

        return null;
    }

    public void removeBatlle(Battle batt)
    {
        this.battles.Remove(batt);
    }
    
    /// <summary>
    /// Busca una batalla en la lista por el nombre de pantalla de uno de los jugadores.
    /// </summary>
    /// <param name="displayName">El nombre de pantalla de uno de los jugadores a buscar.</param>
    /// <returns>La batalla encontrada o <c>null</c> si no se encuentra ninguna batalla con ese jugador.</returns>
    public Battle? FindBattleByDisplayName(string displayName)
    {
        foreach (Battle batlle in battles)
        {
            if (batlle.Player1.Nombre == displayName)
            {
                return batlle;
            }

            if (batlle.Player2.Nombre == displayName)
            {
                return batlle;
            }
        }

        return null;
    }
}