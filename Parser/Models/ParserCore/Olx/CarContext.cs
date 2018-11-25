using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Parser.Models.ParserCore.Olx
{
    public class CarContext : DbContext
    {
        public DbSet<Car> cars { get; set; }
    }
}