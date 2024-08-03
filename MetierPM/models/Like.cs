using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int IdLecteur { get; set; }
        [ForeignKey("IdLecteur")]
        public virtual Lecteur Lecteur { get; set; }

        public int IdMemoire { get; set; }
        [ForeignKey("IdMemoire")]
        public virtual Memoire Memoire { get; set; }
    }
}