namespace FindGuitarTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            Inventory inventory = Init();
            GuitarSpec whatErinLikes = new GuitarSpec(Builder.Fender, "stratocastor", Type.Electric, Wood.Alder, Wood.Alder, NumString.Six);
            List<Guitar> matchingList = inventory.Search(whatErinLikes);
            if (matchingList.Count != 0)
            {
                Console.WriteLine("Erin, you might like these guitars: \n");
                foreach (Guitar guitar in matchingList)
                {
                    GuitarSpec showingGuitar = guitar.GetGuitarSpec();
                    Console.WriteLine(
                        $"  We have a{showingGuitar.GetBuilder().ToString()}" +
                        $" {showingGuitar.GetModel()} {showingGuitar.GetType().ToString()} guitar:\n    " +
                        $"{showingGuitar.GetBackWood().ToString()} back and slides,\n    " +
                        $"{showingGuitar.GetTopWood().ToString()} top\n    " + 
                        $"{showingGuitar.GetNumString().ToString()} Strings");
                    Console.WriteLine($"\n  You can have it for only ${guitar.GetPrice()}\n  -----");
                }
            }
            else
            {
                Console.WriteLine("Sorry, Erin, we have nothing for you.");

            }
        }

        private static Inventory Init()
        {
            Guitar[] guitars = new Guitar[10];
            guitars[0] = new Guitar("11277", Builder.Gibson, "CJ", Type.Acoustic, Wood.IndianRosewood, Wood.Sitka, NumString.Twelve, 3999.95);
            guitars[1] = new Guitar("V95693", Builder.Fender, "Stratocastor", Type.Electric, Wood.Alder, Wood.Alder, NumString.Six, 1499.95);
            guitars[2] = new Guitar("V9512", Builder.Fender, "Stratocastor", Type.Electric, Wood.Alder, Wood.Alder, NumString.Twelve, 1549.95);
            guitars[3] = new Guitar("122784", Builder.Martin, "D-18", Type.Acoustic, Wood.Mahogany, Wood.Adirondack, NumString.Twelve, 5495.95);
            guitars[4] = new Guitar("76531", Builder.Gibson, "Les Paul", Type.Electric, Wood.Mahogany, Wood.Maple, NumString.Six, 2295.95);
            guitars[5] = new Guitar("70108276", Builder.Gibson, "SG '61 Reissue", Type.Electric, Wood.Mahogany, Wood.Mahogany, NumString.Six, 1890.95);
            guitars[6] = new Guitar("82765501", Builder.Martin, "OM-28", Type.Acoustic, Wood.BrazilianRosewood, Wood.Adirondack, NumString.Six, 6275.95);
            guitars[7] = new Guitar("77023", Builder.Martin, "D-28", Type.Acoustic, Wood.BrazilianRosewood, Wood.Adirondack, NumString.Twelve, 5955.95);
            guitars[8] = new Guitar("1092", Builder.Gibson, "ES-335", Type.Electric, Wood.Mahogany, Wood.Maple, NumString.Six, 1295.95);
            guitars[9] = new Guitar("566-62", Builder.Ryan, "Cathedral", Type.Acoustic, Wood.Cocobolo, Wood.Cedar, NumString.Twelve, 3600.95);
            Inventory inventory = new Inventory();
            InitInven(inventory, guitars);
            return inventory;
        }

        private static void InitInven(Inventory inventory, Guitar[] guitars)
        {
            foreach (Guitar guitar in guitars)
            {
                inventory.AddGuitar(guitar);
            }
        }
    }
}
