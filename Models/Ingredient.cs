namespace CocktailClub.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CocktailIngredient> CocktailIngredients { get; set; } = new List<CocktailIngredient>();
    }
}
