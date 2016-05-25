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
    static public class Champion
    {
        static public List<Models.Champion.ChampionModel> GetChempionList(String apiKey, String server, bool? freeToPlay)
        {
            try
            {
                #region request part
                var httpRequest = new StringBuilder();
                //server part
                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server);
                httpRequest.Append("/v1.2/champion?freeToPlay=").Append(freeToPlay).Append("&api_key=").Append(apiKey);
               

                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                #region deserialize region
                var responseModel = JsonConvert.DeserializeObject<Dictionary<String,List<Models.Champion.ChampionModel>>>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());

                
                #endregion
                return responseModel.First().Value;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        static public Models.Champion.ChampionModel GetChampion(String apiKey, String server, int id)
        {
            try
            {
                #region request part
                var httpRequest = new StringBuilder();
                //server part
                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server);
                httpRequest.Append("/v1.2/champion/").Append(id.ToString()).Append("?api_key=").Append(apiKey);


                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                #region deserialize region
                var responseModel = JsonConvert.DeserializeObject<Models.Champion.ChampionModel>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());


                #endregion
                return responseModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
