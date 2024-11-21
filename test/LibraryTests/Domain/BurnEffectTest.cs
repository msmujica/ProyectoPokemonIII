using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(BurnEffect))]
public class BurnEffectTest
{

    [Test]
    public void TestStartEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        BurnEffect burn = new BurnEffect();
        string result = burn.StartEffect(Pikachu);
        Assert.That("El pokemon Pikachu ha sido quemado.", Is.EqualTo(result));
    }

    [Test]
    public void TestProcessEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        BurnEffect burn = new BurnEffect();
        burn.StartEffect(Pikachu);
        string result = burn.ProcessEffect(Pikachu);
        Assert.That(result, !Is.Null);
    }
}