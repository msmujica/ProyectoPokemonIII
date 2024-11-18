using JetBrains.Annotations;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Ucu.Poo.DiscordBot.Domain.Tests.Domain;

[TestClass]
[TestSubject(typeof(Attack))]
public class AttackTest
{
    [TestMethod]
    public void ObtainAttack_ShouldReturnCorrectData()
    {
        var result = Attack.ObtainAttack("Pistola Agua");
        Assert.AreEqual(40, result.Damage);
        Assert.AreEqual("Agua", result.Type);
    }

    [TestMethod]
    public void ObtainAttack_NonExistent_ShouldReturnPredeterminedData()
    {
        var result = Attack.ObtainAttack("AtaqueInexistente");
        Assert.AreEqual(0, result.Damage);
        Assert.AreEqual(string.Empty, result.Type);
    }

    [TestMethod]
    public void CalculeDamage_WithCritical_ShouldIncreaseDamage()
    {
        var targetpokemon = new Pokemon("Bulbasaur", 100, new List<string>{"Hoja Afilada"},"Planta");
        var effectsmanager = new EffectsManager();

        // Configura el daño base para un ataque como "Hoja Afilada" (55 daño)
        var (calculedamage, description) = Attack.CalculeDamage("Hoja Afilada", targetpokemon, effectsmanager);

        // Si el ataque es crítico, el daño debería multiplicarse por 1.2
        Assert.AreEqual(calculedamage, 55);
    }
}