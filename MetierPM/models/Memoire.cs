using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class Memoire
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "*")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*")]
        public string DocumentPath { get; set; }

        [Required(ErrorMessage = "*")]
        public string statut {  get; set; }
        public  string imagePath {  get; set; }
        public ICollection<Commentaire> Commentaires { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Vu> Vus { get; set; }
        public ICollection<Favori> Favoris { get; set; }

        //public int IdAnneeAcademique { get; set; }
        //[ForeignKey("IdAnneeAcademique")]
        //public virtual AnneeAcademique AnneeAcademique { get; set; }

    }
}