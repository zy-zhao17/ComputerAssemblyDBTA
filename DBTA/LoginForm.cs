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
    public partial class LoginForm : Form
    {
        public string namestr;
        public bool islogin = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//登录
        {
            List<string> ans = Connection.query($"select MNAME from Mem where MNO='{textBox1.Text}' and MPSWD='{textBox2.Text}'");
            if (ans.Count>0)
            {
                islogin = true;
                namestr = ans[0];
                Close();
            }
            else
            {
                MessageBox.Show("密码错误或用户名不存在！");
            }
        }

        private void button2_Click(object sender, EventArgs e)//注册
        {
            new RegisterForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)//取消
        {
            islogin = false;
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//管理员登录
        {
            if (Connection.query($"select ANO from Administrator where ANO='{textBox1.Text}' and APSWD='{textBox2.Text}'").Count > 0)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                new AdminForm().ShowDialog();
            }
            else MessageBox.Show("你不是管理员！");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
