using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class Verdict
    {
        [Key]
        public int Id { get; set; }
        public string Note { get; set; }    
        public string Mention { get; set; }
        public string Commentaire { get; set; }
        public string Statut { get; set; }

        public int IdJury { get; set; }
        [ForeignKey("IdJury")]
        public virtual Jury Jury { get; set; }

    }
}