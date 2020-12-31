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
                    text= lf.nostr;
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
                gpuno=GPUSELECT.GPUNO;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CPU CPUSELECT = new CPU();
            CPUSELECT.ShowDialog();
            if (CPUSELECT.CPUselect == true)
            {
                textBox6.Text = CPUSELECT.CPUname;
                cpuno = CPUSELECT.CPUNO;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FAN FANSELECT = new FAN();
            FANSELECT.ShowDialog();
            if (FANSELECT.FANselect == true)
            {
                textBox5.Text = FANSELECT.FANname;
                fanno= FANSELECT.FANNO;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BOARD BOARDSELECT = new BOARD();
            BOARDSELECT.ShowDialog();
            if (BOARDSELECT.BOARDselect == true)
            {
                textBox2.Text = BOARDSELECT.BOARDname;
                boardno= BOARDSELECT.BOARDNO;
            }
        }

        private void button4_Click(object sender, EventArgs e)  
        {
            RAM RAMSELECT = new RAM();
            RAMSELECT.ShowDialog();
            if (RAMSELECT.RAMselect == true)
            {
                textBox3.Text = RAMSELECT.RAMname;
                ramno= RAMSELECT.RAMNO;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DISK DISKSELECT = new DISK();
            DISKSELECT.ShowDialog();
            if (DISKSELECT.DISKselect == true)
            {
                textBox4.Text = DISKSELECT.DISKname;
                diskno= DISKSELECT.DISKNO;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            POWER POWERSELECT = new POWER();
            POWERSELECT.ShowDialog();
            if (POWERSELECT.POWERselect == true)
            {
                textBox10.Text = POWERSELECT.POWERname;
                powerno= POWERSELECT.POWERNO;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            @case CASESELECT = new @case();
            CASESELECT.ShowDialog();
            if (CASESELECT.CASEselect == true)
            {
                textBox11.Text = CASESELECT.CASEname;
                caseno= CASESELECT.CASENO;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(islogin)
            {
                string a = text;
                Connection.query($"insert into LIST_PC (LISTNO, LISTNAME, MNO, CPUNO, FANNO,BOARDNO,RAMNO,RAMNUM,DISKNO,GPUNO,POWERNO,CASENO) values('{Guid.NewGuid().ToString()}', '{textBox7.Text}', '{a}', '{cpuno}', '{fanno}', '{boardno}', '{ramno}',{textBox8.Text}, '{diskno}', '{gpuno}', '{powerno}', '{caseno}') ");
                MessageBox.Show("保存装机单成功！");
            }
            else MessageBox.Show("您不是会员，无法保存装机单！");

        }



        private void getListdata()
        {   //0      1        2    3    4     5        6     7      8     9      10      11 
            //LISTNO LISTNAME MNO CPUNO FANNO BOARDNO RAMNO DISKNO GPUNO POWERNO CASENO RAMNUM
            List<string> ab = Connection.query("select * from list_PC");
            dataGridView1.Rows.Clear();

            int nrows = ab.Count / 12;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                string listno = ab[0 + 12 * index];
                string listname = ab[1 + 12 * index];
                string mno = ab[2 + 12 * index];
                string cpuno = ab[3 + 12 * index];
                string fanno = ab[4 + 12 * index];
                string boardno = ab[5 + 12 * index];
                string ramno = ab[6 + 12 * index];
                string diskno = ab[7 + 12 * index];
                string gpuno = ab[8 + 12 * index];
                string powerno = ab[9 + 12 * index];
                string caseno = ab[10 + 12 * index];
                int ramnum = int.Parse(ab[11 + 12 * index]);

                int price = 0;
                price += int.Parse(Connection.query($"select price from CPU_PC where cpuno='{cpuno}'")[0]);
                price += int.Parse(Connection.query($"select price from FAN where cpuno='{fanno}'")[0]);
                price += int.Parse(Connection.query($"select price from BOARD where cpuno='{boardno}'")[0]);
                price += int.Parse(Connection.query($"select price from RAM where cpuno='{ramno}'")[0]) * ramnum;
                price += int.Parse(Connection.query($"select price from DISK_PC where cpuno='{diskno}'")[0]);
                price += int.Parse(Connection.query($"select price from GPU where cpuno='{gpuno}'")[0]);
                price += int.Parse(Connection.query($"select price from POWER_PC where cpuno='{powerno}'")[0]);
                price += int.Parse(Connection.query($"select price from CASE_PC where cpuno='{caseno}'")[0]);

                string mname = Connection.query($"select MNAME from Mem where mno='{mno}'")[0];



                dataGridView1.Rows[index].Cells[0].Value = listname;//名称
                dataGridView1.Rows[index].Cells[1].Value = price.ToString();//总价格
                dataGridView1.Rows[index].Cells[2].Value = mname;//发布人
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 12 * index];//发布时间
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 12 * index];//点赞数
                dataGridView1.Rows[index].Cells[5].Value = ab[5 + 12 * index];//收藏数
            }
        }


        //刷新论坛
        private void button10_Click(object sender, EventArgs e)
        {
            getListdata();
        }
        //查看论坛
        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void onTabSelectChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                getListdata();
            }
        }
    }
}


