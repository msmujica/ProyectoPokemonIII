namespace Library
{
    public class SleepEffect : IEffect
    {
        public int turnosDormidos;
        public bool IcanAttack { get; set; }
        

        public string StartEffect(Pokemon pokemon)
        {
            this.turnosDormidos = new Random().Next(2, 6);
            return $"El pokemon {pokemon.Name} se le aplica el efecto dormir por {this.turnosDormidos - 1} turnos.";
        }

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

        public string Info(Pokemon pokemon)
        {
            return
                $"Al pokemon {pokemon.Name} le quedan {this.turnosDormidos - 1} turnos dormido, por lo cual no puede atacar.";
        }
    }
}