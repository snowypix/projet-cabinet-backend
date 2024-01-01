using Microsoft.AspNetCore.Mvc;
using projet_cabinet.Data;
using projet_cabinet.Models;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace projet_cabinet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
        
    {
        private readonly UsersDBContext context;

        public UserController(UsersDBContext _context)
        {
            context = _context;
        }
        private string GenerateJwtToken(int expirationMinutes, string username, int id)
        {
            var keyBytes = Encoding.ASCII.GetBytes("tkrpgkretrkgreltrgertgtr");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("Id", id.ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "pix",
                audience: "pix",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
        // Dependency injection of your DbContext or user service here
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var user = new User
            {
                Email = model.Email,
                Password = hashedPassword,
                FullName = model.FullName,
                Age = model.Age,
                Genre = model.Genre
            };
            context.Users.Add(user);
            context.SaveChanges();
            // Add user to database and save changes

            return Ok(new { message = "Registration successful" });
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin model)
        {
            // Find the user based on the provided email
            var user = context.Users.FirstOrDefault(u => u.Email == model.Email);

            // Check if the user exists and the password is correct
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid email or password" });
            }
            var token = GenerateJwtToken(10, user.Email, user.ID);
            return Ok(new { message = "Login successful" , token = token});
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
        [HttpPost("rdv/create")]
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
        [HttpGet("rdv/rdvs/{id}")]
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
                })
                .ToList();

            return Ok(rdvs);
        }
    }
}
