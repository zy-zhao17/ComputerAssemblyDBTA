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
        private int cpuprice=0;
        private int fanprice=0;
        private int boardprice=0;
        private int ramprice=0;
        private int diskprice=0;
        private int powerprice=0;
        private int caseprice=0;
        private int gpuprice=0;
        private int ramnum=0;
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
                Connection.query($"insert into LIST_PC (LISTNO, LISTNAME, MNO, CPUNO, FANNO,BOARDNO,RAMNO,RAMNUM,DISKNO,GPUNO,POWERNO,CASENO,POSTTIME) values('{Guid.NewGuid()}', '{textBox7.Text}', '{a}', '{cpuno}', '{fanno}', '{boardno}', '{ramno}',{textBox8.Text}, '{diskno}', '{gpuno}', '{powerno}', '{caseno}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}') ");
                MessageBox.Show("保存装机单成功！");
            }
            else MessageBox.Show("您不是会员，无法保存装机单！");

        }


        private void Count()
        {

            if (textBox8.Text != "")
            {

                int.TryParse(textBox8.Text, out ramnum);

            }

            PRICE = cpuprice + gpuprice + fanprice + boardprice + ramnum * ramprice + diskprice + powerprice + caseprice;
            textBox1.Text = Convert.ToString(PRICE);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            Count();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Count();
        }




        private void onTabSelectChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {

                getListdata();



            }
        }

        private void getListdata(bool onlyfavo = false)
        {   //0      1        2    3    4     5        6     7      8     9      10      11    12
            //LISTNO LISTNAME MNO CPUNO FANNO BOARDNO RAMNO DISKNO GPUNO POWERNO CASENO RAMNUM POSTTIME
            List<string> ab;
            if (onlyfavo)
            {
                ab = Connection.query($"select list_PC.LISTNO,list_PC.LISTNAME,list_PC.MNO,list_PC.CPUNO,list_PC.FANNO,list_PC.BOARDNO,list_PC.RAMNO,list_PC.DISKNO,list_PC.GPUNO,list_PC.POWERNO,list_PC.CASENO,list_PC.RAMNUM,list_PC.POSTTIME from list_PC,FAVOURITE where list_PC.MNO='{text}' and list_PC.LISTNO=FAVOURITE.LISTNO");
            }
            else
            {
                ab = Connection.query("select * from list_PC");
            }

            dataGridView1.Rows.Clear();

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                BeginInvoke(new System.Threading.ThreadStart(() =>
                {

                    int nrows = ab.Count / 13;
                    for (int i = 0; i < nrows; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        int index = dataGridView1.Rows.Add(row);
                        string listno = ab[0 + 13 * index];
                        string listname = ab[1 + 13 * index];
                        string mno = ab[2 + 13 * index];
                        string cpuno = ab[3 + 13 * index];
                        string fanno = ab[4 + 13 * index];
                        string boardno = ab[5 + 13 * index];
                        string ramno = ab[6 + 13 * index];
                        string diskno = ab[7 + 13 * index];
                        string gpuno = ab[8 + 13 * index];
                        string powerno = ab[9 + 13 * index];
                        string caseno = ab[10 + 13 * index];
                        int ramnum = int.Parse(ab[11 + 13 * index]);
                        string posttime = ab[12 + 13 * index];

                        int price = 0;
                        price += int.Parse(Connection.query($"select price from CPU_PC where cpuno='{cpuno}'")[0]);
                        price += int.Parse(Connection.query($"select price from FAN where fanno='{fanno}'")[0]);
                        price += int.Parse(Connection.query($"select price from BOARD where boardno='{boardno}'")[0]);
                        price += int.Parse(Connection.query($"select price from RAM where ramno='{ramno}'")[0]) * ramnum;
                        price += int.Parse(Connection.query($"select price from DISK_PC where diskno='{diskno}'")[0]);
                        price += int.Parse(Connection.query($"select price from GPU where gpuno='{gpuno}'")[0]);
                        price += int.Parse(Connection.query($"select price from POWER_PC where powerno='{powerno}'")[0]);
                        price += int.Parse(Connection.query($"select price from CASE_PC where caseno='{caseno}'")[0]);

                        string mname = Connection.query($"select MNAME from Mem where mno='{mno}'")[0];

                        int nthumb = int.Parse(Connection.query($"select count(*) from thumb where LISTNO='{listno}'")[0]);
                        int nfavo = int.Parse(Connection.query($"select count(*) from favourite where LISTNO='{listno}'")[0]);


                        dataGridView1.Rows[index].Cells[0].Value = listname;//名称
                        dataGridView1.Rows[index].Cells[1].Value = price;//总价格
                        dataGridView1.Rows[index].Cells[2].Value = mname;//发布人
                        dataGridView1.Rows[index].Cells[3].Value = posttime;//发布时间
                        dataGridView1.Rows[index].Cells[4].Value = nthumb;//点赞数
                        dataGridView1.Rows[index].Cells[5].Value = nfavo;//收藏数
                        Refresh();
                    }
                }));
            }))
            { IsBackground = true }.Start();



        }


        private void showlist()
        {
            
        }

        //刷新论坛
        private void button10_Click(object sender, EventArgs e)
        {
            getListdata();
        }
        //查看具体装机单
        private void button11_Click(object sender, EventArgs e)
        {
            showlist();
        }

        //查看收藏
        private void button12_Click(object sender, EventArgs e)
        {
            if (islogin)
            {
                getListdata(true);
            }
            else
            {
                MessageBox.Show("请先登录！");
            }
        }

        private void onCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showlist();
        }
    }
}


