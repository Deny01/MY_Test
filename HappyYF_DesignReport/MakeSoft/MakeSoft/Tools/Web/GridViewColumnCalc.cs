namespace MakeSoft.Tools.Web
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class GridViewColumnCalc
    {
        private int _count = 0;
        private string _displayname;
        private string _fieldName;
        private int _index = 0;
        private GridViewColumnCalcOperation _operacao = GridViewColumnCalcOperation.Sum;
        private double _value = 0.0;

        public GridViewColumnCalc(string fieldname, GridViewColumnCalcOperation operacao, string displayname)
        {
            this.FieldName = fieldname;
            this.Operacao = operacao;
            this._displayname = displayname;
            this.Init();
        }

        public void Init()
        {
            this._count = 0;
            this._value = 0.0;
            if (this.Operacao == GridViewColumnCalcOperation.Min)
            {
                this._value = 9999999999;
            }
            if (this.Operacao == GridViewColumnCalcOperation.Max)
            {
                this._value = -9999999999;
            }
        }

        public void RowDataBound(GridViewRow row)
        {
            switch (this.Operacao)
            {
                case GridViewColumnCalcOperation.Sum:
                    this._value += Convert.ToDouble(DataBinder.Eval(row.DataItem, this.FieldName));
                    break;

                case GridViewColumnCalcOperation.Count:
                    this._value++;
                    break;

                case GridViewColumnCalcOperation.Avg:
                    this._count++;
                    this._value += Convert.ToDouble(DataBinder.Eval(row.DataItem, this.FieldName));
                    break;

                case GridViewColumnCalcOperation.Max:
                    if (Convert.ToDouble(DataBinder.Eval(row.DataItem, this.FieldName)) > this._value)
                    {
                        this._value = Convert.ToDouble(row.Cells[this.Index].Text);
                    }
                    break;

                case GridViewColumnCalcOperation.Min:
                    if (Convert.ToDouble(DataBinder.Eval(row.DataItem, this.FieldName)) <= this._value)
                    {
                        this._value = Convert.ToDouble(row.Cells[this.Index].Text);
                    }
                    break;
            }
        }

        public string DisplayName
        {
            get
            {
                return this._displayname;
            }
            set
            {
                this._displayname = value;
            }
        }

        public string FieldName
        {
            get
            {
                return this._fieldName;
            }
            set
            {
                this._fieldName = value;
            }
        }

        public int Index
        {
            get
            {
                return this._index;
            }
            set
            {
                this._index = value;
            }
        }

        public GridViewColumnCalcOperation Operacao
        {
            get
            {
                return this._operacao;
            }
            set
            {
                this._operacao = value;
            }
        }

        public double Value
        {
            get
            {
                return this._value;
            }
        }
    }
}

