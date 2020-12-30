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
    
    public partial class GPU : Form
    {
        public string GPUname;
        public bool GPUselect = false;

        public GPU()
        {
            InitializeComponent();
            Table();
        }

        private void GPU_Load(object sender, EventArgs e)
        {

        }
        //从数据库读取数据显示在表格文件中
        public void Table()
        {
            dataGridView1.Rows.Clear();
            
            List<string> ab = Connection.query($"select * from GPU");
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
        //根据容量查询数据
        public void TableID()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select * from GPU WHERE VOLUME='{textBox1.Text}'");
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

            List<string> ab = Connection.query($"select * from GPU WHERE BITS='{textBox2.Text}'");
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
                GPUselect = true;
                string id = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//获取GPU的名字
                GPUname = id;
                Close();
            }
            catch
            {

            }
        }
      
    }
}
