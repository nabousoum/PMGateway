using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class Jury
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; }
        public ICollection<MembreJury> MembreJuries { get; set; }
        public ICollection<Verdict> Verdicts { get; set; }
    }
}