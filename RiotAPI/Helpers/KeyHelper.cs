using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Helpers
{
    static public class KeyHelper
    {
        static private readonly Random rnd = new Random();
        static public String GetApiKey()
        {
            try
            {
                return Types.ApiStrings.apiKeys[rnd.Next(0, Types.ApiStrings.apiKeys.Count - 1)];
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
