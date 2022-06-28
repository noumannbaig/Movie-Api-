using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.PayLoad = data;
            //this.Message = null;
            //this.Success = true;
            //this.Errors = null;
        }
    }
}
