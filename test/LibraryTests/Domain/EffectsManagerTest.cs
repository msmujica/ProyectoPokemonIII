using Library;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(EffectsManager))]
public class EffectsManagerTest

{
        [Test]
        public void ApplyEffects()
        {
            var pokemon = new Pokemon("Pikachu", 100, new List<string>{"Impactrueno"} ,"Eléctrico");
            var effectsManager = new EffectsManager();
            var effect = new ParalyzeEffect(); 
            effectsManager.ApplyEffect(effect, pokemon);

                ClassicAssert.IsTrue(effectsManager.PokemonWithEffect(pokemon));
        }

        [Test]
        public void CleanEffects()
        {
            var pokemon = new Pokemon("Pikachu", 100, new List<string>{"Impactrueno"} ,"Eléctrico");
            var effectsManager = new EffectsManager();
            var effect = new ParalyzeEffect();

            effectsManager.ApplyEffect(effect, pokemon);
            effectsManager.CleanEffects(pokemon);

            ClassicAssert.IsFalse(effectsManager.PokemonWithEffect(pokemon));
        }
        

        [Test]
        public void ProcessEffectsDamage()
        {
            var pokemon = new Pokemon("Venusaur", 100, new List<string>{"Látigo Cepa"},"Planta");
            var effectsManager = new EffectsManager();
            var effect = new PoisonEffect(); 
            effectsManager.ApplyEffect(effect, pokemon);
            effectsManager.ProcesarEfectosDaño(pokemon); // Procesa el daño de efectos

            // Aquí se debería verificar que el daño se haya aplicado correctamente
            ClassicAssert.AreEqual(95, pokemon.Health); 
        }
    
}