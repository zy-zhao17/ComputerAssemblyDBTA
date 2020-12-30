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
            new string[]{"装机单编号","装机单名称","发布者账号","CPU编号","散热器编号","主板编号","内存编号","内存条数","硬盘编号","显卡编号","电源编号","机箱编号"},
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
            new string[]{"DISKNO","DISKNAME","VOLUME","DISKTYPE","SLOT","SHAPE","PRICE"},
            new string[]{"GPUNO","GPUNAME","VOLUME","BITS","SLOT","PRICE"},
            new string[]{"POWERNO","POWERNAME","P","POWERTYPE","PRICE"},
            new string[]{"CASENO ","CASENAME","STRUC","PANEL","PRICE"},
            new string[]{"LISTNO","LISTNAME","MNO","CPUNO","FANNO","BOARDNO","RAMNO","RAMNUM","DISKNO","GPUNO","POWERNO","CASENO"},
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

        private void button1_Click(object sender, EventArgs e)
        {

            int idx = comboBox1.SelectedIndex;
            int len= itemname[idx].Count(); 

            DataTable dt = new DataTable();
            dataGridView1.DataSource = dt;
            for (int i = 0; i < len; i++)
            {
                dt.Columns.Add(itemname[idx][i]);
            }
            if (idx >= 1)
            {
                List<string> ans = Connection.query($"select * from {comboBox1.Text}");
                int nrows = ans.Count / len;
                for(int i = 0; i < nrows; i++)
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
    }
}
