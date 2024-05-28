using Newtonsoft.Json;

namespace CocktailClub.External_Api
{
    public class CocktailApi
    {
        public static void Map(WebApplication app)
        {
            var apiUrl = "https://www.thecocktaildb.com/api/json/v1/1";

            //get cocktail by name
            app.MapGet("/cocktails/name/{cocktailName}", async (string cocktailName) =>
            {

                using var client = new HttpClient();
                var response = await client.GetAsync($"{apiUrl}/search.php?s={cocktailName}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var cocktailResponse = JsonConvert.DeserializeObject<ExtCocktailResponse>(json);

                    if (cocktailResponse != null)
                    {
                        var cocktailDto = CocktailDto.FromCocktailResponse(cocktailResponse);
                        return Results.Ok(cocktailDto);
                    }
                    return Results.NotFound("no cocktails found");
                }

                return Results.StatusCode((int)response.StatusCode);
            });

            //get cocktails by spirit
            app.MapGet("/cocktails/spirit/{spiritName}", async (string spiritName) =>
            {

                using var client = new HttpClient();
                var response = await client.GetAsync($"{apiUrl}/filter.php?i={spiritName}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var cocktailResponse = JsonConvert.DeserializeObject<ExtCocktailResponse>(json);

                    if (cocktailResponse != null)
                    {
                        var cocktailDto = CocktailDto.FromCocktailResponse(cocktailResponse);
                        return Results.Ok(cocktailDto);
                    }
                    return Results.NotFound("no cocktails found");
                }

                return Results.StatusCode((int)response.StatusCode);
            });

            //get cocktails by glass
            app.MapGet("/cocktails/glass/{glassName}", async (string glassName) =>
            {

                using var client = new HttpClient();
                var response = await client.GetAsync($"{apiUrl}/filter.php?g={glassName}_glass");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var cocktailResponse = JsonConvert.DeserializeObject<ExtCocktailResponse>(json);
                    if (cocktailResponse != null)
                    {
                        var cocktailDto = CocktailDto.FromCocktailResponse(cocktailResponse);
                        return Results.Ok(cocktailDto);
                    }
                    return Results.NotFound("no cocktails found");
                }

                return Results.StatusCode((int)response.StatusCode);
            });

            //get random cocktail
            app.MapGet("/cocktails/random", async () =>
            {

                using var client = new HttpClient();
                var response = await client.GetAsync($"{apiUrl}/random.php");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var cocktailResponse = JsonConvert.DeserializeObject<ExtCocktailResponse>(json);

                    var cocktailDto = CocktailDto.FromCocktailResponse(cocktailResponse);
                    return Results.Ok(cocktailDto);
                }

                return Results.StatusCode((int)response.StatusCode);
            });
        }
    }
}
