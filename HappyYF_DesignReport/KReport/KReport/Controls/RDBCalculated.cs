namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    internal class RDBCalculated : RDBLabel
    {
        private int _count;
        private double _soma;
        private TypeCalculated tipoCalculo = TypeCalculated.计数;
        private double value_;

        public void Calc()
        {
            if (base.reportData != null)
            {
                object fieldValue = base.reportData.GetFieldValue(base.数据字段, base.数据源);
                if (fieldValue != null)
                {
                    switch (this.统计字段)
                    {
                        case TypeCalculated.计数:
                            this._count++;
                            this.value_ = this._count;
                            break;

                        case TypeCalculated.平均值:
                            this._soma += Convert.ToDouble(fieldValue);
                            this.Value = this._soma / ((double) this._count);
                            break;

                        case TypeCalculated.最小值:
                            if (((((fieldValue.GetType() == typeof(short)) || (fieldValue.GetType() == typeof(int))) || ((fieldValue.GetType() == typeof(long)) || (fieldValue.GetType() == typeof(double)))) || (fieldValue.GetType() == typeof(decimal))) && (this.Value > Convert.ToDouble(fieldValue)))
                            {
                                this.Value = Convert.ToDouble(fieldValue);
                            }
                            break;

                        case TypeCalculated.最大值:
                            if (((((fieldValue.GetType() == typeof(short)) || (fieldValue.GetType() == typeof(int))) || ((fieldValue.GetType() == typeof(long)) || (fieldValue.GetType() == typeof(double)))) || (fieldValue.GetType() == typeof(decimal))) && (this.Value < Convert.ToDouble(fieldValue)))
                            {
                                this.Value = Convert.ToDouble(fieldValue);
                            }
                            break;

                        case TypeCalculated.求和:
                            if ((((fieldValue.GetType() == typeof(short)) || (fieldValue.GetType() == typeof(int))) || ((fieldValue.GetType() == typeof(long)) || (fieldValue.GetType() == typeof(double)))) || (fieldValue.GetType() == typeof(decimal)))
                            {
                                this.Value += Convert.ToDouble(fieldValue);
                            }
                            break;
                    }
                    this.Text = this.Value.ToString();
                }
            }
        }

        public void CalcReset()
        {
            this._soma = 0.0;
            this._count = 0;
            this.value_ = 0.0;
            if (this.统计字段 == TypeCalculated.最小值)
            {
                this.value_ = -99999999999;
            }
            if (this.统计字段 == TypeCalculated.最大值)
            {
                this.value_ = 99999999999;
            }
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            base.ApplyEscala(g);
            base.FormatText(this.Value);
            int x = base.AligimentText(g).X + base.esquerda;
            int y = ((int) Utils.ConvertPixelToDisplay(base.垂直位置)) + offset;
            g.FillRectangle(new SolidBrush(this.BackColor), base.esquerda, y, base.largura, base.altura);
            base.DrawString(g, x, y);
        }

        //[Category("Custom")]
        [Description("统计字段"), Category("数据字段"), ReportElementAttribute("报表元素")]
       // public TypeCalculated Calculated
        public TypeCalculated 统计字段
        {
            get
            {
                return this.tipoCalculo;
            }
            set
            {
                this.tipoCalculo = value;
            }
        }

        private double Value
        {
            get
            {
                return this.value_;
            }
            set
            {
                this.value_ = value;
            }
        }
    }
}

