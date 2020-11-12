using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module
{
    [Serializable]
    public class Camera2Serializable
    {
        /// <summary>
        /// 图像模板
        /// </summary>
        public HTuple Modle { get; set; }
        /// <summary>
        /// 起始角度
        /// </summary>
        public HTuple AngleStrat { get; set; }
        /// <summary>
        /// 角度范围
        /// </summary>
        public HTuple AngleExtent { get; set; }
        /// <summary>
        /// 缩放最小
        /// </summary>
        public HTuple ScaleMini { get; set; }
        /// <summary>
        /// 缩放最大
        /// </summary>
        public HTuple ScaleMax{ get; set; }
        /// <summary>
        /// 查找个数
        /// </summary>
        public HTuple NumMatches { get; set; }
        /// <summary>
        /// 贪婪系数 1-不安全但最快，0-安全消耗时间
        /// </summary>
        public HTuple Greendiness { get; set; }
        /// <summary>
        /// 搜索期间使用的金字塔级别数与确定。如有必要，使用模型创建模型 创建缩放形状模型时，将级别数剪切到给定范围。如果数级设置为0，则使用创建缩放形状模型中的金字塔级别数。
        /// </summary>
        public HTuple NumLevels { get; set; }
        /// <summary>
        /// 重叠度（0-1）0-不允许重叠，1-允许
        /// </summary>
        public HTuple MaxOverlap { get; set; }
        
        public HTuple MetrologyHandle { get; set; }
    }
}
