using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chap7
{
    class Mamal
    {
        public void Nurse()
        {
            Console.WriteLine("Nurse()");
        }
    }
    class Dog: Mamal
    {
        public void Bark()
        {
            Console.WriteLine("Bark()");
        }
    }
    class Cat : Mamal
    {
        public void Meow()
        {
            Console.WriteLine("Meow()");
        }
    }

    public class TypeCasting()
    {
        public static void Run()
        {
            Mamal mamal1 = new Dog();
            Dog dog;
            if(mamal1 is Dog)
            {
                dog = (Dog)mamal1;
                dog.Bark();
            }
            Mamal mamal2 = new Cat();
            
            Cat cat = mamal2 as Cat;
            if (cat != null)
                cat.Meow();

            Cat cat2 = mamal1 as Cat;
            if(cat2 != null) cat2.Meow();
            else
                Console.WriteLine("cat2 is not a Cat");

            Console.WriteLine("===========================");
        }
    }
}
