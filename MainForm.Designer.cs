namespace UserForm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bt_write = new Button();
            bt_connect = new Button();
            bt_reset = new Button();
            bt_read = new Button();
            tab_Main = new TabControl();
            tab_RSSI1 = new TabPage();
            dtgrid_rssi1 = new DataGridView();
            tab_RSSI2 = new TabPage();
            dtgrid_rssi2 = new DataGridView();
            tab_PLL = new TabPage();
            dtgrid_pll = new DataGridView();
            sPISlaveBindingSource = new BindingSource(components);
            groupBox1 = new GroupBox();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            bt_setting = new Button();
            cbBaudRate = new ComboBox();
            cbCom = new ComboBox();
            UpdateTimer = new System.Windows.Forms.Timer(components);
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            groupBox3 = new GroupBox();
            button1 = new Button();
            button2 = new Button();
            groupBox2 = new GroupBox();
            bt_verify = new Button();
            tbMsg = new TextBox();
            registerItemBindingSource = new BindingSource(components);
            tab_Main.SuspendLayout();
            tab_RSSI1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgrid_rssi1).BeginInit();
            tab_RSSI2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgrid_rssi2).BeginInit();
            tab_PLL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgrid_pll).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sPISlaveBindingSource).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)registerItemBindingSource).BeginInit();
            SuspendLayout();
            // 
            // bt_write
            // 
            bt_write.Location = new Point(146, 92);
            bt_write.Name = "bt_write";
            bt_write.Size = new Size(133, 58);
            bt_write.TabIndex = 1;
            bt_write.Text = "Write to chip";
            bt_write.UseVisualStyleBackColor = true;
            bt_write.Click += bt_write_Click;
            // 
            // bt_connect
            // 
            bt_connect.Location = new Point(126, 54);
            bt_connect.Name = "bt_connect";
            bt_connect.Size = new Size(133, 40);
            bt_connect.TabIndex = 1;
            bt_connect.Text = "Connect";
            bt_connect.UseVisualStyleBackColor = true;
            bt_connect.Click += bt_connect_Click;
            // 
            // bt_reset
            // 
            bt_reset.Location = new Point(6, 26);
            bt_reset.Name = "bt_reset";
            bt_reset.Size = new Size(133, 40);
            bt_reset.TabIndex = 1;
            bt_reset.Text = "Reset default";
            bt_reset.UseVisualStyleBackColor = true;
            bt_reset.Visible = false;
            bt_reset.Click += bt_reset_Click;
            // 
            // bt_read
            // 
            bt_read.Location = new Point(6, 92);
            bt_read.Name = "bt_read";
            bt_read.Size = new Size(133, 58);
            bt_read.TabIndex = 1;
            bt_read.Text = "Read from chip";
            bt_read.UseVisualStyleBackColor = true;
            bt_read.Click += bt_read_Click;
            // 
            // tab_Main
            // 
            tab_Main.Controls.Add(tab_RSSI1);
            tab_Main.Controls.Add(tab_RSSI2);
            tab_Main.Controls.Add(tab_PLL);
            tab_Main.Dock = DockStyle.Fill;
            tab_Main.Location = new Point(0, 0);
            tab_Main.Name = "tab_Main";
            tab_Main.SelectedIndex = 0;
            tab_Main.Size = new Size(1663, 691);
            tab_Main.TabIndex = 2;
            tab_Main.SelectedIndexChanged += tab_Main_SelectedIndexChanged;
            // 
            // tab_RSSI1
            // 
            tab_RSSI1.Controls.Add(dtgrid_rssi1);
            tab_RSSI1.Location = new Point(4, 29);
            tab_RSSI1.Name = "tab_RSSI1";
            tab_RSSI1.Padding = new Padding(3);
            tab_RSSI1.Size = new Size(1655, 658);
            tab_RSSI1.TabIndex = 0;
            tab_RSSI1.Text = "RSSI 1";
            tab_RSSI1.UseVisualStyleBackColor = true;
            // 
            // dtgrid_rssi1
            // 
            dtgrid_rssi1.AllowUserToAddRows = false;
            dtgrid_rssi1.AllowUserToDeleteRows = false;
            dtgrid_rssi1.BackgroundColor = SystemColors.Control;
            dtgrid_rssi1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgrid_rssi1.Dock = DockStyle.Fill;
            dtgrid_rssi1.Location = new Point(3, 3);
            dtgrid_rssi1.Name = "dtgrid_rssi1";
            dtgrid_rssi1.RowHeadersWidth = 51;
            dtgrid_rssi1.Size = new Size(1649, 652);
            dtgrid_rssi1.TabIndex = 0;
            dtgrid_rssi1.CellDoubleClick += CellDoubleClick;
            dtgrid_rssi1.CellEndEdit += dgv_CellEndEdit;
            dtgrid_rssi1.CellMouseUp += CellMouseUp;
            dtgrid_rssi1.DataError += dgv_DataError;
            dtgrid_rssi1.EditingControlShowing += dgv_EditingControlShowing;
            // 
            // tab_RSSI2
            // 
            tab_RSSI2.Controls.Add(dtgrid_rssi2);
            tab_RSSI2.Location = new Point(4, 29);
            tab_RSSI2.Name = "tab_RSSI2";
            tab_RSSI2.Padding = new Padding(3);
            tab_RSSI2.Size = new Size(1655, 658);
            tab_RSSI2.TabIndex = 1;
            tab_RSSI2.Text = "RSSI 2";
            tab_RSSI2.UseVisualStyleBackColor = true;
            // 
            // dtgrid_rssi2
            // 
            dtgrid_rssi2.BackgroundColor = SystemColors.Control;
            dtgrid_rssi2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgrid_rssi2.Dock = DockStyle.Fill;
            dtgrid_rssi2.Location = new Point(3, 3);
            dtgrid_rssi2.Name = "dtgrid_rssi2";
            dtgrid_rssi2.RowHeadersWidth = 51;
            dtgrid_rssi2.Size = new Size(1649, 652);
            dtgrid_rssi2.TabIndex = 3;
            dtgrid_rssi2.CellDoubleClick += CellDoubleClick;
            dtgrid_rssi2.CellEndEdit += dgv_CellEndEdit;
            dtgrid_rssi2.CellMouseUp += CellMouseUp;
            dtgrid_rssi2.DataError += dgv_DataError;
            dtgrid_rssi2.EditingControlShowing += dgv_EditingControlShowing;
            // 
            // tab_PLL
            // 
            tab_PLL.Controls.Add(dtgrid_pll);
            tab_PLL.Location = new Point(4, 29);
            tab_PLL.Name = "tab_PLL";
            tab_PLL.Padding = new Padding(3);
            tab_PLL.Size = new Size(1655, 658);
            tab_PLL.TabIndex = 2;
            tab_PLL.Text = "PLL";
            tab_PLL.UseVisualStyleBackColor = true;
            // 
            // dtgrid_pll
            // 
            dtgrid_pll.BackgroundColor = SystemColors.Control;
            dtgrid_pll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgrid_pll.Dock = DockStyle.Fill;
            dtgrid_pll.Location = new Point(3, 3);
            dtgrid_pll.Name = "dtgrid_pll";
            dtgrid_pll.RowHeadersWidth = 51;
            dtgrid_pll.Size = new Size(1649, 652);
            dtgrid_pll.TabIndex = 1;
            dtgrid_pll.CellDoubleClick += CellDoubleClick;
            dtgrid_pll.CellEndEdit += dgv_CellEndEdit;
            dtgrid_pll.CellMouseUp += CellMouseUp;
            dtgrid_pll.DataError += dgv_DataError;
            dtgrid_pll.EditingControlShowing += dgv_EditingControlShowing;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(bt_connect);
            groupBox1.Controls.Add(bt_setting);
            groupBox1.Controls.Add(cbBaudRate);
            groupBox1.Controls.Add(cbCom);
            groupBox1.Location = new Point(6, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(274, 159);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Connection";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(118, 23);
            label3.Name = "label3";
            label3.Size = new Size(36, 20);
            label3.TabIndex = 3;
            label3.Text = "8N1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 33);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 3;
            label1.Text = "COM";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 86);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 3;
            label2.Text = "Baud rate";
            // 
            // bt_setting
            // 
            bt_setting.Location = new Point(126, 101);
            bt_setting.Name = "bt_setting";
            bt_setting.Size = new Size(133, 40);
            bt_setting.TabIndex = 1;
            bt_setting.Text = "Setting";
            bt_setting.UseVisualStyleBackColor = true;
            bt_setting.Click += bt_setting_Click;
            // 
            // cbBaudRate
            // 
            cbBaudRate.FormattingEnabled = true;
            cbBaudRate.Location = new Point(26, 108);
            cbBaudRate.Name = "cbBaudRate";
            cbBaudRate.Size = new Size(94, 28);
            cbBaudRate.TabIndex = 2;
            // 
            // cbCom
            // 
            cbCom.FormattingEnabled = true;
            cbCom.Location = new Point(26, 56);
            cbCom.Name = "cbCom";
            cbCom.Size = new Size(94, 28);
            cbCom.TabIndex = 2;
            // 
            // UpdateTimer
            // 
            UpdateTimer.Enabled = true;
            UpdateTimer.Tick += UpdateTimer_Tick;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tab_Main);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Panel2MinSize = 100;
            splitContainer1.Size = new Size(1663, 905);
            splitContainer1.SplitterDistance = 691;
            splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(2);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(groupBox1);
            splitContainer2.Panel1.Controls.Add(groupBox3);
            splitContainer2.Panel1.Controls.Add(groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tbMsg);
            splitContainer2.Size = new Size(1663, 210);
            splitContainer2.SplitterDistance = 813;
            splitContainer2.SplitterWidth = 3;
            splitContainer2.TabIndex = 6;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox3.Controls.Add(button1);
            groupBox3.Controls.Add(button2);
            groupBox3.Location = new Point(628, 27);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(161, 159);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Data";
            // 
            // button1
            // 
            button1.Location = new Point(6, 26);
            button1.Name = "button1";
            button1.Size = new Size(133, 40);
            button1.TabIndex = 1;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += bt_reset_Click;
            // 
            // button2
            // 
            button2.Location = new Point(6, 92);
            button2.Name = "button2";
            button2.Size = new Size(133, 58);
            button2.TabIndex = 1;
            button2.Text = "Load";
            button2.UseVisualStyleBackColor = true;
            button2.Click += bt_read_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox2.Controls.Add(bt_reset);
            groupBox2.Controls.Add(bt_read);
            groupBox2.Controls.Add(bt_write);
            groupBox2.Controls.Add(bt_verify);
            groupBox2.Location = new Point(314, 27);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(286, 159);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Control";
            // 
            // bt_verify
            // 
            bt_verify.Location = new Point(146, 26);
            bt_verify.Name = "bt_verify";
            bt_verify.Size = new Size(133, 40);
            bt_verify.TabIndex = 1;
            bt_verify.Text = "Verify";
            bt_verify.UseVisualStyleBackColor = true;
            bt_verify.Visible = false;
            bt_verify.Click += bt_verify_Click;
            // 
            // tbMsg
            // 
            tbMsg.Dock = DockStyle.Fill;
            tbMsg.Location = new Point(0, 0);
            tbMsg.Margin = new Padding(2);
            tbMsg.Multiline = true;
            tbMsg.Name = "tbMsg";
            tbMsg.ScrollBars = ScrollBars.Vertical;
            tbMsg.Size = new Size(847, 210);
            tbMsg.TabIndex = 5;
            // 
            // registerItemBindingSource
            // 
            registerItemBindingSource.DataSource = typeof(RegisterItem);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1663, 905);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            tab_Main.ResumeLayout(false);
            tab_RSSI1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgrid_rssi1).EndInit();
            tab_RSSI2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgrid_rssi2).EndInit();
            tab_PLL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgrid_pll).EndInit();
            ((System.ComponentModel.ISupportInitialize)sPISlaveBindingSource).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)registerItemBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button bt_write;
        private Button bt_connect;
        private Button bt_reset;
        private Button bt_read;
        private TabControl tab_Main;
        private System.Windows.Forms.Timer UpdateTimer;
        private SplitContainer splitContainer1;
        private DataGridView dtgrid_pll;
        private DataGridView dtgrid_rssi1;
        private DataGridView dtgrid_rssi2;
        public TabPage tab_RSSI1;
        public TabPage tab_RSSI2;
        public TabPage tab_PLL;
        private Label label2;
        private Label label1;
        private ComboBox cbBaudRate;
        private ComboBox cbCom;
        private Button bt_setting;
        private Button bt_verify;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private BindingSource sPISlaveBindingSource;
        private BindingSource registerItemBindingSource;
        private TextBox tbMsg;
        private SplitContainer splitContainer2;
        private Label label3;
        private GroupBox groupBox3;
        private Button button1;
        private Button button2;
    }
}
