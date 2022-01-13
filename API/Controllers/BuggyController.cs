using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entites;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

       [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        //[Authorize]
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.User.Find(-1);

            if (thing == null) return NotFound(); // vraca not-found, na url doda

            return Ok(thing);
        }

        //[Authorize]
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            //var thing = _context?.User.Find(-1); // trebalo bi bez ? ali puca

            //var thingToReturn = thing?.ToString(); // trebalo bi bez ? ali puca
            // no reference exception dobijamo kad god izvrsavamo null nad necim
            // nismo stavili try catch , vec smo napravili smoj middlevare - Exception Handling Middleware (ExceptionMiddleware)
            // i ukljucili ga u Startap.cs kao app.UseMiddleware<ExceptionMiddleware>(); 

            //var thing = _context.User.Find(-1);
            //var thingToReturn = (thing == null ? "" : thing.ToString());
            AppUser thing  = _context.User.Find(-1); // trebalo bi bez ? ali puca, pa sam reko debugr-u da na ovaj exception ne staje vec da ide dalje

            var thingToReturn = thing.ToString(); // trebalo bi bez ? ali puca, pa sam reko debugr-u da na ovaj exception ne staje vec da ide dalje

            return thingToReturn;
        }

        //[Authorize]
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }

    }
}