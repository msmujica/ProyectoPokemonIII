using Library;
using Library.Items;
using NUnit.Framework;

namespace LibraryTests.Domain
{
    // TestFixture que agrupa las pruebas relacionadas con el manejo de objetos de tipo ItemsManager
    [TestFixture]
    [TestOf(typeof(ItemsManager))]
    public class ItemsManagerTest
    {
        // Prueba para el uso de una Super Poción cuando el Pokémon tiene menos de su vida máxima
        [Test]
        public void TestUseSuperPotion()
        {
            // Arrange: Preparar los datos de entrada
            var pokemon = new Pokemon("Pikachu", 50, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 3;

            // Act: Ejecutar la acción de usar la Super Poción
            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            // Assert: Verificar el resultado esperado
            Assert.That("Usaste una Super Pocion. Usos restantes: 2", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida debe ser 100 después de usar la poción
        }
        
        // Prueba cuando no hay Super Pociones disponibles
        [Test]
        public void TestUseSuperPotionNone()
        {
            // Arrange: Preparar los datos de entrada
            var pokemon = new Pokemon("Pikachu", 50, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 0;

            // Act: Ejecutar la acción de usar la Super Poción
            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            // Assert: Verificar que no hay Super Pociones disponibles
            Assert.That("No tienes Super Pociones disponibles.", Is.EqualTo(result));
        }

        // Prueba para usar la Super Poción cuando el Pokémon ya tiene vida máxima
        [Test]
        public void TestUseSuperPotion_MaxHealth()
        {
            // Arrange: Preparar los datos de entrada
            var pokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 3;

            // Act: Ejecutar la acción de usar la Super Poción
            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            // Assert: Verificar que no se cambia la vida del Pokémon si ya está a máxima vida
            Assert.That("El Pokémon ya está a máxima vida.", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida no debe cambiar si ya está a máximo
        }

        // Prueba cuando se intenta usar un Revivir en un Pokémon que no está derrotado
        [Test]
        public void TestUseRevive_IsNotDefeated()
        {
            // Arrange: Preparar los datos de entrada
            var pokemon = new Pokemon("Bulbasaur", 50, new List<string> { "Hoja Afilada", "Látigo Cepa", "Rayo Solar" }, "Planta");
            var manager = new ItemsManager();
            int reviveConunter = 1;

            // Act: Ejecutar la acción de usar Revivir
            var result = manager.UsarRevivir(pokemon, reviveConunter);

            // Assert: Verificar que el Pokémon no está derrotado
            Assert.That("El Pokémon no está derrotado.", Is.EqualTo(result));
        }
        
        // Prueba cuando no hay Revivir disponible
        [Test]
        public void TestUseReviveNone()
        {
            // Arrange: Preparar los datos de entrada
            var pokemon = new Pokemon("Bulbasaur", 50, new List<string> { "Hoja Afilada", "Látigo Cepa", "Rayo Solar" }, "Planta");
            var manager = new ItemsManager();
            int reviveConunter = 0;

            // Act: Ejecutar la acción de usar Revivir
            var result = manager.UsarRevivir(pokemon, reviveConunter);

            // Assert: Verificar que no hay Revivir disponible
            Assert.That("No tienes Revivir disponible.", Is.EqualTo(result));
        }

        // Prueba para el uso de una Cura Total cuando el Pokémon tiene vida baja
        [Test]
        public void TestUseTotalCure()
        {
            // Arrange: Preparar los datos de entrada
            var pokemon = new Pokemon("Charmander", 30, new List<string>{"Llamarada", "Lanzallamas", "Ascuas"}, "Fuego");
            var manager = new ItemsManager();
            int totalCureCounter = 1;
            var effectsManager = new EffectsManager();

            // Act: Ejecutar la acción de usar la Cura Total
            var result = manager.UsarCuraTotal(pokemon, totalCureCounter, effectsManager);

            // Assert: Verificar que la vida del Pokémon se restaura al máximo
            Assert.That("Usaste una Cura Total. Usos restantes: 0", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida debe ser restaurada al máximo
        }
        
        // Prueba cuando no hay Curaciones Totales disponibles
        [Test]
        public void TestUseTotalCureNone()
        {
            // Arrange: Preparar los datos de entrada
            var pokemon = new Pokemon("Charmander", 30, new List<string>{"Llamarada", "Lanzallamas", "Ascuas"}, "Fuego");
            var manager = new ItemsManager();
            int totalCureCounter = 0;
            var effectsManager = new EffectsManager();

            // Act: Ejecutar la acción de usar la Cura Total
            var result = manager.UsarCuraTotal(pokemon, totalCureCounter, effectsManager);

            // Assert: Verificar que no hay Curaciones Totales disponibles
            Assert.That("No tienes Curaciones Totales disponibles.", Is.EqualTo(result));
        }
    }
}
