using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserForm
{
    public class Serial : SerialPort
    {
        public string Name = "Serial";
        public Queue<DataPackage> pendingPackages = new();
        public Serial()
        {
            DataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.One;
            DtrEnable = true;
            RtsEnable = true;
            DataReceived += new SerialDataReceivedEventHandler(GetResponse);
        }
        public Serial(string name) : this()
        {
            Name = name;
        }
        public void SetConfig(string? portName, string? baudRate)
        {
            SetConfig(portName, baudRate, "8", "None", "1");
        }
        public void SetConfig(string? portName, string? baudRate, string dataBit, string checkBit, string stopBit)
        {
            PortName = portName;

            int iBaudRate = Convert.ToInt32(baudRate);
            int iDataBit = Convert.ToInt32(dataBit);
            BaudRate = iBaudRate;
            DataBits = iDataBit;

            switch (stopBit)
            {
                case "1":
                    StopBits = StopBits.One;
                    break;
                case "1.5":
                    StopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    StopBits = StopBits.Two;
                    break;
                default:
                    break;
            }
            switch (checkBit)
            {
                case "None":
                    Parity = Parity.None;
                    break;
                case "Odd":
                    Parity = Parity.Odd;
                    break;
                case "Even":
                    Parity = Parity.Even;
                    break;
                default:
                    break;
            }

        }

        // TODO: 
        // Push to buffer
        // Add timeout to remove from buffer
        // Cancel timeout clock if packet receive
        public void SendRequest(SpiSlave? slave, SendCMD writeEn, int regAddr, int regValue, Action<DataPackage>? action = null)
        {
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
                SlaveAddr = slave.Info.Address,
                WriteEn = writeEn
            };

            byte[] packetSend = dataSend.CreatePackage().GetData();

            Write(packetSend, 0, packetSend.Length);

            action?.Invoke(dataSend);

            pendingPackages.Enqueue(dataSend);

            new Thread(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                lock (pendingPackages)
                {
                    pendingPackages.Dequeue();
                }
            }).Start();

            return;
        }
        private void GetResponse(object sender, SerialDataReceivedEventArgs e)
        {
            if (IsOpen && BytesToRead >= 18)
            {
                try
                {
                    byte[] values = new byte[18];

                    Read(values, 0, 18);

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
    }
}

