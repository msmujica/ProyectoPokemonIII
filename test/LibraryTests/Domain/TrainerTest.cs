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
            trainer.Team.Add(newPokemon);

            Assert.That(7, Is.EqualTo(trainer.Team.Count)); 
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
            trainer.UsarItem("Superpocion", pokemon, effectsManager);
            Assert.That(4, Is.EqualTo(trainer.CounterSuperPotion));
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
}
