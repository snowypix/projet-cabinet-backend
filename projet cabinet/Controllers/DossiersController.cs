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
    [Route("dossier")]
    [ApiController]
    public class DossiersController : ControllerBase
    {
        private readonly UsersDBContext context;

        public DossiersController(UsersDBContext _context)
        {
            context = _context;
        }

        [HttpPost("create/{patientId}/{medecinid}")]
        public IActionResult CreateDossierMedical(int patientId, int medecinid)
        {
            var patient = context.Patients.Find(patientId);
            if (patient == null)
            {
                return NotFound("Patient inexistant");
            }

            var dossier = new Dossier
            {
                // Initialize the properties of the dossier according to your model
                PatientID = patientId,
                MedecinID = medecinid
            };

            context.Dossiers.Add(dossier);
            context.SaveChanges();

            return CreatedAtAction("GetDossier", new { id = dossier.DossierID }, dossier);
        }
        [HttpGet("dossiers/{medecinid}")]
        public IActionResult GetDossiersForMedecin(int mededinid)
        {
            // Find all dossiers for the logged-in Medecin
            var patientDossiers = context.Patients
            .Select(user => new
            {
                PatientID = user.ID,
                PatientName = user.FullName,
                DossierExists = context.Dossiers.Any(dossier => dossier.PatientID == user.ID)
            })
            .ToList();

            
            if (patientDossiers == null || !patientDossiers.Any())
            {
                return NotFound("No dossiers found for the current Medecin.");
            }

            return Ok(patientDossiers);
        }
    }
}
