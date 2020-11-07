namespace HappyYF.YuanXin.Data
{


    public partial class LYMaterialMange
    {
        partial class ly_inma0010yl_maintenanceDataTable
        {
        }

        partial class ly_inma0010ylDataTable
        {
        }
    }
}

namespace HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters
{
    partial class ly_item_allcost_NoVatLabTableAdapter
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
                //this.Adapter.InsertCommand.CommandTimeout = value;
                ////this._adapter.InsertCommand.CommandTimeout = value;
                //this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }

    public partial class ly_item_allcostTableAdapter : global::System.ComponentModel.Component
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
                //this.Adapter.InsertCommand.CommandTimeout = value;
                ////this._adapter.InsertCommand.CommandTimeout = value;
                //this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }

    public partial class ly_item_allcost_NoVatTableAdapter : global::System.ComponentModel.Component
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
                //this.Adapter.InsertCommand.CommandTimeout = value;
                ////this._adapter.InsertCommand.CommandTimeout = value;
                //this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }
}
