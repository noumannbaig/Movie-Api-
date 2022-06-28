using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI1.Models
{
    public class Ticket
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]

        public int Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime WatchDate { get; set; }

        public int? MoviesId { get; set; }

        public Movies Movie { get; set; }
        
    }
}
