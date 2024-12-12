using Microsoft.AspNetCore.Mvc;

namespace GameStore.PL.Controllers;

public class ProductDetailsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}