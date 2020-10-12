using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Advanced.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DataContext(DbContextOptions<DataContext> opts)
            : base(opts) { }
        
            
        
    }
}
