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
        private readonly IFileService _fileService;
        IWebHostEnvironment _webHostEnvironment;
        public GameController(IGenericCRUDService service, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Game> games = await _service.GetAllAsync<Game>();
            List<Game> activeGames = games.Where(g => !g.IsDeleted).ToList();
            
            List<IndexVM> indexVMs = new List<IndexVM>();
            foreach (Game game in activeGames)
            {
                IndexVM indexVm = new IndexVM()
                {
                    Id = game.Id,
                    Title = game.Title,
                    Description = game.Description,
                    Price = game.Price,
                    NewPrice = game.NewPrice,
                    ImageUrl = game.ImageUrl,
                    GameId = game.GameId,
                    HowMany = game.HowMany,
                };

                indexVMs.Add(indexVm);
            }
            return View(indexVMs);
        }

        public async Task<IActionResult> Trash(int? id)
        {
            IEnumerable<Game> games = await _service.GetAllAsync<Game>();
            List<Game> activeGames = games.Where(g => g.IsDeleted).ToList();
            
            List<TrashVM> trashVms = new List<TrashVM>();
            foreach (Game game in activeGames)
            {
                TrashVM trashVm = new TrashVM()
                {
                    Id = game.Id,
                    Title = game.Title,
                    Description = game.Description,
                    ImageUrl = game.ImageUrl
                };

                trashVms.Add(trashVm);
            }
            return View(trashVms); 
        }

        public async Task<IActionResult> Details(int? id)
        {
            Game? game = await _service.GetByIdAsync<Game>(id ?? default);
            
            if (game == null) { return BadRequest("Game not found"); }

            DetailsVM detailsVm = new DetailsVM()
            {
                Id = game.Id,
                CreatedDate = game.CreatedDate,
                ModifiedDate = game.ModifiedDate,
                IsDeleted = game.IsDeleted,
                DeletedDate = game.DeletedDate,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                NewPrice = game.NewPrice,
                ImageUrl = game.ImageUrl,
                GameId = game.GameId,
                HowMany = game.HowMany
            };
            
            return View(detailsVm);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM createVm)
        {
            if (!ModelState.IsValid) { return View(createVm); }
            
            Game game = new Game()
            {
                Title = createVm.Title,
                Description = createVm.Description,
                Price = createVm.Price,
                NewPrice = createVm.NewPrice,
                GameId = createVm.GameId,
                HowMany = createVm.HowMany
            };
            
            game.ImageUrl = await _fileService.SaveFileAsync(createVm.Image, _webHostEnvironment.WebRootPath, new [] {".jpg", ".jpeg", ".png", ".webp" });
            
            await _service.CreateAsync(game);
            
            return RedirectToAction(nameof(Index));
        } 
        
        public async Task<IActionResult> Update(int id)
        {
            Game? game = await _service.GetByIdAsync<Game>(id);
            
            if (game == null) { return NotFound(); }

            UpdateVM updateVm = new UpdateVM
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                CurrentImageUrl = game.ImageUrl,
                Price = game.Price,
                NewPrice = game.NewPrice,
                GameId = game.GameId,
                HowMany = game.HowMany
            };

            return View(updateVm);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateVM updateVm)
        {
            if (!ModelState.IsValid) { return View(updateVm); }

            Game? game = await _service.GetByIdAsync<Game>(updateVm.Id);
            
            if (game == null) { return NotFound(); }

            game.Title = updateVm.Title;
            game.Description = updateVm.Description;
            game.Price = updateVm.Price;
            game.NewPrice = updateVm.NewPrice;
            game.GameId = updateVm.GameId;
            game.HowMany = updateVm.HowMany;

            if (updateVm.Image != null)
            {
                if (!string.IsNullOrEmpty(game.ImageUrl))
                {
                    _fileService.DeleteFile(game.ImageUrl, _webHostEnvironment.WebRootPath);
                }
                
                game.ImageUrl = await _fileService.SaveFileAsync(updateVm.Image, _webHostEnvironment.WebRootPath, new[] { ".jpg", ".jpeg", ".png", ".webp" });
            }

            await _service.UpdateAsync(game);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SoftDelete(int id)
        {
            Game? game = await _service.GetByIdAsync<Game>(id);
            
            if (game == null) { return NotFound(); }
            
            await _service.SoftDeleteAsync<Game>(id);
            
            return RedirectToAction(nameof(Index)); 
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            Game? game = await _service.GetByIdAsync<Game>(id);
            
            if (game == null) { return NotFound(); }
            
            _fileService.DeleteFile(game.ImageUrl, _webHostEnvironment.WebRootPath);
            await _service.DeleteAsync<Game>(id);
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int id)
        {
            Game? game = await _service.GetByIdAsync<Game>(id);
            
            if (game == null) { return NotFound(); }
            
            await _service.RestoreAsync<Game>(id); 
           
            return RedirectToAction(nameof(Index));
        }
    }
}