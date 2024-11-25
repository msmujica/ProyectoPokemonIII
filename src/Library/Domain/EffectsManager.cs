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
    public class EffectsManager
    {
        // Diccionario que almacena los efectos activos para cada Pokémon
        private Dictionary<Pokemon, List<IEffect>> activeEffects;

        /// <summary>
        /// Constructor que inicializa el diccionario de efectos activos.
        /// </summary>
        public EffectsManager()
        {
            activeEffects = new Dictionary<Pokemon, List<IEffect>>();
        }

        /// <summary>
        /// Aplica un efecto específico a un Pokémon.
        /// </summary>
        /// <param name="effect">El efecto a aplicar.</param>
        /// <param name="pokemon">El Pokémon que recibirá el efecto.</param>
        /// <returns>Un mensaje que indica si el efecto fue aplicado correctamente.</returns>

        public string ApplyEffect(IEffect effect, Pokemon pokemon)
        {
            if (effect == null || pokemon == null)
            {
                // El efecto o el Pokémon son nulos y no se puede aplicar el efecto.
                return "";
            }

            // Asegura que haya una lista de efectos para el Pokémon en el diccionario
            if (!activeEffects.ContainsKey(pokemon))
            {
                activeEffects[pokemon] = new List<IEffect>();
            }

            // Añade el efecto a la lista de efectos del Pokémon
            activeEffects[pokemon].Add(effect);

            // Inicia el efecto, lo que podría implicar acciones como mostrar un mensaje
            return effect.StartEffect(pokemon);
        }

        /// <summary>
        /// Procesa los efectos que alteran el comportamiento del Pokémon (como dormir o paralizar).
        /// </summary>
        /// <param name="pokem">El Pokémon cuyo estado de efectos se va a procesar.</param>
        /// <returns>
        /// <c>true</c> si el efecto sigue activo (por ejemplo, sigue dormido o paralizado).
        /// <c>false</c> si el efecto ha terminado o no aplica.
        /// </returns>
        public bool IcanAttack(Pokemon pokem)
        {
            foreach (var entry in activeEffects)
            {
                Pokemon pokemon = entry.Key;
                List<IEffect> efectos = entry.Value;
                for (int i = efectos.Count - 1; i >= 0; i--)
                {
                    IEffect effect = efectos[i];
                    if ((pokemon == pokem) && (effect is ParalyzeEffect) || (pokemon == pokem)&&(effect is SleepEffect))
                    {
                        return effect.IcanAttack;
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// Procesa los efectos de control, como dormir o paralizar, y devuelve un mensaje que describe el resultado.
        /// </summary>
        /// <param name="pokem">El Pokémon cuyo estado de efectos de control se va a procesar.</param>
        /// <returns>Un mensaje con el resultado del procesamiento del efecto.</returns>
    public string ProcesarControlMasa(Pokemon pokem)
        {
            // Inicializamos la descripción vacía

            // Verifica si el Pokémon tiene efectos activos
            if (!activeEffects.ContainsKey(pokem)) return $"El pokemon {pokem.Name} no tiene efectos activos.";

            List<IEffect> effects = activeEffects[pokem];
            foreach (var v in effects)
            {
                // Procesa efectos como dormir o paralizar
                if (v is SleepEffect)
                {
                    return v.ProcessEffect(pokem); // Devuelve si el efecto sigue activo
                }
                else if (v is ParalyzeEffect)
                {
                    return v.ProcessEffect(pokem); // Devuelve si el efecto sigue activo
                }
            }

            return ""; // Si no es un efecto de control como dormir o paralizar, retorna false
        }


        /// <summary>
        /// Procesa efectos de daño continuo (como veneno o quemadura) que afectan a la vida del Pokémon.
        /// </summary>
        /// <returns>Un mensaje con el resultado del procesamiento de los efectos de daño.</returns>
        public string ProcesarEfectosDaño(Pokemon pokem)
        {
            string description = "";
            // Recorre todos los efectos activos
            foreach (var entry in activeEffects)
            {
                Pokemon pokemon = entry.Key;
                List<IEffect> efectos = entry.Value;

                // Recorre cada efecto y aplica los que son de daño continuo
                for (int i = efectos.Count - 1; i >= 0; i--)
                {
                    IEffect effect = efectos[i];
                    if (pokemon != pokem)
                    {
                        if (!(effect is ParalyzeEffect))
                        {
                            // Procesa el daño del efecto
                            description += effect.ProcessEffect(pokemon) + "\n";
                        }
                    }
                }
            }
            return description;
        }

        /// <summary>
        /// Limpia todos los efectos activos de un Pokémon.
        /// </summary>
        /// <param name="pokemon">El Pokémon cuyo efecto se eliminará.</param>
        public string CleanEffects(Pokemon pokemon)
        {
            // Elimina los efectos activos del Pokémon si existen
            if (activeEffects.Remove(pokemon))
            {
                activeEffects.Remove(pokemon);
                return ($"Todos los efectos han sido eliminados de {pokemon.Name}.");
            }
            
            return "";
            
        }

        /// <summary>
        /// Verifica si un Pokémon tiene efectos activos.
        /// </summary>
        /// <param name="pokemon">El Pokémon a verificar.</param>
        /// <returns><c>true</c> si el Pokémon tiene efectos activos, <c>false</c> si no.</returns>
        public bool PokemonWithEffect(Pokemon pokemon)
        {
            return activeEffects.ContainsKey(pokemon);
        }

        /// <summary>
        /// Verifica si un Pokémon tiene el efecto de parálisis activo.
        /// </summary>
        /// <param name="pokem">El Pokémon a verificar.</param>
        /// <returns><c>true</c> si el Pokémon está paralizado, <c>false</c> si no.</returns>
        public bool IsParalyze(Pokemon pokem)
        {
            if (!activeEffects.ContainsKey(pokem)) return false;
            List<IEffect> effects = activeEffects[pokem];
            foreach (var v in effects)
            {
                // Procesa efectos como dormir o paralizar
                if (v is ParalyzeEffect)
                {
                    return true; // Devuelve si el pokemon tiene paralisis.
                }
            }
            return false;
        }
    }
}