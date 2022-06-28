using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DataTransferObjects.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
