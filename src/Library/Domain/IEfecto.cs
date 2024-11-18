namespace Library
{
    /// <summary>
    /// Interfaz que define la estructura básica para los efectos especiales que pueden
    /// aplicarse a un Pokémon durante una batalla. Cada efecto debe ser capaz de iniciar
    /// su acción y procesar sus efectos de manera periódica.
    /// </summary>
    public interface IEfecto
    {
        /// <summary>
        /// Inicia el efecto sobre un Pokémon. Este método debe definir cómo el efecto
        /// afecta al Pokémon al momento de ser aplicado, como cambiar su estado o estadísticas.
        /// </summary>
        /// <param name="pokemon">El Pokémon al que se le va a aplicar el efecto.</param>
        public void IniciarEfecto(Pokemon pokemon);

        /// <summary>
        /// Procesa el efecto sobre un Pokémon. Este método es responsable de actualizar
        /// el estado del Pokémon en función de la duración o los efectos adicionales
        /// que el efecto pueda tener con el tiempo (por ejemplo, daño por veneno o parálisis).
        /// </summary>
        /// <param name="pokemon">El Pokémon sobre el cual se procesará el efecto.</param>
        /// <returns>Un valor booleano que indica si el efecto sigue activo o si ha terminado.</returns>
        public bool ProcesarEfecto(Pokemon pokemon);
    }
}