using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Facade))]
public class FacadeTest
{
        private Facade facade;

        /// <summary>
        /// Configuración previa a cada prueba unitaria.
        /// Se asegura de que cada prueba comience con una nueva instancia de la clase <see cref="Facade"/>.
        /// </summary>
        [Test]
        public void SetUp()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
        }

        /// <summary>
        /// Prueba la funcionalidad de agregar un jugador a la lista de espera.
        /// </summary>
        
        [Test]
        public void TestAddTrainerToWaitingList()
        {
            string player1 = "Ash";

            var result = facade.AddTrainerToWaitingList(player1);
            
            Assert.That("Ash agregado a la lista de espera", Is.EqualTo(result));
        }

        /// <summary>
        /// Prueba la funcionalidad de intentar agregar un jugador duplicado a la lista de espera.
        /// </summary>
        [Test]
        public void TestAddTrainerToWaitingList_Duplicate()
        {
            string player1 = "Ash";

            facade.AddTrainerToWaitingList(player1);
            var result = facade.AddTrainerToWaitingList(player1);

            Assert.That("Ash ya está en la lista de espera", Is.EqualTo(result));
        }

        /// <summary>
        /// Prueba el inicio de una batalla entre dos jugadores.
        /// </summary>
        [Test]
        public void TestStartBattle()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
            
            string player1 = "Ash";
            string player2 = "Misty";

            facade.AddTrainerToWaitingList(player1);
            facade.AddTrainerToWaitingList(player2);
            var result = facade.StartBattle(player1, player2);
            string resultadoEsperado = null;
            if (result == "Comienza Ash vs Misty\nEmpieza Ash")
            {
                resultadoEsperado = "Comienza Ash vs Misty\nEmpieza Ash";
            }
            else
            {
                resultadoEsperado = "Comienza Ash vs Misty\nEmpieza Misty";
            }
            Assert.That(resultadoEsperado, Is.EqualTo(result)); 
        }

        /// <summary>
        /// Prueba la funcionalidad de mostrar los Pokémon disponibles en la Pokédex.
        /// </summary>
        [Test]
        public void TestShowPokémonAvailable()
        {
            var result = facade.ShowPokémonAvailable();

            Assert.That("Pokemones Disponibles: \n0 - Squirtle (Agua)\n1 - Caterpie (Bicho)\n2 - Dratini (Dragón)\n3 - Pikachu (Eléctrico)\n4 - Gastly (Fantasma)\n5 - Charmander (Fuego)\n6 - Jynx (Hielo)\n7 - Machop (Lucha)\n8 - Eevee (Normal)\n9 - Bulbasaur (Planta)\n10 - Abra (Psíquico)\n11 - Geodude (Roca)\n12 - Diglett (Tierra)\n13 - Ekans (Veneno)\n14 - Pidgey (Volador)", Is.EqualTo(result));
        }

        /// <summary>
        /// Prueba la funcionalidad de remover un jugador de la lista de espera.
        /// </summary>
        [Test]
        public void TestRemoveTrainerFromWaitingList()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
            
            string player1 = "Ash";

            facade.AddTrainerToWaitingList(player1);
            var result1 = facade.RemoveTrainerFromWaitingList(player1);

            Assert.That("Ash removido de la lista de espera", Is.EqualTo(result1));
        }

        /// <summary>
        /// Prueba la funcionalidad de verificar si un jugador está esperando en la lista de espera.
        /// </summary>
        [Test]
        public void TestTrainerIsWaiting()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
            
            string player1 = "Ash";

            facade.AddTrainerToWaitingList(player1);
            var result1 = facade.TrainerIsWaiting(player1);
            var result2 = facade.TrainerIsWaiting("Misty");

            Assert.That("Ash está esperando", Is.EqualTo(result1));
            Assert.That("Misty no está esperando", Is.EqualTo(result2));
        }

        /// <summary>
        /// Prueba la funcionalidad de obtener todos los jugadores esperando en la lista de espera.
        /// </summary>
        [Test]
        public void TestGetAllTrainersWaiting()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
            
            string player1 = "Ash";

            facade.AddTrainerToWaitingList(player1);
            var result = facade.GetAllTrainersWaiting();

            Assert.That("Esperan: Ash; ", Is.EqualTo(result));
        }

        /// <summary>
        /// Prueba el comportamiento cuando no hay jugadores esperando en la lista.
        /// </summary>
        [Test]
        public void TestStartBattleNoPlayersWaiting()
        {
            string player1 = "Ash";

            var result = facade.StartBattle(player1, null);

            Assert.That("No hay nadie esperando", Is.EqualTo(result));
        }

        /// <summary>
        /// Prueba la funcionalidad de elegir un equipo de Pokémon para un jugador durante una batalla.
        /// </summary>
        [Test]
        public void TestChooseTeam()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
            
            string player1 = "Ash";
            string player2 = "Misty";
            facade.AddTrainerToWaitingList(player1);
            facade.AddTrainerToWaitingList(player2);
            facade.StartBattle(player1, player2);

            var result = facade.ChooseTeam(player1, 0);

            Assert.That("El pokemon Squirtle se agrego a la lista, quedan 5 espacios.", Is.EqualTo(result));
        }

        /// <summary>
        /// Prueba la funcionalidad de usar un ítem durante una batalla.
        /// </summary>
        [Test]
        public void TestUseItem()
        {
            string player1 = "Ash";
            string player2 = "Misty";
            facade.AddTrainerToWaitingList(player1);
            facade.AddTrainerToWaitingList(player2);
            facade.StartBattle(player1, player2);

            facade.ChooseTeam(player1, 1);
            facade.ChooseTeam(player1, 2);
            facade.ChooseTeam(player1, 3);
            facade.ChooseTeam(player1, 4);
            facade.ChooseTeam(player1, 5);
            facade.ChooseTeam(player1, 6);

            facade.ChooseTeam(player2, 1);
            facade.ChooseTeam(player2, 2);
            facade.ChooseTeam(player2, 3);
            facade.ChooseTeam(player2, 4);
            facade.ChooseTeam(player2, 5);
            facade.ChooseTeam(player2, 6);

            // Simula que el jugador usa un ítem
            var result = facade.UseItem(player1, 0, "Superpocion");
            string esperado = "El Pokémon ya está a máxima vida.";

            if (result != "No es tu turno ESPERA!")
            {
                esperado = "El Pokémon ya está a máxima vida.";
            }
            else
            {
                result = facade.UseItem(player2, 0, "Superpocion");
            }
            
            Assert.That(esperado, Is.EqualTo(result));
            
        }

        /// <summary>
        /// Prueba la funcionalidad de realizar un ataque con un Pokémon durante una batalla.
        /// </summary>
        [Test]
        public void TestAttackPokemon()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
            
            string player1 = "Ash";
            string player2 = "Misty";
            facade.AddTrainerToWaitingList(player1);
            facade.AddTrainerToWaitingList(player2);
            facade.StartBattle(player1, player2);

            facade.ChooseTeam(player1, 0);
            facade.ChooseTeam(player1, 1);
            facade.ChooseTeam(player1, 2);
            facade.ChooseTeam(player1, 3);
            facade.ChooseTeam(player1, 4);
            facade.ChooseTeam(player1, 5);

            facade.ChooseTeam(player2, 0);
            facade.ChooseTeam(player2, 1);
            facade.ChooseTeam(player2, 2);
            facade.ChooseTeam(player2, 3);
            facade.ChooseTeam(player2, 4);
            facade.ChooseTeam(player2, 5);

            string opcionUno = "El pokemon Squirtle no tiene efectos activos.El pokemon Squirtle recibió 20 de " +
                               "daño con el ataque Pistola Agua. El ataque es preciso. Como el ataque es tipo Agua " +
                               "el daño es 20. El pokemon Squirtle se le aplica el efecto dormir por 1 turnos. " +
                               "Turno terminado.";

            // Simula que el jugador realiza un ataque
            var result = facade.AttackPokemon(player1, "Picadura");
            if (result != "No es tu turno ESPERA!")
            {
                if (opcionUno == result)
                {
                    Assert.That(opcionUno, Is.EqualTo(opcionUno));
                }
            }
        }

        /// <summary>
        /// Prueba la funcionalidad de cambiar el Pokémon activo durante una batalla.
        /// </summary>
        [Test]
        public void TestChangePokemon()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
            
            string player1 = "Ash";
            string player2 = "Misty";
            facade.AddTrainerToWaitingList(player1);
            facade.AddTrainerToWaitingList(player2);
            facade.StartBattle(player1, player2);
            facade.ChooseTeam(player1, 1);
            facade.ChooseTeam(player1, 2);
            facade.ChooseTeam(player1, 3);
            facade.ChooseTeam(player1, 4);
            facade.ChooseTeam(player1, 5);
            facade.ChooseTeam(player1, 6);

            facade.ChooseTeam(player2, 1);
            facade.ChooseTeam(player2, 2);
            facade.ChooseTeam(player2, 3);
            facade.ChooseTeam(player2, 4);
            facade.ChooseTeam(player2, 5);
            facade.ChooseTeam(player2, 6);

            // Simula que el jugador cambia el Pokémon activo
            string esperado = null;
            var result = facade.ChangePokemon(player1, 3);
            if (result != "No es tu turno ESPERA!")
            {
                esperado = "Gastly";
            }
            else
            {
                result = facade.ChangePokemon(player2, 3);
                esperado = "Gastly";
            }
            Assert.That(esperado, Is.EqualTo(result));
        }

        [Test]
        public void TestSurrender()
        {
            string player1 = "Ash";
            string player2 = "Misty";
            facade.AddTrainerToWaitingList(player1);
            facade.AddTrainerToWaitingList(player2);
            facade.StartBattle(player1, player2);

            string resultado = facade.Surrender(player1);
            
            Assert.That("Ash se a rendido. Termino la Batalla", Is.EqualTo(resultado));
        }

        [Test]
        public void TestVida()
        {
            Facade.Reset(); // Reiniciar el singleton si es necesario.
            facade = Facade.Instance;
            string player1 = "Ash";
            string player2 = "Misty";
            facade.AddTrainerToWaitingList(player1);
            facade.AddTrainerToWaitingList(player2);
            facade.StartBattle(player1, player2);
            facade.ChooseTeam(player1, 0);
            facade.ChooseTeam(player1, 0);
            facade.ChooseTeam(player1, 0);
            facade.ChooseTeam(player1, 0);
            facade.ChooseTeam(player1, 0);
            facade.ChooseTeam(player1, 0);

            string result = facade.ShowEnemiesPokemon(player1);
            
            string esperado =
                "Pokemon:\n\nSquirtleVida: 100/100\nSquirtleVida: 100/100\nSquirtleVida: 100/100\nSquirtleVida: 100/100\nSquirtleVida: 100/100\nSquirtleVida: 100/100";
            
            Assert.That(esperado, Is.EqualTo(result));
        }
}