using System.Data.SqlTypes;
using System.Security.Cryptography;
using Library;

namespace Ucu.Poo.DiscordBot.Domain;

public class Restricion
{
    private List<string> Tipos = new List<string>
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
    
    private List<string> namesPokemon = new List<string>
    {
        "Squirtle", // Agua
        "Caterpie", // Bicho
        "Dratini", // Dragón
        "Pikachu", // Eléctrico
        "Gastly", // Fantasma
        "Charmander", // Fuego
        "Jynx", // Hielo
        "Machop", // Lucha
        "Eevee", // Normal
        "Bulbasaur", // Planta
        "Abra", // Psíquico
        "Geodude", // Roca
        "Diglett", // Tierra
        "Ekans", // Veneno
        "Pidgey" // Volador
    };

    public List<string> PokemonReestringido { get; }
    
    public List<string> TiposReestringido { get; }
    public List<string> ItemReestringido { get; }

    public string ReestriccionPokemon(string nombre)
    {
        if (this.namesPokemon.Contains(nombre))
        {
            if (!PokemonReestringido.Contains(nombre))
            {
                PokemonReestringido.Add(nombre);
                return $"Se agrego el pokemon {nombre} a la lista de reestricciones. ";
            }
            return "Pokemon ya agregado, elija otro";
        }
        return "Ingrese un pokemon valido";
    }

    public string ReestriccionItem(string Item)
    {
        if ((Item == "Superpocion") || Item == "Revivir" || Item == "CuraTotal")
        {
            if (!ItemReestringido.Contains(Item))
            {
                ItemReestringido.Add(Item);
                return $"El item {Item} se agrego a la lista de reestricciones. ";
            }

            return "Item ya agregado, elija otro";
        }
        return "Ingrese un item valido";
    }

    public string ReestriccionTipos(string tipo)
    {
        if (this.Tipos.Contains(tipo))
        {
            if (!TiposReestringido.Contains(tipo))
            {
                TiposReestringido.Add(tipo);
                return $"Se agrego el tipo {tipo} a la lista de reestricciones. ";
            }
            return "Tipo ya agregado, elija otro";
        }
        return "Ingrese un tipo valido";
    }

    public string Reestricciones()
    {
        string items = "";
        foreach (var VARIABLE in ItemReestringido)
        {
            items += VARIABLE + "-";
        }
        string tipos = "";
        foreach (var VARIABLE in TiposReestringido)
        {
            tipos += VARIABLE + "-";
        }
        string pokemones = "";
        foreach (var VARIABLE in PokemonReestringido)
        {
            pokemones += VARIABLE + "-";
        }
        if (pokemones == "") items = "Ninguno";
        if (items == "") items = "Ninguno";
        if (tipos == "") items = "Ninguno";
        string description = $"Caracteristicas reestringidas esta batalla: \n" +
                             $"Items: {items} \n" +
                             $"Tipos: {tipos} \n" +
                             $"Pokemones {pokemones} .";
        return description;
    }
    public bool EstePokemon(int pokemon)
    {
        
        if (PokemonReestringido.Contains(namesPokemon[pokemon]) || TiposReestringido.Contains(Tipos[pokemon])) return false;
        return true;
    }
    public bool EsteItem(string Item)
    {
        if (ItemReestringido.Contains(Item)) return false;
        return true;
    }

}