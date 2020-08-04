using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class walletUser
    {
        public int Id { get; set; }
        public string publicKey { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
