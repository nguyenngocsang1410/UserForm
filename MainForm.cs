using System.Drawing.Drawing2D;
using System.IO.Ports;

namespace UserForm
{
    public partial class MainForm : Form
    {
        public List<SPISlave> db_Slaves = [];


        public bool[] db_initialized = [false,false,false];

        public Serial serial = new("SPI");

        public bool SPIInTransmission = false;

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
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }
        private void SetUpBitGrid(int gridIdx)
        {
            if (gridIdx >= db_Slaves.Count) return;

            SPISlave slave = db_Slaves[gridIdx];

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
                foreach (DataGridViewCell cell in row.Cells)
                {
                    int cellIdx = row.Cells.IndexOf(cell);
                    if (cellIdx < 3)
                    { continue; }

                    CustomCheckBoxCell? custem_cell = cell as CustomCheckBoxCell;
                    if (custem_cell != null)
                        custem_cell.Label = slave.Registers[row_idx].BitName[cellIdx - 3];
                }
            }
            dgv.Update();
            dgv.Refresh();

            db_initialized[gridIdx] = true;
        }
        private void UpdateDataGridView()
        {
            DataGridView? dgv = tab_Main.SelectedTab?.Controls.OfType<DataGridView>().FirstOrDefault();
            if (dgv != null)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    int idx = dgv.Rows.IndexOf(row);
                    SPISlave slave = db_Slaves[tab_Main.SelectedIndex];
                    RegisterItem reg = slave.Registers[idx];

                    bool[] checkList = ConvertByteToBoolArray(reg.Value, slave.Info.RegValueSize);
                    for (int i = 0; i < checkList.Length; i++)
                    {
                        CustomCheckBoxCell cell = (CustomCheckBoxCell)row.Cells[i+3];
                        bool cellVal = (bool)cell.Value;
                        if (cellVal != checkList[i])
                        {
                            // HIGHLIGHT THIS!!!!!
                            row.Cells[i + 3].Value = cellVal;
                            cell.Style.BackColor = Color.LightYellow;
                        }
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
            ConnectForm connectForm = new(serial,()=>
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

                    string? strSerialName    = (cbCom.SelectedItem)?.ToString();
                    string? strBaudRate      = (cbBaudRate.SelectedItem)?.ToString();

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


                    SendRequest(slave.Info.Address, false, reg.Addr, reg.Value);
                    int retVal = GetResponse(slave.Info.Address, false, reg.Addr, reg.Value, 500);
                }
            }
        }

        public void SendRequest(int slaveAddr, bool write_en, int regAddr, int regValue)
        {
            SPISlave? slave = GetSPISlave(slaveAddr);

            if (slave == null || slave.Info == null)
            {
                MessageBox.Show("Unrecognize slave", "Error");
                return;
            }



            serial.port.Write(dataSend, 0, dataSend.Length);
        }


        public SPISlave? GetSPISlave(int slaveAddr)
        {
            return db_Slaves.Find(item => item.Info.Address == slaveAddr);
        }

        public async DataPackage GetResponse(int slaveAddr, int write_en, int regAddr, int regValue,
                                             int timeout = 500, Action<int>? listener = null)
        {
            SPISlave? slave = db_Slaves.Find(item => item.Info.Address == slaveAddr);
            if (slave == null || slave.Info == null)
            {
                throw new Exception("Invalid slave");
            }

            int frmLen = 18;
            Task pollingResponse = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int retry_times = 10;
                    int counter = 0;
                    if (serial.port.BytesToRead >= frmLen)
                    {
                        int ret;
                        ret = serial.port.Read(slave.Buffer, 0, frmLen);
                        if (ret < frmLen) MessageBox.Show("Response invalid", "Error");
                    }
                    else
                    {
                        Thread.Sleep(100);
                        if(counter++ > retry_times) break;
                    }
                }
            });

            if (await Task.WhenAny(pollingResponse, Task.Delay(timeout)) == pollingResponse)
            {
                DataPackage retPackage = DataPackage.TryParse(slave.Buffer[..frmLen]) ?? new();

                string err;
                if (write_en != retPackage.WriteEn)
                    err = "0x12";
                else if (package[4] != slaveAddr)
                    err = "0x12";
                else if (regAddrRead != regAddr)
                    err = "0x12";
                else if (package[4] != slave.RegAddressSize)
                    err = "0x12";
                else if (write_en & (regValueRead != regValue))
                    err = "0x12";
                else if (package[9] != slave.RegValueSize)
                    err = "0x12";
                else if (package[10] != 0)
                    err = Convert.ToHexString([package[10]]);
            }
            else
            {
                // timeout logic
                MessageBox.Show("Timeout!");
            }

        }

        //        {
        //            /**
        //             * Call -> wait for get enough bit -> return
        //             *      `-> timeout -> Message show -> return
        //             * 
        //             */

        //            SlaveData? slave = null;
        //            foreach (SlaveData sd in slaves)
        //            {
        //                if (sd.slaveAddr == slaveAddr)
        //                {
        //                    slave = sd;
        //                    break;
        //                }
        //}
        //if (slave == null)
        //{
        //    MessageBox.Show("Unrecognize slave", "Error");
        //    return;
        //}

        //// Response frame
        //int frmLen = 1 + 2 + 1 + slave.RegAddressSize + 1 + slave.RegValueSize + 1 + 1 + 2;



        //if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
        //{
        //    // task completed within timeout
        //    ProcessReceiveData(slave, write_en, regAddr, regValue, frmLen);
        //    listener?.Invoke(slave, regAddr);
        //}
        //else
        //{
        //    // timeout logic
        //    MessageBox.Show("Timeout!");
        //}
        //        }
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
    }
}