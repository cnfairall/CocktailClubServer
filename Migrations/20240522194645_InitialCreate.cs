using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CocktailClub.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Glasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spirits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spirits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavedCocktails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DrinkId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GlassId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Instructions = table.Column<string>(type: "text", nullable: false),
                    Grade = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Made = table.Column<bool>(type: "boolean", nullable: false),
                    Public = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedCocktails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedCocktails_Glasses_GlassId",
                        column: x => x.GlassId,
                        principalTable: "Glasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientSavedCocktail",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "integer", nullable: false),
                    SavedCocktailsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientSavedCocktail", x => new { x.IngredientsId, x.SavedCocktailsId });
                    table.ForeignKey(
                        name: "FK_IngredientSavedCocktail_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientSavedCocktail_SavedCocktails_SavedCocktailsId",
                        column: x => x.SavedCocktailsId,
                        principalTable: "SavedCocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Glasses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Highball" },
                    { 2, "Cocktail" },
                    { 3, "Punch Bowl" },
                    { 4, "Pitcher" },
                    { 5, "Collins" },
                    { 6, "Pousse cafe" },
                    { 7, "Champagne flute" },
                    { 8, "Copper mug" },
                    { 9, "Cordial" },
                    { 10, "Brandy snifter" },
                    { 11, "Wine" },
                    { 12, "Nick and Nora" },
                    { 13, "Hurricane" },
                    { 14, "Coffee mug" },
                    { 15, "Shot" },
                    { 16, "Pint" },
                    { 17, "Margarita" },
                    { 18, "Martini" },
                    { 19, "Balloon" },
                    { 20, "Coupe" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Lemon Juice" },
                    { 2, "Gin" },
                    { 3, "Maraschino Liqueur" }
                });

            migrationBuilder.InsertData(
                table: "Spirits",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Gin" },
                    { 2, "Vodka" },
                    { 3, "Rum" },
                    { 4, "Bourbon" },
                    { 5, "Tequila" },
                    { 6, "Scotch" },
                    { 7, "White wine" },
                    { 8, "Red wine" },
                    { 9, "Rosé" },
                    { 10, "Mezcal" },
                    { 11, "Pisco" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "Email", "FirstName", "ImageUrl", "LastName", "Uid", "Username" },
                values: new object[,]
                {
                    { 1, "", "jenphen@gmail.com", "Jen", "", "Marrow", "HK475G8BK", "svengali" },
                    { 2, "", "lincolnlog@yahoo.com", "Linc", "", "Wyatt", "LL4910HEJ", "terrordactyl" }
                });

            migrationBuilder.InsertData(
                table: "SavedCocktails",
                columns: new[] { "Id", "DrinkId", "GlassId", "Grade", "ImageUrl", "Instructions", "Made", "Name", "Notes", "Public", "UserId" },
                values: new object[,]
                {
                    { 1, 17180, 2, "A", "https://www.thecocktaildb.com/images/media/drink/trbplb1606855233.jpg", "Add all ingredients into cocktail shaker filled with ice. Shake well and strain into cocktail glass. Garnish with a cherry.", true, "Aviation", "Needed a bit more lemon", true, 1 },
                    { 2, 11000, 1, null, "https://www.thecocktaildb.com/images/media/drink/metwgh1606770327.jpg", "Add all ingredients into cocktail shaker filled with ice. Shake well and strain into cocktail glass. Garnish with a cherry.", false, "Mojito", null, false, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientSavedCocktail_SavedCocktailsId",
                table: "IngredientSavedCocktail",
                column: "SavedCocktailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedCocktails_GlassId",
                table: "SavedCocktails",
                column: "GlassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientSavedCocktail");

            migrationBuilder.DropTable(
                name: "Spirits");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "SavedCocktails");

            migrationBuilder.DropTable(
                name: "Glasses");
        }
    }
}
