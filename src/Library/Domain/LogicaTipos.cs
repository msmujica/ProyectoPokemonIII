namespace Library;

/// <summary>
/// Esta clase maneja la lógica de tipos en el sistema de batalla, determinando las debilidades, resistencias e inmunidades 
/// de los tipos de Pokémon frente a otros tipos de ataques.
/// La clase Pokedex sigue estos principios:
/// •	Expert: Centraliza la creación de Pokémon y la gestión de sus nombres y tipos, lo que la convierte en la experta en esta información.
/// •	Low Coupling: La clase tiene bajo acoplamiento, ya que solo gestiona datos básicos de los Pokémon sin depender de la lógica de batalla.
/// •	High Cohesion: La cohesión es alta, ya que todos sus métodos están relacionados exclusivamente con el manejo de la información de la Pokedex.
/// •	Creator: Como creadora de objetos Pokemon, facilita la extensibilidad al incorporar nuevos Pokémon fácilmente.

/// </summary>
public static class LogicaTipos
{
    // Diccionario de cada tipo de debilidades (tipos que son fuertes contra ese tipo).
    private static readonly Dictionary<string, List<string>> Debilidades = new Dictionary<string, List<string>>
    {
        { "Agua", new List<string> { "Eléctrico", "Planta" } },
        { "Bicho", new List<string> { "Fuego", "Roca", "Volador", "Veneno" } },
        { "Dragón", new List<string> { "Dragón", "Hielo" } },
        { "Eléctrico", new List<string> { "Tierra" } },
        { "Fantasma", new List<string> { "Fantasma" } },
        { "Fuego", new List<string> { "Agua", "Roca", "Tierra" } },
        { "Hielo", new List<string> { "Fuego", "Lucha", "Roca" } },
        { "Lucha", new List<string> { "Psíquico", "Volador" } },
        { "Normal", new List<string> { "Lucha" } },
        { "Planta", new List<string> { "Bicho", "Fuego", "Hielo", "Veneno", "Volador" } },
        { "Psíquico", new List<string> { "Bicho", "Fantasma" } },
        { "Roca", new List<string> { "Agua", "Lucha", "Planta", "Tierra" } },
        { "Tierra", new List<string> { "Agua", "Hielo", "Planta" } },
        { "Veneno", new List<string> { "Psíquico", "Tierra" } },
        { "Volador", new List<string> { "Eléctrico", "Hielo", "Roca" } },
    };

    // Diccionario de cada tipo de sus resistencias (tipos que son débiles contra ese tipo).
    private static readonly Dictionary<string, List<string>> Resistencias = new Dictionary<string, List<string>>
    {
        { "Agua", new List<string> { "Agua", "Fuego", "Hielo" } },
        { "Bicho", new List<string> { "Lucha", "Planta", "Tierra" } },
        { "Dragón", new List<string> { "Agua", "Eléctrico", "Fuego", "Planta" } },
        { "Eléctrico", new List<string> { "Volador" } },
        { "Fantasma", new List<string> { "Veneno", "Lucha" } },
        { "Fuego", new List<string> { "Bicho", "Fuego", "Planta" } },
        { "Hielo", new List<string> { "Hielo" } },
        { "Roca", new List<string> { "Fuego", "Normal", "Volador" } },
        { "Tierra", new List<string> { "Eléctrico" } },
        { "Veneno", new List<string> { "Planta", "Veneno" } },
        { "Volador", new List<string> { "Bicho", "Lucha", "Planta" } },
    };

    // Diccionario de cada tipo de sus inmunidades (tipos contra los que no tienen ningún efecto).
    private static readonly Dictionary<string, List<string>> Inmunidades = new Dictionary<string, List<string>>
    {
        { "Eléctrico", new List<string> { "Eléctrico" } },
        { "Fantasma", new List<string> { "Normal" } },
        { "Normal", new List<string> { "Fantasma" } }
    };

    /// <summary>
    /// Calcula el multiplicador de daño que se aplica a un ataque según el tipo del atacante y el tipo del defensor.
    /// </summary>
    /// <param name="tipoAtaque">El tipo del Pokémon atacante.</param>
    /// <param name="tipoDefensor">El tipo del Pokémon defensor.</param>
    /// <returns>El multiplicador de daño (1: neutral, 2: super efectivo, 0.5: poco efectivo, 0: sin efecto).</returns>
    public static double CalcularMultiplicador(string tipoAtaque, string tipoDefensor)
    {
        if (Inmunidades.ContainsKey(tipoDefensor) && Inmunidades[tipoDefensor].Contains(tipoAtaque)){
            return 0;
        }

        if (Debilidades.ContainsKey(tipoDefensor) && Debilidades[tipoDefensor].Contains(tipoAtaque))
        {
            return 2;
        }

        if (Resistencias.ContainsKey(tipoDefensor) && Resistencias[tipoDefensor].Contains(tipoAtaque))
        {
            return 0.5;
        }

        return 1;
    }
}