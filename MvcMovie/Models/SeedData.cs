using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models
{

    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider) {

            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
                { 
                if (context.Movie.Any()) 
                {

                    return;
                }

                //This adds several movies
                context.Movie.AddRange(
                    new Movie 
                    { 
                        Title = "When Harry met Sally",
                        ReleaseDate = DateTime.Parse("1989-01-01"),
                        Genre = "Romantic Comedy",
                        Price = 7
                    },
                    new Movie
                    {
                        Title = "Ghostbuster",
                        ReleaseDate = DateTime.Parse("2020-01-01"),
                        Genre = "Comedy",
                        Price = 10
                    },
                    new Movie
                    {
                        Title = "Ghostbuster 2",
                        ReleaseDate = DateTime.Parse("2021-01-01"),
                        Genre = "Comedy",
                        Price = 12
                    },
                    new Movie
                    {
                        Title = "Rio",
                        ReleaseDate = DateTime.Parse("2022-01-01"),
                        Genre = "Cartoon",
                        Price = 23
                    }

                    );
                context.SaveChanges();


                    



                
            }
                    
        }
    }
}
