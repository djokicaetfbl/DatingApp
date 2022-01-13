using API.Extensions;
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
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interest { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; } /* one to many, one user can have many photos, napravit ce kolonu u Tabeli Photo AppUserId - strani kljuc */

        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge(); // DateOfBirt je tipa DateTime a u DateTime ugradili smo extension method CalculateAge()
        //}

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }


}