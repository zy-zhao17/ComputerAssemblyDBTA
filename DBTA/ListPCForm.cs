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
    public partial class ListPCForm : Form
    {
        private bool hasthumb;
        private bool hasfavo;
        private bool hasfollow;

        private string listno;
        private bool islogin;
        private string Imno;
        private string POSTERmno;



        public ListPCForm(string listno,bool islogin,string mno)
        {
            InitializeComponent();
            this.listno = listno;
            this.islogin = islogin;
            this.Imno = mno;

            

            //获取装机单
            //0      1        2    3    4     5        6     7      8     9      10      11    12
            //LISTNO LISTNAME MNO CPUNO FANNO BOARDNO RAMNO DISKNO GPUNO POWERNO CASENO RAMNUM POSTTIME
            List<string> ans = Connection.query($"select * from LIST_PC where listno='{listno}'");
            textBox1.Text = ans[1];
            this.POSTERmno = ans[2];
            textBox2.Text = Connection.query($"select CPUNAME from CPU_PC where CPUNO='{ans[3]}'")[0];
            textBox3.Text = Connection.query($"select FANNAME from FAN where FANNO='{ans[4]}'")[0];
            textBox4.Text = Connection.query($"select BOARDNAME from BOARD where BOARDNO='{ans[5]}'")[0];
            textBox5.Text = Connection.query($"select RAMNAME from RAM where RAMNO='{ans[6]}'")[0];
            textBox6.Text = Connection.query($"select DISKNAME from DISK_PC where DISKNO='{ans[7]}'")[0];
            textBox7.Text = Connection.query($"select GPUNAME from GPU where GPUNO='{ans[8]}'")[0];
            textBox8.Text = Connection.query($"select POWERNAME from POWER_PC where POWERNO='{ans[9]}'")[0];
            textBox9.Text = Connection.query($"select CASENAME from CASE_PC where CASENO='{ans[10]}'")[0];

            //获取评论
            dataGridView1.Rows.Clear();
            List<string>ab = Connection.query($"select Mem.MNAME,COMMENT_PC.COMTIME,COMMENT_PC.COMCONTENT from COMMENT_PC,MEM where MEM.MNO=COMMENT_PC.MNO and COMMENT_PC.LISTNO='{listno}'");
            int nrows = ab.Count / 3;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 3 * index];//评论人
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 3 * index];//评论时间
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 3 * index];//评论内容
            }
            //点赞、评论、收藏功能
            if(!islogin)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                textBox10.Enabled = false;
            }
            else
            {
                if (Connection.query($"select * from THUMB where MNO='{Imno}' and LISTNO='{this.listno}'").Count > 0)
                {
                    hasthumb = true;
                    button1.Text = "取消赞";
                }
                else
                {
                    hasthumb = false;
                    button1.Text = "点赞";
                }

                if (Connection.query($"select * from FAVOURITE where MNO='{Imno}' and LISTNO='{this.listno}'").Count > 0)
                {
                    hasfavo = true;
                    button2.Text = "取消收藏";
                }
                else
                {
                    hasfavo = false;
                    button2.Text = "收藏";
                }

                if (Connection.query($"select * from FOLLOW where MNO='{POSTERmno}' and FANSNO='{this.Imno}'").Count > 0)
                {
                    hasfollow = true;
                    button4.Text = "取消关注";
                }
                else
                {
                    hasfollow = false;
                    button4.Text = "关注作者！";
                }
            }
        }

        //点赞
        private void button1_Click(object sender, EventArgs e)
        {
            if (!hasthumb)
            {
                Connection.query($"insert into THUMB (MNO, LISTNO,THUMBTIME) values('{Imno}', '{listno}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}') ");
                button1.Text = "取消赞";
                hasthumb = true;
            }
            else
            {
                Connection.query($"delete from THUMB where MNO='{Imno}' and LISTNO='{listno}'");
                button1.Text = "点赞";
                hasthumb = false;

            }
        }

        //收藏
        private void button2_Click(object sender, EventArgs e)
        {
            if (!hasfavo)
            {
                Connection.query($"insert into FAVOURITE (MNO, LISTNO,FAVOTIME) values('{Imno}', '{listno}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}') ");
                button2.Text = "取消收藏";
                hasfavo = true;
            }
            else
            {
                Connection.query($"delete from FAVOURITE where MNO='{Imno}' and LISTNO='{listno}'");
                button2.Text = "收藏";
                hasfavo = false;

            }
        }

        //评论
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox10.Text==" ")
            {
                MessageBox.Show("请输入评论！");
                return;
            }
            Connection.query($"insert into COMMENT_PC (COMNO,LISTNO, MNO, COMTIME, COMCONTENT) values ('{Guid.NewGuid()}', '{listno}', '{Imno}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{textBox10.Text}')");
            //获取评论
            dataGridView1.Rows.Clear();
            List<string> ab = Connection.query($"select Mem.MNAME,COMMENT_PC.COMTIME,COMMENT_PC.COMCONTENT from COMMENT_PC,MEM where MEM.MNO=COMMENT_PC.MNO and COMMENT_PC.LISTNO='{listno}'");
            int nrows = ab.Count / 3;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 3 * index];//评论人
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 3 * index];//评论时间
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 3 * index];//评论内容
            }
            textBox10.Text = "";
        }

        //关注作者
        private void button4_Click(object sender, EventArgs e)
        {
            if (!hasfollow)
            {
                Connection.query($"insert into FOLLOW (MNO, FANSNO,FOLTIME) values('{POSTERmno}', '{Imno}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}') ");
                button4.Text = "取消关注";
                hasfollow = true;
            }
            else
            {
                Connection.query($"delete from FOLLOW where MNO='{POSTERmno}' and FANSNO='{Imno}'");
                button4.Text = "关注作者！";
                hasfollow = false;

            }
        }

        //关闭
        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
