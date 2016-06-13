using System;
using System.Collections.Generic;
using LoLWay.Helpers;
using System.Web.Mvc;
using MoreLinq;

namespace LoLWay.Models
{
    public class BuildModels
    {
        public SelectList elements { get; set;}
        public IEnumerable<int> selectedElements { get; set; }

        public BuildModels(SelectList elements)
        {
            this.elements = elements;
        }

        public BuildModels(SelectList elements, IEnumerable<int> selectedElements)
        {
            this.elements = elements;
            this.selectedElements = selectedElements;
        }
    }

    public class BuildDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime modificationDate { get; set; }
        public string notes { get; set; }
        public mastery mastery { get; set; }
        public champion champion { get; set; }
        public List<rune> runeList { get; set; }
        public List<item> itemList { get; set; }


        public BuildDetails(build buildItem)
        {
            id = buildItem.id;
            name = buildItem.champion.name;
            modificationDate = buildItem.modificationDate;
            notes = buildItem.notes;
            mastery = RiotImageHelper.GetMasteryImages(buildItem.mastery);
            champion = RiotImageHelper.GetChampionImages(buildItem.champion);
            lolwayEntities db = new lolwayEntities();
            runeList = new List<rune>();
            itemList = new List<item>();
            foreach (var runes in buildItem.runes.Split( new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                runeList.Add(BuildHelper.GetRune(int.Parse(runes), db));
            }
            foreach (var items in buildItem.items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                itemList.Add(BuildHelper.GetItem(int.Parse(items), db));
            }
        }
    }
}