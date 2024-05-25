using CocktailClub.Models;

namespace CocktailClub.Api
{
    public class SpiritApi
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/api/spirits", (CCDbContext db) =>
            {
                return db.Spirits;
            });

            app.MapGet("/api/spirits/{id}", (CCDbContext db, int id) =>
            {
                Spirit spirit = db.Spirits.SingleOrDefault(s => s.Id == id);
                if (spirit != null)
                {
                    return Results.Ok(spirit);
                }
                return Results.NotFound("spirit not found");
            });

            app.MapPost("/api/spirits", (CCDbContext db, Spirit spirit) =>
            {
                db.Spirits.Add(spirit);
                db.SaveChanges();
                return Results.Created($"/spirits/{spirit.Id}", spirit);
            });
        
        }

    }
}
