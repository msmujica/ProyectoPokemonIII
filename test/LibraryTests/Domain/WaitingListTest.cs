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
            
            Assert.That(result, Is.True); 
            Assert.That(waitingList.Count, Is.EqualTo(1));
            
        }


        /// <summary> 
        /// Comprueba que no se pueda agregar un entrenador con un nombre duplicado. 
        /// </summary>

        [Test]
        public void AddTrainerShouldReturnFalseWhenDisplayNameIsDuplicate()
        {
            waitingList.AddTrainer("Player1");

            bool result = waitingList.AddTrainer("Player1");

            Assert.That(result, Is.False); 
            Assert.That(waitingList.Count, Is.EqualTo(1));
        }

        /// <summary> 
        /// Valida que se arroje una excepción si el nombre proporcionado es nulo o vacío. 
        /// </summary>

        [Test]
        public void AddTrainerShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => waitingList.AddTrainer(null));
            Assert.Throws<ArgumentException>(() => waitingList.AddTrainer(string.Empty));
            
        }
        
        /// <summary> 
        /// Verifica que se pueda eliminar correctamente a un entrenador existente.
        /// </summary>

        [Test]
        public void RemoveExistingTrainerShouldRemoveTrainer()
        {
            waitingList.AddTrainer("Player1");
            
            bool result = waitingList.RemoveTrainer("Player1");
            
            Assert.That(result, Is.True);
            Assert.That(waitingList.Count, Is.EqualTo(0));
            
        }
        
        /// <summary> 
        /// Comprueba que intentar eliminar un entrenador inexistente retorne false. 
        /// </summary>

        [Test]
        public void RemoveTrainerShouldReturnFalse()
        {
            bool result = waitingList.RemoveTrainer("Player1");
            
            Assert.That(result, Is.False);
            Assert.That(waitingList.Count, Is.EqualTo(0)); 
        }
        
        
        /// <summary> 
        /// Verifica que se pueda encontrar a un entrenador por su nombre si existe en la lista. 
        /// </summary>

        [Test]
        public void SearchTrainerByDisplayNameShouldReturnTrainer()
        {
            waitingList.AddTrainer("Player1");
            
            var trainer = waitingList.FindTrainerByDisplayName("Player1");
            
            Assert.That(trainer, Is.Not.Null);
            Assert.That(trainer?.Name, Is.EqualTo("Player1"));
            
        }
        
        
        /// <summary> 
        /// Comprueba que buscar a un entrenador inexistente retorne null. 
        /// </summary>

        [Test]
        public void SearchTrainerByDisplayNameShouldReturnNull()
        {
            var trainer = waitingList.FindTrainerByDisplayName("Player1");
            
            Assert.That(trainer, Is.Null);
            
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
            
            Assert.That(trainer, Is.Not.Null); 
            Assert.That(trainer?.Name, Is.EqualTo("Player1"));
            
        }
        
        /// <summary> 
        /// Comprueba que intentar obtener un entrenador de una lista vacía retorne null. 
        /// </summary>

        [Test]
        public void GetAnyoneWaitingShouldReturnNull()
        {
            var trainer = waitingList.GetAnyoneWaiting();
            
            Assert.That(trainer, Is.Null);
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
            
            Assert.That(allTrainers.Count, Is.EqualTo(2));
            Assert.That(allTrainers[0].Name, Is.EqualTo("Player1")); 
            Assert.That(allTrainers[1].Name, Is.EqualTo("Player2"));
            
        }
    }
}
