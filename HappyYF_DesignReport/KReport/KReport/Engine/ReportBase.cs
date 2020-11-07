namespace KReport.Engine
{
    using KReport.Controls;
    using System;
    using System.Collections;
    using System.Data;
    using System.Drawing.Printing;
    

    public abstract class ReportBase
    {
        private Units _unit;
        protected float areaPrintableHeight = 0f;
        protected int areaPrintableWidth = 0;
        private BandCollection bandcollection;

        public  BandCollection Bandcollection
        {
            get { return bandcollection; }
            set { bandcollection = value; }
        }
        private BandCollection bandgroupcolletion;

        public  BandCollection Bandgroupcolletion
        {
            get { return bandgroupcolletion; }
            set { bandgroupcolletion = value; }
        }
        protected ReportDataCollection dataSources;
        protected ReportDevice device;
        private RPage page = new RPage();
        private ArrayList subreports;
        protected ReportTypeDevice typedevice = ReportTypeDevice.DocumentPrint;

        protected ReportBase()
        {
            this.CreateDevice();
            this.dataSources = new ReportDataCollection();
            this.bandcollection = new BandCollection();
            this.bandgroupcolletion = new BandCollection();
            this.subreports = new ArrayList();
        }

        public void AddBand(BandBase band)
        {
            if (band != null)
            {
                if (this.GetBand(band.BandType) == null)
                {
                    this.bandcollection.Add(band);
                    band.Report = this;
                    if (band.BandType == BandType.BandGroup)
                    {
                        band.SetIndex(this.BandsGroup.Count);
                        (band as BandGroup).BandFooder.Report = this;
                        (band as BandGroup).BandHeader.Report = this;
                    }
                }
                this.SortBands();
            }
        }

        public void AddSource(DataSet source)
        {
            foreach (DataTable table in source.Tables)
            {
                this.AddSource(table, table.TableName);
            }
        }

        public void AddSource(ArrayList source, string sourcename)
        {
            this.DataSources.Add(new ReportDataList(source, sourcename));
        }

        public void AddSource(DataTable source, string sourceName)
        {
            this.dataSources.Add(new ReportDataTable(source, sourceName));
        }

        public void AddSubReport(Report subreport)
        {
            this.subreports.Add(subreport);
        }

        protected void ConfigurePaperSize()
        {
            this.device.DefaultPageSettings.PaperSize = new PaperSize(this.PageSetting.PaperName, this.device.ConvertPixelToDevice(this.PageSetting.PageWidth), this.device.ConvertPixelToDevice(this.PageSetting.PageHeight));
            this.device.DefaultPageSettings.PaperSize.Height = this.device.ConvertPixelToDevice(this.PageSetting.PageHeight);
            this.device.DefaultPageSettings.PaperSize.Width = this.device.ConvertPixelToDevice(this.PageSetting.PageWidth);
            this.device.DefaultPageSettings.Margins.Top = this.device.ConvertPixelToDevice(this.PageSetting.MarginTop);
            this.device.DefaultPageSettings.Margins.Left = this.device.ConvertPixelToDevice(this.PageSetting.MarginLeft);
            this.device.DefaultPageSettings.Margins.Bottom = this.device.ConvertPixelToDevice(this.PageSetting.MarginBottom);
            this.device.DefaultPageSettings.Margins.Right = this.device.ConvertPixelToDevice(this.PageSetting.MarginRigth);
            this.device.DefaultPageSettings.PrinterSettings.DefaultPageSettings.Margins.Bottom = this.device.DefaultPageSettings.Margins.Bottom;
            this.device.DefaultPageSettings.PrinterSettings.DefaultPageSettings.Margins.Top = this.device.DefaultPageSettings.Margins.Top;
            this.device.DefaultPageSettings.PrinterSettings.DefaultPageSettings.Margins.Left = this.device.DefaultPageSettings.Margins.Left;
            this.device.DefaultPageSettings.PrinterSettings.DefaultPageSettings.Margins.Right = this.device.DefaultPageSettings.Margins.Right;
            this.device.DefaultPageSettings.Landscape = this.PageSetting.LandScape;
            if (this.PageSetting.LandScape)
            {
                this.areaPrintableHeight = this.device.DefaultPageSettings.PaperSize.Width - (this.device.DefaultPageSettings.Margins.Left + this.device.DefaultPageSettings.Margins.Right);
            }
            else
            {
                this.areaPrintableHeight = this.device.DefaultPageSettings.PaperSize.Height - (this.device.DefaultPageSettings.Margins.Top + this.device.DefaultPageSettings.Margins.Bottom);
            }
        }

        public void CreateBandDefauts()
        {
            if (this.GetBand(BandType.BandDetail) == null)
            {
                this.AddBand(new BandDetalhe());
            }
        }

        protected void CreateDevice()
        {
            this.device = FactoryDevice.Instance(this.typedevice);
        }

        public BandBase GetBand(BandType bandtype)
        {
            foreach (BandBase base2 in this.Bands)
            {
                if (base2.BandType == bandtype)
                {
                    return base2;
                }
            }
            return null;
        }

        protected BandGroup GetBandGroup(string bandname)
        {
            foreach (BandGroup group in this.bandcollection)
            {
                if (group.Name == bandname)
                {
                    return group;
                }
            }
            return null;
        }

        protected void LoadFromFile(string filename)
        {
            new ReportTemplateXML(this).LoadFromXML(filename);
        }

        public void RemoveBand(BandType bandtype)
        {
            BandBase band = this.GetBand(bandtype);
            if (band != null)
            {
                band.Controls.Clear();
                this.bandcollection.Remove(band);
            }
        }

        protected void ResetBands()
        {
            this.bandgroupcolletion.Clear();
            this.bandcollection.Clear();
        }

        protected void SaveToFile(string filename)
        {
            new ReportTemplateXML(this).SaveToXML(filename);
        }

        public virtual void SetUniqueName(CustomControl control)
        {
        }

        private void SortBands()
        {
            this.Bands.Sort();
        }

        public BandCollection Bands
        {
            get
            {
                BandCollection bands = new BandCollection();
                this.bandgroupcolletion.Clear();
                foreach (BandBase base2 in this.bandcollection)
                {
                    if (base2.BandType == BandType.BandGroup)
                    {
                        this.bandgroupcolletion.Add(base2);
                        bands.Add(((BandGroup) base2).BandHeader);
                        bands.Add(((BandGroup) base2).BandFooder);
                    }
                    else
                    {
                        bands.Add(base2);
                    }
                }
                bands.Sort();
                return bands;
            }
        }

        public BandCollection BandsGroup
        {
            get
            {
                return this.bandgroupcolletion;
            }
        }

        public ReportDataCollection DataSources
        {
            get
            {
                return this.dataSources;
            }
        }

        public ReportTypeDevice DeviceType
        {
            get
            {
                return this.typedevice;
            }
            set
            {
                if (this.typedevice != value)
                {
                    this.typedevice = value;
                    this.CreateDevice();
                }
            }
        }

        internal RPage PageSetting
        {
            get
            {
                return this.page;
            }
            set
            {
                this.page = value;
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
                foreach (BandBase base2 in this.bandgroupcolletion)
                {
                    base2.Unit = this.Unit;
                }
                foreach (BandBase base2 in this.Bands)
                {
                    base2.Unit = this.Unit;
                }
            }
        }
    }
}

