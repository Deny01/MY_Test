namespace HappyYF.YuanXin.Data
{


    public partial class LYProductMange
    {
        partial class ly_production_submit_periodDataTable
        {
        }

        partial class LY_productiontask_selDataTable
        {
        }

    }
}



namespace HappyYF.YuanXin.Data.LYProductMangeTableAdapters
{



    public partial class ly_production_submit_periodTableAdapter : global::System.ComponentModel.Component
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
                //this.Adapter.InsertCommand.CommandTimeout = value;ly_production_submit_periodTableAdapter
                ////this._adapter.InsertCommand.CommandTimeout = value;
                //this.Adapter.UpdateCommand.CommandTimeout = value;ly_production_detail_viewTableAdapter
            }
        }
    }

    public partial class ly_production_detail_viewTableAdapter : global::System.ComponentModel.Component
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
                //this.Adapter.InsertCommand.CommandTimeout = value;ly_production_submit_periodTableAdapter
                ////this._adapter.InsertCommand.CommandTimeout = value;
                //this.Adapter.UpdateCommand.CommandTimeout = value;ly_production_detail_viewTableAdapter
            }
        }
    }
}
