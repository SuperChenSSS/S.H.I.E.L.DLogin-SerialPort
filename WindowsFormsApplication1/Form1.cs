using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private bool m_isOpen;
        private bool m_isHexDisplay;
        private bool m_isHexSend;
        private bool m_isPrint;
        private System.Timers.Timer m_timerAutoSend;
        private string m_strCom;
        private int m_intBaud;
        private string m_strParity;
        private int m_intDatabits;
        private int m_intstopDatabits;
        //private string m_sRecevied;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 5;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 1;
            comboBox5.SelectedIndex = 1;
            button1.BackColor = Color.Red;
            //serialPort.Open();
            m_isOpen = false;
            m_isHexDisplay = false;
            m_isHexSend = false;
            m_isPrint = true;
        }

        public void setComParam()
        {
            serialPort.PortName = m_strCom;
            serialPort.BaudRate = m_intBaud;
            switch (m_strParity)
            {
                case "EVEN":serialPort.Parity = System.IO.Ports.Parity.Even;
                            break;
                case "ODD": serialPort.Parity = System.IO.Ports.Parity.Odd;
                            break;
                case "NONE":serialPort.Parity = System.IO.Ports.Parity.None;
                            break;
            }
            serialPort.DataBits = m_intDatabits;
            switch (m_intstopDatabits)
            {
                case 1: serialPort.StopBits = System.IO.Ports.StopBits.One;
                        break;
                case 2: serialPort.StopBits = System.IO.Ports.StopBits.Two;
                        break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!m_isOpen)
                {
                    setComParam();
                    m_isOpen = !m_isOpen;
                    serialPort.Open();
                    button1.BackColor = Color.Green;
                    button1.Text = "串口打开";
                    Form2.playSimpleSound("incomingtransmission.wav");
                }
                else
                {
                    m_isOpen = !m_isOpen;
                    serialPort.Close();
                    button1.BackColor = Color.Red;
                    button1.Text = "串口关闭";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.ToString(), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            m_isOpen = false;
            serialPort.Close();
            button1.BackColor = Color.Red;
            button1.Text = "串口关闭";
            m_strCom = comboBox1.Text;
            Console.WriteLine(m_strCom);
            //textBox1.Text = strTemp;
        }

        private void OnRecvSerialData(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string strTemp = serialPort.ReadExisting();

            /*m_sRecevied  += strTemp;
            String SerialIn = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.Default.GetBytes(), 0, m_sRecevied.Length); 

            Console.Write(strTemp + "|  ");
            Console.WriteLine("m_sRecevied:" + m_sRecevied);
            if (m_sRecevied.Substring(m_sRecevied.Length - 1, 1) == "\r")
            {
                if (m_sRecevied.Substring(m_sRecevied.Length - 2, 1) == "*")
                {
                    Console.WriteLine("recv:" + m_sRecevied);
                    //m_logDlg.textAppendText(m_sRecevied);
                    m_sRecevied = "";
                }
                else
                {
                    serialPort.Write("\r");
                }
            }
            serialPort.DiscardOutBuffer();
            */

            byte[] array = System.Text.Encoding.ASCII.GetBytes(strTemp);  //数组array为对应的ASCII数组     
            string ASCIIstr = null;
            for (int i = 0; i < array.Length; i++)
            {
                int asciicode = (int)(array[i]);
                ASCIIstr += asciicode.ToString("X");
            }
            if (m_isHexDisplay&&m_isPrint)
            {
                Console.WriteLine(ASCIIstr);
                addTextBoxMessage(ASCIIstr);
            }
            else if(!m_isHexDisplay&&m_isPrint)
            {
                Console.WriteLine(strTemp);
                addTextBoxMessage(strTemp);
            }
          
        }

        private void addTextBoxMessage(String msg)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                textBox1.Text += msg + Environment.NewLine;
            }));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2.playSimpleSound("complete.wav");
            this.Close();
            //此处应有Form2退出代码，但目前找不到解决方案==
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2.playSimpleSound("transfer.wav");
            string Tmp = textBox3.Text;
            byte[] array = System.Text.Encoding.ASCII.GetBytes(Tmp);  //数组array为对应的ASCII数组     
            string ASCIIstr = null;
            for (int i = 0; i < array.Length; i++)
            {
                int asciicode = (int)(array[i]);
                ASCIIstr += asciicode.ToString("X");
            }
            if (!m_isHexSend)
                serialPort.Write(Tmp);
            else
                serialPort.Write(ASCIIstr);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //m_isHexDisplay = !m_isHexDisplay;
            if (checkBox2.Checked)
                m_isHexDisplay = true;
            else
                m_isHexDisplay = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_isPrint = !m_isPrint;
            if (!m_isPrint)
                button3.Text = "开始显示";
            else
                button3.Text = "停止显示";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                m_isHexSend = true;
            else
                m_isHexSend = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            int textMeg = Convert.ToInt32(textBox2.Text);
            if (checkBox4.Checked)
            {
                m_timerAutoSend = new System.Timers.Timer(textMeg);
                m_timerAutoSend.Elapsed += new System.Timers.ElapsedEventHandler(button7_Click);//到达时间的时候执行事件；
                m_timerAutoSend.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
                m_timerAutoSend.Start();//是否执行System.Timers.Timer.Elapsed事件；
            }
            else if(!checkBox4.Checked)
            {
                m_timerAutoSend.AutoReset = false;
                m_timerAutoSend.Stop();
            }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            m_isOpen = false;
            serialPort.Close();
            button1.BackColor = Color.Red;
            button1.Text = "串口关闭";
            m_intBaud = Convert.ToInt32(comboBox2.Text);
            Console.WriteLine(m_intBaud);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_isOpen = false;
            serialPort.Close();
            button1.BackColor = Color.Red;
            button1.Text = "串口关闭";
            m_strParity = comboBox3.Text;
            Console.WriteLine(m_strParity);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_isOpen = false;
            serialPort.Close();
            button1.BackColor = Color.Red;
            button1.Text = "串口关闭";
            m_intDatabits = Convert.ToInt32(comboBox4.Text);
            Console.WriteLine(m_intDatabits);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_isOpen = false;
            serialPort.Close();
            button1.BackColor = Color.Red;
            button1.Text = "串口关闭";
            m_intstopDatabits = Convert.ToInt32(comboBox5.Text);
            Console.WriteLine(m_intstopDatabits);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No References!","References",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            serialPort.DiscardOutBuffer();//防止问题
            string writeBit = "@00RD0001" + DEC2HEX(textBox3.Text);
            string sendBit = Command(writeBit);
            Console.WriteLine(sendBit);
            serialPort.Write(sendBit);
            //textBox1.Text = sendBit.ToString();
        }
        //对指令及数据的字符串进行最终，并添加“*CR” 
        public string Command(string Str)
        {
            char[] chararraytemp = Str.ToCharArray();
            int fcs = 0;
            foreach (char chartemp in chararraytemp)
            {
                fcs = fcs ^ Convert.ToInt16(chartemp);           //对字符进行ASC转换，并进行异或运算
            }
            string fcstohex = String.Format("{0:X2}", (uint)System.Convert.ToUInt32(fcs));
            string result = Str + fcstohex + "*" + Convert.ToChar(13);
            return result;
        }

        public string DEC2HEX(string ox)
        {
            string strHex = String.Format("{0:X4}", (uint)System.Convert.ToUInt32(ox));       //转换为16进制,{0:X4}大写4位；{0:x4}小写4位
            return strHex;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
	            SaveFileDialog saveDlg = new SaveFileDialog();//让用户指定文件的路径名
	            saveDlg.Filter = "文本文件|*.txt";
	           if (saveDlg.ShowDialog() == DialogResult.OK)
	           {
	               FileStream fs = File.Open(saveDlg.FileName,
	                                         FileMode.Create,
	                                         FileAccess.Write);
	               StreamWriter sw = new StreamWriter(fs);
	               //保存textbox1中所有内容
	               sw.Write(textBox1.Text);
	               //关闭文件
	               sw.Flush();
	               sw.Close();
	               fs.Close();
	               //提示用户文件已成功保存
	               MessageBox.Show("文件已成功保存到"+saveDlg.FileName);
	           }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error!"+ex.ToString());
            }
        }
    }
}