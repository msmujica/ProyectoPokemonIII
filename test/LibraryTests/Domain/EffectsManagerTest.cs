using Library;
using NUnit.Framework;

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

        Assert.That(effectsManager.PokemonWithEffect(pokemon), Is.True);
    }

    [Test]
    public void CleanEffects()
    {
        var pokemon = new Pokemon("Pikachu", 100, new List<string>{"Impactrueno"} ,"Eléctrico");
        var effectsManager = new EffectsManager();
        var effect = new ParalyzeEffect();

        effectsManager.ApplyEffect(effect, pokemon);
        effectsManager.CleanEffects(pokemon);

        Assert.That(effectsManager.PokemonWithEffect(pokemon), Is.False);
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
        Assert.That(100, Is.EqualTo(pokemon.Health)); 
    }
    
}