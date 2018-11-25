using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngleSharp.Dom.Html;

namespace Parser.Models.ParserCore.Olx
{
    public class OlxParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            List<string> list = new List<string>();
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("link linkWithHash detailsLink"));
            foreach (var item in items)
            {
                list.Add(item.GetAttribute("href"));
            }

            return list.ToArray();
        }
    }
}