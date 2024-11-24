using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(PoisonEffect))]
public class PoisonEffectTest
{

    [Test]
    public void TestStartEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poison = new PoisonEffect();
        string result = poison.StartEffect(Pikachu);
        Assert.That("El pokemon Pikachu ha sido envenenado, perdera vida cada turno.", Is.EqualTo(result));
    }

    [Test]
    public void TestProcessEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poison = new PoisonEffect();
        poison.StartEffect(Pikachu);
        string result = poison.ProcessEffect(Pikachu);
        Assert.That(result, !Is.Null);
    }
    
    [Test]
    public void TestProcessEffectPokemonDied()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 0, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poison = new PoisonEffect();
        poison.StartEffect(Pikachu);
        string result = poison.ProcessEffect(Pikachu);
        Assert.That(result, Is.EqualTo("El pokemon Pikachu ha caído por envenenamiento. "));
    }
    
    [Test]
    public void TestInfo()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poision = new PoisonEffect();
        
        Assert.That("Al pokemon Pikachu tiene el efecto envenenamiento. ", Is.EqualTo(poision.Info(Pikachu)));
    }

    [Test]
    public void TestICanAttack()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poision = new PoisonEffect();
        
        Assert.That(true, Is.EqualTo(poision.IcanAttack));
    }
}