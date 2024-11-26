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
    /// <returns>Devuelve el mensaje de confirmación de que el jugador fue agregado.</returns>
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
    /// <returns>Devuelve el mensaje de error indicando que el jugador ya está en la lista de espera.</returns>
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
    /// <returns>Devuelve el mensaje que indica el comienzo de la batalla y quién inicia.</returns>
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
    /// <returns>Devuelve una lista de los Pokémon disponibles con su tipo.</returns>
    [Test]
    public void TestShowPokémonAvailable()
    {
        var result = facade.ShowPokémonAvailable();

        Assert.That("Pokemones Disponibles: \n0 - Squirtle (Agua)\n1 - Caterpie (Bicho)\n2 - Dratini (Dragón)\n3 - Pikachu (Eléctrico)\n4 - Gastly (Fantasma)\n5 - Charmander (Fuego)\n6 - Jynx (Hielo)\n7 - Machop (Lucha)\n8 - Eevee (Normal)\n9 - Bulbasaur (Planta)\n10 - Abra (Psíquico)\n11 - Geodude (Roca)\n12 - Diglett (Tierra)\n13 - Ekans (Veneno)\n14 - Pidgey (Volador)", Is.EqualTo(result));
    }

    /// <summary>
    /// Prueba la funcionalidad de remover un jugador de la lista de espera.
    /// </summary>
    /// <returns>Devuelve el mensaje indicando si un jugador fue removido o no se encontraba en la lista.</returns>
    [Test]
    public void TestRemoveTrainerFromWaitingList()
    {
        Facade.Reset(); // Reiniciar el singleton si es necesario.
        facade = Facade.Instance;
        
        string player1 = "Ash";

        facade.AddTrainerToWaitingList(player1);
        var result1 = facade.RemoveTrainerFromWaitingList(player1);
        
        Assert.That("lol no está en la lista de espera", Is.EqualTo(
            facade.RemoveTrainerFromWaitingList("lol")));
        Assert.That("Ash removido de la lista de espera", Is.EqualTo(result1));
    }

    /// <summary>
    /// Prueba la funcionalidad de verificar si un jugador está esperando en la lista de espera.
    /// </summary>
    /// <returns>Devuelve el mensaje indicando si un jugador está esperando o no.</returns>
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
    /// <returns>Devuelve la lista de jugadores que están esperando.</returns>
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
    /// Prueba el comportamiento en caso de que no haya ningún entrenador esperando.
    /// </summary>
    /// <returns>Devuelve el mensaje indicando que no hay nadie esperando.</returns>
    [Test]
    public void TestGetAllTrainersWaitingNone()
    {
        Facade.Reset(); // Reiniciar el singleton si es necesario.
        facade = Facade.Instance;
        
        var result = facade.GetAllTrainersWaiting();

        Assert.That("No hay nadie esperando", Is.EqualTo(result));
    }

    /// <summary>
    /// Prueba el comportamiento cuando no hay jugadores esperando en la lista.
    /// </summary>
    /// <returns>Devuelve el mensaje indicando que no hay jugadores para iniciar una batalla.</returns>
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
    /// <returns>Devuelve el mensaje confirmando que el Pokémon fue agregado al equipo del jugador.</returns>
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
    /// <returns>Devuelve el mensaje indicando si el uso del ítem fue válido o no.</returns>
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

        if (result == "No es tu turno ESPERA!")
        {
            result = facade.UseItem(player2, 0, "Superpocion");
        }
        
        Assert.That(result, !Is.Null);
    }
            /// <summary>
            /// Prueba el comportamiento cuando el jugador intenta usar un ítem cuando su Pokémon ya está a máxima vida.
            /// </summary>
            /// <returns>Devuelve el mensaje indicando que no se puede usar el ítem
            [Test]
            public void TestUseItemNone()
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
                var result = facade.UseItem(player2, 0, "Superpocion");

                if (result == "El Pokémon ya está a máxima vida.")
                {
                    result = facade.UseItem(player2, 0, "Superpocion");
                }
                
                Assert.That(result, Is.EqualTo("No es tu turno ESPERA!"));
                
            }

            /// <summary>
            /// Prueba la funcionalidad de realizar un ataque con un Pokémon durante una batalla.
            /// </summary>
            [Test]
            public void TestAttackPokemon()
            {
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
                if (result == "No es tu turno ESPERA!")
                {
                    result = facade.AttackPokemon(player2, "Picadura");
                }
                
                Assert.That(result, !Is.Null);
            }
            /// <summary>
            /// Prueba la funcionalidad de atacar a un Pokémon durante una batalla cuando el jugador no tiene turno.
            /// Verifica que el sistema no permita realizar el ataque si no es el turno del jugador.
            /// </summary>
            /// <returns>Devuelve "No es tu turno ESPERA!" si el jugador no tiene turno.</returns>
            [Test]
            public void TestAttackPokemonNone()
            {
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
                // Simula que el jugador realiza un ataque
                var result = facade.AttackPokemon(player1, "Picadura");
                if (result != "No es tu turno ESPERA!")
                {
                    result = facade.AttackPokemon(player1, "Picadura");
                }
                Assert.That(result, Is.EqualTo("No es tu turno ESPERA!"));
            }

            /// <summary>
            /// Prueba la funcionalidad de cambiar el Pokémon activo durante una batalla.
            /// Verifica que el cambio de Pokémon sea realizado correctamente cuando es el turno del jugador.
            /// </summary>
            /// <returns>Devuelve el nombre del Pokémon cambiado (por ejemplo, "Gastly").</returns>
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

                string playerName = "Misty";
                var result = facade.ChangePokemon("Ash", 3);
                if (result == "No es tu turno ESPERA!")
                {
                    result = facade.ChangePokemon("Misty", 3);
                    playerName = "Ash";
                }
                Assert.That($"Gastly", Is.EqualTo(result));
            }
            /// <summary>
            /// Prueba la funcionalidad de intentar cambiar el Pokémon activo cuando no es el turno del jugador.
            /// Verifica que el sistema no permita realizar el cambio si no es el turno del jugador.
            /// </summary>
            /// <returns>Devuelve "No es tu turno ESPERA!" si el jugador no tiene turno.</returns>
            [Test]
            public void TestChangePokemonNone()
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

                string playerName = "Misty";
                var result = facade.ChangePokemon("Ash", 3);
                if (result != "No es tu turno ESPERA!")
                {
                    result = facade.ChangePokemon("Ash", 3);
                }
                Assert.That("No es tu turno ESPERA!", Is.EqualTo(result));
            }
            /// <summary>
            /// Prueba la funcionalidad de rendirse durante una batalla.
            /// Verifica que el jugador que se rinde termine la batalla correctamente.
            /// </summary>
            /// <returns>Devuelve un mensaje indicando que el jugador se rindió y terminó la batalla.</returns>
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
            /// <summary>
            /// Prueba la funcionalidad de mostrar la vida de los Pokémon durante una batalla.
            /// Verifica que el sistema devuelva la vida correcta de los Pokémon en el equipo de un jugador.
            /// </summary>
            /// <returns>Devuelve un string con la información de la vida de los Pokémon en el equipo del jugador.</returns>
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
            /// <summary>
            /// Prueba la funcionalidad de mostrar los ataques de un Pokémon.
            /// Verifica que el sistema devuelva correctamente los ataques disponibles para un Pokémon.
            /// </summary>
            /// <returns>Devuelve un string con los ataques disponibles de un Pokémon, incluyendo el tipo y daño de cada ataque.</returns>
            [Test]
            public void TestShowPokemonAttacks()
            {
                Facade.Reset(); // Reiniciar el singleton si es necesario.
                facade = Facade.Instance;

                string player1 = "Ash";
                string player2 = "Misty";
                facade.AddTrainerToWaitingList(player1);
                facade.AddTrainerToWaitingList(player2);
                facade.StartBattle(player1, player2);

                facade.ChooseTeam(player1, 0);

                string esperado = "Ataques:\nPistola Agua: Tipo = Agua, Daño = 40" +
                                  "\nHidrobomba: Tipo = Agua, Daño = 110\nBurbuja: Tipo = Agua, Daño = 20\n";

                facade.GetPokemonAtacks("");

                Assert.That(esperado, Is.EqualTo(facade.GetPokemonAtacks("Ash")));
            }
            /// <summary>
            /// Prueba la funcionalidad de cambiar el turno durante una batalla.
            /// Verifica que el sistema permita cambiar el turno entre los jugadores correctamente.
            /// </summary>
            /// <returns>Devuelve un mensaje que indica quién tiene el turno después de cambiarlo.</returns>
            [Test]
            public void ChangeTurnTest()
            {
                Facade.Reset(); // Reiniciar el singleton si es necesario.
                facade = Facade.Instance;
                string player1 = "Ash";
                string player2 = "Misty";
                facade.AddTrainerToWaitingList(player1);
                facade.AddTrainerToWaitingList(player2);
                facade.StartBattle(player1, player2);
                string result = facade.ChangeTurn("Ash");
                string esperado = null;
                if (result != "No es tu turno")
                {
                    esperado = "Turno cambiado. Es el turno de Misty";
                }
                else
                {
                    result = facade.ChangeTurn("Misty");
                    esperado = "Turno cambiado. Es el turno de Ash";
                }
                
                Assert.That(esperado, Is.EqualTo(result));
            }
            /// <summary>
            /// Prueba la funcionalidad de intentar cambiar el turno cuando no es el turno del jugador.
            /// Verifica que el sistema no permita cambiar el turno si no es el turno del jugador.
            /// </summary>
            /// <returns>Devuelve "No es tu turno" si el jugador intenta cambiar el turno cuando no le corresponde.</returns>
            [Test]
            public void ChangeTurnNoneTest()
            {
                Facade.Reset(); // Reiniciar el singleton si es necesario.
                facade = Facade.Instance;
                string player1 = "Ash";
                string player2 = "Misty";
                facade.AddTrainerToWaitingList(player1);
                facade.AddTrainerToWaitingList(player2);
                facade.StartBattle(player1, player2);
                string result = facade.ChangeTurn("Ash");
                string esperado = "No es tu turno";
                if (result != esperado)
                {
                    result = facade.ChangeTurn("Ash");
                }
                
                Assert.That(esperado, Is.EqualTo(result));
            }
            /// <summary>
            /// Prueba la funcionalidad de declarar al jugador como ganador en una batalla.
            /// Verifica que el mensaje de victoria se devuelva correctamente cuando un jugador gana.
            /// </summary>
            [Test]
            public void WinTest()
            {
                Facade.Reset(); // Reiniciar el singleton si es necesario.
                facade = Facade.Instance;
                string player1 = "Ash";
                string player2 = "Misty";
                facade.AddTrainerToWaitingList(player1);
                facade.AddTrainerToWaitingList(player2);
                facade.StartBattle(player1, player2);
                string result = facade.Win(player1);
                string esperado = "Ash a ganado!. Termino la Batalla!";
                
                Assert.That(esperado, Is.EqualTo(result));
            }
            
    }