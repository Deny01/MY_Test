namespace HappyYF.YuanXin.Data
{


    partial class LYTecSet
    {
    }
}

namespace HappyYF.YuanXin.Data.LYTecSetTableAdapters
{


    
    public partial class ly_material_price_InfoTableAdapter : global::System.ComponentModel.Component
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
