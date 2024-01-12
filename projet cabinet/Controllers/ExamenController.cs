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


        [HttpGet("{dossierId}")]
        public IActionResult Prescriptions(int dossierId)
        {
            var examens = context.Examens
                .Where(p => p.DossierID == dossierId)
                .Select(p => new
                {
                    p.ExamenID,
                    p.ExamenDate,
                    p.ExamenNom,
                    p.Resultat
                }).ToList();
            return Ok(examens);
        }
      

        [HttpPost("{dossierId}")]
        public IActionResult CreateExamen(int dossierId, [FromBody] Examen examen)
        {
            try
            {
                // Make sure the dossier exists
                var dossier = context.Dossiers.Find(dossierId);
                if (dossier == null)
                {
                    return NotFound("Dossier not found");
                }

                // Set the DossierID for the prescription
                examen.DossierID = dossierId;

                // Add the prescription to the context and save changes
                context.Examens.Add(examen);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create examen: {ex.Message}");
            }
        }
    }
}
