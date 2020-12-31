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
    public partial class BOARD : Form
    {
        public string BOARDname;
        public string BOARDNO;
        public string BOARDPRICE;
        public bool BOARDselect = false;
        public BOARD()
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

            List<string> ab = Connection.query($"select * from BOARD");
            int nrows = ab.Count / 6;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 6 * index];
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 6 * index];
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 6 * index];
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 6 * index];
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 6 * index];
                dataGridView1.Rows[index].Cells[5].Value = ab[5 + 6 * index];
            }
        }
        public void TableID()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select * from BOARD WHERE BOARDUSAGE='{textBox1.Text}'");
            int nrows = ab.Count / 6;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 6 * index];
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 6 * index];
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 6 * index];
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 6 * index];
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 6 * index];
                dataGridView1.Rows[index].Cells[5].Value = ab[5 + 6 * index];
            }
        }

        public void TableID2()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select * from GPU WHERE SHAPE='{textBox2.Text}'");
            int nrows = ab.Count / 6;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 6 * index];
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 6 * index];
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 6 * index];
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 6 * index];
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 6 * index];
                dataGridView1.Rows[index].Cells[5].Value = ab[5 + 6 * index];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                BOARDselect = true;
                string id = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//获取BOARD的名字
                string no = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string price = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                BOARDPRICE = price;
                BOARDNO = no;
                BOARDname = id;
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

        private void button3_Click(object sender, EventArgs e)
        {
            TableID2();
        }
    }
}
