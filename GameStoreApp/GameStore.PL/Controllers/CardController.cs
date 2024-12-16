using System.Net;
using GameStore.BL.Services.Abstractions;
using GameStore.CORE.Models;
using GameStore.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameStore.PL.Controllers;

public class CardController : Controller
{
    private readonly IGenericCRUDService _service;

    public CardController(IGenericCRUDService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var card = GetCard();
        return View(card);
    }

    public async Task<IActionResult> AddCard(int gameId)
    {
        Game? game = await _service.GetByIdAsync<Game>(gameId);
        if (game == null) { return NotFound(); }

        CookieOptions options = new CookieOptions()
        {
            Expires = DateTimeOffset.Now.AddDays(7),
            HttpOnly = true,
        };
        
        var cardsVm = GetCard();
        if (cardsVm == null) { cardsVm = new CardsVM(); }
        
        var exitingCardItem = cardsVm.Cards.FirstOrDefault(p => p.GameId == gameId);

        if (exitingCardItem == null)
        {
            CardVM cardVm = new CardVM()
            {
                GameId = game.Id,
                Title = game.Title,
                ImageUrl = game.ImageUrl,
                NewPrice = game.NewPrice,
                HowMany = game.HowMany,
                Quantity = 1
            };
        
            cardsVm.Cards.Add(cardVm); 
        }
        else
        {
            exitingCardItem.Quantity = exitingCardItem.Quantity + 1;
        }
        
        var jsonCard = JsonConvert.SerializeObject(cardsVm);
        Response.Cookies.Append("cards", jsonCard, options);
        
        return RedirectToAction("Index", "Home");
    }

    public CardsVM GetCard()
    {
        var cards = Request.Cookies["cards"];
        if (cards is not null)
        {
            var cardsVm = JsonConvert.DeserializeObject<CardsVM>(cards);
            return cardsVm;
        }

        return null;
    }

    [HttpPost]
    public IActionResult Remove(int gameId)
    {
        var cards = GetCard();
        CardVM? card = cards?.Cards.FirstOrDefault(p => p.GameId == gameId);
        if (card is null) 
        { 
            return Json(new { success = false, message = "Card not found" }); 
        }
    
        cards.Cards.Remove(card);

        CookieOptions options = new CookieOptions()
        {
            Expires = DateTimeOffset.Now.AddDays(7),
            HttpOnly = true,
        };

        var jsonCard = JsonConvert.SerializeObject(cards);
        Response.Cookies.Append("cards", jsonCard, options); 

        return Json(new { success = true, gameId = gameId });
    }
}