using System.ComponentModel.DataAnnotations;

namespace HW03.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [Display(Name = "Director")]
        public string? DirectorsName { get; set; }
        public string? Genre { get; set; }
        public int Year { get; set; }
        [Display(Name = "Poster URL")]
        public string? PosterPath { get; set; }
        public string? Synopsis { get; set; }
    }
}
