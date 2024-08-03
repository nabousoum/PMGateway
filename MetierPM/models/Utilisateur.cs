using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetierPM.models
{
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="*"),MaxLength(80,ErrorMessage ="Taille maximale 80")]
        public string Prenom { get; set; }
        [Required(ErrorMessage ="*"),MaxLength(80, ErrorMessage ="Taille maximale 80")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "*")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "*")]
        public string Adresse { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow;

    }
}