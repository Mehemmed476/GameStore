using GameStore.BL.Services.Abstractions;
using GameStore.CORE.Models;
using GameStore.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.PL.Controllers;

public class ProductDetailsController : Controller
{
    private readonly IGenericCRUDService _service;
    public ProductDetailsController(IGenericCRUDService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int id)
    {
        Game? game = await _service.GetByIdAsync<Game>(id);
        if (game == null) { return BadRequest(); }
        var gameComments = await _service.GetAllAsync<GameComment>();
        game.GameComments = gameComments.Where(gc => gc.GameId == id).ToList();

        ProductDetailVM productDetailVM = new ProductDetailVM()
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            GameId = game.GameId,
            ImageUrl = game.ImageUrl,
            Price = game.Price,
            NewPrice = game.NewPrice,
            HowMany = game.HowMany,
            GameComments = game.GameComments
        };
        return View(productDetailVM);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ProductDetailVM productDetailVM)
    {
        if(!ModelState.IsValid) { return View(productDetailVM); }
        GameComment gameComment = new GameComment() 
        {
            Comment = productDetailVM.newComment,
            GameId = productDetailVM.Id
        };

        await _service.CreateAsync(gameComment);

        Game? game = await _service.GetByIdAsync<Game>(productDetailVM.Id);
        if (game == null) { return BadRequest(); }
        var gameComments = await _service.GetAllAsync<GameComment>();
        game.GameComments = gameComments.Where(gc => gc.GameId == productDetailVM.Id).ToList();

        ProductDetailVM currentProductDetailVM = new ProductDetailVM()
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            GameId = game.GameId,
            ImageUrl = game.ImageUrl,
            Price = game.Price,
            NewPrice = game.NewPrice,
            HowMany = game.HowMany,
            GameComments = game.GameComments
        };
        return View(currentProductDetailVM);
    }
}