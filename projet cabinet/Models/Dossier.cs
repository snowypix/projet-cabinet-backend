namespace projet_cabinet.Models
{
    public class Dossier
    {
        public int DossierID { get; set; }
        // Assuming a one-to-one relationship with Patient
        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }
        public int MedecinID { get; set; }
        public virtual Medecin Medecin { get; set; }
        public virtual ICollection<Examen> Examens { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
