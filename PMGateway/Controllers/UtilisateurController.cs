using Microsoft.AspNetCore.Mvc;

namespace PMGateway.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly ServiceMetier.Service1Client service = new ServiceMetier.Service1Client();

        [HttpGet("getAllUsers")]
        public ICollection<ServiceMetier.Utilisateur> getAllUsers()
        {
            ICollection<ServiceMetier.Utilisateur> utilisateurs = service.getAllUsers();
            return utilisateurs;
        }
        // GET: api/Utilisateur/getUser/5
        [HttpGet("getUser/{id}")]
        public ActionResult<ServiceMetier.Utilisateur> GetUser(int id)
        {
            var utilisateur = service.GetUser(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // POST: api/Utilisateur/addUser
        [HttpPost("addUser")]
        public ActionResult AddUser([FromBody] ServiceMetier.Utilisateur utilisateur)
        {
            if (service.addUser(utilisateur))
            {
                return CreatedAtAction("GetUser", new { id = utilisateur.Id }, utilisateur);
            }
            return BadRequest();
        }

        // PUT: api/Utilisateur/updateUser/5
        [HttpPut("updateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] ServiceMetier.Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest();
            }

            if (service.updateUser(utilisateur))
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/Utilisateur/deleteUser/5
        [HttpDelete("deleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (service.deleteUser(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        // GET: api/Utilisateur/search
        [HttpGet("search")]
        public ActionResult<IEnumerable<ServiceMetier.Utilisateur>> SearchUtilisateurs([FromQuery] string nom, [FromQuery] string prenom)
        {
            var utilisateurs = service.GetUtilisateurs(nom, prenom);
            return Ok(utilisateurs);
        }
    }
}

