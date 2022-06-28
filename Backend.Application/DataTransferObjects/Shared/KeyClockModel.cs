using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.DataTransferObjects.Shared
{
    public class KeyClockModel
    {
        public static string ClientSecret { get; set; }
        public static string ClientId { get; set; }
        public static string GrantType { get; set; }
        public static string Scope { get; set; }
        public static string BaseUrl { get; set; }
        public static string ProfileBaseUrl { get; set; }
    }
}
