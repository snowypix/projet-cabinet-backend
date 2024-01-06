using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_cabinet.Data;
using projet_cabinet.Models;

namespace projet_cabinet.Controllers
{
    [Route("rdvs")]
    [ApiController]
    public class RDVsController : ControllerBase
    {
        private readonly UsersDBContext context;

        public RDVsController(UsersDBContext _context)
        {
            context = _context;
        }
        [AllowAnonymous]
        [HttpGet("horaires")]
        public IActionResult Horaires()
        {
            var horaires = context.Medecins.Select(m => new
            {
                HoraireDebut = m.HoraireDebut,
                HoraireFin = m.HoraireFin,
                FullName = m.FullName,
                Id = m.ID
            }).ToList();
            return Ok(horaires);
        }
        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult rdvCreate(RDV rdv)
        {
            // Check for rendez-vous conflicts
            bool isConflict = CheckRdvConflicts(rdv);

            if (isConflict)
            {
                return BadRequest("Conflit de rendez-vous choisissez un autre.");
            }
            RDV newRdv = new RDV
            {
                Date = rdv.Date,
                Heure = rdv.Heure,
                MedecinID = rdv.MedecinID,
                PatientID = rdv.PatientID
            };

            context.RDVs.Add(newRdv);
            context.SaveChanges();

            return Ok();
        }
        private bool CheckRdvConflicts(RDV rdv)
        {
            // Get the range of 15 minutes
            TimeSpan timeRange = TimeSpan.FromMinutes(15);

            // No need to parse, directly use TimeSpan
            TimeSpan startTime = rdv.Heure - timeRange;
            TimeSpan endTime = rdv.Heure + timeRange;

            // Query the database to check for any conflicts
            bool isConflict = context.RDVs.Any(r =>
                r.MedecinID == rdv.MedecinID &&
                r.Date.Date == rdv.Date.Date && // Compare only the Date part
                r.Heure >= startTime &&
                r.Heure <= endTime);

            return isConflict;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetRDVs(int id)
        {
            var rdvs = context.RDVs
                .Include(rdv => rdv.Medecin)
                .Where(rdv => rdv.PatientID == id)
                .Select(rdv => new
                {
                    rdv.RDVId,
                    rdv.Date,
                    rdv.Heure,
                    MedecinName = rdv.Medecin != null ? rdv.Medecin.FullName : string.Empty
                });
                //.ToList();

            return Ok(rdvs);
        }
        [AllowAnonymous]
        [HttpGet("med/{id}")]
        public IActionResult GetRDVsMedecin(int id)
        {
            var rdvs = context.RDVs
                .Include(rdv => rdv.Patient)
                .Where(rdv => rdv.MedecinID == id)
                .Select(rdv => new
                {
                    rdv.RDVId,
                    rdv.Date,
                    rdv.Heure,
                    PatientName = rdv.Patient != null ? rdv.Patient.FullName : string.Empty
                })
                .ToList();

            return Ok(rdvs);
        }
    }
}
