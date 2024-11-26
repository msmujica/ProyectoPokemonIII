using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Trainer))]
public class TrainerTest
{
    public Trainer trainer;

    /// <summary>
    /// Configura un entrenador antes de cada prueba.
    /// </summary>
    [Test]
    public void SetUp()
    {
        trainer = new Trainer("Ash");
    }
    
    /// <summary>
    /// Verifica que el constructor inicializa correctamente el nombre del entrenador.
    /// </summary>
    [Test]
    public void Constructor()
    {
        var trainer = new Trainer("Ash");
        Assert.That("Ash", Is.EqualTo(trainer.Name));
    }
    
    /// <summary>
    /// Verifica que el equipo del entrenador se inicializa vacio.
    /// </summary>
    [Test]
    public void StartTeam()
    {
        Assert.That(trainer.Team, Is.Not.Null);
        Assert.That(0, Is.EqualTo(trainer.Team.Count));
    }
    
    /// <summary>
    /// Verifica que se puede agregar un Pokémon al equipo del entrenador.
    /// </summary>
    
    [Test]
    public void AddPokemonToTheTeam()
    {
        var trainer = new Trainer("Ash");
        var pokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");
        trainer.Team.Add(pokemon);
        Assert.That(1, Is.EqualTo(trainer.Team.Count));
    }

    /// <summary>
    /// Verifica que no se puede agregar más de 6 Pokémon al equipo del entrenador.
    /// </summary>
    [Test]
    public void TeamFull()
    {
        for (int i = 0; i < 6; i++)
        {
            trainer.Team.Add(new Pokemon($"Pokemon{i}", 100, new List<string> { "Ataque" }, "Normal"));
        }

        var newPokemon = new Pokemon("Charmander", 100, new List<string> { "Ascuas" }, "Fuego");
        var result = trainer.ChooseTeam(1);  // Simular agregar el Pokémon
        Assert.That("Ya tienes la cantidad maxima de Pokemones en tu Equipo", Is.EqualTo(result));
    }

    /// <summary>
    /// Verifica que no se pueden agregar más Pokémon si el equipo ya está lleno.
    /// </summary>
    [Test]
    public void TeamFullCannotAddMore()
    {
        for (int i = 0; i < 6; i++)
        {
            trainer.Team.Add(new Pokemon($"Pokemon{i}", 100, new List<string> { "Ataque" }, "Normal"));
        }

        var newPokemon = new Pokemon("Charmander", 100, new List<string> { "Ascuas" }, "Fuego");
        var result = trainer.ChooseTeam(7);  // Intentar agregar más Pokémon
        Assert.That("Ya tienes la cantidad maxima de Pokemones en tu Equipo", Is.EqualTo(result));
    }
    
    /// <summary>
    /// Verifica que se puede cambiar el Pokémon activo del entrenador.
    /// </summary>
    [Test]
    public void ChangeActivePokemon()
    {
        var trainer = new Trainer("Ash");
        var pokemon1 = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");
        var pokemon2 = new Pokemon("Charizard", 150, new List<string> { "Llamarada" }, "Fuego");

        trainer.Team.Add(pokemon1);
        trainer.Team.Add(pokemon2);
        var result = trainer.ChangeActive(1);

        Assert.That(trainer.Team[1].Name, Is.EqualTo(result));
        Assert.That(trainer.Team[1], Is.EqualTo(trainer.Active));
    }
    
    /// <summary>
    /// Verifica que no se puede cambiar el Pokémon activo con un índice no válido.
    /// </summary>
    [Test]
    public void InvalidIndex()
    {
        var trainer = new Trainer("Jessy");
        var result = trainer.ChangeActive(0);
        Assert.That("Indice no valido. No se pudo cambiar el pokemon", Is.EqualTo(result));
    }
    
    /// <summary>
    /// Verifica el uso de una superpoción en un Pokémon.
    /// </summary>
    [Test]
    public void UseItemSuperpotion()
    {
        var effectsManager = new EffectsManager();
        var pokemon = new Pokemon("Bulbasaur", 100, new List<string> { "Latigazo" }, "Planta");

        trainer.ItemSetting();
        var result = trainer.UsarItem("Superpocion", pokemon, effectsManager);
        Assert.That(4, Is.EqualTo(trainer.CounterSuperPotion));
        Assert.That("El Pokémon ya está a máxima vida.", Is.EqualTo(result));  // Asumimos que ese es el resultado
    }

    /// <summary>
    /// Verifica el uso de un revivir en un Pokémon derrotado.
    /// </summary>
    [Test]
    public void UseItemRevivir()
    {
        var effectsManager = new EffectsManager();
        var pokemon = new Pokemon("Onix", 0, new List<string> { "Golpe Roca" }, "Roca") { IsDefeated = true };

        trainer.ItemSetting();
        var result = trainer.UsarItem("Revivir", pokemon, effectsManager);
        Assert.That(1, Is.EqualTo(trainer.CounterRevive));
        Assert.That("Usaste un Revivir. Usos restantes: 0", Is.EqualTo(result));  // Asumimos que ese es el resultado
    }
    
    /// <summary>
    /// Verifica el uso de una cura total en un Pokémon.
    /// </summary>
    [Test]
    public void UseItemCuraTotal()
    {
        var effectsManager = new EffectsManager();
        var pokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");

        trainer.ItemSetting();
        var result = trainer.UsarItem("CuraTotal", pokemon, effectsManager);
        Assert.That(2, Is.EqualTo(trainer.CounterTotalCure));
        Assert.That("Usaste una Cura Total. Usos restantes: 1", Is.EqualTo(result));  // Asumimos que ese es el resultado
    }
    
    /// <summary>
    /// Verifica el uso de un ítem no válido.
    /// </summary>
    [Test]
    public void UseItemNone()
    {
        var effectsManager = new EffectsManager();
        var pokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");

        trainer.ItemSetting();
        var result = trainer.UsarItem("CurasTotales", pokemon, effectsManager);
        Assert.That(2, Is.EqualTo(trainer.CounterTotalCure));
        Assert.That("Item no valido!", Is.EqualTo(result));  // Asumimos que ese es el resultado
    }

    /// <summary>
    /// Verifica que se cambie automáticamente un Pokémon activo derrotado.
    /// </summary>
    [Test]
    public void ChangeDeadPokemon()
    {
        var trainer = new Trainer("Brook");
        var deadPokemon = new Pokemon("Onix", 0, new List<string> { "Golpe Roca" }, "Roca") { IsDefeated = true };
        var livePokemon = new Pokemon("Jigglypuff", 100, new List<string> { "Canto" }, "Normal");

        trainer.Team = new List<Pokemon> { deadPokemon, livePokemon };
        trainer.Active = deadPokemon;

        trainer.CambioPokemonMuerto();
        
        Assert.That(livePokemon, Is.EqualTo(trainer.Active));
    }
    
    /// <summary>
    /// Verifica que los contadores de ítems se configuren correctamente.
    /// </summary>
    [Test]
    public void SettingItems()
    {
        var trainer = new Trainer("Misty");
        trainer.ItemSetting();
        Assert.That(4, Is.EqualTo(trainer.CounterSuperPotion));
        Assert.That(2, Is.EqualTo(trainer.CounterTotalCure));
        Assert.That(1, Is.EqualTo(trainer.CounterRevive));
    }
    
    /// <summary>
    /// Verifica la funcionalidad para elegir un ataque.
    /// </summary>
    [Test]
    public void ChooseAttack()
    {
        var effectsManager = new EffectsManager();
        var opponent = new Pokemon("Charmander", 100, new List<string> { "Ascuas" }, "Fuego");
        var activePokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");

        trainer = new Trainer("Ash");
        trainer.Team.Add(activePokemon);
        trainer.Active = activePokemon;
        
        var result = trainer.ChooseAttack("Impactrueno", opponent, effectsManager);
        
        Assert.That(result, Is.Not.Null);
    }
}
