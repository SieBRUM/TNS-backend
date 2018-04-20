using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSApi.Mapping;
using TNSApi.Mapping.Link_tables;

namespace TNSApi.Services
{
    public interface IDatabaseServiceProvider
    {
        DbSet<User> Users { get; set; }
        DbSet<Wheelchair> Wheelchairs { get; set; }
        DbSet<Addition> Additions { get; set; }
        DbSet<Article> Articles { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Frontwheel> Frontwheels { get; set; }
        DbSet<Hoop> Hoops { get; set; }
        DbSet<RalColor> RalColors { get; set; }
        DbSet<Tire> Tires { get; set; }
        DbSet<Wheel> Wheels { get; set; }
        DbSet<Wheelprotector> Wheelprotectors { get; set; }


        DbSet<WheelchairArticle> WheelchairArticles { get; set; }
        DbSet<WheelchairFrontwheel> WheelchairFrontwheels { get; set; }
        DbSet<WheelchairHoop> WheelchairHoops { get; set; }
        DbSet<WheelchairTire> WheelchairTires { get; set; }
        DbSet<WheelchairWheelprotector> WheelchairWheelprotectors { get; set; }
    }
}
