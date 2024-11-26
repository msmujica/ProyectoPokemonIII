namespace Library;

/// <summary>
/// Clase que representa un Pokémon con nombre, puntos de vida, lista de ataques, tipo y estado de derrota.
/// Permite recibir daño, realizar ataques y gestionar su estado de derrota.
/// La clase Pokemon aplica los siguientes principios:
/// •	Expert: Pokemon gestiona su propia vida, ataques y estado, siguiendo el principio de asignar responsabilidades al experto en la información.
/// •	Acoplamiento bajo: Utiliza GestorEfectos para manejar efectos adicionales en los ataques, manteniendo bajo el acoplamiento con otras clases.
/// •	OCP (Open-Closed Principle): Permite la extensión con nuevas interfaces y funcionalidades, sin modificar el código existente.
/// </summary>

public class Pokemon
{
    private string name;
    private int health;
    private List<string> attack;
    private string type;
    private bool isDefeated;

    /// <summary>
    /// Nombre del Pokémon.
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    /// Puntos de vida del Pokémon. Este valor disminuye cuando el Pokémon recibe daño.
    /// </summary>
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    /// <summary>
    /// Lista de ataques disponibles para el Pokémon. Cada ataque es una cadena con el nombre del ataque.
    /// </summary>
    public List<string> Attacks
    {
        get { return attack; }
        set { attack = value; }
    }
    
    /// <summary>
    /// Tipo o tipos del Pokémon, como "Fuego", "Agua", "Eléctrico", etc.
    /// </summary>
    public string Types
    {
        get { return type; }
        set { type = value; }
    }

    /// <summary>
    /// Estado del Pokémon, indica si el Pokémon ha sido derrotado.
    /// </summary>
    public bool IsDefeated
    {
        get { return isDefeated; }
        set { isDefeated = value; }
    }
    
    /// <summary>
    /// Constructor de la clase Pokémon.
    /// </summary>
    /// <param name="nombre">Nombre del Pokémon.</param>
    /// <param name="vida">Puntos de vida iniciales del Pokémon.</param>
    /// <param name="ataques">Lista de ataques que puede realizar el Pokémon.</param>
    /// <param name="tipo">Tipo o tipos del Pokémon.</param>
    public Pokemon(string name, int health, List<string> attack, string type)
    {
        Name = name;
        Health = health;
        Attacks = attack;
        Types = type;
        IsDefeated = false;
    }

    /// <summary>
    /// Método que permite al Pokémon recibir un cierto daño.
    /// </summary>
    /// <param name="daño">Cantidad de daño recibido.</param>
    public void recibirDaño(int daño)
    {
        if (!IsDefeated)
        {
            Health -= daño;
            if (Health <= 0)
            {
                IsDefeated = true;
                Health = 0;
                Console.WriteLine($"{Name} a sido derrotado");
            }
        }
        else
        {
            Console.WriteLine($"{Name} no puede recibir daño por que ya a sido derrotado");
        }
    }

    /// <summary>
    /// Método que permite al Pokémon realizar un ataque sobre otro Pokémon.
    /// </summary>
    /// <param name="oponente">Pokémon sobre el cual se realizará el ataque.</param>
    /// <param name="ataque">Nombre del ataque que se realizará.</param>
    /// <param name="effectsManager">Gestor de efectos para calcular el daño del ataque.</param>
    /// <returns>El valor del daño causado al oponente como una cadena.</returns>
    public string attacks(Pokemon oponente, string ataque, EffectsManager effectsManager)
    {
        foreach (var VARIABLE in Attacks)
        {
            if (VARIABLE == ataque)
            {
                var (valor, mensaje) = Attack.CalculeDamage(ataque, oponente, effectsManager);
                oponente.recibirDaño(valor);
                return $"El pokemon {oponente.name} recibió {valor} de daño con el ataque {ataque}. {mensaje}"; // Devolvemos el mensaje
            }
        }

        return "Este no es tu ataque";
    }
}