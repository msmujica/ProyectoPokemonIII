using Library;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Battle))]
public class BattleTest
{
    [Test]
    public void ValidacionPokemon_DeberiaRetornarTrue_CuandoUnJugadorTieneMenosDeSeisPokemon()
    {
        // Configuración del escenario
        var trainer1 = new Trainer("Player 1");
        var trainer2 = new Trainer("Player 2");

        // Solo agregamos menos de 6 Pokémon a cada equipo
        trainer1.ChooseTeam(1);
        trainer1.ChooseTeam(2);
        trainer1.ChooseTeam(3);
        trainer1.ChooseTeam(4);
        trainer1.ChooseTeam(5);
        trainer1.ChooseTeam(6);

        trainer2.ChooseTeam(1);
        trainer2.ChooseTeam(2);
        trainer2.ChooseTeam(3);
        trainer2.ChooseTeam(4);
        trainer2.ChooseTeam(5);

        var battle = new Battle(trainer1, trainer2);

        // Actuar
        var result = battle.validacionPokemon();

        // Verificar
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidacionWin_DeberiaRetornarTrue_CuandoTodosLosPokemonDelOponenteEstanMuertos()
    {
        // Configuración del escenario
        var trainer1 = new Trainer("Player 1");
        var trainer2 = new Trainer("Player 2");

        // Solo agregamos 6 Pokémon a cada equipo
        trainer1.ChooseTeam(1);
        trainer1.ChooseTeam(2);
        trainer1.ChooseTeam(3);
        trainer1.ChooseTeam(4);
        trainer1.ChooseTeam(5);
        trainer1.ChooseTeam(6);

        trainer2.ChooseTeam(1);
        trainer2.ChooseTeam(2);
        trainer2.ChooseTeam(3);
        trainer2.ChooseTeam(4);
        trainer2.ChooseTeam(5);
        trainer2.ChooseTeam(6);


        foreach (var pokemon in trainer2.Team)
        {
            pokemon.Health = 0; // Todos los Pokémon de Player2 tienen vida 0, provocando su derrota.
        }

        var battle = new Battle(trainer1, trainer2);

        // Actuar
        var resultado = battle.ValidacionWin();

        // Verificar
        Assert.That(resultado, Is.True);
    }

    [Test]
    public void CambiarTurno_DeberiaAlternarEntreJugadores()
    {
        // Configuración del escenario
        var trainer1 = new Trainer("Player 1");
        var trainer2 = new Trainer("Player 2");

        // Solo agregamos 6 Pokémon a cada equipo
        trainer1.ChooseTeam(1);
        trainer1.ChooseTeam(2);
        trainer1.ChooseTeam(3);
        trainer1.ChooseTeam(4);
        trainer1.ChooseTeam(5);
        trainer1.ChooseTeam(6);

        trainer2.ChooseTeam(1);
        trainer2.ChooseTeam(2);
        trainer2.ChooseTeam(3);
        trainer2.ChooseTeam(4);
        trainer2.ChooseTeam(5);
        trainer2.ChooseTeam(6);


        var battle = new Battle(trainer1, trainer2);

        // Actuar y verificar
        if (battle.ActualTurn == trainer1)
        {
            Assert.That(battle.ActualTurn, Is.EqualTo(trainer1));

            battle.CambiarTurno();

            Assert.That(battle.ActualTurn, Is.EqualTo(trainer2));

            battle.CambiarTurno();

            Assert.That(battle.ActualTurn, Is.EqualTo(trainer1));
        }
        else
        {
            Assert.That(battle.ActualTurn, Is.EqualTo(trainer2));

            battle.CambiarTurno();

            Assert.That(battle.ActualTurn, Is.EqualTo(trainer1));

            battle.CambiarTurno();

            Assert.That(battle.ActualTurn, Is.EqualTo(trainer2));
        }

        // Actuar: se realiza el ataque
        var resultado = battle.IntermediaryAttack("Picadura");

        // Verificar
        Assert.That("El pokemon Caterpie no tiene efectos activos.30", Is.EqualTo(resultado));
        Assert.That(trainer2.Active.Health, Is.EqualTo(battle.ActualTurn.Active.Health));

        resultado = battle.IntermediaryChangeActivePokemon(2);

            // Verificar
            Assert.That(trainer1.Active, Is.EqualTo(battle.ActualTurn.Active));
            Assert.That("Hecho", Is.EqualTo(resultado));
        }
}
