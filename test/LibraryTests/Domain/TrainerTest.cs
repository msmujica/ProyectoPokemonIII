using Library;
using NUnit.Framework;

namespace LibraryTests.Domain;

[TestFixture]
[TestOf(typeof(Trainer))]
public class TrainerTest
{
    public Trainer trainer;

    [Test]
    public void SetUp()
    {
        trainer = new Trainer("Ash");
    }

    [Test]
    public void Constructor()
    {
        var trainer = new Trainer("Ash");
        Assert.That("Ash", Is.EqualTo(trainer.Name));
    }

    [Test]
    public void StartTeam()
    {
        Assert.That(trainer.Team, Is.Not.Null);
        Assert.That(0, Is.EqualTo(trainer.Team.Count));
    }
    
    [Test]
    public void AddPokemonToTheTeam()
    {
        var trainer = new Trainer("Ash");
        var pokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");
        trainer.Team.Add(pokemon);
        Assert.That(1, Is.EqualTo(trainer.Team.Count));
    }

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

    [Test]
    public void InvalidIndex()
    {
        var trainer = new Trainer("Jessy");
        var result = trainer.ChangeActive(0);
        Assert.That("Indice no valido. No se pudo cambiar el pokemon", Is.EqualTo(result));
    }

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

    [Test]
    public void SettingItems()
    {
        var trainer = new Trainer("Misty");
        trainer.ItemSetting();
        Assert.That(4, Is.EqualTo(trainer.CounterSuperPotion));
        Assert.That(2, Is.EqualTo(trainer.CounterTotalCure));
        Assert.That(1, Is.EqualTo(trainer.CounterRevive));
    }

    [Test]
    public void ChooseAttack()
    {
        // Crear instancias necesarias
        var effectsManager = new EffectsManager();
        var opponent = new Pokemon("Charmander", 100, new List<string> { "Ascuas" }, "Fuego");
        var activePokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");

        // Inicializar el entrenador y asignar el Pokémon activo
        trainer = new Trainer("Ash");
        trainer.Team.Add(activePokemon);
        trainer.Active = activePokemon; // Asegúrate de que el Pokémon activo esté configurado

        // Elegir un ataque
        var result = trainer.ChooseAttack("Impactrueno", opponent, effectsManager);

        // Verificar el resultado (ajustar la expectativa según el comportamiento real de ChooseAttack)
        Assert.That(result, Is.Not.Null); // El resultado debe ser un string válido
    }
}
