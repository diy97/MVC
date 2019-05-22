using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace AjaxProject
{
    /// <summary>
    /// 数据库帮助类
    /// </summary>
    public class DBHelper
    {
        private static string Connstr = "server=.;uid=sa;pwd=sa;database=SN_DB";
        /// <summary>
        /// 执行添加、删除、修改的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                //打开数据库连接
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);
                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        /// <summary>
        /// 执行SQL并返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                object obj = command.ExecuteScalar();
                return obj;
            }
        }

        /// <summary>
        /// 热行SQL返回SqlDataReader对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] paras)
        {
            SqlConnection conn = new SqlConnection(Connstr);
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddRange(paras);
            return command.ExecuteReader(CommandBehavior.CloseConnection);//命令行为
        }

        public static DataRow GetDataRow(string sql, params SqlParameter[] paras)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        public static DataTable GetDataTable(string sql, params SqlParameter[] paras)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            return dt;
        }

        public static DataSet GetDataSet(string sql, params SqlParameter[] paras)
        {
            DataSet ds = null;
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds);
            }
            return ds;
        }

    }
}
