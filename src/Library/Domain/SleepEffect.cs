namespace Library
{
    /// <summary>
    /// Representa un efecto de estado de sueño que puede ser aplicado a un Pokémon.
    /// Este efecto impide que el Pokémon ataque durante un número limitado de turnos.
    /// </summary>
    public class SleepEffect : IEffect
    {
        /// <summary>
        /// Número de turnos que el Pokémon permanecerá dormido.
        /// </summary>
        public int turnosDormidos;

        /// <summary>
        /// Indica si el Pokémon puede atacar. 
        /// Es falso mientras el Pokémon esté dormido.
        /// </summary>
        public bool IcanAttack { get; set; }
        
        /// <summary>
        /// Aplica el efecto de sueño a un Pokémon y define la cantidad de turnos que estará dormido.
        /// </summary>
        /// <param name="pokemon">El Pokémon al que se le aplica el efecto de sueño.</param>
        /// <returns>Un mensaje indicando el inicio del efecto de sueño y la cantidad de turnos.</returns>
        public string StartEffect(Pokemon pokemon)
        {
            this.turnosDormidos = new Random().Next(2, 6);
            return $"El pokemon {pokemon.Name} se le aplica el efecto dormir por {this.turnosDormidos - 1} turnos.";
        }

        /// <summary>
        /// Procesa el efecto de sueño en cada turno, disminuyendo la cantidad de turnos restantes.
        /// </summary>
        /// <param name="pokemon">El Pokémon que tiene el efecto de sueño.</param>
        /// <returns>Un mensaje indicando el estado del Pokémon (si sigue dormido o si despertó).</returns>
        public string ProcessEffect(Pokemon pokemon)
        {
            if (turnosDormidos >= 1)
            {
                turnosDormidos--;
                this.IcanAttack = false;
                if (turnosDormidos == 0)
                {
                    this.IcanAttack = true;
                    return $"El pokemon {pokemon.Name} ha despertado.";
                }
                return $"Al pokemon {pokemon.Name} le quedan {this.turnosDormidos - 1} turnos dormido, por lo cual no puede atacar.";
            }

            this.IcanAttack = true;
            return $"El pokemon {pokemon.Name} ha despertado.";
        }

        /// <summary>
        /// Proporciona información sobre el estado actual del efecto de sueño en un Pokémon.
        /// </summary>
        /// <param name="pokemon">El Pokémon afectado por el sueño.</param>
        /// <returns>Un mensaje indicando cuántos turnos quedan para que el Pokémon despierte.</returns>
        public string Info(Pokemon pokemon)
        {
            return
                $"Al pokemon {pokemon.Name} le quedan {this.turnosDormidos - 1} turnos dormido, por lo cual no puede atacar.";
        }
    }
}
