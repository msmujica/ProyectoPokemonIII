namespace Library
{
    /// <summary>
    /// Gestor que maneja los efectos activos de un Pokémon en combate.
    /// Puede aplicar efectos, procesarlos (como daño continuo o estados) y limpiar los efectos.
    /// La clase GestorEfectos sigue varios principios de diseño:
    /// •	SRP: La clase tiene una única responsabilidad, gestionar los efectos de los Pokémon durante las batallas, como la aplicación, el procesamiento y la limpieza de efectos. No asume otras responsabilidades, como el cálculo de daño o el control de la batalla.
    /// •	OCP: Es fácil extender la funcionalidad de esta clase sin modificarla. Por ejemplo, si deseas agregar nuevos efectos (como efectos de control o efectos de daño), puedes crear nuevas clases que implementen IEfecto, y la clase GestorEfectos seguirá funcionando sin necesidad de modificación.
    /// •	Principio de Expert: La clase es experta en la gestión de los efectos activos de los Pokémon. Sabe cómo almacenar, aplicar y procesar efectos, y cómo interactuar con otras clases como Pokemon y IEfecto para ejecutar la lógica asociada.
    /// •	Bajo Acoplamiento: La clase interactúa con los efectos a través de la interfaz IEfecto, lo que significa que no depende de implementaciones específicas de efectos. Esto permite agregar efectos nuevos sin afectar al resto del sistema, mejorando la modularidad.
    /// </summary>
    public class GestorEfectos
    {
        // Diccionario que almacena los efectos activos para cada Pokémon
        private Dictionary<Pokemon, List<IEfecto>> efectosActivos;

        // Constructor que inicializa el diccionario de efectos
        public GestorEfectos()
        {
            efectosActivos = new Dictionary<Pokemon, List<IEfecto>>();
        }

        /// <summary>
        /// Aplica un efecto específico a un Pokémon.
        /// </summary>
        /// <param name="efecto">El efecto a aplicar.</param>
        /// <param name="pokemon">El Pokémon que recibirá el efecto.</param>
        public void AplicarEfecto(IEfecto efecto, Pokemon pokemon)
        {
            if (efecto == null || pokemon == null)
            {
                Console.WriteLine("El efecto o el Pokémon son nulos y no se puede aplicar el efecto.");
                return;
            }

            // Asegura que haya una lista de efectos para el Pokémon en el diccionario
            if (!efectosActivos.ContainsKey(pokemon))
            {
                efectosActivos[pokemon] = new List<IEfecto>();
            }

            // Añade el efecto a la lista de efectos del Pokémon
            efectosActivos[pokemon].Add(efecto);

            // Inicia el efecto, lo que podría implicar acciones como mostrar un mensaje
            efecto.IniciarEfecto(pokemon);
        }

        /// <summary>
        /// Procesa los efectos que alteran el comportamiento del Pokémon (como dormir o paralizar).
        /// </summary>
        /// <param name="pokem">El Pokémon cuyo estado de efectos se va a procesar.</param>
        /// <returns>
        /// <c>true</c> si el efecto sigue activo (por ejemplo, sigue dormido o paralizado).
        /// <c>false</c> si el efecto ha terminado o no aplica.
        /// </returns>
        public bool ProcesarControlMasa(Pokemon pokem)
        {
            // Verifica si el Pokémon tiene efectos activos
            if (!efectosActivos.ContainsKey(pokem))
            {
                Console.WriteLine($"{pokem.Name} no tiene efectos activos.");
                return false;
            }

            List<IEfecto> efectos = efectosActivos[pokem];
            foreach (var v in efectos)
            {
                // Procesa efectos como dormir o paralizar
                if (v is EfectoDormir || v is EfectoParalizar)
                {
                    return v.ProcesarEfecto(pokem); // Devuelve si el efecto sigue activo
                }
            }

            return false; // Si no es un efecto de control como dormir o paralizar, retorna false
        }

        /// <summary>
        /// Procesa efectos de daño continuo (como veneno o quemadura) que afectan a la vida del Pokémon.
        /// </summary>
        public void ProcesarEfectosDaño()
        {
            // Recorre todos los efectos activos
            foreach (var entry in efectosActivos)
            {
                Pokemon pokemon = entry.Key;
                List<IEfecto> efectos = entry.Value;

                // Recorre cada efecto y aplica los que son de daño continuo
                for (int i = efectos.Count - 1; i >= 0; i--)
                {
                    IEfecto efecto = efectos[i];
                    if (efecto is EfectoEnvenenar || efecto is EfectoQuemar)
                    {
                        // Procesa el daño del efecto
                        efecto.ProcesarEfecto(pokemon);
                    }
                }
            }
        }

        /// <summary>
        /// Limpia todos los efectos activos de un Pokémon.
        /// </summary>
        /// <param name="pokemon">El Pokémon cuyo efecto se eliminará.</param>
        public void LimpiarEfectos(Pokemon pokemon)
        {
            // Elimina los efectos activos del Pokémon si existen
            if (efectosActivos.ContainsKey(pokemon))
            {
                efectosActivos.Remove(pokemon);
                Console.WriteLine($"Todos los efectos han sido eliminados de {pokemon.Name}.");
            }
            else
            {
                Console.WriteLine($"{pokemon.Name} no tiene efectos activos.");
            }
        }

        /// <summary>
        /// Verifica si un Pokémon tiene efectos activos.
        /// </summary>
        /// <param name="pokemon">El Pokémon a verificar.</param>
        /// <returns><c>true</c> si el Pokémon tiene efectos activos, <c>false</c> si no.</returns>
        public bool PokemonConEfecto(Pokemon pokemon)
        {
            return efectosActivos.ContainsKey(pokemon);
        }
    }
}