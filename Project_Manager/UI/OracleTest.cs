using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project_Manager.ClassTest;

namespace Project_Manager.UI
{
    public partial class OracleTest : Form
    {
        OneAddOne oao = new OneAddOne();
        
        public OracleTest()
        {
            InitializeComponent();
        }

        private void fIR_ORA_TABLEBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.fIR_ORA_TABLEBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);

        }

        private void OracleTest_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dataSet1.FIR_ORA_TABLE”中。您可以根据需要移动或移除它。
            this.fIR_ORA_TABLETableAdapter.Fill(this.dataSet1.FIR_ORA_TABLE);
            oao.ShowResultEvent += new OneAddOne.ShowResult(this.RealShowResult);

        }
        private void RealShowResult(int i)
        {

            MessageBox.Show(i.ToString (),"事件运行结果...");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            oao.One =int .Parse ( this .textBox1 .Text) ;
        }
    }
}
