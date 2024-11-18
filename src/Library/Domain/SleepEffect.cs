namespace Library
{
    /// <summary>
    /// Clase que representa el efecto de "dormir" aplicado a un Pokémon.
    /// Cuando un Pokémon está dormido, pierde turnos y no puede actuar hasta que despierte.
    /// </summary>
    public class SleepEffect : IEffect
    {
        // Almacena el número de turnos que el Pokémon permanecerá dormido
        public int turnosDormidos;
        public bool PuedoAtacar { get; set; }
        private EffectsManager effectsManager;

        /// <summary>
        /// Inicia el efecto de "dormir" en un Pokémon.
        /// El Pokémon será dormido por un número aleatorio de turnos entre 1 y 4.
        /// </summary>
        /// <param name="pokemon">El Pokémon que será afectado por el sueño.</param>
        public string IniciarEfecto(Pokemon pokemon)
        {
            // Determina cuántos turnos el Pokémon estará dormido, un valor aleatorio entre 1 y 4
            this.turnosDormidos = new Random().Next(1, 5);
            return $"El pokemon {pokemon} se le aplica el efecto dormir por {this.turnosDormidos} turnos.";
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
        public string ProcesarEfecto(Pokemon pokemon)
        {
            if (turnosDormidos > 0)
            {
                // Reduce el número de turnos restantes del sueño
                turnosDormidos--;
                this.PuedoAtacar = false;

                // Si ya no quedan turnos, el Pokémon se despierta
                if (turnosDormidos == 0)
                {
                    effectsManager.LimpiarEfectos(pokemon);
                    return $"El pokemon {pokemon} se despierta";
                }

                return $"Al pokemon {pokemon} le quedan {this.turnosDormidos} turnos dormido, por lo cual no puede atacar"; // El efecto sigue activo (el Pokémon sigue dormido)
            }
            this.PuedoAtacar = true;
            effectsManager.LimpiarEfectos(pokemon);
            return "";
            // El efecto ha terminado
        }
    }
}