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
    public partial class AdminForm : Form
    {

        public string retstr;
        private static readonly List<string[]> itemname = new List<string[]>{
            new string[]{},
            new string[]{"管理员账号","管理员密码","管理员姓名","管理员邮箱","管理员电话"},
            new string[]{"会员账号","会员密码","会员昵称","会员生日","会员邮箱","会员电话","会员积分"},
            new string[]{"CPU编号","CPU名称","CPU品牌","插槽类型","核心数量","有无集显","市场价格"},
            new string[]{"散热器编号","散热器名称","适用范围","转速描述","价格"},
            new string[]{"主板编号","主板名称","插槽类型","主板板型","内存类型","市场价格"},
            new string[]{"内存编号","内存名称","内存容量","内存代数","内存频率","工作电压","市场价格"},
            new string[]{"硬盘编号","硬盘名称","硬盘容量","硬盘类型","接口类型","外形尺寸","市场价格"},
            new string[]{"显卡编号","显卡名称","显存容量","显存位宽","接口类型","市场价格"},
            new string[]{"电源编号","电源名称","额定功率","出线类型","市场价格"},
            new string[]{"机箱编号","机箱名称","机箱结构","面板接口","市场价格"},
            new string[]{"装机单编号","装机单名称","发布者账号","CPU编号","散热器编号","主板编号","内存编号","硬盘编号","显卡编号","电源编号","机箱编号","内存条数"},
            new string[]{"评论编号","装机单编号","发布者账号","评论时间","评论内容"},
            new string[]{"CPU编号","主板编号","某宝链接"},
            new string[]{"点赞者的账号","装机单编号","点赞时间"},
            new string[]{"收藏者的账号","装机单编号","收藏时间"},
            new string[]{"关注者的账号","被关注者的账号","关注时间"},
        };
        private static readonly List<string[]> itemkey = new List<string[]>{
            new string[]{},
            new string[]{"ANO","APSWD","ANAME","AEMAIL","ATEL"},
            new string[]{"MNO","MPSWD","MNAME","MBIRTHDAY","MEMAIL","MTEL","MPOINT"},
            new string[]{"CPUNO","CPUNAME","BRAND","SLOT","CPUCORE","INTEGRAPH","PRICE"},
            new string[]{"FANNO","FANNAME","FANUSAGE","SPEED","PRICE"},
            new string[]{"BOARDNO","BOARDNAME","BOARDUSAGE","SHAPE","RAMSLOT","PRICE"},
            new string[]{"RAMNO","RAMNAME","VOLUME","GEN","FREQ","VOLT","PRICE"},
            new string[]{"DISKNO","DISKNAME","VOLUME","DISKTYPE", "SOLT", "SHAPE","PRICE"},
            new string[]{"GPUNO","GPUNAME","VOLUME","BITS","SLOT","PRICE"},
            new string[]{"POWERNO","POWERNAME","P","POWERTYPE","PRICE"},
            new string[]{"CASENO ","CASENAME","STRUC","PANEL","PRICE"},
            new string[]{"LISTNO","LISTNAME","MNO","CPUNO","FANNO","BOARDNO","RAMNO","DISKNO","GPUNO","POWERNO","CASENO","RAMNUM"},
            new string[]{"COMNO","LISTNO","MNO","COMTIME","COMCONTENT"},
            new string[]{"CPUNO","BOARDNO","LINK_TAO"},
            new string[]{"MNO","LISTNO","THUMBTIME"},
            new string[]{"MNO","LISTNO","FAVOTIME"},
            new string[]{"FANSNO","MNO","FOLTIME"},
        };



        public AdminForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }



        private void adddata()
        {



        }

        //删除数据时，应当满足表级完整性约束！
        private void deletedata()
        {
            //dataGridView1.SelectedRows;
            int idx = comboBox1.SelectedIndex;
            switch (idx)
            {
                case 0://没有选择表
                    MessageBox.Show("请先选择一个数据表！");
                    break;
                case 1://Administrator
                    MessageBox.Show("不能删除管理员！");
                    break;
                case 2://Mem
                    if (MessageBox.Show("确认删除选中的会员吗？如果删除，其对应的装机单、点赞、关注、被关注、收藏、评论记录都会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            List<string> mnos = Connection.query($"select MNO from Mem where MNO='{dr.Cells[0].Value}'");
                            foreach (string mno in mnos)
                            {
                                //删除会员对应的点赞、关注、被关注、收藏、评论记录
                                Connection.query($"delete from THUMB where MNO='{mno}'");
                                Connection.query($"delete from FOLLOW where MNO='{mno}'");
                                Connection.query($"delete from FOLLOW where FANSNO='{mno}'");
                                Connection.query($"delete from FAVOURITE where MNO='{mno}'");
                                Connection.query($"delete from COMMENT_PC where MNO='{mno}'");
                                //删除会员对应的装机单，首先查询会员提交的装机单。
                                List<string> listnos = Connection.query($"select LISTNO from LIST_PC where MNO='{mno}'");
                                foreach (string listno in listnos)
                                {
                                    //删除此装机单对应的点赞、评论和收藏
                                    Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                    Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                    Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                    //删除此装机单
                                    Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                                }
                                //删除会员
                                Connection.query($"delete from Mem where MNO='{mno}'");
                            }
                        }
                    }
                    break;
                case 3://CPU_PC
                    if (MessageBox.Show("确认删除选中的CPU吗？如果删除，包含此CPU的装机单和板U套装都会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string cpuno = dr.Cells[0].Value.ToString();
                            //删除板U套装
                            Connection.query($"delete from PAIR where CPUNO='{cpuno}'");

                            //删除CPU对应的装机单，首先查询CPU对应的装机单。
                            List<string> listnos = Connection.query($"select LISTNO from LIST_PC where CPUNO='{cpuno}'");
                            foreach (string listno in listnos)
                            {
                                //删除此装机单对应的点赞、评论和收藏
                                Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                //删除此装机单
                                Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                            }
                            //删除此CPU
                            Connection.query($"delete from CPU_PC where CPUNO='{cpuno}'");
                        }
                    }
                    break;
                case 4://FAN
                    if (MessageBox.Show("确认删除选中的散热器吗？如果删除，包含此散热器的装机单也会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string fanno = dr.Cells[0].Value.ToString();
                            //删除fan对应的装机单，首先查询fan对应的装机单。
                            List<string> listnos = Connection.query($"select LISTNO from LIST_PC where fanNO='{fanno}'");
                            foreach (string listno in listnos)
                            {
                                //删除此装机单对应的点赞、评论和收藏
                                Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                //删除此装机单
                                Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                            }
                            //删除此fan
                            Connection.query($"delete from FAN where fanNO='{fanno}'");
                        }
                    }
                    break;
                case 5://BOARD
                    if (MessageBox.Show("确认删除选中的主板吗？如果删除，包含此主板的装机单和板U套装都会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string boardno = dr.Cells[0].Value.ToString();
                            //删除板U套装
                            Connection.query($"delete from PAIR where BOARDNO='{boardno}'");
                            //删除board对应的装机单，首先查询board对应的装机单。
                            List<string> listnos = Connection.query($"select LISTNO from LIST_PC where boardNO='{boardno}'");
                            foreach (string listno in listnos)
                            {
                                //删除此装机单对应的点赞、评论和收藏
                                Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                //删除此装机单
                                Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                            }
                            //删除此board
                            Connection.query($"delete from BOARD where boardNO='{boardno}'");
                        }
                    }
                    break;
                case 6://RAM
                    if (MessageBox.Show("确认删除选中的内存吗？如果删除，包含此内存的装机单也会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string ramno = dr.Cells[0].Value.ToString();
                            //删除ram对应的装机单，首先查询ram对应的装机单。
                            List<string> listnos = Connection.query($"select LISTNO from LIST_PC where ramNO='{ramno}'");
                            foreach (string listno in listnos)
                            {
                                //删除此装机单对应的点赞、评论和收藏
                                Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                //删除此装机单
                                Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                            }
                            //删除此ram
                            Connection.query($"delete from RAM where ramNO='{ramno}'");
                        }
                    }
                    break;
                case 7://DISK_PC
                    if (MessageBox.Show("确认删除选中的硬盘吗？如果删除，包含此硬盘的装机单也会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string diskno = dr.Cells[0].Value.ToString();
                            //删除disk对应的装机单，首先查询disk对应的装机单。
                            List<string> listnos = Connection.query($"select LISTNO from LIST_PC where diskNO='{diskno}'");
                            foreach (string listno in listnos)
                            {
                                //删除此装机单对应的点赞、评论和收藏
                                Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                //删除此装机单
                                Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                            }
                            //删除此disk
                            Connection.query($"delete from DISK_PC where diskNO='{diskno}'");
                        }
                    }
                    break;
                case 8://GPU
                    if (MessageBox.Show("确认删除选中的显卡吗？如果删除，包含此显卡的装机单也会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string gpuno = dr.Cells[0].Value.ToString();
                            //删除gpu对应的装机单，首先查询gpu对应的装机单。
                            List<string> listnos = Connection.query($"select LISTNO from LIST_PC where gpuNO='{gpuno}'");
                            foreach (string listno in listnos)
                            {
                                //删除此装机单对应的点赞、评论和收藏
                                Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                //删除此装机单
                                Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                            }
                            //删除此gpu
                            Connection.query($"delete from GPU where gpuNO='{gpuno}'");
                        }
                    }
                    break;
                case 9://POWER_PC
                    if (MessageBox.Show("确认删除选中的电源吗？如果删除，包含此电源的装机单也会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string powerno = dr.Cells[0].Value.ToString();
                            //删除power对应的装机单，首先查询power对应的装机单。
                            List<string> listnos = Connection.query($"select LISTNO from LIST_PC where powerNO='{powerno}'");
                            foreach (string listno in listnos)
                            {
                                //删除此装机单对应的点赞、评论和收藏
                                Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                //删除此装机单
                                Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                            }
                            //删除此power
                            Connection.query($"delete from POWER_PC where powerNO='{powerno}'");
                        }
                    }
                    break;
                case 10://CASE_PC
                    if (MessageBox.Show("确认删除选中的机箱吗？如果删除，包含此机箱的装机单也会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string caseno = dr.Cells[0].Value.ToString();
                            //删除case对应的装机单，首先查询case对应的装机单。
                            List<string> listnos = Connection.query($"select LISTNO from LIST_PC where caseNO='{caseno}'");
                            foreach (string listno in listnos)
                            {
                                //删除此装机单对应的点赞、评论和收藏
                                Connection.query($"delete from THUMB where LISTNO='{listno}'");
                                Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                                Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                                //删除此装机单
                                Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                            }
                            //删除此case
                            Connection.query($"delete from CASE_PC where caseNO='{caseno}'");
                        }
                    }
                    break;
                case 11://LIST_PC
                    if (MessageBox.Show("确认删除选中的装机单吗？如果删除，此装机单对应的点赞、评论和收藏也会被删除！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string listno = dr.Cells[0].Value.ToString();
                            //删除此装机单对应的点赞、评论和收藏
                            Connection.query($"delete from THUMB where LISTNO='{listno}'");
                            Connection.query($"delete from COMMENT_PC where LISTNO='{listno}'");
                            Connection.query($"delete from FAVOURITE where LISTNO='{listno}'");
                            //删除此装机单
                            Connection.query($"delete from LIST_PC where LISTNO='{listno}'");
                        }
                    }
                    break;
                case 12://COMMENT_PC
                    if (MessageBox.Show("确认删除选中的评论吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string comno = dr.Cells[0].Value.ToString();
                            Connection.query($"delete from COMMENT_PC where COMNO='{comno}'");
                        }
                    }
                    break;
                case 13://PAIR
                    if (MessageBox.Show("确认删除选中的板U套装吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string cpuno = dr.Cells[0].Value.ToString();
                            string boardno = dr.Cells[1].Value.ToString();
                            Connection.query($"delete from PAIR where CPUNO='{cpuno}' and BOARDNO='{boardno}'");
                        }
                    }
                    break;
                case 14://THUMB
                    if (MessageBox.Show("确认删除选中的点赞吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string mno = dr.Cells[0].Value.ToString();
                            string listno = dr.Cells[1].Value.ToString();
                            Connection.query($"delete from THUMB where MNO='{mno}' and LISTNO='{listno}'");
                        }
                    }
                    break;
                case 15://FAVOURITE
                    if (MessageBox.Show("确认删除选中的收藏吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string mno = dr.Cells[0].Value.ToString();
                            string listno = dr.Cells[1].Value.ToString();
                            Connection.query($"delete from FAVOURITE where MNO='{mno}' and LISTNO='{listno}'");
                        }
                    }
                    break;
                case 16://FOLLOW
                    if (MessageBox.Show("确认删除选中的关注吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            string fansno = dr.Cells[0].Value.ToString();
                            string mno = dr.Cells[1].Value.ToString();
                            Connection.query($"delete from FOLLOW where FANSNO='{fansno}' and MNO='{mno}'");
                        }
                    }
                    break;
                default:
                    MessageBox.Show("选择不正确！");
                    break;
            }
            getdata();

        }


        private void modifydata()
        {
            int idx = comboBox1.SelectedIndex;
            if (idx == 0)
            {
                MessageBox.Show("请先选择一个数据表！");
                return;
            }
            int nums = itemname[idx].Count();
            string[] contents = new string[nums];
            int rowidx = dataGridView1.SelectedRows.Count - 1;
            DataGridViewRow dr = dataGridView1.SelectedRows[rowidx];

            int prior = 1;
            if (idx > 12) prior = 2;

            for (int i = 0; i < nums; i++)
            {
                contents[i] = dr.Cells[i].Value.ToString();
            }
            new AdminEditForm(nums,comboBox1.Text, itemname[idx],itemkey[idx], contents, prior).ShowDialog();
            getdata();
        }


        private void getdata()
        {
            int idx = comboBox1.SelectedIndex;
            int len = itemname[idx].Count();

            DataTable dt = new DataTable();
            dataGridView1.DataSource = dt;
            for (int i = 0; i < len; i++)
            {
                dt.Columns.Add(itemname[idx][i]);
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            if (idx >= 1)
            {
                List<string> ans = Connection.query($"select * from {comboBox1.Text}");
                int nrows = ans.Count / len;
                for (int i = 0; i < nrows; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < len; j++)
                    {
                        dr[j] = ans[j + len * i];
                    }
                    dt.Rows.Add(dr);

                }
            }
        }

        //获取数据
        private void button1_Click(object sender, EventArgs e)
        {
            getdata();
        }

        //修改数据
        private void OnCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            modifydata();
            getdata();
        }

        //增加数据
        private void button2_Click(object sender, EventArgs e)
        {
            adddata();
        }

        //删除数据
        private void button3_Click(object sender, EventArgs e)
        {
            deletedata();
        }

        //修改数据
        private void button4_Click(object sender, EventArgs e)
        {
            modifydata();
            getdata();
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            getdata();
        }
    }
}
