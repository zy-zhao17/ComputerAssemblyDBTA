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
    }
}


