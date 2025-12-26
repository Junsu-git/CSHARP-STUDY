namespace DogDoorSimulator
{
    internal class BarkRecognizer
    {
        private DogDoor door;
        public BarkRecognizer(DogDoor door)
        {
            this.door = door;
        }
        public void Recognize(Barking iDogSound)
        {
            iDogSound.Equals();
            Console.WriteLine($"BarkRecognizer: Heard a \"{bark}\"");
            door.Open();
        }
    }
}
