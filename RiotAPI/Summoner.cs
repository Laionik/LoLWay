using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using RiotAPI.Types;
using RiotAPI.Models.Summoner;
using System.Linq;

namespace RiotAPI
{
    public class Summoner
    {
        static public List<SummonerModel> GetSummonersByName(List<String> summonersName, String server)
        {
            try
            {
                //request part
                var request = WebRequest.Create(CreateHttpRequest(summonersName, server, ApiStrings.GetSummonerByName)) as WebRequest;
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

        static public SummonerModel GetSummonerByName(String summonerName, String server)
        {
            try
            {
                return GetSummonersByName(new List<String> { summonerName }, server)[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static public List<SummonerModel> GetSummonersByID(List<String> summonersIDs, String server)
        {
            try
            {
                //request part
                var request = WebRequest.Create(CreateHttpRequest(summonersIDs, server, ApiStrings.GetSummonerByID)) as WebRequest;
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

        static public SummonerModel GetSummonerByID(String summonerID, String server)
        {
            return GetSummonersByID(new List<String> { summonerID }, server)[0];
        }

        static public List<SummonerMysteryPagesModel> GetSummonersMasteryPages(List<String> summonersIds, String server)
        {
            try
            {
                var apiKey = Helpers.KeyHelper.GetApiKey();
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

        static public SummonerMysteryPagesModel GetSummonerMysteryPages(String summonerId, String server)
        {
            try
            {
                return GetSummonersMasteryPages(new List<String> { summonerId }, server)[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static public List<SummonerRunesPagesModel> GetSummonersRunesPages(List<String> summonersIds, String server)
        {
            try
            {
                var apiKey = Helpers.KeyHelper.GetApiKey();
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

        static public SummonerRunesPagesModel GetSummonerRunesPages(String SummonerId, String server)
        {
            try
            {
                return GetSummonersRunesPages(new List<String> { SummonerId }, server)[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static private String CreateHttpRequest(List<String> parameters, String server, String methodPart)
        {
            var apiKey = Helpers.KeyHelper.GetApiKey();
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

        static public Models.Summoner.SummonerStatsModel GetSummonerStats(String summonerId, String server)
        {
            try
            {
                var apiKey = Helpers.KeyHelper.GetApiKey();
                #region request part
                var httpRequest = new StringBuilder();
                //server part
                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server).Append("/v1.3/stats/by-summoner/");
                httpRequest.Append(summonerId).Append("/ranked?season=SEASON2016&api_key=").Append(apiKey);

                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                #region deserialize region
                var responseModel = JsonConvert.DeserializeObject<Models.Summoner.SummonerStatsModel>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());


                #endregion
                return responseModel;
            }
            catch (Exception  e)
            {
                if(e.Message.Contains("404"))
                {
                    var summoner = new SummonerStatsModel();
                    summoner.summonerId = int.Parse(summonerId);
                    return summoner;
                }
                throw e;
            }
        }

        static public Dictionary<int, List<SummonerRankModel>> GetSummonersRankStats(List<String> summonersIds, String server)
        {
            try {
                var apiKey = Helpers.KeyHelper.GetApiKey();
                #region request part
                var httpRequest = new StringBuilder();
                //server part
                httpRequest.Append("https://").Append(server.ToLower()).Append(".api.pvp.net/api/lol/").Append(server.ToLower()).Append("/v2.5/league/by-summoner/");
                //summoner part
                foreach (var param in summonersIds)
                {
                    httpRequest.Append(param).Append(",");
                }
                httpRequest.Remove(httpRequest.Length - 2, 2);
                //api part
                httpRequest.Append("/entry?api_key=").Append(apiKey);

                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                var responseObject = JsonConvert.DeserializeObject<Dictionary<int, List<SummonerRankModel>>>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());

                return responseObject;
            }
            catch (Exception e)
            {
               
                throw e;
            }
        }

        static public SummonerRankModel GetSummonerRankStats(String summonerId, String server)
        {
            try {
                var apiKey = Helpers.KeyHelper.GetApiKey();
                #region request part
                var httpRequest = new StringBuilder();
                //server part
                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server).Append("/v2.5/league/by-summoner/");
                //summoner part
                httpRequest.Append(summonerId);
                //api part
                httpRequest.Append("/entry?api_key=").Append(apiKey);

                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                var responseObject = JsonConvert.DeserializeObject<Dictionary<int, List<SummonerRankModel>>>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());
                var item = new List<SummonerRankModel>();
                var tmp = responseObject.TryGetValue(Convert.ToInt32(summonerId), out item);

                return item.First();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
