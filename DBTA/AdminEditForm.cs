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
    public partial class AdminEditForm : Form
    {

        Label[] labels = new Label[12];
        TextBox[] boxes = new TextBox[12];
        int num;
        int priornum;
        string tablename;
        string[] names;
        string[] keys;
        string[] contents;
        bool isupdate;

        public AdminEditForm(bool isupdate,int num, string tablename, string[] names,string[] keys,string[] contents,int priornum)
        {
            InitializeComponent();
            this.isupdate = isupdate;
            this.num = num;
            this.tablename = tablename;
            this.names = names;
            this.keys = keys;
            this.contents = contents;
            this.priornum = priornum;


            labels[0] = label1;
            labels[1] = label2;
            labels[2] = label3;
            labels[3] = label4;
            labels[4] = label5;
            labels[5] = label6;
            labels[6] = label7;
            labels[7] = label8;
            labels[8] = label9;
            labels[9] = label10;
            labels[10] = label11;
            labels[11] = label12;
            boxes[0] = textBox1;
            boxes[1] = textBox2;
            boxes[2] = textBox3;
            boxes[3] = textBox4;
            boxes[4] = textBox5;
            boxes[5] = textBox6;
            boxes[6] = textBox7;
            boxes[7] = textBox8;
            boxes[8] = textBox9;
            boxes[9] = textBox10;
            boxes[10] = textBox11;
            boxes[11] = textBox12;
            if (isupdate)
            {
                for (int i = 0; i < priornum; i++)
                {
                    labels[i].Enabled = false;
                    boxes[i].Enabled = false;
                }
            }


            for (int i = 0; i < num; i++)
            {
                labels[i].Text = names[i];
                boxes[i].Text = contents[i];
                //删除char前面的空格
                while((boxes[i].Text.Count()>0)&&(boxes[i].Text[boxes[i].Text.Count()-1]==' '))
                {
                    boxes[i].Text=boxes[i].Text.Remove(boxes[i].Text.Count() - 1);
                }

            }
            for(int i = num; i < 12; i++)
            {
                labels[i].Visible = false;
                labels[i].Enabled = false;
                boxes[i].Visible = false;
                boxes[i].Enabled = false;
            }
        }

        //提交
        private void button1_Click(object sender, EventArgs e)
        {
            if (isupdate)
            {
                for (int i = 0; i < num; i++)
                {
                    if (boxes[i].Text == "")
                    {
                        MessageBox.Show($"请填写{names[i]}！");
                        return;
                    }
                }

                if (priornum == 1)
                {
                    for (int i = 1; i < num; i++)
                    {
                        Connection.query($"UPDATE {tablename} SET {keys[i]} = '{boxes[i].Text}' WHERE {keys[0]} = '{boxes[0].Text}'");
                    }
                }
                else
                {
                    for (int i = 2; i < num; i++)
                    {
                        Connection.query($"UPDATE {tablename} SET {keys[i]} = '{boxes[i].Text}' WHERE {keys[0]} = '{boxes[0].Text}' AND  {keys[1]} = '{boxes[1].Text}'");
                    }
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder("insert into ");
                sb.Append(tablename);
                sb.Append(" (");
                for(int i = 0; i < num-1; i++)
                {
                    sb.Append(keys[i]);
                    sb.Append(", ");
                }
                sb.Append(keys[num - 1]);
                sb.Append(") values('");
                for(int i = 0; i < num - 1; i++)
                {
                    sb.Append(boxes[i].Text);
                    sb.Append("', '");
                }
                sb.Append(boxes[num - 1].Text);
                sb.Append("')");
                Connection.query(sb.ToString());

                //"insert into CPU_PC (CPUNO, CPUNAME,BRAND,SLOT,CPUCORE,INTEGRAPH,PRICE) values('CPU00010', '酷睿i9 10900K', 'Intel', 'LGA 1200', 10, 'Y', 4099); ";
            }



            Close();
        }

        //退出
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
