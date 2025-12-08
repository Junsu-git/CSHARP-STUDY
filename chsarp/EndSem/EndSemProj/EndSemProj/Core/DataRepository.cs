using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndSemProj.Core
{
    // 4. 무기 데이터 저장소
    static class DataRepository
    {
        public static Dictionary<string, int> Weapons = new Dictionary<string, int>()
        {
            { "RustySword", 5 }, { "IronSword", 15 }, { "DiamondSword", 30 },
            { "WoodenStaff", 8 }, { "MagicStaff", 20 }, { "LegendStaff", 45 }
        };
    }
}
