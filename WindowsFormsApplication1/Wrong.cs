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
    public partial class Wrong : Form
    {
        private bool m_isTimer1;
        public Wrong()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Wrong_Load(object sender, EventArgs e)
        {
            Form2.playSimpleSound("warning.wav");
            m_isTimer1 = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_isTimer1 = !m_isTimer1;
            if(m_isTimer1)
                BackColor = Color.Black;
            else
                BackColor = Color.Red;
        }
    }
}
