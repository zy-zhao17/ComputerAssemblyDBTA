﻿using System;
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
    public partial class @case : Form
    {
        public string CASEname;
        public bool CASEselect = false;
        public string CASENO;
        public @case()
        {
            InitializeComponent();
            Table();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void Table()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select * from CASE_PC");
            int nrows = ab.Count / 5;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 5 * index];
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 5 * index];
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 5 * index];
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 5 * index];
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 5 * index];
            }
        }
        //根据容量查询数据
        public void TableID()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select * from CASE_PC WHERE PANEL LIKE '%{textBox1.Text}%'");
            int nrows = ab.Count / 5;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 5 * index];
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 5 * index];
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 5 * index];
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 5 * index];
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 5 * index];

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                CASEselect = true;
                string id = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//获取CASE的名字
                string no= dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                CASENO = no;
                CASEname = id;
                Close();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TableID();
        }
    }
}
