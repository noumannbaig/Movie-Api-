using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Infrastructure.Cookies
{
    public class AuthCookiesValue
    {
        public readonly static string AuthKey = "authToken";
        // hours
        public readonly static int Expires = 720;
    }
}
