namespace Library
{
    /// <summary>
    /// Clase que representa el efecto de "dormir" aplicado a un Pokémon.
    /// Cuando un Pokémon está dormido, pierde turnos y no puede actuar hasta que despierte.
    /// </summary>
    public class SleepEffect : IEffect
    {
        // Almacena el número de turnos que el Pokémon permanecerá dormido
        public int turnsSleep;
        public bool IcanAttack { get; set; }
        private EffectsManager effectsManager;

        /// <summary>
        /// Inicia el efecto de "dormir" en un Pokémon.
        /// El Pokémon será dormido por un número aleatorio de turnos entre 1 y 4.
        /// </summary>
        /// <param name="pokemon">El Pokémon que será afectado por el sueño.</param>
        public string StartEffect(Pokemon pokemon)
        {
            // Determina cuántos turnos el Pokémon estará dormido, un valor aleatorio entre 1 y 4
            this.turnsSleep = new Random().Next(1, 5);
            return $"El pokemon {pokemon.Name} se le aplica el efecto dormir por {this.turnsSleep} turnos. ";
        }

        /// <summary>
        /// Procesa el efecto de "dormir" durante cada turno del Pokémon afectado.
        /// Cada vez que se llama, reduce los turnos restantes de sueño.
        /// </summary>
        /// <param name="pokemon">El Pokémon que está bajo el efecto del sueño.</param>
        /// <returns>
        /// <c>true</c> si el efecto sigue activo (es decir, el Pokémon sigue dormido).
        /// <c>false</c> si el efecto ha terminado (es decir, el Pokémon ha despertado).
        /// </returns>
        public string ProcessEffect(Pokemon pokemon)
        {
            if (turnsSleep > 0)
            {
                // Reduce el número de turnos restantes del sueño
                turnsSleep--;
                this.IcanAttack = false;

                // Si ya no quedan turnos, el Pokémon se despierta
                if (turnsSleep == 0)
                {
                    effectsManager.CleanEffects(pokemon);
                    return $"El pokemon {pokemon.Name} se despierta. ";
                }

                return $"Al pokemon {pokemon.Name} le quedan {this.turnsSleep} turnos dormido, por lo cual no puede atacar. "; // El efecto sigue activo (el Pokémon sigue dormido)
            }
            this.IcanAttack = true;
            effectsManager.CleanEffects(pokemon);
            return "";
            // El efecto ha terminado
        }
    }
}