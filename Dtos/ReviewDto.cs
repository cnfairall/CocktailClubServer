namespace CocktailClub.Dtos
{
    public class ReviewDto
    {
        public int SavedCocktailId { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
        public string Grade { get; set; }
    }
}
