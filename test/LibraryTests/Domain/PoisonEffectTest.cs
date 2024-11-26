using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(PoisonEffect))]
public class PoisonEffectTest
{
    /// <summary>
    /// Prueba que verifica el inicio del efecto de envenenamiento en un Pokemon.
    /// Se asegura de que el mensaje correcto se devuelva cuando el efecto es aplicado.
    /// </summary>

    [Test]
    public void TestStartEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poison = new PoisonEffect();
        string result = poison.StartEffect(Pikachu);
        Assert.That("El pokemon Pikachu ha sido envenenado, perdera vida cada turno.", Is.EqualTo(result));
    }
    /// <summary>
    /// Verifica el procesamiento del efecto de envenenamiento en un pokemon
    /// </summary>
    [Test]
    public void TestProcessEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poison = new PoisonEffect();
        poison.StartEffect(Pikachu);
        string result = poison.ProcessEffect(Pikachu);
        Assert.That(result, !Is.Null);
    }
    
    /// <summary>
    /// Verifica el processamiento del efecto envenenamiento cuando el pokemon es derrotado
    /// </summary>
    [Test]
    public void TestProcessEffectPokemonDied()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 0, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poison = new PoisonEffect();
        poison.StartEffect(Pikachu);
        string result = poison.ProcessEffect(Pikachu);
        Assert.That(result, Is.EqualTo("El pokemon Pikachu ha caído por envenenamiento. "));
    }
    
    /// <summary>
    /// Verifica la informacion sobre el efecto de envenenamiento en un pokemon.
    /// </summary>
    [Test]
    public void TestInfo()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poision = new PoisonEffect();
        
        Assert.That("Al pokemon Pikachu tiene el efecto envenenamiento. ", Is.EqualTo(poision.Info(Pikachu)));
    }
    
    /// <summary>
    /// Verifica si un pokemon puede atacar mientras tiene el efecto de envenamiento.
    /// </summary>
    [Test]
    public void TestICanAttack()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        PoisonEffect poision = new PoisonEffect();
        
        Assert.That(true, Is.EqualTo(poision.IcanAttack));
    }
}