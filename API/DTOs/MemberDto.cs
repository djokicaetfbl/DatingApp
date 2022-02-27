using API.Entites;
using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; } // EF prepoznaje da ce nam ti da bude primary key, zbog EF-a potrebno je da atributi budu public
        public string Username { get; set; } // za pocetak koristit cemo SQLLite za development jer je cross-platform,  pa cemo kasnije preci na SQL Server.
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }// = DateTime.Now;
        public DateTime LastActive { get; set; }// = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDto> Photos { get; set; } /* one to many, one user can have many photos, napravit ce kolonu u Tabeli Photo AppUserId - strani kljuc */

    }
}
