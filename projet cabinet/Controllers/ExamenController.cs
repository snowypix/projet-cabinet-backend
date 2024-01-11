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
    [Route("examen")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly UsersDBContext context;

        public ExamenController(UsersDBContext _context)
        {
            context = _context;
        }


        // POST: api/Examen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostExamen(Examen examen)
        {
            if (context.Examens == null)
            {
                return Problem("Entity set 'UsersDBContext.Examens'  is null.");
            }
            context.Examens.Add(examen);
            context.SaveChangesAsync();

            return CreatedAtAction("GetExamen", new { id = examen.ExamenID }, examen);
        }

    }
}
