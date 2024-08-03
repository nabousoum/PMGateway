using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class Commentaire
    {
        public int Id { get; set; }
        public string Description { get; set;}
        public DateTime Date { get; set;}

        public int IdLecteur { get; set;}
        [ForeignKey("IdLecteur")]
        public virtual Lecteur Lecteur { get; set; }

        public int IdMemoire { get; set; }
        [ForeignKey("IdMemoire")]
        public virtual Memoire Memoire { get; set; }
    }
}