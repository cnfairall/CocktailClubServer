using CocktailClub.Models;

namespace CocktailClub.Api
{
    public class GlassApi
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/glasses", (CCDbContext db) =>
            {
                return db.Glasses;
            });

            app.MapGet("/api/glasses/{id}", (CCDbContext db, int id) =>
            {
                Glass glass = db.Glasses.SingleOrDefault(g => g.Id == id);
                if (glass != null)
                {
                    return Results.Ok(glass);
                }
                return Results.NotFound("glass not found");
            });

            app.MapPost("/api/glasses", (CCDbContext db, Glass glass) =>
            {
                db.Glasses.Add(glass);
                db.SaveChanges();
                return Results.Created($"/glasses/{glass.Id}", glass);
            });

        }
    }
}
