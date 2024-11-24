using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests.Domain
{
    [TestFixture]
    [TestOf(typeof(WaitingList))]
    public class WaitingListTest
    {
        private WaitingList waitingList;

        [SetUp]
        public void Setup()
        {
            waitingList = new WaitingList();
        }

        [Test]
        public void AddTrainer_ShouldAddTrainer_WhenDisplayNameIsValid()
        {
            // Act
            bool result = waitingList.AddTrainer("Player1");

            // Assert
            Assert.That(result, Is.True); // Debería haberse agregado correctamente.
            Assert.That(waitingList.Count, Is.EqualTo(1)); // La lista debería tener un solo entrenador.
        }

        [Test]
        public void AddTrainer_ShouldReturnFalse_WhenDisplayNameIsDuplicate()
        {
            // Arrange
            waitingList.AddTrainer("Player1");

            // Act
            bool result = waitingList.AddTrainer("Player1");

            // Assert
            Assert.That(result, Is.False); // Debería retornar false al ser un nombre duplicado.
            Assert.That(waitingList.Count, Is.EqualTo(1)); // La lista debería seguir teniendo un solo entrenador.
        }

        [Test]
        public void AddTrainer_ShouldThrowArgumentException_WhenDisplayNameIsNullOrEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => waitingList.AddTrainer(null)); // Debería lanzar una excepción si es null.
            Assert.Throws<ArgumentException>(() => waitingList.AddTrainer(string.Empty)); // Debería lanzar una excepción si es vacío.
        }

        [Test]
        public void RemoveExistingTrainer_ShouldRemoveTrainer_WhenTrainerExists()
        {
            // Arrange
            waitingList.AddTrainer("Player1");

            // Act
            bool result = waitingList.RemoveTrainer("Player1");

            // Assert
            Assert.That(result, Is.True); // El entrenador debería haberse eliminado correctamente.
            Assert.That(waitingList.Count, Is.EqualTo(0)); // La lista debería estar vacía.
        }

        [Test]
        public void RemoveTrainer_ShouldReturnFalse_WhenTrainerDoesNotExist()
        {
            // Act
            bool result = waitingList.RemoveTrainer("Player1");

            // Assert
            Assert.That(result, Is.False); // Debería retornar false si el jugador no existe.
            Assert.That(waitingList.Count, Is.EqualTo(0)); // La lista sigue vacía.
        }

        [Test]
        public void SearchTrainerByDisplayName_ShouldReturnTrainer_WhenTrainerExists()
        {
            // Arrange
            waitingList.AddTrainer("Player1");

            // Act
            var trainer = waitingList.FindTrainerByDisplayName("Player1");

            // Assert
            Assert.That(trainer, Is.Not.Null); // El entrenador debería ser encontrado.
            Assert.That(trainer?.Name, Is.EqualTo("Player1")); // El nombre del entrenador debería coincidir.
        }

        [Test]
        public void SearchTrainerByDisplayName_ShouldReturnNull_WhenTrainerDoesNotExist()
        {
            // Act
            var trainer = waitingList.FindTrainerByDisplayName("Player1");

            // Assert
            Assert.That(trainer, Is.Null); // No debería encontrar al entrenador, ya que no está en la lista.
        }

        [Test]
        public void GetAnyoneWaiting_ShouldReturnFirstTrainer_WhenTrainersExist()
        {
            // Arrange
            waitingList.AddTrainer("Player1");
            waitingList.AddTrainer("Player2");

            // Act
            var trainer = waitingList.GetAnyoneWaiting();

            // Assert
            Assert.That(trainer, Is.Not.Null); // Debería retornar un entrenador.
            Assert.That(trainer?.Name, Is.EqualTo("Player1")); // El primer entrenador debería ser "Player1".
        }

        [Test]
        public void GetAnyoneWaiting_ShouldReturnNull_WhenNoTrainersExist()
        {
            // Act
            var trainer = waitingList.GetAnyoneWaiting();

            // Assert
            Assert.That(trainer, Is.Null); // Debería retornar null si no hay entrenadores en la lista.
        }

        [Test]
        public void GetAllWaiting_ShouldReturnAllTrainers()
        {
            // Arrange
            waitingList.AddTrainer("Player1");
            waitingList.AddTrainer("Player2");

            // Act
            var allTrainers = waitingList.GetAllWaiting();

            // Assert
            Assert.That(allTrainers.Count, Is.EqualTo(2)); // Debería haber dos entrenadores en la lista.
            Assert.That(allTrainers[0].Name, Is.EqualTo("Player1")); // El primer entrenador debería ser "Player1".
            Assert.That(allTrainers[1].Name, Is.EqualTo("Player2")); // El segundo entrenador debería ser "Player2".
        }
    }
}
