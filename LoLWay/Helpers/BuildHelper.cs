using LoLWay.Models;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace LoLWay.Helpers
{
   static public class BuildHelper
    {
        static public void itemsAdd(ref build builds, IEnumerable<int> items)
        {
            StringBuilder sbItems = new StringBuilder();
            foreach (var item in items)
            {
                sbItems.Append(item + ";");
            }
            builds.items = sbItems.ToString();
        }

        static public void runesAdd(ref build builds, IEnumerable<int> runes)
        {
            StringBuilder sbRunes = new StringBuilder();
            foreach (var rune in runes)
            {
                sbRunes.Append(rune + ";");
            }
            builds.runes = sbRunes.ToString();           
        }

        static public rune GetRune(int id, lolwayEntities db)
        {
            var runeItem = db.rune.FirstOrDefault(x => x.id == id);
            return RiotImageHelper.GetRunesImages(runeItem);
        }

        static public item GetItem(int id, lolwayEntities db)
        {
            var itemObject = db.item.FirstOrDefault(x => x.id == id);
            return RiotImageHelper.GetItemsImages(itemObject);
        }
    }
}