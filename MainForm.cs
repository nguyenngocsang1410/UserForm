using System.IO.Ports;
namespace UserForm
{
    public partial class MainForm : Form
    {
        public List<SpiSlave> dbSlaves = [];
        public List<SpiSlave> shadowSlaves = [];
        public bool[] dbInitialized = [false, false, false];
        public Serial serial = new("Serial");

        // TODO: Clean main file
        public MainForm()
        {
            // Close serial port if is open
            if (serial.IsOpen)
            {
                serial.Close();
            }

            CreateDB();

            InitializeComponent();
        }

        #region Main Activity
        private void MainForm_Load(object sender, EventArgs e)
        {

            SetUpBitGrid(0);
            //SetUpBitGrid(1);
            //SetUpBitGrid(2);

            // Port comboBox
            string[] ports = SerialPort.GetPortNames();

            if (ports.Length > 0)
            {
                foreach (string port in ports)
                {
                    cb_comPort.Items.Add(port);
                }

                cb_comPort.SelectedIndex = cb_comPort.Items.IndexOf(serial.PortName);

                if (cb_comPort.SelectedIndex < 0)
                    cb_comPort.SelectedIndex = 0;
            }

            // Baud rate comboBox
            string[] baudRate = ["9600", "19200", "38400", "57600", "115200"];
            foreach (string s in baudRate)
                cb_baudRate.Items.Add(s);

            if (serial.BaudRate != 0)
                cb_baudRate.SelectedIndex = cb_baudRate.Items.IndexOf(serial.BaudRate);

            if (cb_baudRate.SelectedIndex < 0)
                cb_baudRate.SelectedIndex = 0;

            // Disable control group
            tc_main.Enabled = false;
            groupBox2.Enabled = false;

            CheckForIllegalCrossThreadCalls = false;
            serial.DataReceived += new SerialDataReceivedEventHandler(GetResponse);

            WindowState = FormWindowState.Maximized;
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }
        private void SetUpBitGrid(int gridIdx)
        {
            if (dbInitialized[gridIdx]) return;

            if (gridIdx >= dbSlaves.Count) return;

            SpiSlave slave = dbSlaves[gridIdx];

            shadowSlaves[gridIdx].Registers = slave.CopyRegisterList();

            const int rssiColWidth = 170;
            const int pllColWidth = 170;

            DataGridView? dgv = null;

            // Get dgv instance
            int colWidth = 0;
            switch (slave.Name)
            {
                case "RX1_RSSI":
                    dgv = dgv_rssi1;
                    colWidth = rssiColWidth;
                    break;
                case "RX2_RSSI":
                    dgv = dgv_rssi2;
                    colWidth = rssiColWidth;
                    break;
                case "PLL":
                    dgv = dgv_pll;
                    colWidth = pllColWidth;
                    break;
                default:
                    break;
            }
            if (dgv == null)
                return;

            dgv.DataSource = slave.Registers;
            dgv.Tag = slave.Name;

            // Autosize current cols
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.Width = col.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, false);
            }

            // Add bit columns
            int n_bit_cols = slave.Registers[0].BitName.Length;
            for (int i = 0; i < n_bit_cols; i++)
            {
                CustomCheckBoxColumn col = new()
                {
                    Width = colWidth,
                    Label = "b" + (n_bit_cols - 1 - i).ToString(),
                };
                col.Name = col.Label;

                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.HeaderText = col.Label;

                dgv.Columns.Add(col);
            }

            // Sync bit array with value
            for (int rowIdx = 0; rowIdx < dgv.Rows.Count; rowIdx++)
            {
                DataGridViewRow row = dgv.Rows[rowIdx];

                for (int cellIdx = 3; cellIdx < row.Cells.Count; cellIdx++)
                {
                    DataGridViewCell cell = row.Cells[cellIdx];

                    if (cell is CustomCheckBoxCell custom_cell)
                    {
                        custom_cell.Label = slave.Registers[rowIdx].BitName[cellIdx - 3];
                        custom_cell.Value = slave.Registers[rowIdx].BitValue[cellIdx - 3];
                    }
                }
            }

            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[1].ReadOnly = true;

            dgv.Refresh();

            dbInitialized[gridIdx] = true;
        }
        private void UpdateDataGridView()
        {
            // Find DataGridView
            DataGridView? dgv = FindDgv();

            if (dgv != null)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    // Find matching register
                    int idx = dgv.Rows.IndexOf(row);

                    RegisterItem reg = dbSlaves[tc_main.SelectedIndex].Registers[idx];
                    RegisterItem reg_shadow = shadowSlaves[tc_main.SelectedIndex].Registers[idx];

                    //reg.Value = Utils.ConvertBoolArrayToByte(reg.BitValue);
                    //reg_shadow.Value = Utils.ConvertBoolArrayToByte(reg_shadow.BitValue);

                    for (int i = 0; i < reg.BitValue.Length; i++)
                    {
                        CustomCheckBoxCell cell = (CustomCheckBoxCell)row.Cells[i + 3];

                        cell.Value = reg.BitValue[i];

                        if ((bool)cell.Value != reg_shadow.BitValue[i])
                        {
                            // HIGHLIGHT THIS!!!!!
                            cell.Style.BackColor = Color.LightYellow;
                        }
                        else cell.Style.BackColor = Color.White;
                    }
                }
            }
        }
        public void CreateDB()
        {
            dbSlaves = [new SpiSlave("RX1_RSSI"), new SpiSlave("RX2_RSSI"), new SpiSlave("PLL")];

            foreach (SpiSlave slave in dbSlaves)
            {
                shadowSlaves.Add(new SpiSlave(slave.Name));
            }
        }
        #endregion

        #region Action Listener
        // Connection group
        private void bt_setting_Click(object sender, EventArgs e)
        {
            //ConnectForm connectForm = new(serial, () =>
            //    {
            //        string[] ports = SerialPort.GetPortNames();
            //        foreach (string port in ports)
            //        {
            //            cb_comPort.Items.Add(port);
            //        }

            //        if (ports != null)
            //        {
            //            cb_comPort.SelectedIndex = 0;
            //            if (serial.PortName != null)
            //                foreach (string item in cb_comPort.Items)
            //                    if (item == serial.PortName)
            //                        cb_comPort.SelectedIndex = cb_comPort.Items.IndexOf(item);
            //        }

            //        string[] baudRate = ["9600", "19200", "38400", "57600", "115200"];
            //        foreach (string s in baudRate)
            //        {
            //            cb_baudRate.Items.Add(s);
            //        }
            //        cb_baudRate.SelectedIndex = 0;
            //        if (serial.BaudRate != 0)
            //            for (int i = 0; i < baudRate.Length; i++)
            //            {
            //                int val = Convert.ToInt32(baudRate[i]);
            //                if (val == serial.BaudRate)
            //                { cb_baudRate.SelectedIndex = i; }
            //            }
            //    }
            //);
            //connectForm.ShowDialog();
        }
        private void bt_connect_Click(object sender, EventArgs e)
        {
            if (!serial.IsOpen) // Open port
            {
                try
                {
                    if (cb_comPort.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select a COM port");
                        return;
                    }

                    string? strSerialName = (cb_comPort.SelectedItem)?.ToString();
                    string? strBaudRate = (cb_baudRate.SelectedItem)?.ToString();

                    serial.SetConfig(strSerialName, strBaudRate);

                    //Second
                    serial.ReadTimeout = 1000;

                    serial.Open();

                    //Setup completed. Disable setting options
                    bt_connect.Text = "Disconnect";

                    cb_comPort.Enabled = false;
                    cb_baudRate.Enabled = false;
                    bt_setting.Enabled = false;

                    tc_main.Enabled = true;
                    groupBox2.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    throw;
                }
            }
            else // Close port
            {
                serial.Close();

                //Enable setting options
                cb_comPort.Enabled = true;
                cb_baudRate.Enabled = true;

                bt_connect.Text = "Connect";
                bt_setting.Enabled = true;
                tc_main.Enabled = false;
                groupBox2.Enabled = false;
            }
        }

        // Control group
        private void bt_reset_Click(object sender, EventArgs e)
        {

        }
        private void bt_read_Click(object sender, EventArgs e)
        {
            {
                DataGridView? dgv = FindDgv();
                SpiSlave slave;

                if (dgv == null)
                    return;

                slave = dbSlaves[tc_main.SelectedIndex];

                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        int rowIdx = dgv.Rows.IndexOf(row);

                        int regAddr = slave.Registers[rowIdx].Addr;
                        int regValue = slave.Registers[rowIdx].Value;

                        // Write this row value
                        serial.SendRequest(FindSpiSlave(), SendCMD.Read, regAddr, regValue, PrintToMsgBox);
                    }
                }
                else if (dgv.SelectedCells.Count > 0)
                {
                    foreach (DataGridViewCell cell in dgv.SelectedCells)
                    {
                        int rowIdx = cell.RowIndex;

                        int regAddr = slave.Registers[rowIdx].Addr;
                        int regValue = slave.Registers[rowIdx].Value;

                        // Write this row value
                        serial.SendRequest(FindSpiSlave(), SendCMD.Read, regAddr, regValue, PrintToMsgBox);

                    }
                }
            }
        }
        private void bt_write_Click(object sender, EventArgs e)
        {
            if (serial.IsOpen)
            {
                DataGridView? dgv = FindDgv();
                SpiSlave slave;

                if (dgv == null)
                    return;

                slave = dbSlaves[tc_main.SelectedIndex];

                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        int rowIdx = dgv.Rows.IndexOf(row);

                        int regAddr = slave.Registers[rowIdx].Addr;
                        int regValue = slave.Registers[rowIdx].Value;

                        // Write this row value
                        serial.SendRequest(FindSpiSlave(), SendCMD.Write, regAddr, regValue, PrintToMsgBox);
                    }
                }
                else if (dgv.SelectedCells.Count > 0)
                {
                    foreach (DataGridViewCell cell in dgv.SelectedCells)
                    {
                        int rowIdx = cell.RowIndex;

                        int regAddr = slave.Registers[rowIdx].Addr;
                        int regValue = slave.Registers[rowIdx].Value;

                        // Write this row value
                        serial.SendRequest(FindSpiSlave(), SendCMD.Write, regAddr, regValue, PrintToMsgBox);
                    }
                }
            }
        }
        private void bt_verify_Click(object sender, EventArgs e)
        {
            SpiSlave slave = dbSlaves[tc_main.SelectedIndex];
            foreach (RegisterItem reg in slave.Registers)
            {
                // Write this row value
                serial.SendRequest(FindSpiSlave(), SendCMD.Write, reg.Addr, reg.Value, PrintToMsgBox);

            }
        }
        #endregion

        #region SPI method
        public void PrintToMsgBox(DataPackage package)
        {
            string text;
            DateTime dateTimeNow = DateTime.Now;
            //dateTimeNow.GetDateTimeFormats();
            if (package.Header == DataPackage.PKG_HEADER)
            {
                text = $"--> {dateTimeNow}\r\n";
            }
            else if (package.Header == DataPackage.PKG_HEADER_ACK)
            {
                text = $"<-- {dateTimeNow}";
            }
            else return;

            foreach (var value in package.GetData())
            {
                // Convert the decimal value to a hexadecimal value in string form.
                text += $"{value:X2} ";

            }
            text += "\r\n";

            tb_msg.AppendText(text);
            tb_msg.SelectionStart = tb_msg.TextLength;
            tb_msg.ScrollToCaret();//Return to cursor
        }
        private void GetResponse(object sender, SerialDataReceivedEventArgs e)
        {
            if (serial.IsOpen && serial.BytesToRead >= 18)
            {
                try
                {
                    byte[] values = new byte[18];

                    serial.Read(values, 0, 18);

                    if (values.Length != 18)
                        tb_msg.AppendText("Invalid  data frame!\r\n");
                    else
                    {
                        DateTime dateTimeNow = DateTime.Now;
                        //dateTimeNow.GetDateTimeFormats();
                        string datetimeText = string.Format("{0}\r\n", dateTimeNow);
                        string text = "\t\t<-- ";
                        //dateTimeNow.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                        //tb_msg.ForeColor = Color.Red;

                        foreach (var value in values)
                        {
                            // Convert the decimal value to a hexadecimal value in string form.
                            string hexOutput = String.Format("{0:X2}", value);
                            text += hexOutput + " ";

                        }
                        string spaces = "\t\t";
                        text = spaces + datetimeText + text + "\r\n";
                        tb_msg.AppendText(text);
                        tb_msg.SelectionStart = tb_msg.TextLength;
                        tb_msg.ScrollToCaret();//Return to cursor

                        //// save data to file
                        //if (saveDataFS != null)
                        //{
                        //    byte[] info = new UTF8Encoding(true).GetBytes(input + "\r\n");
                        //    saveDataFS.Write(info, 0, info.Length);
                        //}

                        DataPackage? retPackage = DataPackage.TryParse(values);

                        shadowSlaves[tc_main.SelectedIndex].Registers = dbSlaves[tc_main.SelectedIndex].CopyRegisterList();
                        RegisterItem? reg = null;
                        if (retPackage != null && retPackage.WriteEn == SendCMD.Read)
                            try
                            {
                                SpiSlave? slave = dbSlaves.Find(item => item.Info.Address == retPackage.SlaveAddr);
                                if (slave != null)
                                    reg = Array.Find(slave.Registers, item => item.Addr == retPackage.RegAddr);
                                if (reg != null && slave != null)
                                {
                                    reg.Value = (ushort)retPackage.RegValue;
                                    reg.BitValue = Utils.ConvertByteToBoolArray(reg.Value, slave.Info.RegValueSize);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    tb_msg.Text = "";//Clean
                }
            }
        }

        #endregion

        #region DataGridView
        private void tab_Main_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc_main.SelectedIndex < dbSlaves.Count)
                if (!dbInitialized[tc_main.SelectedIndex])
                    SetUpBitGrid(tc_main.SelectedIndex);

            //No need Update()
            //tc_main.TabPages[tc_main.SelectedIndex].Update();
            //tc_main.TabPages[tc_main.SelectedIndex].Refresh();
        }
        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                RegisterItem reg = dbSlaves[tc_main.SelectedIndex].Registers[e.RowIndex];
                reg.BitValue = Utils.ConvertByteToBoolArray(reg.Value,
                    dbSlaves[tc_main.SelectedIndex].Info.RegValueSize);
            }
        }
        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_Value_KeyPress);
            if (((DataGridView)sender).CurrentCell.ColumnIndex == 2) //Desired Column
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_Value_KeyPress);
                }
            }
        }
        // Cell action
        public void CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.RowIndex != -1)
            {
                ((DataGridView)sender).EndEdit();
            }
        }
        public void CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dtgrid = (DataGridView)sender;
            if (e.ColumnIndex >= 3 && e.RowIndex != -1)
            {
                dtgrid.EndEdit();

                RegisterItem reg = dbSlaves[tc_main.SelectedIndex].Registers[e.RowIndex];

                reg.BitValue[e.ColumnIndex - 3]
                    = (bool)dtgrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                reg.Value = Utils.ConvertBoolArrayToByte(reg.BitValue);
                dtgrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
            }
        }
        private void Column_Value_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // allowed only numeric value  ex.10
            //if (!char.IsControl(e.KeyChar)
            //    && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

            // allowed numeric and one dot  ex. 10.23
            if (sender != null)
            {
                if (!((TextBox)sender).Text.StartsWith("0x"))
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == 'x'))
                    {
                        e.Handled = true;
                    }
                    if (e.KeyChar == 'x')
                    {
                        if (((TextBox)sender).Text.IndexOf('x') > -1)
                        {
                            e.Handled = true;
                        }
                        else if (((TextBox)sender).Text != "0")
                        {
                            e.Handled = true;
                        }
                    }
                }
                else
                {
                    char[] allowedChars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                       'A', 'B', 'C', 'D', 'E', 'F',
                                       'a', 'b', 'c', 'd', 'e', 'f'];
                    if (!char.IsControl(e.KeyChar) && !allowedChars.Contains(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                int val = Convert.ToInt32(
                    ((DataGridView)sender).CurrentCell.EditedFormattedValue.ToString(), 16);
                ((DataGridView)sender).CancelEdit();

                RegisterItem reg = dbSlaves[tc_main.SelectedIndex].Registers[e.RowIndex];
                reg.Value = (ushort)val;
                reg.BitValue = Utils.ConvertByteToBoolArray(reg.Value,
                    dbSlaves[tc_main.SelectedIndex].Info.RegValueSize);

                ((DataGridView)sender).RefreshEdit();
            }
            catch (Exception)
            {
                ((DataGridView)sender).CancelEdit();
                ((DataGridView)sender).ClearSelection();
            }
        }
        #endregion

        public DataGridView? FindDgv()
        {
            DataGridView? dgv = null;
            switch (tc_main.SelectedIndex)
            {
                case 0:
                    dgv = dgv_rssi1;
                    break;
                case 1:
                    dgv = dgv_rssi2;
                    break;
                case 2:
                    dgv = dgv_pll;
                    break;
                default:
                    break;
            }
            return dgv;
        }
        public SpiSlave? FindSpiSlave()
        {
            DataGridView? dgv = FindDgv();
            if (dgv == null || dgv.Tag == null) return null;
            else
                return dbSlaves.FirstOrDefault(item => item.Name == (string)dgv.Tag);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serial != null)
            {
                if (serial.IsOpen)
                    serial.Close();
                serial.Dispose();
            }
        }
    }
}