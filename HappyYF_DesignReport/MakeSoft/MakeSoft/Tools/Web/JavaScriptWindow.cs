namespace MakeSoft.Tools.Web
{
    using System;
    using System.Text;

    public class JavaScriptWindow
    {
        private string features;
        public bool HasBrowser;
        public bool HasMenuBar;
        public bool HasScrollBar;
        public bool HasStatusBar;
        public bool HasTitleBar;
        public int Height;
        public bool IsResizable;
        public bool IsTitleBar;
        private StringBuilder jscript;
        public string Title;
        public string Url;
        public int Width;
        public string WindowName;

        public JavaScriptWindow(string windowname, string url, string title)
        {
            this.HasBrowser = false;
            this.HasMenuBar = false;
            this.HasTitleBar = false;
            this.HasScrollBar = true;
            this.HasStatusBar = true;
            this.IsResizable = true;
            this.IsTitleBar = true;
            this.Width = 200;
            this.Height = 300;
            this.WindowName = "";
            this.Title = "";
            this.Url = "";
            this.features = "";
            this.Initialize(windowname, url, title, true, 400, 400);
        }

        public JavaScriptWindow(string windowname, string url, string title, bool isresizable, int width, int height)
        {
            this.HasBrowser = false;
            this.HasMenuBar = false;
            this.HasTitleBar = false;
            this.HasScrollBar = true;
            this.HasStatusBar = true;
            this.IsResizable = true;
            this.IsTitleBar = true;
            this.Width = 200;
            this.Height = 300;
            this.WindowName = "";
            this.Title = "";
            this.Url = "";
            this.features = "";
            this.Initialize(windowname, url, title, isresizable, width, height);
        }

        private void Initialize(string windowname, string url, string title, bool isresizable, int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.IsResizable = false;
            this.WindowName = windowname;
            this.Url = url;
            this.Title = title;
            this.jscript = new StringBuilder();
        }

        public string ToString()
        {
            if (this.HasBrowser)
            {
                this.features = this.features + "location=yes,";
            }
            else
            {
                this.features = this.features + "location=no,";
            }
            if (this.HasMenuBar)
            {
                this.features = this.features + "menubar=yes,";
            }
            else
            {
                this.features = this.features + "menubar=no,";
            }
            if (this.HasScrollBar)
            {
                this.features = this.features + "scrollbars=yes,";
            }
            else
            {
                this.features = this.features + "scrollbars=no,";
            }
            if (this.IsResizable)
            {
                this.features = this.features + "resizable=yes,";
            }
            else
            {
                this.features = this.features + "resizable=no,";
            }
            if (this.IsTitleBar)
            {
                this.features = this.features + "titlebar=yes,";
            }
            else
            {
                this.features = this.features + "titlebar=no,";
            }
            this.features = this.features + "width = " + this.Width.ToString() + ",";
            this.features = this.features + "height = " + this.Height.ToString() + ",";
            if (this.HasScrollBar)
            {
                this.features = this.features + "status=yes";
            }
            else
            {
                this.features = this.features + "status=no";
            }
            this.jscript.Append("<script type='text/javascript'>");
            this.jscript.Append(" window.onload=janelaview;");
            this.jscript.Append(" function janelaview() { ");
            this.jscript.Append(" windowhandle = window.open('" + this.Url + "','" + this.WindowName + "','" + this.features + "');");
            this.jscript.Append(" windowhandle.document.title = '" + this.Title + "';");
            this.jscript.Append(" } </script> ");
            return this.jscript.ToString();
        }
    }
}

