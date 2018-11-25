using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using HtmlAgilityPack;

namespace Parser.Models.ParserCore
{
    public class HtmlLoader
    {
        readonly HttpClient client;
        HtmlWeb web = new HtmlWeb();
        readonly string url;
        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}{settings.Argument}";
        }

        public async Task<string> GetSourseByPageId(int id)
        {
            var currentUrl = url.Replace("{currentPage}", id.ToString());
            var response = await client.GetAsync(currentUrl);
            string sourse = null;
            if (response!=null && response.StatusCode == HttpStatusCode.OK)
            {
                sourse = web.Load(currentUrl).ParsedText;
            }
            return sourse;
        }

        public static string GetSourseByCarUrl(string carUrl)
        {
            HtmlWeb web = new HtmlWeb();
            string sourse = null;
            HtmlDocument doc = web.Load(carUrl);
            if (doc != null)
            {
                sourse = doc.ParsedText;
            }
            return sourse;
        }
    }
}