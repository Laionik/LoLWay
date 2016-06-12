using LoLWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoLWay.Helpers
{
    public static class RiotImageHelper
    {
        /// <summary>
        /// Create image url
        /// </summary>
        /// <param name="itemType">Type of item to update</param>
        /// <param name="image">Image name</param>
        /// <returns>Image url</returns>
        public static string GetImageUrl(string itemType, string image)
        {
            //get version
            string version = "6.12.1";
            switch (itemType)
            {
                case "champion":
                    return "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/champion/" + image;
                case "mastery":
                    return "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/mastery/" + image;
                case "spell":
                    return "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/spell/" + image;
                case "rune":
                    return "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/rune/" + image;
                case "item":
                    return "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/item/" + image;
                default:
                    return "";
            }
        }

        /// <summary>
        /// Update champions image link
        /// </summary>
        /// <param name="whishList">WhishList with included champions</param>
        /// <returns>Updated WhishList</returns>
        public static List<whishlist> GetChampionImages(List<whishlist> whishList)
        {
            foreach (var champion in whishList)
            {
                champion.champion.image = GetImageUrl("champion", champion.champion.image);
            }
            return whishList;
        }

        /// <summary>
        /// Update champions image link
        /// </summary>
        /// <param name="buildList">BuildList with included champions</param>
        /// <returns>Updated BuildList</returns>
        public static List<builds> GetChampionImages(List<builds> buildList)
        {
            foreach (var champion in buildList)
            {
                champion.champion.image = GetImageUrl("champion", champion.champion.image);
            }
            return buildList;
        }

        /// <summary>
        /// Update items image link
        /// </summary>
        /// <param name="itemList">itemList with included champions</param>
        /// <returns>Updated itemList</returns>
        public static List<item> GetItemsImages(List<item> itemList)
        {
            foreach (var item in itemList)
            {
                item.image = GetImageUrl("item", item.image);
            }
            return itemList;
        }

        /// <summary>
        /// Update runes image link
        /// </summary>
        /// <param name="runeList">runeList with included champions</param>
        /// <returns>Updated runeList</returns>
        public static List<rune> GetRunesImages(List<rune> runeList)
        {
            foreach (var runeObject in runeList)
            {
                runeObject.image = GetImageUrl("rune", runeObject.image);
            }
            return runeList;
        }
    }
}