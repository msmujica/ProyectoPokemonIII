using System.Collections.ObjectModel;
using Library;

namespace Ucu.Poo.DiscordBot.Domain;

/// <summary>
/// Esta clase representa la lista de jugadores esperando para jugar.
/// </summary>
public class WaitingList
{
    private readonly List<Entrenador> trainers = new List<Entrenador>();

    public int Count
    {
        get { return trainers.Count; }
    }

    public ReadOnlyCollection<Entrenador> GetAllWaiting()
    {
        return trainers.AsReadOnly();
    }
    
    /// <summary>
    /// Agrega un jugador a la lista de espera.
    /// </summary>
    /// <param name="displayName">El nombre de usuario de Discord en el servidor
    /// del bot a agregar.
    /// </param>
    /// <returns><c>true</c> si se agrega el usuario; <c>false</c> en caso
    /// contrario.</returns>
    public bool AddTrainer(string displayName)
    {
        if (string.IsNullOrEmpty(displayName))
        {
            throw new ArgumentException(nameof(displayName));
        }
        
        if (FindTrainerByDisplayName(displayName) != null) return false;
        trainers.Add(new Entrenador(displayName));
        return true;

    }

    /// <summary>
    /// Remueve un jugador de la lista de espera.
    /// </summary>
    /// <param name="displayName">El nombre de usuario de Discord en el servidor
    /// del bot a remover.
    /// </param>
    /// <returns><c>true</c> si se remueve el usuario; <c>false</c> en caso
    /// contrario.</returns>
    public bool RemoveTrainer(string displayName)
    {
        Entrenador? trainer = FindTrainerByDisplayName(displayName);
        if (trainer == null) return false;
        trainers.Remove(trainer);
        return true;

    }

    /// <summary>
    /// Busca un jugador por el nombre de usuario de Discord en el servidor del
    /// bot.
    /// </summary>
    /// <param name="displayName">El nombre de usuario de Discord en el servidor
    /// del bot a buscar.
    /// </param>
    /// <returns>El jugador encontrado o <c>null</c> en caso contrario.
    /// </returns>
    public Entrenador? FindTrainerByDisplayName(string displayName)
    {
        foreach (Entrenador trainer in trainers)
        {
            if (trainer.Nombre == displayName)
            {
                return trainer;
            }
        }

        return null;
    }

    /// <summary>
    /// Retorna un jugador cualquiera esperando para jugar. En esta
    /// implementación provista no es cualquiera, sino el primero. En la
    /// implementación definitiva, debería ser uno aleatorio.
    /// 
    /// </summary>
    /// <returns></returns>
    public Entrenador? GetAnyoneWaiting()
    {
        if (trainers.Count == 0)
        {
            return null;
        }

        return trainers[0];
    }
}