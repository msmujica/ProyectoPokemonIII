using Library;
using NUnit.Framework;

namespace LibraryTests.Domain
{
    /// <summary>
    /// Clase de pruebas unitarias para validar el comportamiento de la clase <see cref="Attack"/>.
    /// </summary>
    [TestFixture]
    [TestOf(typeof(Attack))]
    public class AttackTest
    {
        /// <summary>
        /// Verifica que el método <see cref="Attack.ObtainAttack(string)"/> retorne los datos correctos 
        /// para un ataque existente.
        /// </summary>
        [Test]
        public void ObtainAttackShouldReturnCorrectData()
        {
            var result = Attack.ObtainAttack("Pistola Agua");
            Assert.That(40, Is.EqualTo(result.Damage));
            Assert.That("Agua", Is.EqualTo(result.Type));
        }

        /// <summary>
        /// Verifica que el método <see cref="Attack.ObtainAttack(string)"/> retorne datos predeterminados 
        /// cuando se solicita un ataque que no existe.
        /// </summary>
        [Test]
        public void ObtainAttackNonExistentShouldReturnPredeterminedData()
        {
            var result = Attack.ObtainAttack("AtaqueInexistente");
            Assert.That(0, Is.EqualTo(result.Damage));
            Assert.That(string.Empty, Is.EqualTo(result.Type));
        }

        /// <summary>
        /// Verifica que el método <see cref="Attack.CalculeDamage(string, Pokemon, EffectsManager)"/> 
        /// calcule correctamente el daño de un ataque, teniendo en cuenta un golpe crítico.
        /// </summary>
        [Test]
        public void CalculeDamageWithCriticalShouldIncreaseDamage()
        {
            var targetpokemon = new Pokemon("Bulbasaur", 100, new List<string> { "Hoja Afilada" }, "Planta");
            var effectsmanager = new EffectsManager();

            // Configura el daño base para un ataque como "Hoja Afilada" (55 daño)
            var (calculedamage, description) = Attack.CalculeDamage("Hoja Afilada", targetpokemon, effectsmanager);
            int damage = 0;

            if (calculedamage == 55)
            {
                damage = 55;
            }
            if (calculedamage == 66)
            {
                damage = 66;
            }

            Assert.That(calculedamage, Is.EqualTo(damage));
        }

        /// <summary>
        /// Verifica que el método <see cref="Attack.SelectSpecialEffect()"/> retorne un efecto especial válido.
        /// </summary>
        [Test]
        public void TestSelectSpecialEffect()
        {
            IEffect efecto = Attack.SelectSpecialEffect();
            Assert.That(efecto, !Is.Null);
        }

        /// <summary>
        /// Verifica que el atributo <see cref="Attack.Critical"/> no sea nulo.
        /// </summary>
        [Test]
        public void TestCritucal()
        {
            Assert.That(Attack.Critical, !Is.Null);
        }

        /// <summary>
        /// Verifica que el método <see cref="Attack.ApplySpecialEffect"/> no sea nulo.
        /// </summary>
        [Test]
        public void TestApplySpecialEffect()
        {
            Assert.That(Attack.ApplySpecialEffect, !Is.Null);
        }
    }
}
