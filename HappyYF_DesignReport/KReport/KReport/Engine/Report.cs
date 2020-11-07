namespace KReport.Engine
{
    using KReport.Controls;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class Report : ReportBase
    {
        private int bandHeigth = 0;
        private ArrayList calcFields = new ArrayList();
        private bool canPrinDetalhe = false;
        private IReportData dataSource;
        private string fileName = string.Empty;
        private int footerHeigth = 0;
        private string masterDataSource = string.Empty;
        private int offset = 0;
        private int pagecount = 0;
        private bool printTitle = true;

        public event BeforePrintControlEventHandler BeforePrintControl;

        public event ReportCompletedEventHandler ReportCompleted;

        public event ReportPageCompletedEventHandler ReportPageCompleted;

        public event ReportPageStartEventHandler ReportPageStart;

        public event ReportStartEventHandler ReportStart;

        public Report()
        {
            this.PrepareDevice();
        }

        private void CalcFieldCalculated()
        {
            foreach (RDBCalculated calculated in this.calcFields)
            {
                calculated.Calc();
            }
        }

        public CustomControl FindControlByName(string name)
        {
            foreach (BandBase base2 in base.Bands)
            {
                foreach (CustomControl control in base2.Controls)
                {
                    if (control.Name.Equals(name))
                    {
                        return control;
                    }
                }
            }
            return null;
        }

        private void GenerateGroupFooters(Graphics g)
        {
            foreach (BandGroup group in base.BandsGroup)
            {
                this.ProcessGroupFooter(group, g);
            }
        }

        private void GeneretePage(Graphics g)
        {
            if (this.BandFooder != null)
            {
                this.footerHeigth = base.device.ConvertPixelToDevice(this.BandFooder.Height);
            }
            this.offset = 0;
            if (this.printTitle && this.PrintBand(this.BandTitle, g))
            {
                this.printTitle = false;
            }
            this.PrintBand(this.BandHeader, g);
            if (!this.dataSource.EndFile())
            {
                this.PrintBandDetalhe(this.BandDetail, g);
            }
            if (this.dataSource.EndFile())
            {
                this.PrintBand(this.BandSummary, g);
            }
            this.GoToFooter();
            this.PrintBand(this.BandFooder, g);
        }

        private void GoToFooter()
        {
            while (this.offset < (base.areaPrintableHeight - this.footerHeigth))
            {
                this.offset += base.device.ConvertPixelToDevice(1);
            }
        }

        private void InitializeCalcField()
        {
            this.calcFields.Clear();
            foreach (BandBase base2 in base.Bands)
            {
                if ((base2.BandType != BandType.BandGroupFooder) && (base2.BandType != BandType.BandGroupHeader))
                {
                    foreach (CustomControl control in base2.Controls)
                    {
                        if (control is RDBCalculated)
                        {
                            this.calcFields.Add(control);
                            (control as RDBCalculated).CalcReset();
                        }
                    }
                }
            }
        }

        private void InitializeDataLink()
        {
            this.calcFields.Clear();
            foreach (BandBase base2 in base.Bands)
            {
                foreach (CustomControl control in base2.Controls)
                {
                    if ((control is IRDBControl) && (base.dataSources.GetSource(((IRDBControl) control).数据源) != null))
                    {
                        ((IRDBControl) control).DataSourceLink = base.dataSources.GetSource(((IRDBControl) control).数据源);
                    }
                }
            }
        }

        public void Load()
        {
            base.ResetBands();
            base.LoadFromFile(this.fileName);
            this.LoadControls();
        }

        private void LoadControls()
        {
            foreach (BandBase base2 in base.Bands)
            {
                foreach (CustomControl control in base2.Controls)
                {
                    control.Load();
                }
            }
        }

        public void PrepareDevice()
        {
            base.device.StartPrint -= new DeviceStartPrintEventHandler(this.ReportPrintStart);
            base.device.EndPrint -= new DeviceEndPrintEventHandler(this.ReportPrintEnd);
            base.device.PageGenerate -= new DevicePageGenerateEventHandler(this.printPageDevice);
            base.device.StartPrint += new DeviceStartPrintEventHandler(this.ReportPrintStart);
            base.device.EndPrint += new DeviceEndPrintEventHandler(this.ReportPrintEnd);
            base.device.PageGenerate += new DevicePageGenerateEventHandler(this.printPageDevice);
        }

        private void PrepareGroups()
        {
            foreach (BandGroup group in base.BandsGroup)
            {
                group.Reset();
            }
        }

        public void Print()
        {
            this.PrintToDevive();
        }

        public bool PrintBand(BandBase band, Graphics g)
        {
            bool flag = true;
            if (band != null)
            {
                this.bandHeigth = base.device.ConvertPixelToDevice(band.Height);
                if (((this.bandHeigth + this.offset) < (base.areaPrintableHeight - this.footerHeigth)) || (band.BandType == BandType.BandFooder))
                {
                    foreach (CustomControl control in band.Controls)
                    {
                        if (this.BeforePrintControl != null)
                        {
                            this.BeforePrintControl(control);
                        }
                        control.DrawCommand(g, this.offset);
                    }
                    this.offset += this.bandHeigth;
                    return flag;
                }
                flag = false;
                this.GoToFooter();
            }
            return flag;
        }

        public void PrintBandDetalhe(BandBase band, Graphics g)
        {
            if (band != null)
            {
                this.bandHeigth = base.device.ConvertPixelToDevice(band.Height);
                while (((this.bandHeigth + this.offset) < (base.areaPrintableHeight - this.footerHeigth)) && !this.dataSource.EndFile())
                {
                    this.ProcessGroups(g);
                    if ((!this.dataSource.EndFile() && this.canPrinDetalhe) && this.PrintBand(band, g))
                    {
                        this.ProcessGrupsCalc();
                        this.CalcFieldCalculated();
                        this.dataSource.DataSourceNextRecord();
                    }
                }
                if (this.dataSource.EndFile())
                {
                    this.GenerateGroupFooters(g);
                }
            }
        }

        private void printPageDevice(Graphics g)
        {
            if (this.ReportPageStart != null)
            {
                this.ReportPageStart();
            }
            this.GeneretePage(g);
            this.PrintWaterMake(g);
            base.device.hasmorepage = !this.dataSource.EndFile();
            if (base.device.hasmorepage)
            {
                this.pagecount++;
            }
            if (this.ReportPageCompleted != null)
            {
                this.ReportPageCompleted();
            }
        }

        public void PrintToDevive()
        {
            this.PrepareDevice();
            base.device.Start();
        }

        private void PrintWaterMake(Graphics g)
        {
            return;
            g.TranslateTransform(200f, 200f);
            g.RotateTransform(0f);
            g.DrawString("中原精密文件", new Font("宋体", 70f, FontStyle.Bold), new SolidBrush(Color.FromArgb(0x40, Color.Black)), (float) 0f, (float) 180f);
            g.ResetTransform();
        }

        private void ProcessGroupFooter(BandGroup group, Graphics g)
        {
            int index = base.BandsGroup.IndexOf(group);
            for (int i = base.BandsGroup.Count - 1; i >= index; i--)
            {
                BandGroup group2 = (BandGroup) base.BandsGroup[i];
                if (group2.IsStarted)
                {
                    if (this.PrintBand(group2.BandFooder, g))
                    {
                        group2.Reset();
                        this.canPrinDetalhe = true;
                    }
                    else
                    {
                        this.canPrinDetalhe = false;
                    }
                }
            }
        }

        private void ProcessGroups(Graphics g)
        {
            this.canPrinDetalhe = true;
            for (int i = 0; i < base.Bandgroupcolletion.Count; i++)
            {
                BandGroup group = (BandGroup) base.Bandgroupcolletion[i];
                if ((this.dataSource.GetFieldValue(group.FieldName) != null) && (group.CurrentValue != this.dataSource.GetFieldValue(group.FieldName).ToString()))
                {
                    if (group.IsStarted)
                    {
                        this.dataSource.DataSourcePriorRecord();
                        this.ProcessGroupFooter(group, g);
                        this.dataSource.DataSourceNextRecord();
                    }
                    if (this.canPrinDetalhe)
                    {
                        group.Start();
                        group.CurrentValue = this.dataSource.GetFieldValue(group.FieldName).ToString();
                        if (!this.PrintBand(group.BandHeader, g))
                        {
                            group.Reset();
                            this.canPrinDetalhe = false;
                        }
                        else
                        {
                            this.canPrinDetalhe = true;
                        }
                    }
                }
            }
        }

        private void ProcessGrupsCalc()
        {
            for (int i = 0; i < base.Bandgroupcolletion.Count; i++)
            {
                BandGroup group = (BandGroup) base.Bandgroupcolletion[i];
                if ((this.dataSource.GetFieldValue(group.FieldName) != null) && (group.CurrentValue == this.dataSource.GetFieldValue(group.FieldName).ToString()))
                {
                    group.Calc();
                }
            }
        }

        private void ReportPrintEnd()
        {
            this.dataSource.DataSourceFirstRecord();
            if (this.ReportCompleted != null)
            {
                this.ReportCompleted();
            }
        }

        private void ReportPrintStart()
        {
            this.dataSource = base.dataSources.GetSource(this.MasterDataSource);
            if (this.dataSource != null)
            {
                this.dataSource.OpenDataSouce();
                this.dataSource.DataSourceFirstRecord();
            }
            else
            {
                this.dataSource = new ReportDataTable();
            }
            this.InitializeDataLink();
            this.InitializeCalcField();
            this.PrepareGroups();
            this.SortControls();
            base.ConfigurePaperSize();
            this.printTitle = true;
            this.pagecount = 1;
            if (this.ReportStart != null)
            {
                this.ReportStart();
            }
        }

        public void Save()
        {
            base.SaveToFile(this.fileName);
        }

        public override void SetUniqueName(CustomControl control)
        {
            int num = 1;
            string name = string.Empty;
            do
            {
                name = control.GetType().Name + num.ToString();
                num++;
            }
            while (this.FindControlByName(name) != null);
            control.Name = name;
        }

        public void Show()
        {
            this.PrepareDevice();
            base.device.Show(this);
        }

        public void ShowDesigner(bool haveDesignRight)
        {
            DesignerReport.ShowDesigner(this, haveDesignRight);
            //if (!haveDesignRight)
            //{ 
            //   DesignerReport
            //}
        }

        public void SortControls()
        {
            foreach (BandBase base2 in base.Bands)
            {
                base2.SortControls();
            }
        }

        public BandDetalhe BandDetail
        {
            get
            {
                return (BandDetalhe) base.GetBand(BandType.BandDetail);
            }
        }

        public KReport.Engine.BandFooder BandFooder
        {
            get
            {
                return (KReport.Engine.BandFooder) base.GetBand(BandType.BandFooder);
            }
        }

        public KReport.Engine.BandHeader BandHeader
        {
            get
            {
                return (KReport.Engine.BandHeader) base.GetBand(BandType.BandHeader);
            }
        }

        public KReport.Engine.BandSummary BandSummary
        {
            get
            {
                return (KReport.Engine.BandSummary) base.GetBand(BandType.BandSummary);
            }
        }

        public KReport.Engine.BandTitle BandTitle
        {
            get
            {
                return (KReport.Engine.BandTitle) base.GetBand(BandType.BandTitle);
            }
        }

        public ReportDevice Device
        {
            get
            {
                return base.device;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public string MasterDataSource
        {
            get
            {
                return this.masterDataSource;
            }
            set
            {
                this.masterDataSource = value;
            }
        }

        public int PagesCount
        {
            get
            {
                return this.pagecount;
            }
        }
    }
}

