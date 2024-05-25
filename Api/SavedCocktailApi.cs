using CocktailClub.Models;
using CocktailClub.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CocktailClub.Api
{
    public class SavedCocktailApi
    {
        public static void Map(WebApplication app)
        {
            //get user's saved cocktails
            app.MapGet("/api/savedcocktails/user/{userId}", (CCDbContext db, int userId) =>
            {
                List<SavedCocktail> savedCocktails = db.SavedCocktails
                .Include(c => c.Glass)
                .Include(c => c.CocktailIngredients)
                .ThenInclude(ci => ci.Ingredient)
                .Where(c => c.UserId == userId).ToList();
                if (savedCocktails == null)
                {
                    return Results.NotFound("No saved cocktails found");
                }
                else
                {
                    return Results.Ok(savedCocktails);

                }
            });

            //get public cocktails
            app.MapGet("/api/savedcocktails/public", (CCDbContext db) =>
            {
                List<SavedCocktail> publicCocktails = db.SavedCocktails
                .Include(c => c.Glass)
                .Include(c => c.CocktailIngredients)
                .ThenInclude(ci => ci.Ingredient)
                .Where(c => c.Public == true).ToList();
                if (publicCocktails == null)
                {
                    return Results.NotFound("No public cocktails found");
                }
                else
                {
                    return Results.Ok(publicCocktails);

                }
            });

            //get cocktail details by id
            app.MapGet("/api/savedcocktails/{id}", (CCDbContext db, int id) =>
            {
                SavedCocktail cocktail = db.SavedCocktails
                .Include (c => c.Glass)
                .Include(c => c.CocktailIngredients)
                .ThenInclude (ci => ci.Ingredient)
                .SingleOrDefault(c => c.Id == id);
                if (cocktail == null)
                {
                    return Results.NotFound("No cocktail found");
                }
                return Results.Ok(cocktail);
            });

            //save cocktail
            app.MapPost("/api/savedcocktails/save", (CCDbContext db, SavedCocktail cocktail) =>
            {
                db.SavedCocktails.Add(cocktail);
                db.SaveChanges();
                return Results.Created($"/savedcocktails/${cocktail.Id}", cocktail);

            });

            //review cocktail
            app.MapPatch("/api/savedcocktails/review", (CCDbContext db, ReviewDto review) =>
            {
                SavedCocktail cocktail = db.SavedCocktails.SingleOrDefault(c => c.Id == review.SavedCocktailId);
                if (cocktail == null)
                {
                    return Results.BadRequest("Cocktail not found");
                }
                cocktail.Made = true;
                cocktail.Notes = review.Notes;
                cocktail.Grade = review.Grade;
                db.SaveChanges();
                return Results.Ok("cocktail updated");
            });

            //share cocktail
            app.MapPatch("/api/savedcocktails/share", (CCDbContext db, int id) =>
            {
                SavedCocktail cocktail = db.SavedCocktails.SingleOrDefault(c => c.Id == id);
                if (cocktail == null)
                {
                    return Results.BadRequest("Cocktail not found");
                }
                cocktail.Public = true;
                db.SaveChanges();
                return Results.Ok("cocktail shared");
            });

            //edit cocktail
            app.MapPut("/api/savedcocktails/{cocktail.Id}", (CCDbContext db, SavedCocktail cocktail) =>
            {
                SavedCocktail cocktailToUpdate = db.SavedCocktails
                .Include(c => c.Glass)
                .Include(c => c.CocktailIngredients)
                .ThenInclude(ci => ci.Ingredient)
                .SingleOrDefault(c => c.Id == cocktail.Id);
                if (cocktailToUpdate == null)
                {
                    return Results.NotFound("No cocktail found");
                }
                cocktailToUpdate.Name = cocktail.Name;
                cocktailToUpdate.ImageUrl = cocktail.ImageUrl;
                cocktailToUpdate.Notes = cocktail.Notes;
                cocktailToUpdate.Grade = cocktail.Grade;
                cocktailToUpdate.GlassId = cocktail.GlassId;
                cocktailToUpdate.Instructions = cocktail.Instructions;
                cocktailToUpdate.CocktailIngredients = cocktail.CocktailIngredients;

                db.SaveChanges();
                return Results.Ok("cocktail updated");
            });

            //delete cocktail
            app.MapDelete("/api/savedcocktails/{id}", (CCDbContext db, int id) =>
            {
                SavedCocktail cocktail = db.SavedCocktails.SingleOrDefault(c => c.Id == id);
                if (cocktail == null)
                {
                    return Results.NotFound("No cocktail found");
                }
                db.SavedCocktails.Remove(cocktail);
                db.SaveChanges();
                return Results.Ok("cocktail removed");
            });

            //add public cocktail to saved
            app.MapPost("/api/savedcocktails/add", (CCDbContext db, int userId, SavedCocktail publicCocktail) =>
            {
                var newCocktail = new SavedCocktail()
                {
                    UserId = userId,
                    Name = publicCocktail.Name,
                    Instructions = publicCocktail.Instructions,
                    CocktailIngredients = publicCocktail.CocktailIngredients,
                    GlassId = publicCocktail.GlassId,
                    DrinkId = publicCocktail.DrinkId,
                };
                db.SavedCocktails.Add(newCocktail);
                db.SaveChanges();
                return Results.Created($"/savedcocktails/${newCocktail.Id}", newCocktail);
            });
        }
    }
}
