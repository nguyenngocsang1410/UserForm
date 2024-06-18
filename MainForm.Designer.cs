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
            tc_main = new TabControl();
            tp_rssi1 = new TabPage();
            dgv_rssi1 = new DataGridView();
            tp_rssi2 = new TabPage();
            dgv_rssi2 = new DataGridView();
            tp_pll = new TabPage();
            dgv_pll = new DataGridView();
            sPISlaveBindingSource = new BindingSource(components);
            groupBox1 = new GroupBox();
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            bt_refresh = new Button();
            bt_setting = new Button();
            cb_baudRate = new ComboBox();
            cb_comPort = new ComboBox();
            UpdateTimer = new System.Windows.Forms.Timer(components);
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            groupBox3 = new GroupBox();
            button1 = new Button();
            button2 = new Button();
            groupBox2 = new GroupBox();
            bt_verify = new Button();
            tb_msg = new TextBox();
            registerItemBindingSource = new BindingSource(components);
            tc_main.SuspendLayout();
            tp_rssi1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_rssi1).BeginInit();
            tp_rssi2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_rssi2).BeginInit();
            tp_pll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_pll).BeginInit();
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
            bt_write.Location = new Point(182, 115);
            bt_write.Margin = new Padding(4);
            bt_write.Name = "bt_write";
            bt_write.Size = new Size(166, 72);
            bt_write.TabIndex = 1;
            bt_write.Text = "Write to chip";
            bt_write.UseVisualStyleBackColor = true;
            bt_write.Click += bt_write_Click;
            // 
            // bt_connect
            // 
            bt_connect.Location = new Point(158, 68);
            bt_connect.Margin = new Padding(4);
            bt_connect.Name = "bt_connect";
            bt_connect.Size = new Size(166, 50);
            bt_connect.TabIndex = 1;
            bt_connect.Text = "Connect";
            bt_connect.UseVisualStyleBackColor = true;
            bt_connect.Click += bt_connect_Click;
            // 
            // bt_reset
            // 
            bt_reset.Location = new Point(8, 32);
            bt_reset.Margin = new Padding(4);
            bt_reset.Name = "bt_reset";
            bt_reset.Size = new Size(166, 50);
            bt_reset.TabIndex = 1;
            bt_reset.Text = "Reset default";
            bt_reset.UseVisualStyleBackColor = true;
            bt_reset.Visible = false;
            bt_reset.Click += bt_reset_Click;
            // 
            // bt_read
            // 
            bt_read.Location = new Point(8, 115);
            bt_read.Margin = new Padding(4);
            bt_read.Name = "bt_read";
            bt_read.Size = new Size(166, 72);
            bt_read.TabIndex = 1;
            bt_read.Text = "Read from chip";
            bt_read.UseVisualStyleBackColor = true;
            bt_read.Click += bt_read_Click;
            // 
            // tc_main
            // 
            tc_main.Controls.Add(tp_rssi1);
            tc_main.Controls.Add(tp_rssi2);
            tc_main.Controls.Add(tp_pll);
            tc_main.Dock = DockStyle.Fill;
            tc_main.Location = new Point(0, 0);
            tc_main.Margin = new Padding(4);
            tc_main.Name = "tc_main";
            tc_main.SelectedIndex = 0;
            tc_main.Size = new Size(1924, 779);
            tc_main.TabIndex = 2;
            tc_main.Tag = "";
            tc_main.SelectedIndexChanged += tab_Main_SelectedIndexChanged;
            // 
            // tp_rssi1
            // 
            tp_rssi1.Controls.Add(dgv_rssi1);
            tp_rssi1.Location = new Point(4, 34);
            tp_rssi1.Margin = new Padding(4);
            tp_rssi1.Name = "tp_rssi1";
            tp_rssi1.Padding = new Padding(4);
            tp_rssi1.Size = new Size(1916, 741);
            tp_rssi1.TabIndex = 0;
            tp_rssi1.Text = "RSSI 1";
            tp_rssi1.UseVisualStyleBackColor = true;
            // 
            // dgv_rssi1
            // 
            dgv_rssi1.AllowUserToAddRows = false;
            dgv_rssi1.AllowUserToDeleteRows = false;
            dgv_rssi1.BackgroundColor = SystemColors.Control;
            dgv_rssi1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_rssi1.Dock = DockStyle.Fill;
            dgv_rssi1.Location = new Point(4, 4);
            dgv_rssi1.Margin = new Padding(4);
            dgv_rssi1.Name = "dgv_rssi1";
            dgv_rssi1.RowHeadersWidth = 51;
            dgv_rssi1.Size = new Size(1908, 733);
            dgv_rssi1.TabIndex = 0;
            dgv_rssi1.CellDoubleClick += CellDoubleClick;
            dgv_rssi1.CellEndEdit += dgv_CellEndEdit;
            dgv_rssi1.CellMouseUp += CellMouseUp;
            dgv_rssi1.DataError += dgv_DataError;
            dgv_rssi1.EditingControlShowing += dgv_EditingControlShowing;
            // 
            // tp_rssi2
            // 
            tp_rssi2.Controls.Add(dgv_rssi2);
            tp_rssi2.Location = new Point(4, 34);
            tp_rssi2.Margin = new Padding(4);
            tp_rssi2.Name = "tp_rssi2";
            tp_rssi2.Padding = new Padding(4);
            tp_rssi2.Size = new Size(1916, 742);
            tp_rssi2.TabIndex = 1;
            tp_rssi2.Text = "RSSI 2";
            tp_rssi2.UseVisualStyleBackColor = true;
            // 
            // dgv_rssi2
            // 
            dgv_rssi2.BackgroundColor = SystemColors.Control;
            dgv_rssi2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_rssi2.Dock = DockStyle.Fill;
            dgv_rssi2.Location = new Point(4, 4);
            dgv_rssi2.Margin = new Padding(4);
            dgv_rssi2.Name = "dgv_rssi2";
            dgv_rssi2.RowHeadersWidth = 51;
            dgv_rssi2.Size = new Size(1908, 734);
            dgv_rssi2.TabIndex = 3;
            dgv_rssi2.CellDoubleClick += CellDoubleClick;
            dgv_rssi2.CellEndEdit += dgv_CellEndEdit;
            dgv_rssi2.CellMouseUp += CellMouseUp;
            dgv_rssi2.DataError += dgv_DataError;
            dgv_rssi2.EditingControlShowing += dgv_EditingControlShowing;
            // 
            // tp_pll
            // 
            tp_pll.Controls.Add(dgv_pll);
            tp_pll.Location = new Point(4, 34);
            tp_pll.Margin = new Padding(4);
            tp_pll.Name = "tp_pll";
            tp_pll.Padding = new Padding(4);
            tp_pll.Size = new Size(1916, 741);
            tp_pll.TabIndex = 2;
            tp_pll.Text = "PLL";
            tp_pll.UseVisualStyleBackColor = true;
            // 
            // dgv_pll
            // 
            dgv_pll.BackgroundColor = SystemColors.Control;
            dgv_pll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_pll.Dock = DockStyle.Fill;
            dgv_pll.Location = new Point(4, 4);
            dgv_pll.Margin = new Padding(4);
            dgv_pll.Name = "dgv_pll";
            dgv_pll.RowHeadersWidth = 51;
            dgv_pll.Size = new Size(1908, 733);
            dgv_pll.TabIndex = 1;
            dgv_pll.CellDoubleClick += CellDoubleClick;
            dgv_pll.CellEndEdit += dgv_CellEndEdit;
            dgv_pll.CellMouseUp += CellMouseUp;
            dgv_pll.DataError += dgv_DataError;
            dgv_pll.EditingControlShowing += dgv_EditingControlShowing;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(bt_refresh);
            groupBox1.Controls.Add(bt_connect);
            groupBox1.Controls.Add(bt_setting);
            groupBox1.Controls.Add(cb_baudRate);
            groupBox1.Controls.Add(cb_comPort);
            groupBox1.Location = new Point(8, 38);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(342, 199);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Connection";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 41);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(53, 25);
            label1.TabIndex = 3;
            label1.Text = "COM";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(113, 29);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(45, 25);
            label3.TabIndex = 3;
            label3.Text = "8N1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 108);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(87, 25);
            label2.TabIndex = 3;
            label2.Text = "Baud rate";
            // 
            // bt_refresh
            // 
            bt_refresh.Location = new Point(158, 16);
            bt_refresh.Margin = new Padding(4);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new Size(166, 50);
            bt_refresh.TabIndex = 1;
            bt_refresh.Text = "Refresh";
            bt_refresh.UseVisualStyleBackColor = true;
            bt_refresh.Click += bt_connect_Click;
            // 
            // bt_setting
            // 
            bt_setting.Location = new Point(158, 126);
            bt_setting.Margin = new Padding(4);
            bt_setting.Name = "bt_setting";
            bt_setting.Size = new Size(166, 50);
            bt_setting.TabIndex = 1;
            bt_setting.Text = "Setting";
            bt_setting.UseVisualStyleBackColor = true;
            bt_setting.Click += bt_setting_Click;
            // 
            // cb_baudRate
            // 
            cb_baudRate.FormattingEnabled = true;
            cb_baudRate.Location = new Point(32, 135);
            cb_baudRate.Margin = new Padding(4);
            cb_baudRate.Name = "cb_baudRate";
            cb_baudRate.Size = new Size(116, 33);
            cb_baudRate.TabIndex = 2;
            // 
            // cb_comPort
            // 
            cb_comPort.FormattingEnabled = true;
            cb_comPort.Location = new Point(32, 70);
            cb_comPort.Margin = new Padding(4);
            cb_comPort.Name = "cb_comPort";
            cb_comPort.Size = new Size(116, 33);
            cb_comPort.TabIndex = 2;
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
            splitContainer1.Margin = new Padding(4);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tc_main);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Panel2MinSize = 100;
            splitContainer1.Size = new Size(1924, 1050);
            splitContainer1.SplitterDistance = 779;
            splitContainer1.SplitterWidth = 5;
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
            splitContainer2.Panel2.Controls.Add(tb_msg);
            splitContainer2.Size = new Size(1924, 266);
            splitContainer2.SplitterDistance = 1016;
            splitContainer2.TabIndex = 6;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox3.Controls.Add(button1);
            groupBox3.Controls.Add(button2);
            groupBox3.Location = new Point(785, 38);
            groupBox3.Margin = new Padding(4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4);
            groupBox3.Size = new Size(201, 199);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Data";
            // 
            // button1
            // 
            button1.Location = new Point(8, 32);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(166, 50);
            button1.TabIndex = 1;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += bt_reset_Click;
            // 
            // button2
            // 
            button2.Location = new Point(8, 115);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(166, 72);
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
            groupBox2.Location = new Point(392, 38);
            groupBox2.Margin = new Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4);
            groupBox2.Size = new Size(358, 199);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Control";
            // 
            // bt_verify
            // 
            bt_verify.Location = new Point(182, 32);
            bt_verify.Margin = new Padding(4);
            bt_verify.Name = "bt_verify";
            bt_verify.Size = new Size(166, 50);
            bt_verify.TabIndex = 1;
            bt_verify.Text = "Verify";
            bt_verify.UseVisualStyleBackColor = true;
            bt_verify.Visible = false;
            bt_verify.Click += bt_verify_Click;
            // 
            // tb_msg
            // 
            tb_msg.Dock = DockStyle.Fill;
            tb_msg.Location = new Point(0, 0);
            tb_msg.Margin = new Padding(2);
            tb_msg.Multiline = true;
            tb_msg.Name = "tb_msg";
            tb_msg.ScrollBars = ScrollBars.Vertical;
            tb_msg.Size = new Size(904, 266);
            tb_msg.TabIndex = 5;
            // 
            // registerItemBindingSource
            // 
            registerItemBindingSource.DataSource = typeof(RegisterItem);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1050);
            Controls.Add(splitContainer1);
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            tc_main.ResumeLayout(false);
            tp_rssi1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_rssi1).EndInit();
            tp_rssi2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_rssi2).EndInit();
            tp_pll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_pll).EndInit();
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
        private TabControl tc_main;
        private System.Windows.Forms.Timer UpdateTimer;
        private SplitContainer splitContainer1;
        private DataGridView dgv_pll;
        private DataGridView dgv_rssi1;
        private DataGridView dgv_rssi2;
        public TabPage tp_rssi1;
        public TabPage tp_rssi2;
        public TabPage tp_pll;
        private Label label2;
        private Label label1;
        private ComboBox cb_baudRate;
        private ComboBox cb_comPort;
        private Button bt_setting;
        private Button bt_verify;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private BindingSource sPISlaveBindingSource;
        private BindingSource registerItemBindingSource;
        private TextBox tb_msg;
        private SplitContainer splitContainer2;
        private Label label3;
        private GroupBox groupBox3;
        private Button button1;
        private Button button2;
        private Button bt_refresh;
    }
}
