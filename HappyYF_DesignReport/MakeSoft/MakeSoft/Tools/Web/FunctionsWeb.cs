namespace MakeSoft.Tools.Web
{
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public abstract class FunctionsWeb
    {
        protected FunctionsWeb()
        {
        }

        public static void PopulatedCombo(DropDownList combo, DataTable source, string[] listDisplayFieldName, string fieldNameKey)
        {
            combo.Items.Clear();
            combo.Items.Add(new ListItem("Todos", "-1"));
            foreach (DataRow row in source.Rows)
            {
                string str = string.Empty;
                foreach (string str2 in listDisplayFieldName)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str + " - ";
                    }
                    str = str + row[str2].ToString();
                }
                combo.Items.Add(new ListItem(str, row[fieldNameKey].ToString()));
            }
        }
    }
}

