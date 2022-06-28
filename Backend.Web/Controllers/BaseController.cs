using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Backend.Data;
using Backend.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Backend.Application.Infrastructure.Cookies;
using System.IdentityModel.Tokens.Jwt;

namespace HRM.Web.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultApiConventions), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    public class BaseController : ControllerBase
    {
        /*protected string GetUserId()
        {
            try
            {
                //string userId = (this.User.Claims.First(i => i.Type == "Id").Value).ToString();

                //return userId;
                var token = Request.Cookies[AuthCookiesValue.AuthKey];
                if (token == null)
                {
                    return "";
                }
                RefreshTokenRequestModel authToken = new RefreshTokenRequestModel();
                 authToken = Newtonsoft.Json.JsonConvert.DeserializeObject<RefreshTokenRequestModel>(token);
                if (authToken == null || authToken.JwtToken == null)
                {

                    return "";
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var temp = tokenHandler.ReadJwtToken(authToken.JwtToken);
                string userId = temp.Claims.First(i => i.Type == "Id").Value;
                return userId;

            }
            catch (Exception)
            {
                throw new Exception("Invalid Token!");
            }
        }*/
        protected string GetUserRole()
        {
            try
            {
                string role = (this.User.Claims.First(i => i.Type == "Role").Value);
                return role;
            }
            catch (Exception)
            {
                throw new Exception("Invalid Token!");
            }
        }
        protected string GetAppUrl()
        {
            try
            {
                string AppUrl = this.User.Claims.First(i => i.Type == "AppURL").Value;
                return AppUrl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        protected string GetUserType()
        {
            try
            {
                string userType = this.User.Claims.First(i => i.Type == "UserType").Value;
                return userType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}