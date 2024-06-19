using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet_cabinet.Models;

namespace projet_cabinet.Controllers
{
    [Route("rdvs")]
    [ApiController]
    public class RDVsController : ControllerBase
    {
        // Create a list of RDV objects
        List<RDV> rdvList = new List<RDV>
            {
                new RDV { RDVId = 1, Date = new DateTime(2023, 6, 19), Heure = new TimeSpan(9, 0, 0), MedecinID = 101, PatientID = 201 },
                new RDV { RDVId = 2, Date = new DateTime(2023, 6, 20), Heure = new TimeSpan(10, 30, 0), MedecinID = 102, PatientID = 202 },
                new RDV { RDVId = 3, Date = new DateTime(2023, 6, 21), Heure = new TimeSpan(11, 0, 0), MedecinID = 103, PatientID = 203 }
            };
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetRDVs(int id)
        {

            return Ok(rdvList);
        }
    }
}
