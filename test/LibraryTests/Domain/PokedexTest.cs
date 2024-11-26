using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Pokedex))]
public class PokedexTest
{
    /// <summary>
    /// Prueba que verifica que se puede obtener un Pokemon por suu indice valido.
    /// </summary>
    [Test]
        public void TestShowPokemonByIndexValid()
        {
            
            var pokemon = Pokedex.ShowPokemonByIndex(0);
            
            Assert.That("Squirtle", Is.EqualTo(pokemon));
        }
    
        /// <summary>
        /// Prueba para que Mostrar Pokédex devuelve la lista completa.
        /// </summary>
        [Test]
        public void TestShowPokedex()
        {
            var pokedex = Pokedex.ShowPokedex(); 
            
            Assert.That(15, Is.EqualTo(pokedex.Count)); 
            Assert.That(pokedex[0].Contains("Squirtle"), Is.True);
            Assert.That(pokedex[0].Contains("Agua"), Is.True);
        }
        
        
        /// <summary>
        /// Verifica la creacion de Pokemon a partir de su indice en la Pokedex.
        /// </summary>
        [Test]
        public void TestCreatePokemonByIndex()
        {
            var trainer = new Trainer("Ash");
            var trainer2 = new Trainer("Misty");
            var trainer3 = new Trainer("Pikachu");
            
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

            
            Assert.That(pokemon, Is.Not.Null);  
            Assert.That("Squirtle", Is.EqualTo(pokemon.Name));   
            Assert.That(100, Is.EqualTo(pokemon.Health));    
        }
        
        /// <summary>
        ///Se prueba el comportamiento cuando se intenta crear un Pokemón con un índice inválido
        /// </summary>
        [Test]
        public void TestCreatePokemonByIndexInvalid()
        {
            var entrenador = new Trainer("Ash");
            
            var pokemon = Pokedex.CreatePokemonByIndex(100, entrenador);
            
            Assert.That(pokemon, Is.Null); 
        }
}
