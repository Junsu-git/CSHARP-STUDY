namespace DogDoorSimulator
{
    internal class DogDoorSimulator
    {
        static void Main(string[] args)
        {
            DogDoor door = new DogDoor();
            BarkRecognizer Recoder = new BarkRecognizer(door);
            Remote remote = new Remote(door);
            
            // 강아지 소리를 듣는 하드웨어 시뮬레이션
            Thread.Sleep(1000);
            Console.WriteLine("Fido barks to go outside...\n");
            Thread.Sleep(1000);
            Recoder.Recognize("Woof");
            Thread.Sleep(1000);
            Console.WriteLine("FIdo has gone outside...\n");
            Thread.Sleep(1000);
            Console.WriteLine("Fido's all done...\n");
            Thread.Sleep(10000);
            Console.WriteLine("... but he's stuck outside!\n");
            Thread.Sleep(1000);
            Console.WriteLine("Fido Starts barking.\n");
            Recoder.Recognize("Woof");
            Console.WriteLine("FIdo's back inside...\n");
            Console.ReadLine();

            // 직접 버튼을 누르는 시뮬레이션
            //remote.PressButton();
            //Thread.Sleep(1000);
            //Console.WriteLine("FIdo has gone outside...");
            //Thread.Sleep(1000);
            //Console.WriteLine("Fido's all done...");
            //Thread.Sleep(10000);
            //Console.WriteLine("... but he's stuch outside!");
            //Thread.Sleep(1000);
            //Console.WriteLine("\nFido starts barking...");
            //Thread.Sleep(1000);
            //Console.WriteLine("... So Gina grabs the remote control.");
            //remote.PressButton();
            //Thread.Sleep(1000);
            //Console.WriteLine("FIdo's back inside...");
            //Thread.Sleep(1000);
        }
    }
}
