using Library;

namespace Ucu.Poo.DiscordBot.Domain;

/// <summary>
/// Representa una batalla entre dos entrenadores, gestionando turnos, ataques, cambios de Pokémon y uso de ítems.
/// Esta clase también se encarga de validar las condiciones de victoria y de manejar los efectos de estado durante la batalla.
///La clase Battle respeta los siguientes principios:
/// •	SRP: La clase Battle se encarga solo de gestionar la lógica de la batalla (turnos, ataques, cambios de Pokémon, uso de ítems, y validaciones de victoria), lo que le da una única responsabilidad.
/// •	LSP: Los entrenadores (Entrenador) y los Pokémon (Pokemon) son objetos que pueden ser sustituidos por sus subclases sin romper la funcionalidad del sistema, lo que permite que diferentes tipos de entrenadores o Pokémon sean intercambiables en la batalla.
/// •	ISP: Aunque la clase Battle no implementa interfaces explícitas, sigue la filosofía de ISP al no sobrecargar a otras clases con métodos innecesarios; cada clase se encarga de un conjunto limitado de operaciones.
/// •	DIP: La clase Battle depende de abstracciones como GestorEfectos en lugar de clases concretas, lo que facilita la extensión o modificación de la gestión de efectos sin alterar la clase Battle.
/// </summary>
public class Battle
{
    /// <summary>
    /// Obtiene un valor que representa el primer jugador.
    /// </summary>
    public Trainer Player1 { get; }

    /// <summary>
    /// Obtiene un valor que representa al oponente.
    /// </summary>
    public Trainer Player2 { get; }

    private Trainer actualTurn;
    private Trainer lastTurn;
    private EffectsManager effectsManager;

    /// <summary>
    /// Obtiene o establece el jugador que está actuando en el turno actual.
    /// </summary>
    public Trainer ActualTurn
    {
        get { return actualTurn; }
        set { actualTurn = value; }
    }
    
    /// <summary>
    /// Obtiene o establece el jugador que está esperando en el turno pasado.
    /// </summary>
    public Trainer LastTurn
    {
        get { return lastTurn; }
        set { lastTurn = value; }
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Battle"/> con los entrenadores proporcionados.
    /// Además, inicializa el gestor de efectos y los ítems de los entrenadores.
    /// </summary>
    /// <param name="player1">El primer jugador (entrenador).</param>
    /// <param name="player2">El segundo jugador (oponente).</param>
    public Battle(Trainer player1, Trainer player2)
    {
        Player1 = player1;
        Player2 = player2;
        ActualTurn = player1;
        LastTurn = player2;
        effectsManager = new EffectsManager();
        player1.ItemSetting();
        player2.ItemSetting();
    }

    /// <summary>
    /// Valida si ambos jugadores tienen al menos 6 Pokémon en su equipo.
    /// </summary>
    /// <returns>Devuelve <c>true</c> si algún jugador tiene menos de 6 Pokémon, de lo contrario <c>false</c>.</returns>

    public bool PokemonValidation()
    {
        if (Player1.Team.Count < 6)
        {
            return true;
        }

        if (Player2.Team.Count < 6)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Valida si el jugador actual ha ganado la batalla. 
    /// Se considera una victoria cuando todos los Pokémon del oponente tienen vida negativa.
    /// </summary>
    /// <returns>Devuelve <c>true</c> si el jugador ha ganado la batalla.</returns>
    public bool WinValidation()
    {
        int count = 0;
        foreach (var poke in LastTurn.Team)
        {
            if (poke.Health <= 0)
            {
                count++;
            }
        }

        if (count == 6)
        {
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// Valida el estado de los Pokémon activos de ambos jugadores.
    /// Si alguno de los Pokémon está muerto (vida <= 0), se realiza un cambio de Pokémon.
    /// </summary>
    public bool AlivePokemonValidation()
    {
        if (Player1.Active.Health <= 0)
        {
            Player1.ChangeDefeatedPokemon();
            return true;
        }

        if (Player2.Active.Health <= 0)
        {
            Player2.ChangeDefeatedPokemon();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Intermediario para realizar un ataque en la batalla.
    /// Valida la acción de atacar, gestiona los efectos de daño y cambia el turno al siguiente jugador.
    /// </summary>
    /// <param name="opcionAtaque">El nombre del ataque seleccionado por el jugador.</param>
    /// <returns>Mensaje que describe el resultado de realizar el ataque.</returns>
    public string IntermediaryAttack(string opcionAtaque)
    {
        if (AlivePokemonValidation())
        {
            return "Se a cambiado tu pokemon por que murio. Vuelve a realziar el ataque";
        }
        
        if (WinValidation())
        {
            Win();
        }
        
        if (PokemonValidation())
        {
            return "No tenes los pokemones suficientes para empezar la batalla";
        }

        if (effectsManager.ProcessMassControl(ActualTurn.Active))
        {
            ChangeTurn();
            return "No se puede";
        }
        
        try
        {
            string valor = ActualTurn.elegirAtaque(opcionAtaque, LastTurn.Active, effectsManager);
            effectsManager.ProcessDamageEffects();
            ChangeTurn();
            return valor;
        }
        catch (FormatException)
        {
            Console.WriteLine("Entrada inválida. Asegúrate de ingresar el nombre correcto.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }

        return "El ataque se a realizado con exito";
    }

    /// <summary>
    /// Intermediario para cambiar el Pokémon activo durante el turno del jugador.
    /// Valida que el índice del Pokémon esté en el rango del equipo y realiza el cambio de Pokémon.
    /// </summary>
    /// <param name="opcionPokemon">Índice del Pokémon seleccionado para ser el nuevo activo.</param>
    /// <returns>Mensaje que describe el resultado del cambio de Pokémon.</returns>
    public string IntermediaryChangeActivePokemon(int opcionPokemon)
    {
        PokemonValidation();
        
        if (WinValidation())
        {
            Win();
        }
        
        if (PokemonValidation())
        {
            return "No tenes los pokemones suficientes para empezar la batalla";
        }
        
        try
        {
            // Verificar si el índice del Pokémon está en el rango
            if (opcionPokemon < 0 || opcionPokemon >= ActualTurn.Team.Count)
            {
                return "Selección de Pokémon inválida. Por favor, intenta de nuevo.";
            }

            // Cambiar el Pokémon activo
            string valor = ActualTurn.cambiarActivo(opcionPokemon);
            effectsManager.ProcessDamageEffects();
            ChangeTurn();
            return valor;
        }
        catch (FormatException)
        {
            Console.WriteLine("Entrada inválida. Asegúrate de ingresar un número.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }

        return "Hecho";
    }


    /// <summary>
    /// Intermediario para usar un ítem en el Pokémon activo durante la batalla.
    /// Valida el índice del Pokémon y aplica el ítem seleccionado.
    /// </summary>
    /// <param name="opcionPokemon">Índice del Pokémon sobre el que se aplicará el ítem.</param>
    /// <param name="opcionItem">Nombre del ítem a usar.</param>
    /// <returns>Mensaje que describe el resultado del uso del ítem.</returns>
    public string IntermediarioUsarItem(int opcionPokemon, string opcionItem)
    {
        PokemonValidation();
        
        if (WinValidation())
        {
            Win();
        }
        
        if (PokemonValidation())
        {
            return "No tenes los pokemones suficientes para empezar la batalla";
        }

        try
        {
            // Verificar si el índice del Pokémon está en el rango
            if (opcionPokemon < 0 || opcionPokemon >= ActualTurn.Team.Count)
            {
                return "Selección de Pokémon inválida.";
            }

            Pokemon pokemonSeleccionado = ActualTurn.Team[opcionPokemon];

            // Aplicar el ítem seleccionado al Pokémon
            
            effectsManager.ProcessDamageEffects();
            ChangeTurn();
            return ActualTurn.UsarItem(opcionItem, pokemonSeleccionado, effectsManager);

        }
        catch (FormatException)
        {
            Console.WriteLine("Entrada inválida. Asegúrate de ingresar un número.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }

        return "Atravido";
    }

    /// <summary>
    /// Cambia el turno entre los dos jugadores. Resetea el estado de acción y determina quién es el siguiente jugador.
    /// </summary>
    public void ChangeTurn()
    {
        // Cambiar al otro jugador
        ActualTurn = (ActualTurn == Player1) ? Player2 : Player1;
        LastTurn = (LastTurn == Player2) ? Player1 : Player2;

        Console.WriteLine($"Es el turno de {ActualTurn.Team}");
    }

    /// <summary>
    /// Muestra los Pokémon del jugador contrario (el que está en turno pasado).
    /// </summary>
    /// <returns>Lista de los Pokémon del oponente.</returns>
    public List<Pokemon> MostrarPokemonEnemigo()
    {
        return LastTurn.Team;
    } 

    /// <summary>
    /// Muestra el mensaje de victoria cuando el jugador actual gana la batalla.
    /// </summary>
    /// <returns>Mensaje indicando que el jugador actual ha ganado.</returns>
    public string Win()
    {
        ActualTurn = null;
        LastTurn = null;
        return $"El jugador {ActualTurn} a ganado";
    }
}