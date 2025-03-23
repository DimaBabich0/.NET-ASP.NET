using System.ComponentModel.DataAnnotations;

namespace HW03.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Title { get; set; }
        
        [Display(Name = "Director")]
        public string? DirectorsName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Genre { get; set; }

        [Required]
        [Range(1000, 9999, ErrorMessage = "Year must be a four-digit number")]
        public int Year { get; set; }
        
        [Display(Name = "Poster file")]
        public string? PosterPath { get; set; }

        [StringLength(200)]
        public string? Synopsis { get; set; }
    }
}
