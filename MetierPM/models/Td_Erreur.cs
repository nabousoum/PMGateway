using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetierPM.models
{
    public class Td_Erreur
    {
        [Key]
        public int id { get; set; }
        public Nullable<System.DateTime> dateErreur { get; set; }

        [Required, MaxLength(3000)]
        public string descriptionErreur { get; set; }

        [Required, MaxLength(300)]
        public string titreErreur { get; set; }



    }
}