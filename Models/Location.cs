using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advanced.Models
{
    public class Location
    {
        public long LocationId { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public IEnumerable<Person> People { get; set; }
    }
}
