using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class AnneeAcademique
    {
        [Key]
        public int Id { get; set; }
        public string anneeDebut { get; set; }
        public string annneeFin {  get; set; }
    }
}