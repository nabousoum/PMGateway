using Microsoft.AspNetCore.Mvc;

namespace PMGateway.Controllers
{
    public class BibliothecaireController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
