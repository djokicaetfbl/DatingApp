using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // View dolazi sa strane klijenta , to je kod nas Angular aplikacija
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /*[ApiController]
    [Route("api/[controller]")]*/  // ovo vise ne treba jer se nsljedjuje iz BaseApiController kojeg smo napravili
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // IEnumarable jednostavna iteracija kroz kolekciju odredjenog tipa // using System.Threading.Tasks; , zbog asinhronog progrmairanja, kako bi aplikacija bila akalabilna u uspjesno usluzila sve zahtjeve
        {
            return await _context.User.ToListAsync(); // ToList() nije asinhrona metoda , ali ToListAsync() jeste i u sklopu je paketa using Microsoft.EntityFrameworkCore;
            // umjesto await moglo se koristiti _context.User.ToListAsync().Result
        }

        // neophodno je koristi asinhroni pristup, recimo da server ima 100 tredova a mi 1000 korisnika sa 100 upita, server istovremeno nece moci da opsluzi sve, ali uz pomoc asinhronog programiranja to ce biti moguce

        // apit/users/3
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) // IEnumarable jednostavna iteracija kroz kolekciju odredjenog tipa
        {
            return await _context.User.FindAsync(id);
        }
    }
}