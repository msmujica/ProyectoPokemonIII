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
            bool result = manager.IcanAttack(pikachu);
            
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
            
            manager.ApplyEffect(sleepEffect, squirtle);

            
            string result = manager.ProcesarControlMasa(squirtle);

            
            Assert.That(result, Is.EqualTo(sleepEffect.Info(squirtle)));
        }

        [Test]
        public void ProcesarControlMasa_WhenPokemonHasParalyzeEffect_ReturnsParalyzeEffectDescription()
        {
           
            manager.ApplyEffect(paralyzeEffect, pikachu);

            
            string result = manager.ProcesarControlMasa(pikachu);

            
            Assert.That(result, Is.EqualTo(paralyzeEffect.Info(pikachu)));
        }

        [Test]
        public void ProcesarControlMasa_WhenPokemonHasNoControlEffects_ReturnsNoEffectsMessage()
        {
           
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
        
        [Test]
        public void ProcesarEfectosDaño_WhenPokemonHasContinuousDamageEffect_ProcessesEffect()
        {
            
            manager.ApplyEffect(poisonEffect, charmander); // Aplicamos a Charmander en vez de Pikachu

           
            string result = manager.ProcesarEfectosDaño(charmander); // Procesamos Charmander

          
            Assert.That(result, Is.Not.Empty); // Verificamos que no sea vacío
            Assert.That(result, Does.Contain("ha sufrido")); // Confirmamos que el mensaje contiene el texto esperado
        }



        [Test]
        public void ProcesarEfectosDaño_WhenPokemonHasNoEffects_ReturnsEmptyMessage()
        {
            
            string result = manager.ProcesarEfectosDaño(squirtle);

          
            Assert.That(result, Is.EqualTo(""));
        }
        
        [Test]
        public void ProcesarEfectosDaño_WhenOtherPokemonHasEffects_ProcessesEffects()
        {
            
            manager.ApplyEffect(poisonEffect, charmander); // Agregamos un efecto a Charmander

            
            string result = manager.ProcesarEfectosDaño(squirtle); // Procesamos efectos en Squirtle

            
            Assert.That(result, Is.Empty); // Confirmamos que el resultado es vacío, ya que Squirtle no tiene efectos
        }
        
        [Test]
        public void CleanEffects_WhenPokemonHasEffects_RemovesEffectsAndReturnsMessage()
        {
            // Arrange
            manager.ApplyEffect(paralyzeEffect, pikachu);

            // Act
            string result = manager.CleanEffects(pikachu);

            // Assert
            Assert.That(result, Is.EqualTo($"Todos los efectos han sido eliminados de {pikachu.Name}."));
            Assert.That(manager.PokemonWithEffect(pikachu), Is.False); // Verifica que los efectos han sido eliminados
        }
        

        [Test]
        public void CleanEffects_WhenPokemonHasNoEffects_ReturnsEmptyMessage()
        {
          
            string result = manager.CleanEffects(charmander);

           
            Assert.That(result, Is.EqualTo(""));
        }
        
        [Test]
        public void ApplyEffect_WhenEffectIsNull_ReturnsEmptyString()
        {
            // Act
            string result = manager.ApplyEffect(null, pikachu);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ApplyEffect_WhenPokemonIsNull_ReturnsEmptyString()
        {
            // Act
            string result = manager.ApplyEffect(paralyzeEffect, null);

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}
