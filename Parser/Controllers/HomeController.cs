using Parser.Models;
using Parser.Models.ParserCore;
using Parser.Models.ParserCore.Olx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Parser.Controllers
{
    public class HomeController : Controller
    {
        ParserWorker<string[]> parser = new ParserWorker<string[]>(
                new OlxParser()                
            );

        CarContext db = new CarContext();
         
        public async Task<ActionResult> Index()
        {
            
            return View();
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            foreach (var carUrl in arg2)
            {
                ParserCar car = new ParserCar();
                car.ParseHtml(carUrl);
                Car carTmp = car.GetCar();
                if (carTmp != null && carTmp.Data_item != null)
                {
                    if (!(db.cars.FirstOrDefault(item=> item.Data_item == carTmp.Data_item) != null))
                    {
                        db.cars.Add(carTmp);
                    }
                }
            }
            db.SaveChangesAsync();
            
        }

        public async Task<ActionResult> StartParse(int? start, int? end)
        {
            
            if (start != null && end != null && !parser.IsActive)
            {
                parser.OnCompleted += Parser_OnCompleted;
                parser.OnNewData += Parser_OnNewData;
                parser.ParserSettings = new OlxSettings((int)start, (int)end);
                var d = await parser.Start();
            }
            
            return RedirectToAction("Index");
        }

        

        private void Parser_OnCompleted(object obj)
        {
            View("About");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}