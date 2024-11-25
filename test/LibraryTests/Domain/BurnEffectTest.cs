using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(BurnEffect))]
public class BurnEffectTest
{
    /// <summary>
    /// Prueba el inicio del efecto de quemadura en un Pokémon.
    /// Verifica que el mensaje correcto se devuelva cuando se inicia el efecto.
    /// </summary>
    [Test]
    public void TestStartEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
        BurnEffect burn = new BurnEffect();
        string result = burn.StartEffect(Pikachu);
        Assert.That("El pokemon Pikachu ha sido quemado.", Is.EqualTo(result));
    }

    /// <summary>
    /// Prueba el procesamiento del efecto de quemadura en un Pokémon.
    /// Verifica que el resultado del procesamiento del efecto no sea nulo.
    /// </summary>
    [Test]
    public void TestProcessEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
        BurnEffect burn = new BurnEffect();
        burn.StartEffect(Pikachu);
        string result = burn.ProcessEffect(Pikachu);
        Assert.That(result, !Is.Null);
    }
    
    /// <summary>
    /// Prueba el procesamiento del efecto de quemadura cuando el Pokémon está derrotado.
    /// Verifica que el mensaje correcto se devuelva cuando el Pokémon ha caído debido a la quemadura.
    /// </summary>
    [Test]
    public void TestProcessEffectPokemonDied()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 0, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
        BurnEffect burn = new BurnEffect();
        burn.StartEffect(Pikachu);
        string result = burn.ProcessEffect(Pikachu);
        Assert.That(result, Is.EqualTo("El pokemon Pikachu ha caído por quemadura. "));
    }
    
    /// <summary>
    /// Prueba la obtención de la información del efecto de quemadura en un Pokémon.
    /// Verifica que el mensaje correcto se devuelva sobre el estado del Pokémon.
    /// </summary>
    [Test]
    public void TestInfo()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
        BurnEffect burn = new BurnEffect();
        
        Assert.That("Al pokemon Pikachu tiene el efecto quemadura. ", Is.EqualTo(burn.Info(Pikachu)));
    }

    /// <summary>
    /// Prueba si un Pokémon afectado por quemadura puede atacar.
    /// Verifica que la respuesta de la propiedad <see cref="BurnEffect.IcanAttack"/> sea verdadera.
    /// </summary>
    [Test]
    public void TestICanAttack()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
        BurnEffect burn = new BurnEffect();
        
        Assert.That(true, Is.EqualTo(burn.IcanAttack));
    }
}
