using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBTA
{
    public partial class Form1 : Form
    {
        private bool islogin = false;

        public Form1()
        {
            InitializeComponent();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!islogin)
            {
                LoginForm lf = new LoginForm();
                lf.ShowDialog();
                islogin = lf.islogin;
                if (islogin)
                {
                    int right = label1.Right;
                    label1.Text = "欢迎您，" + lf.namestr;
                    label1.Left = right - label1.Width;
                    linkLabel1.Text = "退出登录";
                }
            }
            else
            {
                int right = label1.Right;
                label1.Text = "欢迎您，游客";
                label1.Left = right - label1.Width;
                linkLabel1.Text = "请登录！";
                islogin = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GPU GPUSELECT = new GPU();
            GPUSELECT.ShowDialog();
            if(GPUSELECT.GPUselect == true)
            {
                textBox9.Text = GPUSELECT.GPUname;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CPU CPUSELECT = new CPU();
            CPUSELECT.ShowDialog();
            if (CPUSELECT.CPUselect == true)
            {
                textBox6.Text = CPUSELECT.CPUname;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FAN FANSELECT = new FAN();
            FANSELECT.ShowDialog();
            if (FANSELECT.FANselect == true)
            {
                textBox5.Text = FANSELECT.FANname;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BOARD BOARDSELECT = new BOARD();
            BOARDSELECT.ShowDialog();
            if (BOARDSELECT.BOARDselect == true)
            {
                textBox5.Text = BOARDSELECT.BOARDname;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RAM RAMSELECT = new RAM();
            RAMSELECT.ShowDialog();
            if (RAMSELECT.RAMselect == true)
            {
                textBox3.Text = RAMSELECT.RAMname;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DISK DISKSELECT = new DISK();
            DISKSELECT.ShowDialog();
            if (DISKSELECT.DISKselect == true)
            {
                textBox4.Text = DISKSELECT.DISKname;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            POWER POWERSELECT = new POWER();
            POWERSELECT.ShowDialog();
            if (POWERSELECT.POWERselect == true)
            {
                textBox10.Text = POWERSELECT.POWERname;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            @case CASESELECT = new @case();
            CASESELECT.ShowDialog();
            if (CASESELECT.CASEselect == true)
            {
                textBox11.Text = CASESELECT.CASEname;
            }
        }
    }
}


