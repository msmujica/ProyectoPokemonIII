using Library;
using NUnit.Framework;


namespace LibraryTests.Domain
{
    [TestFixture]
    [TestOf(typeof(TypeLogic))]
    public class TypeLogicTest
    {
        /// <summary>
        /// Prueba para verificar que el tipo "Fuego" es super efectivo contra "Planta"
        /// </summary>
        [Test]
        public void Weaknesses()
        {
            double multiplier = TypeLogic.CalculeMultiplier("Fuego", "Planta");
            
            Assert.That(multiplier, Is.EqualTo(2), "El ataque de tipo Fuego debería ser super efectivo contra tipo Planta.");
        }

        /// <summary>
        /// Prueba para verificar que el tipo "Fuego" es poco efectivo contra "Agua"
        /// </summary>
        [Test]
        public void Resistances()
        {
            double multiplier = TypeLogic.CalculeMultiplier("Fuego", "Agua");
            
            Assert.That(multiplier, Is.EqualTo(0.5), "El ataque de tipo Fuego debería ser poco efectivo contra tipo Agua.");
        }

        /// <summary>
        /// Prueba para verificar que el tipo "Eléctrico" no tiene efecto sobre "Tierra"
        /// </summary>
        [Test]
        public void Immunities()
        {
            double multiplier = TypeLogic.CalculeMultiplier("Eléctrico", "Eléctrico");
            
            Assert.That(multiplier, Is.EqualTo(0), "El ataque de tipo Eléctrico no tiene efecto sobre tipo Tierra.");
        }

        /// <summary>
        /// Prueba para un caso neutral (ataque que no es efectivo ni poco efectivo)
        /// </summary>
        [Test]
        public void NeutralEffect()
        {
            double multiplier = TypeLogic.CalculeMultiplier("Agua", "Eléctrico");
            
            Assert.That(multiplier, Is.EqualTo(1), "El ataque de tipo Agua debería ser neutral contra tipo Eléctrico.");
        }

        /// <summary>
        /// Prueba para un tipo inexistente como atacante
        /// </summary>
        [Test]
        public void UnknownAttackerType()
        {
            double multiplier = TypeLogic.CalculeMultiplier("Desconocido", "Fuego");
            
            Assert.That(multiplier, Is.EqualTo(1), "Un tipo atacante desconocido debería resultar en un multiplicador neutral.");
        }

        /// <summary>
        /// Prueba para un tipo inexistente como defensor
        /// </summary>
        [Test]
        public void UnknownDefenderType()
        {
            double multiplier = TypeLogic.CalculeMultiplier("Fuego", "Desconocido");
            
            Assert.That(multiplier, Is.EqualTo(1), "Un tipo defensor desconocido debería resultar en un multiplicador neutral.");
        }
    }
}
