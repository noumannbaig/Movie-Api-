using System;
using System.Collections.Generic;
using System.Text;


namespace Backend.Application.Interfaces
{
    public interface IUtility
    {
        string GetApplicationRootPath();
        int StartRecordNumber(int PageNo, int PageSize);
    }
}
