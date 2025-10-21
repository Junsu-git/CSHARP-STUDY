namespace Chap1
{
    internal class Inventory
    {
        private List<Guitar> guitars;
        public Inventory()
        {
            guitars = new List<Guitar>();
        }

        public void AddGuitar(string serialNumber, string builder, string model, string type, string backWood, string topWood, double price)
        {
            Guitar guitar = new Guitar(serialNumber, builder, model, type, backWood, topWood, price);
            guitars.Add(guitar);
        }
        public Guitar Search(Guitar searchGuitar)
        {
            foreach (Guitar guitar in guitars)
            {
                string builder = searchGuitar.GetBuilder();
                if ((builder != null) && (!builder.Equals("")) && (!builder.Equals(guitar.GetBuilder())))
                    continue;
                string model = searchGuitar.GetModel();
                if ((model != null) && (!model.Equals("")) && (!model.Equals(guitar.GetModel())))
                    continue;
                string type = searchGuitar.GetType();
                if ((type != null) && (!type.Equals("")) && (!type.Equals(guitar.GetType())))
                    continue;
                string backWood = searchGuitar.GetBackWood();
                if ((backWood != null) && (!backWood.Equals("")) && (!backWood.Equals(guitar.GetBackWood())))
                    continue;
                string topWood = searchGuitar.GetTopWood();
                if ((topWood != null) && (!topWood.Equals("")) && (!topWood.Equals(guitar.GetTopWood())))
                    continue;
                return guitar;
            }
            return null;
        }
    }
}