using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entites
{
    public class AppUser
    {
        public int Id { get; set; } // EF prepoznaje da ce nam ti da bude primary key, zbog EF-a potrebno je da atributi budu public
        public string UserName { get; set; } // za pocetak koristit cemo SQLLite za development jer je cross-platform,  pa cemo kasnije preci na SQL Server.
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set;}

    }
}