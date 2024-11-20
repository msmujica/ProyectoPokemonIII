namespace Library
{
    /// <summary>
    /// Clase que representa el efecto de "envenenar" a un Pokémon.
    /// Un Pokémon envenenado pierde una cierta cantidad de vida por cada turno que pasa.
    /// </summary>
    public class EfectoEnvenenar : IEfecto
    {
        public bool PuedoAtacar
        {
            get { return true; }
        }

        // Porcentaje de vida que el Pokémon pierde por turno debido al veneno (5%)
        private double porcentajeDaño = 0.05;
        private GestorEfectos gestorEfectos;

        /// <summary>
        /// Inicia el efecto de "envenenar" en el Pokémon.
        /// Este efecto causará daño periódico a la vida del Pokémon.
        /// </summary>
        /// <param name="pokemon">El Pokémon que será envenenado.</param>
        public string IniciarEfecto(Pokemon pokemon)
        {
            return $"El pokemon {pokemon.Name} ha sido envenenado, perdera vida cada turno.";
        }

        /// <summary>
        /// Procesa el efecto de "envenenar" durante cada turno del Pokémon afectado.
        /// Cada vez que se llama, el Pokémon pierde un porcentaje de su vida debido al veneno.
        /// </summary>
        /// <param name="pokemon">El Pokémon que está bajo el efecto de veneno.</param>
        /// <returns>
        /// <c>true</c> si el efecto sigue activo (es decir, el Pokémon no ha muerto debido al veneno).
        /// <c>false</c> si el efecto ha terminado (es decir, el Pokémon ha quedado fuera de combate por el veneno).
        /// </returns>
        public string ProcesarEfecto(Pokemon pokemon)
        {
            // Calcula el daño causado por el veneno (5% de la vida actual del Pokémon)
            int daño = (int)(pokemon.Vida * porcentajeDaño);
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
            return $"El {pokemon.Name} tiene el efecto envenenar. ";
        }
    }
}