using Microsoft.AspNetCore.Mvc;

namespace PMGateway.Controllers
{
    public class LecteurController : Controller
    {
        private readonly ServiceMetier.Service1Client service = new ServiceMetier.Service1Client();

        // GET: api/Lecteur/getAllLecteurs
        [HttpGet("getAllLecteurs")]
        public ICollection<ServiceMetier.Lecteur> GetAllLecteurs()
        {
            ICollection<ServiceMetier.Lecteur> lecteurs = service.GetAllLecteurs();
            return lecteurs;
        }

        // GET: api/Lecteur/getLecteur/5
        [HttpGet("getLecteur/{id}")]
        public ActionResult<ServiceMetier.Lecteur> GetLecteur(int id)
        {
            var lecteur = service.GetLecteur(id);

            if (lecteur == null)
            {
                return NotFound();
            }

            return lecteur;
        }

        // POST: api/Lecteur/addLecteur
        [HttpPost("addLecteur")]
        public ActionResult AddLecteur([FromBody] ServiceMetier.Lecteur lecteur)
        {
            if (service.AddLecteur(lecteur))
            {
                return CreatedAtAction("GetLecteur", new { id = lecteur.Id }, lecteur);
            }
            return BadRequest();
        }

        // PUT: api/Lecteur/updateLecteur/5
        [HttpPut("updateLecteur/{id}")]
        public IActionResult UpdateLecteur(int id, [FromBody] ServiceMetier.Lecteur updatedLecteur)
        {
            if (updatedLecteur == null)
            {
                return BadRequest("Invalid lecteur data.");
            }

            // Récupérer l'objet existant
            var existingLecteur = service.GetLecteur(id);
            if (existingLecteur == null)
            {
                return NotFound();
            }

            // Mettre à jour uniquement les champs non nuls
            if (!string.IsNullOrEmpty(updatedLecteur.Nom))
            {
                existingLecteur.Nom = updatedLecteur.Nom;
            }
            if (!string.IsNullOrEmpty(updatedLecteur.Prenom))
            {
                existingLecteur.Prenom = updatedLecteur.Prenom;
            }
            if (updatedLecteur.DateNaissance != default)
            {
                existingLecteur.DateNaissance = updatedLecteur.DateNaissance;
            }
            if (!string.IsNullOrEmpty(updatedLecteur.Sexe))
            {
                existingLecteur.Sexe = updatedLecteur.Sexe;
            }
            if (updatedLecteur.Commentaires != null)
            {
                existingLecteur.Commentaires = updatedLecteur.Commentaires;
            }
            if (updatedLecteur.Likes != null)
            {
                existingLecteur.Likes = updatedLecteur.Likes;
            }
            if (updatedLecteur.Vus != null)
            {
                existingLecteur.Vus = updatedLecteur.Vus;
            }
            if (updatedLecteur.Favoris != null)
            {
                existingLecteur.Favoris = updatedLecteur.Favoris;
            }

            // Appel du service de mise à jour
            if (service.updateLecteur(id,existingLecteur))
            {
                return NoContent();
            }
            return StatusCode(500, "A problem happened while handling your request.");
        }


        // DELETE: api/Lecteur/deleteLecteur/5
        [HttpDelete("deleteLecteur/{id}")]
        public IActionResult DeleteLecteur(int id)
        {
            if (service.DeleteLecteur(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("searchLecteur")]
        public ActionResult<IEnumerable<ServiceMetier.Lecteur>> SearchLecteurs([FromQuery] string nom, [FromQuery] string prenom)
        {
            var lecteurs = service.SearchLecteurs(nom, prenom);
            return Ok(lecteurs);
        }

    }
}
