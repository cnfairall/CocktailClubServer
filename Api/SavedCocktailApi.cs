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
                } else
                {
                    return Results.Ok(cocktail);
                }
            });

            //create cocktail
            app.MapPost("/api/savedcocktails/create", (CCDbContext db, SavedCocktail cocktail) =>
            {
                db.SavedCocktails.Add(cocktail);
                db.SaveChanges();
                return Results.Created($"/savedcocktails/${cocktail.Id}", cocktail);

            });

          //save cocktail from external API
            app.MapPost("/api/savedcocktails/{userId}/save", (CCDbContext db, CocktailDto cocktailDto, int userId) =>
            {
                //check user has already saved this cocktail
                SavedCocktail sc = db.SavedCocktails.SingleOrDefault(sc => sc.UserId == userId && sc.DrinkId == Convert.ToInt16(cocktailDto.IdDrink));
                if (sc != null)
                {
                    return Results.BadRequest("Cocktail already saved");
                }

                //if not, find ingredients
                var ciList = new List<CocktailIngredient>();
                foreach (var item in cocktailDto.CocktailIngredients)
                {
                   Ingredient ingredient = db.Ingredients.SingleOrDefault(i => i.Name == item.Key.ToLower());
                   if (ingredient == null)
                   {
                      ciList.Add(new CocktailIngredient() { Ingredient = new Ingredient() { Name = item.Key.ToLower() }, Amount = item.Value });
                   } else
                   {
                      ciList.Add(new CocktailIngredient() { Ingredient = ingredient, Amount = item.Value });
                   }
                }
        
                //then find glass in db
                Glass glass = db.Glasses.SingleOrDefault(g => cocktailDto.StrGlass.Contains(g.Name));
                if (glass != null) //if we don't have to make a new glass, save cocktail
                {
                    var cocktailToSave = new SavedCocktail()
                    {
                        Name = cocktailDto.StrDrink,
                        DrinkId = Convert.ToInt16(cocktailDto.IdDrink),
                        UserId = userId,
                        GlassId = glass.Id,
                        ImageUrl = cocktailDto.StrDrinkThumb,
                        Instructions = cocktailDto.StrInstructions,
                        CocktailIngredients = ciList
                    };
                    db.SavedCocktails.Add(cocktailToSave);
                    db.SaveChanges();
                    return Results.Ok(cocktailToSave);
                }
                else //else create glass 
                {
                    var cocktail = new SavedCocktail()
                    {
                        Name = cocktailDto.StrDrink,
                        DrinkId = Convert.ToInt16(cocktailDto.IdDrink),
                        UserId = userId,
                        Glass = new Glass()
                        {
                            Name = cocktailDto.StrGlass
                        },
                        ImageUrl = cocktailDto.StrDrinkThumb,
                        Instructions = cocktailDto.StrInstructions,
                        CocktailIngredients= ciList
                    };
                    db.SavedCocktails.Add(cocktail);
                    db.SaveChanges();
                    return Results.Ok(cocktail);
                }
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
            app.MapPatch("/api/savedcocktails/{id}/share", (CCDbContext db, int id) =>
            {
                SavedCocktail cocktail = db.SavedCocktails.SingleOrDefault(c => c.Id == id);
                if (cocktail == null)
                {
                    return Results.BadRequest("Cocktail not found");
                }
                else
                {
                    cocktail.Public = true;
                    db.SaveChanges();
                    return Results.Ok("cocktail shared");
                }
            });

            //edit cocktail
            app.MapPut("/api/savedcocktails/edit", (CCDbContext db, SavedCocktail cocktail) =>
            {
                SavedCocktail cocktailToUpdate = db.SavedCocktails
                .Include(c => c.Glass)
                .Include(c => c.CocktailIngredients)
                .ThenInclude(ci => ci.Ingredient)
                .SingleOrDefault(c => c.Id == cocktail.Id);
                if (cocktailToUpdate != null)
                {
                   cocktailToUpdate.Name = cocktail.Name;
                   cocktailToUpdate.ImageUrl = cocktail.ImageUrl;
                   cocktailToUpdate.Notes = cocktail.Notes;
                   cocktailToUpdate.Grade = cocktail.Grade;
                   cocktailToUpdate.GlassId = cocktail.GlassId;
                   cocktailToUpdate.Instructions = cocktail.Instructions;
                   cocktailToUpdate.CocktailIngredients = cocktail.CocktailIngredients;

                   db.SaveChanges();
                   return Results.Ok("cocktail updated");
                } else
                {
                  return Results.NotFound("No cocktail found");
                }
            });

            //delete cocktail
            app.MapDelete("/api/savedcocktails/{id}", (CCDbContext db, int id) =>
            {
                SavedCocktail cocktail = db.SavedCocktails.SingleOrDefault(c => c.Id == id);
                if (cocktail != null)
                {
                    db.SavedCocktails.Remove(cocktail);
                    db.SaveChanges();
                    return Results.Ok("cocktail removed");
                } else
                {
                    return Results.NotFound("No cocktail found");
                }
            });

            //add public cocktail to saved
            app.MapPost("/api/savedcocktails/{cocktailId}/add/{userId}", (CCDbContext db, int userId, int cocktailId) =>
            {
                SavedCocktail cocktailToCopy = db.SavedCocktails
                .Include(c => c.CocktailIngredients)
                .SingleOrDefault(c => c.Id == cocktailId);
                if (cocktailToCopy == null)
                {
                    return Results.BadRequest("no cocktail found");
                } else
                {
                    var newCocktail = new SavedCocktail()
                    {
                        UserId = userId,
                        Name = cocktailToCopy.Name,
                        Instructions = cocktailToCopy.Instructions,
                        CocktailIngredients = cocktailToCopy.CocktailIngredients,
                        GlassId = cocktailToCopy.GlassId,
                        DrinkId = cocktailToCopy.DrinkId,
                        ImageUrl = cocktailToCopy.ImageUrl,
                    };
                    db.SavedCocktails.Add(newCocktail);
                    db.SaveChanges();
                    return Results.Created($"/savedcocktails/${newCocktail.Id}", newCocktail);
                }
            });
        }
    }
}
