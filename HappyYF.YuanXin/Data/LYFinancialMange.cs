namespace HappyYF.YuanXin.Data
{
    

     
}

namespace HappyYF.YuanXin.Data.LYFinancialMangeTableAdapters
{

    public partial class ly_payable_itemsTableAdapter : global::System.ComponentModel.Component
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
                this.Adapter.InsertCommand.CommandTimeout = value;
                //this._adapter.InsertCommand.CommandTimeout = value;
                this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }

    public partial class ly_Settlement_In_FinishGood_SumTableAdapter : global::System.ComponentModel.Component
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
    

 public partial class LY_payable_mainTableAdapter : global::System.ComponentModel.Component
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

    public partial class Get_SalesPrice_ByApprove_Fin_month_detailTableAdapter : global::System.ComponentModel.Component
    
        
        
        
        //public partial class get_SalesPrice_ByApprove_Fin_month_detailBindingSource : global::System.ComponentModel.Component
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

    

    public partial class ly_get_wip_material_periodTableAdapter : global::System.ComponentModel.Component
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
   


 public partial class ly_store_inout_wip_periodTableAdapter : global::System.ComponentModel.Component
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
                //this._adapter.InsertCommand.CommandTimeout = value;
               // this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }
    public partial class ly_get_wip_storein_periodTableAdapter : global::System.ComponentModel.Component
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
                //this._adapter.InsertCommand.CommandTimeout = value;
                // this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }
    public partial class ly_get_wip_storeout_periodTableAdapter : global::System.ComponentModel.Component
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
                //this._adapter.InsertCommand.CommandTimeout = value;
                // this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }
   
    public partial class LY_payable_periodTableAdapter : global::System.ComponentModel.Component
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
                //this._adapter.InsertCommand.CommandTimeout = value;
                // this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }

    public partial class ly_payable_planTableAdapter : global::System.ComponentModel.Component
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
                this.Adapter.InsertCommand.CommandTimeout = value;
                //this._adapter.InsertCommand.CommandTimeout = value;
                this.Adapter.UpdateCommand.CommandTimeout = value;
            }
        }
    }



   

}




namespace HappyYF.YuanXin.Data
{


    partial class LYFinancialMange
    {
        partial class LY_payable_mainDataTable
        {
        }

        partial class LY_payable_detail_allDataTable
        {
        }
    }
}
