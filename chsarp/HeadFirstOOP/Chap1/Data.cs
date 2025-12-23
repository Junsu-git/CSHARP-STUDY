namespace FindGuitarTest // java의 enum 대용
{
    public class Builder
    {
        public string Name { get; }
        private Builder(string name) { Name = name; }
        public static readonly Builder Fender = new Builder("Fender");
        public static readonly Builder Martin = new Builder("Martin");
        public static readonly Builder Gibson = new Builder("Gibson");
        public static readonly Builder Collings = new Builder("Collings");
        public static readonly Builder Olson = new Builder("Olson");
        public static readonly Builder Ryan = new Builder("Ryan");
        public static readonly Builder PRS = new Builder("PRS");

        public new string ToString() => Name;
    }
    public class Type
    {
        public string Name { get; }
        private Type(string name) { Name = name; }

        public static readonly Type Acoustic = new Type("acoustic");
        public static readonly Type Electric = new Type("electric");

        public new string ToString() => Name;
    }
    public class Wood
    {
        public string Name { get; }
        private Wood(string name) { Name = name; }

        public static readonly Wood IndianRosewood = new Wood("Indian Rosewood");
        public static readonly Wood BrazilianRosewood = new Wood("Brazilian Rosewood");
        public static readonly Wood Mahogany = new Wood("Mahogany");
        public static readonly Wood Maple = new Wood("Maple");
        public static readonly Wood Cocobolo = new Wood("Cocobolo");
        public static readonly Wood Cedar = new Wood("Cedar");
        public static readonly Wood Adirondack = new Wood("Adirondack");
        public static readonly Wood Alder = new Wood("Alder");
        public static readonly Wood Sitka = new Wood("Sitka");

        public new string ToString() => Name;
    }

    public class NumString
    {
        public string Name { get; }
        private NumString(string name) { Name = name; }
        public static readonly NumString Six = new NumString("6");
        public static readonly NumString Twelve = new NumString("12");
        public new string ToString() => Name;
    }
}