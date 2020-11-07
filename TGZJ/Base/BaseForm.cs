using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TGZJ.Base
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();

           

           
            
        }

        private bool  RegisterSelf(string  parentcontrol, string selfname)
        {
            return true;
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            RegisterSelf(parent_model, this.Text);
        }
    }
}
