using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TGZJ.Base
{
    public partial class ButtonMenu : UserControl
    {
        public event EventHandler OnMenuSelection;
        private static ArrayList NavItems = new ArrayList();
        private static string selecteditem = string.Empty;

        private System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources", System.Reflection.Assembly.GetExecutingAssembly());
        
        
        
        public ButtonMenu()
        {
            InitializeComponent();
        }

        public ArrayList MenuItems
        {
            set
            {
                NavItems = value;
            }
            get
            {
                return (NavItems);
            }
        }





        private void ctrlMenuBar_Load(object sender, EventArgs e)
        {

            /**************************/




            /**************************/





        }
        public void RenderMenu()
        {
            if (NavItems.Count == 0) return;
            LoadMenuItems(NavItems);
        }

        private void LoadMenuItems(ArrayList NavItems)
        {
            int btnHeight = Properties.Resources.btnBgnd.Height;
          

            Panel mainpanel = new Panel();
            mainpanel.Dock = DockStyle.Top;
            mainpanel.AutoSize = true;
            mainpanel.Name = "mainpanel";

            Panel pnl = new Panel();
            pnl.Height = 20;
            pnl.Dock = DockStyle.Top;
            mainpanel.Controls.Add(pnl);
            string selectedbutton = string.Empty;
            foreach (NavItem navitems in NavItems)
            {
                Button btn = new Button();
                btn.Height = btnHeight;
                //btn.BackgroundImage = (Image)Properties.Resources.btnBgnd;
                btn.BackgroundImage = Properties.Resources.btnBgnd;
                btn.Dock = DockStyle.Bottom;
                btn.Name = navitems.eventCode;
                btn.Click += new EventHandler(btn_Click);
                btn.Text = navitems.MnuItem;

                if (navitems.Selected == true) selectedbutton = btn.Name;

                mainpanel.Controls.Add(btn);


                pnl = new Panel();
                pnl.Dock = DockStyle.Bottom;
                pnl.Name = "panel_" + navitems.eventCode;
                pnl.Visible = false;

                foreach (childNavItems childnavitems in navitems.childNavItems)
                {
                    pnl.Height = navitems.childNavItems.Count * btnHeight;
                    Button childbtn = new Button();

                    childbtn.FlatStyle = FlatStyle.Flat;
                    childbtn.FlatAppearance.MouseOverBackColor = Color.LightGray;
                    childbtn.Height = btnHeight;
                    childbtn.Dock = DockStyle.Bottom;
                    childbtn.Text = "- " + childnavitems.MnuItem;
                    childbtn.TextAlign = ContentAlignment.MiddleLeft;
                    childbtn.Click += new EventHandler(childbtnbtn_Click);
                    childbtn.Name = childnavitems.eventCode;


                    pnl.Controls.Add(childbtn);
                }

                mainpanel.Controls.Add(pnl);


            }
            panel1.Controls.Add(mainpanel);


            if (selectedbutton != string.Empty)
            {
                Button btn = (Button)panel1.Controls["mainpanel"].Controls[selectedbutton];
                selecteditem = btn.Name;
                ShowChilds(btn);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (selecteditem != ((Button)sender).Name)
            {
                ShowChilds((Button)sender);
            }


            OnMenuSelection(sender, e);
            selecteditem = ((Button)sender).Name;

        }
        private void ShowChilds(Button btn)
        {
            ResetAllPanels();

            Panel pnl = (Panel)panel1.Controls["mainpanel"].Controls["panel_" + btn.Name];
            pnl.Visible = true;


        }
        private void childbtnbtn_Click(object sender, EventArgs e)
        {
            OnMenuSelection(sender, e);
        }
        private void ResetAllPanels()
        {
            foreach (Control ctrl in panel1.Controls["mainpanel"].Controls)
            {
                if (ctrl is Panel)
                {
                    if (ctrl.Name.Length >= 6)
                    {
                        if (ctrl.Name.Substring(0, 6) == "panel_")
                        {
                            if (ctrl.Visible == true) ctrl.Visible = false;
                        }
                    }
                }
            }
        }

        public class NavItem
        {
            public string MnuItem;
            public string eventCode;
            public ArrayList childNavItems;
            public bool Selected = false;

            public NavItem(string _MnuItem, string _eventCode, ArrayList _childNavItems, bool _selected)
            {
                MnuItem = _MnuItem;
                eventCode = _eventCode;
                childNavItems = _childNavItems;
                Selected = _selected;
            }
            public NavItem(string _MnuItem, string _eventCode, ArrayList _childNavItems)
            {
                MnuItem = _MnuItem;
                eventCode = _eventCode;
                childNavItems = _childNavItems;
            }

        }

        public class childNavItems
        {
            public string MnuItem;
            public string eventCode;
            public childNavItems(string _MnuItem, string _eventCode)
            {
                MnuItem = _MnuItem;
                eventCode = _eventCode;
            }
        }


    }
}
