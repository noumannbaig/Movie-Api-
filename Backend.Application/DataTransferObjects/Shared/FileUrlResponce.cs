using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DataTransferObjects.Shared
{
    public class FileUrlResponce
    {
        public string URL { get; set; } = "";
        public string ThumbnailbUrl { get; set; } = "";
        public bool IsVideo { get; set; }
    }
}
