namespace AttendanceSystem
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.mSerialGroupBox = new System.Windows.Forms.GroupBox();
            this.mSerialComboBox = new System.Windows.Forms.ComboBox();
            this.btnSerialConnect = new System.Windows.Forms.Button();
            this.btnSerialDisconnect = new System.Windows.Forms.Button();
            this.btnSerialRefresh = new System.Windows.Forms.Button();
            this.mFindGroupBox = new System.Windows.Forms.GroupBox();
            this.mFindUserComboBox = new System.Windows.Forms.ComboBox();
            this.btnFindUser = new System.Windows.Forms.Button();
            this.dataTable = new System.Windows.Forms.DataGridView();
            this.mNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mCardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mManageGroupBox = new System.Windows.Forms.GroupBox();
            this.btnManageUser = new System.Windows.Forms.Button();
            this.mClearUserComboBox = new System.Windows.Forms.ComboBox();
            this.btnClearUserCheckIn = new System.Windows.Forms.Button();
            this.mSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.mTimer = new System.Windows.Forms.Timer(this.components);
            this.mFindCardTimer = new System.Windows.Forms.Timer(this.components);
            this.mStatusStrip.SuspendLayout();
            this.mSerialGroupBox.SuspendLayout();
            this.mFindGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).BeginInit();
            this.mManageGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mStatusStrip
            // 
            this.mStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mInfo});
            this.mStatusStrip.Location = new System.Drawing.Point(0, 579);
            this.mStatusStrip.Name = "mStatusStrip";
            this.mStatusStrip.Size = new System.Drawing.Size(784, 22);
            this.mStatusStrip.SizingGrip = false;
            this.mStatusStrip.TabIndex = 0;
            this.mStatusStrip.Text = "statusStrip";
            // 
            // mInfo
            // 
            this.mInfo.Name = "mInfo";
            this.mInfo.Size = new System.Drawing.Size(56, 17);
            this.mInfo.Text = "信息提示";
            // 
            // mSerialGroupBox
            // 
            this.mSerialGroupBox.Controls.Add(this.mSerialComboBox);
            this.mSerialGroupBox.Controls.Add(this.btnSerialConnect);
            this.mSerialGroupBox.Controls.Add(this.btnSerialDisconnect);
            this.mSerialGroupBox.Controls.Add(this.btnSerialRefresh);
            this.mSerialGroupBox.Location = new System.Drawing.Point(12, 12);
            this.mSerialGroupBox.Name = "mSerialGroupBox";
            this.mSerialGroupBox.Size = new System.Drawing.Size(590, 60);
            this.mSerialGroupBox.TabIndex = 2;
            this.mSerialGroupBox.TabStop = false;
            this.mSerialGroupBox.Text = "连接设置";
            // 
            // mSerialComboBox
            // 
            this.mSerialComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mSerialComboBox.FormattingEnabled = true;
            this.mSerialComboBox.Location = new System.Drawing.Point(152, 24);
            this.mSerialComboBox.Name = "mSerialComboBox";
            this.mSerialComboBox.Size = new System.Drawing.Size(100, 20);
            this.mSerialComboBox.TabIndex = 0;
            // 
            // btnSerialConnect
            // 
            this.btnSerialConnect.Location = new System.Drawing.Point(338, 22);
            this.btnSerialConnect.Name = "btnSerialConnect";
            this.btnSerialConnect.Size = new System.Drawing.Size(100, 23);
            this.btnSerialConnect.TabIndex = 2;
            this.btnSerialConnect.Text = "连接";
            this.btnSerialConnect.UseVisualStyleBackColor = true;
            this.btnSerialConnect.Click += new System.EventHandler(this.btnSerialConnect_Click);
            // 
            // btnSerialDisconnect
            // 
            this.btnSerialDisconnect.Enabled = false;
            this.btnSerialDisconnect.Location = new System.Drawing.Point(444, 22);
            this.btnSerialDisconnect.Name = "btnSerialDisconnect";
            this.btnSerialDisconnect.Size = new System.Drawing.Size(100, 23);
            this.btnSerialDisconnect.TabIndex = 3;
            this.btnSerialDisconnect.Text = "断开";
            this.btnSerialDisconnect.UseVisualStyleBackColor = true;
            this.btnSerialDisconnect.Click += new System.EventHandler(this.btnSerialDisconnect_Click);
            // 
            // btnSerialRefresh
            // 
            this.btnSerialRefresh.Location = new System.Drawing.Point(46, 22);
            this.btnSerialRefresh.Name = "btnSerialRefresh";
            this.btnSerialRefresh.Size = new System.Drawing.Size(100, 23);
            this.btnSerialRefresh.TabIndex = 1;
            this.btnSerialRefresh.Text = "刷新串口";
            this.btnSerialRefresh.UseVisualStyleBackColor = true;
            this.btnSerialRefresh.Click += new System.EventHandler(this.btnSerialRefresh_Click);
            // 
            // mFindGroupBox
            // 
            this.mFindGroupBox.Controls.Add(this.mFindUserComboBox);
            this.mFindGroupBox.Controls.Add(this.btnFindUser);
            this.mFindGroupBox.Location = new System.Drawing.Point(608, 12);
            this.mFindGroupBox.Name = "mFindGroupBox";
            this.mFindGroupBox.Size = new System.Drawing.Size(164, 200);
            this.mFindGroupBox.TabIndex = 4;
            this.mFindGroupBox.TabStop = false;
            this.mFindGroupBox.Text = "考勤查询";
            // 
            // mFindUserComboBox
            // 
            this.mFindUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mFindUserComboBox.FormattingEnabled = true;
            this.mFindUserComboBox.Location = new System.Drawing.Point(32, 66);
            this.mFindUserComboBox.Name = "mFindUserComboBox";
            this.mFindUserComboBox.Size = new System.Drawing.Size(100, 20);
            this.mFindUserComboBox.TabIndex = 0;
            // 
            // btnFindUser
            // 
            this.btnFindUser.Location = new System.Drawing.Point(32, 119);
            this.btnFindUser.Name = "btnFindUser";
            this.btnFindUser.Size = new System.Drawing.Size(100, 23);
            this.btnFindUser.TabIndex = 2;
            this.btnFindUser.Text = "查询";
            this.btnFindUser.UseVisualStyleBackColor = true;
            this.btnFindUser.Click += new System.EventHandler(this.btnFindUser_Click);
            // 
            // dataTable
            // 
            this.dataTable.AllowUserToAddRows = false;
            this.dataTable.AllowUserToDeleteRows = false;
            this.dataTable.AllowUserToResizeColumns = false;
            this.dataTable.AllowUserToResizeRows = false;
            this.dataTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mNO,
            this.mName,
            this.mDepartment,
            this.mPosition,
            this.mCardNo,
            this.mDate});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataTable.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataTable.Location = new System.Drawing.Point(12, 78);
            this.dataTable.Name = "dataTable";
            this.dataTable.ReadOnly = true;
            this.dataTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataTable.RowHeadersVisible = false;
            this.dataTable.RowTemplate.Height = 23;
            this.dataTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataTable.Size = new System.Drawing.Size(590, 498);
            this.dataTable.TabIndex = 3;
            // 
            // mNO
            // 
            this.mNO.HeaderText = "编号";
            this.mNO.Name = "mNO";
            this.mNO.ReadOnly = true;
            this.mNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mNO.Width = 60;
            // 
            // mName
            // 
            this.mName.HeaderText = "姓名";
            this.mName.Name = "mName";
            this.mName.ReadOnly = true;
            this.mName.Width = 70;
            // 
            // mDepartment
            // 
            this.mDepartment.HeaderText = "部门";
            this.mDepartment.Name = "mDepartment";
            this.mDepartment.ReadOnly = true;
            // 
            // mPosition
            // 
            this.mPosition.HeaderText = "职位";
            this.mPosition.Name = "mPosition";
            this.mPosition.ReadOnly = true;
            // 
            // mCardNo
            // 
            this.mCardNo.HeaderText = "卡号";
            this.mCardNo.Name = "mCardNo";
            this.mCardNo.ReadOnly = true;
            this.mCardNo.Width = 120;
            // 
            // mDate
            // 
            this.mDate.HeaderText = "时间";
            this.mDate.Name = "mDate";
            this.mDate.ReadOnly = true;
            this.mDate.Width = 120;
            // 
            // mManageGroupBox
            // 
            this.mManageGroupBox.Controls.Add(this.btnManageUser);
            this.mManageGroupBox.Controls.Add(this.mClearUserComboBox);
            this.mManageGroupBox.Controls.Add(this.btnClearUserCheckIn);
            this.mManageGroupBox.Location = new System.Drawing.Point(608, 218);
            this.mManageGroupBox.Name = "mManageGroupBox";
            this.mManageGroupBox.Size = new System.Drawing.Size(164, 358);
            this.mManageGroupBox.TabIndex = 5;
            this.mManageGroupBox.TabStop = false;
            this.mManageGroupBox.Text = "人员管理";
            // 
            // btnManageUser
            // 
            this.btnManageUser.Location = new System.Drawing.Point(32, 252);
            this.btnManageUser.Name = "btnManageUser";
            this.btnManageUser.Size = new System.Drawing.Size(100, 23);
            this.btnManageUser.TabIndex = 4;
            this.btnManageUser.Text = "人员管理";
            this.btnManageUser.UseVisualStyleBackColor = true;
            this.btnManageUser.Click += new System.EventHandler(this.btnManageUser_Click);
            // 
            // mClearUserComboBox
            // 
            this.mClearUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mClearUserComboBox.FormattingEnabled = true;
            this.mClearUserComboBox.Location = new System.Drawing.Point(32, 91);
            this.mClearUserComboBox.Name = "mClearUserComboBox";
            this.mClearUserComboBox.Size = new System.Drawing.Size(100, 20);
            this.mClearUserComboBox.TabIndex = 0;
            // 
            // btnClearUserCheckIn
            // 
            this.btnClearUserCheckIn.Location = new System.Drawing.Point(32, 137);
            this.btnClearUserCheckIn.Name = "btnClearUserCheckIn";
            this.btnClearUserCheckIn.Size = new System.Drawing.Size(100, 23);
            this.btnClearUserCheckIn.TabIndex = 2;
            this.btnClearUserCheckIn.Text = "清除人员考勤";
            this.btnClearUserCheckIn.UseVisualStyleBackColor = true;
            this.btnClearUserCheckIn.Click += new System.EventHandler(this.btnClearUserCheckIn_Click);
            // 
            // mSerialPort
            // 
            this.mSerialPort.ReadTimeout = 50;
            // 
            // mTimer
            // 
            this.mTimer.Tick += new System.EventHandler(this.mTimer_Tick);
            // 
            // mFindCardTimer
            // 
            this.mFindCardTimer.Interval = 1000;
            this.mFindCardTimer.Tick += new System.EventHandler(this.mFindCardTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 601);
            this.Controls.Add(this.mManageGroupBox);
            this.Controls.Add(this.mFindGroupBox);
            this.Controls.Add(this.dataTable);
            this.Controls.Add(this.mSerialGroupBox);
            this.Controls.Add(this.mStatusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考勤管理系统(125K只读卡)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mStatusStrip.ResumeLayout(false);
            this.mStatusStrip.PerformLayout();
            this.mSerialGroupBox.ResumeLayout(false);
            this.mFindGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).EndInit();
            this.mManageGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel mInfo;
        private System.Windows.Forms.GroupBox mSerialGroupBox;
        private System.Windows.Forms.ComboBox mSerialComboBox;
        private System.Windows.Forms.Button btnSerialConnect;
        private System.Windows.Forms.Button btnSerialDisconnect;
        private System.Windows.Forms.Button btnSerialRefresh;
        private System.Windows.Forms.GroupBox mFindGroupBox;
        private System.Windows.Forms.ComboBox mFindUserComboBox;
        private System.Windows.Forms.Button btnFindUser;
        private System.Windows.Forms.DataGridView dataTable;
        private System.Windows.Forms.GroupBox mManageGroupBox;
        private System.Windows.Forms.ComboBox mClearUserComboBox;
        private System.Windows.Forms.Button btnClearUserCheckIn;
        private System.IO.Ports.SerialPort mSerialPort;
        private System.Windows.Forms.Timer mTimer;
        private System.Windows.Forms.Timer mFindCardTimer;
        private System.Windows.Forms.DataGridViewTextBoxColumn mNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn mName;
        private System.Windows.Forms.DataGridViewTextBoxColumn mDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn mPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn mCardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn mDate;
        private System.Windows.Forms.Button btnManageUser;
    }
}

