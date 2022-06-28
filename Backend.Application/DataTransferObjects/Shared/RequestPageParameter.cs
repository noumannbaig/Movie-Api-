using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DataTransferObjects.Shared
{
    public class RequestPageParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public string OrderType { get; set; } = "DESC";

        public RequestPageParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 100;
        }

        public RequestPageParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
