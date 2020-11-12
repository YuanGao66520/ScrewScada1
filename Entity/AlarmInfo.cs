using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class AlarmInfo
    {

        /// <summary>
        /// 报警的名称
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 报警触发与消除状态
        /// </summary>
        public string AlarmState { get; set; }

        /// <summary>
        /// 报警优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 报警的说明
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 报警时登录用户名称
        /// </summary>
        public string User { get; set; }


        /// <summary>
        /// 报警值
        /// </summary>
        public string AlarmValue { get; set; }

        /// <summary>
        /// 报警触发时间
        /// </summary>
        public DateTime AlarmTime { get; set; }



    }

}
