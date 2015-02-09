namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtDevCnt = new System.Windows.Forms.TextBox();
            this.lstDev = new System.Windows.Forms.ListBox();
            this.cmdInvite = new System.Windows.Forms.Button();
            this.cmdLocate = new System.Windows.Forms.Button();
            this.locateTmr = new System.Windows.Forms.Timer(this.components);
            this.cmdChangIP1 = new System.Windows.Forms.Button();
            this.txtDevSetCnt1 = new System.Windows.Forms.TextBox();
            this.cmdUpgrade = new System.Windows.Forms.Button();
            this.txtDevUpCnt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chktargetAP = new System.Windows.Forms.CheckBox();
            this.txtTargetAP = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chktargetKer = new System.Windows.Forms.CheckBox();
            this.txtTargetKer = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chktargetIP = new System.Windows.Forms.CheckBox();
            this.txtTargetIP = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chktargetModel = new System.Windows.Forms.CheckBox();
            this.txtTargetModel = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtNewAP = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtNewKernal = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstFilesBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.設定 = new System.Windows.Forms.ToolStripMenuItem();
            this.清理FWtemp資料夾 = new System.Windows.Forms.ToolStripMenuItem();
            this.內網環境建置 = new System.Windows.Forms.ToolStripMenuItem();
            this.檢視 = new System.Windows.Forms.ToolStripMenuItem();
            this.重新載入Firmware列表 = new System.Windows.Forms.ToolStripMenuItem();
            this.支援 = new System.Windows.Forms.ToolStripMenuItem();
            this.cPU型號 = new System.Windows.Forms.ToolStripMenuItem();
            this.inviteTmr = new System.Windows.Forms.Timer(this.components);
            this.cmdCloseIn = new System.Windows.Forms.Button();
            this.txtDevDefCnt = new System.Windows.Forms.TextBox();
            this.cmdDefaultIP = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdWeb = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDevCnt
            // 
            this.txtDevCnt.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtDevCnt.Location = new System.Drawing.Point(89, 209);
            this.txtDevCnt.Name = "txtDevCnt";
            this.txtDevCnt.Size = new System.Drawing.Size(39, 22);
            this.txtDevCnt.TabIndex = 5;
            this.txtDevCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lstDev
            // 
            this.lstDev.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDev.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstDev.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDev.FormattingEnabled = true;
            this.lstDev.HorizontalExtent = 1500;
            this.lstDev.HorizontalScrollbar = true;
            this.lstDev.ItemHeight = 16;
            this.lstDev.Location = new System.Drawing.Point(134, 203);
            this.lstDev.Name = "lstDev";
            this.lstDev.Size = new System.Drawing.Size(736, 484);
            this.lstDev.Sorted = true;
            this.lstDev.TabIndex = 4;
            this.lstDev.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstDev_MouseClick);
            // 
            // cmdInvite
            // 
            this.cmdInvite.BackColor = System.Drawing.SystemColors.Control;
            this.cmdInvite.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdInvite.Location = new System.Drawing.Point(8, 203);
            this.cmdInvite.Name = "cmdInvite";
            this.cmdInvite.Size = new System.Drawing.Size(75, 30);
            this.cmdInvite.TabIndex = 3;
            this.cmdInvite.Text = "&Invite";
            this.cmdInvite.UseVisualStyleBackColor = true;
            this.cmdInvite.Click += new System.EventHandler(this.cmdInvite_Click);
            // 
            // cmdLocate
            // 
            this.cmdLocate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdLocate.Enabled = false;
            this.cmdLocate.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdLocate.Location = new System.Drawing.Point(8, 275);
            this.cmdLocate.Name = "cmdLocate";
            this.cmdLocate.Size = new System.Drawing.Size(75, 30);
            this.cmdLocate.TabIndex = 6;
            this.cmdLocate.Text = "Locate";
            this.cmdLocate.UseVisualStyleBackColor = true;
            this.cmdLocate.Click += new System.EventHandler(this.cmdLocate_Click);
            // 
            // locateTmr
            // 
            this.locateTmr.Tick += new System.EventHandler(this.locateTmr_Tick);
            // 
            // cmdChangIP1
            // 
            this.cmdChangIP1.BackColor = System.Drawing.SystemColors.Control;
            this.cmdChangIP1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdChangIP1.Location = new System.Drawing.Point(8, 311);
            this.cmdChangIP1.Name = "cmdChangIP1";
            this.cmdChangIP1.Size = new System.Drawing.Size(75, 30);
            this.cmdChangIP1.TabIndex = 7;
            this.cmdChangIP1.Text = "Chang IP";
            this.cmdChangIP1.UseVisualStyleBackColor = true;
            this.cmdChangIP1.Click += new System.EventHandler(this.cmdChangIP1_Click);
            // 
            // txtDevSetCnt1
            // 
            this.txtDevSetCnt1.Location = new System.Drawing.Point(89, 317);
            this.txtDevSetCnt1.Name = "txtDevSetCnt1";
            this.txtDevSetCnt1.Size = new System.Drawing.Size(39, 22);
            this.txtDevSetCnt1.TabIndex = 8;
            this.txtDevSetCnt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdUpgrade
            // 
            this.cmdUpgrade.BackColor = System.Drawing.SystemColors.Control;
            this.cmdUpgrade.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdUpgrade.Location = new System.Drawing.Point(8, 383);
            this.cmdUpgrade.Name = "cmdUpgrade";
            this.cmdUpgrade.Size = new System.Drawing.Size(75, 30);
            this.cmdUpgrade.TabIndex = 9;
            this.cmdUpgrade.Text = "Upgrade";
            this.cmdUpgrade.UseVisualStyleBackColor = true;
            this.cmdUpgrade.Click += new System.EventHandler(this.cmdUpgrade_Click);
            // 
            // txtDevUpCnt
            // 
            this.txtDevUpCnt.Location = new System.Drawing.Point(89, 389);
            this.txtDevUpCnt.Name = "txtDevUpCnt";
            this.txtDevUpCnt.Size = new System.Drawing.Size(39, 22);
            this.txtDevUpCnt.TabIndex = 10;
            this.txtDevUpCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox9);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(8, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(862, 170);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.groupBox8);
            this.groupBox9.Controls.Add(this.groupBox7);
            this.groupBox9.Controls.Add(this.groupBox6);
            this.groupBox9.Controls.Add(this.groupBox3);
            this.groupBox9.Location = new System.Drawing.Point(448, 15);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(408, 150);
            this.groupBox9.TabIndex = 12;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Search by";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chktargetAP);
            this.groupBox8.Controls.Add(this.txtTargetAP);
            this.groupBox8.Location = new System.Drawing.Point(207, 85);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(195, 61);
            this.groupBox8.TabIndex = 15;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "AP version";
            // 
            // chktargetAP
            // 
            this.chktargetAP.AutoSize = true;
            this.chktargetAP.Location = new System.Drawing.Point(6, 42);
            this.chktargetAP.Name = "chktargetAP";
            this.chktargetAP.Size = new System.Drawing.Size(55, 16);
            this.chktargetAP.TabIndex = 6;
            this.chktargetAP.Text = "Assign";
            this.chktargetAP.UseVisualStyleBackColor = true;
            // 
            // txtTargetAP
            // 
            this.txtTargetAP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTargetAP.Location = new System.Drawing.Point(6, 14);
            this.txtTargetAP.MaxLength = 50;
            this.txtTargetAP.Name = "txtTargetAP";
            this.txtTargetAP.Size = new System.Drawing.Size(182, 22);
            this.txtTargetAP.TabIndex = 5;
            this.txtTargetAP.Leave += new System.EventHandler(this.txtTargetAP_Leave);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chktargetKer);
            this.groupBox7.Controls.Add(this.txtTargetKer);
            this.groupBox7.Location = new System.Drawing.Point(207, 18);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(195, 61);
            this.groupBox7.TabIndex = 14;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Kernel version";
            // 
            // chktargetKer
            // 
            this.chktargetKer.AutoSize = true;
            this.chktargetKer.Location = new System.Drawing.Point(6, 42);
            this.chktargetKer.Name = "chktargetKer";
            this.chktargetKer.Size = new System.Drawing.Size(55, 16);
            this.chktargetKer.TabIndex = 6;
            this.chktargetKer.Text = "Assign";
            this.chktargetKer.UseVisualStyleBackColor = true;
            // 
            // txtTargetKer
            // 
            this.txtTargetKer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTargetKer.Location = new System.Drawing.Point(6, 14);
            this.txtTargetKer.MaxLength = 50;
            this.txtTargetKer.Name = "txtTargetKer";
            this.txtTargetKer.Size = new System.Drawing.Size(182, 22);
            this.txtTargetKer.TabIndex = 5;
            this.txtTargetKer.Leave += new System.EventHandler(this.txtTargetKer_Leave);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chktargetIP);
            this.groupBox6.Controls.Add(this.txtTargetIP);
            this.groupBox6.Location = new System.Drawing.Point(6, 85);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(195, 61);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "IP / 網段";
            this.toolTip1.SetToolTip(this.groupBox6, "ex: 10.0.50.100 / 10.0");
            // 
            // chktargetIP
            // 
            this.chktargetIP.AutoSize = true;
            this.chktargetIP.Location = new System.Drawing.Point(6, 42);
            this.chktargetIP.Name = "chktargetIP";
            this.chktargetIP.Size = new System.Drawing.Size(55, 16);
            this.chktargetIP.TabIndex = 6;
            this.chktargetIP.Text = "Assign";
            this.chktargetIP.UseVisualStyleBackColor = true;
            this.chktargetIP.Click += new System.EventHandler(this.chktargetIP_Click);
            // 
            // txtTargetIP
            // 
            this.txtTargetIP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTargetIP.Location = new System.Drawing.Point(6, 14);
            this.txtTargetIP.Name = "txtTargetIP";
            this.txtTargetIP.Size = new System.Drawing.Size(182, 22);
            this.txtTargetIP.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtTargetIP, "ex: 10.0.50.100 / 10.0");
            this.txtTargetIP.Leave += new System.EventHandler(this.txtTargetIP_Leave);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chktargetModel);
            this.groupBox3.Controls.Add(this.txtTargetModel);
            this.groupBox3.Location = new System.Drawing.Point(6, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 61);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Model name";
            // 
            // chktargetModel
            // 
            this.chktargetModel.AutoSize = true;
            this.chktargetModel.Location = new System.Drawing.Point(6, 42);
            this.chktargetModel.Name = "chktargetModel";
            this.chktargetModel.Size = new System.Drawing.Size(55, 16);
            this.chktargetModel.TabIndex = 6;
            this.chktargetModel.Text = "Assign";
            this.chktargetModel.UseVisualStyleBackColor = true;
            this.chktargetModel.Click += new System.EventHandler(this.chktargetModel_Click);
            // 
            // txtTargetModel
            // 
            this.txtTargetModel.Location = new System.Drawing.Point(6, 14);
            this.txtTargetModel.MaxLength = 20;
            this.txtTargetModel.Name = "txtTargetModel";
            this.txtTargetModel.Size = new System.Drawing.Size(182, 22);
            this.txtTargetModel.TabIndex = 5;
            this.txtTargetModel.Leave += new System.EventHandler(this.txtTargetModel_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtNewAP);
            this.groupBox5.Location = new System.Drawing.Point(287, 63);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(109, 41);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "new AP version";
            // 
            // txtNewAP
            // 
            this.txtNewAP.Location = new System.Drawing.Point(6, 13);
            this.txtNewAP.Name = "txtNewAP";
            this.txtNewAP.Size = new System.Drawing.Size(98, 22);
            this.txtNewAP.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtNewKernal);
            this.groupBox4.Location = new System.Drawing.Point(287, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(109, 41);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "new Kernel version";
            // 
            // txtNewKernal
            // 
            this.txtNewKernal.Location = new System.Drawing.Point(6, 13);
            this.txtNewKernal.Name = "txtNewKernal";
            this.txtNewKernal.Size = new System.Drawing.Size(98, 22);
            this.txtNewKernal.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstFilesBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 155);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Firmware version";
            // 
            // lstFilesBox
            // 
            this.lstFilesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFilesBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstFilesBox.Font = new System.Drawing.Font("細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lstFilesBox.ForeColor = System.Drawing.Color.Black;
            this.lstFilesBox.FormattingEnabled = true;
            this.lstFilesBox.ItemHeight = 16;
            this.lstFilesBox.Location = new System.Drawing.Point(6, 14);
            this.lstFilesBox.Name = "lstFilesBox";
            this.lstFilesBox.Size = new System.Drawing.Size(263, 132);
            this.lstFilesBox.Sorted = true;
            this.lstFilesBox.TabIndex = 2;
            this.lstFilesBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFilesBox_MouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定,
            this.檢視,
            this.支援});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(882, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 設定
            // 
            this.設定.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清理FWtemp資料夾,
            this.內網環境建置});
            this.設定.Name = "設定";
            this.設定.Size = new System.Drawing.Size(43, 20);
            this.設定.Text = "設定";
            // 
            // 清理FWtemp資料夾
            // 
            this.清理FWtemp資料夾.Name = "清理FWtemp資料夾";
            this.清理FWtemp資料夾.Size = new System.Drawing.Size(180, 22);
            this.清理FWtemp資料夾.Text = "清理FW_temp資料夾";
            this.清理FWtemp資料夾.Click += new System.EventHandler(this.清理FWtemp資料夾_Click);
            // 
            // 內網環境建置
            // 
            this.內網環境建置.Name = "內網環境建置";
            this.內網環境建置.Size = new System.Drawing.Size(180, 22);
            this.內網環境建置.Text = "內網環境建置";
            this.內網環境建置.Click += new System.EventHandler(this.內網環境建置_Click);
            // 
            // 檢視
            // 
            this.檢視.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新載入Firmware列表});
            this.檢視.Name = "檢視";
            this.檢視.Size = new System.Drawing.Size(43, 20);
            this.檢視.Text = "檢視";
            // 
            // 重新載入Firmware列表
            // 
            this.重新載入Firmware列表.Name = "重新載入Firmware列表";
            this.重新載入Firmware列表.Size = new System.Drawing.Size(190, 22);
            this.重新載入Firmware列表.Text = "重新載入Firmware列表";
            this.重新載入Firmware列表.Click += new System.EventHandler(this.重新載入Firmware列表_Click);
            // 
            // 支援
            // 
            this.支援.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPU型號});
            this.支援.Name = "支援";
            this.支援.Size = new System.Drawing.Size(43, 20);
            this.支援.Text = "支援";
            // 
            // cPU型號
            // 
            this.cPU型號.Name = "cPU型號";
            this.cPU型號.Size = new System.Drawing.Size(118, 22);
            this.cPU型號.Text = "CPU型號";
            this.cPU型號.Click += new System.EventHandler(this.cPU型號_Click);
            // 
            // inviteTmr
            // 
            this.inviteTmr.Interval = 1000;
            this.inviteTmr.Tick += new System.EventHandler(this.inviteTmr_Tick);
            // 
            // cmdCloseIn
            // 
            this.cmdCloseIn.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCloseIn.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdCloseIn.Location = new System.Drawing.Point(8, 239);
            this.cmdCloseIn.Name = "cmdCloseIn";
            this.cmdCloseIn.Size = new System.Drawing.Size(75, 30);
            this.cmdCloseIn.TabIndex = 22;
            this.cmdCloseIn.Text = "Close invite";
            this.cmdCloseIn.UseVisualStyleBackColor = true;
            this.cmdCloseIn.Click += new System.EventHandler(this.cmdCloseIn_Click);
            // 
            // txtDevDefCnt
            // 
            this.txtDevDefCnt.Location = new System.Drawing.Point(89, 353);
            this.txtDevDefCnt.Name = "txtDevDefCnt";
            this.txtDevDefCnt.Size = new System.Drawing.Size(39, 22);
            this.txtDevDefCnt.TabIndex = 24;
            this.txtDevDefCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDevDefCnt.Visible = false;
            // 
            // cmdDefaultIP
            // 
            this.cmdDefaultIP.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDefaultIP.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdDefaultIP.Location = new System.Drawing.Point(8, 347);
            this.cmdDefaultIP.Name = "cmdDefaultIP";
            this.cmdDefaultIP.Size = new System.Drawing.Size(75, 30);
            this.cmdDefaultIP.TabIndex = 23;
            this.cmdDefaultIP.Text = "Default IP";
            this.cmdDefaultIP.UseVisualStyleBackColor = true;
            this.cmdDefaultIP.Visible = false;
            this.cmdDefaultIP.Click += new System.EventHandler(this.cmdDefaultIP_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 300;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.ReshowDelay = 10;
            // 
            // cmdWeb
            // 
            this.cmdWeb.BackColor = System.Drawing.SystemColors.Control;
            this.cmdWeb.Enabled = false;
            this.cmdWeb.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmdWeb.Location = new System.Drawing.Point(8, 419);
            this.cmdWeb.Name = "cmdWeb";
            this.cmdWeb.Size = new System.Drawing.Size(75, 30);
            this.cmdWeb.TabIndex = 25;
            this.cmdWeb.Text = "Web page";
            this.cmdWeb.UseVisualStyleBackColor = true;
            this.cmdWeb.Click += new System.EventHandler(this.cmdWeb_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 701);
            this.Controls.Add(this.cmdWeb);
            this.Controls.Add(this.txtDevDefCnt);
            this.Controls.Add(this.cmdDefaultIP);
            this.Controls.Add(this.cmdCloseIn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtDevUpCnt);
            this.Controls.Add(this.cmdUpgrade);
            this.Controls.Add(this.txtDevSetCnt1);
            this.Controls.Add(this.cmdChangIP1);
            this.Controls.Add(this.cmdLocate);
            this.Controls.Add(this.txtDevCnt);
            this.Controls.Add(this.lstDev);
            this.Controls.Add(this.cmdInvite);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = " MultiUpdate integration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDevCnt;
        private System.Windows.Forms.ListBox lstDev;
        private System.Windows.Forms.Button cmdInvite;
        private System.Windows.Forms.Button cmdLocate;
        private System.Windows.Forms.Timer locateTmr;
        private System.Windows.Forms.Button cmdChangIP1;
        private System.Windows.Forms.TextBox txtDevSetCnt1;
        private System.Windows.Forms.Button cmdUpgrade;
        private System.Windows.Forms.TextBox txtDevUpCnt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstFilesBox;
        private System.Windows.Forms.TextBox txtNewKernal;
        private System.Windows.Forms.TextBox txtNewAP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 檢視;
        private System.Windows.Forms.ToolStripMenuItem 重新載入Firmware列表;
        private System.Windows.Forms.ToolStripMenuItem 設定;
        private System.Windows.Forms.ToolStripMenuItem 支援;
        private System.Windows.Forms.ToolStripMenuItem cPU型號;
        private System.Windows.Forms.ToolStripMenuItem 清理FWtemp資料夾;
        private System.Windows.Forms.ToolStripMenuItem 內網環境建置;
        private System.Windows.Forms.Timer inviteTmr;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chktargetAP;
        private System.Windows.Forms.TextBox txtTargetAP;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chktargetKer;
        private System.Windows.Forms.TextBox txtTargetKer;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chktargetIP;
        private System.Windows.Forms.TextBox txtTargetIP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chktargetModel;
        private System.Windows.Forms.TextBox txtTargetModel;
        private System.Windows.Forms.Button cmdCloseIn;
        private System.Windows.Forms.TextBox txtDevDefCnt;
        private System.Windows.Forms.Button cmdDefaultIP;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button cmdWeb;
    }
}

