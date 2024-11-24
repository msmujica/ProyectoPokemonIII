using NUnit.Framework;

namespace Library.Tests
{
    [TestFixture]
    public class EffectsManagerTests
    {
        private EffectsManager manager;
        private Pokemon pikachu;
        private Pokemon charmander;
        private Pokemon squirtle;
        private ParalyzeEffect paralyzeEffect;
        private SleepEffect sleepEffect;
        private PoisonEffect poisonEffect; // Efecto adicional para pruebas


        [SetUp]
        public void SetUp()
        {
            manager = new EffectsManager();
            pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");
            charmander = new Pokemon("Charmander", 100, new List<string> { "Impactrueno" }, "Eléctrico");
            squirtle = new Pokemon("Squirtle", 100, new List<string> { "Impactrueno" }, "Eléctrico");
            paralyzeEffect = new ParalyzeEffect { IcanAttack = false };
            sleepEffect = new SleepEffect { IcanAttack = false };
            poisonEffect = new PoisonEffect(); // Daño continuo, no afecta ataque
        }

        // Tests para IsParalyze
        [Test]
        public void IsParalyze_WhenPokemonHasParalyzeEffect_ReturnsTrue()
        {
            // Arrange
            manager.ApplyEffect(paralyzeEffect, pikachu);

            // Act
            bool result = manager.IsParalyze(pikachu);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsParalyze_WhenPokemonHasNoEffects_ReturnsFalse()
        {
            // Act
            bool result = manager.IsParalyze(pikachu);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsParalyze_WhenPokemonHasOtherEffects_ReturnsFalse()
        {
            // Arrange
            manager.ApplyEffect(sleepEffect, pikachu);

            // Act
            bool result = manager.IsParalyze(pikachu);

            // Assert
            Assert.That(result, Is.False);
        }

        // Tests para IcanAttack
        [Test]
        public void IcanAttack_WhenPokemonHasParalyzeEffectAndCannotAttack_ReturnsFalse()
        {
            // Arrange
            manager.ApplyEffect(paralyzeEffect, pikachu);

            // Act
            bool result = manager.IcanAttack(pikachu);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IcanAttack_WhenPokemonHasSleepEffectAndCannotAttack_ReturnsFalse()
        {
            // Arrange
            manager.ApplyEffect(sleepEffect, charmander);

            // Act
            bool result = manager.IcanAttack(charmander);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IcanAttack_WhenPokemonHasNoControlEffects_ReturnsTrue()
        {
            // Act
            bool result = manager.IcanAttack(pikachu);

            // Assert
            Assert.That(result, Is.True);
        }
        [Test]
        public void IcanAttack_WhenPokemonHasNonControlEffect_ReturnsTrue()
        {
            manager.ApplyEffect(poisonEffect, pikachu);
            bool result = manager.IcanAttack(pikachu);
            Assert.That(result, Is.True);
        }

        // Tests para ProcesarControlMasa
        [Test]
        public void ProcesarControlMasa_WhenPokemonHasSleepEffect_ReturnsSleepEffectDescription()
        {
            // Arrange
            manager.ApplyEffect(sleepEffect, squirtle);

            // Act
            string result = manager.ProcesarControlMasa(squirtle);

            // Assert
            Assert.That(result, Is.EqualTo(sleepEffect.Info(squirtle)));
        }

        [Test]
        public void ProcesarControlMasa_WhenPokemonHasParalyzeEffect_ReturnsParalyzeEffectDescription()
        {
            // Arrange
            manager.ApplyEffect(paralyzeEffect, pikachu);

            // Act
            string result = manager.ProcesarControlMasa(pikachu);

            // Assert
            Assert.That(result, Is.EqualTo(paralyzeEffect.Info(pikachu)));
        }

        [Test]
        public void ProcesarControlMasa_WhenPokemonHasNoControlEffects_ReturnsNoEffectsMessage()
        {
            // Act
            string result = manager.ProcesarControlMasa(charmander);

            Assert.That(result, Is.EqualTo($"El pokemon {charmander.Name} no tiene efectos activos."));
        }
        [Test]
        public void ProcesarControlMasa_WhenPokemonHasNonControlEffect_IgnoresEffect()
        {
            manager.ApplyEffect(poisonEffect, pikachu);
            string result = manager.ProcesarControlMasa(pikachu);
            Assert.That(result, Is.EqualTo( $""));
        }
    }
}
