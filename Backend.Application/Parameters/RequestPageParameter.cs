using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Parameters
{
   public class RequestPageParameterOld
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string OrderType { get; set; } = "DESC";
        public string SearchString { get; set; }

        public RequestPageParameterOld()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public RequestPageParameterOld(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
