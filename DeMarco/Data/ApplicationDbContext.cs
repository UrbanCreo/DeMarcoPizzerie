using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeMarco.Models;

namespace DeMarco.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DeMarco.Models.Pizza> Pizza { get; set; } = default!;
        public DbSet<DeMarco.Models.Article> Article { get; set; } = default!;
        public DbSet<DeMarco.Models.HomeArticle> HomeArticle { get; set; } = default!;
        public DbSet<DeMarco.Models.Pasta> Pasta { get; set; } = default!;
        public DbSet<DeMarco.Models.Food> Food { get; set; } = default!;
        public DbSet<DeMarco.Models.Fries> Fries { get; set; } = default!;
        public DbSet<DeMarco.Models.Addon> Addon { get; set; } = default!;
        public DbSet<DeMarco.Models.AlcoholicDrink> AlcoholicDrink { get; set; } = default!;
        public DbSet<DeMarco.Models.NonAlcoholicDrink> NonAlcoholicDrink { get; set; } = default!;
    }
}