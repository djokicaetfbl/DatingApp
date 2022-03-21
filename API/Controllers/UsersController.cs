using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entites;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IPhotoService _photoService;


        //public UsersController(DataContext context)
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            //_context = context;
            
            _mapper = mapper;
            _photoService = photoService;
            _userRepository = userRepository;
        }

        [HttpGet]
        //public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // IEnumarable jednostavna iteracija kroz kolekciju odredjenog tipa // using System.Threading.Tasks; , zbog asinhronog progrmairanja, kako bi aplikacija bila skalabilna i uspjesno usluzila sve zahtjeve
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
        [HttpGet("{username}", Name = "GetUser")]
        //public async Task<ActionResult<AppUser>> GetUser(int id) // IEnumarable jednostavna iteracija kroz kolekciju odredjenog tipa
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //return await _context.User.FindAsync(id);
            //return await _userRepository.GetUserByUsernameAsync(username);
            //var user = await _userRepository.GetUserByUsernameAsync(username); // ovaj dio ce da se cuva u memoriji , sto nije efikasno, bolje je da se direkt komunicira sa bazom
            return await _userRepository.GetMemberAsync(username);

            //return _mapper.Map<MemberDto>(user);
           
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.GetUsername();
            var user = await _userRepository.GetUserByUsernameAsync(username);

   
            _mapper.Map(memberUpdateDto, user); // koristimo Map f-ju iz AutoMapperProfile.cs kako ne bismo za svaki atribut trebal ida radimo: user.city = memberUpdateDto.city vec to definisemo u AutoMapperProfile.cs sa CreateMap()

            _userRepository.Update(user); // redefinisali smo u _userRepository ef Update

            if (await _userRepository.SaveAllAsync()) return NoContent();
            // u _userRepository (IUserRepository) naveli mso osnove ef metode i onda ih redefinisali tako sto smo o5 pozvai ef metode :D

            return BadRequest("failed to update user!");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                publicId = result.PublicId,
            };

            if(user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())
            {
                //return _mapper.Map<PhotoDto>(photo);
                return CreatedAtRoute("GetUser", new {username = user.UserName} ,_mapper.Map<PhotoDto>(photo));  // za ovu rutu [HttpGet("{username}", Name = "GetUser")]
            }   // nakon uspjesnog dodavanja neka ide na tu rutu

            return BadRequest("Problem adding photo.");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto (int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.IsMain) return BadRequest("This is already your man photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMain != null) { 
                currentMain.IsMain = true;
                user.Photos.Where(x => x.Id != photoId).ToList().ForEach(y => y.IsMain = false); // ovo mu je falilo da ostali budu false, generalno je ovo blesavo napisao.
            }
            photo.IsMain = true;

            if (await _userRepository.SaveAllAsync())
                return NoContent();
            return BadRequest("Failed to set main photo");

        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(X => X.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete your main photo!");

            if (photo.publicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.publicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete photo!");
        }


    }
}