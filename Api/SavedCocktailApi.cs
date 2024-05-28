﻿using CocktailClub.Models;
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

            //create cocktail
            app.MapPost("/api/savedcocktails/create", (CCDbContext db, SavedCocktail cocktail) =>
            {
                db.SavedCocktails.Add(cocktail);
                db.SaveChanges();
                return Results.Created($"/savedcocktails/${cocktail.Id}", cocktail);

            });

            //save cocktail from external API: Part I: adding cocktail to db
            app.MapPost("/api/savedcocktails/{userId}/save", (CCDbContext db, CocktailDto cocktailDto, int userId) =>
            {
                //check if cocktail is already saved
                SavedCocktail sc = db.SavedCocktails.SingleOrDefault(sc => sc.DrinkId == Convert.ToInt16(cocktailDto.IdDrink));
                if (sc != null)
                {
                    return Results.BadRequest("Cocktail already saved");
                }
        
                //if not, find glass in db
                Glass glass = db.Glasses.SingleOrDefault(g => g.Name == cocktailDto.StrGlass);
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
                    };
                    db.SavedCocktails.Add(cocktailToSave);
                    db.SaveChanges();
                    return Results.Ok(cocktailToSave.Id);
                } //else create glass 
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
                };
                db.SavedCocktails.Add(cocktail);
                db.SaveChanges();
                return Results.Ok(cocktail);

            });

            //save cocktail: Part II: adding ingredients
            app.MapPatch("/api/savedcocktails/add/{cocktailId}", (CCDbContext db, int cocktailId, CocktailDto cocktailDto) =>
            {
                SavedCocktail cocktailToPatch = db.SavedCocktails
                .Include(c => c.CocktailIngredients)
                .ThenInclude(ci => ci.Ingredient)
                .SingleOrDefault(c => c.Id == cocktailId);
                if (cocktailToPatch == null)
                {
                    return Results.BadRequest("cocktail not found");
                }
                //find ingredients in db
                foreach (var item in cocktailDto.CocktailIngredients)
                {
                    Ingredient ingredient = db.Ingredients.SingleOrDefault(i => i.Name == item.Key);
                    if (ingredient != null)
                    {
                        var ci = new CocktailIngredient()
                        {
                            IngredientId = ingredient.Id,
                            SavedCocktailId = cocktailToPatch.Id,
                            Amount = item.Value
                        };
                        cocktailToPatch.CocktailIngredients.Add(ci);
                    } else

                    {
                        var c = new CocktailIngredient()
                        {
                            Ingredient = new Ingredient()
                            {
                                Name = item.Key,
                            },
                            SavedCocktailId = cocktailToPatch.Id,
                            Amount = item.Value
                        };
                        cocktailToPatch.CocktailIngredients.Add(c);
                    }     
                }
                db.SaveChanges();
                return Results.Ok(cocktailToPatch);

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

            //filter saved by spirit
            app.MapGet("/api/savedcocktails/spirit/{userId}/{spiritName}", (CCDbContext db, int userId, string spiritName) =>
            {
                var filteredCocktails = db.SavedCocktails
                .Include(sc => sc.CocktailIngredients)
                .ThenInclude(ci => ci.Ingredient)
                .Where(i => i.UserId == userId && i.Name.Contains(spiritName)).ToList();
                if (filteredCocktails == null)
                {
                    return Results.NotFound("no results found");
                }
                return Results.Ok(filteredCocktails);
            });

            //filter saved by glass
            app.MapGet("/api/savedcocktails/glass/{userId}/{glassName}", (CCDbContext db, int userId, string glassName) =>
            {
                var filteredCocktails = db.SavedCocktails
                .Include(sc => sc.Glass)
                .Where(i => i.UserId == userId && i.Name.Contains(glassName)).ToList();
                if (filteredCocktails == null)
                {
                    return Results.NotFound("no results found");
                }
                return Results.Ok(filteredCocktails);
            });
        }
    }
}
