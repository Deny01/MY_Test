namespace HappyYF.YuanXin.Data
{


    public partial class LYQualityInspector
    {
        partial class LY_productionorder_listDataTable
        {
        }

        partial class ly_production_order_inspectionAllDataTable
        {
        }
    }
}
namespace HappyYF.YuanXin.Data.LYQualityInspectorTableAdapters
{
    public partial class DNI_ByTimeTableAdapter : global::System.ComponentModel.Component
    {
        public int CommandTimeout
        {
            set
            {
                for (int n = 0; n < this.CommandCollection.Length; n++)
                {
                    if (this.CommandCollection[n] != null)
                    {
                        this.CommandCollection[n].CommandTimeout = value;
                    }
                }
                
            }
        }

    }
    public partial class LY_Invoice_storeInAll_ByTimeTableAdapter : global::System.ComponentModel.Component
    {
        public int CommandTimeout
        {
            set
            {
                for (int n = 0; n < this.CommandCollection.Length; n++)
                {
                    if (this.CommandCollection[n] != null)
                    {
                        this.CommandCollection[n].CommandTimeout = value;
                    }
                }

            }
        }

    }

}
