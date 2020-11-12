using Module;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewScada
{
    public partial class FrmSearchAndAddCamera : Form
    {
        List<CameraInfo> lc;
        private List<CameraInfo> AddList = new List<CameraInfo>();
        private MyCamera m_pMyCamera;
        public FrmSearchAndAddCamera()
        {
            InitializeComponent();
        }

        private void FrmSearchAndAddCamera_Load(object sender, EventArgs e)
        {
            try
            {
                //1.创建文件流，打开文件
                using (FileStream fsRead = new FileStream(Directory.GetCurrentDirectory() + "//.gra", FileMode.Open))
                {
                    //2.创建二进制序列化器
                    BinaryFormatter bfRead = new BinaryFormatter();
                    AddList = (List<CameraInfo>)bfRead.Deserialize(fsRead);
                };
            }
            catch
            {
            }
            UpdataListViewItem();
            lc = CameraDevice.DeviceListAcq();
            comboBox1.DataSource = lc;
        }
        private void UpdataListViewItem()
        {
            listView1.Items.Clear();
            foreach (var VARIABLE in AddList)
            {
                listView1.Items.Add(((CameraInfo)VARIABLE).ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lc = CameraDevice.DeviceListAcq();
            comboBox1.DataSource = lc;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ch:IP转换 | en:IP conversion
            IPAddress clsIpAddr;
            if (false == IPAddress.TryParse(textBox1.Text, out clsIpAddr))
            {
                MessageBox.Show("IP地址格式不合法");
                return;
            }
            long nIp = IPAddress.NetworkToHostOrder(clsIpAddr.Address);

            // ch:掩码转换 | en:Mask conversion
            IPAddress clsSubMask;
            if (false == IPAddress.TryParse(textBox2.Text, out clsSubMask))
            {
                MessageBox.Show("子网掩码格式不合法");
                return;
            }
            long nSubMask = IPAddress.NetworkToHostOrder(clsSubMask.Address);

            // ch:网关转换 | en:Gateway conversion
            IPAddress clsDefaultWay;
            if (false == IPAddress.TryParse(textBox3.Text, out clsDefaultWay))
            {
                MessageBox.Show("网关格式不合法");
                return;
            }
            long nDefaultWay = IPAddress.NetworkToHostOrder(clsDefaultWay.Address);
            IntPtr ptr = CameraDevice.GetIntPtrForSerialNumber(((CameraInfo)comboBox1.SelectedItem).SeriaNumber);
            if (ptr == IntPtr.Zero) return;

            MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(ptr,
                    typeof(MyCamera.MV_CC_DEVICE_INFO));

            // ch:打开设备 | en:Open device
            if (null == m_pMyCamera)
            {
                m_pMyCamera = new MyCamera();
                if (null == m_pMyCamera)
                {
                    return;
                }
            }

            int nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            nRet = m_pMyCamera.MV_GIGE_ForceIpEx_NET((uint)(nIp >> 32), (uint)(nSubMask >> 32), (uint)(nDefaultWay >> 32));
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }
            m_pMyCamera = null;
            lc = CameraDevice.DeviceListAcq();
            int index = comboBox1.SelectedIndex;
            comboBox1.DataSource = lc;
            comboBox1.SelectedIndex = index;
            comboBox1_SelectedIndexChanged(null, null);
            GC.Collect();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = ((CameraInfo)comboBox1.SelectedItem).IPAddress;
            textBox2.Text = ((CameraInfo)comboBox1.SelectedItem).CurrentSubNetMask;
            textBox3.Text = ((CameraInfo)comboBox1.SelectedItem).DefultGateWay;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;
            if (AddList.Find((CameraInfo info) =>
                info.SeriaNumber == ((CameraInfo)comboBox1.SelectedItem).SeriaNumber) == null)
            {
                AddList.Add((CameraInfo)comboBox1.SelectedItem);
                UpdataListViewItem();
            }
            else
            {
                MessageBox.Show("相机已添加");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddList.RemoveAt(listView1.FocusedItem.Index);
            UpdataListViewItem();
        }

        private void FrmSearchAndAddCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AddList != null)
            {
                FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + "//.gra", FileMode.Create);
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(fileStream, AddList);
                fileStream.Close();
            }
        }
    }
}
