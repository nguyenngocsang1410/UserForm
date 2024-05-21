using System.IO.Ports;
using System.Text;

namespace UserForm
{
    public partial class ConnectForm : Form
    {
        public SerialPort serialPort = new();
        public Serial serial;

        public Action? closeListener;

        //String saveDataFile = null;
        //FileStream saveDataFS = null;

        public ConnectForm(Serial serial, Action closeListener)
        {
            this.serial = serial;
            if (serial.IsOpen)
            {
                serial.Close();
            }
            this.closeListener = closeListener;

            InitializeComponent();
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {
            Init_Port();

            CheckForIllegalCrossThreadCalls = false;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            //Second
            serialPort.ReadTimeout = 1000;

            serialPort.Close();

            btSend.Enabled = false;
        }

        // Data read by handler
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {

        }

        private void btClear_Click(object sender, EventArgs e)
        {
            tbReceive.Clear();
        }
        private void btSend_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("Open port", "Error");
                return;
            }

            if (rbtSendDataASCII.Checked == true)
            {
                string strSend = tbTransmit.Text;
                serialPort.WriteLine(strSend);

            }
            else
            {
                try
                {
                    // Read the hex string from the TextBox
                    string hexString = tbTransmit.Text;

                    // Split the hex string into an array of hex values
                    string[] hexValues = hexString.Split(' ');

                    // Convert each hex value to a byte and store in a byte array
                    byte[] bytes = new byte[hexValues.Length];
                    for (int i = 0; i < hexValues.Length; i++)
                    {
                        bytes[i] = Convert.ToByte(hexValues[i], 16);
                    }

                    // Send the byte array to the SerialPort
                    serialPort.Write(bytes, 0, bytes.Length);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Invalid hex format. Please enter a valid hex string.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            cbCom.Text = "";
            cbCom.Items.Clear();

            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                return;
            }

            foreach (string s in str)
            {
                cbCom.Items.Add(s);
            }

            cbCom.SelectedIndex = 0;
            cbBaudRate.SelectedIndex = 0;
            cbDataBit.SelectedIndex = 3;
            cbCheckBit.SelectedIndex = 0;
            cbStopBit.SelectedIndex = 0;

        }

        private void btOpenClose_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen) // Open port
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
                    string? strDataBit = (cbDataBit.SelectedItem)?.ToString();
                    string? strCheckBit = (cbCheckBit.SelectedItem)?.ToString();
                    string? strStopBit = (cbStopBit.SelectedItem)?.ToString();

                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);
                    Int32 iDataBit = Convert.ToInt32(strDataBit);

                    serialPort.PortName = strSerialName;
                    serialPort.BaudRate = iBaudRate;
                    serialPort.DataBits = iDataBit;

                    switch (strStopBit)
                    {
                        case "1":
                            serialPort.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            serialPort.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            serialPort.StopBits = StopBits.Two;
                            break;
                        default:
                            break;
                    }
                    switch (strCheckBit)
                    {
                        case "None":
                            serialPort.Parity = Parity.None;
                            break;
                        case "Odd":
                            serialPort.Parity = Parity.Odd;
                            break;
                        case "Even":
                            serialPort.Parity = Parity.Even;
                            break;
                        default:
                            break;
                    }

                    //if (saveDataFile != null)
                    //{
                    //    saveDataFS = File.Create(saveDataFile);
                    //}

                    serialPort.Open();

                    //Setup completed. Disable setting options
                    cbCom.Enabled = false;
                    cbBaudRate.Enabled = false;
                    cbDataBit.Enabled = false;
                    cbCheckBit.Enabled = false;
                    cbStopBit.Enabled = false;
                    rbtSendDataASCII.Enabled = false;
                    rbtSendDataHex.Enabled = false;
                    rbtReceiveDataASCII.Enabled = false;
                    rbtReceiveDataHEX.Enabled = false;
                    btSend.Enabled = true;
                    btRefresh.Enabled = false;

                    btOpenClose.Text = "Close";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    throw;
                }
            }
            else // Close port
            {
                serialPort.Close();

                //Enable setting options
                cbCom.Enabled = true;
                cbBaudRate.Enabled = true;
                cbDataBit.Enabled = true;
                cbCheckBit.Enabled = true;
                cbStopBit.Enabled = true;
                rbtSendDataASCII.Enabled = true;
                rbtSendDataHex.Enabled = true;
                rbtReceiveDataASCII.Enabled = true;
                rbtReceiveDataHEX.Enabled = true;
                btSend.Enabled = false;
                btRefresh.Enabled = true;

                btOpenClose.Text = "Open";

                //if (saveDataFS != null)
                //{
                //    saveDataFS.Close(); // Close filestream
                //    saveDataFS = null;// Release resource
                //}
            }
        }

        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                DateTime dateTimeNow = DateTime.Now;
                //dateTimeNow.GetDateTimeFormats();
                tbReceive.Text += string.Format("{0}\r\n", dateTimeNow);
                //dateTimeNow.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                tbReceive.ForeColor = Color.Red;

                if (rbtReceiveDataASCII.Checked == true)
                {
                    try
                    {
                        string input = serialPort.ReadLine();
                        string[] splittedinput;
                        if (input.Contains(Environment.NewLine))
                        {
                            splittedinput = input.Split("\r\n");
                        }
                        else splittedinput = [input];

                        foreach (string line in splittedinput)
                        {
                            tbReceive.Text += line + "\r\n";
                        }

                        //// save data to file
                        //if (saveDataFS != null)
                        //{
                        //    byte[] info = new UTF8Encoding(true).GetBytes(input + "\r\n");
                        //    saveDataFS.Write(info, 0, info.Length);
                        //}
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                    tbReceive.SelectionStart = tbReceive.Text.Length;
                    tbReceive.ScrollToCaret();//Return to cursor
                    serialPort.DiscardInBuffer(); //Clear Serial port buffer 
                }
                else //Receive data hex
                {
                    try
                    {
                        string input = serialPort.ReadLine();
                        char[] values = input.ToCharArray();
                        foreach (char letter in values)
                        {
                            // Get the integral value of the character.
                            int value = Convert.ToInt32(letter);
                            // Convert the decimal value to a hexadecimal value in string form.
                            string hexOutput = String.Format("{0:X}", value);
                            tbReceive.AppendText(hexOutput + " ");
                            tbReceive.SelectionStart = tbReceive.Text.Length;
                            tbReceive.ScrollToCaret();//Return to cursor
                            //tbReceive.Text += hexOutput + " ";

                        }

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
                        tbReceive.Text = "";//Clean
                    }
                }
            }
            else
            {
            }
        }

        private void Init_Port()
        {
            /* Setup port */

            // Get port name
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

            string[] dataBit = ["5", "6", "7", "8"];
            foreach (string s in dataBit)
            {
                cbDataBit.Items.Add(s);
            }
            cbDataBit.SelectedIndex = 3;

            string[] checkBit = ["None", "Even", "Odd", "Mask", "Space"];
            foreach (string s in checkBit)
            {
                cbCheckBit.Items.Add(s);
            }
            cbCheckBit.SelectedIndex = 0;

            string[] stopBit = ["1", "1.5", "2"];
            foreach (string s in stopBit)
            {
                cbStopBit.Items.Add(s);
            }
            cbStopBit.SelectedIndex = 0;

            string? strSerialName = (cbCom.SelectedItem)?.ToString();
            string? strBaudRate = (cbBaudRate.SelectedItem)?.ToString();
            string? strDataBit = (cbDataBit.SelectedItem)?.ToString();
            string? strCheckBit = (cbCheckBit.SelectedItem)?.ToString();
            string? strStopBit = (cbStopBit.SelectedItem)?.ToString();

            switch (strStopBit)
            {
                case "1":
                    serialPort.StopBits = StopBits.One;
                    break;
                case "1.5":
                    serialPort.StopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    serialPort.StopBits = StopBits.Two;
                    break;
                default:
                    break;
            }
            switch (strCheckBit)
            {
                case "None":
                    serialPort.Parity = Parity.None;
                    break;
                case "Odd":
                    serialPort.Parity = Parity.Odd;
                    break;
                case "Even":
                    serialPort.Parity = Parity.Even;
                    break;
                default:
                    break;
            }

            Int32 iBaudRate = Convert.ToInt32(strBaudRate);
            Int32 iDataBit = Convert.ToInt32(strDataBit);

            serialPort.PortName = strSerialName;
            serialPort.BaudRate = iBaudRate;
            serialPort.DataBits = iDataBit;

            rbtSendDataASCII.Checked = true;
            rbtReceiveDataASCII.Checked = true;

            //rbtReceiveDataASCII.Enabled = false;
            //rbtReceiveDataHEX.Enabled = false;
        }

        private void ConnectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
                serialPort.Close();

            serial.PortName = (cbCom.SelectedItem)?.ToString() ?? "";
            serial.BaudRate = Convert.ToInt32((cbBaudRate.SelectedItem)?.ToString());
            serial.DataBits = serialPort.DataBits;
            serial.Parity = serialPort.Parity;
            serial.StopBits = serialPort.StopBits;

            serialPort.Dispose();
            closeListener?.Invoke();
        }
    }
}
