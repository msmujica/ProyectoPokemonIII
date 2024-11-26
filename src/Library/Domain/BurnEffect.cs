namespace Library
{
    /// <summary>
    /// Clase que representa el efecto de "quemar" a un Pokémon.
    /// Un Pokémon quemado pierde un 10% de su vida máxima en cada turno.
    /// </summary>
    public class BurnEffect : IEffect
    {
        /// <summary>
        /// Obtiene si el Pokémon bajo el efecto de quemadura puede atacar.
        /// En este caso, el Pokémon aún puede atacar, por lo que siempre devuelve <c>true</c>.
        /// </summary>
        public bool IcanAttack
        {
            get { return true; }
        }
        // Porcentaje de la vida máxima que pierde el Pokémon debido a la quemadura (10%)
        private static double dmgPercentage = 0.10; 
        
        
        /// <summary>
        /// Inicia el efecto de "quemar" en el Pokémon.
        /// Este efecto causa daño continuo al Pokémon en cada turno.
        /// </summary>
        /// <param name="pokemon">El Pokémon que será quemado.</param>
        public string StartEffect(Pokemon pokemon)
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
        public string ProcessEffect(Pokemon pokemon)
        {
            // Calcula el daño causado por el veneno (5% de la vida actual del Pokémon)
            int daño = (int)(pokemon.Health * dmgPercentage);
            pokemon.Health -= daño;
            
            // Si la vida del Pokémon llega a cero o menos, el efecto ha terminado
            if (pokemon.Health <= 0)
            {
                return $"El pokemon {pokemon.Name} ha caído por quemadura. ";
            }
            
            return $"El pokemon {pokemon.Name} ha sufrido {daño} de daño por envenenamiento. ";
            // El efecto continúa (el Pokémon sigue vivo y envenenado)
        }
        
        /// <summary>
        /// Obtiene información sobre el efecto de quemadura aplicado al Pokémon.
        /// </summary>
        /// <param name="pokemon">El Pokémon que tiene el efecto de quemadura.</param>
        /// <returns>
        /// Un mensaje indicando que el Pokémon tiene el efecto de quemadura.
        /// </returns>
        public string Info(Pokemon pokemon)
        {
            return
                $"Al pokemon {pokemon.Name} tiene el efecto quemadura. ";
        }
    }
}