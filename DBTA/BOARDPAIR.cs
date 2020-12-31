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
    public partial class BOARDPAIR : Form
    {
        public string CPUBOARD;
        public BOARDPAIR(string a)
        {
            CPUBOARD = a;
            InitializeComponent();
            Table();
        }

        public void Table()
        {
            dataGridView1.Rows.Clear();

            List<string> ab = Connection.query($"select a.CPUNO,a.CPUNAME,a.BRAND,a.SLOT,a.CPUCORE,A.INTEGRAPH,a.PRICE  from CPU_PC a, PAIR b  WHERE a.CPUNO=b.CPUNO AND b.BOARDNO='{CPUBOARD}'");
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
    }
}
