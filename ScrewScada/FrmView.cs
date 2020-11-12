using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication;
using HslCommunication.Core;
using HslCommunication.Profinet.Siemens;

namespace ScrewScada
{
    public partial class FrmView : Form
    {

        [Browsable(false)]
        public HslCommunication.Core.IReadWriteNet ReadWriteNet { get; set; }

        public FrmView()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        public void SetReadWrite(IReadWriteNet readWrite)
        {
            ReadWriteNet = readWrite;
            // 启动后台线程，定时读取PLC中的数据，然后在曲线控件中显示
            if (!isThreadRun)
            {
                if (!int.TryParse("10", out timeSleep))
                {
                    MessageBox.Show("Time input wrong！");
                    return;
                }
                isThreadRun = true;
                thread = new Thread(ThreadReadServer);
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                isThreadRun = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Closed += Form1_Closed;
            userCurve1.SetLeftCurve("Test", null);
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            isThreadRun = false;
            Thread.Sleep(10);
            thread.Abort();
            thread.DisableComObjectEagerCleanup();
        }

        private void ThreadReadServer()
        {
            if (ReadWriteNet != null)
            {
                while (isThreadRun)
                {
                    Thread.Sleep(timeSleep);

                    try
                    {
                        OperateResult<uint> read = ReadWriteNet.ReadUInt32("DB1.102");
                        if (read.IsSuccess)
                        {
                            if (isThreadRun) Invoke(new Action(() =>
                            {
                                userCurve1.AddCurveData("Test", read.Content);
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Read failed：" + ex.Message);
                    }
                }
            }
        }


        private void AddDataCurve(short data)
        {
            userCurve1.AddCurveData("A", data);
        }
        // 外加曲线显示

        private Thread thread = null;              // 后台读取的线程
        private int timeSleep = 300;               // 读取的间隔
        private bool isThreadRun = false;          // 用来标记线程的运行状态
        /// <summary>
        /// 退出线程信息
        /// </summary>
        public void ThreadQuit()
        {
            isThreadRun = false;
        }
    }
}
