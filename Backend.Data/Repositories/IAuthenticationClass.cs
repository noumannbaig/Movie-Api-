//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Backend.Application.DataTransferObjects.DTO;
//using Backend.Data.Entities.Authentication.LoginModel;
//using Backend.Data.IRepositories;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Backend.Application.IdentityAuth;
//using Microsoft.Extensions.Configuration;
//using Backend.Data.Entities.Authentication.RegisterModel;
//using Backend.Application.IdentityAuth.Status;
//using Backend.Data.Entities.Authentication;
//using System.Security.Claims;
//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.IdentityModel.Tokens;
//using Backend.Data.Entities.Authentication.Token;

//namespace Backend.Data.Repositories
//{
//    public class IAuthenticationClass : IAuthentication
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly IConfiguration _iconfiguration;
//        public async Task <Token> Login(Login _loginData)
//        {
//            var user = await _userManager.FindByNameAsync(_loginData.Username);
//            if (user != null && await _userManager.CheckPasswordAsync(user, _loginData.Password))
//            {
//                var userRoles = await _userManager.GetRolesAsync(user);

//                var authClaims = new List<Claim>
//{
//             new Claim(ClaimTypes.Name, user.UserName),
//             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//};

//                foreach (var userRole in userRoles)
//                {
//                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
//                }

//                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]));

//                var token = new JwtSecurityToken(
//                issuer: _iconfiguration["JWT: ValidIssuer"],
//                audience: _iconfiguration["JWT: ValidAudience"],
//                expires: DateTime.Now.AddHours(3),
//                claims: authClaims,
//                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//                );
//                return token;
//                //return Ok(new
//                //{
//                //    token = new JwtSecurityTokenHandler().WriteToken(token),
//                //    expiration = token.ValidTo
//                //});
//            }
//            return null;
//            //return Unauthorized();
//            //throw new NotImplementedException();
//        }

//        public Task<Login> Register(RegisterDTO _registerData)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
