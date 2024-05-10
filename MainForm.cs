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
                cbCom.SelectedIndex = 0;
                if (serial.port.PortName != null)
                    foreach (string item in cbCom.Items)
                        if (item == serial.port.PortName)
                            cbCom.SelectedIndex = cbCom.Items.IndexOf(item);
            }

            string[] baudRate = ["9600", "19200", "38400", "57600", "115200"];
            foreach (string s in baudRate)
            {
                cbBaudRate.Items.Add(s);
            }
            cbBaudRate.SelectedIndex = 0;
            if (serial.port.BaudRate != 0)
                for (int i = 0; i < baudRate.Length; i++)
                {
                    int val = Convert.ToInt32(baudRate[i]);
                    if (val == serial.port.BaudRate)
                    { cbBaudRate.SelectedIndex = i; }
                }

            shadowSlaves[tab_Main.SelectedIndex].Registers = db_Slaves[tab_Main.SelectedIndex].CopyRegister();
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
            const int pll_col_width = 70;

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
            dgv.Update();
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
                    
                    if (reg.Value == reg_shadow.Value)
                    {
                        return;
                    }

                    //reg.BitValue = ConvertByteToBoolArray(reg.Value, slave.Info.RegValueSize);
                    //reg_shadow.BitValue = ConvertByteToBoolArray(reg_shadow.Value, slave.Info.RegValueSize);

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
                    if (serial.port.PortName != null)
                        foreach (string item in cbCom.Items)
                            if (item == serial.port.PortName)
                                cbCom.SelectedIndex = cbCom.Items.IndexOf(item);
                }

                string[] baudRate = ["9600", "19200", "38400", "57600", "115200"];
                foreach (string s in baudRate)
                {
                    cbBaudRate.Items.Add(s);
                }
                cbBaudRate.SelectedIndex = 0;
                if (serial.port.BaudRate != 0)
                    for (int i = 0; i < baudRate.Length; i++)
                    {
                        int val = Convert.ToInt32(baudRate[i]);
                        if (val == serial.port.BaudRate)
                        { cbBaudRate.SelectedIndex = i; }
                    }
            });
            connectForm.ShowDialog();
        }
        private void bt_connect_Click(object sender, EventArgs e)
        {

            if (!serial.port.IsOpen) // Open port
            {
                try
                {
                    if (cbCom.SelectedIndex == -1)
                    {
                        MessageBox.Show("Error: Select COM port", "Error");
                        return;
                    }

                    string? strSerialName = (cbCom.SelectedItem)?.ToString();
                    string? strBaudRate = (cbBaudRate.SelectedItem)?.ToString();

                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);

                    serial.port.PortName = strSerialName;
                    serial.port.BaudRate = iBaudRate;

                    serial.port.Open();

                    //Setup completed. Disable setting options
                    cbCom.Enabled = false;
                    cbBaudRate.Enabled = false;

                    bt_connect.Text = "Disconnect";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    throw;
                }
            }
            else // Close port
            {
                serial.port.Close();

                //Enable setting options
                cbCom.Enabled = true;
                cbBaudRate.Enabled = true;

                bt_connect.Text = "Connect";
            }
        }
        private void bt_reset_Click(object sender, EventArgs e)
        {

        }
        private void bt_read_Click(object sender, EventArgs e)
        {
            QueryRegister(db_Slaves[tab_Main.SelectedIndex]);
            UpdateDataGridView();
        }
        private void bt_write_Click(object sender, EventArgs e)
        {

        }
        private void bt_verify_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region SPI method

        private void QueryRegister(SPISlave slave)
        {
            foreach (RegisterItem reg in slave.Registers)
            {
                if (slave.Info != null)
                {
                    SendRequest(slave.Info.Address, 0x01, reg.Addr, reg.Value);
                    GetResponse(slave.Info.Address, 0x01, reg.Addr, reg.Value, 500,
                        (retPackage) =>
                        {

                        });
                }
            }
        }

        public void SendRequest(int slaveAddr, int write_en, int regAddr, int regValue)
        {
            SPISlave? slave = db_Slaves.Find(item => item.Info.Address == slaveAddr);

            if (slave == null || slave.Info == null)
            {
                MessageBox.Show("Unrecognize slave", "Error");
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

            serial.port.Write(packetSend, 0, packetSend.Length);
        }




        public async void GetResponse(int slaveAddr, int write_en, int regAddr, int regValue,
                                             int timeout = 500, Action<DataPackage>? responseHandler = null)
        {
            SPISlave? slave = db_Slaves.Find(item => item.Info.Address == slaveAddr);
            if (slave == null || slave.Info == null)
            {
                throw new Exception("Invalid slave");
            }

            int frmLen = 18;

            byte[] readBuffer = new byte[frmLen];

            Task pollingResponse = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int retry_times = 10;
                    int counter = 0;
                    if (serial.port.BytesToRead >= frmLen)
                    {
                        int ret;
                        ret = serial.port.Read(readBuffer, 0, frmLen);
                        if (ret < frmLen) MessageBox.Show("Response invalid", "Error");
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

                string err;

                if (write_en != retPackage.WriteEn)
                    err = "0x12";
                else if (retPackage.SlaveAddr != slaveAddr)
                    err = "0x12";
                else if (retPackage.RegAddr != regAddr)
                    err = "0x12";
                else if (retPackage.RegAddrSize != slave.Info.RegAddressSize)
                    err = "0x12";
                else if (write_en == 1 & (retPackage.RegValue != regValue))
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
                responseHandler?.Invoke(retPackage);
            }
            else
            {
                // timeout logic
                MessageBox.Show("Timeout!");
            }
        }
        #endregion
        #region Utils
        private static byte ConvertBoolArrayToByte(bool[] source)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 8 - source.Length;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (byte)(1 << (7 - index));

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

        private void dtgrid_rssi1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgrid_rssi2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgrid_pll_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgrid_rssi1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.RowIndex != -1)
            {
                dtgrid_rssi1.EndEdit();
                db_Slaves[tab_Main.SelectedIndex].Registers[e.RowIndex].BitValue[e.ColumnIndex] 
                    = (bool)dtgrid_rssi1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void dtgrid_rssi1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.RowIndex != -1)
            {
                dtgrid_rssi1.EndEdit();
            }
        }
    }
}