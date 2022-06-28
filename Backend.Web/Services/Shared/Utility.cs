using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace Backend.Web.Services.Shared
{
    public class Utility : IUtility
    {
        public IHostingEnvironment _appEnvironment;
        public IConfiguration Configurations { get; }
        public Utility(IHostingEnvironment hosting, IConfiguration config)
        {
            _appEnvironment = hosting;
            Configurations = config;
        }

        public string GetApplicationRootPath()
        {
            string path = string.Empty;
            path = _appEnvironment.ContentRootPath;
            return path;
        }

        public int StartRecordNumber(int PageNo, int PageSize)
        {
            return (PageNo - 1) * PageSize;
        }
    }
}
