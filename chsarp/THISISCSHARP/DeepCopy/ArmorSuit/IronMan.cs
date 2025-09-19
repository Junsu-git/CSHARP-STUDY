using Chap7.Overriding;

namespace Chap7.Overriding
{
    class IronMan : ArmorSuite
    {
        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Repulsor Rays Armed");
        }
    }
}
