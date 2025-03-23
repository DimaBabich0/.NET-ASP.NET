using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HW03.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                Movies?.Add(new Movie {
                    Title = "The Shawshank Redemption",
                    Year = 1994,
                    DirectorsName = "Frank Darabont",
                    Genre = "Drama",
                    PosterPath = "https://xl.movieposterdb.com/05_03/1994/0111161/xl_8494_0111161_3bb8e662.jpg",
                    Synopsis = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."
                });

                Movies?.Add(new Movie
                {
                    Title = "The Godfather",
                    Year = 1972,
                    DirectorsName = "Francis Ford Coppola",
                    Genre = "Drama",
                    PosterPath = "https://xl.movieposterdb.com/23_11/1972/68646/xl_the-godfather-movie-poster_a186b36f.jpg",
                    Synopsis = "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son."
                });

                Movies?.Add(new Movie
                {
                    Title = "The Dark Knight",
                    Year = 2008,
                    DirectorsName = "Christopher Nolan",
                    Genre = "Action",
                    PosterPath = "https://xl.movieposterdb.com/08_06/2008/468569/xl_468569_fe24b125.jpg",
                    Synopsis = "When a menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman, James Gordon and Harvey Dent must work together to put an end to the madness."
                });

                SaveChanges();
            }
        }
    }
}
