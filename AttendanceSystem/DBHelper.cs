using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace AttendanceSystem
{
    class DBHelper
    {
        protected OleDbConnection conn = null;

        /// <summary>
        /// 数据库访问类构造函数
        /// </summary>
        public DBHelper()
        {
            string dbConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\DBSystem";
            conn = new OleDbConnection(dbConStr);
        }
        /// <summary>
        /// 数据库访问类构造函数
        /// </summary>
        /// <param name="connStr">数据库链接字符串</param>
        public DBHelper(string connStr)
        {
            if (connStr != "")
            {
                conn = new OleDbConnection(connStr);
            }
        }
        /// <summary>
        /// 打开数据库
        /// </summary>
        public bool openDB()
        {
            bool isScuess = false;
            try
            {
                if (conn == null) return false;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    isScuess = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isScuess;
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public bool closeDB()
        {
            bool isScuess = false;
            try
            {
                if (conn == null) return false;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    isScuess = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isScuess;
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        public bool excuteSql(string Sqlstr)
        {
            bool isScuess = false;
            try
            {
                openDB();
                OleDbCommand cmd = new OleDbCommand(Sqlstr);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    isScuess = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                closeDB();
            }
            return isScuess;
        }
        /// <summary>
        /// 执行带有参数的查询SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>dataTable-->返回的数据表</returns>
        public DataTable executeQuery(string Sqlstr)
        {
            OleDbDataReader dataReader = null;
            DataTable dataTable = new DataTable();
            try
            {
                openDB();
                OleDbCommand cmd = new OleDbCommand(Sqlstr);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataTable.Load(dataReader);

            }
            catch (Exception ex)
            {
                dataTable = null;
                throw ex;
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                closeDB();
            }
            return dataTable;
        }
    }
}
