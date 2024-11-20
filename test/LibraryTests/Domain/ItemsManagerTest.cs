using Library;
using Library.Items;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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
            ClassicAssert.AreEqual("Usaste una Super Pocion. Usos restantes: 2", result);
            ClassicAssert.AreEqual(100, pokemon.Health); // La vida debe ser 100 después de usar la poción
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
            ClassicAssert.AreEqual("El Pokémon ya está a máxima vida.", result);
            ClassicAssert.AreEqual(100, pokemon.Health); // La vida no debe cambiar si ya está a máximo
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
            ClassicAssert.AreEqual("El Pokémon no está derrotado.", result);
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
            ClassicAssert.AreEqual("Usaste una Cura Total. Usos restantes: 0", result);
            ClassicAssert.AreEqual(100, pokemon.Health); // La vida debe ser restaurada al máximo
        }
}