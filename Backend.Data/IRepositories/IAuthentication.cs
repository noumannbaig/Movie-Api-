using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Data.Entities.Authentication.LoginModel;
using Backend.Data.Entities.Authentication.RegisterModel;
using Backend.Application.DataTransferObjects.DTO;
using Backend.Data.Entities.Authentication.Token;

namespace Backend.Data.IRepositories
{
    public interface IAuthentication
    {
       Task< Token> Login(Login _loginData);
        Task<Login> Register(RegisterDTO _registerData);
    }
}
