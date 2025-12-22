namespace supplement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Airplane biplane = new Airplane();
            biplane.SetSpeed(212);
            Console.WriteLine(biplane.GetSpeed());
            Jet boeing = new Jet();
            boeing.SetSpeed(422);
            Console.WriteLine(boeing.GetSpeed());
            int x = 0;
            while(x < 4)
            {
                boeing.Accelerate();
                Console.WriteLine(boeing.GetSpeed());
                if(boeing.GetSpeed() > 5000)
                {
                    biplane.SetSpeed(biplane.GetSpeed() * 2);
                }
                else
                {
                    boeing.Accelerate();
                }
                x++;
            }
            Console.WriteLine(biplane.GetSpeed());
        }

        /* jet1의 속도 : 212   `
         * jet2의 속도 : 424
         * jet 객체의 경우, SetSpeed 메서드를 사용하였을 때, 항상 입력값의 2배의 속도를 내도록 만들었다.
         * 그러나 닫혀있던 speed가 공개되면서, 직접 수정할 수 있게 되었고, 이룰 통해 항상 입력 값의 2배를 갖는다는
         * 의미를 잃게 되었다.
         * 캡슐화의 장점: 사용자가, 직접 수정할 수 있는 부분을 제한하면서, 객체가 다양한 환경에서 동일한 작업을 할 수 있도록함.
         */
    }
}
