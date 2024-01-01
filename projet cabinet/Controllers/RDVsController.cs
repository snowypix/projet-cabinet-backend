using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_cabinet.Data;
using projet_cabinet.Models;

namespace projet_cabinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDVsController : ControllerBase
    {
        private readonly UsersDBContext _context;

        public RDVsController(UsersDBContext context)
        {
            _context = context;
        }
        
    }
}
