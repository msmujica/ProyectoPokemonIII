using Library;
using Library.Items;
using NUnit.Framework;
using System.Collections.Generic;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests.Domain
{
    [TestFixture]
    public class BattleTests
    {
        private Trainer trainer1;
        private Trainer trainer2;
        private Battle battle;

        [SetUp]
        public void SetUp()
        {
            // Crear entrenadores
            trainer1 = new Trainer("Ash");
            trainer2 = new Trainer("Misty");
            
            // Crear la batalla
            battle = new Battle(trainer1, trainer2);
            battle.ActualTurn = trainer2;
            battle.LastTurn = trainer1;

            // Añadir exactamente 6 Pokémon al equipo del primer entrenador
            battle.ActualTurn.ChooseTeam(0);       
            battle.ActualTurn.ChooseTeam(0);        
            battle.ActualTurn.ChooseTeam(0);        
            battle.ActualTurn.ChooseTeam(0);        
            battle.ActualTurn.ChooseTeam(0);     
            battle.ActualTurn.ChooseTeam(0);

            battle.LastTurn.ChooseTeam(0);
            battle.LastTurn.ChooseTeam(0);
            battle.LastTurn.ChooseTeam(0);
            battle.LastTurn.ChooseTeam(0);
            battle.LastTurn.ChooseTeam(0);
            battle.LastTurn.ChooseTeam(0);
        }

        [Test]
        public void CannotStartBattle_WithLessThanSixPokemon()
        {
            // Crear un entrenador con menos de 6 Pokémon
            var brock = new Trainer("Brock");
            var misty = new Trainer("a");
            Battle invalid = new Battle(brock, misty);
            invalid.ActualTurn.ChooseTeam(0);
            invalid.LastTurn.ChooseTeam(0);
            
            // Intentar iniciar la batalla y capturar la excepción
            Assert.That(invalid.IntermediaryAttack("Burbuja"), Is.EqualTo("No tenes los pokemones suficientes para empezar la batalla. "));
        }

        [Test]
        public void SwitchingPokemon_WorksCorrectly()
        {
            // Cambiar el Pokémon activo de Trainer1 al segundo Pokémon
            string result = battle.IntermediaryChangeActivePokemon(1);

            // Verificar que el Pokémon activo ha cambiado correctamente
            Assert.That(result, Is.Not.Empty, "El cambio de Pokémon debería generar un mensaje de resultado.");
            Assert.That(trainer1.Active.Name, Is.EqualTo("Squirtle"), "El Pokémon activo debería ser el segundo del equipo.");
        }

        [Test]
        public void IntermediaryAttack_WithInvalidAttackName()
        {
            // Intentar realizar un ataque con un nombre inválido
            string result = battle.IntermediaryAttack("AtaqueInvalido");

            // Verificar que el resultado es un mensaje de error
            Assert.That(result, Is.EqualTo("El pokemon Squirtle no tiene efectos activos. Este no es tu ataque. Turno terminado. \n"));
        }



        [Test]
        public void ValidacionPokemonVivo_HandlesFaintedPokemon()
        {
            while (trainer1.Active.Health > 0)
            {
                battle.IntermediaryAttack("Burbuja");
                battle.CambiarTurno();
            }
            battle.CambiarTurno();

            // Verificar que se realiza el cambio de Pokémon automáticamente
            bool result = battle.ValidacionPokemonVivo();

            Assert.That(result, Is.True);
            Assert.That(trainer1.Active.Health, Is.GreaterThan(0));
        }

        [Test]
        public void IntermediaryUseItem_AppliesItemCorrectly()
        {
            while (trainer1.Active.Health == 100)
            {
                battle.IntermediaryAttack("Burbuja");
                battle.CambiarTurno();
            }
            battle.CambiarTurno();
            // Usar un ítem de curación en un Pokémon
            string result = battle.IntermediaryUseItem(0, "Superpocion");

            // Verificar que la salud del Pokémon activo ha aumentado
            Assert.That(result, Does.Contain("Usaste una Super Pocion. Usos restantes: 3"));
            Assert.That(trainer1.Active.Health, Is.EqualTo(100));
        }

        [Test]
        public void IntermediaryChangeActivePokemon_WithInvalidIndex()
        {
            // Intentar cambiar a un Pokémon que no existe en el equipo
            string result = battle.IntermediaryChangeActivePokemon(10);

            // Verificar que la respuesta es la esperada
            Assert.That(result, Is.EqualTo("Selección de Pokémon inválida. Por favor, intenta de nuevo."));
        }

        [Test]
        public void IntermediaryUseItem_WithInvalidItem()
        {
            // Intentar usar un ítem que no existe
            string result = battle.IntermediaryUseItem(0, "ItemInvalido");

            // Verificar que el resultado sea el esperado
            Assert.That(result, Is.EqualTo("Item no valido. "));
            result = battle.IntermediaryUseItem(10, "Superpocion");
            Assert.That(result, Is.EqualTo("Selección de Pokémon inválida."));


        }

        [Test]
        public void ValidacionPokemon_ReturnsFalse_WhenBothHaveSixPokemons()
        {
            // Ambos jugadores tienen 6 Pokémon
            bool result = battle.validacionPokemon();

            // Verificar que la validación sea correcta
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidacionWin_RecognizesWinnerWhenAllOpponentPokemonAreFainted()
        {

            while (!battle.ValidacionWin())
            {
                battle.IntermediaryAttack("Burbuja");
                battle.CambiarTurno();
            }
            
            // Verificar que se detecta la victoria
            bool result = battle.ValidacionWin();

            Assert.That(result, Is.True, "El jugador debería ganar si todos los Pokémon del oponente están derrotados.");
        }
    }
}
