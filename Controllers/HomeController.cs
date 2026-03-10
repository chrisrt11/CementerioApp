using Microsoft.AspNetCore.Mvc;

namespace CementerioApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Mapa", "Cementerio");
    }
}
