
namespace Chap1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Guitar[] guitars = new Guitar[10];
            guitars[0] = new Guitar("11277", "Collings", "CJ", "Acoustic", "Indian Rosewood", "Sitka", 3999.95);
            guitars[1] = new Guitar("V95693", "Fender", "Stratocastor", "Electric", "Alder", "Alder", 1499.95);
            guitars[2] = new Guitar("V9512", "Fender", "Stratocastor", "Electric", "Alder", "Alder", 1549.95);
            guitars[3] = new Guitar("122784", "Martin", "D-18", "Acoustic", "Mahogany", "Adirondack", 5495.95);
            guitars[4] = new Guitar("76531", "Gibson", "Les Paul", "Electric", "Mahogany", "Maple", 2295.95);
            guitars[5] = new Guitar("70108276", "Gibson", "SG '61 Reissue", "Electric", "Mahogany", "Mahogany", 1890.95);
            guitars[6] = new Guitar("82765501", "Martin", "OM-28", "Acoustic", "Brazilian Rosewood", "Adirondack", 6275.95);
            guitars[7] = new Guitar("77023", "Martin", "D-28", "Acoustic", "Brazilian Rosewood", "Adirondack", 5955.95);
            guitars[8] = new Guitar("1092", "Olson", "SJ", "Acoustic", "Cocobolo", "Cedar", 3795.95);
            guitars[9] = new Guitar("566-62", "Ryan", "Cathedral", "Acoustic", "Cocobolo", "Cedar", 3600.95);
            Inventory inventory = new Inventory();
            InitializeInventory(inventory, guitars);

            Guitar whatErinLikes = new Guitar("", "fender", "Stratocastor", "Electric", "alder", "Alder", 0);
            Guitar guitar = inventory.Search(whatErinLikes);
            if (guitar != null)
            {
                Console.WriteLine("Erin, you might like this " +
                    guitar.GetBuilder() + " " + guitar.GetModel() + " " +
                    guitar.GetType() + " guitar:\n   " +
                    guitar.GetBackWood() + " back and sides,\n   " +
                    guitar.GetTopWood() + " top.\nYou can have it for only $" +
                    guitar.GetPrice() + "!");
            }
            else
            {
                Console.WriteLine("Sorry, Erin, we have nothing for you.");

            }
        }

        private static void InitializeInventory(Inventory inventory, Guitar[] guitars)
        {
            foreach (Guitar guitar in guitars)
            {
                inventory.AddGuitar(guitar.GetSerialNumber(), guitar.GetBuilder(), guitar.GetModel(), guitar.GetType(), guitar.GetBackWood(), guitar.GetTopWood(), guitar.GetPrice());
            }
        }
    }
}
