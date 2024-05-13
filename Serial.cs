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
        //public SerialPort port;

        public Serial()
        {
            DataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.One;
        }

        public Serial(string name) : this()
        {
            Name = name;
        }
        public void SetConfig(string portName, string baudRate, string dataBit, string checkBit, string stopBit)
        {
            PortName = portName;

            int iBaudRate = Convert.ToInt32(baudRate);
            int iDataBit  = Convert.ToInt32(dataBit);
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
        public void Connect()
        {
            try
            {
                Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                throw;
            }
        }
        public void DisConnect()
        {
            Close();
        }
    }
}

