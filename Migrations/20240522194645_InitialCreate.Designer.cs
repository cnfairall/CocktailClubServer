﻿// <auto-generated />
using CocktailClub;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CocktailClub.Migrations
{
    [DbContext(typeof(CCDbContext))]
    [Migration("20240522194645_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CocktailClub.Models.Glass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Glasses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Highball"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cocktail"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Punch Bowl"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Pitcher"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Collins"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Pousse cafe"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Champagne flute"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Copper mug"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Cordial"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Brandy snifter"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Wine"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Nick and Nora"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Hurricane"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Coffee mug"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Shot"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Pint"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Margarita"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Martini"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Balloon"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Coupe"
                        });
                });

            modelBuilder.Entity("CocktailClub.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Lemon Juice"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Gin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Maraschino Liqueur"
                        });
                });

            modelBuilder.Entity("CocktailClub.Models.SavedCocktail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DrinkId")
                        .HasColumnType("integer");

                    b.Property<int>("GlassId")
                        .HasColumnType("integer");

                    b.Property<string>("Grade")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Made")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<bool>("Public")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GlassId");

                    b.ToTable("SavedCocktails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DrinkId = 17180,
                            GlassId = 2,
                            Grade = "A",
                            ImageUrl = "https://www.thecocktaildb.com/images/media/drink/trbplb1606855233.jpg",
                            Instructions = "Add all ingredients into cocktail shaker filled with ice. Shake well and strain into cocktail glass. Garnish with a cherry.",
                            Made = true,
                            Name = "Aviation",
                            Notes = "Needed a bit more lemon",
                            Public = true,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            DrinkId = 11000,
                            GlassId = 1,
                            ImageUrl = "https://www.thecocktaildb.com/images/media/drink/metwgh1606770327.jpg",
                            Instructions = "Add all ingredients into cocktail shaker filled with ice. Shake well and strain into cocktail glass. Garnish with a cherry.",
                            Made = false,
                            Name = "Mojito",
                            Public = false,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("CocktailClub.Models.Spirit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Spirits");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Gin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Vodka"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Rum"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Bourbon"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Tequila"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Scotch"
                        },
                        new
                        {
                            Id = 7,
                            Name = "White wine"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Red wine"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Rosé"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Mezcal"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Pisco"
                        });
                });

            modelBuilder.Entity("CocktailClub.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "",
                            Email = "jenphen@gmail.com",
                            FirstName = "Jen",
                            ImageUrl = "",
                            LastName = "Marrow",
                            Uid = "HK475G8BK",
                            Username = "svengali"
                        },
                        new
                        {
                            Id = 2,
                            Bio = "",
                            Email = "lincolnlog@yahoo.com",
                            FirstName = "Linc",
                            ImageUrl = "",
                            LastName = "Wyatt",
                            Uid = "LL4910HEJ",
                            Username = "terrordactyl"
                        });
                });

            modelBuilder.Entity("IngredientSavedCocktail", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("integer");

                    b.Property<int>("SavedCocktailsId")
                        .HasColumnType("integer");

                    b.HasKey("IngredientsId", "SavedCocktailsId");

                    b.HasIndex("SavedCocktailsId");

                    b.ToTable("IngredientSavedCocktail");
                });

            modelBuilder.Entity("CocktailClub.Models.SavedCocktail", b =>
                {
                    b.HasOne("CocktailClub.Models.Glass", "Glass")
                        .WithMany()
                        .HasForeignKey("GlassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Glass");
                });

            modelBuilder.Entity("IngredientSavedCocktail", b =>
                {
                    b.HasOne("CocktailClub.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailClub.Models.SavedCocktail", null)
                        .WithMany()
                        .HasForeignKey("SavedCocktailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}