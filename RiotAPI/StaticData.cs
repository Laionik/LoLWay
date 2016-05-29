using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI
{
    static public class StaticData
    {
        static private readonly Random rnd = new Random();
        static public Models.StaticData.ChampionList GetAllChampionData(String apiKey, String server)
        {
            try
            {
                var stringRequest = new StringBuilder();
                stringRequest.Append("https://global.api.pvp.net/api/lol/static-data/").Append(server).Append("/v1.2/champion?api_key=").Append(apiKey);

                var request = WebRequest.Create(stringRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;

                var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();

                var list = JsonConvert.DeserializeObject<Models.StaticData.ChampionList>(reader);

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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
