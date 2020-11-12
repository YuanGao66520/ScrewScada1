using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ScrewScada
{
    public partial class FrmCameraConfig : Form
    {
        public FrmCameraConfig()
        {
            InitializeComponent();
            this.Load += FrmCameraConfig_Load;
        }

        private void FrmCameraConfig_Load(object sender, EventArgs e)
        {
            dockPanel2.Theme = vS2015BlueTheme1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFormMethod("FrmCamera1Config");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFormMethod("FrmCamera2Config");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFormMethod("FrmCamera3Config");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFormMethod("FrmCamera4Config");
        }
        private void OpenFormMethod(string formName)
        {
            //避免重复打开

            DockContent frm = FindDockContent(formName);

            if (frm != null)
            {
                frm.BringToFront();
                frm.Activate();
                return;
            }

            switch (formName)
            {
                case "FrmCamera1Config":
                    new FrmCamera1Config().Show(dockPanel2);
                    break;
                case "FrmCamera2Config":
                    new FrmCamera2Config().Show(dockPanel2);
                    break;
                case "FrmCamera3Config":
                    new FrmCamera3Config().Show(dockPanel2);
                    break;
                case "FrmCamera4Config":
                    new FrmCamera4Config().Show(dockPanel2);
                    break;
                default:
                    break;
            }

        }


        #region 判断当前窗体名称是否已经打开

        private DockContent FindDockContent(string frmName)
        {
            foreach (DockContent item in dockPanel2.Documents)
            {
                if (item.Text == frmName)
                {
                    return item;
                }
            }
            return null;
        }


        #endregion
    }
}
