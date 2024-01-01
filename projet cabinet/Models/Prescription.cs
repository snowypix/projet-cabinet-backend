namespace projet_cabinet.Models
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public string PrescriptionDate { get; set; }
        public string Medicaments { get; set; }
        public int DossierID { get; set; }
        public virtual Dossier Dossier { get; set; }
    }
}
