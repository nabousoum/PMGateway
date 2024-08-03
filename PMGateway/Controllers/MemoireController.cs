using Microsoft.AspNetCore.Mvc;
using ServiceMetier;

namespace PMGateway.Controllers
{
    public class MemoireController : ControllerBase
    {
        private readonly ServiceMetier.Service1Client service = new ServiceMetier.Service1Client();
        private readonly ServiceFile.ServiceFileClient serviceFile = new ServiceFile.ServiceFileClient();

        [HttpGet("getAllMemoires")]
        public ActionResult<IEnumerable<Memoire>> GetAllMemoires()
        {
            var memoires = service.GetAllMemoires();
            return Ok(memoires);
        }

        [HttpGet("getMemoire/{id}")]
        public ActionResult<Memoire> GetMemoire(int id)
        {
            var memoire = service.GetMemoire(id);
            if (memoire == null)
            {
                return NotFound();
            }
            return Ok(memoire);
        }

        [HttpPost("addMemoire")]
        public ActionResult AddMemoire([FromBody] Memoire memoire)
        {
            if (service.AddMemoire(memoire))
            {
                return CreatedAtAction("GetMemoire", new { id = memoire.Id }, memoire);
            }
            return BadRequest();
        }

        [HttpPut("updateMemoire/{id}")]
        public IActionResult UpdateMemoire(int id, [FromBody] Memoire updatedMemoire)
        {
            if (updatedMemoire == null || id != updatedMemoire.Id)
            {
                return BadRequest("Invalid memoire data.");
            }

            if (service.UpdateMemoire(updatedMemoire))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("deleteMemoire/{id}")]
        public IActionResult DeleteMemoire(int id)
        {
            if (service.DeleteMemoire(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost("uploadDocument")]
        public IActionResult UploadDocument()
        {
            var httpRequest = HttpContext.Request;
            var postedFile = httpRequest.Form.Files[0];
            var fileName = postedFile.FileName;

            using (var ms = new MemoryStream())
            {
                postedFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                bool isUploaded = serviceFile.UploadFile(fileBytes, fileName);

                if (isUploaded)
                {
                    return Ok(new { Message = "File uploaded successfully." });
                }
                return StatusCode(500, "An error occurred while uploading the file.");
            }
        }

        [HttpGet("downloadDocument/{fileName}")]
        public IActionResult DownloadDocument(string fileName)
        {
            var fileBytes = serviceFile.DownloadFile(fileName);
            if (fileBytes == null)
            {
                return NotFound("File not found.");
            }
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}
