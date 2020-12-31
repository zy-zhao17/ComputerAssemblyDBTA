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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        //注册
        private void button1_Click(object sender, EventArgs e)
        {
            //MNO	MPSWD	MNAME	MBIRTHDAY	MEMAIL	MTEL	MPOINT
            //"insert into CPU_PC (CPUNO, CPUNAME,BRAND,SLOT,CPUCORE,INTEGRAPH,PRICE) values('CPU00010', '酷睿i9 10900K', 'Intel', 'LGA 1200', 10, 'Y', 4099); ";
            if (textBox3.Text.Equals(textBox4.Text))
            {
                Connection.query($"insert into Mem (MNO,MPSWD,MNAME,MBIRTHDAY,MEMAIL,MTEL,MPOINT) values('{textBox1.Text}','{textBox3.Text}','{textBox2.Text}','{textBox5.Text}','{textBox6.Text}','{textBox7.Text}','0')");
                Close();
            }
            else
            {
                MessageBox.Show("两次输入密码不一致！");
            }
        }

        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
