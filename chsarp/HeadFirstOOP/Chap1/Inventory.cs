namespace FindGuitarTest
{
    internal class Inventory
    {
        private List<Guitar> guitars;
        public Inventory()
        {
            guitars = new List<Guitar>();
        }

        public void AddGuitar(string serialNumber, Builder builder, string model, Type type, Wood backWood, Wood topWood, double price)
        {
            Guitar guitar = new Guitar(serialNumber, builder, model, type, backWood, topWood, price);
            guitars.Add(guitar);
        }
        public List<Guitar> Search(Guitar searchGuitar)
        {
            List<Guitar> matchingGuitars = new List<Guitar>();
            foreach (Guitar guitar in guitars)
            {
                Builder builder = searchGuitar.GetBuilder();
                if ((builder != null) && (!builder.Equals("")) && (!builder.Equals(guitar.GetBuilder())))
                    continue;
                string model = searchGuitar.GetModel().ToLower();
                if ((model != null) && (!model.Equals("")) && (!model.Equals(guitar.GetModel().ToLower())))
                    continue;
                Type type = searchGuitar.GetType();
                if ((type != null) && (!type.Equals("")) && (!type.Equals(guitar.GetType())))
                    continue;
                Wood backWood = searchGuitar.GetBackWood();
                if ((backWood != null) && (!backWood.Equals("")) && (!backWood.Equals(guitar.GetBackWood())))
                    continue;
                Wood topWood = searchGuitar.GetTopWood();
                if ((topWood != null) && (!topWood.Equals("")) && (!topWood.Equals(guitar.GetTopWood())))
                    continue;
                matchingGuitars.Add(guitar);
            }
            return matchingGuitars;
        }
    }
}