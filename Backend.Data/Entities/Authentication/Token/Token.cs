using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.Entities.Authentication.Token
{
    public class Token
    {
        public string Tokens { get; set; }
        public string RefreshToken { get; set; }
    }
}
