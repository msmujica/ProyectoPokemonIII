using Library;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(BattlesList))]
public class BattlesListTest
{
    private BattlesList battlesList;
    private Trainer player1;
    private Trainer player2;
    
    [Test]
    public void AgregarLista()
    {
        
        player1 = new Trainer("Player1");
        player2 = new Trainer("Player2");
        battlesList = new BattlesList();
        Battle battle = battlesList.AddBattle(player1, player2);

        // Assert
        Assert.That(battle, Is.Not.Null);
        Assert.That("Player1", Is.EqualTo(battle.Player1.Name));
        Assert.That("Player2", Is.EqualTo(battle.Player2.Name));
    }
    
    [Test]
    public void BuscarEntrenadorPorDisplayName()
    {
        player1 = new Trainer("Player1");
        player2 = new Trainer("Player2");
        battlesList = new BattlesList();
        Battle battle = battlesList.AddBattle(player1, player2);

        // Act
        var result = battlesList.FindTrainerByDisplayName("Player1");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(player1, Is.EqualTo(result));
    }
}