using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace MatterLib
{
    public static class Utils
    {
        public static string connectionStr = "Server=s2008\\indigo;User Id=sa;Password=;Database=KostiaHH;";
        public static DataSet GetLoadedDataSet(string SQL)
        {
            try
            {

                DbConnection conn;
                DbDataAdapter adapter;

                conn = new SqlConnection(connectionStr);
                adapter = new SqlDataAdapter(SQL, conn as SqlConnection);

                conn.Open();
                adapter.SelectCommand.CommandTimeout = 0;
                DataSet retValue = new DataSet();
                adapter.Fill(retValue);
                conn.Close();
                return retValue;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message + "\n\n" + SQL);
            }
        }

        public static void ExecuteSql(string sql)
        {

            DbConnection conn;

            conn = new SqlConnection(connectionStr);

            using (conn)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }

        }

        public static void ExecuteSql(string dbName, IEnumerable<string> sqlBatch)
        {

            DbConnection conn;

                conn = new SqlConnection(connectionStr);
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = String.Join("\n", sqlBatch);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
        }

        public static string StringAsSql(string str)
        {
            return "'" + str.Replace("'", "''") + "'";
        }

        public static string BoolAsSql(Boolean value)
        {
                return value ? "1" : "0";

        }

        public static string DateAsSql(DateTime value)
        {
            
                return "CONVERT(DATE,'" + value.ToString("yyyyMMdd") + "')";

        }

        public static string DateTimeAsSql(DateTime value)
        {
                return "CONVERT(DATETIME2,'" + value.ToString("yyyyMMdd HH:mm:ss.FFF") + "')";

        }

        public static string GuidAsSql(Guid value)
        {
                return "CONVERT(UNIQUEIDENTIFIER,'" + value.ToString() + "')";

        }

        public static string NullAsSql(string dialect)
        {
            return "NULL";
        }


        public static string GetRandomString(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }

}
