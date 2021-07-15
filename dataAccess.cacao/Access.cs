using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace dataAccess.cacao
{
    public class Access : IDisposable
    {
        string connectionS = ConfigurationManager.ConnectionStrings["cacaodb"].ConnectionString;
        SqlConnection connection;
        private const Int32 TIMEOUT = 300;

        public Access()
        {
            connection = new SqlConnection(connectionS);
        }

        public SqlParameter CreateParameter(string nombre, DbType tipo, object valor)
        {
            SqlParameter param = new SqlParameter
            {
                ParameterName = nombre,
                DbType = tipo,
                Value = valor ?? DBNull.Value
            };

            return param;
        }

        public SqlCommand CreateSqlCommand(string queryName, List<SqlParameter> parameters = null, bool isStoreProcedure = false)
        {
            SqlCommand command = new SqlCommand(queryName)
            {
                CommandType = isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text,
                Connection = connection
            };

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }
            command.CommandTimeout = TIMEOUT;
            return command;
        }

        public SqlDataReader ExecuteCommand(SqlCommand command)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();

            connection.Open();

            return command.ExecuteReader(CommandBehavior.Default);
        }

        public void ExecuteNoneCommand(SqlCommand command)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();

            connection.Open();

            command.ExecuteNonQuery();
        }
        public void Dispose()
        {
            CerrarConexion();
            connection.Dispose();
            GC.SuppressFinalize(this);
        }

        void CerrarConexion()
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }

    }
}
