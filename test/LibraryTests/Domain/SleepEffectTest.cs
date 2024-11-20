using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(SleepEffect))]
public class SleepEffectTest
{

    [Test]
    public void TestStartEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        SleepEffect sleep = new SleepEffect();
        string result = sleep.StartEffect(Pikachu);
        Assert.That(result, !Is.Null);
    }

    [Test]
    public void TestProcessEffect()
    {
        Pokemon Pikachus = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        SleepEffect sleep = new SleepEffect();
        sleep.StartEffect(Pikachus);
        string result = sleep.ProcessEffect(Pikachus);
        Assert.That(result, !Is.EqualTo(""));
    }
}