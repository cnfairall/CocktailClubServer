namespace CocktailClub.Models
{
    public class CocktailIngredient
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int SavedCocktailId { get; set; }
        public SavedCocktail SavedCocktail { get; set; }
    }
}
