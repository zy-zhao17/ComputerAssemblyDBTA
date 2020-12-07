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
            islogin = true;
            namestr = textBox1.Text;
            Close();
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
    }
}
