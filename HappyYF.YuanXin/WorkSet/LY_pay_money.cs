using System;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Text.RegularExpressions;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_pay_money : Form
    {
        public string contract_code_add;
        public decimal moeny;
        public decimal max_moeny;
        public string txtRemark;
        public LY_pay_money()
        {
            InitializeComponent();
        }

       

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(contract_code.Text))
            {
                MessageBox.Show("合同编号不能为空！"); return;
            }
            if (string.IsNullOrEmpty(contract_pay_money.Text))
            {
                MessageBox.Show("预付金额不能为空！"); contract_pay_money.Focus(); return;
            }
            if (!(Regex.IsMatch(this.contract_pay_money.Text, @"^([-]{1}[0-9]+(.[0-9]{2})?$)|([0-9]+(.[0-9]{2})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("数字格式错误，请重新输入");
                this.contract_pay_money.Focus();
                return; 
            }
            decimal txt_money = decimal.Parse(this.contract_pay_money.Text);
            if (txt_money <= 0)
            {
                MessageBox.Show("金额不能小于等于0，请重新输入");
                this.contract_pay_money.Focus();
                return;
            }
            if(txt_money > max_moeny)
            {
                MessageBox.Show("申请金额不能大于合同金额，请重新输入");
                this.contract_pay_money.Focus();
                return;
            }
            moeny = txt_money;
            txtRemark = txt_remark.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();




        }

        private void LY_pay_money_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(contract_code_add))
            {
                 contract_code.Text= contract_code_add ;
            }
        }
    }
}
