namespace KReport.Engine
{
    using KReport.Controls;
    using System;
    using System.Collections;

    public class BandGroup : BandBase
    {
        private BandGroupFooder _bandFooder;
        private BandGroupHeader _bandHeader;
        private string _currentValue;
        private string _fieldName;
        private ArrayList _listColumnsCalc;
        private bool isStarted;

        public BandGroup()
        {
            this._currentValue = string.Empty;
            this.isStarted = false;
            this.Init();
            this.FieldName = string.Empty;
        }

        public BandGroup(string fieldName)
        {
            this._currentValue = string.Empty;
            this.isStarted = false;
            this.Init();
            this.FieldName = fieldName;
        }

        public void Calc()
        {
            foreach (CustomControl control in this.BandFooder.Controls)
            {
                if (control is RDBCalculated)
                {
                    (control as RDBCalculated).Calc();
                }
            }
        }

        private void CalcReset()
        {
            foreach (CustomControl control in this.BandFooder.Controls)
            {
                if (control is RDBCalculated)
                {
                    (control as RDBCalculated).CalcReset();
                }
            }
        }

        private void Init()
        {
            this._bandFooder = new BandGroupFooder();
            this._bandHeader = new BandGroupHeader();
            this._listColumnsCalc = new ArrayList();
            base.SetBandType(BandType.BandGroup);
        }

        public void Reset()
        {
            this.CalcReset();
            this.isStarted = false;
            this.CurrentValue = string.Empty;
        }

        public override void SetIndex(int value)
        {
            base.SetIndex(value);
            this.BandHeader.SetIndex(value);
            this.BandFooder.SetIndex(value);
        }

        public void Start()
        {
            this.CalcReset();
            this.isStarted = true;
            this.CurrentValue = string.Empty;
        }

        public BandGroupFooder BandFooder
        {
            get
            {
                return this._bandFooder;
            }
            set
            {
                this._bandFooder = value;
            }
        }

        public BandGroupHeader BandHeader
        {
            get
            {
                return this._bandHeader;
            }
            set
            {
                this._bandHeader = value;
            }
        }

        public string CurrentValue
        {
            get
            {
                return this._currentValue;
            }
            set
            {
                this._currentValue = value;
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
                this._bandHeader.Name = "分组头 - " + this._fieldName;
                this._bandFooder.Name = "分组尾 - " + this._fieldName;
            }
        }

        public bool IsStarted
        {
            get
            {
                return this.isStarted;
            }
        }
    }
}

