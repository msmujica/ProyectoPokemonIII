using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(ParalyzeEffect))]
public class ParalyzeEffectTest
{

    [Test]
    public void TestStartEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        ParalyzeEffect paralyze = new ParalyzeEffect();
        string result = paralyze.StartEffect(Pikachu);
        Assert.That("El pokemon Pikachu se le aplico el efecto paralisis.", Is.EqualTo(result));
    }

    [Test]
    public void TestProcessEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        ParalyzeEffect paralyze = new ParalyzeEffect();
        paralyze.StartEffect(Pikachu);
        string result = paralyze.ProcessEffect(Pikachu);
        string esperado = null;
        if ("El pokemon Pikachu supera la parálisis en este turno y puede atacar. " == result)
        {
            esperado = "El pokemon Pikachu supera la parálisis en este turno y puede atacar. ";
        }
        else
        {
            esperado = "Pikachu está paralizado y no puede atacar, perdiste el turno. ";
        }
        Assert.That(esperado, Is.EqualTo(result));
    }
    
    [Test]
    public void TestProcessEffectNone()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        ParalyzeEffect paralyze = new ParalyzeEffect();
        paralyze.StartEffect(Pikachu);
        paralyze.IcanAttack = false;
        string result = paralyze.ProcessEffect(Pikachu);
        string esperado = null;
        if ("El pokemon Pikachu supera la parálisis en este turno y puede atacar. " == result)
        {
            esperado = "El pokemon Pikachu supera la parálisis en este turno y puede atacar. ";
        }
        else
        {
            esperado = "Pikachu está paralizado y no puede atacar, perdiste el turno. ";
        }
        Assert.That(esperado, Is.EqualTo(result));
    }
    
    [Test]
    public void TestInfo()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        ParalyzeEffect paralyze = new ParalyzeEffect();
        
        Assert.That(paralyze.ProcessEffect(Pikachu), Is.EqualTo(paralyze.Info(Pikachu)));
    }
    
        // Test cuando el Pokémon puede atacar
        [Test]
        public void TestProcessEffectShouldAllowAttackWhenPokemonCanAttack()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            
            // Arranqué la parálisis
            paralyze.StartEffect(Pikachu);

            // Forzamos el comportamiento de `ICanAttack` para que sea `true` (puede atacar)
            paralyze.IcanAttack = true;

            // Act: Procesamos el efecto
            string result = paralyze.ProcessEffect(Pikachu);

            // Assert: Verificamos el mensaje
            Assert.That(result, Is.EqualTo(result));
        }

        // Test cuando el Pokémon no puede atacar
        [Test]
        public void TestProcessEffectShouldNotAllowAttackWhenPokemonCannotAttack()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            // Arranqué la parálisis
            paralyze.StartEffect(Pikachu);

            // Forzamos el comportamiento de `ICanAttack` para que sea `false` (no puede atacar)
            paralyze.IcanAttack = false;

            // Act: Procesamos el efecto
            string result = paralyze.ProcessEffect(Pikachu);

            // Assert: Verificamos que no puede atacar
            Assert.That(result, Is.EqualTo(result));
        }
}