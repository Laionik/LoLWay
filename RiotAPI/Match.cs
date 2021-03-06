﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace RiotAPI
{
    static public class Match
    {
        static public Models.Match.SummonerMatchListModel GetMatchList(String summonerID, String server)
        {
            try
            {
                #region request part
                var apiKey = Helpers.KeyHelper.GetApiKey();
                var httpRequest = new StringBuilder();
                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server).Append("/v2.2/matchlist/by-summoner/");
                httpRequest.Append(summonerID).Append("?api_key=").Append(apiKey);

                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                #region deserialize region
                var responseModel = JsonConvert.DeserializeObject<Models.Match.SummonerMatchListModel>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());


                #endregion
                return responseModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static public Models.Match.SummonerMatchListModel GetMatchListBySummonerName(String summonerName, String server)
        {
            try
            {
                var summoner = Summoner.GetSummonerByName(summonerName, server);

                return GetMatchList(summoner.id.ToString(), server);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static public Models.Match.MatchDetails.MatchDetailsModel GetMatchById(String matchId, String server)
        {
            try
            {
                #region request part
                var apiKey = Helpers.KeyHelper.GetApiKey();
                var httpRequest = new StringBuilder();

                httpRequest.Append("https://").Append(server).Append(".api.pvp.net/api/lol/").Append(server).Append("/v2.2/match/");
                httpRequest.Append(matchId).Append("?api_key=").Append(apiKey);

                var request = WebRequest.Create(httpRequest.ToString()) as WebRequest;
                request.Method = "GET";
                var response = request.GetResponse() as HttpWebResponse;
                #endregion

                #region deserialize region
                var responseModel = JsonConvert.DeserializeObject<Models.Match.MatchDetails.MatchDetailsModel>(new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd());


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
