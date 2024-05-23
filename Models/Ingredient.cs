namespace CocktailClub.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SavedCocktail> SavedCocktails { get; set; }
    }
}
