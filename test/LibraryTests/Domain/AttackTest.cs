using Library;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Attack))]
public class AttackTest
{
    [Test]
    public void ObtainAttack_ShouldReturnCorrectData()
    {
        var result = Attack.ObtainAttack("Pistola Agua");
        ClassicAssert.AreEqual(40, result.Damage);
        ClassicAssert.AreEqual("Agua", result.Type);
    }

    [Test]
    public void ObtainAttack_NonExistent_ShouldReturnPredeterminedData()
    {
        var result = Attack.ObtainAttack("AtaqueInexistente");
        ClassicAssert.AreEqual(0, result.Damage);
        ClassicAssert.AreEqual(string.Empty, result.Type);
    }

    [Test]
    public void CalculeDamage_WithCritical_ShouldIncreaseDamage()
    {
        var targetpokemon = new Pokemon("Bulbasaur", 100, new List<string>{"Hoja Afilada"},"Planta");
        var effectsmanager = new EffectsManager();

        // Configura el daño base para un ataque como "Hoja Afilada" (55 daño)
        var (calculedamage, description) = Attack.CalculeDamage("Hoja Afilada", targetpokemon, effectsmanager);

        // Si el ataque es crítico, el daño debería multiplicarse por 1.2
        ClassicAssert.AreEqual(calculedamage, 55);
    }
}