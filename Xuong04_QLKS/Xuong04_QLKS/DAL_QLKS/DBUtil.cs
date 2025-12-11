using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public static class DBUtil
    {
        public static string connString = @"Data Source=PHONG\SQLEXPRESS;Initial Catalog=Xuong_QuanLyKhachSan1;Integrated Security=True;Trust Server Certificate=True";

        public static SqlCommand CreateCommand(string sql, Dictionary<string, object> args, CommandType cmdType, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = cmdType;

            if (args != null)
            {
                foreach (var param in args)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            return cmd;
        }

        public static int Update(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                using (SqlCommand cmd = CreateCommand(sql, args, cmdType, conn))
                {
                    cmd.Transaction = transaction;

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        transaction.Commit();
                        return rowsAffected;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static object ScalarQuery(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = CreateCommand(sql, args, cmdType, conn))
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        public static DataTable Query(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = CreateCommand(sql, args, cmdType, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
        }

        public static T QuerySingle<T>(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text) where T : new()
        {
            DataTable dt = Query(sql, args, cmdType);
            if (dt.Rows.Count > 0)
            {
                return MapDataRowToT<T>(dt.Rows[0]);
            }
            return default;
        }

        public static List<T> QueryList<T>(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text) where T : new()
        {
            DataTable dt = Query(sql, args, cmdType);
            List<T> resultList = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                resultList.Add(MapDataRowToT<T>(row));
            }
            return resultList;
        }

        private static T MapDataRowToT<T>(DataRow row) where T : new()
        {
            T item = new T();
            Type type = typeof(T);

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo property = type.GetProperty(column.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {
                    object value = row[column.ColumnName];
                    if (value == DBNull.Value)
                    {
                        property.SetValue(item, null);
                    }
                    else
                    {
                        try
                        {
                            Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(value, targetType));
                        }
                        catch
                        {
                            // Log lỗi nếu cần
                        }
                    }
                }
            }

            return item;
        }

        public static T Value<T>(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text) where T : new()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = CreateCommand(sql, args, cmdType, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        T result = new T();
                        Type type = typeof(T);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);
                            PropertyInfo property = type.GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                            if (property != null && property.CanWrite)
                            {
                                object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                if (value != null)
                                {
                                    Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                    property.SetValue(result, Convert.ChangeType(value, targetType));
                                }
                                else if (property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) == null)
                                {
                                    property.SetValue(result, Activator.CreateInstance(property.PropertyType));
                                }
                                else
                                {
                                    property.SetValue(result, null);
                                }
                            }
                        }
                        return result;
                    }
                    return default;
                }
            }
        }


        // Alias cho phương thức Select
        public static List<T> Select<T>(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text) where T : new()
        {
            return QueryList<T>(sql, args, cmdType);
        }

        // Alias cho phương thức SelectOne
        public static T SelectOne<T>(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text) where T : new()
        {
            return QuerySingle<T>(sql, args, cmdType);
        }

        public static List<T> ExecuteQuery<T>(string sql, Dictionary<string, object> args = null, CommandType cmdType = CommandType.Text) where T : new()
        {
            return QueryList<T>(sql, args, cmdType);
        }

        public static T ExecuteQuerySingle<T>(string sql, Dictionary<string, object> args = null, CommandType cmdType = CommandType.Text) where T : new()
        {
            return QuerySingle<T>(sql, args, cmdType);
        }



    }
}
