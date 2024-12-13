using GameStore.CORE.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.CORE.Models
{
    public class GameComment : BaseAuditableEntity
    {
        public string Comment {  get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
    }
}
