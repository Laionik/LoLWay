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
        static public Models.CurrentGame.CurrentMatchModel GetCurrentGame(String apiKey, int summonerID, String server)
        {
            #region request part
            String platformID = Helpers.ServerHelper.GetPlatformId(server);
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

        static public Models.CurrentGame.CurrentMatchModel GetCurrentGameBySummonerName(String apiKey, String summonerName, String server)
        {
            try
            {
                var summoner = Summoner.GetSummonerByName(apiKey, summonerName, server);
                return GetCurrentGame(apiKey, summoner.id, server);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
