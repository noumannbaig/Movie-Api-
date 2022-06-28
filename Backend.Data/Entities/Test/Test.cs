using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities.Test
{
    public class Test:BaseEntity
    {
        [Key]
        public Guid TestId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Test Key should be less than 50 characters")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Key { get; set; }

        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Test Value should be less than 50 characters")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Value  { get; set; }
    }
}
