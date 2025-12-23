namespace FindGuitarTest
{
    internal class Guitar
    {
        private string serialNumber;
        private double price;
        GuitarSpec guitarSpec;

        public Guitar(string serialNumber, Builder builder, string model, Type type, 
            Wood topWood, Wood backWood, NumString numString, double price)
        {
            this.serialNumber = serialNumber;
            guitarSpec = new GuitarSpec(builder, model, type, backWood, topWood, numString);
            this.price = price;
        }

        public Guitar(string serialNumber, GuitarSpec guitarSpec, double price)
        {
            this.serialNumber = serialNumber;
            this.guitarSpec = guitarSpec;
            this.price = price;
        }

        public string GetSerialNumber() { return serialNumber; }
        public double GetPrice() { return price; }
        public void SetPrice(double newPrice) { price = newPrice; }
        public GuitarSpec GetGuitarSpec() { return guitarSpec; }
    }
}