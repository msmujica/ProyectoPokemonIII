using Library;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;
using NUnit.Framework.Legacy;

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
        ClassicAssert.IsNotNull(battle);
        ClassicAssert.AreEqual("Player1", battle.Player1.Name);
        ClassicAssert.AreEqual("Player2", battle.Player2.Name);
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
        ClassicAssert.IsNotNull(result);
        ClassicAssert.AreEqual(player1, result);
    }
}