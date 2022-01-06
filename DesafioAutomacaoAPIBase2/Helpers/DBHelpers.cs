using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DesafioAutomacaoAPIBase2.Helpers
{
    public class DBHelpers
    {
        #region PostGreSQL
        //private static NpgsqlConnection GetDBConnectionPostGreSQL()
        //{
        //    string connectionStringPostGreSQL = "Server =" +
        //        JsonBuilder.ReturnParameterAppSettings("DB_URL") + "; Port = " +
        //        JsonBuilder.ReturnParameterAppSettings("DB_PORT") + "; User Id = " +
        //        JsonBuilder.ReturnParameterAppSettings("DB_USER") + "; Password = " +
        //        JsonBuilder.ReturnParameterAppSettings("DB_PASSWORD") + "; Database = " +
        //        JsonBuilder.ReturnParameterAppSettings("DB_NAME") + ";";

        //    NpgsqlConnection connection = new NpgsqlConnection(connectionStringPostGreSQL);

        //    return connection;
        //}

        //public static void ExecuteQueryPostGreSQL(string query)
        //{
        //    using (var command = new NpgsqlCommand(query, GetDBConnectionPostGreSQL()))
        //    {
        //        command.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
        //        command.Connection.Open();
        //        command.ExecuteNonQuery();
        //        command.Connection.Close();
        //    }
        //}

        //public static List<string> RetornaDadosQueryPostGreSQL(string query)
        //{
        //    DataSet ds = new DataSet();
        //    List<string> lista = new List<string>();

        //    using (NpgsqlCommand command = new NpgsqlCommand(query, GetDBConnectionPostGreSQL()))
        //    {
        //        command.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
        //        command.Connection.Open();

        //        DataTable table = new DataTable();
        //        table.Load(command.ExecuteReader());
        //        ds.Tables.Add(table);

        //        command.Connection.Close();
        //    }

        //    if (ds.Tables[0].Columns.Count == 0)
        //    {
        //        return null;
        //    }

        //    try
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
        //            {
        //                lista.Add(ds.Tables[0].Rows[i][j].ToString());
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return lista;
        //}
        #endregion

        #region SQL
        //private static SqlConnection GetDBConnection()
        //{
        //    string connectionString = "Data Source=" + JsonBuilder.ReturnParameterAppSettings("DB_URL") + ";" +
        //                              "Initial Catalog=" + JsonBuilder.ReturnParameterAppSettings("DB_NAME") + "; " +
        //                              "User ID=" + JsonBuilder.ReturnParameterAppSettings("DB_USER") + "; " +
        //                              "Password=" + JsonBuilder.ReturnParameterAppSettings("DB_PASSWORD") + "; ";

        //    SqlConnection connection = new SqlConnection(connectionString);

        //    return connection;
        //}

        //public static void ExecuteQuery(string query)
        //{
        //    using (SqlCommand cmd = new SqlCommand(query, GetDBConnection()))
        //    {
        //        cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //    }
        //}

        //public static List<string> RetornaDadosQuery(string query)
        //{
        //    DataSet ds = new DataSet();
        //    List<string> lista = new List<string>();

        //    using (SqlCommand cmd = new SqlCommand(query, GetDBConnection()))
        //    {
        //        cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
        //        cmd.Connection.Open();

        //        DataTable table = new DataTable();
        //        table.Load(cmd.ExecuteReader());
        //        ds.Tables.Add(table);

        //        cmd.Connection.Close();
        //    }

        //    if (ds.Tables[0].Columns.Count == 0)
        //    {
        //        return null;
        //    }

        //    try
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
        //            {
        //                lista.Add(ds.Tables[0].Rows[i][j].ToString());
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return lista;
        //}
        #endregion

        #region Oracle
        //private static OracleConnection GetDBConnectionOracle()
        //{
        //    string connString = "DATA SOURCE=" + JsonBuilder.ReturnParameterAppSettings("DB_URL") + ";" +
        //    "PERSIST SECURITY INFO=True;USER ID=" + JsonBuilder.ReturnParameterAppSettings("DB_USER") +
        //    "; password=" + JsonBuilder.ReturnParameterAppSettings("DB_PASSWORD") + "; Pooling = False; ";

        //    OracleConnection conn = new OracleConnection(connString);

        //    return conn;
        //}

        //public static void ExecuteQueryOracle(string query)
        //{
        //    using (OracleCommand cmd = new OracleCommand(query, GetDBConnectionOracle()))
        //    {
        //        cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //    }
        //}

        //public static List<string> RetornaDadosQueryOracle(string query)
        //{
        //    DataSet ds = new DataSet();
        //    List<string> lista = new List<string>();

        //    using (OracleCommand cmd = new OracleCommand(query, GetDBConnectionOracle()))
        //    {
        //        cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
        //        cmd.Connection.Open();

        //        DataTable table = new DataTable();
        //        table.Load(cmd.ExecuteReader());
        //        ds.Tables.Add(table);

        //        cmd.Connection.Close();
        //    }

        //    if (ds.Tables[0].Columns.Count == 0)
        //    {
        //        return null;
        //    }

        //    try
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
        //            {
        //                lista.Add(ds.Tables[0].Rows[i][j].ToString());
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return lista;
        //}
        #endregion

        #region MySQL
        private static MySqlConnection GetdbConnectionMySql()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = JsonBuilder.ReturnParameterAppSettings("DB_URL"),
                Database = JsonBuilder.ReturnParameterAppSettings("DB_NAME"),  
                Port = uint.Parse(JsonBuilder.ReturnParameterAppSettings("PORT")),
                Password = JsonBuilder.ReturnParameterAppSettings("DB_PASSWORD"),
                UserID = JsonBuilder.ReturnParameterAppSettings("DB_USER"),
                SslMode = MySqlSslMode.None,
            };

            MySqlConnection conn = new MySqlConnection(builder.ConnectionString);

            return conn;
        }

        public static void ExecuteQueryMySQL(string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, GetdbConnectionMySql()))
            {
                cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public static List<string> RetornaDadosQueryMySQL(string query)
        {
            DataSet ds = new DataSet();
            List<string> lista = new List<string>();

            using (MySqlCommand cmd = new MySqlCommand(query, GetdbConnectionMySql()))
            {
                cmd.CommandTimeout = Int32.Parse(JsonBuilder.ReturnParameterAppSettings("DB_CONNECTION_TIMEOUT"));
                cmd.Connection.Open();

                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);

                cmd.Connection.Close();
            }

            if (ds.Tables[0].Columns.Count == 0)
            {
                return null;
            }

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        lista.Add(ds.Tables[0].Rows[i][j].ToString());
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return lista;
        }

        public static DataTable RetornaDadosDataTableQuery(string query)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            List<string> lista = new List<string>();

            using (MySqlCommand cmd = new MySqlCommand(query, GetdbConnectionMySql()))
            {
                cmd.CommandTimeout = 60;
                cmd.Connection.Open();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);

                cmd.Connection.Close();
            }

            if (ds.Tables[0].Columns.Count == 0)
            {
                return null;
            }

            return table;
        }
        #endregion
    }
}
