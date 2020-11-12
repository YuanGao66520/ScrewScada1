using Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        aaa aaaaa;
        private void Form1_Load(object sender, EventArgs e)
        {
            //aaaaa = new aaa();
            //aaaaa.File = "sss";
            //propertyGrid1.SelectedObject = aaaaa;
            Module.SequenceModules.SeqGrayImage iii = new Module.SequenceModules.SeqGrayImage();

            propertyGrid1.SelectedObject = iii;

        }
    }
}
