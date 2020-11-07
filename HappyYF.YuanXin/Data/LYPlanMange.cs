namespace HappyYF.YuanXin.Data
{


    public partial class LYPlanMange
    {
        partial class ly_plan_getmaterialCombinationOutDataTable
        {
        }

        partial class ly_material_task_viewNewDataTable
        {
        }
    }
}

namespace HappyYF.YuanXin.Data.LYPlanMangeTableAdapters
{
    partial class ly_plan_getmaterialTableAdapter
    {
    }

    public partial class ly_store_planitemcountTableAdapter : global::System.ComponentModel.Component
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

    public partial class ly_material_task_viewNewTableAdapter : global::System.ComponentModel.Component
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




namespace HappyYF.YuanXin.Data.LYPlanMangeTableAdapters
{


    public partial class ly_store_planitemcountTableAdapter
    {
    }
}
