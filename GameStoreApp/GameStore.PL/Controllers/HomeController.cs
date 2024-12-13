using GameStore.BL.Services.Abstractions;
using GameStore.CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenericCRUDService _service;
        public HomeController(IGenericCRUDService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Game> games = await _service.GetAllAsync<Game>();
            List<Game> lastFourGame = games.ToList().OrderByDescending(x => x.Id).Take(4).ToList();
            return View(lastFourGame);
        }
    }
}
