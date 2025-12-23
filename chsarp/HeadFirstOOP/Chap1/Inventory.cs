namespace FindGuitarTest
{
    internal class Inventory
    {
        private List<Guitar> guitars;
        public Inventory()
        {
            guitars = new List<Guitar>();
        }

        public void AddGuitar(Guitar iGuitar)
        {
            guitars.Add(iGuitar);
        }

        public List<Guitar> Search(GuitarSpec searchingSpec)
        {
            List<Guitar> matchingGuitars = new List<Guitar>();
            foreach (Guitar guitar in guitars)
            {
                GuitarSpec InventorySpec = guitar.GetGuitarSpec();
                if( InventorySpec.IsMatching(searchingSpec))
                    matchingGuitars.Add(guitar);
            }
            return matchingGuitars;
        }
    }
}