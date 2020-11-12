namespace ScrewScada
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pic_Config = new System.Windows.Forms.PictureBox();
            this.lbl_date = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbl_LoginName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.spl_Main = new System.Windows.Forms.SplitContainer();
            this.pan_Main = new System.Windows.Forms.Panel();
            this.usl_ConnectLemp = new HZH_Controls.Controls.UCSignalLamp();
            this.ucBtnImg6 = new HZH_Controls.Controls.UCBtnImg();
            this.btn_Report = new HZH_Controls.Controls.UCBtnImg();
            this.btn_History = new HZH_Controls.Controls.UCBtnImg();
            this.btn_Alarm = new HZH_Controls.Controls.UCBtnImg();
            this.btn_ParaSet = new HZH_Controls.Controls.UCBtnImg();
            this.btn_View = new HZH_Controls.Controls.UCBtnImg();
            this.lbl_PlcState = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Config)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spl_Main)).BeginInit();
            this.spl_Main.Panel1.SuspendLayout();
            this.spl_Main.Panel2.SuspendLayout();
            this.spl_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.splitContainer1.Panel1.Controls.Add(this.lbl_Title);
            this.splitContainer1.Panel1.Controls.Add(this.pic_Config);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_date);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox4);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_LoginName);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_Time);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.splitContainer1.Panel1.DoubleClick += new System.EventHandler(this.SplitContainer1_Panel1_DoubleClick);
            this.splitContainer1.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseDown);
            this.splitContainer1.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseMove);
            this.splitContainer1.Panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseUp);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.spl_Main);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(2000, 1102);
            this.splitContainer1.SplitterDistance = 131;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SplitContainer1_KeyDown);
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lbl_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(168)))), ((int)(((byte)(0)))));
            this.lbl_Title.Location = new System.Drawing.Point(924, 44);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(156, 45);
            this.lbl_Title.TabIndex = 37;
            this.lbl_Title.Text = "控制流程";
            // 
            // pic_Config
            // 
            this.pic_Config.Image = ((System.Drawing.Image)(resources.GetObject("pic_Config.Image")));
            this.pic_Config.Location = new System.Drawing.Point(1888, 0);
            this.pic_Config.Margin = new System.Windows.Forms.Padding(0);
            this.pic_Config.Name = "pic_Config";
            this.pic_Config.Size = new System.Drawing.Size(38, 38);
            this.pic_Config.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Config.TabIndex = 36;
            this.pic_Config.TabStop = false;
            this.pic_Config.Click += new System.EventHandler(this.Pic_Config_Click);
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.BackColor = System.Drawing.Color.Transparent;
            this.lbl_date.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_date.ForeColor = System.Drawing.Color.Lime;
            this.lbl_date.Location = new System.Drawing.Point(1782, 48);
            this.lbl_date.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(102, 27);
            this.lbl_date.TabIndex = 35;
            this.lbl_date.Text = "2017/8/7";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(1925, 0);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(38, 38);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 36;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.PictureBox4_Click);
            this.pictureBox4.MouseEnter += new System.EventHandler(this.PictureBox4_MouseEnter);
            this.pictureBox4.MouseLeave += new System.EventHandler(this.PictureBox4_MouseLeave);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1962, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 36;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox2_MouseClick);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.PictureBox2_MouseEnter);
            this.pictureBox2.MouseLeave += new System.EventHandler(this.PictureBox2_MouseLeave);
            // 
            // lbl_LoginName
            // 
            this.lbl_LoginName.AutoSize = true;
            this.lbl_LoginName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_LoginName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_LoginName.ForeColor = System.Drawing.Color.Lime;
            this.lbl_LoginName.Location = new System.Drawing.Point(1782, 86);
            this.lbl_LoginName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_LoginName.Name = "lbl_LoginName";
            this.lbl_LoginName.Size = new System.Drawing.Size(152, 27);
            this.lbl_LoginName.TabIndex = 34;
            this.lbl_LoginName.Text = "Administrator";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Lime;
            this.label7.Location = new System.Drawing.Point(1674, 86);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 27);
            this.label7.TabIndex = 33;
            this.label7.Text = "登录用户：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Lime;
            this.label6.Location = new System.Drawing.Point(1674, 48);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 27);
            this.label6.TabIndex = 32;
            this.label6.Text = "系统时间：";
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Time.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Time.ForeColor = System.Drawing.Color.Lime;
            this.lbl_Time.Location = new System.Drawing.Point(1898, 48);
            this.lbl_Time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(96, 27);
            this.lbl_Time.TabIndex = 31;
            this.lbl_Time.Text = "22:10:23";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(210, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 39);
            this.label3.TabIndex = 22;
            this.label3.Text = "XXX操作控制平台";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseDown);
            this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseMove);
            this.label3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(168)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(211, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(332, 32);
            this.label4.TabIndex = 23;
            this.label4.Text = "Operation Control System ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(60, 36);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(116, 89);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseUp);
            // 
            // spl_Main
            // 
            this.spl_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.spl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spl_Main.Location = new System.Drawing.Point(0, 0);
            this.spl_Main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spl_Main.Name = "spl_Main";
            this.spl_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spl_Main.Panel1
            // 
            this.spl_Main.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.spl_Main.Panel1.Controls.Add(this.pan_Main);
            // 
            // spl_Main.Panel2
            // 
            this.spl_Main.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.spl_Main.Panel2.Controls.Add(this.usl_ConnectLemp);
            this.spl_Main.Panel2.Controls.Add(this.ucBtnImg6);
            this.spl_Main.Panel2.Controls.Add(this.btn_Report);
            this.spl_Main.Panel2.Controls.Add(this.btn_History);
            this.spl_Main.Panel2.Controls.Add(this.btn_Alarm);
            this.spl_Main.Panel2.Controls.Add(this.btn_ParaSet);
            this.spl_Main.Panel2.Controls.Add(this.btn_View);
            this.spl_Main.Panel2.Controls.Add(this.lbl_PlcState);
            this.spl_Main.Panel2MinSize = 80;
            this.spl_Main.Size = new System.Drawing.Size(2000, 966);
            this.spl_Main.SplitterDistance = 851;
            this.spl_Main.SplitterIncrement = 5;
            this.spl_Main.SplitterWidth = 1;
            this.spl_Main.TabIndex = 0;
            // 
            // pan_Main
            // 
            this.pan_Main.BackColor = System.Drawing.Color.White;
            this.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_Main.Location = new System.Drawing.Point(0, 0);
            this.pan_Main.Margin = new System.Windows.Forms.Padding(0);
            this.pan_Main.Name = "pan_Main";
            this.pan_Main.Size = new System.Drawing.Size(2000, 851);
            this.pan_Main.TabIndex = 0;
            // 
            // usl_ConnectLemp
            // 
            this.usl_ConnectLemp.IsHighlight = false;
            this.usl_ConnectLemp.IsShowBorder = false;
            this.usl_ConnectLemp.LampColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red};
            this.usl_ConnectLemp.Location = new System.Drawing.Point(1962, 46);
            this.usl_ConnectLemp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.usl_ConnectLemp.Name = "usl_ConnectLemp";
            this.usl_ConnectLemp.Size = new System.Drawing.Size(26, 26);
            this.usl_ConnectLemp.TabIndex = 1;
            this.usl_ConnectLemp.TwinkleSpeed = 0;
            // 
            // ucBtnImg6
            // 
            this.ucBtnImg6.BackColor = System.Drawing.Color.White;
            this.ucBtnImg6.BtnBackColor = System.Drawing.Color.White;
            this.ucBtnImg6.BtnFont = new System.Drawing.Font("微软雅黑", 12F);
            this.ucBtnImg6.BtnForeColor = System.Drawing.Color.White;
            this.ucBtnImg6.BtnText = "退出系统";
            this.ucBtnImg6.ConerRadius = 10;
            this.ucBtnImg6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg6.EnabledMouseEffect = false;
            this.ucBtnImg6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(115)))), ((int)(((byte)(163)))));
            this.ucBtnImg6.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg6.Image = null;
            this.ucBtnImg6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ucBtnImg6.ImageFontIcons = null;
            this.ucBtnImg6.IsRadius = true;
            this.ucBtnImg6.IsShowRect = true;
            this.ucBtnImg6.IsShowTips = false;
            this.ucBtnImg6.Location = new System.Drawing.Point(1466, 22);
            this.ucBtnImg6.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg6.Name = "ucBtnImg6";
            this.ucBtnImg6.RectColor = System.Drawing.Color.White;
            this.ucBtnImg6.RectWidth = 1;
            this.ucBtnImg6.Size = new System.Drawing.Size(192, 64);
            this.ucBtnImg6.TabIndex = 0;
            this.ucBtnImg6.TabStop = false;
            this.ucBtnImg6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg6.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg6.TipsText = "";
            this.ucBtnImg6.BtnClick += new System.EventHandler(this.UcBtnImg6_BtnClick);
            // 
            // btn_Report
            // 
            this.btn_Report.BackColor = System.Drawing.Color.White;
            this.btn_Report.BtnBackColor = System.Drawing.Color.White;
            this.btn_Report.BtnFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Report.BtnForeColor = System.Drawing.Color.White;
            this.btn_Report.BtnText = "数据导入";
            this.btn_Report.ConerRadius = 10;
            this.btn_Report.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Report.EnabledMouseEffect = false;
            this.btn_Report.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(115)))), ((int)(((byte)(163)))));
            this.btn_Report.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Report.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Report.Image = null;
            this.btn_Report.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Report.ImageFontIcons = null;
            this.btn_Report.IsRadius = true;
            this.btn_Report.IsShowRect = true;
            this.btn_Report.IsShowTips = false;
            this.btn_Report.Location = new System.Drawing.Point(1166, 22);
            this.btn_Report.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Report.Name = "btn_Report";
            this.btn_Report.RectColor = System.Drawing.Color.White;
            this.btn_Report.RectWidth = 1;
            this.btn_Report.Size = new System.Drawing.Size(192, 64);
            this.btn_Report.TabIndex = 0;
            this.btn_Report.TabStop = false;
            this.btn_Report.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Report.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Report.TipsText = "";
            this.btn_Report.BtnClick += new System.EventHandler(this.Btn_Report_BtnClick);
            // 
            // btn_History
            // 
            this.btn_History.BackColor = System.Drawing.Color.White;
            this.btn_History.BtnBackColor = System.Drawing.Color.White;
            this.btn_History.BtnFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_History.BtnForeColor = System.Drawing.Color.White;
            this.btn_History.BtnText = "历史趋势";
            this.btn_History.ConerRadius = 10;
            this.btn_History.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_History.EnabledMouseEffect = false;
            this.btn_History.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(115)))), ((int)(((byte)(163)))));
            this.btn_History.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_History.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_History.Image = null;
            this.btn_History.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_History.ImageFontIcons = null;
            this.btn_History.IsRadius = true;
            this.btn_History.IsShowRect = true;
            this.btn_History.IsShowTips = false;
            this.btn_History.Location = new System.Drawing.Point(886, 22);
            this.btn_History.Margin = new System.Windows.Forms.Padding(0);
            this.btn_History.Name = "btn_History";
            this.btn_History.RectColor = System.Drawing.Color.White;
            this.btn_History.RectWidth = 1;
            this.btn_History.Size = new System.Drawing.Size(192, 64);
            this.btn_History.TabIndex = 0;
            this.btn_History.TabStop = false;
            this.btn_History.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_History.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_History.TipsText = "";
            // 
            // btn_Alarm
            // 
            this.btn_Alarm.BackColor = System.Drawing.Color.White;
            this.btn_Alarm.BtnBackColor = System.Drawing.Color.White;
            this.btn_Alarm.BtnFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Alarm.BtnForeColor = System.Drawing.Color.White;
            this.btn_Alarm.BtnText = "报警日志";
            this.btn_Alarm.ConerRadius = 10;
            this.btn_Alarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Alarm.EnabledMouseEffect = false;
            this.btn_Alarm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(115)))), ((int)(((byte)(163)))));
            this.btn_Alarm.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Alarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Alarm.Image = null;
            this.btn_Alarm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Alarm.ImageFontIcons = null;
            this.btn_Alarm.IsRadius = true;
            this.btn_Alarm.IsShowRect = true;
            this.btn_Alarm.IsShowTips = false;
            this.btn_Alarm.Location = new System.Drawing.Point(592, 22);
            this.btn_Alarm.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Alarm.Name = "btn_Alarm";
            this.btn_Alarm.RectColor = System.Drawing.Color.White;
            this.btn_Alarm.RectWidth = 1;
            this.btn_Alarm.Size = new System.Drawing.Size(192, 64);
            this.btn_Alarm.TabIndex = 0;
            this.btn_Alarm.TabStop = false;
            this.btn_Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Alarm.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Alarm.TipsText = "";
            this.btn_Alarm.BtnClick += new System.EventHandler(this.Btn_Alarm_BtnClick);
            // 
            // btn_ParaSet
            // 
            this.btn_ParaSet.BackColor = System.Drawing.Color.White;
            this.btn_ParaSet.BtnBackColor = System.Drawing.Color.White;
            this.btn_ParaSet.BtnFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_ParaSet.BtnForeColor = System.Drawing.Color.White;
            this.btn_ParaSet.BtnText = "参数设置";
            this.btn_ParaSet.ConerRadius = 10;
            this.btn_ParaSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ParaSet.EnabledMouseEffect = false;
            this.btn_ParaSet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(115)))), ((int)(((byte)(163)))));
            this.btn_ParaSet.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_ParaSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_ParaSet.Image = null;
            this.btn_ParaSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ParaSet.ImageFontIcons = null;
            this.btn_ParaSet.IsRadius = true;
            this.btn_ParaSet.IsShowRect = true;
            this.btn_ParaSet.IsShowTips = false;
            this.btn_ParaSet.Location = new System.Drawing.Point(302, 22);
            this.btn_ParaSet.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ParaSet.Name = "btn_ParaSet";
            this.btn_ParaSet.RectColor = System.Drawing.Color.White;
            this.btn_ParaSet.RectWidth = 1;
            this.btn_ParaSet.Size = new System.Drawing.Size(192, 64);
            this.btn_ParaSet.TabIndex = 0;
            this.btn_ParaSet.TabStop = false;
            this.btn_ParaSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_ParaSet.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_ParaSet.TipsText = "";
            this.btn_ParaSet.BtnClick += new System.EventHandler(this.Btn_ParaSet_BtnClick);
            // 
            // btn_View
            // 
            this.btn_View.BackColor = System.Drawing.Color.White;
            this.btn_View.BtnBackColor = System.Drawing.Color.White;
            this.btn_View.BtnFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_View.BtnForeColor = System.Drawing.Color.White;
            this.btn_View.BtnText = "运行控制";
            this.btn_View.ConerRadius = 10;
            this.btn_View.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_View.EnabledMouseEffect = false;
            this.btn_View.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(115)))), ((int)(((byte)(163)))));
            this.btn_View.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_View.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_View.Image = null;
            this.btn_View.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_View.ImageFontIcons = null;
            this.btn_View.IsRadius = true;
            this.btn_View.IsShowRect = true;
            this.btn_View.IsShowTips = false;
            this.btn_View.Location = new System.Drawing.Point(49, 22);
            this.btn_View.Margin = new System.Windows.Forms.Padding(0);
            this.btn_View.Name = "btn_View";
            this.btn_View.RectColor = System.Drawing.Color.White;
            this.btn_View.RectWidth = 1;
            this.btn_View.Size = new System.Drawing.Size(192, 64);
            this.btn_View.TabIndex = 0;
            this.btn_View.TabStop = false;
            this.btn_View.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_View.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_View.TipsText = "";
            this.btn_View.BtnClick += new System.EventHandler(this.Btn_View_BtnClick);
            // 
            // lbl_PlcState
            // 
            this.lbl_PlcState.AutoSize = true;
            this.lbl_PlcState.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PlcState.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PlcState.ForeColor = System.Drawing.Color.White;
            this.lbl_PlcState.Location = new System.Drawing.Point(1800, 41);
            this.lbl_PlcState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_PlcState.Name = "lbl_PlcState";
            this.lbl_PlcState.Size = new System.Drawing.Size(123, 31);
            this.lbl_PlcState.TabIndex = 30;
            this.lbl_PlcState.Text = "PLC State";
            this.lbl_PlcState.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseDown);
            this.lbl_PlcState.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseMove);
            this.lbl_PlcState.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frm_MouseUp);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(2000, 1102);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Config)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.spl_Main.Panel1.ResumeLayout(false);
            this.spl_Main.Panel2.ResumeLayout(false);
            this.spl_Main.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spl_Main)).EndInit();
            this.spl_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer spl_Main;
        private HZH_Controls.Controls.UCBtnImg btn_View;
        private HZH_Controls.Controls.UCBtnImg ucBtnImg6;
        private HZH_Controls.Controls.UCBtnImg btn_Report;
        private HZH_Controls.Controls.UCBtnImg btn_History;
        private HZH_Controls.Controls.UCBtnImg btn_Alarm;
        private HZH_Controls.Controls.UCBtnImg btn_ParaSet;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.Label lbl_LoginName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_PlcState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pan_Main;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pic_Config;
        private System.Windows.Forms.PictureBox pictureBox4;
        private HZH_Controls.Controls.UCSignalLamp usl_ConnectLemp;
        private System.Windows.Forms.Label lbl_Title;
    }
}

