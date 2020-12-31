using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBTA.Utils;

namespace DBTA
{
    public partial class Form1 : Form
    {
        private bool islogin = false;
        private string text;
        private string caseno;
        private string gpuno;
        private string cpuno;
        private string fanno;
        private string boardno;
        private string ramno;
        private string diskno;
        private string powerno;
        private int cpuprice;
        private int fanprice;
        private int boardprice;
        private int ramprice;
        private int diskprice;
        private int powerprice;
        private int caseprice;
        private int gpuprice;
        private int ramnum;
        private int PRICE = 0;
        public Form1()
        {
            InitializeComponent();
            Count();
            

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
                    text = lf.namestr;
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
            if (GPUSELECT.GPUselect == true)
            {
                textBox9.Text = GPUSELECT.GPUname;
                gpuno = GPUSELECT.GPUNO;
                int.TryParse(GPUSELECT.GPUPRICE, out gpuprice);
            }
            Count();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CPU CPUSELECT = new CPU();
            CPUSELECT.ShowDialog();
            if (CPUSELECT.CPUselect == true)
            {
                textBox6.Text = CPUSELECT.CPUname;
                cpuno = CPUSELECT.CPUNO;
                int.TryParse(CPUSELECT.CPUPRICE, out cpuprice);
            }
            Count();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FAN FANSELECT = new FAN();
            FANSELECT.ShowDialog();
            if (FANSELECT.FANselect == true)
            {
                textBox5.Text = FANSELECT.FANname;
                fanno = FANSELECT.FANNO;
                int.TryParse(FANSELECT.FANPRICE, out fanprice);
            }
            Count();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BOARD BOARDSELECT = new BOARD();
            BOARDSELECT.ShowDialog();
            if (BOARDSELECT.BOARDselect == true)
            {
                textBox2.Text = BOARDSELECT.BOARDname;
                boardno = BOARDSELECT.BOARDNO;
                int.TryParse(BOARDSELECT.BOARDPRICE, out boardprice);
            }
            Count();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RAM RAMSELECT = new RAM();
            RAMSELECT.ShowDialog();
            if (RAMSELECT.RAMselect == true)
            {
                textBox3.Text = RAMSELECT.RAMname;
                ramno = RAMSELECT.RAMNO;
                int.TryParse(RAMSELECT.RAMPRICE, out ramprice);
            }
            Count();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DISK DISKSELECT = new DISK();
            DISKSELECT.ShowDialog();
            if (DISKSELECT.DISKselect == true)
            {
                textBox4.Text = DISKSELECT.DISKname;
                diskno = DISKSELECT.DISKNO;
                int.TryParse(DISKSELECT.DISKPRICE, out diskprice);
            }
            Count();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            POWER POWERSELECT = new POWER();
            POWERSELECT.ShowDialog();
            if (POWERSELECT.POWERselect == true)
            {
                textBox10.Text = POWERSELECT.POWERname;
                powerno = POWERSELECT.POWERNO;
                int.TryParse(POWERSELECT.POWERPRICE, out powerprice);
            }
            Count();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            @case CASESELECT = new @case();
            CASESELECT.ShowDialog();
            if (CASESELECT.CASEselect == true)
            {
                textBox11.Text = CASESELECT.CASEname;
                caseno = CASESELECT.CASENO;
                int.TryParse(CASESELECT.CASEPRICE, out caseprice);
            }
            Count();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (islogin)
            {
                string a = text;
                Connection.query($"insert into LIST_PC (LISTNO, LISTNAME, MNO, CPUNO, FANNO,BOARDNO,RAMNO,RAMNUM,DISKNO,GPUNO,POWERNO,CASENO) values('{Guid.NewGuid().ToString()}', '{textBox7.Text}', '{a}', '{cpuno}', '{fanno}', '{boardno}', '{ramno}',{textBox8.Text}, '{diskno}', '{gpuno}', '{powerno}', '{caseno}') ");
                MessageBox.Show("保存装机单成功！");
            }
            else MessageBox.Show("您不是会员，无法保存装机单！");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }
        private void Count()
        {
            if (textBox6.Text != "")
            {
                PRICE = PRICE + cpuprice;
                
            }
            if (textBox9.Text != "")
            {
                PRICE = PRICE + gpuprice;
            }
            if (textBox5.Text != "")
            {
                PRICE = PRICE + fanprice;
            }
            if (textBox2.Text != "")
            {
                PRICE = PRICE + boardprice;
            }
            if (textBox3.Text != "")
            {
                if (textBox8.Text == "")
                {
                    PRICE = PRICE + ramprice;
                }
                else
                {
                    int.TryParse(textBox8.Text, out ramnum);
                    PRICE = PRICE + ramnum * ramprice;
                }

            }
            if (textBox4.Text != "")
            {
                PRICE = PRICE + diskprice;
            }
            if (textBox10.Text != "")
            {
                PRICE = PRICE + powerprice;
            }
            if (textBox11.Text != "")
            {
                PRICE = PRICE + caseprice;
            }
            textBox1.Text = Convert.ToString(PRICE);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            Count();
        }
    }
}


