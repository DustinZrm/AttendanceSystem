using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace AttendanceSystem
{
    class DBUser
    {
        DBHelper db_helper = null;
        public DBUser()
        {
            db_helper = new DBHelper();
        }

        public DataTable getAllUser()
        {
            string strSQL = string.Format("select ID,mName,mDepartment,mPosition,mCardNo,mDate from tb_User order by ID desc");
            DataTable dt = db_helper.executeQuery(strSQL);
            return dt;
        }

        public DataTable getUserByCardNo(string strCardNo)
        {
            string strSQL = string.Format("select ID,mName,mDepartment,mPosition,mCardNo from tb_User where mCardNo = '{0:s}'", strCardNo);
            DataTable dt = db_helper.executeQuery(strSQL);
            return dt;
        }

        public bool addUser(string strName, string strDepartment, string strPosition, string strCardNo)
        {
            string strSQL = string.Format("select ID,mName,mDepartment,mPosition,mCardNo from tb_User where mCardNo = '{0:s}'", strCardNo);
            DataTable dt = db_helper.executeQuery(strSQL);
            if (dt.Rows.Count == 0)
            {
                strSQL = string.Format("insert into tb_User(mName,mDepartment,mPosition,mCardNo) values('{0:s}','{1:s}','{2:s}','{3:s}')", strName, strDepartment, strPosition, strCardNo);
                db_helper.excuteSql(strSQL);
                return true;
            }
            return false;
        }

        public void modifyUser(string strName, string strDepartment, string strPosition, string strCardNo)
        {
            string strSQL = string.Format("update tb_User set mName = '{0:s}',mDepartment = '{1:s}',mPosition = '{2:s}' where mCardNo = '{3:s}'", strName, strDepartment, strPosition, strCardNo);
            db_helper.excuteSql(strSQL);
        }

        public void deleteUserByCardNo(string strCardNo)
        {
            string strSQL = string.Format("delete from tb_User where mCardNo = '{0:s}'", strCardNo);
            db_helper.excuteSql(strSQL);
        }
    }
}
