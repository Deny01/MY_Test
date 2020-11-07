namespace MakeSoft.Tools.Web
{
    using System;
    using System.Text;

    public class JavaScriptWindowPrint
    {
        public string ButtonName;
        private StringBuilder jscript;

        public JavaScriptWindowPrint(string buttonname)
        {
            this.ButtonName = buttonname;
            this.jscript = new StringBuilder();
        }

        public string ToString()
        {
            this.jscript.Append("<script type='text/javascript'>");
            this.jscript.Append(this.ButtonName + ".onclick=window.print();");
            this.jscript.Append(" } </script> ");
            return this.jscript.ToString();
        }
    }
}

