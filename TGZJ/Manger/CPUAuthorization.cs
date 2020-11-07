using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace TGZJ.Manger
{
    public partial class CPUAuthorization : Form
    {
        public CPUAuthorization()
        {
            InitializeComponent();
        }

        private void t_AuthorizationBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.t_AuthorizationBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.machineAuthorization);

        }

        private void CPUAuthorization_Load(object sender, EventArgs e)
        {
            this.t_AuthorizationTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.tableAdapterManager.Connection = this.t_AuthorizationTableAdapter.Connection;
            this.t_AuthorizationTableAdapter.Fill(this.machineAuthorization.T_Authorization);

        }

        private void CPUAuthorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Validate();
            this.t_AuthorizationBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.machineAuthorization);
        }
    }
}
