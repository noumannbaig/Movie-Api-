using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Data.MultipleResultSet
{
 public   class SqlParameterConfig
    {
        public bool IsDataList { get; set; }
        public SqlParameterConfig(bool _isDataList)
        {
            IsDataList = _isDataList;
        }
    }
}
