using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Parser.Models.ParserCore;
using Parser.Models.ParserCore.Olx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Parser.Models
{
    public class ParserCar
    {
        public string CarHtml { get; set; }
        public void ParseHtml(string carUrl)
        {
            CarHtml = HtmlLoader.GetSourseByCarUrl(carUrl);
        }

        public Car GetCar()
        {
            Car car = null;
            if (CarHtml != null)
            {
                List<string> list = new List<string>();
                var domParser = new HtmlParser();
                var document = domParser.Parse(CarHtml);

                var EngineCapacity =
                    car = new Car
                    {
                        Data_item = GetDataItem(document),
                        Price = GetPrice(document),
                        Organization = GetInfo(document, "Объявление от"),
                        Brand = GetInfo(document, "Марка"),
                        Model = GetInfo(document, "Модель"),
                        YearOfIssue = GetInfo(document, "Год выпуска"),
                        BodyType = GetInfo(document, "Тип кузова"),
                        Colour = GetInfo(document, "Цвет"),
                        TypeOfFuel = GetInfo(document, "Вид топлива"),
                        EngineCapacity = GetInfo(document, "Объем двигателя"),
                        Transmission = GetInfo(document, "Коробка передач"),
                        Mileage = GetInfo(document, "Пробег"),
                        Other = GetInfo(document, "Прочее")
                    };
            }
            return car;
        }

        private string GetInfo(IHtmlDocument document,  string value)
        {
            try
            {
                return document.QuerySelectorAll("strong")?.Where(item => item.ParentElement.ClassName != null && item.ParentElement.ClassName.Contains("value") && item.ParentElement.PreviousElementSibling.TextContent.Contains(value))?.First()?.TextContent.Replace("\t", "")?.Replace("\n", "");
            }
            catch 
            {
                return null;
            }
            
        }

        private string GetDataItem(IHtmlDocument document)
        {

            var items = document.QuerySelectorAll("div").Where(item => item.HasAttribute("data-item"));
            foreach (var item in items)
            {
                return item.GetAttribute("data-item");
            }
            return null;
        }

        private string GetPrice(IHtmlDocument document)
        {
            try
            {
                return document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("price-label")).FirstOrDefault()?.QuerySelectorAll("strong").FirstOrDefault()?.TextContent.Replace("\t", "").Replace("\n", "");
            }
            catch
            {
                return null;
            }

        }
    }
}