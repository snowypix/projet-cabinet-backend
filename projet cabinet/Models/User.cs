using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projet_cabinet.Models
{
    public class User
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
        public int Age { get; set; }

    }
    [Table("Users")]
    public class Patient : User
    {
        public int DossierID { get; set; }
        public virtual Dossier Dossier { get; set; }
        public virtual ICollection<RDV> RDVs { get; set; }
        // Properties specific to the Patient
    }

    [Table("Users")]
    public class Infirmier : User
    {
        // Properties specific to the Infirmier
    }

    [Table("Users")]
    public class Medecin : User
    {
        public virtual ICollection<Dossier> Dossiers { get; set; }
        public virtual ICollection<RDV> RDVs { get; set; }
        public TimeSpan HoraireDebut { get; set; }
        public TimeSpan HoraireFin { get; set; }
    }
}
