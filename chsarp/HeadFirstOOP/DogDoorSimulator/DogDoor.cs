using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogDoorSimulator
{
    internal class DogDoor
    {
        private bool open;
        public DogDoor()
        {
            this.open = false;
        }
        public void Open()
        {
            Console.WriteLine("The dog door opens.");
            open = true;
            _ = AutoCloseAsync(5000);
        }

        public void close()
        {
            Console.WriteLine("The dog door closes.");
            open = false;
        }
        public bool IsOpen()
        {
            return open;
        }

        private async Task AutoCloseAsync(int delayMs)
        {
            await Task.Delay(delayMs);
            if (IsOpen()) close();
        }   
    }
}
