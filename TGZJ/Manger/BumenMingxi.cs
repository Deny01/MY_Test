using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TGZJ.Manger
{
    public partial class BumenMingxi : Form
    {
        public BumenMingxi()
        {
            InitializeComponent();
        }

        private void t_bumenBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.t_bumenBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bumenDataSet);

        }

        private void BumenMingxi_Load(object sender, EventArgs e)
        {
         
            this.t_bumenTableAdapter.Fill(this.bumenDataSet.T_bumen);

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }
    }
}
