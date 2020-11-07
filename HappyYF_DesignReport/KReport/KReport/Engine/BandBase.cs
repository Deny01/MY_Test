namespace KReport.Engine
{
    using KReport.Controls;
    using System;
    using System.Collections;

    public class BandBase
    {
        private BandDesigner _banddesigner = null;
        protected KReport.Engine.BandType _bandtype;
        private ArrayList _controls = new ArrayList();
        private int _heigth = 30;
        private int _index;
        protected string _name;
        private ReportBase _report;
        private KReport.Controls.Ruler _ruler;
        private int _top = 0;
        private Units _unit;

        public void AddControl(CustomControl control)
        {
            this._controls.Add(control);
            control.Load();
        }

        public void AjusteControls()
        {
        }

        protected void SetBandType(KReport.Engine.BandType bandtype)
        {
            this._bandtype = bandtype;
        }

        public virtual void SetIndex(int value)
        {
            this._index = value;
        }

        public void SortControls()
        {
            this.Controls.Sort(new SortControl());
        }

        public KReport.Engine.BandType BandType
        {
            get
            {
                return this._bandtype;
            }
        }

        public ArrayList Controls
        {
            get
            {
                return this._controls;
            }
        }

        internal BandDesigner DesignerControl
        {
            get
            {
                return this._banddesigner;
            }
            set
            {
                this._banddesigner = value;
            }
        }

        public int Height
        {
            get
            {
                return this._heigth;
            }
            set
            {
                this._heigth = value;
                if (this.Ruler != null)
                {
                    this.Ruler.Height = this._heigth;
                }
            }
        }

        public int Index
        {
            get
            {
                return this._index;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public ReportBase Report
        {
            get
            {
                return this._report;
            }
            set
            {
                this._report = value;
            }
        }

        internal KReport.Controls.Ruler Ruler
        {
            get
            {
                return this._ruler;
            }
            set
            {
                this._ruler = value;
            }
        }

        public int Top
        {
            get
            {
                return this._top;
            }
            set
            {
                this._top = value;
            }
        }

        public Units Unit
        {
            get
            {
                return this._unit;
            }
            set
            {
                this._unit = value;
                if (this.Ruler != null)
                {
                    this.Ruler.Escala = this.Unit;
                }
            }
        }
    }
}

