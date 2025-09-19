using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chap7
{
    public class InheritanceBase
    {
        protected string name;
        public InheritanceBase(string name)
        {
            this.name = name;
            Console.WriteLine($"{this.name}.base()");

        }
        ~InheritanceBase()
        {
            Console.WriteLine($"{this.name}.~base()");
        }

        public void BaseMethod()
        {
            Console.WriteLine($"{name}.BaseMethod()");
        }
    }

    public class InheritanceDerived : InheritanceBase
    {
        public InheritanceDerived(string name) : base(name)
        {
            Console.WriteLine($"{this.name}.Derived()");
        }
        ~InheritanceDerived() 
        {
            Console.WriteLine($"{this.name}.~Derived()");
        }
        public void DerivedMethod()
        {
            Console.WriteLine($"{name}.DerivedMethod()");
        }
    } 

    public class InheritanceRun
    {
        public static void Run()
        {
            InheritanceBase a = new InheritanceBase("a");
            a.BaseMethod();

            InheritanceDerived b = new InheritanceDerived("b");
            b.BaseMethod();
            b.DerivedMethod();
        }
    }
}
