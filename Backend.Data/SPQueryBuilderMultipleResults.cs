using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Backend.Data
{
    public static class SPQueryBuilderMultipleResults
    {
        public static MultipleResultSetWrapper QueryMultipleResults(this DbContext db, string sql)
        {
            return new MultipleResultSetWrapper(db, sql);
        }
        public class MultipleResultSetWrapper
        {
            /// <summary>
            /// The database
            /// </summary>
            private readonly DbContext _db;
            /// <summary>
            /// The stored procedure
            /// </summary>
            private readonly string _storedProcedure;
            /// <summary>
            /// Initializes a new instance of the <see cref="MultipleResultSetWrapper" /> class.
            /// </summary>
            /// <param name="db">The database.</param>
            /// <param name="sql">The SQL.</param>
            public MultipleResultSetWrapper(DbContext db, string sql)
            {
                _db = db;
                _storedProcedure = sql;
            }
            /// <summary>
            /// Executes the specified parameters.
            /// </summary>
            /// <param name="parameters">The parameters.</param>
            /// <param name="returnTypes">The return types.</param>
            /// <returns></returns>
            public List<List<dynamic>> ExecuteMultipleResults(SqlParameter[] parameters, params Type[] types)
            {
                List<List<dynamic>> results = new List<List<dynamic>>();

                var connection = _db.Database.GetDbConnection();
                var command = connection.CreateCommand();
                command.CommandText = _storedProcedure;
                command.CommandType = CommandType.StoredProcedure;

                AddParameters(parameters, command);
                OpenConnection(command);

                int counter = 0;
                using (var reader = command.ExecuteReader())
                {
                    do
                    {
                        var innerResults = new List<dynamic>();

                        if (counter > types.Length - 1) { break; }

                        while (reader.Read())
                        {
                            var item = Activator.CreateInstance(types[counter]);
                            ItemMapper(item, reader);
                            innerResults.Add(item);
                        }
                        results.Add(innerResults);
                        counter++;
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
                return results;
            }

            private void ItemMapper(object item, System.Data.Common.DbDataReader reader)
            {
                for (int inc = 0; inc < reader.FieldCount; inc++)
                {
                    Type type = item.GetType();
                    string name = reader.GetName(inc);
                    PropertyInfo property = type.GetProperty(name);

                    if (property != null && name == property.Name)
                    {
                        var value = reader.GetValue(inc);
                        if (value != null && value != DBNull.Value)
                        {
                            property.SetValue(item, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType), null);
                        }
                    }
                }
            }

            private void OpenConnection(System.Data.Common.DbCommand command)
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

            }

            // function to add parameters in DB Command
            private void AddParameters(SqlParameter[] parameters, System.Data.Common.DbCommand command)
            {
                if (parameters != null && parameters.Any())
                {
                    command.Parameters.AddRange(parameters);
                }
            }
        }
    }
}
