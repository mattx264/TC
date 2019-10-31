using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;

namespace TC.BrowserEngine.HtmlAgility
{
    public class HtmlElement
    {
        public List<HtmlNode>  FindAllElements(HtmlDocument htmlDocument, string selectorQuery)
        {
           return htmlDocument.DocumentNode.QuerySelectorAll(selectorQuery).ToList();
        }
    }
}
