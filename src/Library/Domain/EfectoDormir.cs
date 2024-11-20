namespace Library
{
    public class EfectoDormir : IEfecto
    {
        public int turnosDormidos;
        public bool PuedoAtacar { get; set; }
        

        public string IniciarEfecto(Pokemon pokemon)
        {
            this.turnosDormidos = new Random().Next(1, 5) + 1;
            return $"El pokemon {pokemon.Name} se le aplica el efecto dormir por {this.turnosDormidos - 1} turnos.";
        }

        public string ProcesarEfecto(Pokemon pokemon)
        {
            if (turnosDormidos >= 1)
            {
                turnosDormidos--;
                this.PuedoAtacar = false;
                if (turnosDormidos == 0)
                {
                    this.PuedoAtacar = true;
                    return $"El pokemon {pokemon.Name} ha despertado.";
                }
                return $"Al pokemon {pokemon.Name} le quedan {this.turnosDormidos} turnos dormido, por lo cual no puede atacar.";
            }

            this.PuedoAtacar = true;
            return $"El pokemon {pokemon.Name} ha despertado.";
        }

        public string Info(Pokemon pokemon)
        {
            return
                $"Al pokemon {pokemon.Name} le quedan {this.turnosDormidos} turnos dormido, por lo cual no puede atacar.";
        }
    }
}