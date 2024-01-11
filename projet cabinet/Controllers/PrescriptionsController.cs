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
        [HttpPost]
        public IActionResult AddPrescription([FromBody] Prescription model)
        {
            var nouvellePrescription = new Prescription
            {
                PrescriptionDate = model.PrescriptionDate,
                Medicaments = model.Medicaments,
                DossierID = model.DossierID
                // Assurez-vous que tous les champs nécessaires sont inclus
            };

            context.Prescriptions.Add(nouvellePrescription);
            context.SaveChanges();

            return Ok(nouvellePrescription);
        }
    }
}
