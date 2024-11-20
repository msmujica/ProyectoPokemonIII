using Library;
using NUnit.Framework;
using NUnit.Framework.Legacy;


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
            ClassicAssert.AreEqual("Ash", trainer.Name);
        }

        [Test]
        public void StartTeam()
        {
            ClassicAssert.IsNotNull(trainer.Team);
            ClassicAssert.AreEqual(0, trainer.Team.Count);
        }

        [Test]
        public void AddPokemonToTheTeam()
        {
            var pokemon = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");
            trainer.Team.Add(pokemon);
            ClassicAssert.AreEqual(1, trainer.Team.Count);
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

            ClassicAssert.AreEqual(7, trainer.Team.Count); 
        }

        [Test]
        public void ChangeActivePokemon()
        {
            var pokemon1 = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno" }, "Eléctrico");
            var pokemon2 = new Pokemon("Charizard", 150, new List<string> { "Llamarada" }, "Fuego");

            trainer.Team.Add(pokemon1);
            trainer.Team.Add(pokemon2);

            var result = trainer.ChangeActive(1);

            ClassicAssert.AreEqual(trainer.Team[1].Name, result);
            ClassicAssert.AreEqual(trainer.Team[1], trainer.Active);
        }

        [Test]
        public void InvalidIndex()
        {
            var result = trainer.ChangeActive(0);
            ClassicAssert.AreEqual("Indice no valido. No se pudo cambiar el pokemon", result);
        }

        [Test]
        public void UseItemSuperpotion()
        {
            var effectsManager = new EffectsManager();
            var pokemon = new Pokemon("Bulbasaur", 100, new List<string> { "Latigazo" }, "Planta");

            trainer.ItemSetting();
            trainer.UsarItem("Superpocion", pokemon, effectsManager);
            ClassicAssert.AreEqual(4, trainer.CounterSuperPotion);
        }

        [Test]
        public void ChangeDeadPokemon()
        {
            var deadPokemon = new Pokemon("Onix", 0, new List<string> { "Golpe Roca" }, "Roca") { IsDefeated = true };
            var livePokemon = new Pokemon("Jigglypuff", 100, new List<string> { "Canto" }, "Normal");

            trainer.Team = new List<Pokemon> { deadPokemon, livePokemon };
            trainer.Active = deadPokemon;

            trainer.CambioPokemonMuerto();

            ClassicAssert.AreEqual(livePokemon, trainer.Active);
        }

        [Test]
        public void SettingItems()
        {
            trainer.ItemSetting();
            ClassicAssert.AreEqual(4, trainer.CounterSuperPotion);
            ClassicAssert.AreEqual(2, trainer.CounterTotalCure);
            ClassicAssert.AreEqual(1, trainer.CounterRevive);
        }
}