using CocktailClub.Models;

namespace CocktailClub
{
    //Cocktails.Db external API returns array of drink objects
    public class ExtCocktailResponse
    {
        public List<Cocktail> Drinks { get; set; }
    }

    //creates model for each object returned 
    public class Cocktail
    {
        public string IdDrink { get; set; }
        public string StrDrink { get; set; }
        public string StrDrinkAlternate { get; set; }
        public string StrTags { get; set; }
        public string StrVideo { get; set; }
        public string StrCategory { get; set; }
        public string StrIBA { get; set; }
        public string StrAlcoholic { get; set; }
        public string StrGlass { get; set; }
        public string StrInstructions { get; set; }
        public string StrInstructionsES { get; set; }
        public string StrInstructionsDE { get; set; }
        public string StrInstructionsFR { get; set; }
        public string StrInstructionsIT { get; set; }
        public string StrInstructionsZH_HANS { get; set; }
        public string StrInstructionsZH_HANT { get; set; }
        public string StrDrinkThumb { get; set; }
        public string StrIngredient1 { get; set; }
        public string StrIngredient2 { get; set; }
        public string StrIngredient3 { get; set; }
        public string StrIngredient4 { get; set; }
        public string StrIngredient5 { get; set; }
        public string StrIngredient6 { get; set; }
        public string StrIngredient7 { get; set; }
        public string StrIngredient8 { get; set; }
        public string StrIngredient9 { get; set; }
        public string StrIngredient10 { get; set; }
        public string StrIngredient11 { get; set; }
        public string StrIngredient12 { get; set; }
        public string StrIngredient13 { get; set; }
        public string StrIngredient14 { get; set; }
        public string StrIngredient15 { get; set; }
        public string StrMeasure1 { get; set; }
        public string StrMeasure2 { get; set; }
        public string StrMeasure3 { get; set; }
        public string StrMeasure4 { get; set; }
        public string StrMeasure5 { get; set; }
        public string StrMeasure6 { get; set; }
        public string StrMeasure7 { get; set; }
        public string StrMeasure8 { get; set; }
        public string StrMeasure9 { get; set; }
        public string StrMeasure10 { get; set; }
        public string StrMeasure11 { get; set; }
        public string StrMeasure12 { get; set; }
        public string StrMeasure13 { get; set; }
        public string StrMeasure14 { get; set; }
        public string StrMeasure15 { get; set; }
        public string StrImageSource { get; set; }
        public string StrImageAttribution { get; set; }
        public string StrCreativeCommonsConfirmed { get; set; }
        public string DateModified { get; set; }
    }

    //create DTO to store only needed data from response
    public class CocktailDto
    {
        public string IdDrink { get; set; }
        public string StrDrink { get; set; }
        public string StrCategory { get; set; }
        public string StrAlcoholic { get; set; }
        public string StrGlass { get; set; }
        public string StrInstructions { get; set; }
        public string StrDrinkThumb { get; set; }

        //allows us to combine ingredient and measurement into key/value pair, of which there are multiple
        public Dictionary<string, string> CocktailIngredients { get; set; }

        //method to transfer needed data into cocktail Dto
        public static IEnumerable<CocktailDto> FromCocktailResponse(ExtCocktailResponse cocktailResponse)
        {
            return cocktailResponse.Drinks.Select(c => new CocktailDto
            {
                IdDrink = c.IdDrink,
                StrDrink = c.StrDrink,
                StrCategory = c.StrCategory,
                StrAlcoholic = c.StrAlcoholic,
                StrGlass = c.StrGlass,
                StrInstructions = c.StrInstructions,
                StrDrinkThumb = c.StrDrinkThumb,
                CocktailIngredients = CreateCocktailIngredientsDictionary(c)
            });
        }

        //allows us to combine ingredient and measurement into key/value pair, of which there are multiple
        //API returns 15 ingredients and measures for every cocktail whether truthy or null
        //ensures correct ingredient is matched to correct measurement

        public static Dictionary<string, string> CreateCocktailIngredientsDictionary(Cocktail cocktail)
        {
            var cocktailIngredients = new Dictionary<string, string>();

            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient1, cocktail.StrMeasure1);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient2, cocktail.StrMeasure2);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient3, cocktail.StrMeasure3);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient4, cocktail.StrMeasure4);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient5, cocktail.StrMeasure5);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient6, cocktail.StrMeasure6);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient7, cocktail.StrMeasure7);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient8, cocktail.StrMeasure8);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient9, cocktail.StrMeasure9);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient10, cocktail.StrMeasure10);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient11, cocktail.StrMeasure11);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient12, cocktail.StrMeasure12);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient13, cocktail.StrMeasure13);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient14, cocktail.StrMeasure14);
            AddCocktailIngredient(cocktailIngredients, cocktail.StrIngredient15, cocktail.StrMeasure15);


            return cocktailIngredients;
        }
        //method to add needed cocktailIngredients to dictionary 
        public static void AddCocktailIngredient(Dictionary<string, string> cocktailIngredients, string ingredient, string measure)
        {
            if (!string.IsNullOrEmpty(ingredient) && !string.IsNullOrEmpty(measure))
            {
                cocktailIngredients[ingredient] = measure;
            }
        }

 
    }
}
