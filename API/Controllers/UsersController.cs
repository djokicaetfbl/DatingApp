using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entites;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // View dolazi sa strane klijenta , to je kod nas Angular aplikacija
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    /*[ApiController]
    [Route("api/[controller]")]*/  // ovo vise ne treba jer se nsljedjuje iz BaseApiController kojeg smo napravili
    [Authorize] // sada ce sve metode u klasi da zahtjevaju autorizaciju
    public class UsersController : BaseApiController
    {
        //private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;


        //public UsersController(DataContext context)
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            //_context = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        //public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // IEnumarable jednostavna iteracija kroz kolekciju odredjenog tipa // using System.Threading.Tasks; , zbog asinhronog progrmairanja, kako bi aplikacija bila akalabilna u uspjesno usluzila sve zahtjeve
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //return await _context.User.ToListAsync(); // ToList() nije asinhrona metoda , ali ToListAsync() jeste i u sklopu je paketa using Microsoft.EntityFrameworkCore;
            // umjesto await moglo se koristiti _context.User.ToListAsync().Result
            //return Ok(await _userRepository.GetUsersAsync());
            //var users = await _userRepository.GetUsersAsync();

            //var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            var users = await  _userRepository.GetMembersAsync();

            return Ok(users);
        }

        // neophodno je koristi asinhroni pristup, recimo da server ima 100 tredova a mi 1000 korisnika sa 100 upita, server istovremeno nece moci da opsluzi sve, ali uz pomoc asinhronog programiranja to ce biti moguce

        // apit/users/3
        //[HttpGet("{id}")]
        [HttpGet("{username}")]
        //public async Task<ActionResult<AppUser>> GetUser(int id) // IEnumarable jednostavna iteracija kroz kolekciju odredjenog tipa
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //return await _context.User.FindAsync(id);
            //return await _userRepository.GetUserByUsernameAsync(username);
            //var user = await _userRepository.GetUserByUsernameAsync(username); // ovaj dio ce da se cuva u memoriji , sto nije efikasno, bolje je da se direkt komunicira sa bazom
            return await _userRepository.GetMemberAsync(username);

            //return _mapper.Map<MemberDto>(user);
           
        }
    }
}