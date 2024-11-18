namespace Library
{
    /// <summary>
    /// Clase que representa el efecto de "quemar" a un Pokémon.
    /// Un Pokémon quemado pierde un 10% de su vida máxima en cada turno.
    /// </summary>
    public class EfectoQuemar : IEfecto
    {
        // Porcentaje de la vida máxima que pierde el Pokémon debido a la quemadura (10%)
        private static double dmgPercentage = 0.10; 

        /// <summary>
        /// Inicia el efecto de "quemar" en el Pokémon.
        /// Este efecto causa daño continuo al Pokémon en cada turno.
        /// </summary>
        /// <param name="pokemon">El Pokémon que será quemado.</param>
        public void IniciarEfecto(Pokemon pokemon)
        {
            Console.WriteLine($"{pokemon.Name} ha sido quemado.");
        }

        /// <summary>
        /// Procesa el efecto de la quemadura en el Pokémon en cada turno.
        /// Reduce la vida del Pokémon en función de su vida máxima.
        /// </summary>
        /// <param name="pokemon">El Pokémon que está bajo el efecto de la quemadura.</param>
        /// <returns>
        /// <c>true</c> si el efecto sigue activo (es decir, el Pokémon sigue quemado y pierde vida).
        /// <c>false</c> si el efecto ha terminado (es decir, el Pokémon ha quedado KO).
        /// </returns>
        public bool ProcesarEfecto(Pokemon pokemon)
        {
            // Calcula el daño de la quemadura (10% de la vida máxima)
            int daño = (int)(pokemon.Vida * dmgPercentage);
            pokemon.Vida -= daño;
            
            if (pokemon.Vida <= 0)
            { 
                // Si la vida del Pokémon llega a cero o menos, el efecto ha terminado (el Pokémon está KO)
                return false; 
            }

            // El efecto sigue activo, ya que el Pokémon sigue con vida
            return true;
        }
    }
}