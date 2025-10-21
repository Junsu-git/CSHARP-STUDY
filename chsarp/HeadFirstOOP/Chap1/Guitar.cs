namespace Chap1
{
    internal class Guitar
    {
        private string serialNumber, builder, model, type, backWood, topWood;
        private double price;

        public Guitar(string serialNumber, string builder, string model, string type, string backWood, string topWood, double price)
        {
            this.serialNumber = serialNumber;
            this.builder = builder;
            this.model = model;
            this.type = type;
            this.backWood = backWood;
            this.topWood = topWood;
            this.price = price;
        }
        public string GetSerialNumber() { return serialNumber; }
        public double GetPrice() { return price; }
        public void SetPrice(double newPrice) { price = newPrice; }
        public string GetBuilder() { return builder; }
        public string GetModel() { return model; }
        public new string GetType() { return type; }
        public string GetBackWood() { return backWood; }
        public string GetTopWood() { return topWood; }
    }
}