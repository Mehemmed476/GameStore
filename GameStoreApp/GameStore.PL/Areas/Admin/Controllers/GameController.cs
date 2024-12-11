using GameStore.BL.Services.Abstractions;
using GameStore.CORE.Models;
using GameStore.PL.Areas.Admin.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private readonly IGenericCRUDService _service;

        public GameController(IGenericCRUDService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Game> games = await _service.GetAllAsync<Game>();
            List<IndexVM> indexVMs = new List<IndexVM>();
            foreach (Game game in games)
            {
                IndexVM indexVM = new IndexVM()
                {
                    Title = game.Title,
                    Description = game.Description,
                    Price = game.Price,
                    NewPrice = game.NewPrice,
                    ImageUrl = game.ImageUrl,
                    GameId = game.GameId,
                    HowMany = game.HowMany,
                };

                indexVMs.Add(indexVM);
            }


            return View(indexVMs);
        }
    }
}
