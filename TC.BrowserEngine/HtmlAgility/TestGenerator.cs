using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace TC.BrowserEngine.HtmlAgility
{
    public class TestGenerator
    {
        private HtmlElement _htmlElementHelper;
        private HtmlDocument _htmlDocument;

        public TestGenerator()
        {
            _htmlDocument = new HtmlDocument();
            _htmlElementHelper = new HtmlElement();
        }
        public void LoadFile(string filePath)
        {
            _htmlDocument.Load(filePath);
        }
        public void LoadHtml(string html)
        {
            _htmlDocument.LoadHtml(html);
        }
        public List<HtmlNode> CollectPossibleRoutingActions()
        {
            // get all links, buttons -submit button or 
            var actionElements = _htmlElementHelper.FindAllElements(_htmlDocument, "a");
            actionElements.AddRange(_htmlElementHelper.FindAllElements(_htmlDocument, "button"));
            //foreach (var item in actionElements)
            //{
            //    if (item.Attributes.AttributesWithName("submit").Any())
            //    {

            //    }
            //}
            return actionElements;
        }


        public List<HtmlNode> CollectPossibleActions()
        {
            // get all links, buttons, inputs 
            var actionElements = _htmlElementHelper.FindAllElements(_htmlDocument, "a");
            actionElements.AddRange(_htmlElementHelper.FindAllElements(_htmlDocument, "button"));
            actionElements.AddRange(_htmlElementHelper.FindAllElements(_htmlDocument, "input"));
            return actionElements;
        }
    }
}
