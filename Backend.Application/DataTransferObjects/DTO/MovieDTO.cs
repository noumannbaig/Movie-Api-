using System;
using System.ComponentModel.DataAnnotations;
namespace MovieAPI1.DTO
{
    public class MovieDTO
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Genre { get; set; }
    }
}
