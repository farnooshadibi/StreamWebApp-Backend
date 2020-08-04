using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public Guid userToken { get; set; }
        public string email { get; set; }
        public string publicKey { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public DateTime createAt { get; set; }



    }
}
