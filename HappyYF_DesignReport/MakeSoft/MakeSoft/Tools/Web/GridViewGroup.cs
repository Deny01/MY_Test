namespace MakeSoft.Tools.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class GridViewGroup
    {
        private string _currentvalue = string.Empty;
        private string _displayname;
        private string _fieldName;
        private int _index = 0;
        private List<GridViewColumnCalc> _listaColumnCalc;
        private bool _started = false;

        public GridViewGroup(string fieldname, string displayname)
        {
            this._fieldName = fieldname;
            this._displayname = displayname;
            this._listaColumnCalc = new List<GridViewColumnCalc>();
        }

        public void Reset()
        {
            this.Start();
            this._started = false;
        }

        public void RowDataBound(GridViewRow row)
        {
            foreach (GridViewColumnCalc calc in this._listaColumnCalc)
            {
                calc.RowDataBound(row);
            }
        }

        public void Start()
        {
            this._currentvalue = string.Empty;
            foreach (GridViewColumnCalc calc in this._listaColumnCalc)
            {
                calc.Init();
            }
            this._started = true;
        }

        public string CurrentValue
        {
            get
            {
                return this._currentvalue;
            }
            set
            {
                this._currentvalue = value;
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

        public bool IsStarted
        {
            get
            {
                return this._started;
            }
        }

        public List<GridViewColumnCalc> ListColumnsCalc
        {
            get
            {
                return this._listaColumnCalc;
            }
        }
    }
}

