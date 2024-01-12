using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_cabinet.Models;

namespace projet_cabinet.Data
{
    [Route("prescription")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly UsersDBContext context;

        public PrescriptionsController(UsersDBContext _context)
        {
            context = _context;
        }

        [HttpGet("{dossierId}")]
        public IActionResult Prescriptions(int dossierId)
        {
            var prescriptions = context.Prescriptions
                .Where(p => p.DossierID == dossierId)
                .Select(p => new
                {
                    p.PrescriptionID,
                    p.PrescriptionDate,
                    p.Medicaments
                }).ToList();
            return Ok(prescriptions);
        }
        [HttpGet("patient/{patientId}")]
        public IActionResult PrescriptionsByPatientId(int patientId)
        {
            var prescriptions = context.Dossiers
                .Where(d => d.PatientID == patientId)
                .SelectMany(d => d.Prescriptions)
                .Select(p => new
                {
                    p.PrescriptionID,
                    p.PrescriptionDate,
                    p.Medicaments
                    // Add other relevant properties of Prescription here
                }).ToList();

            return Ok(prescriptions);
        }
        [HttpPost("{dossierId}")]
        public IActionResult CreatePrescription(int dossierId, [FromBody] Prescription prescription)
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
                prescription.DossierID = dossierId;

                // Add the prescription to the context and save changes
                context.Prescriptions.Add(prescription);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create prescription: {ex.Message}");
            }
        }
    }
}
