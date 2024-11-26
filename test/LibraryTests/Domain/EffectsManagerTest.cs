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
        public void IsParalyzeWhenPokemonHasParalyzeEffectReturnsTrue()
        {
            manager.ApplyEffect(paralyzeEffect, pikachu);

            bool result = manager.IsParalyze(pikachu);
            
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IsParalyze"/> devuelva falso cuando un Pokémon no tenga efectos activos.
        /// </summary>
        [Test]
        public void IsParalyzeWhenPokemonHasNoEffectsReturnsFalse()
        {
            bool result = manager.IsParalyze(pikachu);
            
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IsParalyze"/> devuelva falso cuando un Pokémon tenga un efecto distinto de parálisis.
        /// </summary>
        [Test]
        public void IsParalyzeWhenPokemonHasOtherEffectsReturnsFalse()
        {
            manager.ApplyEffect(sleepEffect, pikachu);
            
            bool result = manager.IsParalyze(pikachu);
            
            Assert.That(result, Is.False);
        }

        // Tests para IcanAttack
        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IcanAttack"/> devuelva falso cuando un Pokémon tenga el efecto de parálisis y no pueda atacar.
        /// </summary>
        [Test]
        public void IcanAttackWhenPokemonHasParalyzeEffectAndCannotAttackReturnsFalse()
        {
            manager.ApplyEffect(paralyzeEffect, pikachu);
            
            bool result = manager.IcanAttack(pikachu);
            
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IcanAttack"/> devuelva falso cuando un Pokémon tenga el efecto de sueño y no pueda atacar.
        /// </summary>
        [Test]
        public void IcanAttackWhenPokemonHasSleepEffectAndCannotAttackReturnsFalse()
        {
            manager.ApplyEffect(sleepEffect, charmander);

            bool result = manager.IcanAttack(charmander);
            
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IcanAttack"/> devuelva verdadero cuando un Pokémon no tenga efectos que le impidan atacar.
        /// </summary>
        [Test]
        public void IcanAttackWhenPokemonHasNoControlEffectsReturnsTrue()
        {
            bool result = manager.IcanAttack(pikachu);
            
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.IcanAttack"/> devuelva verdadero cuando un Pokémon tenga un efecto que no controle su capacidad de atacar.
        /// </summary>
        [Test]
        public void IcanAttackWhenPokemonHasNonControlEffectReturnsTrue()
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
        public void ProcesarControlMasaWhenPokemonHasSleepEffectReturnsSleepEffectDescription()
        {
            
            manager.ApplyEffect(sleepEffect, squirtle);

            
            string result = manager.ProcesarControlMasa(squirtle);

            
            Assert.That(result, Is.EqualTo(sleepEffect.Info(squirtle)));
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.ProcesarControlMasa"/> devuelva la descripción del efecto de parálisis cuando un Pokémon tenga ese efecto.
        /// </summary>
        [Test]
        public void ProcesarControlMasaWhenPokemonHasParalyzeEffectReturnsParalyzeEffectDescription()
        {
           
            manager.ApplyEffect(paralyzeEffect, pikachu);

            
            string result = manager.ProcesarControlMasa(pikachu);

            
            Assert.That(result, Is.EqualTo(paralyzeEffect.Info(pikachu)));
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.ProcesarControlMasa"/> devuelva un mensaje de que el Pokémon no tiene efectos activos cuando no tenga efectos.
        /// </summary>
        [Test]
        public void ProcesarControlMasaWhenPokemonHasNoControlEffectsReturnsNoEffectsMessage()
        {
           
            string result = manager.ProcesarControlMasa(charmander);

            Assert.That(result, Is.EqualTo($"El pokemon {charmander.Name} no tiene efectos activos."));
        }

        /// <summary>
        /// Prueba que el método <see cref="EffectsManager.ProcesarControlMasa"/> ignore los efectos no controlables, como el veneno.
        /// </summary>
        [Test]
        public void ProcesarControlMasaWhenPokemonHasNonControlEffectIgnoresEffect()
        {
            manager.ApplyEffect(poisonEffect, pikachu);
            string result = manager.ProcesarControlMasa(pikachu);
            Assert.That(result, Is.EqualTo($""));
        }
        
        
        [Test]
        public void ProcesarEfectosDañoWhenPokemonHasNoEffectsReturnsEmptyMessage()
        {
            
            string result = manager.ProcesarEfectosDaño(squirtle);

          
            Assert.That(result, Is.EqualTo(""));
        }
        
        
        [Test]
        public void CleanEffectsWhenPokemonHasEffectsRemovesEffectsAndReturnsMessage()
        {
            manager.ApplyEffect(paralyzeEffect, pikachu);
            
            string result = manager.CleanEffects(pikachu);
            
            Assert.That(result, Is.EqualTo($"Todos los efectos han sido eliminados de {pikachu.Name}."));
            Assert.That(manager.PokemonWithEffect(pikachu), Is.False); // Verifica que los efectos han sido eliminados
        }
        

        [Test]
        public void CleanEffectsWhenPokemonHasNoEffectsReturnsEmptyMessage()
        {
          
            string result = manager.CleanEffects(charmander);

           
            Assert.That(result, Is.EqualTo(""));
        }
        
        [Test]
        public void ApplyEffectWhenEffectIsNullReturnsEmptyString()
        {
            string result = manager.ApplyEffect(null, pikachu);
            
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ApplyEffectWhenPokemonIsNullReturnsEmptyString()
        {
            string result = manager.ApplyEffect(paralyzeEffect, null);
            
            Assert.That(result, Is.Empty);
        }
    }
}
