using Library;
using Ucu.Poo.DiscordBot.Commands;

namespace Ucu.Poo.DiscordBot.Domain;

public class Restrictions
{
    private static List<string> Types = new List<string>
    {
        "Agua",
        "Bicho",
        "Dragón",
        "Eléctrico",
        "Fantasma",
        "Fuego",
        "Hielo",
        "Lucha",
        "Normal",
        "Planta",
        "Psíquico",
        "Roca",
        "Tierra",
        "Veneno",
        "Volador"
    };

    public string RemovePlayerNotAccepted { get; }
    
    //public string RestrictedPokemons(Pokemon pokemon)

    //public bool RestrictedItem(string item)
    
    

public string GetPokemonByIndex(int indice, Trainer entrenador)
    {
        int counter = 0;
        Pokemon pokem = Pokedex.CreatePokemonByIndex(indice, entrenador);
        if (pokem == null) return "Porfavor, Ingrese un indice valido de Pokemón";
        //foreach (var poke in Types())
        {
            //if (poke != pokem) counter -= 1;
        }

        return ".";
    }
}        
    
    
    public string RemovePlayerNotAccepted(string displayName)
    {
        //if (WaitingList.RemoveTrainer(displayName))
        {
            return $"{displayName} No aceptó las restricciones de la batalla";
        }

        return $"{displayName} no está en la lista de espera";
    }   


    
    