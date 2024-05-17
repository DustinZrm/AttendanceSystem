using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO.Ports;
using System.Threading;

namespace AttendanceSystem
{
    public partial class MainForm : Form
    {
        //数据包头
        public const byte RecvHead = 0xAA;
        //数据缓冲长度
        public const int MaxLen = 1024;
        //单条数据长度
        public const int MaxDataLen = 100;
        //数据读写锁标志
        public bool bLock = false;
        //线程运行标志
        public bool bThread = true;
        //数据缓冲区
        public byte[] byteRecBuff = new byte[MaxLen];
        //数据读缓冲区
        public byte[] bytesData = new byte[MaxDataLen];
        //数据进出标志
        public int iDataIn = 0, iDataOut = 0;
        //卡号标志
        public string strAddrCardNo = "";
        //进入人员管理标志
        public bool bManage = false;
        DBCheckIn db_checkIn = null;
        DBUser db_user = null;

        public MainForm()
        {
            InitializeComponent();
            db_checkIn = new DBCheckIn();
            db_user = new DBUser();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            initComComboBox();
            mTimer.Start();
            mFindCardTimer.Start();
            recvThreadStart();
            getAllCheckInInfo();
            updateUserList();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            recvThreadStop();
            closeSerial();
            mFindCardTimer.Stop();
            mTimer.Stop();
        }

        private void btnSerialRefresh_Click(object sender, EventArgs e)
        {
            closeSerial();
            initComComboBox();
        }

        private void btnSerialConnect_Click(object sender, EventArgs e)
        {
            openSerial();
        }

        private void btnSerialDisconnect_Click(object sender, EventArgs e)
        {
            closeSerial();
        }

        private void mTimer_Tick(object sender, EventArgs e)
        {
            scanData();
        }

        private void mFindCardTimer_Tick(object sender, EventArgs e)
        {
            if (mSerialPort.IsOpen)
            {
                findCard();
            }
        }

        private void btnFindUser_Click(object sender, EventArgs e)
        {
            if (mFindUserComboBox.SelectedIndex == 0)
            {
                getAllCheckInInfo();
            }
            else
            {
                string strName = mFindUserComboBox.SelectedItem.ToString();
                getCheckInInfoByName(strName);
            }
        }

        private void btnClearUserCheckIn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要清除考勤信息？","提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (mClearUserComboBox.SelectedIndex == 0)
                {
                    db_checkIn.clearAllCheckIn();
                    getAllCheckInInfo();
                }
                else
                {
                    string strName = mClearUserComboBox.SelectedItem.ToString();
                    db_checkIn.clearCheckInByName(strName);
                    getAllCheckInInfo();
                }
            }
        }

        private void btnManageUser_Click(object sender, EventArgs e)
        {
            UserManage dlg = new UserManage();
            dlg.Owner = this;
            dlg.ShowDialog();
            getAllCheckInInfo();
            updateUserList();
            bManage = false;
        }

        //初始化串口选择列表
        private void initComComboBox()
        {
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            mSerialComboBox.Items.Clear();
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    mSerialComboBox.Items.Add(sValue);
                    mSerialComboBox.SelectedIndex = 0;
                }
            }
        }

        //信息栏信息显示
        private void showMsg(string strMsg)
        {
            mInfo.Text = strMsg;
        }

        //显示字符数组
        private void showBytesMsg(byte[] msg)
        {
            string strTemp = "";
            for (int i = 0; i < msg.Length; i++)
            {
                strTemp += string.Format("{0:X2} ", msg[i]);
            }
            showMsg(strTemp);
        }

        //打开串口
        private void openSerial()
        {
            try
            {
                SwitchCOMThread(mSerialComboBox.Text);

                btnSerialConnect.Enabled = false;
                btnSerialDisconnect.Enabled = true;
                showMsg("串口：连接成功！");
            }
            catch (Exception ex)
            {
                showMsg("串口：" + ex.Message);
            }
        }

        //关闭串口
        private void closeSerial()
        {
            try
            {
                if (mSerialPort.IsOpen)
                {
                    mSerialPort.Close();
                }
                //清空缓冲区
                iDataIn = 0;
                iDataOut = 0;
                btnSerialConnect.Enabled = true;
                btnSerialDisconnect.Enabled = false;
                showMsg("串口：断开连接！");
            }
            catch (Exception ex)
            {
                showMsg("串口：" + ex.Message);
            }
        }

        //串口数据发送
        private bool serialSend(byte[] msg)
        {
            if (mSerialPort.IsOpen)
            {
                try
                {
                    mSerialPort.Write(msg, 0, msg.Length);
                    return true;
                }
                catch (Exception ex)
                {
                    showMsg("串口：" + ex.Message);
                    return false;
                }
            }
            else
            {
                showMsg("串口：请先建立连接！");
            }
            return false;
        }

        //串口数据接收
        private void serialReceive()
        {
            if (mSerialPort.IsOpen)
            {
                try
                {
                    int i = 0;
                    int iDataLen = mSerialPort.BytesToRead;
                    if (iDataLen > 50) iDataLen = 50;
                    //读取缓冲区的数据到数组
                    mSerialPort.Read(bytesData, 0, iDataLen);
                    if (bLock == false)
                    {
                        bLock = true;
                        if (iDataIn + iDataLen <= MaxLen)
                        {
                            for (i = 0; i < iDataLen; i++)
                            {
                                byteRecBuff[iDataIn + i] = bytesData[i];
                            }
                            iDataIn += iDataLen;
                        }
                        else
                        {
                            for (i = iDataIn; i < MaxLen; i++)
                            {
                                byteRecBuff[i] = bytesData[i - iDataIn];
                            }
                            for (i = 0; i < iDataLen - MaxLen + iDataIn; i++)
                            {
                                byteRecBuff[i] = bytesData[i + MaxLen - iDataIn];
                            }
                            iDataIn = iDataLen - MaxLen + iDataIn;
                        }
                        bLock = false;
                    }
                }
                catch (Exception ex)
                {
                    showMsg("串口：" + ex.Message);
                    bLock = false;
                }
            }
        }

        //接收数据线程
        private void serialThreadReceive()
        {
            while (bThread)
            {
                serialReceive();

                Thread.Sleep(50);
            }
        }

        //启动接收线程
        private void recvThreadStart()
        {
            Thread newthread = new Thread(new ThreadStart(serialThreadReceive));
            newthread.Start();
        }

        //关闭接收线程
        private void recvThreadStop()
        {
            bThread = false;
        }

        //返回数据缓冲区内的有效数据长度
        private int validReceiveLen()
        {
            if (iDataOut < iDataIn)
            {
                return (iDataIn - iDataOut);
            }
            else if (iDataOut > iDataIn)
            {
                return (MaxLen - iDataOut + iDataIn);
            }
            else
            {
                return 0;
            }
        }

        //返回后面第iNum有效数据的位置
        private int dataOutAdd(int iNum)
        {
            int ret = 0;
            if (iDataOut + iNum < MaxLen)
            {
                ret = iDataOut + iNum;
            }
            else if (iDataOut + iNum > MaxLen)
            {
                ret = iDataOut + iNum - MaxLen;
            }
            return ret;
        }

        //校验数据
        private bool checkSummationVerify(byte[] bytes)
        {
            byte b = 0x00;
            for (int i = 0; i < bytes.Length - 1; i++)
            {
                b += bytes[i];
            }
            if (bytes[bytes.Length - 1] == b)
            {
                return true;
            }
            return false;
        }

        //设置校验值
        private void setSummationVerify(byte[] bytes)
        {
            byte b = 0x00;
            for (int i = 0; i < bytes.Length - 1; i++)
            {
                b += bytes[i];
            }
            bytes[bytes.Length - 1] = b;
        }

        //读取出数据缓冲区内的有效数据
        private void scanData()
        {
            if (bLock == false)
            {
                bLock = true;
                if (iDataIn != iDataOut)
                {
                    int iValidLen = validReceiveLen();
                    while (iValidLen > 7)
                    {
                        //判断包头
                        if (byteRecBuff[dataOutAdd(0)] == RecvHead)
                        {
                            int PacketLen = byteRecBuff[dataOutAdd(1)];
                            //判断数据包是否完整
                            if (PacketLen <= iValidLen)
                            {
                                //读出一个数据包
                                byte[] Packet = new byte[PacketLen];
                                for (int i = 0; i < PacketLen; i++)
                                {
                                    Packet[i] = byteRecBuff[dataOutAdd(i)];
                                }
                                handleData(Packet);
                                //showBytesMsg(Packet);
                                iDataOut = dataOutAdd(PacketLen);
                            }
                            bLock = false;
                            return;
                        }
                        else
                        {
                            iDataOut = dataOutAdd(1);
                            iValidLen--;
                        }
                    }
                }
                bLock = false;
            }
        }

        //处理有效数据包
        private void handleData(byte[] Packet)
        {
            //判断校验位
            if (checkSummationVerify(Packet))
            {
                if (Packet[2] == (byte)0x03 && Packet[4] == (byte)0x01)
                {
                    if (Packet[5] == (byte)0x02 && Packet[6] == (byte)0x0A)
                    {
                        if (Packet[7] == (byte)0x86 && Packet[8] == (byte)0x00)
                        {
                            string strCardNo = "";
                            for (int i = 0; i < 4; i++)
                            {
                                strCardNo += string.Format("{0:X2}", Packet[i + 11]);
                            }
                            DataTable dt = db_user.getUserByCardNo(strCardNo);
                            if (!bManage)
                            {
                                strAddrCardNo = "";
                                if (dt.Rows.Count != 0)
                                {
                                    db_checkIn.addCheckIn(strCardNo);
                                    getAllCheckInInfo();
                                    showMsg("提示：打开成功");
                                }
                                else
                                {
                                    showMsg("提示：该卡片并未绑定用户");
                                }
                            }
                            else
                            {
                                strAddrCardNo = strCardNo;
                            }
                        }
                    }
                }
            }
        }

        //切换类型线程
        public void SwitchCOMThread(string strCOM)
        {
            Thread newthread = new Thread(new ParameterizedThreadStart(SwitchCOM));
            newthread.Start((object)strCOM);
        }

        //切换类型
        public void SwitchCOM(object obj)
        {
            string strCOM = (string)obj;
            try
            {
                mSerialPort.BaudRate = 115200;
                mSerialPort.PortName = strCOM;
                mSerialPort.Open();
            }
            catch (Exception ex)
            {
                showMsg("串口：" + ex.Message);
            }
            Thread.Sleep(100);
            byte[] bytes = new byte[7];
            bytes[0] = (byte)0xAA;
            bytes[1] = (byte)bytes.Length;
            bytes[2] = (byte)0x01;
            bytes[3] = (byte)0x00;
            bytes[4] = (byte)0x01;
            bytes[5] = (byte)0x04;
            setSummationVerify(bytes);
            serialSend(bytes);
            Thread.Sleep(100);
            try
            {
                if (mSerialPort.IsOpen)
                {
                    mSerialPort.Close();
                }
                mSerialPort.BaudRate = 9600;
                mSerialPort.PortName = strCOM;
                mSerialPort.Open();
            }
            catch (Exception ex)
            {
                showMsg("串口：" + ex.Message);
            }
        }

        //寻卡命令
        public void findCard()
        {
            byte[] bytes = new byte[11];
            bytes[0] = (byte)0xAA;
            bytes[1] = (byte)bytes.Length;
            bytes[2] = (byte)0x03;
            bytes[3] = (byte)0x00;
            bytes[4] = (byte)0x01;
            bytes[5] = (byte)0x02;
            bytes[6] = (byte)0x03;
            bytes[7] = (byte)0x86;
            bytes[8] = (byte)0x85;
            bytes[9] = (byte)0x03;
            setSummationVerify(bytes);
            serialSend(bytes);
        }

        //将考勤信息加入到表格中显示
        public void addDataTableRow(DataGridView DataTable, string strName, string strDepartment, string strPosition, string strCardNo, string strDate)
        {
            DataTable.Rows.Add();
            int i, j;
            for (i = DataTable.RowCount - 1; i > 0; i--)
            {
                DataTable.Rows[i].Cells[0].Value = (i+1);
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

        //显示考勤信息
        public void getAllCheckInInfo()
        {
            dataTable.Rows.Clear();
            DataTable dt = db_checkIn.getAllCheckInInfo();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strName = dt.Rows[i].ItemArray[0].ToString();
                    string strDepartment = dt.Rows[i].ItemArray[1].ToString();
                    string strPosition = dt.Rows[i].ItemArray[2].ToString();
                    string strCardNo = dt.Rows[i].ItemArray[3].ToString();
                    string strDate = dt.Rows[i].ItemArray[4].ToString();
                    addDataTableRow(dataTable, strName, strDepartment, strPosition, strCardNo, strDate);
                }
            }
        }

        //显示指定人员考勤信息
        public void getCheckInInfoByName(string strname)
        {
            dataTable.Rows.Clear();
            DataTable dt = db_checkIn.getCheckInInfoByName(strname);
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strName = dt.Rows[i].ItemArray[0].ToString();
                    string strDepartment = dt.Rows[i].ItemArray[1].ToString();
                    string strPosition = dt.Rows[i].ItemArray[2].ToString();
                    string strCardNo = dt.Rows[i].ItemArray[3].ToString();
                    string strDate = dt.Rows[i].ItemArray[4].ToString();
                    addDataTableRow(dataTable, strName, strDepartment, strPosition, strCardNo, strDate);
                }
            }
        }

        //更新查询人员列表
        public void updateUserList()
        {
            mFindUserComboBox.Items.Clear();
            mClearUserComboBox.Items.Clear();
            DataTable dt = db_user.getAllUser();
            mFindUserComboBox.Items.Add("全部人员");
            mClearUserComboBox.Items.Add("全部人员");
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strName = dt.Rows[i].ItemArray[1].ToString();
                    mFindUserComboBox.Items.Add(strName);
                    mClearUserComboBox.Items.Add(strName);
                }
                mFindUserComboBox.SelectedIndex = 0;
                mClearUserComboBox.SelectedIndex = 0;
            }
        }
    }
}
