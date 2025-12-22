using System.Reflection.Metadata.Ecma335;

namespace FindGuitarTest
{
    internal class Guitar
    {
        private string serialNumber, model;
        private double price;
        GuitarSpec guitarSpec;

        public Guitar(string serialNumber, Builder builder, string model, Type type, Wood topWood, Wood backWood, double price)
        {
            this.serialNumber = serialNumber;
            this.builder = builder;
            this.guitarSpec.
            this.model = model;
            this.type = type;
            this.backWood = backWood;
            this.topWood = topWood;
            this.price = price;
        }

        public string GetSerialNumber() { return serialNumber; }
        public double GetPrice() { return price; }
        public void SetPrice(double newPrice) { price = newPrice; }
        
        public GuitarSpec GetGuitarSpec() { return guitarSpec; }
    }
}