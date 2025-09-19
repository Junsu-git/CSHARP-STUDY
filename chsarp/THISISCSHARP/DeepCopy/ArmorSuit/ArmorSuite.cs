namespace Chap7.Overriding
{
    internal class ArmorSuite
    {
        public ArmorSuite()
        {
        }

        public virtual void Initialize()
        {
            Console.WriteLine("Armored");
        }
    }
}