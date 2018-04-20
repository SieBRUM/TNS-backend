using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSApi.Mapping;

namespace TNSApi.Services
{
    public interface IDatabaseServiceProvider
    {
        DbSet<User> Users { get; set; }
        DbSet<Wheelchair> Wheelchairs { get; set; }
    }
}
