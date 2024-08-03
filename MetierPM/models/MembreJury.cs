using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class MembreJury
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Profession { get; set; }

        public int IdJury { get; set;}
        [ForeignKey("IdJury")]
        public virtual Jury Jury { get; set; }
    }
}