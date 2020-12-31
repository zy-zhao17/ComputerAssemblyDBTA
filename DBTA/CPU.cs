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
    public partial class CPU : Form
    {
        public string CPUname;
        public string CPUNO;
        public bool CPUselect = false;
        public CPU()
        {
            InitializeComponent();
            Table();
        }
        public void Table()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select * from CPU_PC");
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

            List<string> ab = Connection.query($"select * from CPU_PC WHERE BRAND='{textBox1.Text}'");
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

            List<string> ab = Connection.query($"select * from GPU WHERE CPUCORE={textBox2.Text}");
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CPU_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TableID();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TableID2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                CPUselect = true;
                string id = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//获取GPU的名字
                string no = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                CPUNO = no;
                CPUname = id;
                Close();
            }
            catch
            {

            }
        }
    }
}
