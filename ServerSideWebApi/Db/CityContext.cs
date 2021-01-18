using Microsoft.EntityFrameworkCore;
using ServerSideWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSideWebApi.Db
{
    public class CityContext : DbContext
    {
        public CityContext()
        {

        }
        public CityContext(DbContextOptions<CityContext>options) : base(options)
        {

        }

        public virtual DbSet<City> City { get; set; }
    }
}
