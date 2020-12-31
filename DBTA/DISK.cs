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
    public partial class DISK : Form
    {
        public string DISKname;
        public string DISKNO;
        public bool DISKselect = false;
        public DISK()
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

            List<string> ab = Connection.query($"select * from DISK_PC");
            int nrows = ab.Count / 7;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 7 * index];
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 7 * index];
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 7 * index];
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 7 * index];
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 7 * index];
                dataGridView1.Rows[index].Cells[5].Value = ab[5 + 7 * index];
                dataGridView1.Rows[index].Cells[6].Value = ab[6 + 7 * index];
            }
        }
        //根据容量查询数据
        public void TableID()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select * from DISK_PC WHERE VOLUME= '{textBox1.Text}'");
            int nrows = ab.Count / 7;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 7 * index];
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 7 * index];
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 7 * index];
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 7 * index];
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 7 * index];
                dataGridView1.Rows[index].Cells[5].Value = ab[5 + 7 * index];
                dataGridView1.Rows[index].Cells[6].Value = ab[6 + 7 * index];
            }
        }

        public void TableID2()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select * from DISK_PC WHERE DISKTYPE='{textBox2.Text}'");
            int nrows = ab.Count / 7;
            for (int i = 0; i < nrows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = ab[0 + 7 * index];
                dataGridView1.Rows[index].Cells[1].Value = ab[1 + 7 * index];
                dataGridView1.Rows[index].Cells[2].Value = ab[2 + 7 * index];
                dataGridView1.Rows[index].Cells[3].Value = ab[3 + 7 * index];
                dataGridView1.Rows[index].Cells[4].Value = ab[4 + 7 * index];
                dataGridView1.Rows[index].Cells[5].Value = ab[5 + 7 * index];
                dataGridView1.Rows[index].Cells[6].Value = ab[6 + 7 * index];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                DISKselect = true;
                string id = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//获取DISK的名字
                string no = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                DISKname = id;
                DISKNO = no;
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
