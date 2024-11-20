using Library;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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
            ClassicAssert.AreEqual("Squirtle", pokemon);
        }

        [Test]
        //Prueba para que Mostrar Pokédex devuelve la lista completa
        public void TestShowPokedex()
        {
            // Act: Se obtiene la lista de Pokemón de la Pokédex
            var pokedex = Pokedex.ShowPokedex();

            // Assert: Se verifica que la liosta contenga exactamente 15 Pokemons y que el primero sea Squirtle
            ClassicAssert.AreEqual(15, pokedex.Count); // Hay 15 Pokémon
            ClassicAssert.IsTrue(pokedex[0].Contains("Squirtle"));
            ClassicAssert.IsTrue(pokedex[0].Contains("Agua"));
        }
        
        [Test]
        public void TestCreatePokemonByIndex()
        {
            // Arrange: Se crea un entrenador 
            var trainer = new Trainer("Ash");

            // Act: Se crea un Pokemón de inidice 0 para el entrenador 
            var pokemon = Pokedex.CreatePokemonByIndex(0, trainer);

            //Assert
            ClassicAssert.IsNotNull(pokemon);  //El pokemon existe
            ClassicAssert.AreEqual("Squirtle", pokemon.Name);    //El nombre del Pokemón debe ser Squirtle
            ClassicAssert.AreEqual(100, pokemon.Health);     //La vida del Pokemón debe ser 100 
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
            ClassicAssert.IsNull(pokemon); // No debe existir un Pokemón con ese índice
        }
}