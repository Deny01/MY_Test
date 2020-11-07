using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure;

namespace TGZJ.Presentation
{
    public partial class ItemOfService : Form
    {
        HappyYF.Model.ItemsofService.Itemofservice nowItem;
        HappyYF.Infrastructure.Repositories.ItemofserviceRepository nowRepository;
        
        public ItemOfService()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nowItem = new HappyYF.Model.ItemsofService.Itemofservice("06");
            UnitOfWork uk = new UnitOfWork();
            
            nowRepository = new HappyYF.Infrastructure.Repositories.ItemofserviceRepository(uk);


             IList < HappyYF.Model.ItemsofService.Itemofservice>    iii= nowRepository.FindAll();
             this.bindingSource1.DataSource = iii.AsEnumerable();


             this.dataGridView1.Columns.Clear();

             this.dataGridView1.AutoGenerateColumns = true;
             dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

             //BindingSource bindingSource1 = new BindingSource();



             //DataTableReader reader = new DataTableReader(allData.Tables[0]);

             //this.billMainDataSet.BalanceBill.Clear();
             //this.billMainDataSet.BalanceBill.Load(reader);
             //ds = allData;
            
           
             this.bindingNavigator1.BindingSource = bindingSource1;

             bindingSource1.ResetBindings(true);

            
            
            
            
            
            //nowItem = nowRepository.FindBy("06");
             //nowRepository.Add(nowItem);
             //uk.Commit();
            
            //nowItem.Name = "Here";
            //nowRepository.PersistNewItem(nowItem);
            //nowRepository.PersistUpdatedItem(nowItem);
            //nowRepository.PersistDeletedItem(nowItem);
        }
    }
}
