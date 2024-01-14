using System.ComponentModel.DataAnnotations;

namespace projet_cabinet.Models
{
    public class UserDto
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        public int Age { get; set; }
        public string? Antecedents { get; set; }
        public TimeSpan? HoraireDebut { get; set; }
        public TimeSpan? HoraireFin { get; set; }
        public string UserType { get; set; }
    }
}
