using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDoorSimulator
{
    internal class Barking
    {
        string sound;
        public Barking(string sound)
        {
            this.sound = sound;
        }
        public string GetSound() => sound;
        public bool IsEqual(object obj)
        {
            if (obj is Barking dogSound)
            {
                return dogSound.GetSound() == this.GetSound();
            }
            return false;
        }
    }
}