namespace TNSApi.Models
{
    using System.Data.Entity;
    using TNSApi.Mapping;
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
    }
}