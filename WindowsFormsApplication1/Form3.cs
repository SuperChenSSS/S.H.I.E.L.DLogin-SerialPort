using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        private bool m_isTimer1;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form2.playSimpleSound("verified.wav");
            m_isTimer1 = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Dispose();
            this.Close();
            f1.ShowDialog();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_isTimer1 = !m_isTimer1;
            if (m_isTimer1)
                BackColor = Color.Chartreuse;
            else
                BackColor = Color.Black;
        }
    }
}
