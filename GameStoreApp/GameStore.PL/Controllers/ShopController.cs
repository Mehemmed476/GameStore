using Microsoft.AspNetCore.Mvc;

namespace GameStore.PL.Controllers;

public class ShopController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}