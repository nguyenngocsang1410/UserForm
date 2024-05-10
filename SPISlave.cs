using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserForm
{
    public class SPISlave(string Name)
    {
        public string Name { get; set; } = Name;
        public List<RegisterItem> Registers { get; set; } = GetRegisterList(Name);

        public SlaveData Info { get; set; } = slavesInfo.Find(item => item.Name == Name) ?? new();

        private static readonly List<SlaveData> slavesInfo = new()
        {
            new SlaveData(){Name = "QEC_TX1"    , Address = SlaveAddress.QEC_TX1    , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "FIR_TX1"    , Address = SlaveAddress.FIR_TX1    , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "QEC_RX1"    , Address = SlaveAddress.QEC_RX1    , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "FIR_RX1"    , Address = SlaveAddress.FIR_RX1    , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "QEC_ORX1"   , Address = SlaveAddress.QEC_ORX1   , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "FIR_ORX1"   , Address = SlaveAddress.FIR_ORX1   , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "QEC_TX2"    , Address = SlaveAddress.QEC_TX2    , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "FIR_TX2"    , Address = SlaveAddress.FIR_TX2    , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "QEC_RX2"    , Address = SlaveAddress.QEC_RX2    , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "FIR_RX2"    , Address = SlaveAddress.FIR_RX2    , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "QEC_ORX2"   , Address = SlaveAddress.QEC_ORX2   , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "FIR_ORX2"   , Address = SlaveAddress.FIR_ORX2   , RegAddressSize = 10, RegValueSize = 8},
            new SlaveData(){Name = "RX1"        , Address = SlaveAddress.RX1        , RegAddressSize = 7 , RegValueSize = 8},
            new SlaveData(){Name = "RX2"        , Address = SlaveAddress.RX2        , RegAddressSize = 7 , RegValueSize = 8},
            new SlaveData(){Name = "ORX1"       , Address = SlaveAddress.ORX1       , RegAddressSize = 7 , RegValueSize = 8},
            new SlaveData(){Name = "ORX2"       , Address = SlaveAddress.ORX2       , RegAddressSize = 7 , RegValueSize = 8},
            new SlaveData(){Name = "TX1"        , Address = SlaveAddress.TX1        , RegAddressSize = 7 , RegValueSize = 8},
            new SlaveData(){Name = "TX2"        , Address = SlaveAddress.TX2        , RegAddressSize = 7 , RegValueSize = 8},
            new SlaveData(){Name = "PLL"        , Address = SlaveAddress.PLL        , RegAddressSize = 7 , RegValueSize = 8},
            new SlaveData(){Name = "ADC1"       , Address = SlaveAddress.ADC1       , RegAddressSize = 14, RegValueSize = 32},
            new SlaveData(){Name = "ADC2"       , Address = SlaveAddress.ADC2       , RegAddressSize = 14, RegValueSize = 32},
            new SlaveData(){Name = "ADC3"       , Address = SlaveAddress.ADC3       , RegAddressSize = 14, RegValueSize = 32},
            new SlaveData(){Name = "ADC4"       , Address = SlaveAddress.ADC4       , RegAddressSize = 14, RegValueSize = 32},
            new SlaveData(){Name = "DAC1"       , Address = SlaveAddress.DAC1       , RegAddressSize = 14, RegValueSize = 32},
            new SlaveData(){Name = "DAC2"       , Address = SlaveAddress.DAC2       , RegAddressSize = 14, RegValueSize = 32},
            new SlaveData(){Name = "PHY"        , Address = SlaveAddress.PHY        , RegAddressSize = 6 , RegValueSize = 8},
            new SlaveData(){Name = "RX1_RSSI"   , Address = SlaveAddress.RX1_RSSI   , RegAddressSize = 7 , RegValueSize = 10},
            new SlaveData(){Name = "RX2_RSSI"   , Address = SlaveAddress.RX2_RSSI   , RegAddressSize = 7 , RegValueSize = 10},
            new SlaveData(){Name = "JESD204_RX ", Address = SlaveAddress.JESD204_RX , RegAddressSize = 10, RegValueSize = 32},
            new SlaveData(){Name = "JESD204_TX1", Address = SlaveAddress.JESD204_TX1, RegAddressSize = 10, RegValueSize = 32},
            new SlaveData(){Name = "JESD204_TX2", Address = SlaveAddress.JESD204_TX2, RegAddressSize = 10, RegValueSize = 32},
        };
        private static List<RegisterItem> GetRegisterList(string Name)
        {
            List<RegisterItem> regList;
            switch (Name)
            {
                case "QEC_TX1":
                case "FIR_TX1":
                case "QEC_RX1":
                case "FIR_RX1":
                case "QEC_ORX1":
                case "FIR_ORX1":
                case "QEC_TX2":
                case "FIR_TX2":
                case "QEC_RX2":
                case "FIR_RX2":
                case "QEC_ORX2":
                case "FIR_ORX2":
                case "RX1":
                case "RX2":
                case "ORX1":
                case "ORX2":
                case "TX1":
                case "TX2":
                    return [];
                case "PLL":
                    regList = [
                        new RegisterItem() {Name = "r_ref_buf",           Addr = 0},
                        new RegisterItem() {Name = "r_ref_div",           Addr = 1},
                        new RegisterItem() {Name = "r_pfd_cp",            Addr = 2},
                        new RegisterItem() {Name = "r_ext_lf",            Addr = 3},
                        new RegisterItem() {Name = "r_int_lf_cap_1",      Addr = 4},
                        new RegisterItem() {Name = "r_int_lf_cap_2",      Addr = 5},
                        new RegisterItem() {Name = "r_int_lf_cap_3",      Addr = 6},
                        new RegisterItem() {Name = "r_int_lf_res",        Addr = 7},
                        new RegisterItem() {Name = "r_vco",               Addr = 8},
                        new RegisterItem() {Name = "r_vco_ctrl_Iref",     Addr = 9},
                        new RegisterItem() {Name = "r_vco_bndset",        Addr = 10},
                        new RegisterItem() {Name = "r_afc",               Addr = 11},
                        new RegisterItem() {Name = "r_dsm",               Addr = 12},
                        new RegisterItem() {Name = "r_isynth",            Addr = 13},
                        new RegisterItem() {Name = "r_isynth",            Addr = 14},
                        new RegisterItem() {Name = "r_fsynth_1",          Addr = 15},
                        new RegisterItem() {Name = "r_fsynth_2",          Addr = 16},
                        new RegisterItem() {Name = "r_fsynth_3",          Addr = 17},
                        new RegisterItem() {Name = "r_lo_gen",            Addr = 18},
                        new RegisterItem() {Name = "r_prediv8",           Addr = 19},
                        new RegisterItem() {Name = "r_iq_gen",            Addr = 20},
                        new RegisterItem() {Name = "r_iq_tp",             Addr = 21},
                        new RegisterItem() {Name = "r_pll_tp",            Addr = 22},
                        new RegisterItem() {Name = "reserved",            Addr = 23},
                        new RegisterItem() {Name = "r_pll_lock",          Addr = 24},
                        new RegisterItem() {Name = "r_vco_bndset_read",   Addr = 25},
                        new RegisterItem() {Name = "r_vco_cap_valid",     Addr = 26}
                        ];
                    regList[0].BitName = ["r_00_b7",
                                        "r_00_b6",
                                        "r_00_b5",
                                        "r_00_b4",
                                        "r_00_b3",
                                        "ref_div_rst",
                                        "ref_div_en_n",
                                        "ref_buf_en_n"];
                    regList[1].BitName = ["ld_div_val<3>",
                                        "ld_div_val<2>",
                                        "ld_div_val<1>",
                                        "ld_div_val<0>",
                                        "ld_rst_n",
                                        "ref_div_val<2>",
                                        "ref_div_val<1>",
                                        "ref_div_val<0>"];
                    regList[2].BitName = ["r_02_b7",
                                        "r_02_b6",
                                        "cp_sel<3>",
                                        "cp_sel<2>",
                                        "cp_sel<1>",
                                        "cp_sel<0>",
                                        "cp_bias_en_1p8_n",
                                        "pfd_en_n"];
                    regList[3].BitName = ["r_03_b7",
                                        "r_03_b6",
                                        "r_03_b5",
                                        "r_03_b4",
                                        "r_03_b3",
                                        "r_03_b2",
                                        "sel_vtune",
                                        "lf_sel"];
                    regList[4].BitName = ["lf_cap_1<7>",
                                        "lf_cap_1<6>",
                                        "lf_cap_1<5>",
                                        "lf_cap_1<4>",
                                        "lf_cap_1<3>",
                                        "lf_cap_1<2>",
                                        "lf_cap_1<1>",
                                        "lf_cap_1<0>"];
                    regList[5].BitName = ["lf_cap_2<7>",
                                        "lf_cap_2<6>",
                                        "lf_cap_2<5>",
                                        "lf_cap_2<4>",
                                        "lf_cap_2<3>",
                                        "lf_cap_2<2>",
                                        "lf_cap_2<1>",
                                        "lf_cap_2<0>"];
                    regList[6].BitName = ["lf_cap_3<7>",
                                        "lf_cap_3<6>",
                                        "lf_cap_3<5>",
                                        "lf_cap_3<4>",
                                        "lf_cap_3<3>",
                                        "lf_cap_3<2>",
                                        "lf_cap_3<1>",
                                        "lf_cap_3<0>"];
                    regList[7].BitName = ["lf_res<7>",
                                        "lf_res<6>",
                                        "lf_res<5>",
                                        "lf_res<4>",
                                        "lf_res<3>",
                                        "lf_res<2>",
                                        "lf_res<1>",
                                        "lf_res<0>"];
                    regList[8].BitName = ["r_08_b7",
                                        "r_08_b6",
                                        "vco_bias_1p8_en_n",
                                        "vco_bias_0p9_en_n",
                                        "vco_lf_Iref_en_n",
                                        "vco_lf_buf_n",
                                        "vco_hf_Iref_en_n",
                                        "vco_hf_buf_n"];
                    regList[9].BitName = ["r_09_b7",
                                        "r_09_b6",
                                        "ld_gen_en_n",
                                        "vco_ctrl_Iref<4>",
                                        "vco_ctrl_Iref<3>",
                                        "vco_ctrl_Iref<2>",
                                        "vco_ctrl_Iref<1>",
                                        "vco_ctrl_Iref<0>"];
                    regList[10].BitName = ["r_0a_b7",
                                        "r_0a_b6",
                                        "vco_bndset<5>",
                                        "vco_bndset<4>",
                                        "vco_bndset<3>",
                                        "vco_bndset<2>",
                                        "vco_bndset<1>",
                                        "vco_bndset<0>"];
                    regList[11].BitName = ["r_0b_b7",
                                        "r_0b_b6",
                                        "r_0b_b5",
                                        "r_0b_b4",
                                        "r_0b_b3",
                                        "r_0b_b2",
                                        "afc_rst",
                                        "afc_start"];
                    regList[12].BitName = ["r_0c_b7",
                                        "r_0c_b6",
                                        "r_0c_b5",
                                        "r_0c_b4",
                                        "r_0c_b3",
                                        "dsm_bypass_dither",
                                        "dsm_rst",
                                        "dsm_en_n"];
                    regList[13].BitName = ["r_0d_b7",
                                        "r_0d_b6",
                                        "r_0d_b5",
                                        "dsm_int_div<4>",
                                        "dsm_int_div<3>",
                                        "dsm_int_div<2>",
                                        "dsm_int_div<1>",
                                        "dsm_int_div<0>"];
                    regList[14].BitName = ["dsm_frac_div<7>",
                                        "dsm_frac_div<6>",
                                        "dsm_frac_div<5>",
                                        "dsm_frac_div<4>",
                                        "dsm_frac_div<3>",
                                        "dsm_frac_div<2>",
                                        "dsm_frac_div<1>",
                                        "dsm_frac_div<0>"];
                    regList[15].BitName = ["dsm_frac_div<15>",
                                        "dsm_frac_div<14>",
                                        "dsm_frac_div<13>",
                                        "dsm_frac_div<12>",
                                        "dsm_frac_div<11>",
                                        "dsm_frac_div<10>",
                                        "dsm_frac_div<9>",
                                        "dsm_frac_div<8>"];
                    regList[16].BitName = ["dsm_frac_div<23>",
                                        "dsm_frac_div<22>",
                                        "dsm_frac_div<21>",
                                        "dsm_frac_div<20>",
                                        "dsm_frac_div<19>",
                                        "dsm_frac_div<18>",
                                        "dsm_frac_div<17>",
                                        "dsm_frac_div<16>"];
                    regList[17].BitName = ["r_12_b7",
                                        "r_12_b6",
                                        "r_12_b5",
                                        "r_12_b4",
                                        "r_12_b3",
                                        "r_12_b2",
                                        "dsm_frac_div<25>",
                                        "dsm_frac_div<24>"];
                    regList[18].BitName = ["lo_gen_div<5>",
                                        "lo_gen_div<4>",
                                        "lo_gen_div<3>",
                                        "lo_gen_div<2>",
                                        "lo_gen_div<1>",
                                        "lo_gen_div<0>",
                                        "lo_gen_en_n",
                                        "mux21_en_n"];
                    regList[19].BitName = ["r_14_b7",
                                        "r_14_b6",
                                        "r_14_b5",
                                        "r_14_b4",
                                        "r_14_b3",
                                        "r_14_b2",
                                        "r_14_b1",
                                        "prediv8_en_n"];
                    regList[20].BitName = ["r_15_b7",
                                        "r_15_b6",
                                        "r_15_b5",
                                        "r_15_b4",
                                        "r_15_b3",
                                        "r_15_b2",
                                        "r_15_b1",
                                        "iq_gen_en_n"];
                    regList[21].BitName = ["r_16_b7",
                                        "r_16_b6",
                                        "r_16_b5",
                                        "r_16_b4",
                                        "r_16_b3",
                                        "r_16_b2",
                                        "r_16_b1",
                                        "iq_tp_en_n"];
                    regList[22].BitName = ["r_17_b7",
                                        "r_17_b6",
                                        "r_17_b5",
                                        "r_17_b4",
                                        "r_17_b3",
                                        "r_17_b2",
                                        "r_17_b1",
                                        "pll_tp_en_n"];
                    regList[23].BitName = ["r_18_b7",
                                        "r_18_b6",
                                        "r_18_b5",
                                        "r_18_b4",
                                        "r_18_b3",
                                        "r_18_b2",
                                        "r_18_b1",
                                        "r_18_b0"];
                    regList[24].BitName = ["r_19_b7",
                                        "r_19_b6",
                                        "r_19_b5",
                                        "r_19_b4",
                                        "r_19_b3",
                                        "r_19_b2",
                                        "r_19_b1",
                                        "pll_lock"];
                    regList[25].BitName = ["r_20_b7",
                                        "r_20_b6",
                                        "vco_cal<5>",
                                        "vco_cal<4>",
                                        "vco_cal<3>",
                                        "vco_cal<2>",
                                        "vco_cal<1>",
                                        "vco_cal<0>"];
                    regList[26].BitName = ["r_21_b7",
                                        "r_21_b6",
                                        "r_21_b5",
                                        "r_21_b4",
                                        "r_21_b3",
                                        "r_21_b2",
                                        "r_21_b1",
                                        "vco_cap_valid"];
                    return regList;
                case "ADC1":
                case "ADC2":
                case "ADC3":
                case "ADC4":
                case "DAC1":
                case "DAC2":
                case "PHY":
                    return [];
                case "RX1_RSSI":
                case "RX2_RSSI":
                    regList = [
                        new RegisterItem() { Name = "rssi_rcalib", Addr = 0 },
                        new RegisterItem() { Name = "rssi_bgr_calib", Addr = 1 },
                        new RegisterItem() { Name = "rssi_pd", Addr = 2 },
                        new RegisterItem() { Name = "freq_div_rst", Addr = 3 },
                        new RegisterItem() { Name = "div_sel", Addr = 4 },
                        new RegisterItem() { Name = "adc_ext_rst", Addr = 5 },
                        new RegisterItem() { Name = "adc_bgr_calib", Addr = 6 },
                        new RegisterItem() { Name = "adc_pd", Addr = 7 },
                        new RegisterItem() { Name = "RESERVED", Addr = 8 },
                        new RegisterItem() { Name = "adc_dout", Addr = 9 },
                    ];
                    regList[0].BitName = ["rssi_rcalib<9>", "rssi_rcalib<8>", "rssi_rcalib<7>",
                                          "rssi_rcalib<6>", "rssi_rcalib<5>", "rssi_rcalib<4>",
                                          "rssi_rcalib<3>", "rssi_rcalib<2>", "rssi_rcalib<1>", "rssi_rcalib<0>"];
                    regList[1].BitName = ["rssi_bgr_calib<9>", "rssi_bgr_calib<8>", "rssi_bgr_calib<7>",
                                          "rssi_bgr_calib<6>", "rssi_bgr_calib<5>", "rssi_bgr_calib<4>",
                                          "rssi_bgr_calib<3>", "rssi_bgr_calib<2>", "rssi_bgr_calib<1>", "rssi_bgr_calib<0>"];
                    regList[2].BitName = ["rssi_pd<9>", "rssi_pd<8>", "rssi_pd<7>",
                                          "rssi_pd<6>", "rssi_pd<5>", "rssi_pd<4>",
                                          "rssi_pd<3>", "rssi_pd<2>", "rssi_pd<1>", "rssi_pd<0>"];
                    regList[3].BitName = ["freq_div_rst<9>", "freq_div_rst<8>", "freq_div_rst<7>",
                                          "freq_div_rst<6>", "freq_div_rst<5>", "freq_div_rst<4>",
                                          "freq_div_rst<3>", "freq_div_rst<2>", "freq_div_rst<1>", "freq_div_rst<0>"];
                    regList[4].BitName = ["div_sel<9>", "div_sel<8>", "div_sel<7>",
                                          "div_sel<6>", "div_sel<5>", "div_sel<4>",
                                          "div_sel<3>", "div_sel<2>", "div_sel<1>", "div_sel<0>"];
                    regList[5].BitName = ["adc_ext_rst<9>", "adc_ext_rst<8>", "adc_ext_rst<7>",
                                          "adc_ext_rst<6>", "adc_ext_rst<5>", "adc_ext_rst<4>",
                                          "adc_ext_rst<3>", "adc_ext_rst<2>", "adc_ext_rst<1>", "adc_ext_rst<0>"];
                    regList[6].BitName = ["adc_bgr_calib<9>", "adc_bgr_calib<8>", "adc_bgr_calib<7>",
                                          "adc_bgr_calib<6>", "adc_bgr_calib<5>", "adc_bgr_calib<4>",
                                          "adc_bgr_calib<3>", "adc_bgr_calib<2>", "adc_bgr_calib<1>", "adc_bgr_calib<0>"];
                    regList[7].BitName = ["adc_pd<9>", "adc_pd<8>", "adc_pd<7>",
                                          "adc_pd<6>", "adc_pd<5>", "adc_pd<4>",
                                          "adc_pd<3>", "adc_pd<2>", "adc_pd<1>", "adc_pd<0>"];
                    regList[8].BitName = ["NoConn", "NoConn", "NoConn",
                                          "NoConn", "NoConn", "NoConn",
                                          "NoConn", "NoConn", "NoConn", "NoConn"];
                    regList[9].BitName = ["adc_dout<9>", "adc_dout<8>", "adc_dout<7>",
                                          "adc_dout<6>", "adc_dout<5>", "adc_dout<4>",
                                          "adc_dout<3>", "adc_dout<2>", "adc_dout<1>", "adc_dout<0>"];
                    return regList;
                case "JESD204_RX":
                case "JESD204_TX1":
                case "JESD204_TX2":
                default:
                    return [];
            }
        }
        public List<RegisterItem> CopyRegister()
        {
            List<RegisterItem> retList = GetRegisterList(Name);

            foreach (RegisterItem reg in retList)
            {
                reg.Value = Registers.Find(item => item.Addr == reg.Addr)?.Value ?? 0;
            }

            return retList;
        }
    }


    public class RegisterItem
    {
        public string Name { get; set; } = "";
        public int Addr { get; set; } = 0;
        public ushort Value { get; set; } = 0;
        public bool[] BitValue { get; set; } = [];
        public string[] BitName { get; set; } = [];
    }
    public class SlaveData
    {
        public string Name { get; set; } = "";
        public int Address { get; set; }
        public int RegAddressSize { get; set; }
        public int RegValueSize { get; set; }
    }
    public static class SlaveAddress
    {
        public const int QEC_TX1 = 0x01;
        public const int FIR_TX1 = 0x02;
        public const int QEC_RX1 = 0x03;
        public const int FIR_RX1 = 0x04;
        public const int QEC_ORX1 = 0x05;
        public const int FIR_ORX1 = 0x06;
        public const int QEC_TX2 = 0x07;
        public const int FIR_TX2 = 0x08;
        public const int QEC_RX2 = 0x09;
        public const int FIR_RX2 = 0x0A;
        public const int QEC_ORX2 = 0x0B;
        public const int FIR_ORX2 = 0x0C;
        public const int RX1 = 0x0D;
        public const int RX2 = 0x0E;
        public const int ORX1 = 0x0F;
        public const int ORX2 = 0x10;
        public const int TX1 = 0x11;
        public const int TX2 = 0x12;
        public const int PLL = 0x13;
        public const int ADC1 = 0x14;
        public const int ADC2 = 0x15;
        public const int ADC3 = 0x16;
        public const int ADC4 = 0x17;
        public const int DAC1 = 0x18;
        public const int DAC2 = 0x19;
        public const int PHY = 0x1A;
        public const int RX1_RSSI = 0x1B;
        public const int RX2_RSSI = 0x1C;
        public const int JESD204_RX = 0x1D;
        public const int JESD204_TX1 = 0x1E;
        public const int JESD204_TX2 = 0x1F;
        public const int NA = 0xFF;
    }


}
