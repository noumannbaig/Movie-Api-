using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.DataTransferObjects.Shared
{
    public class TicketsModel
    {
        public static string BaseUrl { get; set; }
        public static string Company { get; set; }
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string Encrypted { get; set; }
        public static string LanguageId { get; set; }
    }
}