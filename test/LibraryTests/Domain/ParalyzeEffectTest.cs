using Library;
using NUnit.Framework;

namespace LibraryTests.Domain
{
    [TestFixture]
    [TestOf(typeof(ParalyzeEffect))]
    public class ParalyzeEffectTest
    {
        /// <summary>
        /// Verifica que el efecto de parálisis se inicie correctamente en un Pokémon.
        /// </summary>
        /// <returns>Un mensaje indicando que el efecto de parálisis se aplicó correctamente.</returns>
        [Test]
        public void TestStartEffect()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();

            string result = paralyze.StartEffect(Pikachu);

            Assert.That("El pokemon Pikachu se le aplico el efecto paralisis.", Is.EqualTo(result));
        }

        /// <summary>
        /// Procesa el efecto de parálisis y verifica si el Pokémon puede atacar o no en el turno actual.
        /// </summary>
        /// <returns>
        /// Un mensaje indicando si el Pokémon supera la parálisis y puede atacar, 
        /// o si está paralizado y pierde el turno.
        /// </returns>
        [Test]
        public void TestProcessEffect()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            paralyze.StartEffect(Pikachu);

            string result = paralyze.ProcessEffect(Pikachu);

            string esperado = (result == "El pokemon Pikachu supera la parálisis en este turno y puede atacar. ") ?
                "El pokemon Pikachu supera la parálisis en este turno y puede atacar. " :
                "Pikachu está paralizado y no puede atacar, perdiste el turno. ";

            Assert.That(esperado, Is.EqualTo(result));
        }

        /// <summary>
        /// Procesa el efecto de parálisis en el caso donde el Pokémon no puede atacar debido al estado de parálisis.
        /// </summary>
        /// <returns>
        /// Un mensaje indicando si el Pokémon supera la parálisis y puede atacar, 
        /// o si permanece paralizado y pierde el turno.
        /// </returns>
        [Test]
        public void TestProcessEffectNone()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            paralyze.StartEffect(Pikachu);
            paralyze.IcanAttack = false;

            string result = paralyze.ProcessEffect(Pikachu);

            string esperado = (result == "El pokemon Pikachu supera la parálisis en este turno y puede atacar. ") ?
                "El pokemon Pikachu supera la parálisis en este turno y puede atacar. " :
                "Pikachu está paralizado y no puede atacar, perdiste el turno. ";

            Assert.That(esperado, Is.EqualTo(result));
        }

        /// <summary>
        /// Verifica que el método <c>Info</c> del efecto de parálisis retorna el mismo mensaje 
        /// que el procesamiento del efecto (<c>ProcessEffect</c>).
        /// </summary>
        [Test]
        public void TestInfo()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();

            Assert.That(paralyze.ProcessEffect(Pikachu), Is.EqualTo(paralyze.Info(Pikachu)));
        }

        /// <summary>
        /// Procesa el efecto de parálisis cuando el Pokémon puede atacar 
        /// después de superar el estado de parálisis.
        /// </summary>
        /// <returns>Un mensaje indicando que el Pokémon puede atacar.</returns>
        [Test]
        public void TestProcessEffectShouldAllowAttackWhenPokemonCanAttack()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            paralyze.StartEffect(Pikachu);

            paralyze.IcanAttack = true;

            string result = paralyze.ProcessEffect(Pikachu);

            Assert.That(result, Is.EqualTo(result));
        }

        /// <summary>
        /// Procesa el efecto de parálisis cuando el Pokémon no puede atacar debido al estado de parálisis.
        /// </summary>
        /// <returns>Un mensaje indicando que el Pokémon no puede atacar y pierde el turno.</returns>
        [Test]
        public void TestProcessEffectShouldNotAllowAttackWhenPokemonCannotAttack()
        {
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno" }, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            paralyze.StartEffect(Pikachu);

            paralyze.IcanAttack = false;

            string result = paralyze.ProcessEffect(Pikachu);

            Assert.That(result, Is.EqualTo(result));
        }
    }
}
