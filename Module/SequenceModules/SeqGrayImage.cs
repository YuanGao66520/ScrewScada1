using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Module.SequenceModules
{

    public class SeqGrayImage
    {
        public delegate void LoadImage(object sender, string path);
        public event LoadImage OnImageFileLoad;
        private int exposureTime = 35000;
        private string fileName;
        private eImageSourse imageSourse;
        private double length1;
        private double length2;
        private double center1;
        private double center2;
        private double ratate;
        public int ExposureTime
        {
            get { return exposureTime; }
            set
            {
                if (value > 100 && value < 500000)
                {
                    exposureTime = value;
                }
            }
        }

        [Category("文件")]
        [DisplayName("数据文件")]
        [Description("用户数据列表文件路径")]
        [ReadOnlyAttribute(false), Browsable(true)]
        [Editor(typeof(PropertyGridFileSelector), typeof(UITypeEditor))]
        public string ImageFile
        {
            get { return fileName; }
            set
            {
                if (ImageSourse == eImageSourse.File)
                {
                    OnImageFileLoad?.Invoke(new object(), value);
                }
                fileName = value;
            }
        }

        public eImageSourse ImageSourse
        {
            get { return imageSourse; }
            set
            {
                if (!string.IsNullOrEmpty(fileName) && value == eImageSourse.File) OnImageFileLoad?.Invoke(new object(), fileName);
                imageSourse = value;
            }
        }

        // public string   Name { get; set; }

        public string SaveImage { get; set; }
        [ReadOnlyAttribute(true), Browsable(true)]
        [CategoryAttribute("测量结果"), DescriptionAttribute("被测量矩形的长（MM）")]
        public double Length1 { get { return Math.Round(length1, 2); } set { length1 = value; } }
        [ReadOnlyAttribute(true), Browsable(true)]
        [CategoryAttribute("测量结果"), DescriptionAttribute("被测量矩形的宽（MM）")]
        public double Length2 { get { return Math.Round(length2, 2); } set { length2 = value; } }
        [ReadOnlyAttribute(true), Browsable(true)]
        [CategoryAttribute("测量结果"), DescriptionAttribute("中心坐标X（MM）")]
        public double CenterX { get { return Math.Round(center1, 2); } set { center1 = value; } }
        [ReadOnlyAttribute(true), Browsable(true)]
        [CategoryAttribute("测量结果"), DescriptionAttribute("中心坐标Y（MM）")]
        public double CenterY { get { return Math.Round(center2, 2); } set { center2 = value; } }
        [ReadOnlyAttribute(true), Browsable(true)]
        [CategoryAttribute("测量结果"), DescriptionAttribute("旋转（弧度）")]
        public double Rotate { get { return Math.Round(ratate, 2); } set { ratate = value; } }

        public enum eImageSourse
        {
            Camera = 1,
            File = 2
        }
        public enum camera
        {
            Camera_1 = 1,
            Camera_2 = 2,
            Camera_3 = 4,
            Camera_4 = 8,
        }
    }
    public class PropertyGridFileSelector : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {

                // 可以打开任何特定的对话框  
                OpenFileDialog dialog = new OpenFileDialog
                {
                    AddExtension = false,
                    Title = "打开文件",
                    Filter = "*.bmp|*.bmp"
                };
                if (dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    return dialog.FileName;
                }
            }
            return value;
        }
    }
}
