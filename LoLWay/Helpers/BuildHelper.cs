using LoLWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoLWay.Helpers
{
   static public class BuildHelper
    {
        static public void itemsAdd(ref build builds, IEnumerable<int> items, lolwayEntities db)
        {
            foreach (var item in items)
            {
                builds.item.Add(db.item.Find(item));
            }
        }

        static public void runesAdd(ref build builds, IEnumerable<int> runes, lolwayEntities db)
        {
            foreach (var rune in runes)
            {
                builds.rune.Add(db.rune.Find(rune));
            }
        }
    }
}