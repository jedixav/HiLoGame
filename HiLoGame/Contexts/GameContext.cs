using HiLoGame.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HiLoGame.Context
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public DbSet<GameEntity> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameContext).Assembly);
        }
    }
}
