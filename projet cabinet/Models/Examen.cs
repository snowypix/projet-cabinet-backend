namespace projet_cabinet.Models
{
    public class Examen
    {
        public int ExamenID { get; set; }
        public string ExamenDate { get; set; }
        public string ExamenNom { get; set; }
        public string Resultat { get; set; }
        public int DossierID { get; set; }
        public virtual Dossier Dossier { get; set; }
    }
}
