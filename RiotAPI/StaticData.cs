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
        static public Models.StaticData.ChampionList GetAllChampionData(String server)
        {
            try
            {
                var apiKey = Helpers.KeyHelper.GetApiKey();
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
    }
}
