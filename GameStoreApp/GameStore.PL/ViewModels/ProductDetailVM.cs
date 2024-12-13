using GameStore.CORE.Models;

namespace GameStore.PL.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string? GameId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? Price { get; set; }
        public int? NewPrice { get; set; }
        public int? HowMany { get; set; }
        public ICollection<GameComment>? GameComments { get; set; }
        public string newComment { get; set; }

    }
}
