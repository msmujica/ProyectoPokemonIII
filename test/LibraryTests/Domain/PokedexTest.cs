using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Pokedex))]
public class PokedexTest
{
    [Test]
        public void TestShowPokemonByIndex_Valid()
        {
            // Act: se obtiene pokemon con indice 0
            var pokemon = Pokedex.ShowPokemonByIndex(0);

            // Assert: Se verifica que el pokemón devuelto sea Squirtle
            Assert.That("Squirtle", Is.EqualTo(pokemon));
        }

        [Test]
        //Prueba para que Mostrar Pokédex devuelve la lista completa
        public void TestShowPokedex()
        {
            // Act: Se obtiene la lista de Pokemón de la Pokédex
            var pokedex = Pokedex.ShowPokedex();

            // Assert: Se verifica que la liosta contenga exactamente 15 Pokemons y que el primero sea Squirtle
            Assert.That(15, Is.EqualTo(pokedex.Count)); // Hay 15 Pokémon
            Assert.That(pokedex[0].Contains("Squirtle"), Is.True);
            Assert.That(pokedex[0].Contains("Agua"), Is.True);
        }
        
        [Test]
        public void TestCreatePokemonByIndex()
        {
            // Arrange: Se crea un entrenador 
            var trainer = new Trainer("Ash");
            var trainer2 = new Trainer("Misty");
            var trainer3 = new Trainer("Pikachu");

            // Act: Se crea un Pokemón de inidice 0 para el entrenador 
            var pokemon = Pokedex.CreatePokemonByIndex(0, trainer);
            var pokemon1 = Pokedex.CreatePokemonByIndex(1, trainer);
            var pokemon2 = Pokedex.CreatePokemonByIndex(2, trainer);
            var pokemon3 = Pokedex.CreatePokemonByIndex(3, trainer);
            var pokemon4 = Pokedex.CreatePokemonByIndex(4, trainer);
            var pokemon5 = Pokedex.CreatePokemonByIndex(5, trainer);
            var pokemon6 = Pokedex.CreatePokemonByIndex(6, trainer2);
            var pokemon7 = Pokedex.CreatePokemonByIndex(7, trainer2);
            var pokemon8 = Pokedex.CreatePokemonByIndex(8, trainer2);
            var pokemon9 = Pokedex.CreatePokemonByIndex(9, trainer2);
            var pokemon10 = Pokedex.CreatePokemonByIndex(10, trainer2);
            var pokemon11 = Pokedex.CreatePokemonByIndex(11, trainer2);
            var pokemon12 = Pokedex.CreatePokemonByIndex(12, trainer3);
            var pokemon13 = Pokedex.CreatePokemonByIndex(13, trainer3);
            var pokemon14 = Pokedex.CreatePokemonByIndex(14, trainer3);

            
            //Assert
            Assert.That(pokemon, Is.Not.Null);  //El pokemon existe
            Assert.That("Squirtle", Is.EqualTo(pokemon.Name));    //El nombre del Pokemón debe ser Squirtle
            Assert.That(100, Is.EqualTo(pokemon.Health));     //La vida del Pokemón debe ser 100 
        }

        [Test]
        // Se prueba el comportamiento cuando se intenta crear un Pokemón con un índice inválido
        public void TestCreatePokemonByIndex_Invalid()
        {
            // Arrange: Se crea un entrenador
            var entrenador = new Trainer("Ash");

            // Act: Se crea un Pokemón con índice inválido (100) para el entrenador
            var pokemon = Pokedex.CreatePokemonByIndex(100, entrenador);

            // Assert: 
            Assert.That(pokemon, Is.Null); // No debe existir un Pokemón con ese índice
        }
}
