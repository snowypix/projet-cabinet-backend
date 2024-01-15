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
            string userType = context.Users.Where(u => u.ID == id).Select(u => EF.Property<string>(u, "UserType")).FirstOrDefault();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("Id", id.ToString()),
                new Claim("Type", userType)
            };

            var tokenOptions = new JwtSecurityToken
            (
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
        public IActionResult Register([FromBody] Patient model)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var user = new Patient
            {
                Email = model.Email,
                Password = hashedPassword,
                FullName = model.FullName,
                Age = model.Age,
                Genre = model.Genre,
                Adresse = model.Adresse,
                Antecedents = model.Antecedents
            };
            context.Users.Add(user);
            context.SaveChanges();
            // Add user to database and save changes
            return Ok(new { message = "Registration successful" });
        }

        [AllowAnonymous]
        [HttpPost("add")]
        public IActionResult Add([FromBody] UserDto model)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            switch (model.UserType)
            {
                case "Patient":
                    var user = new Patient
                    {
                        Antecedents = model.Antecedents,
                        Email = model.Email,
                        Password = hashedPassword,
                        FullName = model.FullName,
                        Age = model.Age,
                        Genre = model.Genre,
                        Adresse = model.Adresse,
                    };
                    context.Users.Add(user);
                    break;
                case "Medecin":
                    var user1 = new Medecin
                    {
                        Email = model.Email,
                        Password = hashedPassword,
                        FullName = model.FullName,
                        Age = model.Age,
                        Genre = model.Genre,
                        Adresse = model.Adresse,
                        HoraireDebut = model.HoraireDebut,
                        HoraireFin = model.HoraireFin
                    };
                    context.Users.Add(user1);
                    break;
                case "Infirmier":
                    var user2 = new Infirmier
                    {
                        Email = model.Email,
                        FullName = model.FullName,
                        Age = model.Age,
                        Password = hashedPassword,
                        Genre = model.Genre,
                        Adresse = model.Adresse,
                        HoraireDebut = model.HoraireDebut,
                        HoraireFin = model.HoraireFin
                    };
                    context.Users.Add(user2);
                    break;
                default:
                    var user3 = new Admin
                    {
                        Email = model.Email,
                        FullName = model.FullName,
                        Password = hashedPassword,
                        Age = model.Age,
                        Genre = model.Genre,
                        Adresse = model.Adresse
                    };
                    context.Users.Add(user3);
                    break;
            }


            context.SaveChanges();


            // Add user to database and save changes
            return Ok(new { message = "Registration successful" });
        }
        [AllowAnonymous]
        [HttpPost("modify/{id}")]
        public IActionResult Modify(int id, [FromBody] UserDto model)
        {
            // You would need to fetch the user from the database first
            User user = context.Users.Find(id);

            // Handle the case where the user might not be found
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }


            if (model.UserType == "Patient")
            {
                var patient = new Patient
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    Age = model.Age,
                    Password = model.Password,
                    Genre = model.Genre,
                    Adresse = model.Adresse,
                    Antecedents = model.Antecedents
                };
                context.Users.Add(patient);
                // Rest of your code with the patient object
            }
            if (model.UserType == "Medecin")
            {
                var patient = new Medecin
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    Age = model.Age,
                    Password = model.Password,
                    Genre = model.Genre,
                    Adresse = model.Adresse,
                    HoraireDebut = model.HoraireDebut,
                    HoraireFin = model.HoraireFin
                };
                context.Users.Add(patient);
                // Rest of your code with the patient object
            }
            if (model.UserType == "Infirmier")
            {
                var patient = new Infirmier
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    Age = model.Age,
                    Password = model.Password,
                    Genre = model.Genre,
                    Adresse = model.Adresse,
                    HoraireDebut = model.HoraireDebut,
                    HoraireFin = model.HoraireFin
                };
                context.Users.Add(patient);
                // Rest of your code with the patient object
            }
            if (model.UserType == "Admin")
            {
                var patient = new Admin
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    Age = model.Age,
                    Password = model.Password,
                    Genre = model.Genre,
                    Adresse = model.Adresse
                };
                context.Users.Add(patient);
                // Rest of your code with the patient object
            }
            // Handle different user types if necessary
            context.Users.Remove(user);

            // Save the changes to the database
            context.SaveChanges();

            return Ok(new { message = "User modified successfully" });
        }
        [AllowAnonymous]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            context.Users.Remove(user);
            context.SaveChanges();

            return Ok(new { message = "User deleted successfully" });
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
            return Ok(new { message = "Login successful", token = token });
        }

        [HttpGet("all")]
        //[Authorize(Roles = "Admin")] 
        public IActionResult GetAllUsers1()
        {
            var users = context.Users.ToList();
            return Ok(users);
        }
        [HttpGet("all2")]
        //[Authorize(Roles = "Admin")] 
        public IActionResult GetAllUsers2()
        {
            var users = context.Users.ToList();
            var userDetails = users.Select(user =>
            {
                var userdto = new UserDto
                {
                    ID = user.ID,
                    Email = user.Email,
                    Password = user.Password,
                    FullName = user.FullName,
                    Genre = user.Genre,
                    Adresse = user.Adresse,
                    Age = user.Age
                };

                if (user is Patient patient)
                {
                    userdto.Antecedents = patient.Antecedents;
                }
                if (user is Medecin medecin)
                {
                    userdto.HoraireDebut = medecin.HoraireDebut;
                    userdto.HoraireFin = medecin.HoraireFin;
                }
                if (user is Infirmier infirmier)
                {
                    userdto.HoraireDebut = infirmier.HoraireDebut;
                    userdto.HoraireFin = infirmier.HoraireFin;
                }
                return userdto;
            }).ToList();

            return Ok(userDetails);
        }

        [HttpGet("{id}")]
        /*[Authorize] */
        public IActionResult GetUserById(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.ID == id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }
    }
}
