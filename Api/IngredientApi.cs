using CocktailClub.Models;

namespace CocktailClub.Api
{
    public class IngredientApi
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/ingredients", (CCDbContext db) =>
            {
                return db.Ingredients;
            });

            app.MapGet("/api/ingredients/{id}", (CCDbContext db, int id) =>
            {
                Ingredient ingredient = db.Ingredients.SingleOrDefault(i => i.Id == id);
                if (ingredient == null)
                {
                    return Results.NotFound("ingredient not found");
                }
                return Results.Ok(ingredient);
            });

            app.MapPost("/api/ingredients", (CCDbContext db, Ingredient ingredient) =>
            {
                db.Ingredients.Add(ingredient);
                db.SaveChanges();
                return Results.Created($"/ingredients/{ingredient.Id}", ingredient);
            });
        }
    }
}
