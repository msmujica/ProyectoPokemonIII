namespace Library
{
    /// <summary>
    /// Clase que representa el efecto de "quemar" a un Pokémon.
    /// Un Pokémon quemado pierde un 10% de su vida máxima en cada turno.
    /// </summary>
    public class EfectoQuemar : IEfecto
    {
        public bool PuedoAtacar
        {
            get { return true; }
        }
        // Porcentaje de la vida máxima que pierde el Pokémon debido a la quemadura (10%)
        private static double dmgPercentage = 0.10; 
        private GestorEfectos gestorEfectos;

        /// <summary>
        /// Inicia el efecto de "quemar" en el Pokémon.
        /// Este efecto causa daño continuo al Pokémon en cada turno.
        /// </summary>
        /// <param name="pokemon">El Pokémon que será quemado.</param>
        public string IniciarEfecto(Pokemon pokemon)
        {
            return $"El pokemon {pokemon.Name} ha sido quemado.";
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
        public string ProcesarEfecto(Pokemon pokemon)
        {
            // Calcula el daño causado por el veneno (5% de la vida actual del Pokémon)
            int daño = (int)(pokemon.Vida * dmgPercentage);
            pokemon.Vida -= daño;
            
            // Si la vida del Pokémon llega a cero o menos, el efecto ha terminado
            if (pokemon.Vida <= 0)
            {
                gestorEfectos.LimpiarEfectos(pokemon);
                return $"El pokemon {pokemon.Name} ha caído por envenenamiento. ";
            }
            
            return $"El pokemon {pokemon.Name} ha sufrido {daño} de daño por envenenamiento. ";
            // El efecto continúa (el Pokémon sigue vivo y envenenado)
        }
        public string Info(Pokemon pokemon)
        {
            return $"El {pokemon.Name} tiene el efecto quemar. ";
        }
    }
}