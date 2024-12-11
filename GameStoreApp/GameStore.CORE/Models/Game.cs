using GameStore.CORE.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.CORE.Models
{
    public class Game : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public int NewPrice { get; set; }
        public string GameId { get; set; }
        public int HowMany { get; set; }
    }
}
