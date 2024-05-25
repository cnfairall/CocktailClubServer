namespace CocktailClub.Models
{
    public class CocktailIngredient
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public Ingredient Ingredient { get; set; }
        public SavedCocktail SavedCocktail { get; set; }
    }
}
