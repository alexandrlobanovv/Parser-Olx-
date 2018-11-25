using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parser.Models.ParserCore.Olx
{
    public class OlxSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://www.olx.ua/transport/legkovye-avtomobili/";
        public string Argument { get; set; } = "?page={currentPage}";
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public OlxSettings(int startPage, int endPage)
        {
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}