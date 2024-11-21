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
}