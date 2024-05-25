﻿using CocktailClub.Models;
using CocktailClub.Dtos;

namespace CocktailClub.Api
{
    public class UserApi
    {
        public static void Map(WebApplication app)
        {
            //check user
            app.MapGet("/api/checkuser/{uid}", (CCDbContext db, string uid) =>
            {
                var user = db.Users.SingleOrDefault(u => u.Uid == uid);
                if (user == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(user);

                }
            });

            //register user
            app.MapPost("/api/register", (CCDbContext db, User userObj) =>
            {
                db.Users.Add(userObj);
                db.SaveChanges();
                return Results.Created($"/users/${userObj.Id}", userObj);
            });

            //get user
            app.MapGet("/api/users/{userId}", (CCDbContext db, int userId) => {
                User user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Results.NotFound("No such user");
                }
                return Results.Ok(user);
            });

            //delete user
            app.MapDelete("/api/users/{userId}", (CCDbContext db, int userId) =>
            {
                User user = db.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Results.NotFound("No such user");
                }
                db.Users.Remove(user);
                db.SaveChanges();
                return Results.Ok("Account deleted");

            });

            //update user
            app.MapPut("/api/users/{user.Id}", (CCDbContext db, User user) =>
            {
                User userToUpdate = db.Users.SingleOrDefault(u => u.Id == user.Id);
                if (userToUpdate == null)
                {
                    return Results.NotFound("No user found");
                }
                userToUpdate.Username = user.Username;
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Email = user.Email;
                userToUpdate.Bio = user.Bio;
                userToUpdate.ImageUrl = user.ImageUrl;

                db.SaveChanges();
                return Results.Ok(user);
            });

        }
    }
}
