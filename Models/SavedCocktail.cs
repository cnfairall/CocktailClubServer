namespace CocktailClub.Models
{
    public class SavedCocktail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DrinkId { get; set; }
        public int UserId { get; set; }
        public int GlassId { get; set; }
        public Glass Glass { get; set; }
        public string ImageUrl { get; set; }
        public string Instructions { get; set; }
        public string? Grade {  get; set; }
        public string? Notes { get; set; }
        public bool Made { get; set; }
        public bool Public { get; set; }
        public List<CocktailIngredient> CocktailIngredients { get; set; } = new List<CocktailIngredient>();

    }
}
