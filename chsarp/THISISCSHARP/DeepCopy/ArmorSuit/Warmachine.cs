namespace Chap7.Overriding
{
    internal class Warmachine : ArmorSuite
    {
        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Double-Barrel Cannons Armed");
            Console.WriteLine("Micro-Rocked Launcher Armed");
        }
    }
}