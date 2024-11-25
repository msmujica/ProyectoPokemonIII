using Library;
using Library.Items;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(ItemsManager))]
public class ItemsManagerTest
{
     [Test]
        public void TestUseSuperPotion()
        {
            // Arrange
            var pokemon = new Pokemon("Pikachu", 50, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 3;

            // Act
            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            // Assert
            Assert.That("Usaste una Super Pocion. Usos restantes: 2", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida debe ser 100 después de usar la poción
        }
        
        [Test]
        public void TestUseSuperPotionNone()
        {
            // Arrange
            var pokemon = new Pokemon("Pikachu", 50, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 0;

            // Act
            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            // Assert
            Assert.That("No tienes Super Pociones disponibles.", Is.EqualTo(result));
           }

        [Test]
        public void TestUseSuperPotion_MaxHealth()
        {
            // Arrange
            var pokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 3;

            // Act
            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            // Assert
            Assert.That("El Pokémon ya está a máxima vida.", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida no debe cambiar si ya está a máximo
        }


        [Test]
        public void TestUseRevive_IsNotDefeated()
        {
            // Arrange
            var pokemon = new Pokemon("Bulbasaur", 50, new List<string> { "Hoja Afilada", "Látigo Cepa", "Rayo Solar" }, "Planta");
            var manager = new ItemsManager();
            int reviveConunter = 1;

            // Act
            var result = manager.UsarRevivir(pokemon, reviveConunter);

            // Assert
            Assert.That("El Pokémon no está derrotado.", Is.EqualTo(result));
        }
        
        [Test]
        public void TestUseReviveNone()
        {
            // Arrange
            var pokemon = new Pokemon("Bulbasaur", 50, new List<string> { "Hoja Afilada", "Látigo Cepa", "Rayo Solar" }, "Planta");
            var manager = new ItemsManager();
            int reviveConunter = 0;

            // Act
            var result = manager.UsarRevivir(pokemon, reviveConunter);

            // Assert
            Assert.That("No tienes Revivir disponible.", Is.EqualTo(result));
        }

        [Test]
        public void TestUseTotalCure()
        {
            // Arrange
            var pokemon = new Pokemon("Charmander", 30, new List<string>{"Llamarada", "Lanzallamas", "Ascuas"}, "Fuego");
            var manager = new ItemsManager();
            int totalCureCounter = 1;
            var effectsManager = new EffectsManager();

            // Act
            var result = manager.UsarCuraTotal(pokemon, totalCureCounter, effectsManager);

            // Assert
            Assert.That("Usaste una Cura Total. Usos restantes: 0", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida debe ser restaurada al máximo
        }
        
        [Test]
        public void TestUseTotalCureNone()
        {
            // Arrange
            var pokemon = new Pokemon("Charmander", 30, new List<string>{"Llamarada", "Lanzallamas", "Ascuas"}, "Fuego");
            var manager = new ItemsManager();
            int totalCureCounter = 0;
            var effectsManager = new EffectsManager();

            // Act
            var result = manager.UsarCuraTotal(pokemon, totalCureCounter, effectsManager);

            // Assert
            Assert.That("No tienes Curaciones Totales disponibles.", Is.EqualTo(result));
            }
}
