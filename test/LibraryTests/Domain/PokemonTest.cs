using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Pokemon))]
public class PokemonTest
{
    // Prueba para verificar que un Pokémon reciba daño correctamente
    [Test]
    public void RecibirDaño_DecreasesHealthCorrectly()
    {
        // Arrange: Crear un Pokémon con salud inicial
        Pokemon charmander = new Pokemon("Charmander", 50, new List<string> { "Llamarada" }, "Fuego");

        // Act: Hacer que reciba 20 puntos de daño
        charmander.recibirDaño(20);

        // Assert: La salud debería ser 30
        Assert.That(charmander.Health, Is.EqualTo(30), "La salud del Pokémon debería haber disminuido correctamente.");
    }

    // Prueba para verificar que un Pokémon no pueda recibir daño una vez derrotado
    [Test]
    public void RecibirDaño_WhenDefeated_NoHealthDecrease()
    {
        // Arrange: Crear un Pokémon con poca salud
        Pokemon squirtle = new Pokemon("Squirtle", 10, new List<string> { "Pistola Agua" }, "Agua");

        // Act: Hacer que reciba suficiente daño para derrotarlo y luego intentar infligir más daño
        squirtle.recibirDaño(20);
        squirtle.recibirDaño(10);

        // Assert: La salud debería ser 0 y no cambiar
        Assert.That(squirtle.Health, Is.EqualTo(0), "La salud de un Pokémon derrotado no debería cambiar.");
        Assert.That(squirtle.IsDefeated, Is.True, "El estado 'IsDefeated' debería ser verdadero.");
    }

    // Prueba para verificar que un ataque válido cause daño al oponente
    [Test]
    public void Attacks_ValidAttack_CausesDamage()
    {
        // Arrange: Crear dos Pokémon y un gestor de efectos
        Pokemon bulbasaur = new Pokemon("Bulbasaur", 60, new List<string> { "Hoja Afilada" }, "Planta");
        Pokemon charmander = new Pokemon("Charmander", 50, new List<string> { "Llamarada" }, "Fuego");
        EffectsManager effectsManager = new EffectsManager();

        // Act: Realizar un ataque
        string result = charmander.attacks(bulbasaur, "Llamarada", effectsManager);

        // Assert: Verificar que se haya reducido la salud del oponente
        Assert.That(bulbasaur.Health, Is.LessThan(61), "La salud del oponente debería haber disminuido.");
        Assert.That(result, Does.Contain("recibió"), "El mensaje del ataque debería indicar que se causó daño.");
    }

    // Prueba para verificar que un ataque inválido no cause daño
    [Test]
    public void Attacks_InvalidAttack_NoDamage()
    {
        // Arrange: Crear dos Pokémon y un gestor de efectos
        Pokemon pikachu = new Pokemon("Pikachu", 40, new List<string> { "Impactrueno" }, "Eléctrico");
        Pokemon geodude = new Pokemon("Geodude", 70, new List<string> { "Placaje" }, "Tierra");
        EffectsManager effectsManager = new EffectsManager();

        // Act: Intentar realizar un ataque no existente
        string result = pikachu.attacks(geodude, "Ataque Rápido", effectsManager);

        // Assert: La salud del oponente no debería cambiar
        Assert.That(geodude.Health, Is.EqualTo(70), "La salud del oponente no debería haber cambiado.");
        Assert.That(result, Is.EqualTo("Este no es tu ataque. "), "El mensaje debería indicar que el ataque no es válido.");
    }

    // Prueba para verificar que un Pokémon se marque como derrotado al llegar a 0 de salud
    [Test]
    public void RecibirDaño_WhenHealthReachesZero_IsDefeated()
    {
        // Arrange: Crear un Pokémon con baja salud
        Pokemon zubat = new Pokemon("Zubat", 5, new List<string> { "Mordisco" }, "Veneno");

        // Act: Infligir suficiente daño para derrotarlo
        zubat.recibirDaño(10);

        // Assert: Verificar que el Pokémon esté derrotado
        Assert.That(zubat.IsDefeated, Is.True, "El Pokémon debería estar marcado como derrotado.");
    }
}
