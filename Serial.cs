using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserForm
{
    public class Serial
    {
        public string Name = "Serial";
        public SerialPort port;

        public char[] receiveBuffer = [];

        public Serial()
        {
            port = new SerialPort()
            {
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
            };
        }

        public Serial(string name) : this()
        {
            Name = name;
        }
        public void SetConfig(string portName, string baudRate, string dataBit, string checkBit, string stopBit)
        {
            port.PortName = portName;

            int iBaudRate = Convert.ToInt32(baudRate);
            int iDataBit  = Convert.ToInt32(dataBit);
            port.BaudRate = iBaudRate;
            port.DataBits = iDataBit;

            switch (stopBit)
            {
                case "1":
                    port.StopBits = StopBits.One;
                    break;
                case "1.5":
                    port.StopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    port.StopBits = StopBits.Two;
                    break;
                default:
                    break;
            }
            switch (checkBit)
            {
                case "None":
                    port.Parity = Parity.None;
                    break;
                case "Odd":
                    port.Parity = Parity.Odd;
                    break;
                case "Even":
                    port.Parity = Parity.Even;
                    break;
                default:
                    break;
            }

        }
        public void Connect()
        {
            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                throw;
            }
        }
        public void DisConnect()
        {
            port.Close();
        }
    }
}

