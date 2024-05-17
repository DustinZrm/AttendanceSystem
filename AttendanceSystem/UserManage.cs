using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AttendanceSystem
{
    public partial class UserManage : Form
    {
        private MainForm parent;
        DBCheckIn db_checkIn = null;
        DBUser db_user = null;
        public UserManage()
        {
            InitializeComponent();
            db_checkIn = new DBCheckIn();
            db_user = new DBUser();
        }

        private void UserManage_Load(object sender, EventArgs e)
        {
            parent = (MainForm)this.Owner;
            getAllUserInfo();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            User dlg = new User();
            dlg.Owner = parent;
            dlg.ShowDialog();
            getAllUserInfo();
        }

        private void btnModifyUser_Click(object sender, EventArgs e)
        {
            if (userTable.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("请选择需要修改的人员信息");
            }
            else
            {
                User dlg = new User();
                dlg.Owner = parent;
                dlg.userCardNo = userTable.SelectedCells[4].Value.ToString();
                dlg.ShowDialog();
                getAllUserInfo();
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (userTable.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("请选择需要清除的人员信息");
            }
            else
            {
                if (MessageBox.Show("确认要清除该人员信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    string strCardNo = userTable.SelectedCells[4].Value.ToString();
                    DeleteThread(strCardNo);
                }
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //将人员信息加入到表格中显示
        public void addDataTableRow(DataGridView DataTable, string strName, string strDepartment, string strPosition, string strCardNo, string strDate)
        {
            DataTable.Rows.Add();
            int i, j;
            for (i = DataTable.RowCount - 1; i > 0; i--)
            {
                DataTable.Rows[i].Cells[0].Value = (i + 1);
                for (j = 1; j < 6; j++)
                {
                    DataTable.Rows[i].Cells[j].Value = DataTable.Rows[i - 1].Cells[j].Value;
                }
            }
            DataTable.Rows[0].Cells[0].Value = "1";
            DataTable.Rows[0].Cells[1].Value = strName;
            DataTable.Rows[0].Cells[2].Value = strDepartment;
            DataTable.Rows[0].Cells[3].Value = strPosition;
            DataTable.Rows[0].Cells[4].Value = strCardNo;
            DataTable.Rows[0].Cells[5].Value = strDate;
        }

        //获取人员信息
        public void getAllUserInfo()
        {
            try
            {
                userTable.Rows.Clear();
                DataTable dt = db_user.getAllUser();
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strName = dt.Rows[i].ItemArray[1].ToString();
                        string strDepartment = dt.Rows[i].ItemArray[2].ToString();
                        string strPosition = dt.Rows[i].ItemArray[3].ToString();
                        string strCardNo = dt.Rows[i].ItemArray[4].ToString();
                        string strDate = dt.Rows[i].ItemArray[5].ToString();
                        addDataTableRow(userTable, strName, strDepartment, strPosition, strCardNo, strDate);
                    }
                }
            }
            catch (Exception ex)
            {}
        }

        //删除人员信息线程
        public void DeleteThread(string strCardNo)
        {
            Thread newthread = new Thread(new ParameterizedThreadStart(DeleteInfo));
            newthread.Start((object)strCardNo);
        }

        //删除人员信息
        public void DeleteInfo(object obj)
        {
            string strCardNo = (string)obj;
            db_user.deleteUserByCardNo(strCardNo);
            Thread.Sleep(500);
            db_checkIn.clearCheckInByCardNo(strCardNo);
            Thread.Sleep(500);
            getAllUserInfo();
        }
    }
}
