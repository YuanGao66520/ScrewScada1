using Module;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrewScada
{
    struct _MV_MATCH_INFO_NET_DETECT_
    {
        public UInt64 nReviceDataSize;    // 已接收数据大小  [统计StartGrabbing和StopGrabbing之间的数据量]
        public UInt32 nLostPacketCount;   // 丢失的包数量
        public uint nLostFrameCount;    // 丢帧数量
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public uint[] nReserved;          // 保留
    };
    
    
    public static class CameraDevice
    {
        #region 属性与字段
        static MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        static MyCamera.cbExceptiondelegate pCallBackFunc;
        static MyCamera.cbOutputExdelegate ImageCallback;
        
        static bool m_bGrabbing;
        static UInt32 m_nBufSizeForDriver = 3072 * 2048 * 3;
        static byte[] m_pBufForDriver = new byte[3072 * 2048 * 3];
        static UInt32 m_nBufSizeForSaveImage = 3072 * 2048 * 3  + 2048;
        static byte[] m_pBufForSaveImage = new byte[3072 * 2048 * 3  + 2048];
        public static event EventHandler<Bitmap> OnGrabed;
        public struct CAMERA//定义相机结构体
        {
            public MyCamera Cam_Info;
            public UInt32 m_nBufSizeForSaveImage;
            public byte[] m_pBufForSaveImage;         // 用于保存图像的缓存
            public string SN;
            public int Frames;
            public IntPtr Hander;
        }
        static MyCamera.cbOutputdelegate cbImage;
        static private CAMERA[] m_pMyCamera;
        static int m_nCanOpenDeviceNum;        // ch:设备使用数量 | en:Used Device Number
        static int m_nDevNum;        // ch:在线设备数量 | en:Online Device Number
        public static int[] m_nFrames;      // ch:帧数 | en:Frame Number
        static bool m_bTimerFlag;     // ch:定时器开始计时标志位 | en:Timer Start Timing Flag Bit
        static bool[] m_bSaveImg;    // ch:保存图片标志位 | en:Save Image Flag Bit
        static IntPtr[] m_hDisplayHandle;
        #endregion

        #region 初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        static CameraDevice()
        {
            m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            m_bGrabbing = false;
            m_nCanOpenDeviceNum = 0;
            m_nDevNum = 0;
            DeviceListAcq();
            m_pMyCamera = new CAMERA[2];
            for (int i = 0; i < 2; i++)
            {
                m_pMyCamera[i].m_nBufSizeForSaveImage = 3072 * 2048  * 3 + 2048;
                m_pMyCamera[i].m_pBufForSaveImage = new byte[3072 * 2048 * 3 + 2048];
            }
            m_nFrames = new int[2];
            //cbImage = new MyCamera.cbOutputdelegate(ImageCallBack1);
            ImageCallback = new MyCamera.cbOutputExdelegate(CallbackRGB);
            m_bTimerFlag = false;
            m_bSaveImg = new bool[2];
            m_hDisplayHandle = new IntPtr[2];
            pCallBackFunc = new MyCamera.cbExceptiondelegate(cbExceptiondelegate);
        }
        // ch:取流回调函数 | en:Aquisition Callback Function
        private static void ImageCallBack1(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO pFrameInfo, IntPtr pUser)
        {
            int nIndex = (int)pUser;

            // ch:抓取的帧数 | en:Aquired Frame Number
            ++m_nFrames[nIndex];

            //ch:判断是否需要保存图片 | en:Determine whether to save image
            if (m_bSaveImg[nIndex])
            {
                SaveImage(pData, pFrameInfo, nIndex);
                m_bSaveImg[nIndex] = false;
            }
        }
        public static void ResetMember()
        {
            m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            m_bGrabbing = false;
            m_nCanOpenDeviceNum = 0;
            m_nDevNum = 0;
            DeviceListAcq();
            m_pMyCamera = new CAMERA[2];
            for (int i = 0; i < 2; i++)
            {
                m_pMyCamera[i].Cam_Info = null;
                m_pMyCamera[i].m_nBufSizeForSaveImage = 3072 * 2048 * 3+ 2048;
                m_pMyCamera[i].m_pBufForSaveImage = new byte[3072 * 2048 * 3+ 2048];
            }
            m_nFrames = new int[8];
            //cbImage = new MyCamera.cbOutputdelegate(ImageCallBack1);
            ImageCallback = new MyCamera.cbOutputExdelegate(CallbackRGB);
            m_bTimerFlag = false;
            m_bSaveImg = new bool[2];
            m_hDisplayHandle = new IntPtr[2];
        }

        private static void CallbackRGB(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            int nIndex = (int) pUser;
            int nRet;
            if ((3 * pFrameInfo.nFrameLen + 2048) > m_pMyCamera[nIndex].m_nBufSizeForSaveImage)
            {
                m_pMyCamera[nIndex].m_nBufSizeForSaveImage = 3 * pFrameInfo.nFrameLen + 2048;
                m_pMyCamera[nIndex].m_pBufForSaveImage = new byte[m_pMyCamera[nIndex].m_nBufSizeForSaveImage];
            }
            MyCamera.MvGvspPixelType enDstPixelType;
            if (IsMonoData(pFrameInfo.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            }
            else if (IsColorData(pFrameInfo.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            }
            else
            {
                //ShowErrorMsg("No such pixel type!", 0);
                return;
            }
            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
            MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
            stConverPixelParam.nWidth = pFrameInfo.nWidth;
            stConverPixelParam.nHeight = pFrameInfo.nHeight;
            stConverPixelParam.pSrcData = pData;
            stConverPixelParam.nSrcDataLen = pFrameInfo.nFrameLen;
            stConverPixelParam.enSrcPixelType = pFrameInfo.enPixelType;
            stConverPixelParam.enDstPixelType = enDstPixelType;
            stConverPixelParam.pDstBuffer = pImage;
            stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
            nRet = m_pMyCamera[nIndex].Cam_Info.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }
            if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
            {
                //************************Mono8 转 Bitmap*******************************
                Bitmap bmp = new Bitmap(pFrameInfo.nWidth, pFrameInfo.nHeight, pFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);

                ColorPalette cp = bmp.Palette;
                // init palette
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = Color.FromArgb(i, i, i);
                }
                // set palette back
                bmp.Palette = cp;

                //bmp.Save("image.bmp", ImageFormat.Bmp);
            }
            else
            {
                //*********************RGB8 转 Bitmap**************************
                for (int i = 0; i < pFrameInfo.nHeight; i++)
                {
                    for (int j = 0; j < pFrameInfo.nWidth; j++)
                    {
                        byte chRed = m_pBufForSaveImage[i * pFrameInfo.nWidth * 3 + j * 3];
                        m_pBufForSaveImage[i * pFrameInfo.nWidth * 3 + j * 3] = m_pBufForSaveImage[i * pFrameInfo.nWidth * 3 + j * 3 + 2];
                        m_pBufForSaveImage[i * pFrameInfo.nWidth * 3 + j * 3 + 2] = chRed;
                    }
                }
                try
                {
                    Bitmap bmp = new Bitmap(pFrameInfo.nWidth, pFrameInfo.nHeight, pFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pImage);
                    //bmp.Save("image.bmp", ImageFormat.Bmp);
                    if (OnGrabed != null)
                    {
                        OnGrabed(m_pMyCamera[nIndex].SN, bmp);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        public static List<CameraInfo> DeviceListAcq()
        {
            List<CameraInfo> list = new List<CameraInfo>();
            list.Clear();
            int nRet;
            // ch:创建设备列表 en:Create Device List
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
            if (0 != nRet)
            {
                //ShowErrorMsg("Enumerate devices fail!", 0);
                return null;
            }

            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    list.Add(new CameraInfo()
                    {
                        LayerType = CameraInfo.eLayerType.Gige,
                        UserDefinedName = gigeInfo.chUserDefinedName,
                        SeriaNumber = gigeInfo.chSerialNumber,
                        DeviceName = gigeInfo.chModelName,
                        DeviceVersion = gigeInfo.chDeviceVersion,
                        ManufacturerName = (CameraInfo.Manufacturer)Enum.Parse(typeof(CameraInfo.Manufacturer), gigeInfo.chManufacturerName),
                        ManufacturerSpecificInfo = gigeInfo.chManufacturerSpecificInfo,
                        IPAddress = GetIPStrFromUint(gigeInfo.nCurrentIp),
                        CurrentSubNetMask = GetIPStrFromUint(gigeInfo.nCurrentSubNetMask),
                        DefultGateWay = GetIPStrFromUint(gigeInfo.nDefultGateWay)
                    });
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    list.Add(new CameraInfo()
                    {
                        LayerType = CameraInfo.eLayerType.USB,
                        UserDefinedName = usbInfo.chUserDefinedName,
                        SeriaNumber = usbInfo.chSerialNumber,
                        DeviceName = usbInfo.chModelName,
                        DeviceVersion = usbInfo.chDeviceVersion,
                        ManufacturerName = (CameraInfo.Manufacturer)Enum.Parse(typeof(CameraInfo.Manufacturer), usbInfo.chManufacturerName)
                    });
                }

            }
            return list;
        }
        #endregion

        #region 打开与关闭相机

        public static int OpenDevice(string sn)
        {
            bool bOpened = false;
            if (m_pDeviceList.nDeviceNum == 0)
            {
                //ShowErrorMsg("No device, please select", 0);
                return -1;
            }
            int nRet = -1;

            for (int k = 0; k < m_pDeviceList.nDeviceNum; k++)
            {
                MyCamera.MV_CC_DEVICE_INFO device =
                (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[k],
                                                              typeof(MyCamera.MV_CC_DEVICE_INFO));
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                if (gigeInfo.chSerialNumber == sn)
                {
                    for (int i = 0, j = 0; j < 7; ++i, ++j)
                    {
                        if (m_pMyCamera[i].Cam_Info == null)
                        {
                            m_pMyCamera[i] = new CAMERA();//???
                            m_pMyCamera[i].SN = sn;
                                                          //ch:获取选择的设备信息 | en:Get Selected Device Information
                            //MyCamera.MV_CC_DEVICE_INFO device =
                            //(MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[state],
                            //                                              typeof(MyCamera.MV_CC_DEVICE_INFO));
                            //ch:打开设备 | en:Open Device
                            if (null == m_pMyCamera[i].Cam_Info)
                            {
                                m_pMyCamera[i].Cam_Info = new MyCamera();
                                if (null == m_pMyCamera)
                                {
                                    return -1;
                                }
                            }
                            nRet = m_pMyCamera[i].Cam_Info.MV_CC_CreateDevice_NET(ref device);
                            if (MyCamera.MV_OK != nRet)
                            {
                                return -1;
                            }
                            nRet = m_pMyCamera[i].Cam_Info.MV_CC_OpenDevice_NET();
                            if (MyCamera.MV_OK != nRet)
                            {
                                i--;
                            }
                            else
                            {
                                m_nCanOpenDeviceNum++;

                                // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
                                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                                {
                                    int nPacketSize = m_pMyCamera[i].Cam_Info.MV_CC_GetOptimalPacketSize_NET();
                                    if (nPacketSize > 0)
                                    {
                                        nRet = m_pMyCamera[i].Cam_Info.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                                        if (nRet != MyCamera.MV_OK)
                                        {
                                            Console.WriteLine("Warning: Set Packet Size failed {0:x8}", nRet);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Warning: Get Packet Size failed {0:x8}", nPacketSize);
                                    }
                                }

                                m_pMyCamera[i].Cam_Info.MV_CC_SetEnumValue_NET("TriggerMode", 0);
                                //m_pMyCamera[i].Cam_Info.MV_CC_RegisterImageCallBack_NET(cbImage, (IntPtr)i);
                                //m_pMyCamera[i].Cam_Info.MV_CC_RegisterImageCallBackForBGR_NET(ImageCallback, (IntPtr)i);
                                m_pMyCamera[i].Cam_Info.MV_CC_RegisterImageCallBackForRGB_NET(ImageCallback, (IntPtr)i);
                                m_nFrames[i] = 0;
                                bOpened = true;
                                nRet = m_pMyCamera[i].Cam_Info.MV_CC_RegisterExceptionCallBack_NET(pCallBackFunc, (IntPtr)i);
                                //if (m_nCanOpenDeviceNum == nCameraUsingNum)
                                //{
                                //    break;
                                //}
                            }
                            return 0;

                        }
                       
                    }
                    
                }
                else
                {
                    //未找到相机
                    if (k == m_pDeviceList.nDeviceNum-1) return -1;
                }
            }
        
            
            return 0;
        }
        //根据相机SN码关闭相机
        public static int CloseCamera(string sn)
        {
            // ch:关闭设备 | en:Close Device
            int nRet;
            if (m_nCanOpenDeviceNum== 0)
            {
                return -1;
            }
            for (int i = 0; i < 8; i++)
            {
                if (m_pMyCamera[i].Cam_Info != null)
                {
                    
                    if (m_pMyCamera[i].SN == sn)
                    {
                        nRet = m_pMyCamera[i].Cam_Info.MV_CC_CloseDevice_NET();
                        if (MyCamera.MV_OK != nRet)
                        {
                            return -1;
                        }
                        nRet = m_pMyCamera[i].Cam_Info.MV_CC_DestroyDevice_NET();
                        if (MyCamera.MV_OK != nRet)
                        {
                            return -1;
                        }
                        //关闭成功将相机内存释放
                        m_pMyCamera[i].Cam_Info = null;
                        m_pMyCamera[i].SN = string.Empty;
                        m_pMyCamera[i].Hander = IntPtr.Zero;
                        //找到相机并关闭即退出循环
                        break;
                    }
                    //else
                    //{
                    //    //未在打开的相机列表里找到所指定的相机
                    //    return -1;
                    //}
                }
                
            }
          
            // ch:取流标志位清零 | en:Reset flow flag bit
            //m_bGrabbing = false;
            return 0;
        }

        public static void CloseAllCamera()
        {
            // ch:关闭设备 | en:Close Device
            int nRet;
            for (int i = 0; i < 8; i++)
            {
                if (m_pMyCamera[i].Cam_Info != null)
                {
                    nRet = m_pMyCamera[i].Cam_Info.MV_CC_CloseDevice_NET();
                    if (MyCamera.MV_OK != nRet)
                    {
                        return;
                    }

                    nRet = m_pMyCamera[i].Cam_Info.MV_CC_DestroyDevice_NET();
                    if (MyCamera.MV_OK != nRet)
                    {
                        return;
                    }
                    //关闭成功将相机内存释放
                    m_pMyCamera[i].Cam_Info = null;
                    m_pMyCamera[i].SN = string.Empty;
                    m_pMyCamera[i].Hander = IntPtr.Zero;

                }

            }
        }

        #endregion

        #region 获取对象与数据

        /// <summary>
        /// 根据SN获取相机对象
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        private static MyCamera GetCameraForSerialNumber(string sn)
        {
            for (int i = 0; i < 8; i++)
            {
                if (m_pMyCamera[i].SN == sn && m_pMyCamera[i].Cam_Info != null)
                {
                    return m_pMyCamera[i].Cam_Info;
                }
            }
            return null;
        }
        private static int GetCameraIndexForSerialNumber(string sn)
        {
            for (int i = 0; i < 8; i++)
            {
                if (m_pMyCamera[i].SN == sn && m_pMyCamera[i].Cam_Info != null)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region 获取图像

        public static int  StartGrab(string sn)
        {
            int nRet;
            for (int i = 0; i < 8; i++)
            {
                if (m_pMyCamera[i].SN == sn && m_pMyCamera[i].Cam_Info != null)
                {
                    // ch:开始采集 | en:Start Grabbing
                    nRet = m_pMyCamera[i].Cam_Info.MV_CC_StartGrabbing_NET();
                    if (MyCamera.MV_OK != nRet)
                    {
                        //ShowErrorMsg("Trigger Fail!", nRet);
                        return -1;
                    }
                    // ch:标志位置位true | en:Set position bit true
                    m_bGrabbing = true;
                    // ch:显示 | en:Display
                    //nRet = m_pMyCamera[i].Cam_Info.MV_CC_Display_NET(handle);
                    //m_pMyCamera[i].Hander = handle;
                    if (MyCamera.MV_OK != nRet)
                    {
                        //ShowErrorMsg("Display Fail！", nRet);
                    }

                    break;
                }
            }
            
            return 0;
        }
        public static int StopGrab(string sn)
        {
            int nRet = -1;
            for (int i = 0; i < 8; i++)
            {
                if (m_pMyCamera[i].SN == sn && m_pMyCamera[i].Cam_Info != null)
                {
                    // ch:停止采集 | en:Stop Grabbing
                    nRet = m_pMyCamera[i].Cam_Info.MV_CC_StopGrabbing_NET();
                    if (nRet != MyCamera.MV_OK)
                    {
                        //ShowErrorMsg("Stop Grabbing Fail!", nRet);
                    }

                    // ch:标志位设为false | en:Set flag bit false
                    m_bGrabbing = false;
                    return 0;
                }
            }
            return -1;

        }
        /// <summary>
        /// 选择触发模式
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>

        public static int OpenTriggerMode(TriggerModule module, CameraInfo info)
        {
            int nRet;
            MyCamera.MVCC_ENUMVALUE stEnumValue = new MyCamera.MVCC_ENUMVALUE();

            MyCamera camera = GetCameraForSerialNumber(info.SeriaNumber);
            if (camera != null)
            {
                if (module != TriggerModule.Continuous)
                {
                    // ch:打开触发模式 | en:Open Trigger Mode
                    nRet = camera.MV_CC_SetEnumValue_NET("TriggerMode", 1);

                    // ch:触发源选择:0 - Line0; | en:Trigger source select:0 - Line0;
                    //           1 - Line1;
                    //           2 - Line2;
                    //           3 - Line3;
                    //           4 - Counter;
                    //           7 - Software;
                    if (module == TriggerModule.SoftWareTrigger)
                    {
                        switch (info.ManufacturerName)
                        {
                            case CameraInfo.Manufacturer.Basler:
                                camera.MV_CC_SetEnumValue_NET("TriggerSource", 0);
                                break;
                            case CameraInfo.Manufacturer.Hikvision:
                                camera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                                break;
                            default:
                                break;
                        }
                        if (m_bGrabbing)
                        {

                        }
                    }
                    else
                    {
                        camera.MV_CC_SetEnumValue_NET("TriggerSource", 1);
                    }
                }
                else
                {
                    //设置连续触发模式
                    camera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
                }

                return 0;
            }
            return -1;
        }
        public static void TriggerExec(string sn)
        {
            int nRet;
            MyCamera camera = GetCameraForSerialNumber(sn);
            if (camera != null)
            {
                // ch:触发命令 | en:Trigger command
                nRet = camera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (MyCamera.MV_OK != nRet)
                {
                    //ShowErrorMsg("Trigger Fail!", nRet);
                }
            }

        }

        public static Bitmap GetBitmapImage(CameraInfo info)
        {
            int nRet;
            MyCamera camera = GetCameraForSerialNumber(info.SeriaNumber);
            if (camera == null) return null;
            UInt32 nPayloadSize = 0;
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            nRet = camera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                //ShowErrorMsg("Get PayloadSize failed", nRet);
                return null;
            }
            nPayloadSize = stParam.nCurValue;
            if (nPayloadSize > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];

                // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
                // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }

            IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            // ch:超时获取一帧，超时时间为1秒 | en:Get one frame timeout, timeout is 1 sec
            nRet = camera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref stFrameInfo, 1000);
            if (MyCamera.MV_OK != nRet)
            {
                //ShowErrorMsg("No Data!", nRet);
                return null;
            }

            MyCamera.MvGvspPixelType enDstPixelType;
            if (IsMonoData(stFrameInfo.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            }
            else if (IsColorData(stFrameInfo.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            }
            else
            {
                //ShowErrorMsg("No such pixel type!", 0);
                return null;
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
            MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
            stConverPixelParam.nWidth = stFrameInfo.nWidth;
            stConverPixelParam.nHeight = stFrameInfo.nHeight;
            stConverPixelParam.pSrcData = pData;
            stConverPixelParam.nSrcDataLen = stFrameInfo.nFrameLen;
            stConverPixelParam.enSrcPixelType = stFrameInfo.enPixelType;
            stConverPixelParam.enDstPixelType = enDstPixelType;
            stConverPixelParam.pDstBuffer = pImage;
            stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
            nRet = camera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
            if (MyCamera.MV_OK != nRet)
            {
                return null;
            }

            if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
            {
                //************************Mono8 转 Bitmap*******************************
                Bitmap bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);
                return bmp;
                //ColorPalette cp = bmp.Palette;
                //// init palette
                //for (int i = 0; i < 256; i++)
                //{
                //    cp.Entries[i] = Color.FromArgb(i, i, i);
                //}
                //// set palette back
                //bmp.Palette = cp;

                //bmp.Save("image.bmp", ImageFormat.Bmp);
            }
            else
            {
                //*********************RGB8 转 Bitmap**************************
                for (int i = 0; i < stFrameInfo.nHeight; i++)
                {
                    for (int j = 0; j < stFrameInfo.nWidth; j++)
                    {
                        byte chRed = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3];
                        m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3] = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2];
                        m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2] = chRed;
                    }
                }
                try
                {
                    Bitmap bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pImage);
                    //bmp.Save("image.bmp", ImageFormat.Bmp);
                    return bmp;
                }
                catch
                {
                }

            }
            return null;
        }

        #endregion

        #region 断线重连

        // ch:回调函数 | en:Callback function
        private static void cbExceptiondelegate(uint nMsgType, IntPtr pUser)
        {
            if (nMsgType == MyCamera.MV_EXCEPTION_DEV_DISCONNECT)
            {
                bool m_bDisConnect = false;

                m_bGrabbing = false;
                int nRet = -1;
                // ch:停止采集 | en:Stop Grabbing
                m_pMyCamera[(int)pUser].Cam_Info.MV_CC_StopGrabbing_NET();
                // ch:关闭设备 | en:Close Device
                nRet = m_pMyCamera[(int)pUser].Cam_Info.MV_CC_CloseDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    return;
                }
                nRet = m_pMyCamera[(int)pUser].Cam_Info.MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    return;
                }

                // ch:获取选择的设备信息 | en:Get Used Device Info
                //MyCamera.MV_CC_DEVICE_INFO device =
                //    (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[cbDeviceList.SelectedIndex],
                //                                                  typeof(MyCamera.MV_CC_DEVICE_INFO));
                // ch:在窗体列表中显示设备名 | en:Display device name in the form list
                for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device =
                        (MyCamera.MV_CC_DEVICE_INFO) Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i],
                            typeof(MyCamera.MV_CC_DEVICE_INFO));
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo =
                        (MyCamera.MV_GIGE_DEVICE_INFO) Marshal.PtrToStructure(buffer,
                            typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (gigeInfo.chSerialNumber == m_pMyCamera[(int) pUser].SN)
                    {
                        // ch:打开设备 | en:Open Device
                        while (!m_bDisConnect)
                        {
                            nRet = m_pMyCamera[(int)pUser].Cam_Info.MV_CC_CreateDevice_NET(ref device);
                            if (MyCamera.MV_OK != nRet)
                            {
                                //ShowErrorMsg("Create Camera failed", nRet);
                                m_pMyCamera[(int)pUser].Cam_Info.MV_CC_DestroyDevice_NET();
                                continue;
                            }

                            nRet = m_pMyCamera[(int)pUser].Cam_Info.MV_CC_OpenDevice_NET();
                            if (MyCamera.MV_OK != nRet)
                            {
                                m_pMyCamera[(int)pUser].Cam_Info.MV_CC_DestroyDevice_NET();
                                continue;
                            }
                            else
                            {
                                nRet = InitCamera((int)pUser);
                                if (MyCamera.MV_OK != nRet)
                                {
                                    m_pMyCamera[(int)pUser].Cam_Info.MV_CC_DestroyDevice_NET();
                                    continue;
                                }
                                m_bDisConnect = true;
                            }
                        }

                        break;
                    }

                }

                
            }
        }
       

        private static int InitCamera(int index)
        {
            int nRet = -1;
            nRet = m_pMyCamera[index].Cam_Info.MV_CC_RegisterExceptionCallBack_NET(pCallBackFunc, IntPtr.Zero);
            GC.KeepAlive(pCallBackFunc);
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }

            // ch:开始采集 | en:Start Grabbing
            nRet = m_pMyCamera[index].Cam_Info.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }

            // ch:标志位置位true | en:Set flag bit true
            m_bGrabbing = true;

            // ch:显示 | en:Display
            nRet = m_pMyCamera[index].Cam_Info.MV_CC_Display_NET(m_pMyCamera[index].Hander);
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }

            return MyCamera.MV_OK;
        }
        #endregion

        #region GetSet
        private static Boolean IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return false;
            }
        }

        /************************************************************************
         *  @fn     IsColorData()
         *  @brief  判断是否是彩色数据
         *  @param  enGvspPixelType         [IN]           像素格式
         *  @return 成功，返回0；错误，返回-1 
         ************************************************************************/
        private static Boolean IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YCBCR411_8_CBYYCRYY:
                    return true;

                default:
                    return false;
            }
        }
        public static IntPtr GetIntPtrForSerialNumber(string sn)
        {
            if (m_pDeviceList.nDeviceNum == 0)
            {
                //ShowErrorMsg("No device, please select", 0);
                return IntPtr.Zero;
            }

            int nRet = -1;

            for (int k = 0; k < m_pDeviceList.nDeviceNum; k++)
            {
                MyCamera.MV_CC_DEVICE_INFO device =
                    (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[k],
                        typeof(MyCamera.MV_CC_DEVICE_INFO));
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                if (gigeInfo.chSerialNumber == sn)
                {
                    return m_pDeviceList.pDeviceInfo[k];
                }
            }
            return IntPtr.Zero;
        }

        public static int GetGrabFrames(CameraInfo info)
        {
            int nRet = GetCameraIndexForSerialNumber(info.SeriaNumber);
            if (nRet == -1)
            {
                return 0;
            }
            return m_nFrames[nRet];
        }

        // ch:获取丢帧数 | en:Get Throw Frame Number
        public static uint GetLostFrame(CameraInfo info)
        {
            MyCamera.MV_CC_DEVICE_INFO stDevInfo = new MyCamera.MV_CC_DEVICE_INFO();
            MyCamera camera = GetCameraForSerialNumber(info.SeriaNumber);
            if (camera == null) return 0;
            int nRet = camera.MV_CC_GetDeviceInfo_NET(ref stDevInfo);

            if (stDevInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                MyCamera.MV_ALL_MATCH_INFO pstInfo = new MyCamera.MV_ALL_MATCH_INFO();
                _MV_MATCH_INFO_NET_DETECT_ MV_NetInfo = new _MV_MATCH_INFO_NET_DETECT_();
                pstInfo.nInfoSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(_MV_MATCH_INFO_NET_DETECT_));
                pstInfo.nType = 0x00000001;
                int size = Marshal.SizeOf(MV_NetInfo);
                pstInfo.pInfo = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(MV_NetInfo, pstInfo.pInfo, false);

                camera.MV_CC_GetAllMatchInfo_NET(ref pstInfo);
                MV_NetInfo = (_MV_MATCH_INFO_NET_DETECT_)Marshal.PtrToStructure(pstInfo.pInfo, typeof(_MV_MATCH_INFO_NET_DETECT_));

                uint sTemp = MV_NetInfo.nLostFrameCount;
                Marshal.FreeHGlobal(pstInfo.pInfo);
                return sTemp;
            }
            else// ch:如果不是Gige设备，默认为U3V设备 | en:If not Gige device, default U3V device
            {
                MyCamera.MV_ALL_MATCH_INFO pstInfo = new MyCamera.MV_ALL_MATCH_INFO();
                MyCamera.MV_MATCH_INFO_USB_DETECT MV_NetInfo = new MyCamera.MV_MATCH_INFO_USB_DETECT();
                pstInfo.nInfoSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MyCamera.MV_MATCH_INFO_USB_DETECT));
                pstInfo.nType = 0x00000004;
                int size = Marshal.SizeOf(MV_NetInfo);
                pstInfo.pInfo = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(MV_NetInfo, pstInfo.pInfo, false);

                camera.MV_CC_GetAllMatchInfo_NET(ref pstInfo);
                MV_NetInfo = (MyCamera.MV_MATCH_INFO_USB_DETECT)Marshal.PtrToStructure(pstInfo.pInfo, typeof(MyCamera.MV_MATCH_INFO_USB_DETECT));

                uint sTemp = MV_NetInfo.nErrorFrameCount;
                Marshal.FreeHGlobal(pstInfo.pInfo);
                return sTemp;
            }
        }
        // ch:保存图片 | en:Save image
        private static void SaveImage(IntPtr pData, MyCamera.MV_FRAME_OUT_INFO stFrameInfo, int nIndex)
        {
            string[] path = { "image1.bmp", "image2.bmp", "image3.bmp", "image4.bmp" };
            int nRet;

            if ((3 * stFrameInfo.nFrameLen + 2048) > m_pMyCamera[nIndex].m_nBufSizeForSaveImage)
            {
                m_pMyCamera[nIndex].m_nBufSizeForSaveImage = 3 * stFrameInfo.nFrameLen + 2048;
                m_pMyCamera[nIndex].m_pBufForSaveImage = new byte[m_pMyCamera[nIndex].m_nBufSizeForSaveImage];
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pMyCamera[nIndex].m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
            stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
            stSaveParam.enPixelType = stFrameInfo.enPixelType;
            stSaveParam.pData = pData;
            stSaveParam.nDataLen = stFrameInfo.nFrameLen;
            stSaveParam.nHeight = stFrameInfo.nHeight;
            stSaveParam.nWidth = stFrameInfo.nWidth;
            stSaveParam.pImageBuffer = pImage;
            stSaveParam.nBufferSize = m_pMyCamera[nIndex].m_nBufSizeForSaveImage;
            stSaveParam.nJpgQuality = 80;
            nRet = m_pMyCamera[nIndex].Cam_Info.MV_CC_SaveImageEx_NET(ref stSaveParam);
            if (MyCamera.MV_OK != nRet)
            {
                string temp = "No.  + (nIndex + 1).ToString() +" + "Device save Failed!";
                //ShowErrorMsg(temp, 0);
            }
            else
            {
                FileStream file = new FileStream(path[nIndex], FileMode.Create, FileAccess.Write);
                file.Write(m_pMyCamera[nIndex].m_pBufForSaveImage, 0, (int)stSaveParam.nImageLen);
                file.Close();
                string temp = "No." + (nIndex + 1).ToString() + "Device Save Succeed!";
                //ShowErrorMsg(temp, 0);
            }
        }
        #endregion

        #region 保存图像

        //public static int SaveImage(CameraInfo info, ImageFormat format)
        //{
        //    int nRet;
        //    UInt32 nPayloadSize = 0;
        //    MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
        //    nRet = m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
        //    if (MyCamera.MV_OK != nRet)
        //    {
        //        ShowErrorMsg("Get PayloadSize failed", nRet);
        //        return;
        //    }
        //    nPayloadSize = stParam.nCurValue;
        //    if (nPayloadSize > m_nBufSizeForDriver)
        //    {
        //        m_nBufSizeForDriver = nPayloadSize;
        //        m_pBufForDriver = new byte[m_nBufSizeForDriver];

        //        // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
        //        // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
        //        m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
        //        m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
        //    }

        //    IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
        //    MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
        //    // ch:超时获取一帧，超时时间为1秒 | en:Get one frame timeout, timeout is 1 sec
        //    nRet = m_pMyCamera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref stFrameInfo, 1000);
        //    if (MyCamera.MV_OK != nRet)
        //    {
        //        ShowErrorMsg("No Data!", nRet);
        //        return;
        //    }

        //    MyCamera.MvGvspPixelType enDstPixelType;
        //    if (IsMonoData(stFrameInfo.enPixelType))
        //    {
        //        enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
        //    }
        //    else if (IsColorData(stFrameInfo.enPixelType))
        //    {
        //        enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
        //    }
        //    else
        //    {
        //        ShowErrorMsg("No such pixel type!", 0);
        //        return;
        //    }

        //    IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
        //    MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
        //    MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
        //    stConverPixelParam.nWidth = stFrameInfo.nWidth;
        //    stConverPixelParam.nHeight = stFrameInfo.nHeight;
        //    stConverPixelParam.pSrcData = pData;
        //    stConverPixelParam.nSrcDataLen = stFrameInfo.nFrameLen;
        //    stConverPixelParam.enSrcPixelType = stFrameInfo.enPixelType;
        //    stConverPixelParam.enDstPixelType = enDstPixelType;
        //    stConverPixelParam.pDstBuffer = pImage;
        //    stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
        //    nRet = m_pMyCamera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
        //    if (MyCamera.MV_OK != nRet)
        //    {
        //        return;
        //    }

        //    if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
        //    {
        //        //************************Mono8 转 Bitmap*******************************
        //        Bitmap bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);

        //        ColorPalette cp = bmp.Palette;
        //        // init palette
        //        for (int i = 0; i < 256; i++)
        //        {
        //            cp.Entries[i] = Color.FromArgb(i, i, i);
        //        }
        //        // set palette back
        //        bmp.Palette = cp;

        //        bmp.Save("image.bmp", ImageFormat.Bmp);
        //    }
        //    else
        //    {
        //        //*********************RGB8 转 Bitmap**************************
        //        for (int i = 0; i < stFrameInfo.nHeight; i++)
        //        {
        //            for (int j = 0; j < stFrameInfo.nWidth; j++)
        //            {
        //                byte chRed = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3];
        //                m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3] = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2];
        //                m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2] = chRed;
        //            }
        //        }
        //        try
        //        {
        //            Bitmap bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pImage);
        //            bmp.Save("image.bmp", ImageFormat.Bmp);
        //        }
        //        catch
        //        {
        //        }

        //    }
        //}

        #endregion

        #region IP 转换Uint->String
        /// <summary>
        /// 将点分十进制格式的IP转换为UInt格式
        /// </summary>
        /// <param name="ipStr">IP的点分十进制表示</param>
        /// <returns></returns>
        private static uint GetUIntFromIP(string ipStr)
        {
            if (String.IsNullOrEmpty(ipStr) || !IsIPAddress(ipStr)) return 0;
            string[] ipArr = ipStr.Split('.');
            List<byte> ipIntArr = new List<byte>();
            foreach (var s in ipArr)
            {
                ipIntArr.Add(byte.Parse(s));
            }
            uint result = 0;
            for (int i = 0; i < 4; i++)
            {
                result += (uint)(ipIntArr[i] << (24 - i * 8));
            }
            return result;
        }
        /// <summary>
        /// 验证是否是正确的IP地址
        /// </summary>
        /// <param name="ipStr">IP字符串</param>
        /// <returns></returns>
        private static bool IsIPAddress(string IpStr)
        {
            if (String.IsNullOrEmpty(IpStr)) return false;
            Regex regText = new Regex(@"^((1?\d?\d|(2([0-4]\d|5[0-5])))\.){3}(1?\d?\d|(2([0-4]\d|5[0-5])))$");
            return regText.IsMatch(IpStr);
        }
        /// <summary>
        /// 将UInt格式的IP转换为点分十进制格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static string GetIPStrFromUint(uint ip)
        {
            byte first = (byte)(ip >> 24);
            byte second = (byte)((ip - (first << 24)) >> 16);
            byte third = (byte)((ip - (second << 16) - (first << 24)) >> 8);
            byte four = (byte)(ip - (second << 16) - (first << 24) - (third << 8));
            return String.Format("{0}.{1}.{2}.{3}", first, second, third, four);
        }
        /// <summary>
        /// 获取网段之间的IP数量
        /// </summary>
        /// <param name="beginIP"></param>
        /// <param name="endIP"></param>
        /// <returns></returns>
        private static uint GetIPCountFromNet(string beginIP, string endIP)
        {
            if (String.IsNullOrEmpty(beginIP) || String.IsNullOrEmpty(endIP)) return 0;
            return GetUIntFromIP(endIP) - GetUIntFromIP(beginIP) + 1;
        }
        /// <summary>
        /// 获取两个IP之间的IP列表
        /// </summary>
        /// <param name="beginIP"></param>
        /// <param name="lastIP"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetIPListFromNet(string beginIP, string lastIP)
        {
            if (String.IsNullOrEmpty(beginIP) || String.IsNullOrEmpty(lastIP)) yield return null;

            uint beginAddress = GetUIntFromIP(beginIP);
            uint lastAddress = GetUIntFromIP(lastIP);
            uint temp = lastAddress - beginAddress;
            for (uint i = 0; i <= temp; i++)
            {
                uint tempAddress = beginAddress + i;
                //将UInt格式的IP转换为点分十进制表示
                string ipAddress = GetIPStrFromUint(tempAddress);
                yield return ipAddress;
            }
        }
        #endregion
    }
    public enum TriggerModule
    {
        HardTrigger = 1,
        SoftWareTrigger = 2,
        Continuous = 4
    }

    public enum ImageFormat
    {
        BMP = 1,
        JPEG = 2
    }
}
