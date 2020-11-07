namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;

    internal class DataSourceDefault : StringConverter
    {
        public static ReportDataCollection ListDataSources;

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            string[] strArray;
            if (ListDataSources.Count > 0)
            {
                strArray = new string[ListDataSources.Count];
            }
            else
            {
                strArray = new string[] { string.Empty };
            }
            int index = 0;
            foreach (IReportData data in ListDataSources)
            {
                strArray[index] = data.DataSourceName;
                index++;
            }
            return new TypeConverter.StandardValuesCollection(strArray);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}

