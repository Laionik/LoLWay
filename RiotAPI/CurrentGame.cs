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
    static public class CurrentGame
    {
        static public Models.CurrentGame.CurrentMatchModel GetCurrentGame(String apiKey, int summonerID, String server, String platformID)
        {
            #region request part
            var httpRequest = new StringBuilder();
            
            httpRequest.Append("https://").Append(server).Append(".api.pvp.net/observer-mode/rest/consumer/getSpectatorGameInfo/");
            httpRequest.Append(platformID).Append("/").Append(summonerID).Append("?api_key=").Append(apiKey);

            var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
            request.Method = "GET";
            var response = request.GetResponse() as HttpWebResponse;
            #endregion

            #region deserialize region
            var responseModel = JsonConvert.DeserializeObject<Models.CurrentGame.CurrentMatchModel>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());


            #endregion
            return responseModel;
        }
    }
}
