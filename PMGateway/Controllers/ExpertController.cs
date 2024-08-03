using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMGateway.Controllers
{
    public class ExpertController : Controller
    {
        private readonly ServiceMetier.Service1Client service = new ServiceMetier.Service1Client();

        [HttpGet("getAllExperts")]
        public ICollection<ServiceMetier.Expert> getAllExperts()
        {
            ICollection<ServiceMetier.Expert> experts = service.getAllExperts();
            return experts;
        }

        // GET: api/Expert/getExpert/5
        [HttpGet("getExpert/{id}")]
        public ActionResult<ServiceMetier.Expert> GetExpert(int id)
        {
            var expert = service.GetExpert(id);

            if (expert == null)
            {
                return NotFound();
            }

            return expert;
        }

        // POST: api/Expert/addExpert
        [HttpPost("addExpert")]
        public ActionResult AddExpert([FromBody] ServiceMetier.Expert expert)
        {
            if (service.addExpert(expert))
            {
                return CreatedAtAction("GetExpert", new { id = expert.Id }, expert);
            }
            return BadRequest();
        }

        [HttpPut("updateExpert/{id}")]
        public IActionResult UpdateExpert(int id, [FromBody] ServiceMetier.Expert updatedExpert)
        {
            if (updatedExpert == null)
            {
                return BadRequest("Invalid expert data.");
            }

            // Récupérer l'objet existant
            var existingExpert = service.GetExpert(id);
            if (existingExpert == null)
            {
                return NotFound();
            }

            // Mettre à jour uniquement les champs non nuls
            if (!string.IsNullOrEmpty(updatedExpert.Nom))
            {
                existingExpert.Nom = updatedExpert.Nom;
            }
            if (!string.IsNullOrEmpty(updatedExpert.Prenom))
            {
                existingExpert.Prenom = updatedExpert.Prenom;
            }
            if (!string.IsNullOrEmpty(updatedExpert.Adresse))
            {
                existingExpert.Adresse = updatedExpert.Adresse;
            }
            if (!string.IsNullOrEmpty(updatedExpert.Telephone))
            {
                existingExpert.Telephone = updatedExpert.Telephone;
            }
            if (!string.IsNullOrEmpty(updatedExpert.fonction))
            {
                existingExpert.fonction = updatedExpert.fonction;
            }

            // Appel du service de mise à jour
            if (service.updateExpert(id,existingExpert))
            {
                return NoContent();
            }
            return StatusCode(500, "A problem happened while handling your request.");
        }
        // DELETE: api/Expert/deleteExpert/5
        [HttpDelete("deleteExpert/{id}")]
        public IActionResult DeleteExpert(int id)
        {
            if (service.deleteExpert(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("searchExpert")]
        public ActionResult<IEnumerable<ServiceMetier.Expert>> SearchExperts([FromQuery] string nom, [FromQuery] string prenom)
        {
            var experts = service.SearchExperts(nom, prenom);
            return Ok(experts);
        }
    }
}
