using Backend.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Web.Services.Shared
{
    public class AuthToken
    {
        /*public RefreshTokenRequestModel GetRefreshTokenService(AuthorizationFilterContext context, RefreshTokenRequestModel model)
        {
            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(model.JwtToken);
            var UserId = principal.Claims.FirstOrDefault().Value;
            LoginModel loginModel = new LoginModel
            {
                RefreshToken = model.RefreshToken
            };
            var dbContext = context.HttpContext
          .RequestServices
          .GetService(typeof(EFDataContext)) as EFDataContext;

            dynamic UserSession = dbContext.UserDevice.Where(m => m.RefreshToken == model.RefreshToken && m.UserId == Guid.Parse(UserId)).FirstOrDefault();// _authRepository.UserSession(loginModel, UserID);
            if (UserSession != null)
            {
                if (UserSession.RefreshTokenExpiryTime != null)
                {
                    DateTime d1 = DateTime.UtcNow;
                    DateTime d2 = Convert.ToDateTime(UserSession.RefreshTokenExpiryTime);

                    TimeSpan t = d1 - d2;
                    double NoOfDays = t.TotalDays;
                    if (NoOfDays <= 0)
                    {
                        if (UserSession.UserId != UserId)
                        {
                            return null;

                        }
                        else
                        {
                            var newRefreshToken =  GenerateRefreshToken().Result;
                            var newJwtToken = GenerateAccessToken(principal.Claims);

                            bool refreshtoken_result = SaveRefreshToken(context, model.RefreshToken, newRefreshToken,UserSession.UserId, "", loginModel);
                            RefreshTokenRequestModel obj = new RefreshTokenRequestModel()
                            {
                                JwtToken = newJwtToken,
                                RefreshToken = newRefreshToken
                            };
                            return obj;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BackendSecretKey1122#$%234"));//_appSettings.Secret

            var jwtToken = new JwtSecurityToken(issuer: null,
                audience: "Anyone",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(60),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            await Task.Run(() =>
            {

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                }
            });
            return Convert.ToBase64String(randomNumber);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("BackendSecretKey1122#$%234")),//_appSettings.Secret
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            return principal;
        }
        public bool SaveRefreshToken(AuthorizationFilterContext context,string OldRefreshToken, string RefreshToken, Guid UserId, string macAddress, LoginModel loginObj)
        {
            var dbContext = context.HttpContext
          .RequestServices
          .GetService(typeof(EFDataContext)) as EFDataContext;

            UserDevice UserDevice = dbContext.UserDevice.Where(m => m.RefreshToken == OldRefreshToken && m.UserId == UserId).FirstOrDefault();
            if (UserDevice != null)
            {
                UserDevice.UserId = UserId;
                UserDevice.RefreshToken = RefreshToken;
                UserDevice.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
                UserDevice.Status = "active";
                UserDevice.RefreshTokenCreatedAt = DateTime.Now;
                UserDevice.RefreshTokenUpdatedAt = DateTime.Now;
            }
            else
            {
                UserDevice obj = new UserDevice
                {
                    LogOutAt = DateTime.Now,
                    UserId = UserId,
                    RefreshToken = RefreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1),
                    RefreshTokenUpdatedAt = DateTime.Now,
                    RefreshTokenCreatedAt = DateTime.Now,
                    Status = "active"
                };
                dbContext.UserDevice.Add(obj);

            }
            dbContext.SaveChanges();

            return true;
        }*/
    }
}
