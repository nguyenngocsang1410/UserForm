namespace UserForm
{
    partial class ConnectForm
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
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            cbStopBit = new ComboBox();
            cbCheckBit = new ComboBox();
            label5 = new Label();
            cbDataBit = new ComboBox();
            label4 = new Label();
            cbBaudRate = new ComboBox();
            label3 = new Label();
            cbCom = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            btOpenClose = new Button();
            groupBox2 = new GroupBox();
            tbTransmit = new TextBox();
            btSend = new Button();
            groupBox3 = new GroupBox();
            tbReceive = new TextBox();
            btClear = new Button();
            UpdateTimer = new System.Windows.Forms.Timer(components);
            btRefresh = new Button();
            groupBox4 = new GroupBox();
            rbtSendDataHex = new RadioButton();
            rbtSendDataASCII = new RadioButton();
            groupBox5 = new GroupBox();
            rbtReceiveDataHEX = new RadioButton();
            rbtReceiveDataASCII = new RadioButton();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(cbStopBit);
            groupBox1.Controls.Add(cbCheckBit);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cbDataBit);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cbBaudRate);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cbCom);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(295, 427);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Port control";
            // 
            // cbStopBit
            // 
            cbStopBit.FormattingEnabled = true;
            cbStopBit.Location = new Point(120, 173);
            cbStopBit.Name = "cbStopBit";
            cbStopBit.Size = new Size(126, 28);
            cbStopBit.TabIndex = 1;
            // 
            // cbCheckBit
            // 
            cbCheckBit.FormattingEnabled = true;
            cbCheckBit.Location = new Point(120, 139);
            cbCheckBit.Name = "cbCheckBit";
            cbCheckBit.Size = new Size(126, 28);
            cbCheckBit.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(50, 181);
            label5.Name = "label5";
            label5.Size = new Size(62, 20);
            label5.TabIndex = 1;
            label5.Text = "Stop bit";
            // 
            // cbDataBit
            // 
            cbDataBit.FormattingEnabled = true;
            cbDataBit.Location = new Point(120, 105);
            cbDataBit.Name = "cbDataBit";
            cbDataBit.Size = new Size(126, 28);
            cbDataBit.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(50, 147);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 1;
            label4.Text = "Check bit";
            // 
            // cbBaudRate
            // 
            cbBaudRate.FormattingEnabled = true;
            cbBaudRate.Location = new Point(120, 71);
            cbBaudRate.Name = "cbBaudRate";
            cbBaudRate.Size = new Size(126, 28);
            cbBaudRate.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(50, 113);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 1;
            label3.Text = "Data bit";
            // 
            // cbCom
            // 
            cbCom.FormattingEnabled = true;
            cbCom.Location = new Point(120, 26);
            cbCom.Name = "cbCom";
            cbCom.Size = new Size(126, 28);
            cbCom.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 79);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 1;
            label2.Text = "Baudrate";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 34);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 1;
            label1.Text = "COM";
            // 
            // btOpenClose
            // 
            btOpenClose.Location = new Point(704, 24);
            btOpenClose.Name = "btOpenClose";
            btOpenClose.Size = new Size(84, 30);
            btOpenClose.TabIndex = 1;
            btOpenClose.Text = "Open";
            btOpenClose.UseVisualStyleBackColor = true;
            btOpenClose.Click += btOpenClose_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(tbTransmit);
            groupBox2.Controls.Add(btSend);
            groupBox2.Location = new Point(316, 459);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(501, 131);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Transmit";
            // 
            // tbTransmit
            // 
            tbTransmit.Dock = DockStyle.Top;
            tbTransmit.Location = new Point(3, 23);
            tbTransmit.Multiline = true;
            tbTransmit.Name = "tbTransmit";
            tbTransmit.Size = new Size(495, 66);
            tbTransmit.TabIndex = 0;
            // 
            // btSend
            // 
            btSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btSend.Location = new Point(382, 95);
            btSend.Name = "btSend";
            btSend.Size = new Size(84, 30);
            btSend.TabIndex = 1;
            btSend.Text = "Send";
            btSend.UseVisualStyleBackColor = true;
            btSend.Click += btSend_Click;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(tbReceive);
            groupBox3.Controls.Add(btClear);
            groupBox3.Location = new Point(316, 60);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(501, 360);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Receive";
            // 
            // tbReceive
            // 
            tbReceive.Dock = DockStyle.Top;
            tbReceive.Location = new Point(3, 23);
            tbReceive.Multiline = true;
            tbReceive.Name = "tbReceive";
            tbReceive.Size = new Size(495, 295);
            tbReceive.TabIndex = 0;
            // 
            // btClear
            // 
            btClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btClear.Location = new Point(411, 324);
            btClear.Name = "btClear";
            btClear.Size = new Size(84, 30);
            btClear.TabIndex = 1;
            btClear.Text = "Clear";
            btClear.UseVisualStyleBackColor = true;
            btClear.Click += btClear_Click;
            // 
            // UpdateTimer
            // 
            UpdateTimer.Enabled = true;
            UpdateTimer.Tick += UpdateTimer_Tick;
            // 
            // btRefresh
            // 
            btRefresh.Location = new Point(614, 24);
            btRefresh.Name = "btRefresh";
            btRefresh.Size = new Size(84, 30);
            btRefresh.TabIndex = 1;
            btRefresh.Text = "Refresh";
            btRefresh.UseVisualStyleBackColor = true;
            btRefresh.Click += btRefresh_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(rbtSendDataHex);
            groupBox4.Controls.Add(rbtSendDataASCII);
            groupBox4.Location = new Point(12, 293);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(295, 223);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "Send format";
            // 
            // rbtSendDataHex
            // 
            rbtSendDataHex.AutoSize = true;
            rbtSendDataHex.Location = new Point(172, 26);
            rbtSendDataHex.Name = "rbtSendDataHex";
            rbtSendDataHex.Size = new Size(58, 24);
            rbtSendDataHex.TabIndex = 2;
            rbtSendDataHex.TabStop = true;
            rbtSendDataHex.Text = "HEX";
            rbtSendDataHex.UseVisualStyleBackColor = true;
            // 
            // rbtSendDataASCII
            // 
            rbtSendDataASCII.AutoSize = true;
            rbtSendDataASCII.Location = new Point(27, 26);
            rbtSendDataASCII.Name = "rbtSendDataASCII";
            rbtSendDataASCII.Size = new Size(65, 24);
            rbtSendDataASCII.TabIndex = 2;
            rbtSendDataASCII.TabStop = true;
            rbtSendDataASCII.Text = "ASCII";
            rbtSendDataASCII.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(rbtReceiveDataHEX);
            groupBox5.Controls.Add(rbtReceiveDataASCII);
            groupBox5.Location = new Point(12, 370);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(295, 223);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "Receive format";
            // 
            // rbtReceiveDataHEX
            // 
            rbtReceiveDataHEX.AutoSize = true;
            rbtReceiveDataHEX.Location = new Point(172, 39);
            rbtReceiveDataHEX.Name = "rbtReceiveDataHEX";
            rbtReceiveDataHEX.Size = new Size(58, 24);
            rbtReceiveDataHEX.TabIndex = 2;
            rbtReceiveDataHEX.TabStop = true;
            rbtReceiveDataHEX.Text = "HEX";
            rbtReceiveDataHEX.UseVisualStyleBackColor = true;
            // 
            // rbtReceiveDataASCII
            // 
            rbtReceiveDataASCII.AutoSize = true;
            rbtReceiveDataASCII.Location = new Point(27, 39);
            rbtReceiveDataASCII.Name = "rbtReceiveDataASCII";
            rbtReceiveDataASCII.Size = new Size(65, 24);
            rbtReceiveDataASCII.TabIndex = 2;
            rbtReceiveDataASCII.TabStop = true;
            rbtReceiveDataASCII.Text = "ASCII";
            rbtReceiveDataASCII.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(829, 602);
            Controls.Add(groupBox3);
            Controls.Add(btRefresh);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox1);
            Controls.Add(btOpenClose);
            Controls.Add(groupBox2);
            Name = "Form1";
            Text = "Form1";
            FormClosing += ConnectForm_FormClosing;
            Load += ConnectForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox cbCom;
        private Button btClose;
        private Button btOpenClose;
        private Label label1;
        private GroupBox groupBox2;
        private Button btSend;
        private TextBox tbTransmit;
        private GroupBox groupBox3;
        private TextBox tbReceive;
        private Button btClear;
        private System.Windows.Forms.Timer UpdateTimer;
        private Button btRefresh;
        private GroupBox groupBox4;
        private RadioButton rbtSendDataHex;
        private RadioButton rbtSendDataASCII;
        private GroupBox groupBox5;
        private ComboBox cbStopBit;
        private ComboBox cbCheckBit;
        private Label label5;
        private ComboBox cbDataBit;
        private Label label4;
        private ComboBox cbBaudRate;
        private Label label3;
        private Label label2;
        private RadioButton rbtReceiveDataHEX;
        private RadioButton rbtReceiveDataASCII;
    }
}