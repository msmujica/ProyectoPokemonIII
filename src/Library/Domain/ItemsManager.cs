namespace Library.Items
{
    /// <summary>
    /// Esta clase gestiona el uso de objetos de curación y revivir en los Pokémon durante la batalla.
    /// La clase GestorDeItems aplica los siguientes principios:
    /// •	SRP: GestorDeItems tiene una única responsabilidad, que es gestionar el uso de ítems en la batalla.
    /// •	OCP: Permite agregar nuevos tipos de ítems sin modificar el código existente, facilitando la extensión.
    /// •	Principio de Expert: GestorDeItems conoce y gestiona la lógica de los ítems, por lo que es el experto en esta funcionalidad.
    /// •	Acoplamiento bajo: Usa GestorEfectos para manejar los efectos negativos, reduciendo la dependencia entre clases.
    /// </summary>
    public class ItemsManager
    {
        /// <summary>
        /// Usa una SuperPoción en un Pokémon, restaurando su vida hasta un máximo de 100.
        /// </summary>
        /// <param name="pokemon">El Pokémon al que se le aplicará la SuperPoción.</param>
        /// <param name="contadorSuperPocion">El número de SuperPociones disponibles.</param>
        /// <returns>Un mensaje indicando el resultado de usar la SuperPoción.</returns>
        /// 
        public string UseSuperPotion(Pokemon pokemon, int superpotionCounter)
        {
            if (superpotionCounter > 0)
            {
                if (pokemon.Health < 100)
                {
                    pokemon.Health += 70;
                    if (pokemon.Health > 100)
                        pokemon.Health = 100; // No superar el 100%
                    superpotionCounter--;
                    return ("Usaste una Super Pocion. Usos restantes: " + superpotionCounter);
                }

                return ("El Pokémon ya está a máxima vida.");
            }

            return ("No tienes Super Pociones disponibles.");
        }

        /// <summary>
        /// Usa un Revivir para resucitar a un Pokémon derrotado, restaurándole 50% de vida.
        /// </summary>
        /// <param name="pokemon">El Pokémon que se revivirá.</param>
        /// <param name="contadorRevivir">El número de Revivir disponibles.</param>
        /// <returns>Un mensaje indicando el resultado de usar el Revivir.</returns>
        public string UseRevive(Pokemon pokemon, int reviveCounter)
        {
            if (reviveCounter > 0)
            {
                if (pokemon.IsDefeated)
                {
                    pokemon.IsDefeated = false;
                    pokemon.Health = 50; // Revive con 50% de vida
                    reviveCounter--;
                    return ("Usaste un Revivir. Usos restantes: " + reviveCounter);
                }

                return ("El Pokémon no está derrotado.");
            }

            return ("No tienes Revivir disponible.");
        }

        /// <summary>
        /// Usa una Cura Total para restaurar la vida de un Pokémon al 100% y eliminar cualquier efecto negativo.
        /// </summary>
        /// <param name="pokemon">El Pokémon al que se le aplicará la Cura Total.</param>
        /// <param name="contadorCuraTotal">El número de Curaciones Totales disponibles.</param>
        /// <param name="gestorEfectos">El gestor de efectos que se usará para limpiar efectos negativos.</param>
        /// <returns>Un mensaje indicando el resultado de usar la Cura Total.</returns>
        public string UseTotalCure(Pokemon pokemon, int totalcureCounter, EffectsManager effectsManager)
        {
            if (totalcureCounter > 0)
            {
                pokemon.Health = 100; // Cura completamente al Pokémon
                effectsManager.CleanEffects(pokemon);
                totalcureCounter--;
                return ("Usaste una Cura Total. Usos restantes: " + totalcureCounter);
            }

            return ("No tienes Curaciones Totales disponibles.");
        }
    }
}