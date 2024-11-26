using Library;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests.Domain
{
    /// <summary>
    /// Clase de pruebas unitarias para validar el comportamiento de la clase <see cref="BattlesList"/>.
    /// </summary>
    [TestFixture]
    [TestOf(typeof(BattlesList))]
    public class BattlesListTest
    {
        private BattlesList battlesList;
        private Trainer player1;
        private Trainer player2;

        /// <summary>
        /// Verifica que el método <see cref="BattlesList.AddBattle(Trainer, Trainer)"/> agregue correctamente 
        /// una nueva batalla a la lista de batallas.
        /// </summary>
        [Test]
        public void AgregarLista()
        {
            // Arrange
            player1 = new Trainer("Player1");
            player2 = new Trainer("Player2");
            battlesList = new BattlesList();

            // Act
            Battle battle = battlesList.AddBattle(player1, player2);

            // Assert
            Assert.That(battle, Is.Not.Null);
            Assert.That("Player1", Is.EqualTo(battle.Player1.Name));
            Assert.That("Player2", Is.EqualTo(battle.Player2.Name));
        }

        /// <summary>
        /// Verifica que el método <see cref="BattlesList.FindTrainerByDisplayName(string)"/> 
        /// busque correctamente a un entrenador por su nombre de usuario (display name).
        /// </summary>
        [Test]
        public void BuscarEntrenadorPorDisplayName()
        {
            player1 = new Trainer("Player1");
            player2 = new Trainer("Player2");
            battlesList = new BattlesList();
            battlesList.AddBattle(player1, player2);
            
            var result = battlesList.FindTrainerByDisplayName("Player1");
            
            Assert.That(result, Is.Not.Null);
            Assert.That(player1, Is.EqualTo(result));
        }
    }
}