namespace Library
{
    /// <summary>
    /// Clase estática que maneja la lógica relacionada con los ataques de Pokémon, 
    /// incluyendo el almacenamiento de ataques predefinidos, su daño, tipo y la 
    /// lógica para calcular el daño de un ataque.
    /// La clase Ataque aplica varios principios de diseño:
    /// •	SRP: La clase se encarga exclusivamente de la lógica relacionada con los ataques de Pokémon, como calcular el daño, determinar la precisión y la probabilidad de efectos especiales. No tiene responsabilidades adicionales.
    /// •	OCP: Está diseñada para ser extendida sin necesidad de modificar el código existente. Por ejemplo, se pueden agregar nuevos ataques o efectos especiales sin alterar el código central.
    /// •	Principio de Expert: La clase es experta en la gestión de ataques y sus efectos. Conoce la lógica de cómo calcular el daño, aplicar efectos especiales, y gestionar la relación entre tipos de ataques y Pokémon.
    /// •	Acoplamiento Bajo: Aunque la clase interactúa con otras clases, como GestorEfectos y Pokemon, se encarga de delegar funcionalidades específicas, como la aplicación de efectos especiales, sin acoplarse demasiado a ellas, lo que permite cambios en otras clases sin afectar a Ataque.

    /// </summary>
    public static class Attack
    {
        /// <summary>
        /// Diccionario que almacena los ataques predefinidos con su respectivo daño y tipo.
        /// Cada clave es el nombre del ataque y el valor es una tupla que contiene el daño y el tipo del ataque.
        /// </summary>
        private static readonly Dictionary<string, (int Damage, string Tipo)> attacks = new Dictionary<string, (int Damage, string Type)>
        {
            // Agua
            { "Pistola Agua", (40, "Agua") },
            { "Hidrobomba", (110, "Agua") },
            { "Burbuja", (20, "Agua") },

            // Bicho
            { "Picadura", (30, "Bicho") },
            { "Pulso Bicho", (90, "Bicho") },
            { "Tijera X", (80, "Bicho") },

            // Dragón
            { "Garra Dragón", (80, "Dragón") },
            { "Cometa Draco", (130, "Dragón") },
            { "Aliento Dragón", (60, "Dragón") },

            // Eléctrico
            { "Impactrueno", (40, "Eléctrico") },
            { "Rayo", (90, "Eléctrico") },
            { "Trueno", (110, "Eléctrico") },

            // Fantasma
            { "Bola Sombra", (80, "Fantasma") },
            { "Puño Spectral", (90, "Fantasma") },
            { "Puño Sombrío", (70, "Fantasma") },

            // Fuego
            { "Llamarada", (110, "Fuego") },
            { "Lanzallamas", (90, "Fuego") },
            { "Ascuas", (40, "Fuego") },

            // Hielo
            { "Rayo Hielo", (90, "Hielo") },
            { "Ventisca", (110, "Hielo") },
            { "Nieve Polvo", (40, "Hielo") },

            // Lucha
            { "Golpe Karate", (50, "Lucha") },
            { "A Bocajarro", (120, "Lucha") },
            { "Puño Dinámico", (100, "Lucha") },

            // Normal
            { "Tackle", (40, "Normal") },
            { "Puño Sombra", (70, "Normal") },
            { "Desenlace", (50, "Normal") },

            // Planta
            { "Hoja Afilada", (55, "Planta") },
            { "Látigo Cepa", (45, "Planta") },
            { "Rayo Solar", (120, "Planta") },

            // Psíquico
            { "Confusión", (50, "Psíquico") },
            { "Psíquico", (90, "Psíquico") },
            { "Premonición", (120, "Psíquico") },

            // Roca
            { "Avalancha", (75, "Roca") },
            { "Lanzarrocas", (50, "Roca") },
            { "Roca Afilada", (100, "Roca") },

            // Tierra
            { "Terremoto", (100, "Tierra") },
            { "Excavar", (80, "Tierra") },
            { "Bofetón Lodo", (20, "Tierra") },

            // Veneno
            { "Ácido", (40, "Veneno") },
            { "Bomba Lodo", (90, "Veneno") },
            { "Cola Veneno", (50, "Veneno") },

            // Volador
            { "Tornado", (40, "Volador") },
            { "Ala de Acero", (70, "Volador") },
            { "Ataque Aéreo", (75, "Volador") },
        };

        /// <summary>
        /// Obtiene el daño y tipo de un ataque a partir de su nombre.
        /// </summary>
        /// <param name="nameAttack">Nombre del ataque que se quiere obtener.</param>
        /// <returns>Una tupla con el daño y el tipo del ataque.</returns>
        public static (int Damage, string Type) ObtainAttack(string nameAttacks)
        {
            if (attacks.ContainsKey(nameAttacks))
            {
                return attacks[nameAttacks];
            }

            Console.WriteLine("Ataque no encontrado.");
            return (0, string.Empty); // Retorna un valor predeterminado si el ataque no existe
        }

        /// <summary>
        /// Calcula el daño de un ataque, teniendo en cuenta la precisión, los críticos, 
        /// la efectividad del tipo y la posibilidad de aplicar efectos especiales.
        /// </summary>
        /// <param name="nombreAtaque">El nombre del ataque que se quiere calcular.</param>
        /// <param name="objetivo">El Pokémon objetivo del ataque.</param>
        /// <param name="gestorEfectos">El objeto que gestiona los efectos especiales que pueden ocurrir.</param>
        /// <returns>El daño calculado para el ataque.</returns>
        public static (int Daño, string Descripcion) CalculeDamage(string nameAttack, Pokemon objetive,
            GestorEfectos effectsManager)
        {
            string description = "";
            var attack = ObtainAttack(nameAttack);
            if (attack.Damage == 0)
                return (0, "Ataque no encontrado"); // Si el ataque no existe, retorna 0 y mensaje

            int totaldmg = attack.Damage;

            // Verifica si el ataque es preciso
            if (ItsPrecise())
            {
                description += "El ataque es preciso. ";
                if (Critical())
                {

                    totaldmg = (int)(totaldmg * 1.2); // Aumenta el daño en un 20% si es crítico
                    description += "¡Es un golpe crítico! ";
                }

                // Calcula el multiplicador de daño según los tipos
                double multiplier = LogicaTipos.CalcularMultiplicador(attack.Type, objetive.Tipos);
                totaldmg = (int)(totaldmg * multiplier);
                description += $"Como el ataque es tipo {attack.Type} el daño es {totaldmg}. ";

                if (effectsManager.PokemonConEfecto(objetive) && ApplySpecialEffect())
                {
                    // Aplica un efecto especial
                    IEfecto efectoEspecial = SelectSpecialEffect();
                    effectsManager.AplicarEfecto(efectoEspecial, objetive);
                    description += $"Se aplica el efecto especial: {efectoEspecial}. ";
                }
            }
            else
            {
                totaldmg = 0; // Si no es preciso, no causa daño
                description = "El ataque falló.";
            }

            return (totaldmg, description);
        }

        /// <summary>
            /// /// /// Determina si el ataque es preciso (con una probabilidad del 70%).
        /// </summary>
        /// <returns>Verdadero si el ataque es preciso, falso si no lo es.</returns>
        public static bool ItsPrecise()
        {
            return new Random().NextDouble() <= 0.7; // Probabilidad fija de 70% para precisión
        }

        /// <summary>
        /// Determina si el ataque es un golpe crítico (con una probabilidad del 20%).
        /// </summary>
        /// <returns>Verdadero si el ataque es crítico, falso si no lo es.</returns>
        public static bool Critical()
        {
            return new Random().NextDouble() <= 0.2; // 20% de probabilidad de crítico
        }

        /// <summary>
        /// Determina si se debe aplicar un efecto especial con una probabilidad del 10%.
        /// </summary>
        /// <returns>Verdadero si se aplica un efecto especial, falso si no se aplica.</returns>
        public static bool ApplySpecialEffect()
        {
            return new Random().NextDouble() <= 0.1; // Probabilidad fija del 10%
        }

        /// <summary>
        /// Selecciona un efecto especial aleatorio para aplicar (dormir, paralizar, envenenar, quemar).
        /// </summary>
        /// <returns>El efecto especial seleccionado.</returns>
        public static IEfecto SelectSpecialEffect()
        {
            int effect = new Random().Next(1, 5);

            switch (effect)
            {
                case 1:
                    return new EfectoDormir();
                case 2:
                    return new EfectoParalizar();
                case 3:
                    return new EfectoEnvenenar();
                case 4:
                    return new EfectoQuemar();
            }

            return null; // Si no se selecciona ningún efecto, retorna null
        }
    }
}