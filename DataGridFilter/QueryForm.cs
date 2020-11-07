using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace DataGridFilter
{
    
    
    
    public partial class QueryForm : Form
    {
        BindingSource bindingSource1 = new BindingSource();

        private string constr;

        public string Constr
        {
            get { return constr; }
            set { constr = value; }
        }
        
        private string sel;

        public string Sel
        {
            get { return sel; }
            set { sel = value; }
        }
        private string fwhere;

        public string Fwhere
        {
            get { return fwhere; }
            set { fwhere = value; }
        }

        private string result;

        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        private string result1;

        public string Result1
        {
            get { return result1; }
            set { result1 = value; }
        }

        private string result2;

        public string Result2
        {
            get { return result2; }
            set { result2 = value; }
        }

        private string result3;

        public string Result3
        {
            get { return result3; }
            set { result3 = value; }
        }

        private string result4;

        public string Result4
        {
            get { return result4; }
            set { result4 = value; }
        }
        private string result5;
        public string Result5
        {
            get { return result5; }
            set { result5 = value; }
        }

        private string result6;
        public string Result6
        {
            get { return result6; }
            set { result6= value; }
        }

        private string result7;
        public string Result7
        {
            get { return result7; }
            set { result7 = value; }
        }
        private int nodiscol = 0;

        public int Nodiscol
        {
            get { return nodiscol; }
            set { nodiscol = value; }
        }

       
        
        public QueryForm( )
        {
            InitializeComponent();
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
             //this.bindingSource1 .Filter =    
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {

            SqlDataAdapter myAdapter = new SqlDataAdapter(sel, constr );

            DataSet allData = new DataSet();


            myAdapter.Fill(allData);
            myAdapter.Dispose();


            this.dataGridView1.Columns.Clear();

            this.dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();


            bindingSource1.DataSource = allData.Tables[0];
            dataGridView1.DataSource = bindingSource1;

            
            bindingSource1.ResetBindings(true);

            if (0 != this.nodiscol && dataGridView1.Columns.Count >=nodiscol )
            {

                for (int i = nodiscol; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Visible = false;
                }
            
            
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            
            // filter = dfilter + 
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //string dFilter = "";

            //for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
            //{
            //    if (i != this.dataGridView1.ColumnCount -1)
            //         dFilter = dFilter + this.dataGridView1.Columns[i].Name + " like  '*" + this .textBox1 .Text + "*' or ";
            //    else
            //        dFilter = dFilter + this.dataGridView1.Columns[i].Name + " like  '*" + this.textBox1.Text + "*' ";
                
            //}

            //this.bindingSource1.Filter = dFilter;

            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.textBox1.Text);

            this.bindingSource1.Filter = filterString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView1.CurrentRow)
            {
                return;
            }
            
            this.result = this.dataGridView1.CurrentRow.Cells[0].Value.ToString ();
            if ( this.dataGridView1.CurrentRow .Cells .Count >1)
            {
                this.result1 = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            if (this.dataGridView1.CurrentRow.Cells.Count > 2)
            {
                this.result2 = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            if (this.dataGridView1.CurrentRow.Cells.Count >3)
            {
                this.result3 = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            if (this.dataGridView1.CurrentRow.Cells.Count > 4)
            {
                this.result4 = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            if (this.dataGridView1.CurrentRow.Cells.Count > 5)
            {
                this.result5 = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            if (this.dataGridView1.CurrentRow.Cells.Count > 6)
            {
                this.result6 = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            if (this.dataGridView1.CurrentRow.Cells.Count > 7)
            {
                this.result7= this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }
            this.Close();
        }

        
    }
}
