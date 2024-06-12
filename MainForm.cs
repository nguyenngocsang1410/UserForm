using System.IO.Ports;
namespace UserForm
{
    public partial class MainForm : Form
    {
        public List<SpiSlave> db_Slaves = [];
        public bool[] db_initialized = [false, false, false];
        public Serial serial = new("Serial");
        public List<SpiSlave> shadowSlaves = [];

        // TODO: Clean main file
        public MainForm()
        {
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

            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                cbCom.Items.Add(port);
            }

            if (ports.Length > 0)
            {
                cbCom.SelectedIndex = cbCom.Items.IndexOf(serial.PortName);

                if (cbCom.SelectedIndex < 0)
                    cbCom.SelectedIndex = 0;
            }

            string[] baudRate = ["9600", "19200", "38400", "57600", "115200"];
            foreach (string s in baudRate)
                cbBaudRate.Items.Add(s);

            if (serial.BaudRate != 0)
                cbBaudRate.SelectedIndex = cbBaudRate.Items.IndexOf(serial.BaudRate);
            if (cbBaudRate.SelectedIndex < 0)
                cbBaudRate.SelectedIndex = 0;

            // Disable control group
            tab_Main.Enabled = false;
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
            if (db_initialized[gridIdx]) return;

            if (gridIdx >= db_Slaves.Count) return;

            SpiSlave slave = db_Slaves[gridIdx];

            shadowSlaves[gridIdx].Registers = slave.CopyRegisterList();

            const int rssi_col_width = 170;
            const int pll_col_width = 170;

            DataGridView? dgv = null;

            // Get dgv instance
            int col_width = 0;
            switch (slave.Name)
            {
                case "RX1_RSSI":
                    dgv = dtgrid_rssi1;
                    col_width = rssi_col_width;
                    break;
                case "RX2_RSSI":
                    dgv = dtgrid_rssi2;
                    col_width = rssi_col_width;
                    break;
                case "PLL":
                    dgv = dtgrid_pll;
                    col_width = pll_col_width;
                    break;
                default:
                    break;
            }
            if (dgv == null)
                return;

            dgv.DataSource = slave.Registers;

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
                    Width = col_width,
                    Label = "b" + (n_bit_cols - 1 - i).ToString(),
                };
                col.Name = col.Label;

                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.HeaderText = col.Label;

                dgv.Columns.Add(col);
            }
            dgv.Refresh();

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
            db_initialized[gridIdx] = true;
        }
        private void UpdateDataGridView()
        {
            // Find DataGridView
            DataGridView? dgv = GetDgv();
            if (dgv != null)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    // Find matching register
                    int idx = dgv.Rows.IndexOf(row);

                    RegisterItem reg = db_Slaves[tab_Main.SelectedIndex].Registers[idx];
                    RegisterItem reg_shadow = shadowSlaves[tab_Main.SelectedIndex].Registers[idx];

                    reg.Value = Utils.ConvertBoolArrayToByte(reg.BitValue);
                    reg_shadow.Value = Utils.ConvertBoolArrayToByte(reg_shadow.BitValue);

                    //if (reg.Value == reg_shadow.Value)
                    //{
                    //    return;
                    //}

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
            db_Slaves = [new SpiSlave("RX1_RSSI"), new SpiSlave("RX2_RSSI"), new SpiSlave("PLL")];

            foreach (SpiSlave slave in db_Slaves)
            {
                if (slave.Registers.Length == 0)
                    throw new Exception("Slave not found!");

                shadowSlaves.Add(new SpiSlave(slave.Name));
            }

            foreach (SpiSlave slave in db_Slaves)
            {
                foreach (RegisterItem reg in slave.Registers)
                {
                    reg.BitValue = Utils.ConvertByteToBoolArray(reg.Value, slave.Info.RegValueSize);
                }
            }

        }
        #endregion

        #region Action Listener
        // Connection group
        private void bt_setting_Click(object sender, EventArgs e)
        {
            ConnectForm connectForm = new(serial, () =>
                {
                    string[] ports = SerialPort.GetPortNames();
                    foreach (string port in ports)
                    {
                        cbCom.Items.Add(port);
                    }

                    if (ports != null)
                    {
                        cbCom.SelectedIndex = 0;
                        if (serial.PortName != null)
                            foreach (string item in cbCom.Items)
                                if (item == serial.PortName)
                                    cbCom.SelectedIndex = cbCom.Items.IndexOf(item);
                    }

                    string[] baudRate = ["9600", "19200", "38400", "57600", "115200"];
                    foreach (string s in baudRate)
                    {
                        cbBaudRate.Items.Add(s);
                    }
                    cbBaudRate.SelectedIndex = 0;
                    if (serial.BaudRate != 0)
                        for (int i = 0; i < baudRate.Length; i++)
                        {
                            int val = Convert.ToInt32(baudRate[i]);
                            if (val == serial.BaudRate)
                            { cbBaudRate.SelectedIndex = i; }
                        }
                }
            );
            connectForm.ShowDialog();
        }
        private void bt_connect_Click(object sender, EventArgs e)
        {
            if (!serial.IsOpen) // Open port
            {
                try
                {
                    if (cbCom.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select a COM port");
                        return;
                    }

                    string? strSerialName = (cbCom.SelectedItem)?.ToString();
                    string? strBaudRate = (cbBaudRate.SelectedItem)?.ToString();

                    serial.SetConfig(strSerialName, strBaudRate);

                    //Second
                    serial.ReadTimeout = 1000;

                    serial.Open();

                    //Setup completed. Disable setting options
                    bt_connect.Text = "Disconnect";

                    cbCom.Enabled = false;
                    cbBaudRate.Enabled = false;
                    bt_setting.Enabled = false;

                    tab_Main.Enabled = true;
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
                cbCom.Enabled = true;
                cbBaudRate.Enabled = true;

                bt_connect.Text = "Connect";
                bt_setting.Enabled = true;
                tab_Main.Enabled = false;
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
                DataGridView? dgv = GetDgv();
                SpiSlave slave;

                if (dgv == null)
                    return;

                slave = db_Slaves[tab_Main.SelectedIndex];

                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        int rowIdx = dgv.Rows.IndexOf(row);

                        int regAddr = slave.Registers[rowIdx].Addr;
                        int regValue = slave.Registers[rowIdx].Value;

                        // Write this row value
                        SendRequest(slave.Info.Address, SendCMD.Read, regAddr, regValue);
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
                        SendRequest(slave.Info.Address, SendCMD.Read, regAddr, regValue);
                    }
                }
            }
        }
        private void bt_write_Click(object sender, EventArgs e)
        {
            if (serial.IsOpen)
            {
                DataGridView? dgv = GetDgv();
                SpiSlave slave;

                if (dgv == null)
                    return;

                slave = db_Slaves[tab_Main.SelectedIndex];

                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        int rowIdx = dgv.Rows.IndexOf(row);

                        int regAddr = slave.Registers[rowIdx].Addr;
                        int regValue = slave.Registers[rowIdx].Value;

                        // Write this row value
                        SendRequest(slave.Info.Address, SendCMD.Write, regAddr, regValue);
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
                        SendRequest(slave.Info.Address, SendCMD.Write, regAddr, regValue);
                    }
                }
            }
        }
        private void bt_verify_Click(object sender, EventArgs e)
        {
            SpiSlave slave = db_Slaves[tab_Main.SelectedIndex];
            foreach (RegisterItem reg in db_Slaves[tab_Main.SelectedIndex].Registers)
            {
                // Write this row value
                SendRequest(slave.Info.Address, SendCMD.Write, reg.Addr, reg.Value);
            }
        }
        #endregion

        #region SPI method
        public DataPackage? SendRequest(int slaveAddr, SendCMD write_en, int regAddr, int regValue)
        {
            SpiSlave? slave = db_Slaves.Find(item => item.Info.Address == slaveAddr);

            if (slave == null || slave.Info == null)
            {
                MessageBox.Show("Unrecognized slave", "Error");
                return null;
            }

            DataPackage dataSend = new()
            {
                RegAddr = regAddr,
                RegValue = regValue,
                RegAddrSize = slave.Info.RegAddressSize,
                RegValueSize = slave.Info.RegValueSize,
                SlaveAddr = slaveAddr,
                WriteEn = write_en
            };
            byte[] packetSend = dataSend.CreatePackage();
            DateTime dateTimeNow = DateTime.Now;
            //dateTimeNow.GetDateTimeFormats();
            string text = string.Format("{0}\r\n--> ", dateTimeNow);
            serial.Write(packetSend, 0, packetSend.Length);

            foreach (var value in packetSend)
            {
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X2}", value);
                text += (hexOutput + " ");

            }
            text += "\r\n";

            tbMsg.AppendText(text);
            tbMsg.SelectionStart = tbMsg.TextLength;
            tbMsg.ScrollToCaret();//Return to cursor

            return null;
        }
        private void GetResponse(object sender, SerialDataReceivedEventArgs e)
        {
            if (serial.IsOpen && serial.BytesToRead >= 18)
            {
                byte[] values = new byte[18];
                DateTime dateTimeNow = DateTime.Now;
                //dateTimeNow.GetDateTimeFormats();
                string datetimeText = string.Format("{0}\r\n", dateTimeNow);
                string text = "\t\t<-- ";
                //dateTimeNow.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                //tbMsg.ForeColor = Color.Red;
                try
                {
                    serial.Read(values, 0, 18);
                    foreach (var value in values)
                    {
                        // Convert the decimal value to a hexadecimal value in string form.
                        string hexOutput = String.Format("{0:X2}", value);
                        text += hexOutput + " ";

                    }
                    string spaces = "\t\t";
                    text = spaces + datetimeText + text + "\r\n";
                    tbMsg.AppendText(text);
                    tbMsg.SelectionStart = tbMsg.TextLength;
                    tbMsg.ScrollToCaret();//Return to cursor

                    //// save data to file
                    //if (saveDataFS != null)
                    //{
                    //    byte[] info = new UTF8Encoding(true).GetBytes(input + "\r\n");
                    //    saveDataFS.Write(info, 0, info.Length);
                    //}


                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    tbMsg.Text = "";//Clean
                }

                if (values.Length != 18)
                    tbMsg.AppendText("Invalid data frame!\r\n");
                else
                {
                    DataPackage? retPackage = DataPackage.TryParse(values);

                    shadowSlaves[tab_Main.SelectedIndex].Registers = db_Slaves[tab_Main.SelectedIndex].CopyRegisterList();
                    RegisterItem? reg = null;
                    if (retPackage != null && retPackage.WriteEn == SendCMD.Read)
                        try
                        {
                            SpiSlave? slave = db_Slaves.Find(item => item.Info.Address == retPackage.SlaveAddr);
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
        }

        #endregion

        #region DataGridView
        private void tab_Main_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab_Main.SelectedIndex < db_Slaves.Count)
                if (!db_initialized[tab_Main.SelectedIndex])
                    SetUpBitGrid(tab_Main.SelectedIndex);
            //No need Update()
            //tab_Main.TabPages[tab_Main.SelectedIndex].Update();
            //tab_Main.TabPages[tab_Main.SelectedIndex].Refresh();
        }
        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                RegisterItem reg = db_Slaves[tab_Main.SelectedIndex].Registers[e.RowIndex];
                reg.BitValue = Utils.ConvertByteToBoolArray(reg.Value,
                    db_Slaves[tab_Main.SelectedIndex].Info.RegValueSize);
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

                RegisterItem reg = db_Slaves[tab_Main.SelectedIndex].Registers[e.RowIndex];

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

                RegisterItem reg = db_Slaves[tab_Main.SelectedIndex].Registers[e.RowIndex];
                reg.Value = (ushort)val;
                reg.BitValue = Utils.ConvertByteToBoolArray(reg.Value,
                    db_Slaves[tab_Main.SelectedIndex].Info.RegValueSize);

                ((DataGridView)sender).RefreshEdit();
            }
            catch (Exception)
            {
                ((DataGridView)sender).CancelEdit();
                ((DataGridView)sender).ClearSelection();
            }
        }
        #endregion

        public DataGridView? GetDgv()
        {
            DataGridView? dgv = null;
            switch (tab_Main.SelectedIndex)
            {
                case 0:
                    dgv = dtgrid_rssi1;
                    break;
                case 1:
                    dgv = dtgrid_rssi2;
                    break;
                case 2:
                    dgv = dtgrid_pll;
                    break;
                default:
                    break;
            }

            return dgv;
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