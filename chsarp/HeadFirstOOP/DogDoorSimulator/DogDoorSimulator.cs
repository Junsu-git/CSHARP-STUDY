namespace DogDoorSimulator
{
    internal class DogDoorSimulator
    {
        static void Main(string[] args)
        {
            DogDoor door = new DogDoor();
            Remote remote = new Remote(door);
            Thread.Sleep(1000);
            Console.WriteLine("Fido barks to go outside...\n");
            remote.PressButton();
            Thread.Sleep(1000);
            Console.WriteLine("FIdo has gone outside...");
            Thread.Sleep(1000);
            Console.WriteLine("Fido's all done...");
            Thread.Sleep(10000);
            Console.WriteLine("... but he's stuch outside!");
            Thread.Sleep(1000);
            Console.WriteLine("\nFido starts barking...");
            Thread.Sleep(1000);
            Console.WriteLine("... So Gina grabs the remote control.");
            remote.PressButton();
            Thread.Sleep(1000);
            Console.WriteLine("FIdo's back inside...");
            Thread.Sleep(1000);
            Console.ReadLine();
        }
    }
}
