namespace TNSApi.Models
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using TNSApi.Mapping;
    using TNSApi.Mapping.Link_tables;
    using TNSApi.Services;

    public class DatabaseServiceProvider : DbContext, IDatabaseServiceProvider
    {
        // Your context has been configured to use a 'DatabaseServiceProvider' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TNSApi.Models.DatabaseServiceProvider' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DatabaseServiceProvider' 
        // connection string in the application configuration file.
        public DatabaseServiceProvider()
            : base("name=dbModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wheelchair> Wheelchairs { get; set; }
        public virtual DbSet<Addition> Additions { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Frontwheel> Frontwheels { get; set; }
        public virtual DbSet<Hoop> Hoops { get; set; }
        public virtual DbSet<RalColor> RalColors { get; set; }
        public virtual DbSet<Tire> Tires { get; set; }
        public virtual DbSet<Wheel> Wheels { get; set; }
        public virtual DbSet<Wheelprotector> Wheelprotectors { get; set; }

        public virtual DbSet<WheelchairArticle> WheelchairArticles { get; set; }
        public virtual DbSet<WheelchairFrontwheel> WheelchairFrontwheels { get; set; }
        public virtual DbSet<WheelchairHoop> WheelchairHoops { get; set; }
        public virtual DbSet<WheelchairTire> WheelchairTires { get; set; }
        public virtual DbSet<WheelchairWheelprotector> WheelchairWheelprotectors { get; set; }

        public virtual DbContext Context
        {
            get
            {
                return this;
            }
        }
    }
}