using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data {
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id=Guid.NewGuid(), Name = "Fireball", Damage = 25 },
                new Skill { Id=Guid.NewGuid(), Name = "Frenzy", Damage = 15 },
                new Skill { Id=Guid.NewGuid(), Name = "Zap", Damage = 20 }
            );
        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();
    }
}