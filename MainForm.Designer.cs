﻿namespace UserForm
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
            groupBox1 = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            bt_setting = new Button();
            cbBaudRate = new ComboBox();
            cbCom = new ComboBox();
            UpdateTimer = new System.Windows.Forms.Timer(components);
            splitContainer1 = new SplitContainer();
            groupBox2 = new GroupBox();
            bt_verify = new Button();
            tab_Main.SuspendLayout();
            tab_RSSI1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgrid_rssi1).BeginInit();
            tab_RSSI2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgrid_rssi2).BeginInit();
            tab_PLL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgrid_pll).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // bt_write
            // 
            bt_write.Location = new Point(6, 156);
            bt_write.Name = "bt_write";
            bt_write.Size = new Size(94, 58);
            bt_write.TabIndex = 1;
            bt_write.Text = "Write to chip";
            bt_write.UseVisualStyleBackColor = true;
            bt_write.Click += bt_write_Click;
            // 
            // bt_connect
            // 
            bt_connect.Location = new Point(6, 144);
            bt_connect.Name = "bt_connect";
            bt_connect.Size = new Size(94, 40);
            bt_connect.TabIndex = 1;
            bt_connect.Text = "Connect";
            bt_connect.UseVisualStyleBackColor = true;
            bt_connect.Click += bt_connect_Click;
            // 
            // bt_reset
            // 
            bt_reset.Location = new Point(6, 26);
            bt_reset.Name = "bt_reset";
            bt_reset.Size = new Size(94, 58);
            bt_reset.TabIndex = 1;
            bt_reset.Text = "Reset default";
            bt_reset.UseVisualStyleBackColor = true;
            bt_reset.Click += bt_reset_Click;
            // 
            // bt_read
            // 
            bt_read.Location = new Point(6, 92);
            bt_read.Name = "bt_read";
            bt_read.Size = new Size(94, 58);
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
            tab_Main.Size = new Size(900, 590);
            tab_Main.TabIndex = 2;
            tab_Main.SelectedIndexChanged += tab_Main_SelectedIndexChanged;
            // 
            // tab_RSSI1
            // 
            tab_RSSI1.Controls.Add(dtgrid_rssi1);
            tab_RSSI1.Location = new Point(4, 29);
            tab_RSSI1.Name = "tab_RSSI1";
            tab_RSSI1.Padding = new Padding(3);
            tab_RSSI1.Size = new Size(892, 557);
            tab_RSSI1.TabIndex = 0;
            tab_RSSI1.Text = "RSSI 1";
            tab_RSSI1.UseVisualStyleBackColor = true;
            // 
            // dtgrid_rssi1
            // 
            dtgrid_rssi1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgrid_rssi1.Dock = DockStyle.Fill;
            dtgrid_rssi1.Location = new Point(3, 3);
            dtgrid_rssi1.Name = "dtgrid_rssi1";
            dtgrid_rssi1.RowHeadersWidth = 51;
            dtgrid_rssi1.Size = new Size(886, 551);
            dtgrid_rssi1.TabIndex = 0;
            // 
            // tab_RSSI2
            // 
            tab_RSSI2.Controls.Add(dtgrid_rssi2);
            tab_RSSI2.Location = new Point(4, 29);
            tab_RSSI2.Name = "tab_RSSI2";
            tab_RSSI2.Padding = new Padding(3);
            tab_RSSI2.Size = new Size(892, 557);
            tab_RSSI2.TabIndex = 1;
            tab_RSSI2.Text = "RSSI 2";
            tab_RSSI2.UseVisualStyleBackColor = true;
            // 
            // dtgrid_rssi2
            // 
            dtgrid_rssi2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgrid_rssi2.Dock = DockStyle.Fill;
            dtgrid_rssi2.Location = new Point(3, 3);
            dtgrid_rssi2.Name = "dtgrid_rssi2";
            dtgrid_rssi2.RowHeadersWidth = 51;
            dtgrid_rssi2.Size = new Size(886, 551);
            dtgrid_rssi2.TabIndex = 3;
            // 
            // tab_PLL
            // 
            tab_PLL.Controls.Add(dtgrid_pll);
            tab_PLL.Location = new Point(4, 29);
            tab_PLL.Name = "tab_PLL";
            tab_PLL.Padding = new Padding(3);
            tab_PLL.Size = new Size(892, 557);
            tab_PLL.TabIndex = 2;
            tab_PLL.Text = "PLL";
            tab_PLL.UseVisualStyleBackColor = true;
            // 
            // dtgrid_pll
            // 
            dtgrid_pll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgrid_pll.Dock = DockStyle.Fill;
            dtgrid_pll.Location = new Point(3, 3);
            dtgrid_pll.Name = "dtgrid_pll";
            dtgrid_pll.RowHeadersWidth = 51;
            dtgrid_pll.Size = new Size(886, 551);
            dtgrid_pll.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(bt_connect);
            groupBox1.Controls.Add(bt_setting);
            groupBox1.Controls.Add(cbBaudRate);
            groupBox1.Controls.Add(cbCom);
            groupBox1.Location = new Point(12, 29);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(146, 243);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Connection";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 23);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 3;
            label1.Text = "COM";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 87);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 3;
            label2.Text = "Baud rate";
            // 
            // bt_setting
            // 
            bt_setting.Location = new Point(6, 190);
            bt_setting.Name = "bt_setting";
            bt_setting.Size = new Size(94, 40);
            bt_setting.TabIndex = 1;
            bt_setting.Text = "Setting";
            bt_setting.UseVisualStyleBackColor = true;
            bt_setting.Click += bt_setting_Click;
            // 
            // cbBaudRate
            // 
            cbBaudRate.FormattingEnabled = true;
            cbBaudRate.Location = new Point(6, 110);
            cbBaudRate.Name = "cbBaudRate";
            cbBaudRate.Size = new Size(94, 28);
            cbBaudRate.TabIndex = 2;
            // 
            // cbCom
            // 
            cbCom.FormattingEnabled = true;
            cbCom.Location = new Point(6, 46);
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
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tab_Main);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Panel2MinSize = 100;
            splitContainer1.Size = new Size(1065, 590);
            splitContainer1.SplitterDistance = 900;
            splitContainer1.TabIndex = 4;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(bt_reset);
            groupBox2.Controls.Add(bt_read);
            groupBox2.Controls.Add(bt_write);
            groupBox2.Controls.Add(bt_verify);
            groupBox2.Location = new Point(12, 292);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(146, 291);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Control";
            // 
            // bt_verify
            // 
            bt_verify.Location = new Point(6, 211);
            bt_verify.Name = "bt_verify";
            bt_verify.Size = new Size(94, 40);
            bt_verify.TabIndex = 1;
            bt_verify.Text = "Verify";
            bt_verify.UseVisualStyleBackColor = true;
            bt_verify.Click += bt_verify_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1065, 590);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            tab_Main.ResumeLayout(false);
            tab_RSSI1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgrid_rssi1).EndInit();
            tab_RSSI2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgrid_rssi2).EndInit();
            tab_PLL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtgrid_pll).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
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
    }
}