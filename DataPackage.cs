﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserForm
{
    public class DataPackage
    {
        public int SlaveAddr { get; set; } = SlaveAddress.NA;
        public int ID { get; set; }
        public SendCMD WriteEn { get; set; } = SendCMD.NA;
        public int RegAddr { get; set; } = 0;
        public int RegAddrSize { get; set; } = 0;
        public int RegValue { get; set; } = 0;
        public int RegValueSize { get; set; } = 0;
        public string StatusCode { get; set; } = "";
        public byte[] Data { get; set; } = [];
        public byte Header = 0;
        public static readonly byte PKG_HEADER = 0x0A;
        public static readonly byte PKG_HEADER_ACK = 0xA0;

        public DataPackage CreatePackage()
        {
            // [HEADER:1][LEN:2][CID:1][SLAVE A:1][REG A:4][REG A S:1][REG V S:1][Number of regs:4][CRC:2]
            // [1 + 2]*0 + 1 + 1 + 4 + 1 + 4 + 1 + 2 
            int frmLen = 17;
            int cmdLen = frmLen - 1 - 2;
            byte[] package = new byte[frmLen];
            int checkSum = 0;
            int idx = 0;

            // Frame Header 1 byte
            Header = PKG_HEADER;
            package[idx++] = Header;

            // Frame length 2 bytes
            package[idx++] = (byte)(cmdLen & 0xFF);
            package[idx++] = (byte)((cmdLen >> 8) & 0xFF);

            // Command ID 1 byte
            package[idx++] = (byte)(WriteEn);

            // Slave address 1 byte
            package[idx++] = (byte)(SlaveAddr & 0xFF);

            // Reg address 4 bytes

            package[idx++] = (byte)(RegAddr & 0xFF);
            package[idx++] = (byte)((RegAddr >> 8) & 0xFF);
            package[idx++] = (byte)((RegAddr >> 16) & 0xFF);
            package[idx++] = (byte)((RegAddr >> 24) & 0xFF);

            // Reg address size 1 byte
            package[idx++] = (byte)(RegAddrSize & 0xFF);

            if (WriteEn == SendCMD.Write || WriteEn == SendCMD.WriteAck)
            {
                // Reg data 4 bytes
                package[idx++] = (byte)(RegValue & 0xFF);
                package[idx++] = (byte)((RegValue >> 8) & 0xFF);
                package[idx++] = (byte)((RegValue >> 16) & 0xFF);
                package[idx++] = (byte)((RegValue >> 24) & 0xFF);
                // Reg data size 1 byte
                package[idx++] = (byte)(RegValueSize & 0xFF);
            }
            else
            {
                package[idx++] = (byte)(RegValueSize & 0xFF);

                uint numRegs = 1;

                package[idx++] = (byte)(numRegs & 0xFF);
                package[idx++] = (byte)((numRegs >> 8) & 0xFF);
                package[idx++] = (byte)((numRegs >> 16) & 0xFF);
                package[idx++] = (byte)((numRegs >> 24) & 0xFF);
            }

            // Check sum 2 bytes
            for (int i = 0; i < frmLen - 2; i++)
            {
                checkSum += package[i];
            }
            package[idx++] = (byte)(checkSum & 0xFF);
            package[idx++] = (byte)((checkSum >> 8) & 0xFF);

            if (idx != frmLen)
            {
                throw new Exception("Wrong frame");
            }

            Data = package;
            ID = checkSum;
            return this;
        }
        public byte[] GetData() { return Data; }
        public static DataPackage? TryParse(byte[] package)
        {
            int frmLen = 18; // + status byte
            int cmdLen = frmLen - 1 - 2;

            if (package.Length != frmLen)
                throw new Exception("Invalid data package");

            ushort checkSum = 0;
            for (int i = 0; i < frmLen - 2; i++)
            {
                checkSum += package[i];
            }
            byte header = package[0];
            int cmdLenRead = package[1] + (package[2] << 8);
            int writeEnRead = package[3];
            int slaveAddrRead = package[4];

            byte[] responseData = package[5..(frmLen - 2)];

            // Parse data
            int regAddrRead = responseData[0] + (responseData[1] << 8)
                              + (responseData[2] << 16) + (responseData[3] << 24);
            int regAddrSizeRead = responseData[4];

            int regValueSizeRead = responseData[5];
            int regValueRead = responseData[6] + (responseData[7] << 8)
                              + (responseData[8] << 16) + (responseData[9] << 24);

            byte statusRead = responseData[10];

            ushort csumRead = (ushort)((package[frmLen - 2] & 0xFF) + ((package[frmLen - 1] << 8) & 0xFF00));

            string err;
            if (header != PKG_HEADER_ACK)
                err = "0x10";
            else if (cmdLenRead != cmdLen)
                err = "0x11";
            //else if ((write_en && package[3] != 0x03) || (!write_en && package[3] != 0x02))
            //    err = "0x12";
            //else if (package[4] != slave.slaveAddr)
            //    err = "0x12";
            //else if (regAddrRead != regAddr)
            //    err = "0x12";
            //else if (package[4] != slave.RegAddressSize)
            //    err = "0x12";
            //else if (write_en & (regValueRead != regValue))
            //    err = "0x12";
            //else if (package[9] != slave.RegValueSize)
            //    err = "0x12";
            //else if (package[10] != 0)
            //    err = Convert.ToHexString([package[10]]);
            else if (0 != csumRead)
                err = "0x13";
            else
                err = "0";

            if (err != "0")
            {
                ErrorCode(err);
                return null;
            }
            return new DataPackage()
            {
                ID = checkSum,
                Header = header,
                SlaveAddr = slaveAddrRead,
                WriteEn = (SendCMD)writeEnRead,
                RegAddr = regAddrRead,
                RegAddrSize = regAddrSizeRead,
                RegValue = regValueRead,
                RegValueSize = regValueSizeRead,
                StatusCode = Convert.ToHexString([statusRead]),
            };
        }
        public static void ErrorCode(string error)
        {
            switch (error)
            {

                case "0":
                    MessageBox.Show("No Error");
                    break;
                case "0x10":
                    MessageBox.Show("Invalid frame");
                    break;
                case "0x11":
                    MessageBox.Show("Invalid length");
                    break;
                case "0x12":
                    MessageBox.Show("Invalid Command");
                    break;
                case "0x13":
                    MessageBox.Show("Invalid CRC");
                    break;
                case "0x14":
                    MessageBox.Show("Command execution failed");
                    break;
                case "0x15":
                    MessageBox.Show("Command timeout");
                    break;
                case "0xFF":
                    MessageBox.Show("Unknown error");
                    break;
                default: break;
            }
        }
    }
    public enum SendCMD
    {
        Write = 0x01,
        WriteAck = 0x02,
        Read = 0x03,
        AutoRead = 0x04,
        NA = 0x00,
    }

}
