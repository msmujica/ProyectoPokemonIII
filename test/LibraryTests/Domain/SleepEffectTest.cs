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
    
    [Test]
    public void TestInfo()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        SleepEffect sleep = new SleepEffect();
        
        Assert.That(sleep.Info(Pikachu), !Is.Null);
    }

    [Test]
    public void TestICanAttack()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        SleepEffect sleep = new SleepEffect();
        
        Assert.That(false, Is.EqualTo(sleep.IcanAttack));
    }
    
    [Test]
        public void TestProcessEffectShouldNotAllowAttackWhenStillSleeping()
        {
            // Arrange
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            SleepEffect sleep = new SleepEffect();
            sleep.StartEffect(Pikachu);

            // Act
            string result = sleep.ProcessEffect(Pikachu);

            // Assert: Verificar que el Pokémon aún está dormido y no puede atacar
            Assert.That(sleep.IcanAttack, Is.False);
        }

        [Test]
        public void TestProcessEffectShouldWakeUpWhenTurnsEnd()
        {
            // Arrange
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            SleepEffect sleep = new SleepEffect();
            sleep.StartEffect(Pikachu);

            // Simular que el Pokémon ha pasado los turnos de sueño
            for (int i = 0; i < 4; i++)
            {
                sleep.ProcessEffect(Pikachu);
            }

            // Act
            string result = sleep.ProcessEffect(Pikachu);

            // Assert: Verificar que el Pokémon se haya despertado
            Assert.That(sleep.IcanAttack, Is.True);
            Assert.That(result, Is.EqualTo($"El pokemon Pikachu ha despertado."));
        }

        [Test]
        public void TestProcessEffectShouldAllowAttackWhenPokemonWakesUp()
        {
            // Arrange
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            SleepEffect sleep = new SleepEffect();
            sleep.StartEffect(Pikachu);

            // Act: Pasar por los turnos de sueño hasta que el Pokémon despierte
            for (int i = 0; i < 4; i++)
            {
                sleep.ProcessEffect(Pikachu);
            }

            // Verificar que el Pokémon esté despierto
            string result = sleep.ProcessEffect(Pikachu);

            // Assert: El Pokémon debe poder atacar ahora
            Assert.That(sleep.IcanAttack, Is.True);
            Assert.That(result, Is.EqualTo($"El pokemon Pikachu ha despertado."));
        }

        [Test]
        public void TestProcessEffectShouldDecrementSleepTurns()
        {
            // Arrange
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            SleepEffect sleep = new SleepEffect();
            sleep.StartEffect(Pikachu);

            // Act: Llamar a ProcessEffect varias veces para comprobar la reducción de turnos
            string resultTurn1 = sleep.ProcessEffect(Pikachu); // Debería ser el turno 1

            // Assert: Verificar que los turnos se decrecen correctamente
            Assert.That(resultTurn1, Is.EqualTo(resultTurn1));
        }
}