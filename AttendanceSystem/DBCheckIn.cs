using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace AttendanceSystem
{
    class DBCheckIn
    {
        DBHelper db_helper = null;
        public DBCheckIn()
        {
            db_helper = new DBHelper();
        }

        public DataTable getAllCheckInInfo()
        {
            string strSQL = string.Format("select tb_User.mName,tb_User.mDepartment,tb_User.mPosition,tb_User.mCardNo,tb_CheckIn.mDate " +
                                          "from tb_User right join tb_CheckIn on tb_User.mCardNo = tb_CheckIn.mCardNo order by tb_CheckIn.mDate asc");
            DataTable dt = db_helper.executeQuery(strSQL);
            return dt;
        }

        public DataTable getCheckInInfoByName(string strName)
        {
            string strSQL = string.Format("select tb_User.mName,tb_User.mDepartment,tb_User.mPosition,tb_User.mCardNo,tb_CheckIn.mDate " +
                                          "from tb_User right join tb_CheckIn on tb_User.mCardNo = tb_CheckIn.mCardNo " +
                                          "where mName = '{0:s}' order by tb_CheckIn.mDate asc", strName);
            DataTable dt = db_helper.executeQuery(strSQL);
            return dt;
        }

        public void clearAllCheckIn()
        {
            string strSQL = string.Format("delete from tb_CheckIn");
            db_helper.excuteSql(strSQL);
        }

        public void clearCheckInByName(string strName)
        {
            string strSQL = string.Format("delete from tb_CheckIn where mCardNo in(select mCardNo from tb_User where mName = '{0:s}')", strName);
            db_helper.excuteSql(strSQL);
        }

        public void clearCheckInByCardNo(string strCardNo)
        {
            string strSQL = string.Format("delete from tb_CheckIn where mCardNo = '{0:s}'", strCardNo);
            db_helper.excuteSql(strSQL);
        }

        public void addCheckIn(string strCardNo)
        {
            string strSQL = string.Format("insert into tb_CheckIn(mCardNo) values('{0:s}')", strCardNo);
            db_helper.excuteSql(strSQL);
        }
    }
}
