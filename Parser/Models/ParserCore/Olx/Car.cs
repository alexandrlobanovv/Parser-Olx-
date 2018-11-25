using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parser.Models.ParserCore.Olx
{
    public class Car
    {
        public int Id { get; set; }
        public string Data_item { get; set; }
        public string Organization { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public string BodyType { get; set; }
        public string TypeOfFuel { get; set; }
        public string Transmission { get; set; }
        public string YearOfIssue { get; set; }
        public string Colour { get; set; }
        public string EngineCapacity { get; set; }
        public string Mileage { get; set; }
        public string Other { get; set; }
    }
}