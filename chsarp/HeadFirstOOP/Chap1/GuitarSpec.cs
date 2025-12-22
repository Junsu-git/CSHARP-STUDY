using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace FindGuitarTest
{
    internal class GuitarSpec
    {
        Builder builder;
        string model;
        Type type;
        Wood backWppd;
        Wood topWood;
        public Builder GetBuilder() { return builder; }
        public string GetModel() { return model; }
        public new Type GetType() { return type; }
        public Wood GetBackWood() { return backWppd; }
        public Wood GetTopWood() { return topWood; }
    }
}
