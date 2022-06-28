using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Backend.Data.MultipleResultSet
{
   public class StoredProcedure
    {
        public SqlParameter[] Parameters { get; set; }
        public string Name { get; set; }
    }
}

