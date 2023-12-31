﻿using System.ComponentModel.DataAnnotations;

namespace projet_cabinet.Models
{
    public class RDV
    {
        public int RDVId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Heure { get; set; }
        public int MedecinID { get; set; }
        public virtual Medecin? Medecin { get; set; }
        public int PatientID { get; set; }
        public virtual Patient? Patient { get; set; }
        // Properties specific to the RDV
    }
}
