using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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

public class Entrenador
{
    private string nombre;
    private List<Pokemon> equipo;
    private Pokemon activo;

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public List<Pokemon> Equipo
    {
        get { return equipo; }
        set { equipo = value; }
    }

    public Pokemon Activo
    {
        get { return activo; }
        set { activo = value; }
    }
    public int ContadorSuperPocion { get; set; }
    public int ContadorRevivir { get; set; }
    public int ContadorCuraTotal { get; set; }
    
    private GestorDeItems gestorDeItems;

    
    /// <summary>
    /// Inicializa un nuevo entrenador con el nombre especificado.
    /// </summary>
    /// <param name="nombre">El nombre del entrenador.</param>
    public Entrenador(string nombre)
    {
        this.Nombre = nombre;
        this.Equipo = new List<Pokemon>();
        this.Activo = null;
        gestorDeItems = new GestorDeItems(); 
    }
    
    /// <summary>
    /// Permite al entrenador elegir un Pokémon para agregar a su equipo.
    /// </summary>
    /// <param name="numero">El número del Pokémon que se desea agregar al equipo.</param>
    /// <returns>Un mensaje indicando si el Pokémon fue agregado con éxito o si el equipo está lleno.</returns>

    public string elegirEquipo(int numero)
    {
        if (this.Equipo.Count >= 6)
        {
            return "Ya tienes la cantidad maxima de Pokemones en tu Equipo";
        }
        Pokedex.CrearPokemonPorIndice(numero, this);
        return "Vuelve a ejecutar el mismo comando para poder empezar la batalla";
    }

    /// <summary>
    /// Muestra todos los Pokémon que el entrenador tiene en su Equipo.
    /// </summary>
    public void MostrarmiPokedex()
    {
        int numero = 0;
        Console.WriteLine("Lista de Pokemones en tu Pokedex");
        foreach (var lista in this.Equipo)
        {
            Console.WriteLine($"{numero} - {lista.Nombre}");
            numero += 1;
        }
    }
    
    /// <summary>
    /// Cambia el Pokémon activo del entrenador.
    /// </summary>
    /// <param name="indexPokemonList">El índice del Pokémon en el equipo que se quiere hacer activo.</param>
    /// <returns>El nombre del Pokémon activo, o un mensaje de error si el índice es inválido.</returns>

    public string cambiarActivo(int indexPokemonList)
    {
        if (indexPokemonList >= 0 && indexPokemonList < this.Equipo.Count)
        {
            this.Activo = this.Equipo[indexPokemonList];
            return this.Activo.Nombre;
        }
        else
        {
            return ("Indice no valido. No se pudo cambiar el pokemon");
        }
    }
    
    /// <summary>
    /// Elige un ataque para que el Pokémon activo ataque a un oponente.
    /// </summary>
    /// <param name="nombre">El nombre del ataque a utilizar.</param>
    /// <param name="oponente">El Pokémon oponente que recibirá el ataque.</param>
    /// <param name="gestorEfectos">El gestor de efectos que maneja los efectos adicionales del ataque.</param>
    /// <returns>El resultado de la acción de atacar.</returns>
    public string elegirAtaque(string nombre, Pokemon oponente, GestorEfectos gestorEfectos)
    {
        return this.activo.atacar(oponente, nombre, gestorEfectos);
    }

    /// <summary>
    /// Utiliza un ítem en un Pokémon durante la batalla.
    /// </summary>
    /// <param name="nombreItem">El nombre del ítem a usar (Superpocion, Revivir, CuraTotal).</param>
    /// <param name="pokemon">El Pokémon sobre el que se usará el ítem.</param>
    /// <param name="gestorEfectos">El gestor de efectos que maneja los efectos del ítem.</param>
    /// <returns>Un mensaje indicando el resultado de usar el ítem.</returns>
    public string UsarItem(string nombreItem, Pokemon pokemon, GestorEfectos gestorEfectos)
    {
        string valor = null;
        switch (nombreItem)
        {
            case "Superpocion":
                valor = gestorDeItems.UsarSuperPocion(pokemon, ContadorSuperPocion);
                break;
            case "Revivir":
                valor = gestorDeItems.UsarRevivir(pokemon, ContadorRevivir);
                break;
            case "CuraTotal":
                valor = gestorDeItems.UsarCuraTotal(pokemon, ContadorCuraTotal, gestorEfectos);
                break;
            default:
                Console.WriteLine("Ítem no válido.");
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
        foreach (var pok in this.Equipo)
        {
            if (!pok.EstaDerrotado && count == 0)
            {
                this.Activo = pok;
                count++;
                
                
            }
        }

    }

    /// <summary>
    /// Inicializa los contadores de ítems disponibles para el entrenador.
    /// </summary>
    public void SeteodeItems()
    {
        this.ContadorSuperPocion = 4;
        this.ContadorCuraTotal = 2;
        this.ContadorRevivir = 1;
    }
}