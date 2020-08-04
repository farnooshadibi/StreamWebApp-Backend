using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Container
    {
        static Container()
        {
            Persons = new ConcurrentBag<Person>();
        }

        public static IEnumerable<Person> Persons { get; set; }
    }
}
