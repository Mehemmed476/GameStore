using GameStore.CORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Contexts
{
    public class GameShopDbContext : DbContext
    {
        public GameShopDbContext(DbContextOptions<GameShopDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
    }
}
