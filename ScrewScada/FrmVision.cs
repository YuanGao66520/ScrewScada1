using HalconDotNet;
using Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewScada
{
    public partial class FrmVision : Form
    {
        
        public FrmVision()
        {
            InitializeComponent();
            this.Load += FrmVision_Load;
        }
        HObject ho_Image;
        HImage hImage = new HImage();
        public TCPServer tCPServer;
        private void FrmVision_Load(object sender, EventArgs e)
        {
            CameraDevice.OnGrabed += CameraDevice_OnGrabed;
            tCPServer.OnConnected += TCPServer_OnConnected;
            tCPServer.OnDisConnect += TCPServer_OnDisConnect;
            tCPServer.OnReceiveMessage += TCPServer_OnReceiveMessage;
        }

        #region TCP服务器
        private void TCPServer_OnReceiveMessage(object sender, EventMessage e)
        {
            if (e.Msg == "T1")
            {
                Task.Factory.StartNew(new Action(Test1));
            }
            else if(e.Msg == "T2")
            {
                Task.Factory.StartNew(new Action(Test2));
            }
            else if (e.Msg == "T3")
            {
                Task.Factory.StartNew(new Action(Test3));
            }
            else if (e.Msg == "T4")
            {
                Task.Factory.StartNew(new Action(Test4));
            }
            e.Msg = "OK";
            tCPServer.SendMessage(e);
        }

        private void TCPServer_OnDisConnect(object sender, EventArgs e)
        {

        }

        private void TCPServer_OnConnected(object sender, string e)
        {

        }
        HTuple imageWidth, imageHeight;
        private void Test1()
        {
            ho_Image?.Dispose();
            HOperatorSet.ReadImage(out ho_Image, @"L:\防火砖缺陷检测\2000W相机\Image_20200905114413372.bmp");
            HOperatorSet.GetImageSize(ho_Image, out imageWidth, out imageHeight);
            HOperatorSet.SetPart(HWC1.HalconWindow, 0, 0,imageHeight, imageWidth);
            HWC1.HalconWindow.DispObj(ho_Image);
        }
        private void Test2()
        {
            ho_Image?.Dispose();
            HOperatorSet.ReadImage(out ho_Image, @"L:\防火砖缺陷检测\2000W相机\Image_20200905114413372.bmp");
            HOperatorSet.GetImageSize(ho_Image, out imageWidth, out imageHeight);
            HOperatorSet.SetPart(HWC2.HalconWindow, 0, 0, imageHeight, imageWidth);
            HWC2.HalconWindow.DispObj(ho_Image);
        }
        private void Test3()
        {
            ho_Image?.Dispose();
            HOperatorSet.ReadImage(out ho_Image, @"L:\防火砖缺陷检测\2000W相机\Image_20200905114413372.bmp");
            HOperatorSet.GetImageSize(ho_Image, out imageWidth, out imageHeight);
            HOperatorSet.SetPart(HWC3.HalconWindow, 0, 0, imageHeight, imageWidth);
            HWC3.HalconWindow.DispObj(ho_Image);
        }
        private void Test4()
        {
            ho_Image?.Dispose();
            HOperatorSet.ReadImage(out ho_Image, @"L:\防火砖缺陷检测\2000W相机\Image_20200905114413372.bmp");
            HOperatorSet.GetImageSize(ho_Image, out imageWidth, out imageHeight);
            HOperatorSet.SetPart(HWC4.HalconWindow, 0, 0, imageHeight, imageWidth);
            HWC4.HalconWindow.DispObj(ho_Image);
        }
        #endregion

        private void CameraDevice_OnGrabed(object sender, Bitmap e)
        {
            if (e == null) return;
            //ho_Image = BmpToHObject_8(bitmap);
            ho_Image = Bitmap2HImage_24(e);
                  CommonMethods.coddingList.Insert(0, new Module.Coordinates()
            {
                CurrentTime = DateTime.Now.ToString("HH:mm:ss"),
                CameraX = 1,
                CameraY = 2,
                CameraU = 3,
                OKorNG = "Ok"

            });
            HWC1.HalconWindow.DispImage((HImage)ho_Image);
            this.Invoke(new Action(() => {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = CommonMethods.coddingList;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
                //设置dataGridViews始终选中最后一行
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[dataGridView1.RowCount - 1].Index;
                //HOperatorSet.DispText(hWindowControl1.HalconWindow, CommonMethods.coddingList.Count.ToString(), "image", 100, 30, new HTuple("green"), new HTuple(), new HTuple());
                //HOperatorSet.WriteString(hWindowControl1.HalconWindow,"Test String");
            }));
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int nRet = CameraDevice.StartGrab(CommonMethods.cameraInfos[0].SeriaNumber);
            if (nRet == 0)
            {

            }
        }
        HImage Bitmap2HImage_24(Bitmap bImage)
        {
            BitmapData bmData = null;
            Rectangle rect;
            IntPtr pBitmap;
            IntPtr pPixels;
            rect = new Rectangle(0, 0, bImage.Width, bImage.Height);
            bmData = bImage.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            pBitmap = bmData.Scan0;
            pPixels = pBitmap;
            hImage.GenImageInterleaved(pPixels, "rgb", bImage.Width, bImage.Height, -1, "byte", 0, 0, 0, 0, -1, 0);
            bImage.UnlockBits(bmData);
            return hImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CameraDevice.StopGrab(CommonMethods.cameraInfos[0].SeriaNumber);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CameraDevice.OpenTriggerMode(TriggerModule.SoftWareTrigger, CommonMethods.cameraInfos[0]);
            CameraDevice.TriggerExec(CommonMethods.cameraInfos[0].SeriaNumber);
        }

        //private void CamsGetImageEvent(object sender, GetImageEventArgs e)
        //{
        //    try
        //    {
        //        if (!hikcamera.m_IsConnect)
        //            return;

        //        if (InvokeRequired)
        //        {
        //            IAsyncResult result = BeginInvoke(new HikCameras.OnCamerasGetImageEventHandler(CamsGetImageEvent), sender, e);
        //            EndInvoke(result);
        //            return;
        //        }

        //        ho_Image.GenEmptyObj();
        //        if (e.IsColor)
        //        {
        //            HOperatorSet.GenImageInterleaved(out ho_Image, e.pImage, "bgr", e.Width, e.Height, 0, "byte", e.Width, e.Height, 0, 0, -1, 0);
        //        }
        //        else

        //        {
        //            HOperatorSet.GenImage1(out ho_Image, "byte", e.Width, e.Height, e.pImage);
        //        }
        //        HOperatorSet.SetPart(HWC.HalconWindow, 0, 0, e.Height - 1, e.Width - 1);
        //        HOperatorSet.DispObj(ho_Image, HWC.HalconWindow);
        //        showMsglb(count++.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        showMsglb("CamsGetImageEvent!" + ex.Message);
        //    }
        //}
    }
}
