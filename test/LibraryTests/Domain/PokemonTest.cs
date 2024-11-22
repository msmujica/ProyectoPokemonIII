using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Pokemon))]
public class PokemonTest
{
     // Se prueban debilidades, en este caso se prueba que el tipo Fuego es efectivo contra el tipo Planta
        [Test]
        public void Weaknesses()
        {      
            //Act: Se calcula el multiplicador para un ataque tipo fuego contra un Pokemón tipo planta
            double multiplier = TypeLogic.CalculeMultiplier("Fuego", "Planta");
            //Assert: El multilplicador debe ser 2, indicando que el ataque es superefectivo
            Assert.That(2, Is.EqualTo(multiplier), "El ataque de tipo Fuego debería ser super efectivo contra tipo Planta.");
        }
        // Prueba para verificar que el tipo "Fuego" es poco efectivo contra "Agua"
        [Test]
        public void Resistantces()
        {   // Act: Se calcula el multiplicador para un ataque de tipo Fuego contra un Pokémon de tipo Agua
            double multiplier = TypeLogic.CalculeMultiplier("Fuego", "Agua");
            // Assert: El multiplicador debería ser 0.5, indicando que el ataque es poco efectivo
            Assert.That(0.5, Is.EqualTo(multiplier), "El ataque de tipo Fuego debería ser poco efectivo contra tipo Agua.");
        }
        
        [Test]
        //En este caso se verifica que el tipo Eléctrico no tiene efecto sobre el tipo Tierra
        public void Inmunities()
        {   // Act: Se calcula el multiplicador para un ataque de tipo Eléctrico contra un Pokémon de tipo Tierra
            double multiplier = TypeLogic.CalculeMultiplier("Eléctrico", "Tierra");
            //Assert: El multiplicador debería ser 0, indicando que el ataque no tiene efecto
            Assert.That(0.5, Is.EqualTo(multiplier), "El ataque de tipo Eléctrico no tiene efecto sobre tipo Tierra.");
        }

        [Test]
        // Prueba para verificar que el tipo "Agua" no tiene ningun tipo de efecto contra "Eléctrico"
        public void NoEfects()
        { 
            //Act: Se calcula el multiplicador para un ataque de tipo Agua contra un Pokemón de tipo Eléctrico
            double multiplier = TypeLogic.CalculeMultiplier("Agua", "Eléctrico");
            //Asseert: El multiplicador debe ser 1 ya que sería un ataque normal, sin efecto alguno
            Assert.That(1, Is.EqualTo(multiplier), "El ataque de tipo Agua debería ser neutral contra tipo Eléctrico.");
        }
}