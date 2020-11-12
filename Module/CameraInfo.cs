using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Design;

namespace Module
{
    [Serializable]
    public class CameraInfo
    {

        /// <summary>
        /// 相机类型
        /// </summary>
        [CategoryAttribute("常规"), DescriptionAttribute("用户定义相机名称")]
        public eLayerType LayerType { get; set; }
        /// <summary>
        /// 用户定义相机名称
        /// </summary>
        [Editor("System.Windows.Forms.Design.FileNameEditor", typeof(UITypeEditor))]
        public string UserDefinedName { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SeriaNumber { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 子网掩码
        /// </summary>
        public string CurrentSubNetMask { get; set; }
        /// <summary>
        /// 默认网关
        /// </summary>
        public string DefultGateWay { get; set; }
        /// <summary>
        /// 制造商
        /// </summary>
        public Manufacturer ManufacturerName { get; set; } 
        /// <summary>
        /// 制造商特殊信息
        /// </summary>
        public string ManufacturerSpecificInfo { get; set; }
        /// <summary>
        /// 设备版本
        /// </summary>
        public string DeviceVersion{ get; set; }
        public bool IsOpen { get; set; }
        public bool IsSaveImage { get; set; }
        public bool IsConnected { get; set; }
        public int ExposureTime { get; set; }
        /// <summary>
        /// 曝光延时时间
        /// </summary>
        public int ExposureDelay{ get; set; }
        /// <summary>
        /// 增益
        /// </summary>
        public float Gain { get; set; }

        public override string ToString()
        {
            return DeviceName + "(" + SeriaNumber + ")";
        }
        public enum Manufacturer
        {
            Hikvision = 1,
            Basler = 2,
            Hikrobot = 4
        }
        public enum eLayerType
        {
            Gige = 1,
            USB = 2
        }

    }

    //public Enum LayerType
    //{
    //    Gige = 0;
    //    USB = 1;
    //}
}
