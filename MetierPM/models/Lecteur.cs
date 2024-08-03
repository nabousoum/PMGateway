using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MetierPM.models
{
    public class Lecteur:Utilisateur
    {
        [Required(ErrorMessage = "*")]
        public string DateNaissance {  get; set; }

        [Required(ErrorMessage = "*")]
        public string Sexe { get; set; }
        public ICollection<Commentaire> Commentaires { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Vu> Vus { get; set; }
        public ICollection<Favori> Favoris { get; set; }
    }
}