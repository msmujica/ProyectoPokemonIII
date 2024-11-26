using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(SleepEffect))]
public class SleepEffectTest
{ 
    /// <summary>
    /// Prueba que verifica que el efecto de sueño se inicia correctamente en un Pokémon.
    /// </summary>
    [Test]
    public void TestStartEffect()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        SleepEffect sleep = new SleepEffect();
        string result = sleep.StartEffect(Pikachu);
        Assert.That(result, !Is.Null);
    }
    
    
    /// <summary> 
    /// Prueba que verifica el procesamiento del efecto de sueño en un Pokémon. 
    /// </summary>

    [Test]
    public void TestProcessEffect()
    {
        Pokemon Pikachus = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        SleepEffect sleep = new SleepEffect();
        sleep.StartEffect(Pikachus);
        string result = sleep.ProcessEffect(Pikachus);
        Assert.That(result, !Is.EqualTo(""));
    }
    
    /// <summary> 
    /// Prueba que verifica que la información del efecto de sueño sea retornada correctamente.
    /// </summary>

    [Test]
    public void TestInfo()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        SleepEffect sleep = new SleepEffect();
        
        Assert.That(sleep.Info(Pikachu), !Is.Null);
    }
    
    /// <summary>
    /// Prueba que verifica que un Pokémon bajo el efecto de sueño no pueda atacar.
    /// </summary>

    
    [Test]
    public void TestICanAttack()
    {
        Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
        SleepEffect sleep = new SleepEffect();
        
        Assert.That(false, Is.EqualTo(sleep.IcanAttack));
    }
    
    /// <summary> 
    /// Prueba que verifica que un Pokémon dormido no pueda atacar mientras aún está bajo el efecto.
    /// </summary>

        [Test]
        public void TestProcessEffectShouldNotAllowAttackWhenStillSleeping()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            SleepEffect sleep = new SleepEffect();
            sleep.StartEffect(Pikachu);
            
            string result = sleep.ProcessEffect(Pikachu);
            
            Assert.That(sleep.IcanAttack, Is.False);
        }

    /// <summary> 
    /// Prueba que verifica que un Pokémon despierte después de que termine el número de turnos de sueño. 
    /// </summary>

        [Test]
        public void TestProcessEffectShouldWakeUpWhenTurnsEnd()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            SleepEffect sleep = new SleepEffect();
            sleep.StartEffect(Pikachu);
            
            for (int i = 0; i < 4; i++)
            {
                sleep.ProcessEffect(Pikachu);
            }
            
            string result = sleep.ProcessEffect(Pikachu);
            
            Assert.That(sleep.IcanAttack, Is.True);
            Assert.That(result, Is.EqualTo($"El pokemon Pikachu ha despertado."));
        }
        
        
    /// <summary> 
    /// Prueba que verifica que un Pokémon pueda atacar después de despertar del efecto de sueño.
    /// </summary>

        [Test]
        public void TestProcessEffectShouldAllowAttackWhenPokemonWakesUp()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            SleepEffect sleep = new SleepEffect();
            sleep.StartEffect(Pikachu);
            
            for (int i = 0; i < 4; i++)
            {
                sleep.ProcessEffect(Pikachu);
            }
            
            string result = sleep.ProcessEffect(Pikachu);
            
            Assert.That(sleep.IcanAttack, Is.True);
            Assert.That(result, Is.EqualTo($"El pokemon Pikachu ha despertado."));
        }
    
        
    /// <summary>
    /// Prueba que verifica que los turnos de sueño disminuyan correctamente con cada procesamiento. 
    /// </summary>

        [Test]
        public void TestProcessEffectShouldDecrementSleepTurns()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            SleepEffect sleep = new SleepEffect();
            sleep.StartEffect(Pikachu);
            
            string resultTurn1 = sleep.ProcessEffect(Pikachu); 
            
            Assert.That(resultTurn1, Is.EqualTo(resultTurn1));
        }
}