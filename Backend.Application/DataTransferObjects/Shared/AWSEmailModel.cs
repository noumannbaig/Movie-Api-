using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.DataTransferObjects.Shared
{
    public class AWSEmailModel
    {
        public static string AccessKey { get; set; }
        public static string SecretKey { get; set; }
        public static string Region { get; set; }
    }
}
