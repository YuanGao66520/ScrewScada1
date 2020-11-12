namespace ScrewScada
{
    partial class FrmVision
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lbl_workState1 = new System.Windows.Forms.Label();
            this.lbl_workState2 = new System.Windows.Forms.Label();
            this.lbl_workState3 = new System.Windows.Forms.Label();
            this.lbl_workState4 = new System.Windows.Forms.Label();
            this.HWC1 = new HalconDotNet.HWindowControl();
            this.HWC2 = new HalconDotNet.HWindowControl();
            this.HWC3 = new HalconDotNet.HWindowControl();
            this.HWC4 = new HalconDotNet.HWindowControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1015, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "连续拍照";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(846, 347);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(737, 341);
            this.dataGridView1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1178, 580);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 37);
            this.button2.TabIndex = 3;
            this.button2.Text = "停止拍照";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1301, 570);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 36);
            this.button4.TabIndex = 5;
            this.button4.Text = "手动触发";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lbl_workState1
            // 
            this.lbl_workState1.BackColor = System.Drawing.Color.Black;
            this.lbl_workState1.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_workState1.ForeColor = System.Drawing.Color.Lime;
            this.lbl_workState1.Location = new System.Drawing.Point(363, 9);
            this.lbl_workState1.Name = "lbl_workState1";
            this.lbl_workState1.Size = new System.Drawing.Size(59, 39);
            this.lbl_workState1.TabIndex = 7;
            this.lbl_workState1.Text = "OK";
            // 
            // lbl_workState2
            // 
            this.lbl_workState2.BackColor = System.Drawing.Color.Black;
            this.lbl_workState2.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_workState2.ForeColor = System.Drawing.Color.Lime;
            this.lbl_workState2.Location = new System.Drawing.Point(772, 9);
            this.lbl_workState2.Name = "lbl_workState2";
            this.lbl_workState2.Size = new System.Drawing.Size(68, 39);
            this.lbl_workState2.TabIndex = 8;
            this.lbl_workState2.Text = "OK";
            // 
            // lbl_workState3
            // 
            this.lbl_workState3.BackColor = System.Drawing.Color.Black;
            this.lbl_workState3.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_workState3.ForeColor = System.Drawing.Color.Lime;
            this.lbl_workState3.Location = new System.Drawing.Point(354, 360);
            this.lbl_workState3.Name = "lbl_workState3";
            this.lbl_workState3.Size = new System.Drawing.Size(59, 39);
            this.lbl_workState3.TabIndex = 8;
            this.lbl_workState3.Text = "OK";
            // 
            // lbl_workState4
            // 
            this.lbl_workState4.BackColor = System.Drawing.Color.Black;
            this.lbl_workState4.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_workState4.ForeColor = System.Drawing.Color.Lime;
            this.lbl_workState4.Location = new System.Drawing.Point(772, 351);
            this.lbl_workState4.Name = "lbl_workState4";
            this.lbl_workState4.Size = new System.Drawing.Size(68, 39);
            this.lbl_workState4.TabIndex = 8;
            this.lbl_workState4.Text = "OK";
            // 
            // HWC1
            // 
            this.HWC1.BackColor = System.Drawing.Color.Black;
            this.HWC1.BorderColor = System.Drawing.Color.Black;
            this.HWC1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.HWC1.Location = new System.Drawing.Point(3, 4);
            this.HWC1.Name = "HWC1";
            this.HWC1.Size = new System.Drawing.Size(419, 341);
            this.HWC1.TabIndex = 9;
            this.HWC1.WindowSize = new System.Drawing.Size(419, 341);
            // 
            // HWC2
            // 
            this.HWC2.BackColor = System.Drawing.Color.Black;
            this.HWC2.BorderColor = System.Drawing.Color.Black;
            this.HWC2.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.HWC2.Location = new System.Drawing.Point(428, 4);
            this.HWC2.Name = "HWC2";
            this.HWC2.Size = new System.Drawing.Size(419, 341);
            this.HWC2.TabIndex = 9;
            this.HWC2.WindowSize = new System.Drawing.Size(419, 341);
            // 
            // HWC3
            // 
            this.HWC3.BackColor = System.Drawing.Color.Black;
            this.HWC3.BorderColor = System.Drawing.Color.Black;
            this.HWC3.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.HWC3.Location = new System.Drawing.Point(3, 351);
            this.HWC3.Name = "HWC3";
            this.HWC3.Size = new System.Drawing.Size(419, 341);
            this.HWC3.TabIndex = 9;
            this.HWC3.WindowSize = new System.Drawing.Size(419, 341);
            // 
            // HWC4
            // 
            this.HWC4.BackColor = System.Drawing.Color.Black;
            this.HWC4.BorderColor = System.Drawing.Color.Black;
            this.HWC4.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.HWC4.Location = new System.Drawing.Point(428, 351);
            this.HWC4.Name = "HWC4";
            this.HWC4.Size = new System.Drawing.Size(419, 341);
            this.HWC4.TabIndex = 9;
            this.HWC4.WindowSize = new System.Drawing.Size(419, 341);
            // 
            // FrmVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1982, 704);
            this.Controls.Add(this.lbl_workState2);
            this.Controls.Add(this.lbl_workState4);
            this.Controls.Add(this.lbl_workState3);
            this.Controls.Add(this.lbl_workState1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.HWC4);
            this.Controls.Add(this.HWC3);
            this.Controls.Add(this.HWC2);
            this.Controls.Add(this.HWC1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmVision";
            this.Text = "FrmVision";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lbl_workState1;
        private System.Windows.Forms.Label lbl_workState2;
        private System.Windows.Forms.Label lbl_workState3;
        private System.Windows.Forms.Label lbl_workState4;
        private HalconDotNet.HWindowControl HWC1;
        private HalconDotNet.HWindowControl HWC2;
        private HalconDotNet.HWindowControl HWC3;
        private HalconDotNet.HWindowControl HWC4;
    }
}