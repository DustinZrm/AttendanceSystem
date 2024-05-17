using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AttendanceSystem
{
    public partial class User : Form
    {
        private MainForm parent;
        DBCheckIn db_checkIn = null;
        DBUser db_user = null;
        public string userCardNo = "";
        public User()
        {
            InitializeComponent();
            db_checkIn = new DBCheckIn();
            db_user = new DBUser();
        }

        private void User_Load(object sender, EventArgs e)
        {
            parent = (MainForm)this.Owner;
            if (!userCardNo.Equals(""))
            {
                textCardNo.Text = userCardNo;
                getUserInfo(userCardNo);
            }
            else
            {
                parent.bManage = true;
                addCardNoTimer.Start();
            }
        }

        private void User_FormClosed(object sender, FormClosedEventArgs e)
        {
            addCardNoTimer.Stop();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (textName.Text.Equals(""))
            {
                MessageBox.Show("姓名不能为空！");
            }
            else if (textDepartment.Text.Equals(""))
            {
                MessageBox.Show("部门不能为空！");
            }
            else if (textPosition.Text.Equals(""))
            {
                MessageBox.Show("职位不能为空！");
            }
            else
            {
                if (userCardNo.Equals(""))
                {
                    if (db_user.addUser(textName.Text, textDepartment.Text, textPosition.Text, textCardNo.Text))
                    {
                        MessageBox.Show("添加成功");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("该卡已被占用");
                    }
                }
                else
                {
                    db_user.modifyUser(textName.Text, textDepartment.Text, textPosition.Text, textCardNo.Text);
                    MessageBox.Show("修改成功");
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addCardNoTimer_Tick(object sender, EventArgs e)
        {
            if (!parent.strAddrCardNo.Equals(""))
            {
                textCardNo.Text = parent.strAddrCardNo;
            }
        }

        //获取用户信息
        public void getUserInfo(string strCardNo)
        {
            DataTable dt = db_user.getUserByCardNo(strCardNo);
            if (dt.Rows.Count != 0)
            {
                textName.Text = dt.Rows[0].ItemArray[1].ToString();
                textDepartment.Text = dt.Rows[0].ItemArray[2].ToString();
                textPosition.Text = dt.Rows[0].ItemArray[3].ToString();
            }
        }
    }
}
