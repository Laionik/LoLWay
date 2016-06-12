using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
}