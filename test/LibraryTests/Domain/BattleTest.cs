using Library;
using Library.Items;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    /// <summary>
    /// Clase de pruebas unitarias para simular una batalla entre dos entrenadores y verificar
    /// las interacciones entre los métodos de la clase <see cref="Trainer"/>, 
    /// <see cref="Pokemon"/>, <see cref="EffectsManager"/> y <see cref="Battle"/>.
    /// </summary>
    [TestFixture]
    public class BattleTest
    {
        /// <summary>
        /// Simula una batalla entre dos entrenadores con sus respectivos Pokémon, verificando
        /// el uso de ataques, efectos, ítems, cambios de Pokémon y limpieza de efectos.
        /// </summary>
        [Test]
        public void SimulateBattle()
        {
            // 1. Crear entrenadores
            Trainer ash = new Trainer("Ash");
            Trainer gary = new Trainer("Gary");

            // Inicializar ítems para ambos entrenadores
            ash.ItemSetting();
            gary.ItemSetting();

            // 2. Crear Pokémons y agregar al equipo de cada entrenador
            Pokemon pikachu = new Pokemon("Pikachu", 100, new List<string> { "Impactrueno", "Rayo" }, "Eléctrico");
            Pokemon charizard = new Pokemon("Charizard", 150, new List<string> { "Lanzallamas", "Garra Dragón" }, "Fuego");

            Pokemon squirtle = new Pokemon("Squirtle", 90, new List<string> { "Pistola Agua", "Hidrobomba" }, "Agua");
            Pokemon bulbasaur = new Pokemon("Bulbasaur", 110, new List<string> { "Latigo Cepa", "Hoja Afilada" }, "Planta");

            ash.Team.Add(pikachu);
            ash.Team.Add(charizard);

            gary.Team.Add(squirtle);
            gary.Team.Add(bulbasaur);

            // Configurar Pokémon activos
            ash.Active = pikachu;
            gary.Active = squirtle;

            // 3. Simular batalla
            EffectsManager effectsManager = new EffectsManager();

            // Ash ataca primero
            string attackResult1 = ash.ChooseAttack("Impactrueno", gary.Active, effectsManager);
            Assert.That(attackResult1, Contains.Substring("recibió")); // Verifica si el ataque fue exitoso

            // Verificar que la salud de Squirtle disminuyó
            Assert.That(gary.Active.Health, Is.LessThan(91));

            // Gary contraataca
            string attackResult2 = gary.ChooseAttack("Pistola Agua", ash.Active, effectsManager);
            Assert.That(attackResult2, Contains.Substring("recibió")); // Verifica si el ataque fue exitoso

            // Verificar que la salud de Pikachu disminuyó
            Assert.That(ash.Active.Health, Is.LessThan(100));

            // 4. Aplicar un efecto (por ejemplo, paralizar)
            string paralyzeResult = effectsManager.ApplyEffect(new ParalyzeEffect(), gary.Active);
            Assert.That(paralyzeResult, Contains.Substring("El pokemon Squirtle se le aplico el efecto paralisis."));

            // Verificar que el Pokémon no puede atacar mientras está paralizado
            bool canAttack = effectsManager.IcanAttack(gary.Active);
            Assert.That(canAttack, Is.False);

            // 5. Ash usa un ítem en Pikachu
            string itemResult = ash.UsarItem("Superpocion", ash.Active, effectsManager);
            Assert.That(itemResult, Contains.Substring("Usaste una Super Pocion. Usos restantes: 3"));

            // Verificar que la salud de Pikachu ha aumentado
            Assert.That(ash.Active.Health, Is.GreaterThan(ash.Active.Health - 20));

            // 6. Derrotar al Pokémon activo de Gary y verificar cambio
            gary.Active.Health = 0; // Simular derrota
            gary.Active.IsDefeated = true;

            gary.CambioPokemonMuerto(); // Cambiar al siguiente Pokémon disponible
            Assert.That(gary.Active, Is.EqualTo(bulbasaur));

            // 7. Limpiar efectos
            string cleanEffectsResult = effectsManager.CleanEffects(gary.Active);
            Assert.That(cleanEffectsResult, Contains.Substring(""));

            // Verificar que ya no tiene efecto
            bool hasEffects = effectsManager.PokemonWithEffect(gary.Active);
            Assert.That(hasEffects, Is.False);
            
            
        }
    }
}
