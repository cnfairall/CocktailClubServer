using Microsoft.EntityFrameworkCore;
using CocktailClub.Models;

namespace CocktailClub
{
    public class CCDbContext : DbContext
    {
        public DbSet<SavedCocktail> SavedCocktails { get; set; }
        public DbSet<Glass> Glasses { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Spirit> Spirits { get; set; }
        public DbSet<User> Users { get; set; }

        public CCDbContext(DbContextOptions<CCDbContext> context) : base(context)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavedCocktail>().HasData(new SavedCocktail[]
            {
                    new SavedCocktail { Id = 1, Name = "Aviation", DrinkId = 17180, GlassId = 2, UserId = 1, ImageUrl = "https://www.thecocktaildb.com/images/media/drink/trbplb1606855233.jpg", Grade = "A", Instructions = "Add all ingredients into cocktail shaker filled with ice. Shake well and strain into cocktail glass. Garnish with a cherry.", Made = true, Notes = "Needed a bit more lemon", Public = true },
                    new SavedCocktail { Id = 2, Name = "Mojito", DrinkId = 11000, GlassId = 1, UserId = 1, ImageUrl = "https://www.thecocktaildb.com/images/media/drink/metwgh1606770327.jpg", Instructions = "Add all ingredients into cocktail shaker filled with ice. Shake well and strain into cocktail glass. Garnish with a cherry.", Made = false },

            });

            modelBuilder.Entity<Glass>().HasData(new Glass[]
            {
                    new Glass { Id = 1, Name = "Highball" },
                    new Glass { Id = 2, Name = "Cocktail" },
                    new Glass { Id = 3, Name = "Punch Bowl" },
                    new Glass { Id = 4, Name = "Pitcher" },
                    new Glass { Id = 5, Name = "Collins" },
                    new Glass { Id = 6, Name = "Pousse cafe" },
                    new Glass { Id = 7, Name = "Champagne flute" },
                    new Glass { Id = 8, Name = "Copper mug"},
                    new Glass { Id = 9, Name = "Cordial" },
                    new Glass { Id = 10, Name = "Brandy snifter" },
                    new Glass { Id = 11, Name = "Wine" },
                    new Glass { Id = 12, Name = "Nick and Nora" },
                    new Glass { Id = 13, Name = "Hurricane" },
                    new Glass { Id = 14, Name = "Coffee mug" },
                    new Glass { Id = 15, Name = "Shot" },
                    new Glass { Id = 16, Name = "Pint" },
                    new Glass { Id = 17, Name = "Margarita" },
                    new Glass { Id = 18, Name = "Martini" },
                    new Glass { Id = 19, Name = "Balloon" },
                    new Glass { Id = 20, Name = "Coupe" },
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient[]
            {
                    new Ingredient { Id = 1, Name = "Lemon Juice" },
                    new Ingredient { Id = 2, Name = "Gin" },
                    new Ingredient { Id = 3, Name = "Maraschino Liqueur" },
            });

            modelBuilder.Entity<Spirit>().HasData(new Spirit[]
           {
                   new Spirit { Id = 1, Name = "Gin" },
                   new Spirit { Id = 2, Name = "Vodka" },
                   new Spirit { Id = 3, Name = "Rum" },
                   new Spirit { Id = 4, Name = "Bourbon" },
                   new Spirit { Id = 5, Name = "Tequila" },
                   new Spirit { Id = 6, Name = "Scotch" },
                   new Spirit { Id = 7, Name = "White wine" },
                   new Spirit { Id = 8, Name = "Red wine" },
                   new Spirit { Id = 9, Name = "Rosé" },
                   new Spirit { Id = 10, Name = "Mezcal" },
                   new Spirit { Id = 11, Name = "Pisco" },

           });

            modelBuilder.Entity<User>().HasData(new User[]
          {
                   new User { Id = 1, Uid = "HK475G8BK", Username = "svengali", FirstName = "Jen", LastName = "Marrow", Bio = "", Email = "jenphen@gmail.com", ImageUrl = "" },
                   new User { Id = 2, Uid = "LL4910HEJ", Username = "terrordactyl", FirstName = "Linc", LastName = "Wyatt", ImageUrl = "", Email = "lincolnlog@yahoo.com", Bio = ""  },
          });
        }
    }
    
}