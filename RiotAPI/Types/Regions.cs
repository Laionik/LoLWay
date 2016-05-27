using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Types
{
    static public class Regions
    {
        static public Dictionary<string, string> regionsDic = new Dictionary<string, string>()
        {
            { "EUNE", "EUN1" },
            { "EUW", "EUW1" },
            { "NA", "NA1" },
            { "BR", "BR1" },
            { "JP", "JP1" },
            { "KR", "KR" },
            { "OCE", "OC1" },
            { "RU", "RU1" },
            { "LAN", "LA1" },
            { "LAS", "LA2" },
            { "TR", "TR1" },
        };
    }
}
