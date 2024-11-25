using Library.Items;

namespace Library;

/// <summary>
/// Esta clase representa un entrenador que participa en batallas de Pokémon.
/// El entrenador tiene un equipo de Pokémon, uno activo y puede usar ítems durante la batalla.
/// La clase Entrenador aplica los siguientes principios:
/// •	SRP: Centraliza la gestión del equipo de Pokémon y el uso de ítems, manteniendo responsabilidades claras y específicas.
/// •	Expert: Entrenador conoce y gestiona toda la información necesaria sobre su equipo y los ítems, consolidando las responsabilidades de su rol.
/// •	Low Coupling: Limita el acoplamiento al delegar los efectos especiales en otras clases, manteniéndose independiente de sus implementaciones internas.
/// •	High Cohesion: Agrupa métodos coherentes y directamente relacionados con la gestión de su equipo y uso de ítems, manteniendo cohesión en sus responsabilidades.

/// </summary>

public class Trainer
{
    private string name;
    private List<Pokemon> team;
    private Pokemon active;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public List<Pokemon> Team
    {
        get { return team; }
        set { team = value; }
    }

    public Pokemon Active
    {
        get { return active; }
        set { active = value; }
    }
    public int CounterSuperPotion { get; set; }
    public int CounterRevive { get; set; }
    public int CounterTotalCure { get; set; }
    
    private ItemsManager itemsManager;


    /// <summary>
    /// Inicializa un nuevo entrenador con el nombre especificado.
    /// </summary>
    /// <param name="nombre">El nombre del entrenador.</param>
    public Trainer(string name)
    {
        Name = name;
        Team = new List<Pokemon>();
        Active = null;

    itemsManager = new ItemsManager(); 
    }
    
    /// <summary>
    /// Permite al entrenador elegir un Pokémon para agregar a su equipo.
    /// </summary>
    /// <param name="numero">El número del Pokémon que se desea agregar al equipo.</param>
    /// <returns>Un mensaje indicando si el Pokémon fue agregado con éxito o si el equipo está lleno.</returns>

    public string ChooseTeam(int number)
    {
        if (Team.Count >= 6)
        {
            return "Ya tienes la cantidad maxima de Pokemones en tu Equipo";
        }
        Pokedex.CreatePokemonByIndex(number, this);
        string name = Pokedex.ShowPokemonByIndex(number);
        return $"El pokemon {name} se agrego a la lista, quedan {6 - Team.Count} espacios.";
    }
    
    /// <summary>
    /// Cambia el Pokémon activo del entrenador.
    /// </summary>
    /// <param name="indexPokemonList">El índice del Pokémon en el equipo que se quiere hacer activo.</param>
    /// <returns>El nombre del Pokémon activo, o un mensaje de error si el índice es inválido.</returns>

    public string ChangeActive(int indexPokemonList)
    {
        if (indexPokemonList >= 0 && indexPokemonList < Team.Count)
        {
            Active = Team[indexPokemonList];
            return Active.Name;
        }

        return ("Indice no valido. No se pudo cambiar el pokemon");
    }
    
    /// <summary>
    /// Elige un ataque para que el Pokémon activo ataque a un oponente.
    /// </summary>
    /// <param name="nombre">El nombre del ataque a utilizar.</param>
    /// <param name="oponente">El Pokémon oponente que recibirá el ataque.</param>
    /// <param name="effectsManager">El gestor de efectos que maneja los efectos adicionales del ataque.</param>
    /// <returns>El resultado de la acción de atacar.</returns>
    public string ChooseAttack(string name, Pokemon opponent, EffectsManager effectsManager)
    {
        return active.attacks(opponent, name, effectsManager);
    }

    /// <summary>
    /// Utiliza un ítem en un Pokémon durante la batalla.
    /// </summary>
    /// <param name="nombreItem">El nombre del ítem a usar (Superpocion, Revivir, CuraTotal).</param>
    /// <param name="pokemon">El Pokémon sobre el que se usará el ítem.</param>
    /// <param name="effectsManager">El gestor de efectos que maneja los efectos del ítem.</param>
    /// <returns>Un mensaje indicando el resultado de usar el ítem.</returns>
    public string UsarItem(string nombreItem, Pokemon pokemon, EffectsManager effectsManager)
    {
        string valor = null;
        switch (nombreItem)
        {
            case "Superpocion":
                valor = itemsManager.UsarSuperPocion(pokemon, CounterSuperPotion);
                break;
            case "Revivir":
                valor = itemsManager.UsarRevivir(pokemon, CounterRevive);
                break;
            case "CuraTotal":
                valor = itemsManager.UsarCuraTotal(pokemon, CounterTotalCure, effectsManager);
                break;
            default:
                valor = "Item no valido!";
                break;
        }

        return valor;
    }

    /// <summary>
    /// Cambia al siguiente Pokémon disponible en el equipo si el Pokémon activo está muerto.
    /// </summary>
    public void CambioPokemonMuerto()
    {
        int count = 0;
        foreach (var pok in Team)
        {
            if (!pok.IsDefeated && count == 0)
            {
                Active = pok;
                count++;
                
                
            }
        }

    }

    /// <summary>
    /// Inicializa los contadores de ítems disponibles para el entrenador.
    /// </summary>
    public void ItemSetting()
    {
        CounterSuperPotion = 4;
        CounterTotalCure = 2;
        CounterRevive = 1;
    }
}