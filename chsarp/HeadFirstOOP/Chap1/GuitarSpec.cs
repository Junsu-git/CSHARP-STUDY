namespace FindGuitarTest
{
    internal class GuitarSpec
    {
        public Builder builder;
        public string model;
        public Type type;
        public Wood backWood;
        public Wood topWood;
        public NumString numString;
        public GuitarSpec(Builder builder, string model, Type type, Wood backWood, Wood topWood, NumString numString)
        {
            this.builder = builder;
            this.model = model;
            this.type = type;
            this.backWood = backWood;
            this.topWood = topWood;
            this.numString = numString;
        }
        public Builder GetBuilder() { return builder; }
        public string GetModel() { return model; }
        public new Type GetType() { return type; }
        public Wood GetBackWood() { return backWood; }
        public Wood GetTopWood() { return topWood; }
        public NumString GetNumString() { return numString; }

        public bool IsMatching(GuitarSpec findingSpec)
        {
            Builder builder = findingSpec.GetBuilder();
            if ((builder != null) && (!builder.Equals(this.GetBuilder())))
                return false;
            string model = findingSpec.GetModel().ToLower();
            if ((model != null) && (!model.Equals(this.GetModel().ToLower())))
                return false;
            Type type = findingSpec.GetType();
            if ((type != null) && (!type.Equals(this.GetType())))
                return false;
            Wood backWood = findingSpec.GetBackWood();
            if ((backWood != null) && (!backWood.Equals(this.GetBackWood())))
                return false;
            Wood topWood = findingSpec.GetTopWood();
            if ((topWood != null) && (!topWood.Equals(this.GetTopWood())))
                return false;
            NumString numString = findingSpec.GetNumString();
            if ((numString != null) && (!numString.Equals(this.GetNumString())))
                return false;
            return true;
        }
    }
}
