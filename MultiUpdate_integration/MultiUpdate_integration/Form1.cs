using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MultiUpdate_integration;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //todo: C#取得主程式路徑(Application Path)
        string appPATH = System.Windows.Forms.Application.StartupPath;

        short chg_ip;   // 1 or 2
        public const int UDPport = 55954;
        Process proc;
        UdpClient udpClient;
        Thread udpRcvThread;
        string selectFW, linuxEXE;
        string targetModel, targetIP, targetKer, targetAP;

        public class Global
        {
            public static int Device_Max = 599;
            public static int DevCount;
            public static int DevSetCount;
            public static int DevDefaultCount;
            public static int DevUpgradeCount;
            public static string[,] DA2 = new string[Device_Max, 24];
            public static int localendport;
        }

        private int Shell(string FilePath, string FileName)
        {
            try
            {
                ////////////////////// like VB 【shell】 ///////////////////////
                //System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                proc.EnableRaisingEvents = false;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.FileName = FilePath + "\\" + FileName;
                proc.Start();
                return proc.Id;
                ////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ' " + FileName + " ' ", "Shell error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private void CloseShell(int pid)
        {
            //if (!Process.GetProcessById(pid).HasExited)
            //{
            //    // Close process by sending a close message to its main window.
            //    Process.GetProcessById(pid).CloseMainWindow();
            //    Process.GetProcessById(pid).WaitForExit(3000);
            //}
            if (!Process.GetProcessById(pid).HasExited)
            {
                Process.GetProcessById(pid).Kill();
                Process.GetProcessById(pid).WaitForExit(1000);
            }
        }

        private void CleanARP()
        {
            try
            {
                Shell(appPATH, "arp-d.bat");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CleanARP error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Hold(long timeout)
        {
            bool tmp_Hold = true;
            long delay = 0;
            if (timeout > 0) { delay = timeout / 10; }
            while (true)
            {
                Application.DoEvents();
                Thread.Sleep(10);
                if (timeout > 0)
                {
                    if (delay > 0)
                    {
                        delay -= 1;
                    }
                    else
                    {
                        tmp_Hold = false;   // 時間等到底
                        break;
                    }
                }
            }
            return tmp_Hold;
        }

        /// <summary>
        /// 驗證網段(Network segment)
        /// </summary>
        /// <param name="source"></param>
        /// <returns>規則運算式尋找到符合項目，則為 true，否則為 false</returns>
        public static bool IsNetworkSegment(string source)
        {
            // Regex.IsMatch 方法 (String, String, RegexOptions)
            // 表示指定的規則運算式是否使用指定的比對選項，在指定的輸入字串中尋找相符項目
            //return Regex.IsMatch(source, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
            return Regex.IsMatch(source, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 驗證IP
        /// </summary>
        /// <param name="source"></param>
        /// <returns>規則運算式尋找到符合項目，則為 true，否則為 false</returns>
        public static bool IsIP(string source)
        {
            // Regex.IsMatch 方法 (String, String, RegexOptions)
            // 表示指定的規則運算式是否使用指定的比對選項，在指定的輸入字串中尋找相符項目
            return Regex.IsMatch(source, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
        }

        public static bool HasIP(string source)
        {
            return Regex.IsMatch(source, @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 驗證 Model name
        /// </summary>
        /// <param name="source"></param>
        /// <returns>規則運算式尋找到符合項目，則為 true，否則為 false</returns>
        public static bool IsModelName(string source)
        {
            return Regex.IsMatch(source, @"[A-Z]{1}[A-Z]{1}[0-9]{1}(0|3|4|5|6|7|8|9){1}[0-9]{1}", RegexOptions.None); // ex: SE1902、SW550X
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string fileName;
            if (Directory.Exists(appPATH + @"\FW_Version"))
            {
                var files = Directory.GetFiles(appPATH + @"\FW_Version", "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".dld")
                    || s.EndsWith(".bin") || s.EndsWith(".hex") || s.EndsWith(".HEX") || s.EndsWith(".rom"));
                foreach (var obj in files)
                {
                    fileName = obj.Substring(obj.LastIndexOf("\\") + 1);
                    lstFilesBox.Items.Add(fileName);
                }
            }

            cmdInvite_Click(null, null);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cmdInvite_Click(object sender, EventArgs e)
        {
            lstDev.Items.Clear();
            txtDevCnt.Text = string.Empty;
            Global.DevCount = 0;

            inviteTmr.Enabled = false;

            int i, j;
            for (j = 0; j < Global.Device_Max; j++)
            {
                for (i = 0; i < 24; i++)
                {
                    Global.DA2[j, i] = string.Empty;
                }
            }

            locateTmr.Enabled = false;
            cmdLocate.Enabled = false;
            cmdLocate.BackColor = SystemColors.Control;
            cmdLocate.UseVisualStyleBackColor = true;
            cmdWeb.Enabled = false;

            try
            {
                byte[] bdata = new byte[300];
                bdata[0] = 2;
                bdata[1] = 1;
                bdata[2] = 6;
                bdata[4] = Convert.ToByte(Convert.ToUInt64("92", 16)); //將十六進制轉換為十進制
                bdata[5] = Convert.ToByte(Convert.ToUInt64("DA", 16));
                IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Broadcast, UDPport);   // 目標 port
                // 背景接收執行緒
                if (udpRcvThread == null || !udpRcvThread.IsAlive)
                {
                    udpRcvThread = new Thread(new ThreadStart(ReceiveBroadcast));
                    udpRcvThread.IsBackground = true;
                    udpRcvThread.Priority = ThreadPriority.Highest;
                    udpRcvThread.Start();
                }

                if (!PortInUse(UDPport))  // 判斷 local port 是否占用中
                {
                    udpClient = new UdpClient(UDPport); // 未使用就綁定 local port
                    udpClient.Send(bdata, bdata.Length, ipEndpoint);
                }
                else
                {
                    udpClient.Send(bdata, bdata.Length, ipEndpoint);
                }

                inviteTmr.Interval = 3000;
                inviteTmr.Enabled = true;
                inviteTmr_Tick(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("請關掉 Serial Manager、Monitor、Device Manager 之類的程式 !\n" + "\n" + ex.StackTrace, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// todo: 判斷指定的本機 Udp 端口（只判斷端口）是否被占用
        /// </summary>
        public bool PortInUse(int port)
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveUdpListeners();
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    return true;    // 占用中
                }
            }
            return false;
        }

        public void ReceiveBroadcast()
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, UDPport);
                aLabel:
                while (true)    // 條件永遠成立，是無窮迴圈
                {
                    byte[] bytes = udpClient.Receive(ref ip);
                    if ((bytes[0] == 1 || bytes[0] == 3) && bytes[4] == Convert.ToByte(Convert.ToUInt64("92", 16)) && bytes[5] == Convert.ToByte(Convert.ToUInt64("DA", 16)))
                    {
                        string tmpstr = string.Empty;
                        int idx, i;

                        // get IP
                        string tmpIP = Convert.ToString(bytes[12] + "." + bytes[13] + "." + bytes[14] + "." + bytes[15]);
                        string displayIP = tmpIP;
                        for (i = tmpIP.Length; i <= 15; i++)
                        {
                            displayIP = displayIP + " ";
                        }
                        // get Netmask
                        string tmpNetmask = Convert.ToString(bytes[236] + "." + bytes[237] + "." + bytes[238] + "." + bytes[239]);
                        // get MAC
                        string tmpMAC = TenToSixteen(bytes, 28, 6).ToUpper();
                        // get Model
                        string tmpModel = DecToChar(bytes, 44, 16);
                        // get Kernel
                        string tmpKernel = "[Kernel]:v" + Convert.ToString(bytes[109] + "." + bytes[108]);
                        // get AP version
                        tmpstr = DecToChar(bytes, 110, 125);
                        string tmpAP = "[AP]:" + tmpstr;

                        if (chktargetModel.Checked)     // 指定 Model
                        {
                            if (targetModel != null)
                            {
                                if (!tmpModel.ToUpper().Contains(targetModel))
                                {
                                    goto aLabel;
                                }
                            }
                        }
                        else if (chktargetIP.Checked)   // 指定 IP
                        {
                            if (targetIP != null)
                            {
                                if (!tmpIP.Contains(targetIP))
                                {
                                    goto aLabel;
                                }
                            }
                        }

                        if (chktargetKer.Checked)     // 指定 Kernel
                        {
                            if (targetKer != null)
                            {
                                if (!tmpKernel.ToUpper().Contains(targetKer))
                                {
                                    goto aLabel;
                                }
                            }
                        }

                        if (chktargetAP.Checked)     // 指定 AP
                        {
                            if (targetAP != null)
                            {
                                if (!tmpAP.ToUpper().Contains(targetAP))
                                {
                                    goto aLabel;
                                }
                            }
                        }

                        // 比對有無重複的資料
                        for (idx = 0; idx < Global.DevCount; idx++)
                        {
                            if (string.Equals(Global.DA2[idx, 2], tmpMAC)) // MAC already recorded
                            {
                                if (string.Equals(Global.DA2[idx, 0], tmpIP))
                                {
                                    goto aLabel;    // IP is the same then not necessary to be updated
                                }
                            }
                        }

                        if (idx == Global.DevCount)
                        {
                            if (Global.DevCount >= 300)
                            {
                                break;
                            }  // break while
                        }

                        Global.DA2[idx, 0] = tmpIP;
                        Global.DA2[idx, 1] = tmpNetmask;
                        Global.DA2[idx, 2] = tmpMAC;
                        // IP
                        Global.DA2[idx, 3] = Convert.ToString(bytes[12]);
                        Global.DA2[idx, 4] = Convert.ToString(bytes[13]);
                        Global.DA2[idx, 5] = Convert.ToString(bytes[14]);
                        Global.DA2[idx, 6] = Convert.ToString(bytes[15]);
                        // Model name
                        Global.DA2[idx, 7] = tmpModel;
                        Global.DA2[idx, 8] = string.Empty;
                        //Global.DA2[i, 8] = CStr(ProcStep);
                        // MAC
                        Global.DA2[idx, 9] = Convert.ToString(bytes[28]);
                        Global.DA2[idx, 10] = Convert.ToString(bytes[29]);
                        Global.DA2[idx, 11] = Convert.ToString(bytes[30]);
                        Global.DA2[idx, 12] = Convert.ToString(bytes[31]);
                        Global.DA2[idx, 13] = Convert.ToString(bytes[32]);
                        Global.DA2[idx, 14] = Convert.ToString(bytes[33]);
                        // Netmask
                        Global.DA2[idx, 15] = Convert.ToString(bytes[236]);
                        Global.DA2[idx, 16] = Convert.ToString(bytes[237]);
                        Global.DA2[idx, 17] = Convert.ToString(bytes[238]);
                        Global.DA2[idx, 18] = Convert.ToString(bytes[239]);
                        // last 2 bytes of Gateway
                        Global.DA2[idx, 19] = Convert.ToString(bytes[26]);
                        Global.DA2[idx, 20] = Convert.ToString(bytes[27]);
                        // status
                        Global.DA2[idx, 21] = string.Empty;
                        // Kernel
                        Global.DA2[idx, 22] = tmpKernel;
                        // AP
                        Global.DA2[idx, 23] = tmpAP;

                        // Devices quantity counter
                        Global.DevCount += 1;

                        string stringData = Global.DA2[idx, 2] + ", " + displayIP + ", " + Global.DA2[idx, 7] + ", " + Global.DA2[idx, 22] + ", " + Global.DA2[idx, 23];
                        mylstDev(stringData, lstDev);
                        myText(Global.DevCount.ToString(), txtDevCnt);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 1.委派delegate 宣告
        public delegate void myListBoxCallBack(string Str, ListBox ctl);

        // 2.設計delegate可供媒介的功能
        public void mylstDev(string Str, ListBox ctl)
        {
            if (this.InvokeRequired)
            {
                // 3.建構delegate型別的物件
                myListBoxCallBack myUpdate = new myListBoxCallBack(mylstDev);
                this.Invoke(myUpdate, Str, ctl);
            }
            else
            {
                ctl.Items.Add(Str);
            }
        }

        public delegate void myUICallBack(string Str, Control ctl);

        public void myText(string Str, Control ctl)
        {
            if (this.InvokeRequired)
            {
                myUICallBack myUpdate = new myUICallBack(myText);
                this.Invoke(myUpdate, Str, ctl);
            }
            else
            {
                ctl.Text = Str;
            }
        }

        public string TenToSixteen(byte[] bdata, int Start, int Count)
        {
            int i;
            string op_str = string.Empty;
            string tmpstr = string.Empty;
            int EndNum = Start + Count;
            for (i = Start; i < EndNum; i++)
            {
                tmpstr = Convert.ToString(bdata[i], 16); //十進制轉十六進制,Convert.ToString(166, 16));
                if (tmpstr.Length < 2)
                {
                    tmpstr = "0" + tmpstr;
                }
                op_str = op_str + tmpstr;
            }
            return op_str;
        }

        public string DecToChar(byte[] bdata, int Start, int Count)
        {
            int i;
            string op_str = string.Empty;
            int EndNum = Start + Count;
            for (i = Start; i < EndNum; i++)
            {
                short ch = bdata[i];
                if (ch == 0)
                {
                    break;
                } // break for
                op_str = op_str + Convert.ToChar(ch); // vb6,ChrW() => c#,Convert.ToChar()
            }
            return op_str;
        }

        private void lstDev_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstDev.SelectedIndex >= 0)
            {
                cmdLocate.Enabled = true;
                cmdWeb.Enabled = true;
            }
            else
            {
                cmdLocate.Enabled = false;
                locateTmr.Enabled = false;
                cmdWeb.Enabled = false;
            }
        }

        private void cmdLocate_Click(object sender, EventArgs e)
        {
            if (lstDev.SelectedIndex >= 0)
            {
                cmdLocate.Enabled = true;
            }
            else
            {
                cmdLocate.Enabled = false;
                return;
            }

            // locateTmr ON/OFF
            if (locateTmr.Enabled)
            {
                //cmdLocate.BackColor = Color.FromArgb(255, 255, 192);
                //cmdLocate.BackColor = Color.Gray;
                cmdLocate.BackColor = SystemColors.Control;
                cmdLocate.UseVisualStyleBackColor = true;
                locateTmr.Enabled = false;
                return;
            }
            cmdLocate.BackColor = Color.LightGreen;

            locateTmr.Interval = 5000;
            locateTmr.Enabled = true;
            locateTmr_Tick(null, null);
        }

        private void locateTmr_Tick(object sender, EventArgs e)
        {
            string getStr;
            string[] splitStr;
            string[] ipStr;
            string mac1, mac2, mac3, mac4, mac5, mac6, ip1, ip2, ip3, ip4;
            byte[] bdata = new byte[300];
            getStr = lstDev.SelectedItem.ToString();
            if (getStr == string.Empty)
            {
                cmdLocate.Enabled = false;
                return;
            }
            splitStr = getStr.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            mac1 = splitStr[0].Substring(0, 2);
            mac2 = splitStr[0].Substring(2, 2);
            mac3 = splitStr[0].Substring(4, 2);
            mac4 = splitStr[0].Substring(6, 2);
            mac5 = splitStr[0].Substring(8, 2);
            mac6 = splitStr[0].Substring(10, 2);
            ipStr = splitStr[1].Trim().Split('.');
            ip1 = ipStr[0];
            ip2 = ipStr[1];
            ip3 = ipStr[2];
            ip4 = ipStr[3];

            using (Socket udpLocate = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                udpLocate.EnableBroadcast = true;
                udpLocate.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Broadcast, UDPport);
                bdata[0] = 7;
                bdata[1] = 1;
                bdata[2] = 6;
                bdata[4] = Convert.ToByte(Convert.ToUInt64("92", 16)); //將十六進制轉換為十進制
                bdata[5] = Convert.ToByte(Convert.ToUInt64("DA", 16));
                bdata[12] = Convert.ToByte(ip1);
                bdata[13] = Convert.ToByte(ip2);
                bdata[14] = Convert.ToByte(ip3);
                bdata[15] = Convert.ToByte(ip4);
                bdata[28] = Convert.ToByte(Convert.ToUInt64(mac1, 16));
                bdata[29] = Convert.ToByte(Convert.ToUInt64(mac2, 16));
                bdata[30] = Convert.ToByte(Convert.ToUInt64(mac3, 16));
                bdata[31] = Convert.ToByte(Convert.ToUInt64(mac4, 16));
                bdata[32] = Convert.ToByte(Convert.ToUInt64(mac5, 16));
                bdata[33] = Convert.ToByte(Convert.ToUInt64(mac6, 16));

                udpLocate.SendTo(bdata, ipEndpoint);
            }
        }

        private void cmdChangIP1_Click(object sender, EventArgs e)
        {
            int i;
            Global.DevSetCount = 0;

            inviteTmr.Enabled = false;

            try
            {
                chg_ip = 1;

                for (i = 0; i < Global.DevCount; i++)
                {
                    if (Global.DA2[i, 5] != Global.DA2[i, 13] && Global.DA2[i, 6] == Global.DA2[i, 14])
                    {
                        Global.DevSetCount = Global.DevSetCount + 1;
                    }
                    else
                    {
                        SendConfig(i);
                        Global.DevSetCount = Global.DevSetCount + 1;
                    }
                }
                this.txtDevSetCnt1.Text = Convert.ToString(Global.DevSetCount);
                CleanARP();
                Hold(1000);
                //cmdInvite_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SendConfig(int ndx)
        {
            byte[] bdata = new byte[300];
            using (Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                udpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Broadcast, UDPport);

                // configure IP
                bdata[0] = 0;
                bdata[1] = 1;
                bdata[2] = 6;
                bdata[4] = Convert.ToByte(Convert.ToUInt64("92", 16)); //將十六進制轉換為十進制
                bdata[5] = Convert.ToByte(Convert.ToUInt64("DA", 16));
                // old IP
                bdata[12] = Convert.ToByte(Global.DA2[ndx, 3]);
                bdata[13] = Convert.ToByte(Global.DA2[ndx, 4]);
                bdata[14] = Convert.ToByte(Global.DA2[ndx, 5]);
                bdata[15] = Convert.ToByte(Global.DA2[ndx, 6]);
                // old MAC
                bdata[28] = Convert.ToByte(Global.DA2[ndx, 9]);
                bdata[29] = Convert.ToByte(Global.DA2[ndx, 10]);
                bdata[30] = Convert.ToByte(Global.DA2[ndx, 11]);
                bdata[31] = Convert.ToByte(Global.DA2[ndx, 12]);
                bdata[32] = Convert.ToByte(Global.DA2[ndx, 13]);
                bdata[33] = Convert.ToByte(Global.DA2[ndx, 14]);

                // for ATC lan1 ip: 192.168.0.1、10.0.50.100、10.100.100.1、12.100.100.20、10.100.100.20、100.100.100.17
                switch (Global.DA2[ndx, 3] + Global.DA2[ndx, 4])
                {
                    case "192168":
                        if (chg_ip == 1)
                        {
                            // new IP
                            bdata[16] = 192;
                            bdata[17] = 168;
                            bdata[18] = 0;
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 192;
                            bdata[25] = 168;
                            bdata[26] = 0;
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 255;
                            bdata[239] = 0;
                        }
                        else
                        {
                            // new IP
                            bdata[16] = 192;
                            bdata[17] = 168;
                            bdata[18] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 192;
                            bdata[25] = 168;
                            bdata[26] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 255;
                            bdata[239] = 0;
                        }
                        break;
                    case "100":
                        if (chg_ip == 1)
                        {
                            // new IP
                            bdata[16] = 10;
                            bdata[17] = 0;
                            bdata[18] = 50;
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 10;
                            bdata[25] = 0;
                            bdata[26] = 0;
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 0;
                            bdata[239] = 0;
                        }
                        else
                        {
                            // new IP
                            bdata[16] = 10;
                            bdata[17] = 0;
                            bdata[18] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 10;
                            bdata[25] = 0;
                            bdata[26] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 0;
                            bdata[239] = 0;
                        }
                        break;
                    case "10100":
                        if (chg_ip == 1)
                        {
                            // new IP
                            bdata[16] = 10;
                            bdata[17] = 100;
                            bdata[18] = 100;
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 10;
                            bdata[25] = 100;
                            bdata[26] = 100;
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 255;
                            bdata[239] = 0;
                        }
                        else
                        {
                            // new IP
                            bdata[16] = 10;
                            bdata[17] = 100;
                            bdata[18] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 10;
                            bdata[25] = 100;
                            bdata[26] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 0;
                            bdata[239] = 0;
                        }
                        break;
                    case "12100":
                        if (chg_ip == 1)
                        {
                            // new IP
                            bdata[16] = 12;
                            bdata[17] = 100;
                            bdata[18] = 100;
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 12;
                            bdata[25] = 100;
                            bdata[26] = 100;
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 255;
                            bdata[239] = 0;
                        }
                        else
                        {
                            // new IP
                            bdata[16] = 12;
                            bdata[17] = 100;
                            bdata[18] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 12;
                            bdata[25] = 100;
                            bdata[26] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 255;
                            bdata[239] = 0;
                        }
                        break;
                    //case "10100":
                    //    if (chg_ip == 1)
                    //    {
                    //        // new IP
                    //        bdata[16] = 10;
                    //        bdata[17] = 100;
                    //        bdata[18] = 100;
                    //        bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                    //        // new Gateway
                    //        bdata[24] = 10;
                    //        bdata[25] = 100;
                    //        bdata[26] = 100;
                    //        bdata[27] = 254;
                    //    }
                    //    else
                    //    {
                    //        // new IP
                    //        bdata[16] = 10;
                    //        bdata[17] = 100;
                    //        bdata[18] = Convert.ToByte(Global.DA2[ndx, 13]);
                    //        bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                    //        // new Gateway
                    //        bdata[24] = 10;
                    //        bdata[25] = 100;
                    //        bdata[26] = Convert.ToByte(Global.DA2[ndx, 13]);
                    //        bdata[27] = 254;
                    //    }
                    //    break;
                    case "100100":
                        if (chg_ip == 1)
                        {
                            // new IP
                            bdata[16] = 100;
                            bdata[17] = 100;
                            bdata[18] = 100;
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 100;
                            bdata[25] = 100;
                            bdata[26] = 100;
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 255;
                            bdata[239] = 0;
                        }
                        else
                        {
                            // new IP
                            bdata[16] = 100;
                            bdata[17] = 100;
                            bdata[18] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[19] = Convert.ToByte(Global.DA2[ndx, 14]);
                            // new Gateway
                            bdata[24] = 100;
                            bdata[25] = 100;
                            bdata[26] = Convert.ToByte(Global.DA2[ndx, 13]);
                            bdata[27] = 254;
                            // new Netmask
                            bdata[236] = 255;
                            bdata[237] = 255;
                            bdata[238] = 255;
                            bdata[239] = 0;
                        }
                        break;
                    default:
                        return;
                }
                //
                bdata[70] = 2;
                // suppose user/password is default: "admin "
                bdata[71] = Convert.ToByte('a');
                bdata[72] = Convert.ToByte('d');
                bdata[73] = Convert.ToByte('m');
                bdata[74] = Convert.ToByte('i');
                bdata[75] = Convert.ToByte('n');
                bdata[76] = Convert.ToByte(' ');
                //
                bdata[255] = Convert.ToByte(Convert.ToUInt64("FF", 16));
                // new Host Name =>90~97
                //bdata[90] =
                //bdata[91] =
                //bdata[92] =
                //bdata[93] =
                //bdata[94] =
                //bdata[95] =
                //bdata[96] =
                //bdata[97] =

                udpSocket.SendTo(bdata, ipEndpoint);
            }
        }

        private void cmdUpgrade_Click(object sender, EventArgs e)
        {
            inviteTmr.Enabled = false;

            if (selectFW == null)
            {
                MessageBox.Show("請選擇要升級的 Firmware 版本", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (linuxEXE == null)
            {
                MessageBox.Show("請輸入 Model name，以判斷 cpu 型號", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CleanARP();
            int i;
            if (!Directory.Exists(appPATH + @"\FW_temp\"))
            {
                Directory.CreateDirectory(appPATH + @"\FW_temp");
            }
            // Check FW in FW_temp directory
            if (File.Exists(appPATH + @"\FW_version\" + selectFW))
            {
                File.Copy(appPATH + @"\FW_version\" + selectFW, appPATH + @"\FW_temp\" + selectFW, true);
            }
            // Check toolchain in FW_temp directory
            if (File.Exists(appPATH + @"\" + linuxEXE))
            {
                File.Copy(appPATH + @"\" + linuxEXE, appPATH + @"\FW_temp\" + linuxEXE, true);
            }

            // build upgrade(.bat) files
            for (i = 0; i < Global.DevCount; i++)
            {
                string batFile = appPATH + @"\FW_temp\" + Global.DA2[i, 0] + ".bat";
                using (StreamWriter sw = new StreamWriter(batFile, false, Encoding.Default))
                {
                    // Write a line of text
                    //sw.WriteLine("arp -d");
                    switch (linuxEXE)
                    {
                        case "gwdl.exe": // Old naming rule / outsourcing(外包)
                            sw.WriteLine("gwdl " + Global.DA2[i, 0] + " " + selectFW + " admin");
                            break;
                        //case "1":

                        //    break;
                        //case "2":

                        //    break;
                        case "linux_dl_v2.exe": // IDT-336、IDT-438
                            sw.WriteLine(linuxEXE + " " + selectFW + " " + Global.DA2[i, 0]);
                            break;
                        case "mpc5200_dl_v2.exe": // Free-Scale
                            if (selectFW.ToUpper().Contains("KER"))
                            {
                                sw.WriteLine(linuxEXE + " -l " + selectFW + " " + Global.DA2[i, 0]);    // kernel download script
                            }
                            else if (selectFW.ToUpper().Contains("AP"))
                            {
                                sw.WriteLine(linuxEXE + " -r " + selectFW + " " + Global.DA2[i, 0]);    // ramdisk (AP) download script
                            }
                            //
                            if (selectFW.ToUpper().Contains("K") && !selectFW.ToUpper().Contains("A"))
                            {
                                sw.WriteLine(linuxEXE + " -l " + selectFW + " " + Global.DA2[i, 0]);    // kernel download script
                            }
                            else if (selectFW.ToUpper().Contains("A") && !selectFW.ToUpper().Contains("K"))
                            {
                                sw.WriteLine(linuxEXE + " -r " + selectFW + " " + Global.DA2[i, 0]);    // ramdisk (AP) download script
                            }
                            else if (selectFW.ToUpper().Contains("BL") && !selectFW.ToUpper().Contains("K") && !selectFW.ToUpper().Contains("A"))
                            {
                                sw.WriteLine(linuxEXE + " -b " + selectFW + " " + Global.DA2[i, 0]);
                            }
                            else
                            {
                                sw.WriteLine(linuxEXE + " -r " + selectFW + " " + Global.DA2[i, 0]);
                            }
                            break;
                        //case "6":// Free-Scale (Dual Core)

                        //    break;
                        //case "7":// XScale

                        //    break;
                        case "mpc8308_dl.exe": // 8308
                            if (selectFW.ToUpper().Contains("KER"))
                            {
                                sw.WriteLine(linuxEXE + " -l " + selectFW + " " + Global.DA2[i, 0]);    // kernel download script
                            }
                            else if (selectFW.ToUpper().Contains("AP"))
                            {
                                sw.WriteLine(linuxEXE + " -r " + selectFW + " " + Global.DA2[i, 0]);    // ramdisk (AP) download script
                            }
                            //
                            if (selectFW.ToUpper().Contains("K") && !selectFW.ToUpper().Contains("A"))
                            {
                                sw.WriteLine(linuxEXE + " -l " + selectFW + " " + Global.DA2[i, 0]);    // kernel download script
                            }
                            else if (selectFW.ToUpper().Contains("A") && !selectFW.ToUpper().Contains("K"))
                            {
                                sw.WriteLine(linuxEXE + " -r " + selectFW + " " + Global.DA2[i, 0]);    // ramdisk (AP) download script
                            }
                            else if (selectFW.ToUpper().Contains("BL") && !selectFW.ToUpper().Contains("K") && !selectFW.ToUpper().Contains("A"))
                            {
                                sw.WriteLine(linuxEXE + " -b " + selectFW + " " + Global.DA2[i, 0]);
                            }
                            else
                            {
                                sw.WriteLine(linuxEXE + " -r " + selectFW + " " + Global.DA2[i, 0]);
                            }
                            break;
                        case "ti3352_dl.exe": // TI3352, like mpc8308_dl.exe
                            if (selectFW.ToUpper().Contains("K") && !selectFW.ToUpper().Contains("A"))
                            {
                                sw.WriteLine(linuxEXE + " -l " + selectFW + " " + Global.DA2[i, 0]);    // kernel download script
                            }
                            else if (selectFW.ToUpper().Contains("A") && !selectFW.ToUpper().Contains("K"))
                            {
                                sw.WriteLine(linuxEXE + " -r " + selectFW + " " + Global.DA2[i, 0]);    // ramdisk (AP) download script
                            }
                            else
                            {
                                sw.WriteLine(linuxEXE + " -r " + selectFW + " " + Global.DA2[i, 0]);
                            }
                            break;
                        default:
                            break;
                    }
                    //sw.WriteLine("pause");
                }
            }
            Global.DevUpgradeCount = 0;
            RunUpgrade();
        }

        public void RunUpgrade()
        {
            int i;
            // run upgrade(.bat) files
            ProcessStartInfo Info2 = new ProcessStartInfo();
            for (i = 0; i < Global.DevCount; i++)
            {
                try
                {
                    // 執行的檔案名稱
                    Info2.FileName = Global.DA2[i, 0] + ".bat";
                    // 執行的檔案所在的目錄
                    Info2.WorkingDirectory = appPATH + @"\FW_temp";
                    Process.Start(Info2);

                    Global.DevUpgradeCount = Global.DevUpgradeCount + 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Hold(100);
            }
            this.txtDevUpCnt.Text = Convert.ToString(Global.DevUpgradeCount);
        }

        private void lstFilesBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtNewKernal.Text = string.Empty;
            txtNewAP.Text = string.Empty;
            Regex versionPattern = new Regex("[0-9.]");

            if (lstFilesBox.SelectedItem != null)
            {
                selectFW = lstFilesBox.SelectedItem.ToString();
            }
            //Debug.Print(selectFW);
            int K_idx, A_idx, lastDot_idx;
            string kernel_value, ap_value;
            K_idx = selectFW.LastIndexOf("K", StringComparison.CurrentCultureIgnoreCase);
            A_idx = selectFW.LastIndexOf("A", StringComparison.CurrentCultureIgnoreCase);
            lastDot_idx = selectFW.LastIndexOf(".", StringComparison.CurrentCultureIgnoreCase);
            if (K_idx != -1)
            {
                if (A_idx == -1)
                {
                    kernel_value = selectFW.Substring(K_idx + 1, lastDot_idx - K_idx - 1);
                }
                else
                {
                    kernel_value = selectFW.Substring(K_idx + 1, A_idx - K_idx - 1);
                }

                if (versionPattern.IsMatch(kernel_value))
                {
                    if (kernel_value.Contains("."))
                    {
                        txtNewKernal.Text = kernel_value;
                    }
                    else
                    {
                        txtNewKernal.Text = kernel_value.Substring(0, 1) + "." + kernel_value.Substring(1);
                    }
                }
            }
            if (A_idx != -1)
            {
                ap_value = selectFW.Substring(A_idx + 1, lastDot_idx - A_idx - 1);
                if (versionPattern.IsMatch(ap_value))
                {
                    if (ap_value.Contains("."))
                    {
                        txtNewAP.Text = ap_value;
                    }
                    else
                    {
                        txtNewAP.Text = ap_value.Substring(0, 1) + "." + ap_value.Substring(1);
                    }
                }
            }
            Debug.Print(selectFW);
        }

        private void txtTargetModel_Leave(object sender, EventArgs e)
        {
            if (txtTargetModel.Text != string.Empty)
            {
                if (IsModelName(txtTargetModel.Text))
                {
                    targetModel = txtTargetModel.Text;
                    string cpuModel = txtTargetModel.Text.Substring(3, 1);
                    switch (cpuModel)
                    {
                        case "0":
                            // Old naming rule / outsourcing(外包)
                            linuxEXE = "gwdl.exe";
                            break;
                        case "1":

                            break;
                        case "2":

                            break;
                        case "3":
                            // IDT-336
                            linuxEXE = "linux_dl_v2.exe";
                            break;
                        case "4":
                            // IDT-438
                            linuxEXE = "linux_dl_v2.exe";
                            break;
                        case "5":
                            // Free-Scale
                            linuxEXE = "mpc5200_dl_v2.exe";
                            break;
                        case "6":
                            // Free-Scale (Dual Core)

                            break;
                        case "7":
                            // XScale

                            break;
                        case "8":
                            // 8308
                            linuxEXE = "mpc8308_dl.exe";
                            break;
                        case "9":
                            // TI3352, like mpc8308_dl.exe
                            linuxEXE = "ti3352_dl.exe";
                            break;
                        default:
                            MessageBox.Show("沒有對應的 cpu 型號");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("無法識別 !\n請確認 Model name 的前五個字元格式是否正確: [大寫英][大寫英][數][數][數]．．．");
                    txtTargetModel.Text = string.Empty;
                    targetModel = null;
                }
            }
            else
            {
                targetModel = null;
            }
        }

        private void txtTargetIP_Leave(object sender, EventArgs e)
        {
            if (txtTargetIP.Text != string.Empty)
            {
                if (IsIP(txtTargetIP.Text) || IsNetworkSegment(txtTargetIP.Text))
                {
                    targetIP = txtTargetIP.Text;
                }
                else
                {
                    MessageBox.Show("IP 格式不正確");
                    txtTargetIP.Text = string.Empty;
                    targetIP = null;
                }
            }
            else
            {
                targetIP = null;
            }
        }

        private void txtTargetKer_Leave(object sender, EventArgs e)
        {
            if (txtTargetKer.Text != string.Empty)
            {
                targetKer = txtTargetKer.Text;
            }
            else
            {
                targetKer = null;
            }
        }

        private void txtTargetAP_Leave(object sender, EventArgs e)
        {
            if (txtTargetAP.Text != string.Empty)
            {
                targetAP = txtTargetAP.Text;
            }
            else
            {
                targetAP = null;
            }
        }

        private void 重新載入Firmware列表_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;   // 漏斗指標
            lstFilesBox.Items.Clear();
            Hold(10);
            string fileName;
            if (Directory.Exists(appPATH + @"\FW_Version"))
            {
                var files = Directory.GetFiles(appPATH + @"\FW_Version", "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".dld")
                    || s.EndsWith(".bin") || s.EndsWith(".hex") || s.EndsWith(".HEX") || s.EndsWith(".rom"));
                foreach (var obj in files)
                {
                    fileName = obj.Substring(obj.LastIndexOf("\\") + 1);
                    lstFilesBox.Items.Add(fileName);
                }
            }
            this.Cursor = Cursors.Default;      // 還原預設指標
        }

        private void cPU型號_Click(object sender, EventArgs e)
        {
            MessageBox.Show("支援的 cpu 型號列表:\n\n\tOld naming rule / outsourcing (0)\t\n\tIDT-336 (3)\t\n\tIDT-438 (4)\t\n\tFree-Scale (5)\t\n\t8308 (8)\t\n\tTI (9)\t\n\t" + "\n\n未支援跨 Software ID 的更新方式。"
                , " MultiUpdate integration", MessageBoxButtons.OK, MessageBoxIcon.None);
            /*
             * 尚未支援:
             * Free-Scale (Dual Core) (6)\t\n\t
             * XScale (7)\t\n\t
             */
        }

        private void 清理FWtemp資料夾_Click(object sender, EventArgs e)
        {
            // Delete FW in FW_temp directory
            if (Directory.Exists(appPATH + @"\FW_temp\"))
            {
                Directory.Delete(appPATH + @"\FW_temp\", true);
            }
            // CreateDirectory FW_temp directory
            if (!Directory.Exists(appPATH + @"\FW_temp\"))
            {
                Directory.CreateDirectory(appPATH + @"\FW_temp");
            }
        }

        private void chktargetModel_Click(object sender, EventArgs e)
        {
            chktargetIP.Checked = false;
        }

        private void chktargetIP_Click(object sender, EventArgs e)
        {
            chktargetModel.Checked = false;
        }

        private void 內網環境建置_Click(object sender, EventArgs e)
        {
            Shell(appPATH, "LAN_setting.bat");
        }

        private void inviteTmr_Tick(object sender, EventArgs e)
        {
            byte[] bdata = new byte[300];
            bdata[0] = 2;
            bdata[1] = 1;
            bdata[2] = 6;
            bdata[4] = Convert.ToByte(Convert.ToUInt64("92", 16)); //將十六進制轉換為十進制
            bdata[5] = Convert.ToByte(Convert.ToUInt64("DA", 16));
            IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Broadcast, UDPport);   // 目標 port

            if (!PortInUse(UDPport))  // 判斷 local port 是否占用中
            {
                udpClient = new UdpClient(UDPport); // 未使用就綁定 local port
                udpClient.Send(bdata, bdata.Length, ipEndpoint);
            }
            else
            {
                udpClient.Send(bdata, bdata.Length, ipEndpoint);
            }
            // 背景接收執行緒
            if (udpRcvThread == null || !udpRcvThread.IsAlive)
            {
                udpRcvThread = new Thread(new ThreadStart(ReceiveBroadcast));
                udpRcvThread.IsBackground = true;
                udpRcvThread.Priority = ThreadPriority.Highest;
                udpRcvThread.Start();
            }
        }

        private void cmdCloseIn_Click(object sender, EventArgs e)
        {
            inviteTmr.Enabled = false;
            udpClient.Close();
        }

        private void cmdDefaultIP_Click(object sender, EventArgs e)
        {
            int i;
            Global.DevDefaultCount = 0;

            inviteTmr.Enabled = false;

            try
            {
                for (i = 0; i < Global.DevCount; i++)
                {
                    if ((Global.DA2[i, 5] == Global.DA2[i, 13] && Global.DA2[i, 6] == Global.DA2[i, 14]) ||
                        (Global.DA2[i, 5] != Global.DA2[i, 13] && Global.DA2[i, 6] == Global.DA2[i, 14]))
                    {
                        SendConfigDefault(i); // set to default
                        Global.DevDefaultCount = Global.DevDefaultCount + 1;
                    }
                    else
                    {
                        Global.DevDefaultCount = Global.DevDefaultCount + 1;
                    }
                }
                this.txtDevDefCnt.Text = Convert.ToString(Global.DevDefaultCount);
                CleanARP();
                Hold(1000);
                //cmdInvite_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SendConfigDefault(int ndx)
        {
            byte[] bdata = new byte[300];
            using (Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                udpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Broadcast, UDPport);

                // configure IP
                bdata[0] = 0;
                bdata[1] = 1;
                bdata[2] = 6;
                bdata[4] = Convert.ToByte(Convert.ToUInt64("92", 16)); //將十六進制轉換為十進制
                bdata[5] = Convert.ToByte(Convert.ToUInt64("DA", 16));
                // old IP
                bdata[12] = Convert.ToByte(Global.DA2[ndx, 3]);
                bdata[13] = Convert.ToByte(Global.DA2[ndx, 4]);
                bdata[14] = Convert.ToByte(Global.DA2[ndx, 5]);
                bdata[15] = Convert.ToByte(Global.DA2[ndx, 6]);
                // old MAC
                bdata[28] = Convert.ToByte(Global.DA2[ndx, 9]);
                bdata[29] = Convert.ToByte(Global.DA2[ndx, 10]);
                bdata[30] = Convert.ToByte(Global.DA2[ndx, 11]);
                bdata[31] = Convert.ToByte(Global.DA2[ndx, 12]);
                bdata[32] = Convert.ToByte(Global.DA2[ndx, 13]);
                bdata[33] = Convert.ToByte(Global.DA2[ndx, 14]);

                // for ATC lan1 ip: 192.168.0.1、10.0.50.100、10.100.100.1、12.100.100.20、10.100.100.20、100.100.100.17
                switch (Global.DA2[ndx, 3] + Global.DA2[ndx, 4])
                {
                    case "192168":
                        // new IP
                        bdata[16] = 192;
                        bdata[17] = 168;
                        bdata[18] = 0;
                        bdata[19] = 1;
                        // new Gateway
                        bdata[24] = 192;
                        bdata[25] = 168;
                        bdata[26] = 0;
                        bdata[27] = 254;
                        // new Netmask
                        bdata[236] = 255;
                        bdata[237] = 255;
                        bdata[238] = 255;
                        bdata[239] = 0;
                        break;
                    case "100":
                        // new IP
                        bdata[16] = 10;
                        bdata[17] = 0;
                        bdata[18] = 50;
                        bdata[19] = 100;
                        // new Gateway
                        bdata[24] = 10;
                        bdata[25] = 0;
                        bdata[26] = 0;
                        bdata[27] = 254;
                        // new Netmask
                        bdata[236] = 255;
                        bdata[237] = 255;
                        bdata[238] = 0;
                        bdata[239] = 0;
                        break;
                    case "10100":
                        // new IP
                        bdata[16] = 10;
                        bdata[17] = 100;
                        bdata[18] = 100;
                        bdata[19] = 1;
                        // new Gateway
                        bdata[24] = 10;
                        bdata[25] = 100;
                        bdata[26] = 100;
                        bdata[27] = 254;
                        // new Netmask
                        bdata[236] = 255;
                        bdata[237] = 255;
                        bdata[238] = 255;
                        bdata[239] = 0;
                        break;
                    case "12100":
                        // new IP
                        bdata[16] = 12;
                        bdata[17] = 100;
                        bdata[18] = 100;
                        bdata[19] = 20;
                        // new Gateway
                        bdata[24] = 12;
                        bdata[25] = 100;
                        bdata[26] = 100;
                        bdata[27] = 254;
                        // new Netmask
                        bdata[236] = 255;
                        bdata[237] = 255;
                        bdata[238] = 255;
                        bdata[239] = 0;
                        break;
                    //case "10100":
                    //    new IP
                    //        bdata[16] = 10;
                    //        bdata[17] = 100;
                    //        bdata[18] = 100;
                    //        bdata[19] = 20;
                    //        // new Gateway
                    //        bdata[24] = 10;
                    //        bdata[25] = 100;
                    //        bdata[26] = 100;
                    //        bdata[27] = 254;
                    //    break;
                    case "100100":
                        // new IP
                        bdata[16] = 100;
                        bdata[17] = 100;
                        bdata[18] = 100;
                        bdata[19] = 17;
                        // new Gateway
                        bdata[24] = 100;
                        bdata[25] = 100;
                        bdata[26] = 100;
                        bdata[27] = 254;
                        // new Netmask
                        bdata[236] = 255;
                        bdata[237] = 255;
                        bdata[238] = 255;
                        bdata[239] = 0;
                        break;
                    default:
                        return;
                }
                //
                bdata[70] = 2;
                // suppose user/password is default: "admin "
                bdata[71] = Convert.ToByte('a');
                bdata[72] = Convert.ToByte('d');
                bdata[73] = Convert.ToByte('m');
                bdata[74] = Convert.ToByte('i');
                bdata[75] = Convert.ToByte('n');
                bdata[76] = Convert.ToByte(' ');
                //
                bdata[255] = Convert.ToByte(Convert.ToUInt64("FF", 16));
                // new Host Name =>90~97
                //bdata[90] =
                //bdata[91] =
                //bdata[92] =
                //bdata[93] =
                //bdata[94] =
                //bdata[95] =
                //bdata[96] =
                //bdata[97] =

                udpSocket.SendTo(bdata, ipEndpoint);
            }
        }

        private void cmdWeb_Click(object sender, EventArgs e)
        {
            if (lstDev.SelectedIndex >= 0)
            {
                cmdWeb.Enabled = true;
            }
            else
            {
                cmdWeb.Enabled = false;
                return;
            }

            string getStr;
            string[] splitStr;
            getStr = lstDev.SelectedItem.ToString();
            if (getStr == string.Empty)
            {
                cmdWeb.Enabled = false;
                return;
            }
            splitStr = getStr.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            System.Diagnostics.Process.Start("http://" + splitStr[1].Trim());
        }
    }
}