namespace Library;

/// <summary>
/// Esta clase gestiona la Pokedex, permitiendo la creación de Pokémon a partir de un índice,
/// mostrar la lista de Pokémon en la Pokedex, y obtener detalles sobre un Pokémon en particular.
/// La clase Pokedex sigue los siguientes principios:
/// •	Expert: Pokedex centraliza la creación de Pokémon y el manejo de sus nombres y tipos, actuando como el experto en esta información.
/// •	Low Coupling: La clase mantiene un acoplamiento bajo al estar limitada a gestionar solo los datos básicos de los Pokémon, sin involucrarse en detalles de lógica de batalla.
/// •	High Cohesion: La clase tiene cohesión alta, ya que sus métodos están directamente relacionados con el manejo de los Pokémon en la Pokedex.
/// •	Creator: Pokedex es responsable de crear objetos de tipo Pokemon, lo que facilita la extensibilidad al permitir la incorporación de nuevos Pokémon.
/// </summary>
public static class Pokedex
{
    private static List<string> pokemonNames = new List<string>
    {
        "Squirtle", // Agua
        "Caterpie", // Bicho
        "Dratini", // Dragón
        "Pikachu", // Eléctrico
        "Gastly", // Fantasma
        "Charmander", // Fuego
        "Jynx", // Hielo
        "Machop", // Lucha
        "Eevee", // Normal
        "Bulbasaur", // Planta
        "Abra", // Psíquico
        "Geodude", // Roca
        "Diglett", // Tierra
        "Ekans", // Veneno
        "Pidgey" // Volador
    };

    private static List<string> pokemonTypes = new List<string>
    {
        "Agua",
        "Bicho",
        "Dragón",
        "Eléctrico",
        "Fantasma",
        "Fuego",
        "Hielo",
        "Lucha",
        "Normal",
        "Planta",
        "Psíquico",
        "Roca",
        "Tierra",
        "Veneno",
        "Volador"
    };

    /// <summary>
    /// Muestra el nombre de un Pokémon dado su índice en la Pokedex.
    /// </summary>
    /// <param name="indice">El índice del Pokémon en la Pokedex.</param>
    /// <returns>El nombre del Pokémon en la Pokedex en el índice especificado.</returns>
    public static string ShowPokemonByIndex(int index)
    {
        return pokemonNames[index];
    }

    /// <summary>
    /// Muestra una lista con todos los Pokémon en la Pokedex, junto con su tipo.
    /// </summary>
    /// <returns>Una lista de cadenas que representan los Pokémon en la Pokedex, con su nombre y tipo.</returns>
    public static List<string> ShowPokedex()
    {
        List<string> pokedexList = new List<string>();
        for (int i = 0; i < pokemonNames.Count; i++)
        {
            pokedexList.Add($"{i} - {pokemonNames[i]} ({pokemonTypes[i]})");
        }

        return pokedexList;
    }

    /// <summary>
    /// Crea un Pokémon a partir de su índice en la Pokedex y lo agrega al equipo del entrenador.
    /// </summary>
    /// <param name="indice">El índice del Pokémon en la Pokedex.</param>
    /// <param name="trainer">El entrenador al que se le asignará el Pokémon creado.</param>
    /// <returns>El Pokémon creado, o null si el índice es inválido.</returns>
    public static Pokemon CreatePokemonByIndex(int index, Trainer trainer)
    {
        int initialLife = 100;
        Pokemon newPokemon = null;

        switch (index)
        {
            case 0:
                newPokemon = new Pokemon("Squirtle", initialLife, new List<string> { "Pistola Agua", "Hidrobomba", "Burbuja" }, "Agua");
                break;
            case 1:
                newPokemon = new Pokemon("Caterpie", initialLife, new List<string> { "Picadura", "Pulso Bicho", "Tijera X" }, "Bicho");
                break;
            case 2:
                newPokemon = new Pokemon("Dratini", initialLife, new List<string> { "Garra Dragón", "Cometa Draco", "Aliento Dragón" }, "Dragón");
                break;
            case 3:
                newPokemon = new Pokemon("Pikachu", initialLife, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
                break;
            case 4:
                newPokemon = new Pokemon("Gastly", initialLife, new List<string> { "Bola Sombra", "Puño Spectral", "Puño Sombrío" }, "Fantasma");
                break;
            case 5:
                newPokemon = new Pokemon("Charmander", initialLife, new List<string> { "Llamarada", "Lanzallamas", "Ascuas" }, "Fuego");
                break;
            case 6:
                newPokemon = new Pokemon("Jynx", initialLife, new List<string> { "Rayo Hielo", "Ventisca", "Nieve Polvo" }, "Hielo");
                break;
            case 7:
                newPokemon = new Pokemon("Machop", initialLife, new List<string> { "Golpe Karate", "A Bocajarro", "Puño Dinámico" }, "Lucha");
                break;
            case 8:
                newPokemon = new Pokemon("Eevee", initialLife, new List<string> { "Tackle", "Puño Sombra", "Desenlace" }, "Normal");
                break;
            case 9:
                newPokemon = new Pokemon("Bulbasaur", initialLife, new List<string> { "Hoja Afilada", "Látigo Cepa", "Rayo Solar" }, "Planta");
                break;
            case 10:
                newPokemon = new Pokemon("Abra", initialLife, new List<string> { "Confusión", "Psíquico", "Premonición" }, "Psíquico");
                break;
            case 11:
                newPokemon = new Pokemon("Geodude", initialLife, new List<string> { "Avalancha", "Lanzarrocas", "Roca Afilada" }, "Roca");
                break;
            case 12:
                newPokemon = new Pokemon("Diglett", initialLife, new List<string> { "Terremoto", "Excavar", "Bofetón Lodo" }, "Tierra");
                break;
            case 13:
                newPokemon = new Pokemon("Ekans", initialLife, new List<string> { "Ácido", "Bomba Lodo", "Cola Veneno" }, "Veneno");
                break;
            case 14:
                newPokemon = new Pokemon("Pidgey", initialLife, new List<string> { "Tornado", "Ala de Acero", "Ataque Aéreo" }, "Volador");
                break;
            default:
                Console.WriteLine("Índice inválido.");
                return null;
        }

        // Si se crea un Pokémon, se agrega al equipo del entrenador
        if (newPokemon != null)
        {
            trainer.Team.Add(newPokemon);
            if (trainer.Active == null)
            {
                trainer.Active = newPokemon;
            }
        }

        return newPokemon;
    }

}