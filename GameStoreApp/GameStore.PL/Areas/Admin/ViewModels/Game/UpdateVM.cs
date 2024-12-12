namespace GameStore.PL.Areas.Admin.ViewModels.Game;

public class UpdateVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile? Image { get; set; }
    public string? CurrentImageUrl { get; set; }
    public int Price { get; set; }
    public int NewPrice { get; set; }
    public string GameId { get; set; }
    public int HowMany { get; set; }
}