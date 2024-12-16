namespace GameStore.PL.ViewModels;

public class CardVM
{
    public int GameId { get; set; }
    public string Title { get; set; }
    public int NewPrice { get; set; }
    public string ImageUrl { get; set; }
    public int HowMany { get; set; }
    public int Quantity { get; set; }
}