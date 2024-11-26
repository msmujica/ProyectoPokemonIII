using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests.Domain
{
    [TestFixture]
    [TestOf(typeof(WaitingList))]
    public class WaitingListTest
    {
        private WaitingList waitingList;

        /// <summary>
        /// Verifica que la WaitingList se inicializa correctamente.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            waitingList = new WaitingList();
        }
        
        /// <summary> 
        /// Verifica que un entrenador se agregue correctamente si el nombre es válido.
        /// </summary>

        [Test]
        public void AddTrainerShouldAddTrainerWhenDisplayNameIsValid()
        {
            bool result = waitingList.AddTrainer("Player1");
            
            Assert.That(result, Is.True); // Debería haberse agregado correctamente.
            Assert.That(waitingList.Count, Is.EqualTo(1)); // La lista debería tener un solo entrenador.
        }
        
        
        /// <summary> 
        /// Comprueba que no se pueda agregar un entrenador con un nombre duplicado. 
        /// </summary>

        [Test]
        public void AddTrainerShouldReturnFalseWhenDisplayNameIsDuplicate()
        {
            waitingList.AddTrainer("Player1");
            
            bool result = waitingList.AddTrainer("Player1");
            
            Assert.That(result, Is.False); // Debería retornar false al ser un nombre duplicado.
            Assert.That(waitingList.Count, Is.EqualTo(1)); // La lista debería seguir teniendo un solo entrenador.
        }
        
        /// <summary> 
        /// Valida que se arroje una excepción si el nombre proporcionado es nulo o vacío. 
        /// </summary>

        [Test]
        public void AddTrainerShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => waitingList.AddTrainer(null)); // Debería lanzar una excepción si es null.
            Assert.Throws<ArgumentException>(() => waitingList.AddTrainer(string.Empty)); // Debería lanzar una excepción si es vacío.
        }
        
        /// <summary> 
        /// Verifica que se pueda eliminar correctamente a un entrenador existente.
        /// </summary>

        [Test]
        public void RemoveExistingTrainerShouldRemoveTrainer()
        {
            waitingList.AddTrainer("Player1");
            
            bool result = waitingList.RemoveTrainer("Player1");
            
            Assert.That(result, Is.True); // El entrenador debería haberse eliminado correctamente.
            Assert.That(waitingList.Count, Is.EqualTo(0)); // La lista debería estar vacía.
        }
        
        /// <summary> 
        /// Comprueba que intentar eliminar un entrenador inexistente retorne false. 
        /// </summary>

        [Test]
        public void RemoveTrainerShouldReturnFalse()
        {
            bool result = waitingList.RemoveTrainer("Player1");
            
            Assert.That(result, Is.False); // Debería retornar false si el jugador no existe.
            Assert.That(waitingList.Count, Is.EqualTo(0)); // La lista sigue vacía.
        }
        
        
        /// <summary> 
        /// Verifica que se pueda encontrar a un entrenador por su nombre si existe en la lista. 
        /// </summary>

        [Test]
        public void SearchTrainerByDisplayNameShouldReturnTrainer()
        {
            waitingList.AddTrainer("Player1");
            
            var trainer = waitingList.FindTrainerByDisplayName("Player1");
            
            Assert.That(trainer, Is.Not.Null); // El entrenador debería ser encontrado.
            Assert.That(trainer?.Name, Is.EqualTo("Player1")); // El nombre del entrenador debería coincidir.
        }
        
        
        /// <summary> 
        /// Comprueba que buscar a un entrenador inexistente retorne null. 
        /// </summary>

        [Test]
        public void SearchTrainerByDisplayNameShouldReturnNull()
        {
            var trainer = waitingList.FindTrainerByDisplayName("Player1");
            
            Assert.That(trainer, Is.Null); // No debería encontrar al entrenador, ya que no está en la lista.
        }
        
        /// <summary> 
        /// Valida que se obtenga el primer entrenador de la lista cuando hay entrenadores esperando. 
        /// </summary>

        [Test]
        public void GetAnyoneWaitingShouldReturnFirstTrainer()
        {
            waitingList.AddTrainer("Player1");
            waitingList.AddTrainer("Player2");
            
            var trainer = waitingList.GetAnyoneWaiting();
            
            Assert.That(trainer, Is.Not.Null); // Debería retornar un entrenador.
            Assert.That(trainer?.Name, Is.EqualTo("Player1")); // El primer entrenador debería ser "Player1".
        }
        
        /// <summary> 
        /// Comprueba que intentar obtener un entrenador de una lista vacía retorne null. 
        /// </summary>

        [Test]
        public void GetAnyoneWaitingShouldReturnNull()
        {
            var trainer = waitingList.GetAnyoneWaiting();
            
            Assert.That(trainer, Is.Null); // Debería retornar null si no hay entrenadores en la lista.
        }
        
        /// <summary> 
        /// Verifica que se puedan obtener todos los entrenadores en espera. 
        /// </summary>

        [Test]
        public void GetAllWaitingShouldReturnAllTrainers()
        {
            waitingList.AddTrainer("Player1");
            waitingList.AddTrainer("Player2");
            
            var allTrainers = waitingList.GetAllWaiting();
            
            Assert.That(allTrainers.Count, Is.EqualTo(2)); // Debería haber dos entrenadores en la lista.
            Assert.That(allTrainers[0].Name, Is.EqualTo("Player1")); // El primer entrenador debería ser "Player1".
            Assert.That(allTrainers[1].Name, Is.EqualTo("Player2")); // El segundo entrenador debería ser "Player2".
        }
    }
}
