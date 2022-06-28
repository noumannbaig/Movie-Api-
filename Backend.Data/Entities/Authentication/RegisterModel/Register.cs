using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.Entities.Authentication.RegisterModel
{
    public class Register
    {
        [Required(ErrorMessage ="User Name is Required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }

    }
}
