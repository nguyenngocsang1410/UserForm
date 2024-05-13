using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Security.Cryptography;

namespace UserForm
{
    public partial class MainForm : Form
    {
        public List<SPISlave> db_Slaves = [];

        public bool[] db_initialized = [false, false, false];

        public Serial serial = new("SPI");

        public bool SPIInTransmission = false;
        public List<SPISlave> shadowSlaves = [];


        public MainForm()
        {
            CreateDB();

            if (serial.IsOpen)
            {
                serial.Close();
            }

            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            SetUpBitGrid(0);

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cbCom.Items.Add(port);
            }

            if (ports != null)
            {
                cbCom.SelectedIndex = cbCom.Items.IndexOf(serial.PortName);

                if (cbCom.SelectedIndex < 0)
                    cbCom.SelectedIndex = 0;
            }

            string[] baudRate = ["9600", "19200", "38400", "57600", "115200"];
            foreach (string s in baudRate)
                cbBaudRate.Items.Add(s);

            cbBaudRate.SelectedIndex = 0;
            if (serial.BaudRate != 0)
                cbBaudRate.SelectedIndex = cbBaudRate.Items.IndexOf(serial.BaudRate);
            if (cbBaudRate.SelectedIndex < 0)
                cbBaudRate.SelectedIndex = 0;

            // Disable control group
            tab_Main.Enabled = false;
            groupBox2.Enabled = false;
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void SetUpBitGrid(int gridIdx)
        {
            if (gridIdx >= db_Slaves.Count) return;


            SPISlave slave = db_Slaves[gridIdx];
            shadowSlaves[gridIdx].Registers = slave.CopyRegister();

            const int rssi_col_width = 150;
            const int pll_col_width = 150;

            DataGridView? dgv = null;

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
            int bit_cols = slave.Registers[0].BitName.Length;
            for (int i = 0; i < bit_cols; i++)
            {
                CustomCheckBoxColumn col = new()
                {
                    Width = col_width,
                    Label = "b" + (bit_cols - 1 - i).ToString(),
                };
                col.Name = col.Label;

                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.HeaderText = col.Label;

                dgv.Columns.Add(col);
            }
            dgv.Refresh();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int row_idx = dgv.Rows.IndexOf(row);
                slave.Registers[row_idx].BitValue = ConvertByteToBoolArray(slave.Registers[row_idx].Value, slave.Info.RegValueSize);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    int cellIdx = row.Cells.IndexOf(cell);
                    if (cellIdx < 3)
                    { continue; }

                    if (cell is CustomCheckBoxCell custem_cell)
                    {
                        custem_cell.Label = slave.Registers[row_idx].BitName[cellIdx - 3];
                        custem_cell.Value = slave.Registers[row_idx].BitValue[cellIdx - 3];
                    }
                }
            }
            dgv.Refresh();

            db_initialized[gridIdx] = true;
        }
        private void UpdateDataGridView()
        {
            // Find DataGridView
            DataGridView? dgv = tab_Main.SelectedTab?.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgv != null)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    // Find matching register
                    int idx = dgv.Rows.IndexOf(row);

                    SPISlave slave = db_Slaves[tab_Main.SelectedIndex];
                    RegisterItem reg = slave.Registers[idx];
                    RegisterItem reg_shadow = shadowSlaves[tab_Main.SelectedIndex].Registers[idx];

                    reg.Value = ConvertBoolArrayToByte(reg.BitValue);
                    reg_shadow.Value = ConvertBoolArrayToByte(reg_shadow.BitValue);

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
            SPISlave RX1_RSSI = new("RX1_RSSI");
            SPISlave RX2_RSSI = new("RX2_RSSI");
            SPISlave PLL = new("PLL");

            db_Slaves.Add(RX1_RSSI);
            db_Slaves.Add(RX2_RSSI);
            db_Slaves.Add(PLL);
            foreach (SPISlave slave in db_Slaves)
            {
                if (slave.Registers.Count == 0)
                    throw new Exception("Slave not found!");

                shadowSlaves.Add(new SPISlave(slave.Name));
            }

            foreach (SPISlave slave in db_Slaves)
            {
                foreach (RegisterItem reg in slave.Registers)
                {
                    reg.BitValue = ConvertByteToBoolArray(reg.Value, slave.Info.RegValueSize);
                }
            }

        }

        #region Action Listener
        private void tab_Main_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab_Main.SelectedIndex < db_Slaves.Count)
                if (!db_initialized[tab_Main.SelectedIndex])
                    SetUpBitGrid(tab_Main.SelectedIndex);
            tab_Main.TabPages[tab_Main.SelectedIndex].Update();
            tab_Main.TabPages[tab_Main.SelectedIndex].Refresh();
        }
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
            });
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

                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);

                    serial.PortName = strSerialName;
                    serial.BaudRate = iBaudRate;

                    serial.DtrEnable = true;
                    serial.RtsEnable = true;
                    //Second
                    serial.ReadTimeout = 1000;

                    serial.Open();

                    //Setup completed. Disable setting options
                    cbCom.Enabled = false;
                    cbBaudRate.Enabled = false;

                    bt_connect.Text = "Disconnect";

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
        private void bt_reset_Click(object sender, EventArgs e)
        {

        }
        private void bt_read_Click(object sender, EventArgs e)
        {
            {
                DataGridView? dgv = null;
                SPISlave slave;
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

                        // Read this row value
                        SendRequest(slave.Info.Address, SendCMD.Read, regAddr, regValue);
                    }
                }
                else
                {
                    foreach (RegisterItem reg in db_Slaves[tab_Main.SelectedIndex].Registers)
                    {
                        // Read this row value
                        SendRequest(slave.Info.Address, SendCMD.Read, reg.Addr, reg.Value);
                    }
                }
            }
        }
        private void bt_write_Click(object sender, EventArgs e)
        {
            if (serial.IsOpen)
            {
                DataGridView? dgv = null;
                SPISlave slave;
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
                else
                {
                    foreach (RegisterItem reg in db_Slaves[tab_Main.SelectedIndex].Registers)
                    {
                        // Write this row value
                        SendRequest(slave.Info.Address, SendCMD.Write, reg.Addr, reg.Value);
                    }
                }
            }
        }
        private void bt_verify_Click(object sender, EventArgs e)
        {
            SPISlave slave = db_Slaves[tab_Main.SelectedIndex];
            foreach (RegisterItem reg in db_Slaves[tab_Main.SelectedIndex].Registers)
            {
                // Write to reg and read back
                SendRequest(slave.Info.Address, SendCMD.Write, reg.Addr, reg.Value);
                Thread.Sleep(200);
                // Read request
                SendRequest(slave.Info.Address, SendCMD.Read, reg.Addr, reg.Value);
                Thread.Sleep(200);
            }

            // Process updated data
        }
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

                reg.Value = ConvertBoolArrayToByte(reg.BitValue);
                dtgrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
            }
        }
        #endregion
        #region SPI method
        public void SendRequest(int slaveAddr, SendCMD write_en, int regAddr, int regValue)
        {
            SPISlave? slave = db_Slaves.Find(item => item.Info.Address == slaveAddr);

            if (slave == null || slave.Info == null)
            {
                MessageBox.Show("Unrecognized slave", "Error");
                return;
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

            serial.Write(packetSend, 0, packetSend.Length);

            GetResponse(responseHandler: (retPackage) =>
            {
                string err;

                if (write_en != retPackage.WriteEn)
                    err = "0x12";
                else if (retPackage.SlaveAddr != slaveAddr)
                    err = "0x12";
                else if (retPackage.RegAddr != regAddr)
                    err = "0x12";
                else if (retPackage.RegAddrSize != slave.Info.RegAddressSize)
                    err = "0x12";
                else if (write_en == SendCMD.Write & (retPackage.RegValue != regValue))
                    err = "0x12";
                else if (retPackage.RegValueSize != slave.Info.RegValueSize)
                    err = "0x12";
                else if (retPackage.StatusCode != "0")
                    err = retPackage.StatusCode;
                else err = "0";

                if (err != "0")
                {
                    DataPackage.error_code(err);
                }

                if (write_en == 0)
                {
                    shadowSlaves.Find(item => item.Info.Address == slave.Info.Address)!.Registers = slave.CopyRegister();
                    RegisterItem? reg = slave.Registers.Find(item => item.Addr == regAddr);
                    if (reg != null)
                    {
                        reg.Value = (ushort)retPackage.RegValue;
                        reg.BitValue = ConvertByteToBoolArray(reg.Value, retPackage.RegValueSize);
                    }
                }
            });
        }
        public async void GetResponse(int timeout = 5000, Action<DataPackage>? responseHandler = null)
        {
            int frmLen = 18;

            byte[] readBuffer = new byte[frmLen];

            Task pollingResponse = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int retry_times = 10;
                    int counter = 0;
                    if (serial.BytesToRead >= frmLen)
                    {
                        int ret;
                        ret = serial.Read(readBuffer, 0, frmLen);
                        if (ret < frmLen) MessageBox.Show("Response invalid", "Error");
                        break;
                    }
                    else
                    {
                        Thread.Sleep(100);
                        if (counter++ > retry_times) break;
                    }
                }
            });

            if (await Task.WhenAny(pollingResponse, Task.Delay(timeout)) == pollingResponse)
            {
                DataPackage? retPackage = DataPackage.TryParse(readBuffer[..frmLen]);

                if (retPackage == null)
                    return;

                responseHandler?.Invoke(retPackage);
                return;
            }
            else
            {
                // timeout logic
                MessageBox.Show("Timeout!");
                return;
            }
        }
        #endregion
        #region Utils
        private static ushort ConvertBoolArrayToByte(bool[] source)
        {
            ushort result = 0;
            // This assumes the array never contains more than 8 elements!

            int index = 0;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (ushort)(1 << (source.Length - index - 1));
                index++;
            }

            return result;
        }
        private static bool[] ConvertByteToBoolArray(ushort b, int len)
        {
            // prepare the return result
            bool[] result = new bool[len];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < len; i++)
                result[i] = (b & (1 << i)) != 0;

            // reverse the array
            Array.Reverse(result);

            return result;
        }
        #endregion
    }
}