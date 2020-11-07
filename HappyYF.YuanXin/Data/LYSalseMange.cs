namespace HappyYF.YuanXin.Data
{


    public partial class LYSalseMange
    {
        partial class ly_sales_contract_detail_sumDataTable
        {
        }

        partial class ly_sales_test_detail1DataTable
        {
        }

        partial class ly_sales_test_detail3DataTable
        {
        }

        partial class ly_sales_contract_mainDataTable
        {
        }

        partial class ly_sales_borrowDataTable
        {
        }

        partial class ly_sales_borrow_detailDataTable
        {
        }

        partial class f_PlanExtend_LSPTDataTable
        {
        }
    }
}

namespace HappyYF.YuanXin.Data.LYSalseMangeTableAdapters 
{


    public partial class ly_sales_clientreceivablesNews_RepTableAdapter : global::System.ComponentModel.Component
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

    public partial class ly_lsptb_selTableAdapter : global::System.ComponentModel.Component
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

    public partial class ly_sales_test_detail1TableAdapter : global::System.ComponentModel.Component
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

    public partial class GetOrderByBussCodeAllTableAdapter : global::System.ComponentModel.Component
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

    public partial class GetOrderByBussCodeNoInvoiceTableAdapter : global::System.ComponentModel.Component
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
    public partial class GetOrderByBussCodeTableAdapter : global::System.ComponentModel.Component
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


