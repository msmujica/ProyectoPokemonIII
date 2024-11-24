using Library;
using NUnit.Framework;


namespace LibraryTests.Domain
{
    [TestFixture]
    [TestOf(typeof(TypeLogic))]
    public class TypeLogicTest
    {
        // Prueba para verificar que el tipo "Fuego" es super efectivo contra "Planta"
        [Test]
        public void Weaknesses()
        {
            // Act: Se calcula el multiplicador para un ataque tipo fuego contra un Pokémon tipo planta
            double multiplier = TypeLogic.CalculeMultiplier("Fuego", "Planta");
            // Assert: El multiplicador debe ser 2
            Assert.That(multiplier, Is.EqualTo(2), "El ataque de tipo Fuego debería ser super efectivo contra tipo Planta.");
        }

        // Prueba para verificar que el tipo "Fuego" es poco efectivo contra "Agua"
        [Test]
        public void Resistances()
        {
            // Act: Se calcula el multiplicador para un ataque de tipo Fuego contra un Pokémon de tipo Agua
            double multiplier = TypeLogic.CalculeMultiplier("Fuego", "Agua");
            // Assert: El multiplicador debería ser 0.5
            Assert.That(multiplier, Is.EqualTo(0.5), "El ataque de tipo Fuego debería ser poco efectivo contra tipo Agua.");
        }

        // Prueba para verificar que el tipo "Eléctrico" no tiene efecto sobre "Tierra"
        [Test]
        public void Immunities()
        {
            // Act: Se calcula el multiplicador para un ataque de tipo Eléctrico contra un Pokémon de tipo Tierra
            double multiplier = TypeLogic.CalculeMultiplier("Eléctrico", "Eléctrico");
            // Assert: El multiplicador debería ser 0
            Assert.That(multiplier, Is.EqualTo(0), "El ataque de tipo Eléctrico no tiene efecto sobre tipo Tierra.");
        }

        // Prueba para un caso neutral (ataque que no es efectivo ni poco efectivo)
        [Test]
        public void NeutralEffect()
        {
            // Act: Se calcula el multiplicador para un ataque de tipo Agua contra un Pokémon de tipo Eléctrico
            double multiplier = TypeLogic.CalculeMultiplier("Agua", "Eléctrico");
            // Assert: El multiplicador debe ser 1
            Assert.That(multiplier, Is.EqualTo(1), "El ataque de tipo Agua debería ser neutral contra tipo Eléctrico.");
        }

        // Prueba para un tipo inexistente como atacante
        [Test]
        public void UnknownAttackerType()
        {
            // Act: Se calcula el multiplicador para un tipo atacante inexistente
            double multiplier = TypeLogic.CalculeMultiplier("Desconocido", "Fuego");
            // Assert: El multiplicador debe ser 1
            Assert.That(multiplier, Is.EqualTo(1), "Un tipo atacante desconocido debería resultar en un multiplicador neutral.");
        }

        // Prueba para un tipo inexistente como defensor
        [Test]
        public void UnknownDefenderType()
        {
            // Act: Se calcula el multiplicador para un ataque contra un tipo defensor inexistente
            double multiplier = TypeLogic.CalculeMultiplier("Fuego", "Desconocido");
            // Assert: El multiplicador debe ser 1
            Assert.That(multiplier, Is.EqualTo(1), "Un tipo defensor desconocido debería resultar en un multiplicador neutral.");
        }
    }
}
