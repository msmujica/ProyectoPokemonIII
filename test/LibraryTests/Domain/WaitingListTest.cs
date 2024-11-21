using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

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
        var waitingList = new WaitingList();
        bool result = waitingList.AddTrainer("Player1");

        // Assert
        Assert.That(result, Is.True); // Debería haberse agregado correctamente.
        Assert.That(1, Is.EqualTo(waitingList.Count)); // La lista debería tener un solo entrenador.
    }

    [Test]
    public void RemoveExistingTrainer()
    {
        var waitingList = new WaitingList();
        waitingList.AddTrainer("Player1");

        // Act
        bool result = waitingList.RemoveTrainer("Player1");

        // Assert
        Assert.That(result, Is.True); // El entrenador debería haberse eliminado correctamente.
        Assert.That(0, Is.EqualTo(waitingList.Count)); // La lista debería estar vacía.
    }


    [Test]
    public void SearchTrainerByDisplayName()
    {
        // Arrange
        var waitingList = new WaitingList();
        waitingList.AddTrainer("Player1");

        // Act
        var trainer = waitingList.FindTrainerByDisplayName("Player1");

        // Assert
        Assert.That(trainer, Is.Not.Null); // El entrenador debería ser encontrado.
        Assert.That("Player1", Is.EqualTo(trainer?.Name)); // El nombre del entrenador debería coincidir.
    }
}