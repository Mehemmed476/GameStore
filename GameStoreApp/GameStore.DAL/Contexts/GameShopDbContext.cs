using GameStore.CORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Contexts
{
    public class GameShopDbContext : DbContext
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=GameStoreDB;User ID=SA; Password=reallyStrongPwd123;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True");
        }*/
        public GameShopDbContext(DbContextOptions<GameShopDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameComment> GameComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GameComment>()
            .HasOne(e => e.Game)
            .WithMany(e => e.GameComments)
            .HasForeignKey(e => e.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
