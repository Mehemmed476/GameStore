namespace GameStore.PL.Areas.Admin.ViewModels.Game;

public class DetailsVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Price { get; set; }
    public int NewPrice { get; set; }
    public string GameId { get; set; }
    public int HowMany { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
}