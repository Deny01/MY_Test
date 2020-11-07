namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RSystem : RLabel
    {
        private RSystemType systemType = RSystemType.页码;
        private string text = string.Empty;

        public override void DrawCommand(Graphics g, int offset)
        {
            this.WriteStringSytem();
            base.DrawCommand(g, offset);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.WriteStringSytem();
            base.OnPaint(e);
        }

        private void WriteStringSytem()
        {
            if ((this.systemType == RSystemType.页码) && (base.Band != null))
            {
                this.Text = "第 " + ((Report) base.Band.Report).PagesCount.ToString() + " 页";
            }
            if (this.systemType == RSystemType.日期)
            {
                this.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (this.systemType == RSystemType.日期时间)
            {
                this.Text = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss");
            }
            if (this.systemType == RSystemType.时间)
            {
                this.Text = DateTime.Now.ToString("hh:MM:ss");
            }
        }

        //[Category("Custom")]
        [Description("时间页码"), Category("时间页码"), ReportElementAttribute("报表元素")]
        //public RSystemType SystemType
        public RSystemType 时间页码
        {
            get
            {
                return this.systemType;
            }
            set
            {
                this.systemType = value;
                this.WriteStringSytem();
            }
        }

        public override string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

