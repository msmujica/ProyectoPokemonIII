using System;
using Ucu.Poo.DiscordBot.Domain;
using NUnit.Framework;

namespace LibraryTests.Domain
{
    [TestFixture]
    public class WaitingListTests
    {
        private WaitingList _waitingList;

        [SetUp]
        public void SetUp()
        {
            _waitingList = new WaitingList(); // Inicializamos una nueva lista de espera antes de cada prueba
        }

        [Test]
        public void AddTrainer_ShouldAddTrainer_WhenValidNameIsProvided()
        {
            // Arrange
            string trainerName = "Ash";

            // Act
            bool result = _waitingList.AddTrainer(trainerName);

            // Assert
            Assert.That(result, Is.True, "El entrenador debería ser agregado correctamente.");
            Assert.That(_waitingList.Count, Is.EqualTo(1), "El conteo de entrenadores debería ser 1.");
            Assert.That(_waitingList.FindTrainerByDisplayName(trainerName), Is.Not.Null, "El entrenador debería ser encontrado.");
        }

        [Test]
        public void AddTrainer_ShouldNotAddTrainer_WhenNameIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _waitingList.AddTrainer(null), "Debería lanzar una excepción si el nombre es nulo.");
        }

        [Test]
        public void AddTrainer_ShouldNotAddTrainer_WhenNameIsEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _waitingList.AddTrainer(""), "Debería lanzar una excepción si el nombre está vacío.");
        }

        [Test]
        public void AddTrainer_ShouldNotAddTrainer_WhenTrainerAlreadyExists()
        {
            // Arrange
            string trainerName = "Ash";
            _waitingList.AddTrainer(trainerName); // Añadimos un entrenador

            // Act
            bool result = _waitingList.AddTrainer(trainerName); // Intentamos añadirlo de nuevo

            // Assert
            Assert.That(result, Is.False, "El entrenador no debería ser agregado si ya existe en la lista.");
            Assert.That(_waitingList.Count, Is.EqualTo(1), "El número de entrenadores debería seguir siendo 1.");
        }

        [Test]
        public void RemoveTrainer_ShouldRemoveTrainer_WhenTrainerExists()
        {
            // Arrange
            string trainerName = "Ash";
            _waitingList.AddTrainer(trainerName); // Añadimos un entrenador

            // Act
            bool result = _waitingList.RemoveTrainer(trainerName);

            // Assert
            Assert.That(result, Is.True, "El entrenador debería ser removido correctamente.");
            Assert.That(_waitingList.Count, Is.EqualTo(0), "El número de entrenadores debería ser 0 después de removerlo.");
        }

        [Test]
        public void RemoveTrainer_ShouldNotRemoveTrainer_WhenTrainerDoesNotExist()
        {
            // Act
            bool result = _waitingList.RemoveTrainer("Ash");

            // Assert
            Assert.That(result, Is.False, "No se debería poder remover un entrenador que no existe.");
        }

        [Test]
        public void GetAnyoneWaiting_ShouldReturnTrainer_WhenTrainerIsInList()
        {
            // Arrange
            string trainerName = "Ash";
            _waitingList.AddTrainer(trainerName);

            // Act
            var result = _waitingList.GetAnyoneWaiting();

            // Assert
            Assert.That(result, Is.Not.Null, "Debería retornar un entrenador.");
            Assert.That(result?.Name, Is.EqualTo(trainerName), "El nombre del entrenador debería coincidir.");
        }

        [Test]
        public void GetAnyoneWaiting_ShouldReturnNull_WhenListIsEmpty()
        {
            // Act
            var result = _waitingList.GetAnyoneWaiting();

            // Assert
            Assert.That(result, Is.Null, "Debería retornar null si no hay entrenadores en la lista.");
        }

        [Test]
        public void FindTrainerByDisplayName_ShouldReturnTrainer_WhenTrainerExists()
        {
            // Arrange
            string trainerName = "Ash";
            _waitingList.AddTrainer(trainerName);

            // Act
            var result = _waitingList.FindTrainerByDisplayName(trainerName);

            // Assert
            Assert.That(result, Is.Not.Null, "El entrenador debería ser encontrado.");
            Assert.That(result?.Name, Is.EqualTo(trainerName), "El nombre del entrenador debería coincidir.");
        }

        [Test]
        public void FindTrainerByDisplayName_ShouldReturnNull_WhenTrainerDoesNotExist()
        {
            // Act
            var result = _waitingList.FindTrainerByDisplayName("Ash");

            // Assert
            Assert.That(result, Is.Null, "No debería encontrar un entrenador que no existe.");
        }
    }
}
