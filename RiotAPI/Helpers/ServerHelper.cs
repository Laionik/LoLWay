using RiotAPI.Types;
using System.Linq;

namespace RiotAPI.Helpers
{
    class ServerHelper
    {
        /// <summary>
        /// Get platform id by region name
        /// </summary>
        /// <param name="regionName">region name</param>
        /// <returns>platform id</returns>
        public static string GetPlatformId(string regionName)
        {
            return Regions.regionsDic[regionName];
        }

        /// <summary>
        /// Get region name by platform id
        /// </summary>
        /// <param name="platformId">platform id</param>
        /// <returns>region name</returns>
        public static string GetRegionName(string platformId)
        {
            return Regions.regionsDic.FirstOrDefault(x => x.Value == "platformId").Key;
        }
    }
}
