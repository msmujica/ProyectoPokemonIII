using System;
using System.Collections.Generic;
using Library;
using Library.Items;

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
    public Entrenador Player1 { get; }

    /// <summary>
    /// Obtiene un valor que representa al oponente.
    /// </summary>
    public Entrenador Player2 { get; }

    private Entrenador turnoActual;
    private Entrenador turnoPasado;
    private GestorEfectos gestorEfectos;

    /// <summary>
    /// Obtiene o establece el jugador que está actuando en el turno actual.
    /// </summary>
    public Entrenador TurnoActual
    {
        get { return turnoActual; }
        set { turnoActual = value; }
    }
    
    /// <summary>
    /// Obtiene o establece el jugador que está esperando en el turno pasado.
    /// </summary>
    public Entrenador TurnoPasado
    {
        get { return turnoPasado; }
        set { turnoPasado = value; }
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Battle"/> con los entrenadores proporcionados.
    /// Además, inicializa el gestor de efectos y los ítems de los entrenadores.
    /// </summary>
    /// <param name="player1">El primer jugador (entrenador).</param>
    /// <param name="player2">El segundo jugador (oponente).</param>
    public Battle(Entrenador player1, Entrenador player2)
    {
        this.Player1 = player1;
        this.Player2 = player2;
        this.TurnoActual = player1;
        this.TurnoPasado = player2;
        gestorEfectos = new GestorEfectos();
        player1.SeteodeItems();
        player2.SeteodeItems();
    }

    /// <summary>
    /// Valida si ambos jugadores tienen al menos 6 Pokémon en su equipo.
    /// </summary>
    /// <returns>Devuelve <c>true</c> si algún jugador tiene menos de 6 Pokémon, de lo contrario <c>false</c>.</returns>

    public bool validacionPokemon()
    {
        if (this.Player1.Equipo.Count < 6)
        {
            return true;
        }

        if (this.Player2.Equipo.Count < 6)
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
    public bool ValidacionWin()
    {
        int count = 0;
        foreach (var poke in this.TurnoPasado.Equipo)
        {
            if (poke.Vida <= 0)
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
    public bool ValidacionPokemonVivo()
    {
        if (Player1.Activo.Vida <= 0)
        {
            Player1.CambioPokemonMuerto();
            return true;
        }

        if (Player2.Activo.Vida <= 0)
        {
            Player2.CambioPokemonMuerto();
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
    public string IntermediarioAtacar(string opcionAtaque)
    {
        if (ValidacionPokemonVivo())
        {
            return "Se a cambiado tu pokemon por que murio. Vuelve a realziar el ataque";
            
            
        }
        
        if (ValidacionWin())
        {
            Win();
        }
        
        if (validacionPokemon())
        {
            return "No tenes los pokemones suficientes para empezar la batalla";
        }

        if (this.gestorEfectos.ProcesarControlMasa(this.TurnoActual.Activo))
        {
            this.CambiarTurno();
            return "No se puede";
        }
        
        try
        {
            // Cambiar el Pokémon activo
            string valor = this.TurnoActual.elegirAtaque(opcionAtaque, this.TurnoPasado.Activo, gestorEfectos);
            this.gestorEfectos.ProcesarEfectosDaño();
            this.CambiarTurno();
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
    public string IntermediarioCambiarPokemonActivo(int opcionPokemon)
    {
        validacionPokemon();
        
        if (ValidacionWin())
        {
            Win();
        }
        
        if (validacionPokemon())
        {
            return "No tenes los pokemones suficientes para empezar la batalla";
        }
        
        try
        {
            // Verificar si el índice del Pokémon está en el rango
            if (opcionPokemon < 0 || opcionPokemon >= this.TurnoActual.Equipo.Count)
            {
                return "Selección de Pokémon inválida. Por favor, intenta de nuevo.";
            }

            // Cambiar el Pokémon activo
            string valor = this.TurnoActual.cambiarActivo(opcionPokemon);
            this.gestorEfectos.ProcesarEfectosDaño();
            this.CambiarTurno();
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
        validacionPokemon();
        
        if (ValidacionWin())
        {
            Win();
        }
        
        if (validacionPokemon())
        {
            return "No tenes los pokemones suficientes para empezar la batalla";
        }

        try
        {
            // Verificar si el índice del Pokémon está en el rango
            if (opcionPokemon < 0 || opcionPokemon >= this.TurnoActual.Equipo.Count)
            {
                return "Selección de Pokémon inválida.";
            }

            Pokemon pokemonSeleccionado = this.TurnoActual.Equipo[opcionPokemon];

            // Aplicar el ítem seleccionado al Pokémon
            
            this.gestorEfectos.ProcesarEfectosDaño();
            this.CambiarTurno();
            return this.TurnoActual.UsarItem(opcionItem, pokemonSeleccionado, gestorEfectos);

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
    public void CambiarTurno()
    {
        // Cambiar al otro jugador
        this.TurnoActual = (this.TurnoActual == Player1) ? Player2 : Player1;
        this.TurnoPasado = (this.TurnoPasado == Player2) ? Player1 : Player2;

        Console.WriteLine($"Es el turno de {this.TurnoActual.Nombre}");
    }

    /// <summary>
    /// Muestra los Pokémon del jugador contrario (el que está en turno pasado).
    /// </summary>
    /// <returns>Lista de los Pokémon del oponente.</returns>
    public List<Pokemon> MostrarPokemonEnemigo()
    {
        return this.turnoPasado.Equipo;
    } 

    /// <summary>
    /// Muestra el mensaje de victoria cuando el jugador actual gana la batalla.
    /// </summary>
    /// <returns>Mensaje indicando que el jugador actual ha ganado.</returns>
    public string Win()
    {
        this.TurnoActual = null;
        this.turnoPasado = null;
        return $"El jugador {this.TurnoActual} a ganado";
    }
}