using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;
using NUnit.Framework.Legacy;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(WaitingList))]
public class WaitingListTest
{
    private WaitingList waitingList;

    [Test]
    public void Setup()
    {
        waitingList = new WaitingList();
    }

    [Test]
    public void AddTrainer()
    {
        // Act
        bool result = waitingList.AddTrainer("Player1");

        // Assert
        ClassicAssert.IsTrue(result); // Debería haberse agregado correctamente.
        ClassicAssert.AreEqual(1, waitingList.Count); // La lista debería tener un solo entrenador.
    }

    [Test]
    public void RemoveExistingTrainer()
    {
        // Arrange
        waitingList.AddTrainer("Player1");

        // Act
        bool result = waitingList.RemoveTrainer("Player1");

        // Assert
        ClassicAssert.IsTrue(result); // El entrenador debería haberse eliminado correctamente.
        ClassicAssert.AreEqual(0, waitingList.Count); // La lista debería estar vacía.
    }


    [Test]
    public void SearchTrainerByDisplayName()
    {
        // Arrange
        waitingList.AddTrainer("Player1");

        // Act
        var trainer = waitingList.FindTrainerByDisplayName("Player1");

        // Assert
        ClassicAssert.IsNotNull(trainer); // El entrenador debería ser encontrado.
        ClassicAssert.AreEqual("Player1", trainer?.Name); // El nombre del entrenador debería coincidir.
    }
}