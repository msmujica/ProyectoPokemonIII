using Library;
using Library.Items;
using NUnit.Framework;

namespace LibraryTests.Domain
{
    [TestFixture]
    [TestOf(typeof(ItemsManager))]
    public class ItemsManagerTest
    {
        /// <summary>
        /// Verifica el uso de una Super poción cuando el Pokémon tiene menos de su vida máxima.
        /// </summary>
        /// <returns>
        /// Un mensaje indicando que se usó una Super poción y el número de usos restantes.
        /// Además, la vida del Pokémon se restaura al máximo
        /// </returns>
        [Test]
        public void TestUseSuperPotion()
        {
            var pokemon = new Pokemon("Pikachu", 50, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 3;

            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            Assert.That("Usaste una Super Pocion. Usos restantes: 2", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida debe ser 100 después de usar la poción
        }
        
        /// <summary>
        /// Verifica el comportamiento al intentar usar una Super Poción cuando no hay disponibles.
        /// </summary>
        /// <returns>Un mensaje indicando que no hay Super Pociones disponibles.</returns>
        [Test]
        public void TestUseSuperPotionNone()
        {
            var pokemon = new Pokemon("Pikachu", 50, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 0;

            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            Assert.That("No tienes Super Pociones disponibles.", Is.EqualTo(result));
        }

        /// <summary>
        /// Verifica que no se pueda usar una Super Poción si el Pokémon ya tiene su vida al máximo.
        /// </summary>
        /// <returns>Un mensaje indicando que el Pokémon ya está a su vida máxima.</returns>
        [Test]
        public void TestUseSuperPotion_MaxHealth()
        {
            var pokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            var manager = new ItemsManager();
            int superPotionCounter = 3;

            var result = manager.UsarSuperPocion(pokemon, superPotionCounter);

            Assert.That("El Pokémon ya está a máxima vida.", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida no debe cambiar si ya está a máximo
        }

        /// <summary>
        /// Verifica que no se pueda usar un Revivir en un Pokémon que no está derrotado.
        /// </summary>
        /// <returns>Un mensaje indicando que el Pokémon no está derrotado.</returns>
        [Test]
        public void TestUseRevive_IsNotDefeated()
        {
            var pokemon = new Pokemon("Bulbasaur", 50, new List<string> { "Hoja Afilada", "Látigo Cepa", "Rayo Solar" }, "Planta");
            var manager = new ItemsManager();
            int reviveConunter = 1;

            var result = manager.UsarRevivir(pokemon, reviveConunter);

            Assert.That("El Pokémon no está derrotado.", Is.EqualTo(result));
        }
        
        /// <summary>
        /// Verifica el comportamiento al intentar usar un Revivir cuando no hay disponibles.
        /// </summary>
        /// <returns>Un mensaje indicando que no hay Revivires disponibles.</returns>
        [Test]
        public void TestUseReviveNone()
        {
            var pokemon = new Pokemon("Bulbasaur", 50, new List<string> { "Hoja Afilada", "Látigo Cepa", "Rayo Solar" }, "Planta");
            var manager = new ItemsManager();
            int reviveConunter = 0;

            var result = manager.UsarRevivir(pokemon, reviveConunter);

            Assert.That("No tienes Revivir disponible.", Is.EqualTo(result));
        }

        /// <summary>
        /// Verifica el uso de una Cura Total para restaurar la vida del Pokémon a su máximo y eliminar efectos negativos.
        /// </summary>
        /// <returns>
        /// Un mensaje indicando que se usó una Cura Total y el número de usos restantes.
        /// Además, la vida del Pokémon se restaura a su valor máximo.
        /// </returns>
        [Test]
        public void TestUseTotalCure()
        {
            var pokemon = new Pokemon("Charmander", 30, new List<string>{"Llamarada", "Lanzallamas", "Ascuas"}, "Fuego");
            var manager = new ItemsManager();
            int totalCureCounter = 1;
            var effectsManager = new EffectsManager();

            var result = manager.UsarCuraTotal(pokemon, totalCureCounter, effectsManager);

            Assert.That("Usaste una Cura Total. Usos restantes: 0", Is.EqualTo(result));
            Assert.That(100, Is.EqualTo(pokemon.Health)); // La vida debe ser restaurada al máximo
        }
        
        /// <summary>
        /// Verifica el comportamiento al intentar usar una Cura Total cuando no hay disponibles.
        /// </summary>
        /// <returns>Un mensaje indicando que no hay Curaciones Totales disponibles.</returns>
        [Test]
        public void TestUseTotalCureNone()
        {
            var pokemon = new Pokemon("Charmander", 30, new List<string>{"Llamarada", "Lanzallamas", "Ascuas"}, "Fuego");
            var manager = new ItemsManager();
            int totalCureCounter = 0;
            var effectsManager = new EffectsManager();

            var result = manager.UsarCuraTotal(pokemon, totalCureCounter, effectsManager);

            Assert.That("No tienes Curaciones Totales disponibles.", Is.EqualTo(result));
        }
    }
}
