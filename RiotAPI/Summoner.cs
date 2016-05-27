using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using RiotAPI.Types;
using RiotAPI.Models.Summoner;

namespace RiotAPI
{
    public class Summoner
    {
        static public List<SummonerModel> GetSummonersByName(String apiKey, List<String> summonersName, String server)
        {
            try
            {
                //request part
                var request = WebRequest.Create(CreateHttpRequest(apiKey, summonersName, server, ApiStrings.GetSummonerByName)) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;

                //deserialize response to summoner list
                var model = JsonConvert.DeserializeObject<Dictionary<String, SummonerModel>>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());
                var summonerList = new List<SummonerModel>();
                foreach (var item in model)
                {
                    summonerList.Add(item.Value);
                }

                return summonerList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static public List<SummonerModel> GetSummonersByID(String apiKey, List<String> summonersIDs, String server)
        {
            try
            {
                //request part
                var request = WebRequest.Create(CreateHttpRequest(apiKey, summonersIDs, server, ApiStrings.GetSummonerByID)) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;

                //deserialize response to summoner list
                var model = JsonConvert.DeserializeObject<Dictionary<String, SummonerModel>>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());
                var summonerList = new List<SummonerModel>();
                foreach (var item in model)
                {
                    summonerList.Add(item.Value);
                }

                return summonerList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static public List<SummonerMysteryPagesModel> GetSummonersMasteryPages(String apiKey, List<String> summonersIds, String server)
        {
            try
            {
                #region request region
                var httpRequest = new StringBuilder();
                //server part
                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server).Append("/v1.4/summoner/");
                //summoner part
                foreach (var param in summonersIds)
                {
                    httpRequest.Append(param).Append(", ");
                }
                httpRequest.Remove(httpRequest.Length - 2, 2);
                //api part
                httpRequest.Append("/masteries?api_key=").Append(apiKey);

                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                #region deserialize region
                var responseString = JsonConvert.DeserializeObject<Dictionary<int, object>>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());

                var summonersList = new List<SummonerMysteryPagesModel>();
                foreach (var item in responseString)
                {
                    var i = JsonConvert.DeserializeObject<SummonerMysteryPagesModel>(item.Value.ToString());
                    summonersList.Add(i);
                }
                #endregion
                return summonersList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static public List<SummonerRunesPagesModel> GetSummonersRunesPages(String apiKey, List<String> summonersIds, String server)
        {
            try
            {
                #region request part
                var httpRequest = new StringBuilder();
                //server part
                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server).Append("/v1.4/summoner/");
                //summoner part
                foreach (var param in summonersIds)
                {
                    httpRequest.Append(param).Append(", ");
                }
                httpRequest.Remove(httpRequest.Length - 2, 2);
                //api part
                httpRequest.Append("/runes?api_key=").Append(apiKey);

                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                #region deserialize region
                var responseString = JsonConvert.DeserializeObject<Dictionary<int, object>>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());

                var summonersList = new List<SummonerRunesPagesModel>();
                foreach (var item in responseString)
                {
                    var i = JsonConvert.DeserializeObject<SummonerRunesPagesModel>(item.Value.ToString());
                    summonersList.Add(i);
                }
                #endregion
                return summonersList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static private String CreateHttpRequest(String apiKey, List<String> parameters, String server, String methodPart)
        {
            var httpRequest = new StringBuilder();
            //server part
            httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server);
            //summoner part
            httpRequest.Append(methodPart);
            foreach (var param in parameters)
            {
                httpRequest.Append(param).Append(", ");
            }
            httpRequest.Remove(httpRequest.Length - 2, 2);
            //api part
            httpRequest.Append("?api_key=").Append(apiKey);

            return httpRequest.ToString();
        }

        static public Models.Summoner.SummonerStatsModel GetSummonerStats(String apiKey, String summonerId, String server)
        {
            try
            {
                #region request part
                var httpRequest = new StringBuilder();
                //server part
                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server).Append("/v1.3/stats/by-summoner/");
                httpRequest.Append(summonerId).Append("/ranked?api_key=").Append(apiKey);


                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                #region deserialize region
                var responseModel = JsonConvert.DeserializeObject<Models.Summoner.SummonerStatsModel>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());


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
