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

        /// <summary>
        /// Configura el entorno de prueba antes de cada prueba, inicializando los objetos necesarios.
        /// </summary>
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
        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IsParalyze"/> devuelva verdadero cuando un Pokémon tenga el efecto de parálisis.
        /// </summary>
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

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IsParalyze"/> devuelva falso cuando un Pokémon no tenga efectos activos.
        /// </summary>
        [Test]
        public void IsParalyze_WhenPokemonHasNoEffects_ReturnsFalse()
        {
            // Act
            bool result = manager.IsParalyze(pikachu);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IsParalyze"/> devuelva falso cuando un Pokémon tenga un efecto distinto de parálisis.
        /// </summary>
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
        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IcanAttack"/> devuelva falso cuando un Pokémon tenga el efecto de parálisis y no pueda atacar.
        /// </summary>
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

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IcanAttack"/> devuelva falso cuando un Pokémon tenga el efecto de sueño y no pueda atacar.
        /// </summary>
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

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IcanAttack"/> devuelva verdadero cuando un Pokémon no tenga efectos que le impidan atacar.
        /// </summary>
        [Test]
        public void IcanAttack_WhenPokemonHasNoControlEffects_ReturnsTrue()
        {
            bool result = manager.IcanAttack(pikachu);
            
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IcanAttack"/> devuelva verdadero cuando un Pokémon tenga un efecto que no controle su capacidad de atacar.
        /// </summary>
        [Test]
        public void IcanAttack_WhenPokemonHasNonControlEffect_ReturnsTrue()
        {
            manager.ApplyEffect(poisonEffect, pikachu);
            bool result = manager.IcanAttack(pikachu);
            Assert.That(result, Is.True);
        }

        // Tests para ProcesarControlMasa
        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.ProcesarControlMasa"/> devuelva la descripción del efecto de sueño cuando un Pokémon tenga ese efecto.
        /// </summary>
        [Test]
        public void ProcesarControlMasa_WhenPokemonHasSleepEffect_ReturnsSleepEffectDescription()
        {
            
            manager.ApplyEffect(sleepEffect, squirtle);

            
            string result = manager.ProcesarControlMasa(squirtle);

            
            Assert.That(result, Is.EqualTo(sleepEffect.Info(squirtle)));
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.ProcesarControlMasa"/> devuelva la descripción del efecto de parálisis cuando un Pokémon tenga ese efecto.
        /// </summary>
        [Test]
        public void ProcesarControlMasa_WhenPokemonHasParalyzeEffect_ReturnsParalyzeEffectDescription()
        {
           
            manager.ApplyEffect(paralyzeEffect, pikachu);

            
            string result = manager.ProcesarControlMasa(pikachu);

            
            Assert.That(result, Is.EqualTo(paralyzeEffect.Info(pikachu)));
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.ProcesarControlMasa"/> devuelva un mensaje de que el Pokémon no tiene efectos activos cuando no tenga efectos.
        /// </summary>
        [Test]
        public void ProcesarControlMasa_WhenPokemonHasNoControlEffects_ReturnsNoEffectsMessage()
        {
           
            string result = manager.ProcesarControlMasa(charmander);

            Assert.That(result, Is.EqualTo($"El pokemon {charmander.Name} no tiene efectos activos."));
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.ProcesarControlMasa"/> ignore los efectos no controlables, como el veneno.
        /// </summary>
        [Test]
        public void ProcesarControlMasa_WhenPokemonHasNonControlEffect_IgnoresEffect()
        {
            manager.ApplyEffect(poisonEffect, pikachu);
            string result = manager.ProcesarControlMasa(pikachu);
            Assert.That(result, Is.EqualTo($""));
        }
        
        
        [Test]
        public void ProcesarEfectosDaño_WhenPokemonHasNoEffects_ReturnsEmptyMessage()
        {
            
            string result = manager.ProcesarEfectosDaño(squirtle);

          
            Assert.That(result, Is.EqualTo(""));
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
