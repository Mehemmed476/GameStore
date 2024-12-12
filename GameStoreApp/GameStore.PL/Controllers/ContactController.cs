using Microsoft.AspNetCore.Mvc;

namespace GameStore.PL.Controllers;

public class ContactController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}