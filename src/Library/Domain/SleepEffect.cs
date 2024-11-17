namespace Library
{
    /// <summary>
    /// Clase que representa el efecto de "dormir" aplicado a un Pokémon.
    /// Cuando un Pokémon está dormido, pierde turnos y no puede actuar hasta que despierte.
    /// </summary>
    public class SleepEffect : IEffect
    {
        // Almacena el número de turnos que el Pokémon permanecerá dormido
        private int turnsSleeping;

        /// <summary>
        /// Inicia el efecto de "dormir" en un Pokémon.
        /// El Pokémon será dormido por un número aleatorio de turnos entre 1 y 4.
        /// </summary>
        /// <param name="pokemon">El Pokémon que será afectado por el sueño.</param>
        public void StartEffect(Pokemon pokemon)
        {
            // Determina cuántos turnos el Pokémon estará dormido, un valor aleatorio entre 1 y 4
            turnsSleeping = new Random().Next(1, 5);
            Console.WriteLine($"{pokemon.Name} ha sido dormido por {turnsSleeping} turnos.");
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
        public bool ProcessEffect(Pokemon pokemon)
        {
            if (turnsSleeping > 0)
            {
                // Reduce el número de turnos restantes del sueño
                turnsSleeping--;

                // Si ya no quedan turnos, el Pokémon se despierta
                if (turnsSleeping == 0)
                {
                    Console.WriteLine($"{pokemon.Name} ha despertado.");
                    return false; // El efecto ha terminado
                }

                return true; // El efecto sigue activo (el Pokémon sigue dormido)
            }

            return false; // El efecto ha terminado
        }
    }
}