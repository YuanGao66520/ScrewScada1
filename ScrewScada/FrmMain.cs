using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;

using System.IO.Ports;
using System.Threading;
using System.IO;
using HslCommunication.Profinet.Siemens;
using HZH_Controls.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Runtime.Serialization.Formatters.Binary;
using Module;

namespace ScrewScada
{
    public partial class FrmMain : Form
    {
        #region 声明和属性

        private bool IsRun = true;

        private Timer mytimer;

        private string ConfigPath = Application.StartupPath + "\\Config\\set.xml";

        private Image CloseOnImage;
        private Image CloseOffImage;
        private Image MiniOnImage;
        private Image MiniOffImage;

        private Module.TCPServer tCPServer;
        //视觉窗体
        FrmVision fv = new FrmVision();

        FrmView frmView = new FrmView();
        FrmCameraConfig fcc = new FrmCameraConfig();
        #endregion
        public FrmMain()
        {
            InitializeComponent();
            ControlForm();
            mytimer = new Timer();
            mytimer.Interval = 1000;
            mytimer.Tick += Mytimer_Tick;
            mytimer.Enabled = true;
            this.Load += FrmMain_Load;
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            tCPServer = new TCPServer();
            //tCPServer.OnConnected += TCPServer_OnConnected;
            //tCPServer.OnReceiveMessage += TCPServer_OnReceiveMessage;
            //tCPServer.OnDisConnect += TCPServer_OnDisConnect;
            if (tCPServer.OpenServer(5000))
            {
                fv.tCPServer = tCPServer;
                Console.WriteLine("端口5000监听成功" + "\r\t");
            }
            if (DeserializationCameraFile() == 0)
            {
                if (CommonMethods.cameraInfos.Count > 0)
                {
                    if (!CommonMethods.cameraInfos[0].IsOpen)
                    {
                        if (CameraDevice.OpenDevice(CommonMethods.cameraInfos[0].SeriaNumber) == 0)
                        {
                            CommonMethods.cameraInfos[0].IsOpen = true;
                            // MessageBox.Show("相机打开成功");
                        }
                        else
                        {
                            MessageBox.Show("相机打开失败");
                        }
                    }
                    else
                    {
                        MessageBox.Show("相机已被打开");
                    }
                }
                
            }
            //FrmView fv = new FrmView();
            //if (!CloseWindow(fv))
            //{
            //    OpenWindow(fv);
            //    fv.SetReadWrite(siemensTcpNet);
            //}
            OpenWindow(fv);
            OpenWindow(frmView);
            OpenWindow(fcc);
        }

        private int DeserializationCameraFile()
        {

            try
            {
                //1.创建文件流，打开文件
                using (FileStream fsRead = new FileStream(Directory.GetCurrentDirectory() + "//.gra", FileMode.Open))
                {
                    //2.创建二进制序列化器
                    BinaryFormatter bfRead = new BinaryFormatter();
                    CommonMethods.cameraInfos = (List<CameraInfo>)bfRead.Deserialize(fsRead);
                };
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private void Mytimer_Tick(object sender, EventArgs e)
        {
            //更新系统时间
            this.lbl_date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.lbl_Time.Text = DateTime.Now.ToString("HH:mm:ss");

        }

        #region TCP操作
        private void TCPServer_OnDisConnect(object sender, EventArgs e)
        {

        }

        private void TCPServer_OnReceiveMessage(object sender, EventMessage e)
        {

        }

        private void TCPServer_OnConnected(object sender, string e)
        {

        }
        #endregion

        #region 控制窗体运行位置
        private void ControlForm()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(165, 186);
        }

        #endregion

        #region 创建线程与设备进行连接
        private void InitialAndConnect()
        {
            
        }

        #endregion

        #region 解析数据用到的方法

        #region 自定义截取字节数组

        /// <summary>
        /// 自定义截取字节数组
        /// </summary>
        /// <param name="byteArr"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] GetByteArray(byte[] byteArr, int start, int length)
        {
            byte[] Res = new byte[length];
            if (byteArr != null && byteArr.Length >= start + length)
            {
                for (int i = 0; i < length; i++)
                {
                    Res[i] = byteArr[i + start];
                }

            }
            return Res;
        }

        #endregion

        #region 根据字节获取位

        /// <summary>
        /// 根据字节获取位
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetbitValue(byte input, int index)
        {
            switch (index)
            {
                case 0: return ((byte)((input >> 0) & 0x1)).ToString();
                case 1: return ((byte)((input >> 1) & 0x1)).ToString();
                case 2: return ((byte)((input >> 2) & 0x1)).ToString();
                case 3: return ((byte)((input >> 3) & 0x1)).ToString();
                case 4: return ((byte)((input >> 4) & 0x1)).ToString();
                case 5: return ((byte)((input >> 5) & 0x1)).ToString();
                case 6: return ((byte)((input >> 6) & 0x1)).ToString();
                case 7: return ((byte)((input >> 7) & 0x1)).ToString();
                default:
                    return "-1";
            }
        }
        #endregion

        #region 根据字获取位
        /// <summary>
        /// 根据字获取位
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetbitValueWord(byte[] input, int index)
        {
            if (input.Length == 2 && index >= 0 && index < 16)
            {
                byte low = input[0];
                byte high = input[1];
                switch (index)
                {
                    case 0: return ((byte)((low >> 0) & 0x1)).ToString();
                    case 1: return ((byte)((low >> 1) & 0x1)).ToString();
                    case 2: return ((byte)((low >> 2) & 0x1)).ToString();
                    case 3: return ((byte)((low >> 3) & 0x1)).ToString();
                    case 4: return ((byte)((low >> 4) & 0x1)).ToString();
                    case 5: return ((byte)((low >> 5) & 0x1)).ToString();
                    case 6: return ((byte)((low >> 6) & 0x1)).ToString();
                    case 7: return ((byte)((low >> 7) & 0x1)).ToString();
                    case 8: return ((byte)((high >> 0) & 0x1)).ToString();
                    case 9: return ((byte)((high >> 1) & 0x1)).ToString();
                    case 10: return ((byte)((high >> 2) & 0x1)).ToString();
                    case 11: return ((byte)((high >> 3) & 0x1)).ToString();
                    case 12: return ((byte)((high >> 4) & 0x1)).ToString();
                    case 13: return ((byte)((high >> 5) & 0x1)).ToString();
                    case 14: return ((byte)((high >> 6) & 0x1)).ToString();
                    case 15: return ((byte)((high >> 7) & 0x1)).ToString();
                    default:
                        return "-1";
                }
            }
            else
            {
                return "-1";
            }
        }
        #endregion

        #region 八字节顺序转换

        /// <summary>
        /// 八字节顺序转换：1为ABCDEFGH，2为CDABGHED，3为EFGHABCD，4为 HGFEDCBA
        /// </summary>
        /// <param name="byteArr"></param>
        /// <param name="start"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private byte[] GetDoubleByteArray(byte[] byteArr, int start, int type)
        {
            byte[] Res = new byte[8];
            if (byteArr != null && byteArr.Length >= start + 8 && type >= 1 && type <= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    Res[i] = byteArr[i + start];
                }
                byte[] Res1 = new byte[8];
                switch (type)
                {
                    case 1:
                        Res1 = Res;
                        break;
                    case 2:
                        Res1[0] = Res[2];
                        Res1[1] = Res[3];
                        Res1[2] = Res[0];
                        Res1[3] = Res[1];
                        Res1[4] = Res[6];
                        Res1[5] = Res[7];
                        Res1[6] = Res[4];
                        Res1[7] = Res[5];
                        break;
                    case 3:
                        Res1[0] = Res[4];
                        Res1[1] = Res[5];
                        Res1[2] = Res[6];
                        Res1[3] = Res[7];
                        Res1[4] = Res[0];
                        Res1[5] = Res[1];
                        Res1[6] = Res[2];
                        Res1[7] = Res[3];
                        break;
                    case 4:
                        Res1[0] = Res[7];
                        Res1[1] = Res[6];
                        Res1[2] = Res[5];
                        Res1[3] = Res[4];
                        Res1[4] = Res[3];
                        Res1[5] = Res[2];
                        Res1[6] = Res[1];
                        Res1[7] = Res[0];
                        break;
                }
                return Res1;
            }
            return Res;
        }

        #endregion

        #endregion

        #region 窗体移动
        private Point mouseOff;//鼠标移动位置变量
        private bool leftFlag;//标签是否为左键
        private void Frm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y);
                leftFlag = true;
            }
        }
        private void Frm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);//设置移动后的位置
                Location = mouseSet;

            }
        }
        private void Frm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;

            }
        }



        private void UcBtnImg6_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 导航栏窗体操作
        /// <summary>
        /// 关闭窗体的通用方法
        /// </summary>
        /// <param name="Frm"></param>
        /// <returns></returns>
        private bool CloseWindow(Form Frm)
        {
            bool Res = false;
            foreach (Control ct in this.pan_Main.Controls)
            {
                if (ct is Form frm)
                {
                    if (frm.Name == Frm.Name)
                    {
                        Res = true;
                        break;
                    }
                    else
                    {
                        //frm.Close();
                        frm.Visible = false;
                    }
                }
            }
            return Res;
        }
        private void OpenWindow(Form Frm)
        {
            Frm.TopLevel = false;
            Frm.FormBorderStyle = FormBorderStyle.None;
            Frm.Dock = DockStyle.Fill;
            Frm.Parent = this.pan_Main;
            //实现标题栏同步切换
            this.lbl_Title.Text = Frm.Text;
            //实现导航栏按钮同步切换
            BackColorSet(Frm.Text);
            Frm.Show();
        }
        /// <summary>
        /// 根据窗体的名称改变导航栏的按钮背景颜色
        /// </summary>
        /// <param name="FrmText"></param>
        private void BackColorSet(string FrmText)
        {
            //设置所有按钮为未选中颜色
            this.btn_View.FillColor = Color.FromArgb(11, 115, 163);
            //this.btn_Trend.BackColor = Color.FromArgb(11, 115, 163);
            this.btn_Report.FillColor = Color.FromArgb(11, 115, 163);
            this.btn_ParaSet.FillColor = Color.FromArgb(11, 115, 163);
            this.btn_History.FillColor = Color.FromArgb(11, 115, 163);
            this.btn_Alarm.FillColor = Color.FromArgb(11, 115, 163);
            this.btn_View.BtnForeColor = Color.White;
            //this.btn_Trend.BackColor = Color.FromArgb(11, 115, 163);
            this.btn_Report.BtnForeColor = Color.White;
            this.btn_ParaSet.BtnForeColor = Color.White;
            this.btn_History.BtnForeColor = Color.White;

            //根据需要去修改点击按钮的背景颜色

            switch (FrmText)
            {
                case "运行控制":
                    this.btn_View.FillColor = Color.FromArgb(60, 179, 113);
                    this.btn_View.BtnForeColor = Color.Black;
                    break;
                case "参数设置":
                    this.btn_ParaSet.FillColor = Color.FromArgb(60, 179, 113);
                    this.btn_ParaSet.BtnForeColor = Color.Black;
                    break;
                case "报警日志":
                    this.btn_Alarm.FillColor = Color.FromArgb(60, 179, 113);
                    this.btn_Alarm.BtnForeColor = Color.Black;
                    break;
                case "历史趋势":
                    this.btn_History.BackColor = Color.FromArgb(60, 179, 113);
                    break;
                case "InputExcel":
                    this.btn_Report.FillColor = Color.FromArgb(60, 179, 113);
                    this.btn_Report.BtnForeColor = Color.Black;
                    break;
                default:

                    break;
            }
        }
        private void Btn_View_BtnClick(object sender, EventArgs e)
        {
            if (CloseWindow(fv))
            {
                OpenWindow(fv);
            }
        }
        private void Btn_ParaSet_BtnClick(object sender, EventArgs e)
        {
            if (CloseWindow(frmView))
            {
                OpenWindow(frmView);
            }
        }
        private void Btn_Report_BtnClick(object sender, EventArgs e)
        {
           
        }
        private void Btn_Alarm_BtnClick(object sender, EventArgs e)
        {
            if (CloseWindow(fcc))
            {
                OpenWindow(fcc);
            }
        }

        #endregion

        #region 最小化方法
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.WindowState = FormWindowState.Minimized;
        }

        private void SplitContainer1_Panel1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplitContainer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region 关闭窗体按钮的处理
        private void PictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PictureBox2_MouseEnter(object sender, EventArgs e)
        {
            //private Image CloseOnImage;
            //private Image CloseOffImage;
            //private Image MiniOnImage;
            //private Image MiniOffImage;
            if (CloseOnImage == null)
            {
                CloseOnImage = LoadImageToMem("叉号enter.png");
            }
            pictureBox2.Image = CloseOnImage;
        }
        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (CloseOffImage == null)
            {
                CloseOffImage = LoadImageToMem("叉号.png");
            }
            pictureBox2.Image = CloseOffImage;
        }

        /// <summary>
        /// 占用内存较多
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox4_MouseEnter(object sender, EventArgs e)
        {
            if (MiniOnImage == null)
            {
                MiniOnImage = LoadImageToMem("最小化 enter.png");
            }
            pictureBox4.Image = MiniOnImage;
        }
        private void PictureBox4_MouseLeave(object sender, EventArgs e)
        {
            if (MiniOffImage == null)
            {
                MiniOffImage = LoadImageToMem("最小化.png");
            }
            pictureBox4.Image = MiniOffImage;
        }
        private Image LoadImageToMem(string fileName)
        {
            if (File.Exists(Application.StartupPath + "\\Image\\" + fileName))
            {

                using (FileStream fs = new FileStream(Application.StartupPath + "\\Image\\" + fileName, FileMode.Open))
                {
                    long len = fs.Length;
                    byte[] heByte = new byte[len];
                    fs.Read(heByte, 0, (int)len);
                    MemoryStream ms = new MemoryStream();

                    ms.Write(heByte, 0, (int)len);
                    return Image.FromStream(ms);
                };
            }
            return null;
        }

        #endregion

        private void Pic_Config_Click(object sender, EventArgs e)
        {
            FrmSearchAndAddCamera frmSearchAndAddCamera = new FrmSearchAndAddCamera();
            frmSearchAndAddCamera.ShowDialog();
        }

       
    }
}