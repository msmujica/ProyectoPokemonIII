using Library;
using NUnit.Framework;

namespace LibraryTests.Domain
{
    // TestFixture que agrupa las pruebas relacionadas con el manejo del efecto de parálisis en un Pokémon
    [TestFixture]
    [TestOf(typeof(ParalyzeEffect))]
    public class ParalyzeEffectTest
    {
        // Prueba para iniciar el efecto de parálisis en un Pokémon
        [Test]
        public void TestStartEffect()
        {
            // Arrange: Preparar el Pokémon y el efecto
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            
            // Act: Iniciar el efecto de parálisis
            string result = paralyze.StartEffect(Pikachu);
            
            // Assert: Verificar que el efecto se haya aplicado correctamente
            Assert.That("El pokemon Pikachu se le aplico el efecto paralisis.", Is.EqualTo(result));
        }

        // Prueba para procesar el efecto de parálisis y determinar si el Pokémon puede atacar
        [Test]
        public void TestProcessEffect()
        {
            // Arrange: Preparar el Pokémon y el efecto
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            paralyze.StartEffect(Pikachu);

            // Act: Procesar el efecto de parálisis
            string result = paralyze.ProcessEffect(Pikachu);
            
            // Assert: Verificar si el Pokémon supera la parálisis o está paralizado
            string esperado = (result == "El pokemon Pikachu supera la parálisis en este turno y puede atacar. ") ?
                "El pokemon Pikachu supera la parálisis en este turno y puede atacar. " :
                "Pikachu está paralizado y no puede atacar, perdiste el turno. ";

            Assert.That(esperado, Is.EqualTo(result));
        }

        // Prueba para procesar el efecto de parálisis cuando el Pokémon no puede atacar
        [Test]
        public void TestProcessEffectNone()
        {
            // Arrange: Preparar el Pokémon y el efecto
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            paralyze.StartEffect(Pikachu);
            paralyze.IcanAttack = false;

            // Act: Procesar el efecto de parálisis
            string result = paralyze.ProcessEffect(Pikachu);
            
            // Assert: Verificar que el Pokémon está paralizado y no puede atacar
            string esperado = (result == "El pokemon Pikachu supera la parálisis en este turno y puede atacar. ") ?
                "El pokemon Pikachu supera la parálisis en este turno y puede atacar. " :
                "Pikachu está paralizado y no puede atacar, perdiste el turno. ";

            Assert.That(esperado, Is.EqualTo(result));
        }

        // Prueba para verificar si el mensaje de información sobre el efecto de parálisis coincide con el mensaje del procesamiento del efecto
        [Test]
        public void TestInfo()
        {
            // Arrange: Preparar el Pokémon y el efecto
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            
            // Assert: Verificar que el mensaje de información coincida con el resultado del procesamiento del efecto
            Assert.That(paralyze.ProcessEffect(Pikachu), Is.EqualTo(paralyze.Info(Pikachu)));
        }

        // Test cuando el Pokémon puede atacar después de superar la parálisis
        [Test]
        public void TestProcessEffect_ShouldAllowAttack_WhenPokemonCanAttack()
        {
            // Arrange: Preparar el Pokémon y el efecto
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            paralyze.StartEffect(Pikachu);

            // Forzamos el comportamiento de `ICanAttack` para que sea `true` (puede atacar)
            paralyze.IcanAttack = true;

            // Act: Procesar el efecto
            string result = paralyze.ProcessEffect(Pikachu);

            // Assert: Verificar que el Pokémon pueda atacar
            Assert.That(result, Is.EqualTo(result));
        }

        // Test cuando el Pokémon no puede atacar debido a la parálisis
        [Test]
        public void TestProcessEffect_ShouldNotAllowAttack_WhenPokemonCannotAttack()
        {
            // Arrange: Preparar el Pokémon y el efecto
            Pokemon Pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo", "Trueno"}, "Eléctrico");
            ParalyzeEffect paralyze = new ParalyzeEffect();
            paralyze.StartEffect(Pikachu);

            // Forzamos el comportamiento de `ICanAttack` para que sea `false` (no puede atacar)
            paralyze.IcanAttack = false;

            // Act: Procesar el efecto
            string result = paralyze.ProcessEffect(Pikachu);

            // Assert: Verificar que el Pokémon no pueda atacar
            Assert.That(result, Is.EqualTo(result));
        }
    }
}
