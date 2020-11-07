using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;

namespace DataGridFilter
{
   public  class GroupQueryItem
    {
        private string gq_Name;

        public string 项目
        {
            get { return gq_Name; }
            set { gq_Name = value; }
        }
        private string gq_DatacolumnName;

        //public string 表字段
        //{
        //    get { return gq_DatacolumnName; }
        //    set { gq_DatacolumnName = value; }
        //}
        private string gq_DatatableName;

        //public string 表名
        //{
        //    get { return gq_DatatableName; }
        //    set { gq_DatatableName = value; }
        //}

        private string in_controlName;

        public string 条件
        {
            get { return in_Control.Text; }
            set { in_controlName = value; }
        }

       private Control in_Control=null;

       //public Control In_Control
       //{
       //    get { return in_Control; }
       //    set { in_Control = value; }
       //}



       public GroupQueryItem()
       { 
       
       }

       public GroupQueryItem(string nowgq_Name,string nowgq_DatacolumnName,string nowgq_DatatableName,Control now_inControl)
            : this()
        {
            this.gq_Name = nowgq_Name;
            this.gq_DatacolumnName = nowgq_DatacolumnName;
            this.gq_DatatableName = nowgq_DatatableName;
            this.in_Control = now_inControl;
            
        }

       //string header = " select ";
       //     string ender = " group by ";

           
       //    // string sWhere = "  where ((a.service_number IS NULL) OR (a.service_number is not null)) ";

       //     string sWhere
       public  string  MakeHeader()
       {
           string nowheader = gq_DatatableName + "." + gq_DatacolumnName + " as " + gq_Name + ",";

           if ("工件编号" == gq_Name)
           {
               nowheader = gq_DatatableName + "." + gq_DatacolumnName + " as " + gq_Name + ",a.mch as 工件名称,";
           }

           if ("工人" == gq_Name)
           {
               nowheader = gq_DatatableName + "." + gq_DatacolumnName + " as " + gq_Name + ",a.work_code as 工号,";
           }

           if ("生产计划" == gq_Name)
           {
               nowheader = gq_DatatableName + "." + gq_DatacolumnName + " as " + gq_Name + ",a.remark as 计划说明,";
           }
           /////////////////////////////////////////////////

           if ("成品编号" == gq_Name)
           {
               nowheader = gq_DatatableName + "." + gq_DatacolumnName + " as " + gq_Name + ",a.mch as 工件名称,isnull(a.xhc,'') as 规格型号,";
           }

           return nowheader;
       
       }

       public  string MakeEnder()
       {
           string nowender = gq_DatatableName + "." + gq_DatacolumnName  + ",";

           if ("工件编号" == gq_Name)
            {
                nowender = gq_DatatableName + "." + gq_DatacolumnName +  ",a.mch,";
            }

           if ("工人" == gq_Name)
           {
               nowender = gq_DatatableName + "." + gq_DatacolumnName +  ",a.work_code,";
           }

           if ("生产计划" == gq_Name)
           {
               nowender = gq_DatatableName + "." + gq_DatacolumnName +  ",a.remark,";
           }
           ////////////////////////////////////////////////////

           if ("成品编号" == gq_Name)
           {
               nowender = gq_DatatableName + "." + gq_DatacolumnName + ",a.mch,a.xhc,";
           }


           return nowender;

       }

       public  string MakeWhere()
       {
           string nowwhere="";
           
           if (in_Control.Text.Replace(" ", "").Length > 0)
           {

                nowwhere = " and  ( " + gq_DatatableName + "." + gq_DatacolumnName + " = '" + in_Control.Text + "') ";

                if ("工件编号" == gq_Name)
                {
                    nowwhere = " and  ( " + gq_DatatableName + "." + gq_DatacolumnName + " = '" + in_Control.Text + "') ";
                }


           }

           return nowwhere;

       }

    }

   public class GroupQueryItemSelector
   {
      
       
      // 
      ///////////////////////////////////////////
      
       //private List<GroupQueryItem> in_Controls = new List<GroupQueryItem>();

       private BindingList<GroupQueryItem> in_Controls = new BindingList<GroupQueryItem>();

       

       public  void Add_Incontrol(GroupQueryItem newIncontrol)
       {
           this.in_Controls.Add(newIncontrol);
       }

       public  void Del_Incontrol(string  delIncontrol)
       {
           this.in_Controls.Remove(Find_Incontrol(delIncontrol));

           
       }
       public void Ins_Incontrol(int nowindx,string insIncontrol)
       {

           GroupQueryItem tempItem = Find_Incontrol(insIncontrol);

           this.in_Controls.Remove(tempItem);
           this.in_Controls.Insert(nowindx, tempItem);


       }

       private  GroupQueryItem Find_Incontrol(string findIncontrol)
       {
          GroupQueryItem tempItem=null ;
           
           foreach (GroupQueryItem item in in_Controls)
           {
               if (findIncontrol == item.项目)
               {
                   tempItem = item;
                   break;
               }
           }

           return tempItem;         
          
       }
      
       ///////////////////

       private DataGridView mDataGridView = null;

       public DataGridView DataGridView
       {
           get { return mDataGridView; }
           set
           {
              
               mDataGridView = value;
              
           }
       }

       ///////////////////
       private string formName = "";

       public GroupQueryItemSelector()
       { 
       
       }

       public GroupQueryItemSelector(DataGridView dgv, string winformName)
            : this()
        {
            this.DataGridView = dgv;
            this.formName = winformName;
           // MakeDataset();

        }

       public void DgvBindList()
       {
           this.DataGridView.Columns.Clear();

           this.DataGridView.AutoGenerateColumns = true;
           DataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
           
           
           this.DataGridView.DataSource = in_Controls;
       }
       
       private DataSet in_controlList = new DataSet();

       private void Make_INControl_Dataset()
       {
           // Create a new DataTable.
           System.Data.DataTable table = new DataTable("incontrols");
           // Declare variables for DataColumn and DataRow objects.
           DataColumn column;
           // DataRow row;

           // Create new DataColumn, set DataType, 
           // ColumnName and add to DataTable.


       
       
       //      string gq_DatatableName;
           column = new DataColumn();
           column.DataType = System.Type.GetType("System.String");
           // System.Type.GetType("System.String ");
           column.ColumnName = "选项"; //  gq_Name;
           column.ReadOnly = true;
           column.Unique = true;
           // Add the Column to the DataColumnCollection.
           table.Columns.Add(column);

           // Create second column.
           column = new DataColumn();
           column.DataType = System.Type.GetType("System.String");
           column.ColumnName = "数据字段"; //     gq_DatacolumnName;
           //column.AutoIncrement = false;
           //column.Caption = "ParentItem";
           //column.ReadOnly = false;
           //column.Unique = false;
           // Add the column to the table.
           table.Columns.Add(column);


           // Create second column.
           column = new DataColumn();
           column.DataType = System.Type.GetType("System.String");
           column.ColumnName = "表名"; //     //  gq_DatatableName;
           //column.AutoIncrement = false;
           //column.Caption = "ParentItem";
           //column.ReadOnly = false;
           //column.Unique = false;
           // Add the column to the table.
           table.Columns.Add(column);
           // Make the ID column the primary key column.
           DataColumn[] PrimaryKeyColumns = new DataColumn[1];
           PrimaryKeyColumns[0] = table.Columns["选项"];
           table.PrimaryKey = PrimaryKeyColumns;

           //// Instantiate the DataSet variable.
           //dataSet = new DataSet();
           //// Add the new DataTable to the DataSet.
           in_controlList.Tables.Add(table);

           // Create three new DataRow objects and add 
           // them to the DataTable
           //for (int i = 0; i <= 2; i++)
           //{
           //    row = table.NewRow();
           //    row["id"] = i;
           //    row["ParentItem"] = "ParentItem " + i;
           //    table.Rows.Add(row);
           //}
       }

       private void ResetControlList()
       {
           DataRow row;

          // mCheckedListBox.Items.Clear();


           in_controlList.Tables["incontrols"].Rows.Clear();


           foreach (GroupQueryItem incontrol in in_Controls)

        
           {
              // mCheckedListBox.Items.Add(c.HeaderText, c.Visible);

               row = in_controlList.Tables["incontrols"].NewRow();

               row["选项"] = incontrol.项目;
               //row["数据字段"] = incontrol.表字段;
               //row["表名"] = incontrol.表名;

               in_controlList.Tables["incontrols"].Rows.Add(row);
           }
           //int PreferredHeight = (mCheckedListBox.Items.Count * 9) + 7;
           //mCheckedListBox.Height = (PreferredHeight < MaxHeight) ? PreferredHeight : MaxHeight;
           //mCheckedListBox.Width = this.Width;
           //mCheckedListBox.MultiColumn = true;
       
       
       }

       public  string MakeAllHeader()
       {
           string Allheader = "";

           for (int i = 0; i < this.in_Controls.Count; i++)
           {
               Allheader = Allheader + this.in_Controls[i].MakeHeader();
           }

           return Allheader;

       }

       public  string MakeAllheaderEnder()
       {
           string Allender = "";

           for (int i = 0; i < this.in_Controls.Count; i++)
           {
               Allender = Allender + this.in_Controls[i].MakeEnder();
           }

           return Allender;

       }

       public  string MakeAllWhere()
       {
           string Allwhere = "";

           for (int i = 0; i < this.in_Controls.Count; i++)
           {
               Allwhere = Allwhere + this.in_Controls[i].MakeWhere();
           }


           return Allwhere;

       }
   
   }
}
