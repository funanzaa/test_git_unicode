namespace OPBKKClaim
{
    partial class FormImportEclaim_DBF_TXT
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
			this.labxBrowseFolder = new DevComponents.DotNetBar.LabelX();
			this.txtBxxBrowseFolder = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.btnxBrowseFolder = new DevComponents.DotNetBar.ButtonX();
			this.lblxMsg = new DevComponents.DotNetBar.LabelX();
			this.pgbImport = new DevComponents.DotNetBar.Controls.ProgressBarX();
			this.btnxImport = new DevComponents.DotNetBar.ButtonX();
			this.btnxClose = new DevComponents.DotNetBar.ButtonX();
			this.btnxExportLog = new DevComponents.DotNetBar.ButtonX();
			this.picBxLoading = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picBxLoading)).BeginInit();
			this.SuspendLayout();
			// 
			// labxBrowseFolder
			// 
			this.labxBrowseFolder.AutoSize = true;
			this.labxBrowseFolder.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.labxBrowseFolder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labxBrowseFolder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labxBrowseFolder.ForeColor = System.Drawing.Color.Black;
			this.labxBrowseFolder.Location = new System.Drawing.Point(50, 12);
			this.labxBrowseFolder.Name = "labxBrowseFolder";
			this.labxBrowseFolder.Size = new System.Drawing.Size(127, 21);
			this.labxBrowseFolder.TabIndex = 18;
			this.labxBrowseFolder.Text = "กรุณาเลือกโฟลเดอร์";
			// 
			// txtBxxBrowseFolder
			// 
			this.txtBxxBrowseFolder.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.txtBxxBrowseFolder.Border.Class = "TextBoxBorder";
			this.txtBxxBrowseFolder.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtBxxBrowseFolder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
			this.txtBxxBrowseFolder.ForeColor = System.Drawing.Color.Black;
			this.txtBxxBrowseFolder.Location = new System.Drawing.Point(50, 33);
			this.txtBxxBrowseFolder.Name = "txtBxxBrowseFolder";
			this.txtBxxBrowseFolder.ReadOnly = true;
			this.txtBxxBrowseFolder.Size = new System.Drawing.Size(328, 26);
			this.txtBxxBrowseFolder.TabIndex = 19;
			// 
			// btnxBrowseFolder
			// 
			this.btnxBrowseFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnxBrowseFolder.AntiAlias = true;
			this.btnxBrowseFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
			this.btnxBrowseFolder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
			this.btnxBrowseFolder.Location = new System.Drawing.Point(384, 33);
			this.btnxBrowseFolder.Name = "btnxBrowseFolder";
			this.btnxBrowseFolder.Size = new System.Drawing.Size(65, 26);
			this.btnxBrowseFolder.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
			this.btnxBrowseFolder.TabIndex = 20;
			this.btnxBrowseFolder.Text = "เลือก";
			this.btnxBrowseFolder.Click += new System.EventHandler(this.btnxBrowseFolder_Click);
			// 
			// lblxMsg
			// 
			this.lblxMsg.AutoSize = true;
			this.lblxMsg.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.lblxMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblxMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblxMsg.ForeColor = System.Drawing.Color.Black;
			this.lblxMsg.Location = new System.Drawing.Point(50, 77);
			this.lblxMsg.Name = "lblxMsg";
			this.lblxMsg.Size = new System.Drawing.Size(236, 21);
			this.lblxMsg.TabIndex = 21;
			this.lblxMsg.Text = "กรุณากดปุ่มนำเข้าข้อมูลเพื่อดำเนินการ";
			// 
			// pgbImport
			// 
			this.pgbImport.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.pgbImport.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.pgbImport.ForeColor = System.Drawing.Color.Black;
			this.pgbImport.Location = new System.Drawing.Point(50, 98);
			this.pgbImport.Name = "pgbImport";
			this.pgbImport.Size = new System.Drawing.Size(399, 13);
			this.pgbImport.TabIndex = 22;
			this.pgbImport.Text = "progressBarX2";
			// 
			// btnxImport
			// 
			this.btnxImport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnxImport.AntiAlias = true;
			this.btnxImport.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
			this.btnxImport.Enabled = false;
			this.btnxImport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
			this.btnxImport.Location = new System.Drawing.Point(50, 127);
			this.btnxImport.Name = "btnxImport";
			this.btnxImport.Size = new System.Drawing.Size(104, 26);
			this.btnxImport.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
			this.btnxImport.TabIndex = 23;
			this.btnxImport.Text = "นำเข้าข้อมูล";
			this.btnxImport.Click += new System.EventHandler(this.btnxImport_Click);
			// 
			// btnxClose
			// 
			this.btnxClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnxClose.AntiAlias = true;
			this.btnxClose.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
			this.btnxClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
			this.btnxClose.Location = new System.Drawing.Point(169, 127);
			this.btnxClose.Name = "btnxClose";
			this.btnxClose.Size = new System.Drawing.Size(71, 26);
			this.btnxClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
			this.btnxClose.TabIndex = 24;
			this.btnxClose.Text = "ปิด";
			this.btnxClose.Click += new System.EventHandler(this.btnxClose_Click);
			// 
			// btnxExportLog
			// 
			this.btnxExportLog.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnxExportLog.AntiAlias = true;
			this.btnxExportLog.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
			this.btnxExportLog.Enabled = false;
			this.btnxExportLog.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
			this.btnxExportLog.Location = new System.Drawing.Point(255, 127);
			this.btnxExportLog.Name = "btnxExportLog";
			this.btnxExportLog.Size = new System.Drawing.Size(96, 26);
			this.btnxExportLog.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
			this.btnxExportLog.TabIndex = 64;
			this.btnxExportLog.Text = "Export log";
			this.btnxExportLog.Visible = false;
			this.btnxExportLog.Click += new System.EventHandler(this.btnxExportLog_Click);
			// 
			// picBxLoading
			// 
			this.picBxLoading.BackColor = System.Drawing.Color.White;
			this.picBxLoading.ForeColor = System.Drawing.Color.Black;
			this.picBxLoading.Image = global::OPBKKClaim.Properties.Resources.loader;
			this.picBxLoading.Location = new System.Drawing.Point(12, 82);
			this.picBxLoading.Name = "picBxLoading";
			this.picBxLoading.Size = new System.Drawing.Size(32, 32);
			this.picBxLoading.TabIndex = 65;
			this.picBxLoading.TabStop = false;
			this.picBxLoading.Visible = false;
			// 
			// FormImportEclaim_DBF_TXT
			// 
			this.CaptionFont = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
			this.ClientSize = new System.Drawing.Size(498, 165);
			this.Controls.Add(this.picBxLoading);
			this.Controls.Add(this.btnxClose);
			this.Controls.Add(this.btnxImport);
			this.Controls.Add(this.pgbImport);
			this.Controls.Add(this.lblxMsg);
			this.Controls.Add(this.btnxBrowseFolder);
			this.Controls.Add(this.txtBxxBrowseFolder);
			this.Controls.Add(this.labxBrowseFolder);
			this.Controls.Add(this.btnxExportLog);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormImportEclaim_DBF_TXT";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "นำเข้าข้อมูล e-Claim";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormImportEclaim_DBF_TXT_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.picBxLoading)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labxBrowseFolder;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBxxBrowseFolder;
        private DevComponents.DotNetBar.ButtonX btnxBrowseFolder;
        private DevComponents.DotNetBar.LabelX lblxMsg;
        private DevComponents.DotNetBar.ButtonX btnxImport;
        private DevComponents.DotNetBar.ButtonX btnxClose;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgbImport;
        private DevComponents.DotNetBar.ButtonX btnxExportLog;
        private System.Windows.Forms.PictureBox picBxLoading;

    }
}