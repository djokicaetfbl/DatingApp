using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; // View dolazi sa strane klijenta , to je kod nas Angular aplikacija
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Entites;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}