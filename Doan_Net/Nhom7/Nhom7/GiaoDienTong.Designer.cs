namespace Nhom7
{
    partial class GiaoDienTong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiaoDienTong));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnHT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHT_DN = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHT_DX = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLTK = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLSV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLMT = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnHT,
            this.mnuQLS,
            this.mnuQLTK,
            this.mnuQLSV,
            this.mnuQLMT});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnHT
            // 
            this.mnHT.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHT_DN,
            this.mnuHT_DX});
            this.mnHT.Name = "mnHT";
            this.mnHT.Size = new System.Drawing.Size(71, 20);
            this.mnHT.Text = "Hệ Thống";
            // 
            // mnuHT_DN
            // 
            this.mnuHT_DN.Name = "mnuHT_DN";
            this.mnuHT_DN.Size = new System.Drawing.Size(180, 22);
            this.mnuHT_DN.Text = "Đăng nhập";
            this.mnuHT_DN.Click += new System.EventHandler(this.mnHT_DX_Click);
            // 
            // mnuHT_DX
            // 
            this.mnuHT_DX.Name = "mnuHT_DX";
            this.mnuHT_DX.Size = new System.Drawing.Size(180, 22);
            this.mnuHT_DX.Text = "Đăng xuất";
            this.mnuHT_DX.Click += new System.EventHandler(this.mnHT_DX_Click);
            // 
            // mnuQLS
            // 
            this.mnuQLS.Name = "mnuQLS";
            this.mnuQLS.Size = new System.Drawing.Size(87, 20);
            this.mnuQLS.Text = "Quản lý sách";
            this.mnuQLS.Click += new System.EventHandler(this.mnuQLS_Click);
            // 
            // mnuQLTK
            // 
            this.mnuQLTK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuQLTK.Name = "mnuQLTK";
            this.mnuQLTK.Size = new System.Drawing.Size(112, 20);
            this.mnuQLTK.Text = "Quản lý tài khoản";
            this.mnuQLTK.Click += new System.EventHandler(this.mnuQLTK_Click);
            // 
            // mnuQLSV
            // 
            this.mnuQLSV.Name = "mnuQLSV";
            this.mnuQLSV.Size = new System.Drawing.Size(110, 20);
            this.mnuQLSV.Text = "Quản lý sinh viên";
            this.mnuQLSV.Click += new System.EventHandler(this.mnuQLSV_Click);
            // 
            // mnuQLMT
            // 
            this.mnuQLMT.Name = "mnuQLMT";
            this.mnuQLMT.Size = new System.Drawing.Size(139, 20);
            this.mnuQLMT.Text = "Quản lý mượn trả sách";
            this.mnuQLMT.Click += new System.EventHandler(this.mnuQLMT_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(373, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 217);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.label1.Location = new System.Drawing.Point(219, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(543, 47);
            this.label1.TabIndex = 2;
            this.label1.Text = "Chào mừng bạn đến với thư viện";
            // 
            // GiaoDienTong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(984, 386);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GiaoDienTong";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý thư viện";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.GiaoDienTong_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnHT;
        private System.Windows.Forms.ToolStripMenuItem mnuHT_DN;
        private System.Windows.Forms.ToolStripMenuItem mnuHT_DX;
        private System.Windows.Forms.ToolStripMenuItem mnuQLS;
        private System.Windows.Forms.ToolStripMenuItem mnuQLMT;
        private System.Windows.Forms.ToolStripMenuItem mnuQLTK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem mnuQLSV;
    }
}