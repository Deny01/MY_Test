namespace KReport.Engine
{
    using KReport.Controls;
    using MakeSoft.Tools.BarCodes;
    using System;
    using System.Drawing;
    using System.Xml;

    public class ReportTemplateXML
    {
        private Report report;
        private XmlDocument xDoc = null;

        public ReportTemplateXML(ReportBase report)
        {
            this.report = (Report) report;
            this.xDoc = new XmlDocument();
        }

        private void AddAtributo(XmlElement xElementRoot, string name, string value)
        {
            XmlAttribute node = this.xDoc.CreateAttribute(name);
            node.Value = value;
            xElementRoot.Attributes.Append(node);
        }

        private void AddControlBarCode(XmlElement xElementRoot, RBarCode control)
        {
            this.AddAtributo(xElementRoot, "Text", control.Text);
            this.AddAtributo(xElementRoot, "Code", control.Code);
            this.AddAtributo(xElementRoot, "BarCodeType", Convert.ToInt16(control.BarCodeType).ToString());
            XmlElement element = this.AddElement(xElementRoot, "Font");
            this.AddAtributo(element, "Name", control.Font.Name.ToString());
            this.AddAtributo(element, "Size", control.Font.Size.ToString());
            this.AddAtributo(element, "Height", control.Font.Height.ToString());
            this.AddAtributo(element, "FontStyle", control.Font.Style.ToString());
        }

        private void AddControlDBBarCode(XmlElement xElementRoot, RDBBarCode control)
        {
            this.AddControlBarCode(xElementRoot, control);
            this.AddAtributo(xElementRoot, "FieldName", control.数据字段);
            this.AddAtributo(xElementRoot, "DataSource", control.数据源);
        }

        private void AddControlDBCalc(XmlElement xElementRoot, RDBCalculated control)
        {
            this.AddControlDBText(xElementRoot, control);
            this.AddAtributo(xElementRoot, "Calculated", ((int)control.统计字段).ToString());
        }

        private void AddControlDBImage(XmlElement xElementRoot, RDBImage control)
        {
            this.AddControlImage(xElementRoot, control);
            this.AddAtributo(xElementRoot, "FieldName", control.数据字段);
            this.AddAtributo(xElementRoot, "DataSource", control.数据源);
        }

        private void AddControlDBText(XmlElement xElementRoot, RDBLabel control)
        {
            this.AddControlText(xElementRoot, control);
            this.AddAtributo(xElementRoot, "FieldName", control.数据字段);
            this.AddAtributo(xElementRoot, "DataSource", control.数据源);
            this.AddAtributo(xElementRoot, "FormatString", control.格式化显示);
        }

        private void AddControlImage(XmlElement xElementRoot, RImage control)
        {
            this.AddAtributo(xElementRoot, "BorderColor", control.边界颜色.Name);
            this.AddAtributo(xElementRoot, "BorderWidth", control.边界线宽.ToString());
            this.AddAtributo(xElementRoot, "FileName", control.文件);
            this.AddAtributo(xElementRoot, "Stretch", control.Stretch.ToString());
        }

        private void AddControlLine(XmlElement xElementRoot, RLine control)
        {
            this.AddAtributo(xElementRoot, "Alignment", ((int) control.对齐方式).ToString());
            this.AddAtributo(xElementRoot, "LineStyle", Convert.ToInt16(control.线段类型).ToString());
            this.AddAtributo(xElementRoot, "LineWeight", control.线宽.ToString());
        }

        private void AddControlMemo(XmlElement xElementRoot, RMemo control)
        {
        }

        private void AddControlShape(XmlElement xElementRoot, RShape control)
        {
            this.AddAtributo(xElementRoot, "BorderColor", control.边界颜色.Name);
            this.AddAtributo(xElementRoot, "BorderWidth", control.边界线宽.ToString());
        }

        private void AddControlSystem(XmlElement xElementRoot, RSystem control)
        {
            this.AddControlText(xElementRoot, control);
            this.AddAtributo(xElementRoot, "SystemType", Convert.ToInt16(control.时间页码).ToString());
        }

        private void AddControlText(XmlElement xElementRoot, RLabel control)
        {
            this.AddAtributo(xElementRoot, "Text", control.Text);
            this.AddAtributo(xElementRoot, "Alignment", ((short) control.对齐方式).ToString());
            XmlElement element = this.AddElement(xElementRoot, "Font");
            this.AddAtributo(element, "Name", control.Font.Name.ToString());
            this.AddAtributo(element, "Size", control.Font.Size.ToString());
            this.AddAtributo(element, "Height", control.Font.Height.ToString());
            this.AddAtributo(element, "FontStyle", control.Font.Style.ToString());
        }

        private XmlElement AddElement(XmlElement xElementRoot, string name)
        {
            XmlElement newChild = this.xDoc.CreateElement(name);
            xElementRoot.AppendChild(newChild);
            return newChild;
        }

        private bool GetBoolean(XmlAttributeCollection atributos, string elementName)
        {
            string elementValue = this.GetElementValue(atributos, elementName);
            if (elementValue == string.Empty)
            {
                return false;
            }
            return bool.Parse(elementValue);
        }

        private string GetElementValue(XmlAttributeCollection atributos, string elementName)
        {
            if (atributos[elementName] != null)
            {
                return atributos[elementName].Value;
            }
            return string.Empty;
        }

        private int GetInteger(XmlAttributeCollection atributos, string elementName)
        {
            string elementValue = this.GetElementValue(atributos, elementName);
            if (elementValue == string.Empty)
            {
                return 0;
            }
            return int.Parse(elementValue);
        }

        private string GetString(XmlAttributeCollection atributos, string elementName)
        {
            return this.GetElementValue(atributos, elementName);
        }

        public void LoadFromXML(string fileName)
        {
            this.xDoc.Load(fileName);
            this.SetTemplateReport();
        }

        private void MakeXmlBandGroup(XmlElement xElementRoot, BandGroup band)
        {
            this.AddAtributo(xElementRoot, "Type", band.BandType.ToString());
            this.AddAtributo(xElementRoot, "Name", band.Name);
            this.AddAtributo(xElementRoot, "Top", band.Top.ToString());
            this.AddAtributo(xElementRoot, "FieldName", band.FieldName);
            XmlElement element = this.AddElement(xElementRoot, "BandGroupHeader");
            this.MakeXmlBandSettings(element, band.BandHeader);
            element = this.AddElement(xElementRoot, "BandGroupFooder");
            this.MakeXmlBandSettings(element, band.BandFooder);
        }

        private void MakeXmlBandSettings(XmlElement xElementRoot, BandBase band)
        {
            this.AddAtributo(xElementRoot, "Type", band.BandType.ToString());
            this.AddAtributo(xElementRoot, "Name", band.Name);
            this.AddAtributo(xElementRoot, "Top", band.Top.ToString());
            this.AddAtributo(xElementRoot, "Height", band.Height.ToString());
            foreach (CustomControl control in band.Controls)
            {
                XmlElement element = this.AddElement(xElementRoot, "control");
                this.MakeXmlControlSettings(element, control);
            }
        }

        private void MakeXmlControlSettings(XmlElement xElementRoot, CustomControl control)
        {
            this.AddAtributo(xElementRoot, "Name", control.Name);
            this.AddAtributo(xElementRoot, "Type", control.GetType().Name);
            this.AddAtributo(xElementRoot, "BackColor", control.BackColor.Name);
            this.AddAtributo(xElementRoot, "ForeColor", control.ForeColor.Name);
            this.AddAtributo(xElementRoot, "xRelative", control.水平位置.ToString());
            this.AddAtributo(xElementRoot, "yRelative", control.垂直位置.ToString());
            this.AddAtributo(xElementRoot, "Index", control.Index.ToString());
            XmlElement element = this.AddElement(xElementRoot, "Position");
            this.AddAtributo(element, "Top", control.Top.ToString());
            this.AddAtributo(element, "Left", control.Left.ToString());
            this.AddAtributo(element, "Height", control.Height.ToString());
            this.AddAtributo(element, "Width", control.Width.ToString());
            if (control.GetType().Name == typeof(RLabel).Name)
            {
                this.AddControlText(xElementRoot, (RLabel) control);
            }
            if (control.GetType().Name == typeof(RShape).Name)
            {
                this.AddControlShape(xElementRoot, (RShape) control);
            }
            if (control.GetType().Name == typeof(RLine).Name)
            {
                this.AddControlLine(xElementRoot, (RLine) control);
            }
            if (control.GetType().Name == typeof(RImage).Name)
            {
                this.AddControlImage(xElementRoot, (RImage) control);
            }
            if (control.GetType().Name == typeof(RDBImage).Name)
            {
                this.AddControlDBImage(xElementRoot, (RDBImage) control);
            }
            if (control.GetType().Name == typeof(RDBLabel).Name)
            {
                this.AddControlDBText(xElementRoot, (RDBLabel) control);
            }
            if (control.GetType().Name == typeof(RMemo).Name)
            {
                this.AddControlMemo(xElementRoot, (RMemo) control);
            }
            if (control.GetType().Name == typeof(RDBCalculated).Name)
            {
                this.AddControlDBCalc(xElementRoot, (RDBCalculated) control);
            }
            if (control.GetType().Name == typeof(RBarCode).Name)
            {
                this.AddControlBarCode(xElementRoot, (RBarCode) control);
            }
            if (control.GetType().Name == typeof(RDBBarCode).Name)
            {
                this.AddControlDBBarCode(xElementRoot, (RDBBarCode) control);
            }
            if (control.GetType().Name == typeof(RSystem).Name)
            {
                this.AddControlSystem(xElementRoot, (RSystem) control);
            }
        }

        private void MakeXmlReportSettings(XmlElement xElementReport)
        {
            XmlElement element2;
            this.AddAtributo(xElementReport, "Autor", "Fernado Gregorio Borges");
            this.AddAtributo(xElementReport, "Version", "0.0.0.1");
            XmlElement xElementRoot = this.AddElement(xElementReport, "Page");
            this.AddAtributo(xElementRoot, "PaperName", this.report.PageSetting.PaperName);
            this.AddAtributo(xElementRoot, "Height", this.report.PageSetting.PageHeight.ToString());
            this.AddAtributo(xElementRoot, "Width", this.report.PageSetting.PageWidth.ToString());
            this.AddAtributo(xElementRoot, "MarginTop", this.report.PageSetting.MarginTop.ToString());
            this.AddAtributo(xElementRoot, "MarginBottom", this.report.PageSetting.MarginBottom.ToString());
            this.AddAtributo(xElementRoot, "MarginLeft", this.report.PageSetting.MarginLeft.ToString());
            this.AddAtributo(xElementRoot, "MarginRigth", this.report.PageSetting.MarginRigth.ToString());
            this.AddAtributo(xElementRoot, "LandScape", this.report.PageSetting.LandScape.ToString());
            this.AddAtributo(xElementRoot, "MasterDataSource", this.report.MasterDataSource);
            foreach (BandBase base2 in this.report.Bands)
            {
                if (!((base2 is BandGroupFooder) || (base2 is BandGroupHeader)))
                {
                    element2 = this.AddElement(xElementReport, "Band");
                    this.MakeXmlBandSettings(element2, base2);
                }
            }
            foreach (BandGroup group in this.report.BandsGroup)
            {
                element2 = this.AddElement(xElementReport, "BandGroup");
                this.MakeXmlBandGroup(element2, group);
            }
        }

        private void ProcessBand(XmlElement xElementBand)
        {
            XmlAttribute attribute = xElementBand.Attributes["Type"];
            BandBase band = null;
            if (attribute != null)
            {
                if (attribute.Value == BandType.BandTitle.ToString())
                {
                    band = BandFactory.CreateInstance(BandType.BandTitle);
                }
                if (attribute.Value == BandType.BandDetail.ToString())
                {
                    band = BandFactory.CreateInstance(BandType.BandDetail);
                }
                if (attribute.Value == BandType.BandFooder.ToString())
                {
                    band = BandFactory.CreateInstance(BandType.BandFooder);
                }
                if (attribute.Value == BandType.BandSummary.ToString())
                {
                    band = BandFactory.CreateInstance(BandType.BandSummary);
                }
                if (attribute.Value == BandType.BandHeader.ToString())
                {
                    band = BandFactory.CreateInstance(BandType.BandHeader);
                }
                if (attribute.Value == BandType.BandGroupHeader.ToString())
                {
                    band = BandFactory.CreateInstance(BandType.BandGroupHeader);
                }
                if (attribute.Value == BandType.BandGroupFooder.ToString())
                {
                    band = BandFactory.CreateInstance(BandType.BandGroupFooder);
                }
            }
            if (band != null)
            {
                attribute = xElementBand.Attributes["Top"];
                if (attribute != null)
                {
                    band.Top = Convert.ToInt16(attribute.Value);
                }
                attribute = xElementBand.Attributes["Height"];
                if (attribute != null)
                {
                    band.Height = Convert.ToInt16(attribute.Value);
                }
                foreach (XmlElement element in xElementBand.ChildNodes)
                {
                    this.ProcessControls(element, band);
                }
            }
            this.report.AddBand(band);
        }

        private void ProcessBandGroup(XmlElement xElementBand)
        {
            XmlAttribute attribute = xElementBand.Attributes["Type"];
            BandGroup band = null;
            band = new BandGroup {
                FieldName = xElementBand.Attributes["FieldName"].Value
            };
            BandBase bandHeader = null;
            foreach (XmlElement element in xElementBand.ChildNodes)
            {
                if (element.Name.Equals("BandGroupHeader"))
                {
                    bandHeader = band.BandHeader;
                }
                if (element.Name.Equals("BandGroupFooder"))
                {
                    bandHeader = band.BandFooder;
                }
                bandHeader.Top = this.GetInteger(element.Attributes, "Top");
                bandHeader.Height = this.GetInteger(element.Attributes, "Height");
                foreach (XmlElement element2 in element.ChildNodes)
                {
                    this.ProcessControls(element2, bandHeader);
                }
            }
            this.report.AddBand(band);
        }

        private void ProcessBarCode(XmlElement xElementControl, RBarCode control)
        {
            control.Code = this.GetString(xElementControl.Attributes, "Code");
            control.BarCodeType = (BarCodeType) this.GetInteger(xElementControl.Attributes, "BarCodeType");
        }

        private void ProcessControls(XmlElement xElementControl, BandBase band)
        {
            string str = this.GetString(xElementControl.Attributes, "Type");
            CustomControl control = null;
            if (!string.IsNullOrEmpty(str))
            {
                control = null;
                if (str == typeof(RLabel).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlText);
                    this.ProcessText(xElementControl, (RLabel) control);
                }
                if (str == typeof(RShape).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlShape);
                    this.ProcessShape(xElementControl, (RShape) control);
                }
                if (str == typeof(RLine).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlLine);
                    this.ProcessLine(xElementControl, (RLine) control);
                }
                if (str == typeof(RImage).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlImage);
                    this.ProcessImage(xElementControl, (RImage) control);
                }
                if (str == typeof(RDBImage).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlDBImage);
                    this.ProcessDBImage(xElementControl, (RDBImage) control);
                }
                if (str == typeof(RDBLabel).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlDBText);
                    this.ProcessDBText(xElementControl, (RDBLabel) control);
                }
                if (str == typeof(RDBCalculated).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlDBCalc);
                    this.ProcessDBCalc(xElementControl, (RDBCalculated) control);
                }
                if (str == typeof(RBarCode).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlBarCode);
                    this.ProcessBarCode(xElementControl, (RBarCode) control);
                }
                if (str == typeof(RDBBarCode).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlDBBarCode);
                    this.ProcessDBBarCode(xElementControl, (RDBBarCode) control);
                }
                if (str == typeof(RMemo).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlMemo);
                }
                if (str == typeof(RSystem).Name)
                {
                    control = CustomControl.FactoryControl(ControlType.ControlSystem);
                    this.ProcessSystem(xElementControl, (RSystem) control);
                }
            }
            if (control != null)
            {
                control.Name = this.GetString(xElementControl.Attributes, "Name");
                control.垂直位置  = this.GetInteger(xElementControl.Attributes, "yRelative");
                control.水平位置  = this.GetInteger(xElementControl.Attributes, "xRelative");
                control.BackColor = Color.FromName(this.GetString(xElementControl.Attributes, "BackColor"));
                control.ForeColor = Color.FromName(this.GetString(xElementControl.Attributes, "ForeColor"));
                control.Index = this.GetInteger(xElementControl.Attributes, "Index");
                if (xElementControl["Position"] != null)
                {
                    control.Top = Convert.ToInt16(xElementControl["Position"].Attributes["Top"].Value);
                    control.Left = Convert.ToInt16(xElementControl["Position"].Attributes["Left"].Value);
                    control.Height = Convert.ToInt16(xElementControl["Position"].Attributes["Height"].Value);
                    control.Width = Convert.ToInt16(xElementControl["Position"].Attributes["Width"].Value);
                }
                control.Band = band;
            }
        }

        private void ProcessDBBarCode(XmlElement xElementControl, RDBBarCode control)
        {
            this.ProcessBarCode(xElementControl, control);
            control.数据字段 = this.GetString(xElementControl.Attributes, "FieldName");
            control.数据源= this.GetString(xElementControl.Attributes, "DataSource");
        }

        private void ProcessDBCalc(XmlElement xElementControl, RDBCalculated control)
        {
            this.ProcessDBText(xElementControl, control);
            control.统计字段 = (TypeCalculated)this.GetInteger(xElementControl.Attributes, "Calculated");
        }

        private void ProcessDBImage(XmlElement xElementControl, RDBImage control)
        {
            this.ProcessImage(xElementControl, control);
            control.数据字段 = this.GetString(xElementControl.Attributes, "FieldName");
            control.数据源 = this.GetString(xElementControl.Attributes, "DataSource");
        }

        private void ProcessDBText(XmlElement xElementControl, RDBLabel control)
        {
            this.ProcessText(xElementControl, control);
            control.数据字段 = this.GetString(xElementControl.Attributes, "FieldName");
            control.数据源= this.GetString(xElementControl.Attributes, "DataSource");
            control.格式化显示 = this.GetString(xElementControl.Attributes, "FormatString");
            control.对齐方式 = (TextAlignment)this.GetInteger(xElementControl.Attributes, "Alignment");
        }

        private void ProcessImage(XmlElement xElementControl, RImage control)
        {
            XmlAttribute attribute = xElementControl.Attributes["Image"];
            if (attribute == null)
            {
            }
            control.边界线宽= this.GetInteger(xElementControl.Attributes, "BorderWidth");
            control.边界颜色 = Color.FromName(this.GetString(xElementControl.Attributes, "BorderColor"));
            control.Stretch = this.GetBoolean(xElementControl.Attributes, "Stretch");
            control.文件 = this.GetString(xElementControl.Attributes, "FileName");
        }

        private void ProcessLine(XmlElement xElementControl, RLine control)
        {
            control.对齐方式 = (LineAlignment) this.GetInteger(xElementControl.Attributes, "Alignment");
            control.ForeColor = Color.FromName(this.GetString(xElementControl.Attributes, "ForeColor"));
            control.线段类型 = (LineStyle) this.GetInteger(xElementControl.Attributes, "LineStyle");
            control.线宽 = this.GetInteger(xElementControl.Attributes, "LineWeight");
        }

        private void ProcessPage(XmlAttributeCollection atributos)
        {
            this.report.PageSetting.PaperName = this.GetString(atributos, "PaperName");
            this.report.PageSetting.PageHeight = this.GetInteger(atributos, "Height");
            this.report.PageSetting.PageWidth = this.GetInteger(atributos, "Width");
            this.report.PageSetting.MarginTop = this.GetInteger(atributos, "MarginTop");
            this.report.PageSetting.MarginBottom = this.GetInteger(atributos, "MarginBottom");
            this.report.PageSetting.MarginLeft = this.GetInteger(atributos, "MarginLeft");
            this.report.PageSetting.MarginRigth = this.GetInteger(atributos, "MarginRigth");
            this.report.PageSetting.LandScape = this.GetBoolean(atributos, "LandScape");
            this.report.MasterDataSource = this.GetString(atributos, "MasterDataSource");
        }

        private void ProcessShape(XmlElement xElementControl, RShape control)
        {
            control.边界线宽 = this.GetInteger(xElementControl.Attributes, "BorderWidth");
            control.边界颜色 = Color.FromName(this.GetString(xElementControl.Attributes, "BorderColor"));
        }

        private void ProcessSystem(XmlElement xElementControl, RSystem control)
        {
            control.时间页码 = (RSystemType) this.GetInteger(xElementControl.Attributes, "SystemType");
        }

        private void ProcessText(XmlElement xElementControl, RLabel control)
        {
            control.Text = this.GetString(xElementControl.Attributes, "Text");
            control.对齐方式 = (TextAlignment)this.GetInteger(xElementControl.Attributes, "Alignment");
            if (xElementControl["Font"] != null)
            {
                string familyName = xElementControl["Font"].Attributes["Name"].Value;
                int num = Convert.ToInt16(Math.Round(Convert.ToDouble(xElementControl["Font"].Attributes["Size"].Value)));
                FontStyle regular = FontStyle.Regular;
                if (xElementControl["Font"].Attributes["FontStyle"] != null)
                {
                    string str2 = xElementControl["Font"].Attributes["FontStyle"].Value;
                    if (str2.Equals(FontStyle.Bold.ToString()))
                    {
                        regular = FontStyle.Bold;
                    }
                    if (str2.Equals(FontStyle.Italic.ToString()))
                    {
                        regular = FontStyle.Italic;
                    }
                    if (str2.Equals(FontStyle.Strikeout.ToString()))
                    {
                        regular = FontStyle.Strikeout;
                    }
                    if (str2.Equals(FontStyle.Underline.ToString()))
                    {
                        regular = FontStyle.Underline;
                    }
                }
                control.Font = new Font(familyName, (float) num, regular);
            }
        }

        public void SaveToXML(string fileName)
        {
            XmlProcessingInstruction newChild = this.xDoc.CreateProcessingInstruction("xml", "version='1.0'");
            this.xDoc.AppendChild(newChild);
            XmlComment comment = this.xDoc.CreateComment("Informa\x00e7\x00e3o sobre layout do relat\x00f3rio");
            this.xDoc.AppendChild(comment);
            XmlElement element = this.xDoc.CreateElement("KReport");
            this.xDoc.AppendChild(element);
            this.MakeXmlReportSettings(element);
            this.xDoc.Save(fileName);
        }

        private void SetTemplateReport()
        {
            XmlElement element = this.xDoc["KReport"];
            if (element != null)
            {
                foreach (XmlElement element2 in element.ChildNodes)
                {
                    if (element2.Name.Equals("Page"))
                    {
                        this.ProcessPage(element2.Attributes);
                    }
                    if (element2.Name.Equals("Band"))
                    {
                        this.ProcessBand(element2);
                    }
                    if (element2.Name.Equals("BandGroup"))
                    {
                        this.ProcessBandGroup(element2);
                    }
                }
            }
        }
    }
}

