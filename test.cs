using CocktailClub.Models;
using CocktailClub;

namespace CocktailClub
{
    public class test
    {
        //if ingredient not in db, add to db
        var ingToAdd = new Ingredient()
        {
            Name = item.Key
        };
        db.Ingredients.Add(ingToAdd);
                            db.SaveChanges();


                        
                        var cocktailngredient = new CocktailIngredient()
                        {
                        }
                }




    db.SavedCocktails.Add(cocktailToSave);
                    db.SaveChanges();
                }



foreach (var item in cocktailDto.CocktailIngredients)
{
    Ingredient ingredient = db.Ingredients.SingleOrDefault(i => i.Name == item.Key);
    if (ingredient != null)
    {
        var cocktailIngredient = new CocktailIngredient()
        {
            Ingredient = ingredient,
            SavedCocktail = cocktail,
            Amount = item.Value
        };
    }
    if (ingredient == null)
    {
        var ingToAdd = new Ingredient()
        {
            Name = item.Key
        };
        db.Ingredients.Add(ingToAdd);
        db.SaveChanges();


    }
    var cocktailngredient = new CocktailIngredient()
    {
    }
                }

var cocktailIngredient = new CocktailIngredient();

//iterate over CI key-value pairs and find ingredient in db
foreach (var item in cocktailDto.CocktailIngredients)
{
    Ingredient ingredient = db.Ingredients.SingleOrDefault(i => i.Name == item.Key);
    //if (ingredient != null)
    //{
    cocktailIngredient.Ingredient = ingredient;
    cocktailIngredient.Amount = item.Value;
    cocktailIngredient.SavedCocktail = cocktailToSave;
    // }
}
var cocktailIngredients = new List<CocktailIngredient>();
cocktailIngredients.Add(cocktailIngredient);
