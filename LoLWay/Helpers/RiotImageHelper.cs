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
        /// <param name="whishList">List of whislist</param>
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
        /// <param name="whishList">whishlist element</param>
        /// <returns>Updated WhishList</returns>
        public static whishlist GetChampionImages(whishlist whish)
        {
            whish.champion.image = GetImageUrl("champion", whish.champion.image);
            return whish;
        }

        /// <summary>
        /// Update champions image link
        /// </summary>
        /// <param name="buildList">List of builds</param>
        /// <returns>Updated BuildList</returns>
        public static List<build> GetChampionImages(List<build> buildList)
        {
            foreach (var champion in buildList)
            {
                champion.champion.image = GetImageUrl("champion", champion.champion.image);
            }
            return buildList;
        }

        /// <summary>
        /// Update champions image link
        /// </summary>
        /// <param name="buildItem">Build</param>
        /// <returns>Updated buildItem</returns>
        public static build GetChampionImages(build buildItem)
        {
            buildItem.champion.image = GetImageUrl("champion", buildItem.champion.image);
            return buildItem;
        }

        /// <summary>
        /// Update champion image link
        /// </summary>
        /// <param name="championItem">Champion</param>
        /// <returns>Updated championItem</returns>
        public static champion GetChampionImages(champion championItem)
        {
            championItem.image = GetImageUrl("champion", championItem.image);
            return championItem;
        }

        /// <summary>
        /// Update mastery image link
        /// </summary>
        /// <param name="masteryItem">Mastery</param>
        /// <returns>Updated buildItem</returns>
        public static mastery GetMasteryImages(mastery masteryItem)
        {
            masteryItem.image = GetImageUrl("mastery", masteryItem.image);
            return masteryItem;
        }

        /// <summary>
        /// Update items image link
        /// </summary>
        /// <param name="itemList">List of items</param>
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
        /// Update item image link
        /// </summary>
        /// <param name="itemList">item</param>
        /// <returns>Updated item</returns>
        public static item GetItemsImages(item itemObject)
        {
            itemObject.image = GetImageUrl("item", itemObject.image);
            return itemObject;
        }

        /// <summary>
        /// Update runes image link
        /// </summary>
        /// <param name="runeList">List of runes</param>
        /// <returns>Updated runeList</returns>
        public static List<rune> GetRunesImages(List<rune> runeList)
        {
            foreach (var runeObject in runeList)
            {
                runeObject.image = GetImageUrl("rune", runeObject.image);
            }
            return runeList;
        }

        /// <summary>
        /// Update runes image link
        /// </summary>
        /// <param name="runeItem">runeItem</param>
        /// <returns>Updated runeItem</returns>
        public static rune GetRunesImages(rune runeItem)
        {
            runeItem.image = GetImageUrl("rune", runeItem.image);
            return runeItem;
        }
    }
}