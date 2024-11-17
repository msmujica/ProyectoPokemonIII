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
    private List<string> attacks;
    private string type;
    private bool isDefeated;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public List<string> Attacks
    {
        get { return attacks; }
        set { attacks = value; }
    }

    public string Types
    {
        get { return type; }
        set { type = value; }
    }

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
    public Pokemon(string name, int health, List<string> attacks, string type)
    {
        Name = name;
        Health = health;
        Attacks = attacks;
        Types = type;
        IsDefeated = false;
    }

    /// <summary>
    /// Método que permite al Pokémon recibir un cierto daño.
    /// </summary>
    /// <param name="daño">Cantidad de daño recibido.</param>
    public void RecibeDamage(int dmg)
    {
        if (!IsDefeated)
        {
            Health -= dmg;
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
    /// <param name="gestorEfectos">Gestor de efectos para calcular el daño del ataque.</param>
    /// <returns>El valor del daño causado al oponente como una cadena.</returns>
    public string atacar(Pokemon opponent, string attack, EffectsManager effectsManager)
    {
        foreach (var VARIABLE in Attacks)
        {
            if (VARIABLE == attack)
            {
                var (value, message) = Attack.CalculeDamage(attack, opponent, effectsManager);
                opponent.RecibeDamage(value);
                return $"El oponente recibió {value} de daño con el ataque {attack}. {message}"; // Devolvemos el mensaje
            }
        }

        return "Este no es tu ataque";
    }
}