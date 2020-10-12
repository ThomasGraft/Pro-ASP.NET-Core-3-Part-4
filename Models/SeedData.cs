using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Advanced.Models
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            if (context.People.Count() == 0 && context.Departments.Count() == 0 && context.Locations.Count() == 0)
            {
                var d1 = new Department { Name = "Sales" };
                var d2 = new Department { Name = "Development" };
                var d3 = new Department { Name = "Support" };
                var d4 = new Department { Name = "Facilities" };

                context.Departments.AddRange(d1, d2, d3, d4);
                context.SaveChanges();

                var l1 = new Location { City = "Oakland", State = "CA" };
                var l2 = new Location { City = "San Jose", State = "CA" };
                var l3 = new Location { City = "New York", State = "NY" };

                context.Locations.AddRange(l1, l2, l3);

                context.AddRange(
                    new Person
                    {
                        FirstName = "Kevin",
                        SurName = "Jacobs",
                        Department = d2,
                        Location = l1
                    },
                    new Person
                    {
                        FirstName = "Charles",
                        SurName = "Grant",
                        Department = d2,
                        Location = l3
                    },
                    new Person
                    {
                        FirstName = "Bright",
                        SurName = "Becker",
                        Department = d4,
                        Location = l1
                    },
                    new Person
                    {
                        FirstName = "Murphy",
                        SurName = "Lara",
                        Department = d1,
                        Location = l3
                    },
                    new Person
                    {
                        FirstName = "Beasley",
                        SurName = "Hoffman",
                        Department = d4,
                        Location = l3
                    },
                    new Person
                    {
                        FirstName = "Marks",
                        SurName = "Hays",
                        Department = d4,
                        Location = l1
                    },
                    new Person
                    {
                        FirstName = "Underwood",
                        SurName = "Smith",
                        Department = d2,
                        Location = l1
                    },
                    new Person
                    {
                        FirstName = "Randall",
                        SurName = "Lloyd",
                        Department = d3,
                        Location = l2
                    },
                    new Person
                    {
                        FirstName = "Guzman",
                        SurName = "Case",
                        Department = d2,
                        Location = l2
                    });
                context.SaveChanges();
                        
            }
        }
    }
}
