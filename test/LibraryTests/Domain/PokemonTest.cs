using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Pokemon))]
public class PokemonTest
{
    /// <summary>
    /// Prueba para verificar que un Pokémon reciba daño correctamente
    /// </summary>
    [Test]
    public void ReceiveDamageDecreasesHealthCorrectly()
    {
        
        Pokemon charmander = new Pokemon("Charmander", 50, new List<string> { "Llamarada" }, "Fuego");

        
        charmander.recibirDaño(20);

        
        Assert.That(charmander.Health, Is.EqualTo(30), "La salud del Pokémon debería haber disminuido correctamente.");
    }
    
    /// <summary>
    /// Prueba para verificar que un Pokémon no pueda recibir daño una vez derrotado
    /// </summary>
    [Test]
    public void ReceiveDamageWhenDefeated()
    {
        
        Pokemon squirtle = new Pokemon("Squirtle", 10, new List<string> { "Pistola Agua" }, "Agua");
        
        squirtle.recibirDaño(20);
        squirtle.recibirDaño(10);
        
        Assert.That(squirtle.Health, Is.EqualTo(0), "La salud de un Pokémon derrotado no debería cambiar.");
        Assert.That(squirtle.IsDefeated, Is.True, "El estado 'IsDefeated' debería ser verdadero.");
    }
    
    /// <summary>
    /// Prueba para verificar que un ataque válido cause daño al oponente
    /// </summary>
    [Test]
    public void AttacksValidAttackCausesDamage()
    {
        Pokemon bulbasaur = new Pokemon("Bulbasaur", 60, new List<string> { "Hoja Afilada" }, "Planta");
        Pokemon charmander = new Pokemon("Charmander", 50, new List<string> { "Llamarada" }, "Fuego");
        EffectsManager effectsManager = new EffectsManager();
        
        string result = charmander.attacks(bulbasaur, "Llamarada", effectsManager);
        
        Assert.That(bulbasaur.Health, Is.LessThan(61), "La salud del oponente debería haber disminuido.");
        Assert.That(result, Does.Contain("recibió"), "El mensaje del ataque debería indicar que se causó daño.");
    }

    /// <summary>
    /// Prueba para verificar que un ataque inválido no cause daño
    /// </summary>
    [Test]
    public void AttacksInvalidAttackNoDamage()
    {
        Pokemon pikachu = new Pokemon("Pikachu", 40, new List<string> { "Impactrueno" }, "Eléctrico");
        Pokemon geodude = new Pokemon("Geodude", 70, new List<string> { "Placaje" }, "Tierra");
        EffectsManager effectsManager = new EffectsManager();
        
        string result = pikachu.attacks(geodude, "Ataque Rápido", effectsManager);
        
        Assert.That(geodude.Health, Is.EqualTo(70), "La salud del oponente no debería haber cambiado.");
        Assert.That(result, Is.EqualTo("Este no es tu ataque"), "El mensaje debería indicar que el ataque no es válido.");
    }

    /// <summary>
    /// Prueba para verificar que un Pokémon se marque como derrotado al llegar a 0 de salud
    /// </summary>
    [Test]
    public void ReceiveDamageIsDefeated()
    {
        
        Pokemon zubat = new Pokemon("Zubat", 5, new List<string> { "Mordisco" }, "Veneno");
        
        zubat.recibirDaño(10);
        
        Assert.That(zubat.IsDefeated, Is.True, "El Pokémon debería estar marcado como derrotado.");
    }
}
