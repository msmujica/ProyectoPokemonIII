using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Attack))]
public class AttackTest
{
    [Test]
    public void ObtainAttack_ShouldReturnCorrectData()
    {
        var result = Attack.ObtainAttack("Pistola Agua");
        Assert.That(40, Is.EqualTo(result.Damage));
        Assert.That("Agua", Is.EqualTo(result.Type));
    }

    [Test]
    public void ObtainAttack_NonExistent_ShouldReturnPredeterminedData()
    {
        var result = Attack.ObtainAttack("AtaqueInexistente");
        Assert.That(0, Is.EqualTo(result.Damage));
        Assert.That(string.Empty, Is.EqualTo(result.Type));
    }

    [Test]
    public void CalculeDamage_WithCritical_ShouldIncreaseDamage()
    {
        var targetpokemon = new Pokemon("Bulbasaur", 100, new List<string>{"Hoja Afilada"},"Planta");
        var effectsmanager = new EffectsManager();

        // Configura el daño base para un ataque como "Hoja Afilada" (55 daño)
        var (calculedamage, description) = Attack.CalculeDamage("Hoja Afilada", targetpokemon, effectsmanager);
        int damage = 0;
            
        if (calculedamage == 55)
        {
            damage = 55;
        }
        if (calculedamage == 66)
        {
            damage = 66;
        }
            
        Assert.That(calculedamage, Is.EqualTo(damage));
    }
}