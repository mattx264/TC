using HtmlAgilityPack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TC.BrowserEngine.HtmlAgility;

namespace TC.BrowserEngineTest.TestGenerator
{

    public class HtmlElementTest
    {
        [Test]
        public void FindAllElements()
        {
            var html = new HtmlDocument();
            html.LoadHtml(@"<html>
      <head></head>
      <body>
        <div>
          <p class='content'>Fizzler</p>
          <p>CSS Selector Engine</p></div>
      </body>
  </html>");

            var output=new HtmlElement().FindAllElements(html, "p");
            Assert.AreEqual(output.Count(), 2);
        }
        [Test]
        public void FindAllElementsUES()
        {
            var html = new HtmlDocument();
            using (StreamReader sr = File.OpenText(@"C:\Users\mmachaj\source\repos\TestingCenter\TC.BrowserEngineTest\TestGenerator\HtmlTests\ues-landing-page.html"))
            {
                html.Load(sr);
                var output = new HtmlElement().FindAllElements(html, "input");
                Assert.AreEqual(output.Count(), 2);
            }
        }
    }
}
