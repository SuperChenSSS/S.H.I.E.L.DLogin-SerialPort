using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.FlatStyle = FlatStyle.Standard;//样式
            textBox1.Select();
            playSimpleSound("begin.wav");
        }

        public static void playSimpleSound(string a)//string类型a用作目录下的精确歌曲定位
        {
            SoundPlayer simpleSound = new SoundPlayer(@"../../../sounds/"+a);//指向歌曲地址
            simpleSound.Play();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("SuperChen") && textBox2.Text.Equals("cmycmy"))
            {
                this.Close();
            }
            else
            {
                Wrong wr = new Wrong();
                wr.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Program will exit,Are you sure?","Exit",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void MD(object sender, MouseEventArgs e)
        {
            playSimpleSound("enterauthorizationcode.wav");
        }

        private void KD(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }

        private void FC(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Text.Equals("SuperChen") && textBox2.Text.Equals("cmycmy"))
            {
                Form3 f3 = new Form3();
                f3.ShowDialog();
            }
        }

    }
}
