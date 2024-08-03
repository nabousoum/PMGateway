using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class Expert:Utilisateur
    {
        [Required(ErrorMessage = "*")]
        public string fonction { get; set; }

        [Required(ErrorMessage = "*")]
        public DateTime DateNaissance { get; set; }
    }
}