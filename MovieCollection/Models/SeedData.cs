using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollection.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder application)
        {
            MovieDbContext context = application.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<MovieDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Movies.Any())
            {
                context.Movies.AddRange(

                    new Movie
                    {
                        Category = "Action/Adventure",
                        Title = "Avengers, The",
                        Year = 2012,
                        Director = "Joss Whedon", 
                        Rating = "PG-13",
                        Edited = "N",
                    },
                    new Movie
                    {
                        Category = "Action/Adventure",
                        Title = "District 9",
                        Year = 2009,
                        Director = "Neill Blomkamp",
                        Rating = "R",
                        Edited = "Yes",
                    },
                    new Movie
                    {
                        Category = "Comedy",
                        Title = "Planes Trains and Automobiles",
                        Year = 1987,
                        Director = "John Hughes",
                        Rating = "R",
                        Edited = "Yes",
                        Lent_to = "Dave",
                    },
                    new Movie
                    {
                        Category = "Miscellaneous",
                        Title = "Bellydance Core Conditiong",
                        Year = 2005,
                        Director = "Neena & Veena",
                        Rating = "NR",
                        Edited = "No"
                    },
                    new Movie
                    {
                        Category = "Horror",
                        Title = "Phantom of the Opera, The",
                        Year = 1925,
                        Director = "Lon Chaney, Sr., Edward Sedgwick, Rupert Julian, Ernst Laemmle",
                        Rating = "UR",
                        Edited = "No",
                    },
                    new Movie
                    {
                        Category = "Famliy/Fantasy",
                        Title = "Raya and the Last Dragon",
                        Year = 2021,
                        Director = "Carlos Lopez Estrada, Don Hall",
                        Rating = "PG",
                        Edited = "No",
                    }

                );

                context.SaveChanges();

            }
        }
    }
}
