using Microsoft.AspNetCore.Mvc;

namespace PMGateway.Controllers
{
    public class AnneeAcademiqueController : Controller
    {
        private readonly ServiceMetier.Service1Client service = new ServiceMetier.Service1Client();
        // GET: api/AnneeAcademique
        [HttpGet("getAllAnneeAcademiques")]
        public ICollection<ServiceMetier.AnneeAcademique> getAnneeAcademique()
        {
            ICollection<ServiceMetier.AnneeAcademique> annees = service.GetAllAnneeAcademiques();
            return annees;
        }
        //[HttpGet("getAllAnneeAcademiques")]
        //public ActionResult<ICollection<ServiceMetier.AnneeAcademique>> GetAnneeAcademique()
        //{
        //    ICollection<ServiceMetier.AnneeAcademique> annees = service.GetAllAnneeAcademiques();
        //    return Ok(annees); // Return the data wrapped in an Ok() response for better clarity
        //}
        // GET: api/AnneeAcademique/5
        [HttpGet("getAnneeAcademique/{id}")]
        public ActionResult<ServiceMetier.AnneeAcademique> GetAnneeAcademique(int id)
        {
            var anneeAcademique = service.GetAnneeAcademique(id);

            if (anneeAcademique == null)
            {
                return NotFound();
            }

            return Ok(anneeAcademique);
        }

        // POST: api/AnneeAcademique
        [HttpPost("addAnneeAcademique")]
        public ActionResult<ServiceMetier.AnneeAcademique> PostAnneeAcademique([FromBody] ServiceMetier.AnneeAcademique anneeAcademique)
        {
            if (service.AddAnneeAcademique(anneeAcademique))
            {
                return CreatedAtAction("GetAnneeAcademique", new { id = anneeAcademique.Id }, anneeAcademique);
            }
            return BadRequest();
        }

        // PUT: api/AnneeAcademique/5
        [HttpPut("updateAnneeAcademique/{id}")]
        public IActionResult PutAnneeAcademique(int id, [FromBody] ServiceMetier.AnneeAcademique anneeAcademique)
        {
            if (id != anneeAcademique.Id)
            {
                return BadRequest();
            }

            if (service.UpdateAnneeAcademique(anneeAcademique))
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/AnneeAcademique/5
        [HttpDelete("deletAnneeAcademique/{id}")]
        public IActionResult DeleteAnneeAcademique(int id)
        {
            if (service.DeleteAnneeAcademique(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
