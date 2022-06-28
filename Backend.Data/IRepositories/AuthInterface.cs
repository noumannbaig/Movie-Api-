using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Data.Entities.Authentication.LoginModel;
using Backend.Data.Entities.Authentication.Token;
using Backend.Data.Entities.Authentication.Users;
namespace Backend.Data.IRepositories
{
    public interface AuthInterface
    {
        Token Authenticate(Users users);

    }
}
