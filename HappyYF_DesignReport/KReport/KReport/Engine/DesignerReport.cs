namespace KReport.Engine
{
    using KReport;
    using KReport.Controls;
    using KReport.Designer;
    using KReport.Forms;
    using KReport.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel.Design;
    using KReport.UndoRedo;

    //internal class DesignerReport : Form
    public  class DesignerReport : Form
    {
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private int BottonAreaDesigner;
        private Label browserLabel;
        private ToolStripMenuItem cabecalhoToolStripMenuItem;
        private ToolStripMenuItem centimetrosToolStripMenuItem;
        private object clone;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripComboBox comboDataSource;
        private ToolStripComboBox comboFieldName;
        private IContainer components;
        private ContextMenuStrip contextMenuStripControls;
        private ControlBox controlBox;
        private object controlType;
        private ControlViewer controlViewer;
        private ToolStripButton copyToolStripButton;
        //private BandBase CurrentBand;
        public  BandBase CurrentBand;
        //private BandDesigner CurrentBandControl;
        public  BandDesigner CurrentBandControl;
        private CustomControl CurrentControl;
        private Report CurrentReport;
        private ToolStripButton cutToolStripButton;
        private ToolStripMenuItem dadosToolStripMenuItem;
        private ToolStripButton deleteToolStripButton;
        private static DesignerReport designer = null;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem gruposToolStripMenuItem;
        private ToolStripButton helpToolStripButton;
        private int LastTopAreaDesigner;
        private Label LblObject;
        private ToolStripMenuItem medidasToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem milimetrosToolStripMenuItem;
        private bool movingband;
        private bool movingcontrol;
        private ToolStripButton newToolStripButton;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripButton openToolStripButton;
        private ToolStripMenuItem openToolStripMenuItem;
        private Panel panel1;
        private Panel panel3;
        private ToolStripButton pasteToolStripButton;
        private PictureBox pictureBox1;
        private ToolStripMenuItem pixelToolStripMenuItem;
        private Panel PnlAreaDesigner;
        private Panel PnlBackGroundBottom;
        private Panel pnlBackGroundRigth;
        private Panel PnlBrowse;
        private Panel pnlControls;
        private Panel PnlDesigner;
        private Panel PnlProperty;
        private Panel PnlRuleHorizontal;
        private Panel PnlRuleVertical;
        private ToolStripMenuItem polegadasToolStripMenuItem;
        private Point positionoff;
        private ToolStripButton printToolStripButton;
        private ToolStripMenuItem printToolStripMenuItem;
        private PropertyGrid propertyGrid;
        private Label propertyLabel;
        private ToolStripMenuItem relatorioToolStripMenuItem;
        private ToolStripMenuItem rodapeToolStripMenuItem;
        private Ruler RulerHorizontal;
        private ToolStripMenuItem salveAsToolStripMenuItem;
        private ToolStripButton saveToolStripButton;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ScrollableControl scrollBackGround;
        private ToolStripMenuItem settingsPageToolStripMenuItem;
        private Splitter splitter1;
        private Splitter splitter3;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem sumarioToolStripMenuItem;
        private TabControl tabControl1;
        private TabControl tabControlReport;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage TabPageDesigner;
        private TabPage TabPageView;
        private TabControl TbCrlReportDesigner;
        private ToolStripMenuItem tituloToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButtonAlingCenter;
        private ToolStripButton toolStripButtonAlingLeft;
        private ToolStripButton toolStripButtonAlingRigth;
        private ToolStripButton toolStripButtonCursor;
        private ToolStripButton toolStripButtonItalico;
        private ToolStripButton toolStripButtonNegrito;
        private ToolStripButton toolStripButtonRBarCode;
        private ToolStripButton toolStripButtonRCalculated;
        private ToolStripButton toolStripButtonRChart;
        private ToolStripButton toolStripButtonRDBBarCode;
        private ToolStripButton toolStripButtonRDBImage;
        private ToolStripButton toolStripButtonRDBLabel;
        private ToolStripButton toolStripButtonRImage;
        private ToolStripButton toolStripButtonRLabel;
        private ToolStripButton toolStripButtonRLine;
        private ToolStripButton toolStripButtonRSharp;
        private ToolStripButton toolStripButtonRSystem;
        private ToolStripComboBox toolStripComboBoxFont;
        private ToolStrip toolStripControls;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private NumericUpDown toolStripSizeFont;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripStatusLabel toolStripStatusLabelCoordenadas;
        private ToolStripStatusLabel toolStripStatusPositionX;
        private ToolStripStatusLabel toolStripStatusPositionY;
        private int TopAreaDesigner;
        private TreeView treeViewDataSource;
        private TreeView treeViewReport;
        private ToolStripButton viewtoolStripButton;

        #region By Deny

        private Rectangle Rec_Old = new Rectangle(0, 0, 0, 0);

        /// <summary>
        /// 上一次鼠标移动时的X轴坐标坐标
        /// </summary>
        private int Last_MouseMoveX;

        /// <summary>
        /// 上一次鼠标移动时的X轴坐标坐标
        /// </summary>
        private int Last_MouseMoveY;

        /// <summary>
        /// 选中的控件集合
        /// </summary>
        private List<Control> alControl = new List<Control>();
        private List<SizeLocation> alControlBak = new List<SizeLocation>();
        private ToolStripButton toolStripButtonAlignBottom;
        private ToolStripButton toolStripButtonAlignLeft;
        private ToolStripButton toolStripButtonAlignRight;
        private ToolStripButton toolStripButtonAlignTop;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton toolStripButtonWidthSame;
        private ToolStripButton toolStripButtonHeightSame;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripButton toolStripButtonUndo;
        private ToolStripButton toolStripButtonRedo;
        private ToolStripSeparator toolStripSeparator9;
       
       // private List<ControlBox_Select> alcontrolBox = new List<ControlBox_Select>();
        ControlBox_Select temcontrolBox; 
        /// <summary>
        /// 选中的控件集合备份
        /// </summary>
       // private ArrayList alControlBak = new ArrayList();


        #region 在画矩形框时加入被包含的控件
        /// <summary>
        /// 求出选中哪些控件
        /// </summary>
        /// <param name="x">矩形框的起点横作标</param>
        /// <param name="y">矩形框的起点纵作标</param>
        /// <param name="width">矩形框的宽度</param>
        /// <param name="height">矩形框的高度</param>
        private void ControlContainer(int x, int y, int width, int height)
        {
            foreach (Control cr in this.alControl)
            {
                //ControlBox_Select cs = cr as ControlBox_Select
                ((CustomControl)cr).Cs.Remove();
            }
            alControl.Clear();
            //alcontrolBox.Clear();
            foreach (Control control in PnlAreaDesigner.Controls)
            {
                if (!(control is CustomControl)) continue;
                if (control.Visible == false) continue;
                if (InvoleControl(control, x, y, x + width, y)) continue;
                if (InvoleControl(control, x, y, x, y + height)) continue;
                if (InvoleControl(control, x, y + height, x + width, y + height)) continue;
                if (InvoleControl(control, x + width, y, x + width, y + height)) continue;
                CircleControl(control, x, y, width, height);

            }

            if (!this.alControl.Contains(this.CurrentControl ))
            {
                alControl.Add(this.CurrentControl);
            }

            //SetSelectedComponents();
        }

        /// <summary>
        /// 矩形框压中的控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="x1">矩形边起点横坐标</param>
        /// <param name="y1">矩形边起点横坐标</param>
        /// <param name="x2">矩形边终点点横坐标</param>
        /// <param name="y2">矩形边终点点纵坐标</param>
        /// <returns></returns>
        private bool InvoleControl(Control control, int x1, int y1, int x2, int y2)
        {
            //控件起点的横坐标
            int cx = control.Location.X;

            //控件起点的纵坐标
            int cy = control.Location.Y;

            //控件的宽度
            int cwidth = control.Size.Width;

            //控件的高度
            int cheight = control.Height;


            //横线穿过
            if (y1 == y2)
            {
                if (y1 >= cy && y1 <= cy + cheight && ((x1 <= cx && x2 >= cx) || (x1 <= cx + cwidth && x2 >= cx + cwidth)))
                {
                    alControl.Add(control);

                    // temcontrolBox = new ControlBox_Select();
                    //temcontrolBox.WireControl(control);
                    //alcontrolBox.Add(temcontrolBox);
                   // slRec.Add(control.Name, new Rectangle(cx, cy, cwidth, cheight));
                    return true;
                }

            }

            //横线穿过
            if (x1 == x2)
            {
                if (x1 >= cx && x1 <= cx + cwidth && ((y1 <= cy && y2 >= cy) || (y1 <= cy + cheight && y2 >= cy + cheight)))
                {
                    alControl.Add(control);


                    //temcontrolBox = new ControlBox_Select();
                    //temcontrolBox.WireControl(control);
                    //alcontrolBox.Add(temcontrolBox);
                   // slRec.Add(control.Name, new Rectangle(cx, cy, cwidth, cheight));
                    return true;
                }

            }


            return false;


        }

        /// <summary>
        /// 在矩形框中内部的控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="x">矩形框的起点横坐标</param>
        /// <param name="y">矩形框的起点纵坐标</param>
        /// <param name="width">矩形框的宽度</param>
        /// <param name="height">矩形框的高度</param>
        private void CircleControl(Control control, int x, int y, int width, int height)
        {
            //控件起点的横坐标
            int cx = control.Location.X;

            //控件起点的纵坐标
            int cy = control.Location.Y;

            //控件的宽度
            int cwidth = control.Size.Width;

            //控件的高度
            int cheight = control.Height;
            if (x <= cx && y <= cy && x + width >= cx + cwidth && y + height >= cy + cheight)
            {

                //temcontrolBox = control;
                alControl.Add(control);


                
                //temcontrolBox.WireControl(control);
                //alcontrolBox.Add(temcontrolBox);
                //slRec.Add(control.Name, new Rectangle(cx, cy, cwidth, cheight));
            }

        }


        #endregion
        #region Form上的键盘事件

        /// <summary>
        /// 按下鼠标时是否已按下ctrl or shift 键
        /// </summary>
        private bool keyAssemble = false;

        /// <summary>
        /// 域，是否按下Arrow键
        /// </summary>
        private bool bKeyArrow = false;
        private void Report_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                bKeyArrow = true;
               // SetFocus();
            }

            if (propertyGrid.Parent.ContainsFocus) return;

            //if (e.KeyCode == Keys.Delete && alControl.Count > 0)
            if (e.KeyCode == Keys.Delete )
            {
                DeleteControl();
            }

            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
            {
                keyAssemble = true;
            }

            //移动控件方位
            //if (!((Control.ModifierKeys & Keys.Shift) == Keys.Shift || (Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyCode == Keys.Up)
            //{
            //    BackupSelectedComponents();

            //    DrawMoveRec(0, -1);
            //}

            //if (!((Control.ModifierKeys & Keys.Shift) == Keys.Shift || (Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyCode == Keys.Down)
            //{
            //    BackupSelectedComponents();

            //    DrawMoveRec(0, 1);
            //}

            //if (!((Control.ModifierKeys & Keys.Shift) == Keys.Shift || (Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyCode == Keys.Left)
            //{
            //    BackupSelectedComponents();

            //    DrawMoveRec(-1, 0);
            //}

            //if (!((Control.ModifierKeys & Keys.Shift) == Keys.Shift || (Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyCode == Keys.Right)
            //{
            //    BackupSelectedComponents();

            //    DrawMoveRec(1, 0);
            //}

            //改变控件大小
            //if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift || (Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyCode == Keys.Up)
            //{
            //    BackupSelectedComponents();

            //    ConditionStretchControl("DownMiddle", 0, -1);
            //}

            //全选
            //if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.KeyCode == Keys.A)
            //{
            //    alControl.Clear();
            //    slRec.Clear();
            //    foreach (Control control in page.Controls)
            //    {
            //        if (control.Visible == false) continue;
            //        alControl.Add(control);
            //        //					DrawControlRec(control,Color.White,Color.Black);
            //        slRec.Add(control.Name, new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height));
            //    }

            //    Canclulate_Control_MaxMinValue();
            //    if (alControl.Count > 1)
            //    {
            //        PrimaryControl = (Control)alControl[0];
            //    }

            //    DrawGrabHandle_ChooseControlsExtreme();
            //    SetSelectedComponents();
            //    keyAssemble = false;
            //}

            //if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift || (Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyCode == Keys.Down)
            //{
            //    BackupSelectedComponents();

            //    ConditionStretchControl("DownMiddle", 0, 1);
            //}


            //if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift || (Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyCode == Keys.Left)
            //{
            //    BackupSelectedComponents();

            //    ConditionStretchControl("RightMiddle", -1, 0);
            //}


            //if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift || (Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyCode == Keys.Right)
            //{
            //    BackupSelectedComponents();

            //    ConditionStretchControl("RightMiddle", 1, 0);
            //}
        }


        private void Report_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //			e.Handled = true;
            //if (e.KeyChar == Keys.ShiftKey || e.KeyCode == Keys.ControlKey)
            //{
            //    keyAssemble = false;
            //}
            char a = e.KeyChar;
        }


        private void Report_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                this.Close();
                return;
            }

            //if (e.Control && e.KeyCode == Keys.P)
            //{
            //    Print();
            //    return;
            //}

            //if (e.Control && e.KeyCode == Keys.B)
            //{
            //    Browse();
            //    return;
            //}

            //if (e.Control && e.KeyCode == Keys.S)
            //{
            //    Save(this.xmlName); //保存
            //    return;
            //}

            //if (this.CanUndo && e.Control && e.KeyCode == Keys.Z)
            //{
            //    Undo();
            //    return;
            //}

            //if (this.CanRedo && e.Control && e.KeyCode == Keys.Y)
            //{
            //    Redo();
            //    return;
            //}

            //if (this.alControl.Count > 0 && e.Control && e.KeyCode == Keys.X)
            //{
            //    Cut();
            //    return;
            //}

            //if (this.alControl.Count > 0 && e.Control && e.KeyCode == Keys.C)
            //{
            //    Copy();
            //    return;
            //}

            //if (this.CanPaste() && e.Control && e.KeyCode == Keys.V)
            //{
            //    Paste();
            //    return;
            //}

            //if (this.alControl.Count > 0 && e.Control && e.KeyCode == Keys.C)
            //{
            //    Paste();
            //    return;
            //}

            //if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            //{
            //    bKeyArrow = false;
            //}

            //if (propertyForm.HasFocus) return;


            //if ((Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift) && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            //{
            //    IsFirst = true;
            //    ReiniRec(0, 0, 0, 0);
            //    ReiniControl();
            //    this.page.Refresh();
            //    DrawGrabHandle_ChooseControlsExtreme();
            //    propertyForm.Refresh();
            //    keyAssemble = false;

            //    Edit_Info("Name", "Size", false);
            //    ChangeSelectComponentsSizeLocation("Size");
            //    TransactionsCommit();

            //}

            //全选
            //if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.KeyCode == Keys.A)
            //{
            //    DrawGrabHandle_ChooseControlsExtreme();
            //    keyAssemble = false;
            //}


            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey)
            {
                keyAssemble = false;
            }

            //if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && (Control.ModifierKeys & Keys.Control) == Keys.Control)
            //{
            //    this.page.Refresh();
            //}

            //if ((((Control.ModifierKeys & Keys.Shift) != Keys.Shift && (Control.ModifierKeys & Keys.Control) != Keys.Control)) && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            //{
            //    ReiniControl();
            //    IsFirst = true;
            //    DrawMoveRec(0, 0);
            //    this.page.Refresh();
            //    this.propertyForm.Refresh();

            //    DrawGrabHandle_ChooseControlsExtreme();

            //    Edit_Info("Name", "Location", false);
            //    ChangeSelectComponentsSizeLocation("Location");
            //    TransactionsCommit();

            //}

            //IsFirst = true;
        }



        #endregion
        #endregion

        protected DesignerReport()
        {
            this.components = null;
            this.LastTopAreaDesigner = 0;
            this.movingband = false;
            this.movingcontrol = false;
            this.TopAreaDesigner = 0;
            this.BottonAreaDesigner = 0;
            this.controlViewer = null;
            this.InitializeComponent();

            undoHandler.Attach(this);
        }

        protected DesignerReport(Report report)
        {
            this.components = null;
            this.LastTopAreaDesigner = 0;
            this.movingband = false;
            this.movingcontrol = false;
            this.TopAreaDesigner = 0;
            this.BottonAreaDesigner = 0;
            this.controlViewer = null;
            this.controlBox = new ControlBox();
            this .controlBox .SizeChanging +=new MouseEventHandler(controlBox_SizeChanging);
            this.controlBox.SizeChanged +=new MouseEventHandler(controlBox_SizeChanged);
            this.CurrentReport = report;
            this.InitializeComponent();
            this.InitizalizeEventsTool();
            this.InitializeFonts();
            this.CreateButtonsControls();
            this.InitializeReportDesigner();
            //this.propertyGrid.BrowsableAttributes = new AttributeCollection(new Attribute[] { new CategoryAttribute("Custom"), new BrowsableAttribute(true) });
            this.propertyGrid.BrowsableAttributes = new AttributeCollection(new Attribute[] { new ReportElementAttribute("报表元素"), new BrowsableAttribute(true) });
            //this.propertyGrid.BrowsableAttributes = TypeDescriptor.GetAttributes(typeof(RDBLabel));

            

            this.Text = "一方报表 - " + report.FileName;

            undoHandler.Attach(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutReport().ShowDialog();
        }

        protected void AddBand(BandType bandtype)
        {
            BandBase band = BandFactory.CreateInstance(bandtype);
            this.CurrentReport.AddBand(band);
            this.RefreshWorkArea();
        }

        private void AddTreeViewControl(CustomControl control)
        {
            TreeNode[] nodeArray = this.treeViewReport.Nodes.Find(control.Band.Name, true);
            if (nodeArray.Length > 0)
            {
                this.treeViewReport.BeginUpdate();
                TreeNode node = nodeArray[0];
                TreeNode node2 = node.Nodes.Add(control.Name, control.Name);
                this.treeViewReport.EndUpdate();
                this.treeViewReport.Refresh();
                this.SelectNodeOnTreeView(control.Name);
            }
        }

        //private void ArrangeAreaDesigner()
        public  void ArrangeAreaDesigner()
        {
            int num = 0;
            BandBase base2 = null;
            foreach (BandBase base3 in this.CurrentReport.Bands)
            {
                base3.Top = num;
                base3.Ruler.Top = base3.Top + this.PnlAreaDesigner.Top;
                num = (base3.Top + base3.Height) + base3.DesignerControl.Height;
                base2 = base3;
            }
            if (base2 != null)
            {
                this.BottonAreaDesigner = base2.Top;
                this.PnlAreaDesigner.Height = (this.BottonAreaDesigner + base2.Height) + base2.DesignerControl.Height;
            }
        }

        //private void ArrangeBands()
        public  void ArrangeBands()
        {
            foreach (BandBase base2 in this.CurrentReport.Bands)
            {
                if (base2.DesignerControl != null)
                {
                    base2.DesignerControl.Location = new Point(0, base2.Top + base2.Height);
                }
                foreach (CustomControl control in base2.Controls)
                {
                    control.Location = new Point(control.Location.X, base2.Top + control.垂直位置 );
                }
            }
            if (this.propertyGrid.SelectedObject != null)
            {

                if (!keyAssemble)
                {
                    this.controlBox.SelectControl(this.propertyGrid.SelectedObject, null);
                    this.SelectedObjectPropertyGrid(this.propertyGrid.SelectedObject);
                }
            }
        }

        private void BandControlMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor.Current = Cursors.SizeNS;
                this.positionoff = e.Location;
                this.movingband = true;
                this.CurrentBandControl = (BandDesigner) sender;
                this.CurrentBand = (BandBase) this.CurrentBandControl.Tag;

                this.alControl.Add(this.CurrentControl);

               // BackupSelectedBand();
            }
        }

        private void BandControlMouseMove(object sender, MouseEventArgs e)
        {
            if (this.movingband && (this.CurrentBandControl != null))
            {
                Point point = this.CurrentBandControl.Parent.PointToClient(Cursor.Position);
                Point point2 = new Point(((Control) sender).Location.X, point.Y - this.positionoff.Y);
                this.SetCursorCoordenadas();
                int index = this.CurrentReport.Bands.IndexOf(this.CurrentBand);
                if (index == 0)
                {
                    if (point2.Y < this.TopAreaDesigner)
                    {
                        point2.Y = this.TopAreaDesigner;
                    }
                    foreach (Control control in this.CurrentBand.Controls)
                    {
                        if (point2.Y < (control.Top + control.Height))
                        {
                            point2.Y = control.Top + control.Height;
                        }
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        int num2 = this.CurrentReport.Bands.IndexOf(this.CurrentBand);
                        if (num2 > 0)
                        {
                            BandBase base2 = (BandBase) this.CurrentReport.Bands[num2 - 1];
                            if ((base2.DesignerControl != null) && (point2.Y < (base2.DesignerControl.Top + base2.DesignerControl.Height)))
                            {
                                point2.Y = base2.DesignerControl.Top + base2.DesignerControl.Height;
                            }
                        }
                    }
                    foreach (Control control in this.CurrentBand.Controls)
                    {
                        if (point2.Y < (control.Top + control.Height))
                        {
                            point2.Y = control.Top + control.Height;
                        }
                    }
                }
                ((Control) sender).Location = point2;
            }
        }

        private void BandControlMouseUp(object sender, MouseEventArgs e)
        {
        
                Cursor.Current = Cursors.Default;
                this.movingband = false;
                if (this.CurrentBand != null)
                {
                    this.CurrentBand.Height = this.CurrentBand.DesignerControl.Top - this.CurrentBand.Top;
                }
                this.ArrangeAreaDesigner();
                this.SetRulers();
                this.ArrangeBands();
                this.BandControlPaint(this, null);

                //ChangeSelectBandLocation("Location");
                //TransactionsCommit();
           
        }

        //private void BandControlPaint(object sender, PaintEventArgs e)
        public  void BandControlPaint(object sender, PaintEventArgs e)
        {
            int num = 0;
            num = this.LastTopAreaDesigner - this.PnlAreaDesigner.Top;
            if ((num != 0) && (this.CurrentReport != null))
            {
                foreach (BandBase base2 in this.CurrentReport.Bands)
                {
                    base2.Ruler.Top -= num;
                }
            }
            this.LastTopAreaDesigner = this.PnlAreaDesigner.Top;
        }

        private void BandMove(Point point)
        {
        }

        private void centimetrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetUnits(Units.Centimetro);
            this.pixelToolStripMenuItem.Checked = false;
            this.polegadasToolStripMenuItem.Checked = false;
            this.centimetrosToolStripMenuItem.Checked = true;
            this.milimetrosToolStripMenuItem.Checked = false;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void controlFormat(object sender, EventArgs e)
        {
        }

        private void ControlMouseClick(object sender, MouseEventArgs e)
        {
            this.PrepareFormatButton(sender as CustomControl);
            this.PrepareMenuContext();
        }

        private void ControlMouseDown(object sender, MouseEventArgs e)
        {
            CustomControl control = sender as CustomControl;
            if (!keyAssemble)
            {
                foreach (Control cr in this.alControl)
                {
                    //ControlBox_Select cs = cr as ControlBox_Select
                    ((CustomControl)cr).Cs.Remove();
                }
                alControl.Clear();
                this.SelectedObjectPropertyGrid(sender);
                if (e.Button == MouseButtons.Left)
                {
                    Cursor.Current = Cursors.SizeAll;
                    this.positionoff = e.Location;
                    this.movingcontrol = true;
                }
                this.SelectNodeOnTreeView((sender as CustomControl).Name);
            }
            alControl.Add(control);
          
            BackupSelectedComponents();

           
        }

        private void ControlMouseMove(object sender, MouseEventArgs e)
        {
            if (!keyAssemble)
            {
                if (this.movingcontrol)
                {
                    this.controlBox.HideHandles();
                    Point point = this.CurrentControl.Parent.PointToClient(Cursor.Position);
                    Point point2 = new Point(point.X - this.positionoff.X, point.Y - this.positionoff.Y);
                    this.SetCursorCoordenadas();
                    this.SetCursorControl(point2.Y);
                    if (point2.Y < this.TopAreaDesigner)
                    {
                        point2.Y = this.TopAreaDesigner;
                    }
                    else if (point2.Y > (this.PnlAreaDesigner.Height - 0x20))
                    {
                        point2.Y = this.PnlAreaDesigner.Height - 0x20;
                    }
                    this.CurrentControl.Location = point2;

                    
                    
                    this.CurrentControl.Invalidate();
                    this.CurrentControl.Cs.Remove();
                  
                }
            }
        }

        private void ControlMouseUp(object sender, MouseEventArgs e)
        {
            CustomControl control = (CustomControl)sender;
            if (!keyAssemble)
            {
                Cursor.Current = Cursors.Default;
                this.movingcontrol = false;
                //CustomControl control = (CustomControl)sender;
                BandBase base2 = this.FindBandPosY(control.Location.Y);
                if (base2 != null)
                {
                    control.垂直位置 = control.Location.Y - base2.Top;
                    control.水平位置 = control.Location.X;
                    if (this.CurrentBand != base2)
                    {
                        this.RemoveTreeViewControl(control);
                        control.Band = base2;
                        this.CurrentBand = base2;
                        this.ArrangeBands();
                        this.AddTreeViewControl(control);
                        
                    }
                }
              
               
            }
            else
            {
                //CustomControl control = (CustomControl)sender;
                //alControl.Add(control);

                foreach (Control cr in this.alControl)
                {
                    //ControlBox_Select cs = cr as ControlBox_Select
                    ((CustomControl)cr).Cs.ShowHandles();
                }
                this.movingcontrol = false;
                this.CurrentControl = control;
                this.SetObjectAlignButton();

                //this.controlBox.WireControl( (Control)  this.propertyGrid.SelectedObject);
                //this.controlBox.ShowHandlesDeny(this .CurrentControl);
            
            }
            //alControl.Add(control);
            control.Cs.HideHandles();
            //BackupSelectedComponents();
            ChangeSelectComponentsSizeLocation("Location");
            ChangeSelectComponentsSizeLocation("Size");
            TransactionsCommit();
        }

        private void copyClick(object sender, EventArgs e)
        {
            this.CopyControl();
        }

        private void CopyControl()
        {
            if ((this.CurrentReport != null) && (this.propertyGrid.SelectedObject != null))
            {
                this.clone = ((CustomControl) this.propertyGrid.SelectedObject).Clone();
            }
        }

        private void CreateButtonsControls()
        {
            this.toolStripButtonRDBBarCode.Click += new EventHandler(this.ToolStripButtonControlClick);
            this.toolStripButtonRDBBarCode.Tag = ControlType.ControlDBBarCode;
            //this.toolStripButtonRDBBarCode.Text = "Campo codigo de barras";
            this.toolStripButtonRBarCode.Click += new EventHandler(this.ToolStripButtonControlClick);
            this.toolStripButtonRBarCode.Tag = ControlType.ControlBarCode;
            //this.toolStripButtonRBarCode.Text = "Codigo de barras";
            this.toolStripButtonRLabel.Tag = ControlType.ControlText;
            this.toolStripButtonRLabel.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRLabel.Text = "Texto";
            this.toolStripButtonRDBLabel.Tag = ControlType.ControlDBText;
            this.toolStripButtonRDBLabel.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRDBLabel.Text = "Campo Texto";
            this.toolStripButtonRSharp.Tag = ControlType.ControlShape;
            this.toolStripButtonRSharp.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRSharp.Text = "Poligono";
            this.toolStripButtonRChart.Tag = ControlType.ControlChart;
            this.toolStripButtonRChart.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRChart.Text = "Gr\x00e1fico";
            this.toolStripButtonRCalculated.Tag = ControlType.ControlDBCalc;
            this.toolStripButtonRCalculated.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRCalculated.Text = "Campo Calculado";
            this.toolStripButtonRImage.Tag = ControlType.ControlImage;
            this.toolStripButtonRImage.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRImage.Text = "Imagem";
            this.toolStripButtonRDBImage.Tag = ControlType.ControlDBImage;
            this.toolStripButtonRDBImage.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRDBImage.Text = "Campo imagem";
            this.toolStripButtonRLine.Tag = ControlType.ControlLine;
            this.toolStripButtonRLine.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRLine.Text = "Linha";
            this.toolStripButtonRSystem.Tag = ControlType.ControlSystem;
            this.toolStripButtonRSystem.Click += new EventHandler(this.ToolStripButtonControlClick);
            //this.toolStripButtonRSystem.Text = "Variavel de Sistema";
            this.toolStripButtonCursor.Click += new EventHandler(this.toolStripButtonCursorClick);
            this.toolStripButtonCursor.Text = string.Empty;
            this.toolStripButtonNegrito.Click += new EventHandler(this.toolStripButtonBoldItalicoClick);
            //this.toolStripButtonNegrito.Text = "Negrito";
            this.toolStripButtonItalico.Click += new EventHandler(this.toolStripButtonBoldItalicoClick);
            //this.toolStripButtonItalico.Text = "Italico";
            this.toolStripButtonAlingLeft.Click += new EventHandler(this.toolstripButtonAlingClick);
            this.toolStripButtonAlingLeft.Tag = TextAlignment.Left;
            //this.toolStripButtonAlingLeft.Text = "Alinha texto a esquerda";
            this.toolStripButtonAlingCenter.Click += new EventHandler(this.toolstripButtonAlingClick);
            this.toolStripButtonAlingCenter.Tag = TextAlignment.Center;
            //this.toolStripButtonAlingCenter.Text = "Centraliza texto";
            this.toolStripButtonAlingRigth.Click += new EventHandler(this.toolstripButtonAlingClick);
            this.toolStripButtonAlingRigth.Tag = TextAlignment.Right;
            //this.toolStripButtonAlingRigth.Text = "Alinha texto a direita";
            this.toolStripSizeFont = new NumericUpDown();
            //this.toolStripSizeFont.Name = "toolStripSizeFont";
            this.toolStripSizeFont.Minimum = 1M;
            this.toolStripSizeFont.ValueChanged += new EventHandler(this.toolStripSizeFont_ValueChanged);
            this.toolStripControls.Items.Insert(1, new ToolStripControlHost(this.toolStripSizeFont));
            this.toolStripComboBoxFont.SelectedIndexChanged += new EventHandler(this.toolStripSizeFont_ValueChanged);
            this.comboDataSource.SelectedIndexChanged += new EventHandler(this.SelectDataSource);
            this.comboFieldName.SelectedIndexChanged += new EventHandler(this.SelectFieldName);
        }

        private void cutClick(object sender, EventArgs e)
        {
            this.CopyControl();
            this.DeleteControl();
        }

        private void dadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDataSource.ShowDialog(this.CurrentReport);
        }

        private void deleteClick(object sender, EventArgs e)
        {
            this.DeleteControl();
        }

        private void DeleteControl()
        {
            if ((this.CurrentReport != null) && (this.propertyGrid.SelectedObject != null))
            {
              
                
                CustomControl selectedObject = (CustomControl) this.propertyGrid.SelectedObject;
                OnComponentRemoved(selectedObject);
                selectedObject.Band = null;
                this.propertyGrid.SelectedObject = null;
                this.LblObject.Text = string.Empty;
                this.controlBox.Remove();
                if (this.PnlAreaDesigner.Controls.Contains(selectedObject))
                {
                    this.PnlAreaDesigner.Controls.Remove(selectedObject);
                }
                this.RemoveTreeViewControl(selectedObject);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawRetangle(Graphics g)
        {
        }

        private void FillComboDataSource()
        {
            ReportDataCollection dataSources = this.CurrentReport.DataSources;
            this.comboDataSource.Items.Clear();
            foreach (IReportData data in dataSources)
            {
                this.comboDataSource.Items.Add(data.DataSourceName);
            }
            this.comboDataSource.SelectedIndex = 0;
        }

        private void FillComboFielName()
        {
            ReportDataFieldInfo[] fields = this.CurrentReport.DataSources.GetSource(this.comboDataSource.Text).Fields;
            this.comboFieldName.Items.Clear();
            foreach (ReportDataFieldInfo info in fields)
            {
                this.comboFieldName.Items.Add(info.FieldName);
            }
            this.comboFieldName.SelectedIndex = 0;
        }

        private void FillTreeViewControl()
        {
            this.treeViewReport.BeginUpdate();
            this.treeViewReport.Nodes.Clear();
            foreach (BandBase base2 in this.CurrentReport.Bands)
            {
                if (!this.treeViewReport.Nodes.ContainsKey(base2.Name))
                {
                    TreeNode node = this.treeViewReport.Nodes.Add(base2.Name, base2.Name);
                    foreach (Control control in base2.Controls)
                    {
                        node.Nodes.Add(control.Name, control.Name);
                    }
                }
            }
            this.treeViewReport.EndUpdate();
        }

        private void FillTreeViewDataSource()
        {
            this.treeViewDataSource.BeginUpdate();
            this.treeViewDataSource.Nodes.Clear();
            foreach (IReportData data in this.CurrentReport.DataSources)
            {
                if (!this.treeViewDataSource.Nodes.ContainsKey(data.DataSourceName))
                {
                    TreeNode node = new TreeNode(data.DataSourceName);
                    this.treeViewDataSource.Nodes.Add(node);
                    foreach (ReportDataFieldInfo info in data.Fields)
                    {
                        node.Nodes.Add(info.FieldName, info.FieldName);
                    }
                }
            }
            this.treeViewDataSource.EndUpdate();
        }

        //private BandBase FindBandPosY(int yPosition)
        public  BandBase FindBandPosY(int yPosition)
        {
            foreach (BandBase base2 in this.CurrentReport.Bands)
            {
                if ((yPosition >= base2.Top) && (yPosition <= (base2.Top + base2.Height)))
                {
                    return base2;
                }
            }
            return null;
        }

        private FontFamily GetFontFamily(string namefamily)
        {
            foreach (FontFamily family in FontFamily.Families)
            {
                if (family.Name.Equals(namefamily))
                {
                    return family;
                }
            }
            return null;
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGroup.ShowDialog(this.CurrentReport,this );
            this.InitializeDesignerControls();
            this.ArrangeAreaDesigner();
            this.ArrangeBands();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignerReport));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCoordenadas = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusPositionX = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusPositionY = new System.Windows.Forms.ToolStripStatusLabel();
            this.TbCrlReportDesigner = new System.Windows.Forms.TabControl();
            this.TabPageView = new System.Windows.Forms.TabPage();
            this.TabPageDesigner = new System.Windows.Forms.TabPage();
            this.tabControlReport = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.PnlDesigner = new System.Windows.Forms.Panel();
            this.scrollBackGround = new System.Windows.Forms.ScrollableControl();
            this.pnlBackGroundRigth = new System.Windows.Forms.Panel();
            this.PnlBackGroundBottom = new System.Windows.Forms.Panel();
            this.PnlAreaDesigner = new System.Windows.Forms.Panel();
            this.PnlRuleVertical = new System.Windows.Forms.Panel();
            this.PnlRuleHorizontal = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PnlBrowse = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeViewReport = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeViewDataSource = new System.Windows.Forms.TreeView();
            this.browserLabel = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.PnlProperty = new System.Windows.Forms.Panel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.LblObject = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.propertyLabel = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.toolStripControls = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxFont = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonNegrito = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonItalico = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlingLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlingCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlingRigth = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCursor = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRSystem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRCalculated = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRDBBarCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRBarCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRChart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRDBImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRDBLabel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRLabel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRSharp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.comboDataSource = new System.Windows.Forms.ToolStripComboBox();
            this.comboFieldName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.viewtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAlignBottom = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlignLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlignRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlignTop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonWidthSame = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHeightSame = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatorioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tituloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sumarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cabecalhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rodapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.gruposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medidasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.milimetrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polegadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centimetrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pixelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripControls = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.TbCrlReportDesigner.SuspendLayout();
            this.TabPageDesigner.SuspendLayout();
            this.tabControlReport.SuspendLayout();
            this.PnlDesigner.SuspendLayout();
            this.scrollBackGround.SuspendLayout();
            this.PnlRuleHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.PnlBrowse.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.PnlProperty.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.toolStripControls.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCoordenadas,
            this.toolStripStatusPositionX,
            this.toolStripStatusPositionY});
            this.statusStrip1.Location = new System.Drawing.Point(0, 535);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCoordenadas
            // 
            this.toolStripStatusLabelCoordenadas.AutoSize = false;
            this.toolStripStatusLabelCoordenadas.Name = "toolStripStatusLabelCoordenadas";
            this.toolStripStatusLabelCoordenadas.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabelCoordenadas.Text = "坐标:";
            this.toolStripStatusLabelCoordenadas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusPositionX
            // 
            this.toolStripStatusPositionX.AutoSize = false;
            this.toolStripStatusPositionX.Name = "toolStripStatusPositionX";
            this.toolStripStatusPositionX.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusPositionY
            // 
            this.toolStripStatusPositionY.AutoSize = false;
            this.toolStripStatusPositionY.Name = "toolStripStatusPositionY";
            this.toolStripStatusPositionY.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TbCrlReportDesigner
            // 
            this.TbCrlReportDesigner.Controls.Add(this.TabPageView);
            this.TbCrlReportDesigner.Controls.Add(this.TabPageDesigner);
            this.TbCrlReportDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbCrlReportDesigner.Location = new System.Drawing.Point(0, 24);
            this.TbCrlReportDesigner.Name = "TbCrlReportDesigner";
            this.TbCrlReportDesigner.SelectedIndex = 0;
            this.TbCrlReportDesigner.Size = new System.Drawing.Size(1028, 511);
            this.TbCrlReportDesigner.TabIndex = 3;
            this.TbCrlReportDesigner.Selected += new System.Windows.Forms.TabControlEventHandler(this.TbCrlReportDesigner_Selected);
            // 
            // TabPageView
            // 
            this.TabPageView.Location = new System.Drawing.Point(4, 21);
            this.TabPageView.Name = "TabPageView";
            this.TabPageView.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageView.Size = new System.Drawing.Size(1020, 486);
            this.TabPageView.TabIndex = 1;
            this.TabPageView.Text = "报表预览";
            this.TabPageView.UseVisualStyleBackColor = true;
            // 
            // TabPageDesigner
            // 
            this.TabPageDesigner.Controls.Add(this.tabControlReport);
            this.TabPageDesigner.Controls.Add(this.PnlDesigner);
            this.TabPageDesigner.Controls.Add(this.splitter3);
            this.TabPageDesigner.Controls.Add(this.panel1);
            this.TabPageDesigner.Controls.Add(this.pnlControls);
            this.TabPageDesigner.Location = new System.Drawing.Point(4, 21);
            this.TabPageDesigner.Name = "TabPageDesigner";
            this.TabPageDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageDesigner.Size = new System.Drawing.Size(1020, 486);
            this.TabPageDesigner.TabIndex = 0;
            this.TabPageDesigner.Text = "报表设计";
            this.TabPageDesigner.UseVisualStyleBackColor = true;
            // 
            // tabControlReport
            // 
            this.tabControlReport.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlReport.Controls.Add(this.tabPage3);
            this.tabControlReport.Controls.Add(this.tabPage4);
            this.tabControlReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControlReport.Location = new System.Drawing.Point(3, 460);
            this.tabControlReport.Name = "tabControlReport";
            this.tabControlReport.SelectedIndex = 0;
            this.tabControlReport.Size = new System.Drawing.Size(748, 23);
            this.tabControlReport.TabIndex = 6;
            this.tabControlReport.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(740, 0);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(740, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // PnlDesigner
            // 
            this.PnlDesigner.Controls.Add(this.scrollBackGround);
            this.PnlDesigner.Controls.Add(this.PnlRuleVertical);
            this.PnlDesigner.Controls.Add(this.PnlRuleHorizontal);
            this.PnlDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlDesigner.Location = new System.Drawing.Point(3, 54);
            this.PnlDesigner.Name = "PnlDesigner";
            this.PnlDesigner.Size = new System.Drawing.Size(748, 429);
            this.PnlDesigner.TabIndex = 3;
            // 
            // scrollBackGround
            // 
            this.scrollBackGround.AutoScroll = true;
            this.scrollBackGround.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.scrollBackGround.Controls.Add(this.pnlBackGroundRigth);
            this.scrollBackGround.Controls.Add(this.PnlBackGroundBottom);
            this.scrollBackGround.Controls.Add(this.PnlAreaDesigner);
            this.scrollBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollBackGround.Location = new System.Drawing.Point(22, 22);
            this.scrollBackGround.Name = "scrollBackGround";
            this.scrollBackGround.Size = new System.Drawing.Size(726, 407);
            this.scrollBackGround.TabIndex = 3;
            this.scrollBackGround.Text = "scrollableControl1";
            this.scrollBackGround.Scroll += new System.Windows.Forms.ScrollEventHandler(this.PnlBackGround_Scroll);
            // 
            // pnlBackGroundRigth
            // 
            this.pnlBackGroundRigth.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlBackGroundRigth.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBackGroundRigth.Location = new System.Drawing.Point(769, 0);
            this.pnlBackGroundRigth.Name = "pnlBackGroundRigth";
            this.pnlBackGroundRigth.Size = new System.Drawing.Size(54, 406);
            this.pnlBackGroundRigth.TabIndex = 6;
            // 
            // PnlBackGroundBottom
            // 
            this.PnlBackGroundBottom.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.PnlBackGroundBottom.Location = new System.Drawing.Point(0, 317);
            this.PnlBackGroundBottom.Name = "PnlBackGroundBottom";
            this.PnlBackGroundBottom.Size = new System.Drawing.Size(769, 89);
            this.PnlBackGroundBottom.TabIndex = 4;
            // 
            // PnlAreaDesigner
            // 
            this.PnlAreaDesigner.BackColor = System.Drawing.SystemColors.Window;
            this.PnlAreaDesigner.Location = new System.Drawing.Point(0, 0);
            this.PnlAreaDesigner.Name = "PnlAreaDesigner";
            this.PnlAreaDesigner.Size = new System.Drawing.Size(748, 311);
            this.PnlAreaDesigner.TabIndex = 5;
            // 
            // PnlRuleVertical
            // 
            this.PnlRuleVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlRuleVertical.Location = new System.Drawing.Point(0, 22);
            this.PnlRuleVertical.Name = "PnlRuleVertical";
            this.PnlRuleVertical.Size = new System.Drawing.Size(22, 407);
            this.PnlRuleVertical.TabIndex = 2;
            // 
            // PnlRuleHorizontal
            // 
            this.PnlRuleHorizontal.Controls.Add(this.pictureBox1);
            this.PnlRuleHorizontal.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlRuleHorizontal.Location = new System.Drawing.Point(0, 0);
            this.PnlRuleHorizontal.Name = "PnlRuleHorizontal";
            this.PnlRuleHorizontal.Size = new System.Drawing.Size(748, 22);
            this.PnlRuleHorizontal.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Properties.Resources.RulerHS;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 19);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(751, 54);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(5, 429);
            this.splitter3.TabIndex = 2;
            this.splitter3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PnlBrowse);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.PnlProperty);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(756, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 429);
            this.panel1.TabIndex = 1;
            // 
            // PnlBrowse
            // 
            this.PnlBrowse.Controls.Add(this.tabControl1);
            this.PnlBrowse.Controls.Add(this.browserLabel);
            this.PnlBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlBrowse.Location = new System.Drawing.Point(0, 0);
            this.PnlBrowse.Name = "PnlBrowse";
            this.PnlBrowse.Size = new System.Drawing.Size(261, 125);
            this.PnlBrowse.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(261, 109);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeViewReport);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(253, 84);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "报表元素";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeViewReport
            // 
            this.treeViewReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewReport.Location = new System.Drawing.Point(3, 3);
            this.treeViewReport.Name = "treeViewReport";
            this.treeViewReport.Size = new System.Drawing.Size(247, 78);
            this.treeViewReport.TabIndex = 0;
            this.treeViewReport.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewReport_AfterSelect);
            this.treeViewReport.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewReport_NodeMouseClick);
            this.treeViewReport.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewReport_BeforeSelect);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.treeViewDataSource);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(253, 84);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据字段";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeViewDataSource
            // 
            this.treeViewDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDataSource.Location = new System.Drawing.Point(3, 3);
            this.treeViewDataSource.Name = "treeViewDataSource";
            this.treeViewDataSource.Size = new System.Drawing.Size(247, 78);
            this.treeViewDataSource.TabIndex = 1;
            // 
            // browserLabel
            // 
            this.browserLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.browserLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.browserLabel.Location = new System.Drawing.Point(0, 0);
            this.browserLabel.Name = "browserLabel";
            this.browserLabel.Size = new System.Drawing.Size(261, 16);
            this.browserLabel.TabIndex = 1;
            this.browserLabel.Text = "对象浏览";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 125);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(261, 5);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // PnlProperty
            // 
            this.PnlProperty.Controls.Add(this.propertyGrid);
            this.PnlProperty.Controls.Add(this.LblObject);
            this.PnlProperty.Controls.Add(this.panel3);
            this.PnlProperty.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlProperty.Location = new System.Drawing.Point(0, 130);
            this.PnlProperty.Name = "PnlProperty";
            this.PnlProperty.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.PnlProperty.Size = new System.Drawing.Size(261, 299);
            this.PnlProperty.TabIndex = 2;
            // 
            // propertyGrid
            // 
            this.propertyGrid.BackColor = System.Drawing.SystemColors.Control;
            this.propertyGrid.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(0, 44);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(261, 253);
            this.propertyGrid.TabIndex = 5;
            // 
            // LblObject
            // 
            this.LblObject.BackColor = System.Drawing.Color.LightGray;
            this.LblObject.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblObject.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblObject.Location = new System.Drawing.Point(0, 22);
            this.LblObject.Name = "LblObject";
            this.LblObject.Size = new System.Drawing.Size(261, 22);
            this.LblObject.TabIndex = 4;
            this.LblObject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.propertyLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 2);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panel3.Size = new System.Drawing.Size(261, 20);
            this.panel3.TabIndex = 3;
            // 
            // propertyLabel
            // 
            this.propertyLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.propertyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyLabel.Location = new System.Drawing.Point(0, 2);
            this.propertyLabel.Name = "propertyLabel";
            this.propertyLabel.Size = new System.Drawing.Size(261, 16);
            this.propertyLabel.TabIndex = 1;
            this.propertyLabel.Text = "属性";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.toolStripControls);
            this.pnlControls.Controls.Add(this.toolStrip1);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(3, 3);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1014, 51);
            this.pnlControls.TabIndex = 4;
            // 
            // toolStripControls
            // 
            this.toolStripControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxFont,
            this.toolStripButtonNegrito,
            this.toolStripButtonItalico,
            this.toolStripButtonAlingLeft,
            this.toolStripButtonAlingCenter,
            this.toolStripButtonAlingRigth,
            this.toolStripSplitButton1,
            this.toolStripSeparator6,
            this.toolStripButtonCursor,
            this.toolStripButtonRSystem,
            this.toolStripButtonRCalculated,
            this.toolStripButtonRDBBarCode,
            this.toolStripButtonRBarCode,
            this.toolStripButtonRChart,
            this.toolStripButtonRDBImage,
            this.toolStripButtonRImage,
            this.toolStripButtonRDBLabel,
            this.toolStripButtonRLabel,
            this.toolStripButtonRLine,
            this.toolStripButtonRSharp,
            this.toolStripSeparator7,
            this.comboDataSource,
            this.comboFieldName});
            this.toolStripControls.Location = new System.Drawing.Point(0, 25);
            this.toolStripControls.Name = "toolStripControls";
            this.toolStripControls.Size = new System.Drawing.Size(1014, 25);
            this.toolStripControls.TabIndex = 3;
            this.toolStripControls.Text = "toolStrip2";
            // 
            // toolStripComboBoxFont
            // 
            this.toolStripComboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxFont.Name = "toolStripComboBoxFont";
            this.toolStripComboBoxFont.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripButtonNegrito
            // 
            this.toolStripButtonNegrito.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNegrito.Image = global::Properties.Resources.boldhs;
            this.toolStripButtonNegrito.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNegrito.Name = "toolStripButtonNegrito";
            this.toolStripButtonNegrito.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNegrito.Text = "粗体";
            // 
            // toolStripButtonItalico
            // 
            this.toolStripButtonItalico.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonItalico.Image = global::Properties.Resources.ItalicHS;
            this.toolStripButtonItalico.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonItalico.Name = "toolStripButtonItalico";
            this.toolStripButtonItalico.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonItalico.Text = "斜体";
            // 
            // toolStripButtonAlingLeft
            // 
            this.toolStripButtonAlingLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlingLeft.Image = global::Properties.Resources.AlignTableCellMiddleLeftJust;
            this.toolStripButtonAlingLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlingLeft.Name = "toolStripButtonAlingLeft";
            this.toolStripButtonAlingLeft.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlingLeft.Text = "左齐";
            // 
            // toolStripButtonAlingCenter
            // 
            this.toolStripButtonAlingCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlingCenter.Image = global::Properties.Resources.AlignTableCellMiddleCenterHS;
            this.toolStripButtonAlingCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlingCenter.Name = "toolStripButtonAlingCenter";
            this.toolStripButtonAlingCenter.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlingCenter.Text = "聚中";
            // 
            // toolStripButtonAlingRigth
            // 
            this.toolStripButtonAlingRigth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlingRigth.Image = global::Properties.Resources.AlignTableCellMiddleRightHS;
            this.toolStripButtonAlingRigth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlingRigth.Name = "toolStripButtonAlingRigth";
            this.toolStripButtonAlingRigth.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlingRigth.Text = "右齐";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = global::Properties.Resources.Color_font;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton1.Text = "字体颜色";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCursor
            // 
            this.toolStripButtonCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCursor.Image = global::Properties.Resources.toolStripButtonCursor_Image;
            this.toolStripButtonCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCursor.Name = "toolStripButtonCursor";
            this.toolStripButtonCursor.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripButtonRSystem
            // 
            this.toolStripButtonRSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRSystem.Image = global::Properties.Resources.toolStripButtonRSystem_Image;
            this.toolStripButtonRSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRSystem.Name = "toolStripButtonRSystem";
            this.toolStripButtonRSystem.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRSystem.Text = "页码时间";
            // 
            // toolStripButtonRCalculated
            // 
            this.toolStripButtonRCalculated.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRCalculated.Image = global::Properties.Resources.toolStripButtonRCalculated_Image;
            this.toolStripButtonRCalculated.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRCalculated.Name = "toolStripButtonRCalculated";
            this.toolStripButtonRCalculated.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRCalculated.Text = "统计汇总";
            this.toolStripButtonRCalculated.Click += new System.EventHandler(this.toolStripButtonRCalculated_Click);
            // 
            // toolStripButtonRDBBarCode
            // 
            this.toolStripButtonRDBBarCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRDBBarCode.Image = global::Properties.Resources.BarCodeHS;
            this.toolStripButtonRDBBarCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRDBBarCode.Name = "toolStripButtonRDBBarCode";
            this.toolStripButtonRDBBarCode.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRDBBarCode.Text = "数据条码";
            this.toolStripButtonRDBBarCode.Visible = false;
            // 
            // toolStripButtonRBarCode
            // 
            this.toolStripButtonRBarCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRBarCode.Image = global::Properties.Resources.BarCodeHS;
            this.toolStripButtonRBarCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRBarCode.Name = "toolStripButtonRBarCode";
            this.toolStripButtonRBarCode.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRBarCode.Text = "普通条码";
            this.toolStripButtonRBarCode.Visible = false;
            // 
            // toolStripButtonRChart
            // 
            this.toolStripButtonRChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRChart.Image = global::Properties.Resources.toolStripButtonRChart_Image;
            this.toolStripButtonRChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRChart.Name = "toolStripButtonRChart";
            this.toolStripButtonRChart.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRChart.Text = "图例";
            this.toolStripButtonRChart.Visible = false;
            // 
            // toolStripButtonRDBImage
            // 
            this.toolStripButtonRDBImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRDBImage.Image = global::Properties.Resources.toolStripButtonRDBImage_Image;
            this.toolStripButtonRDBImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRDBImage.Name = "toolStripButtonRDBImage";
            this.toolStripButtonRDBImage.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRDBImage.Text = "数据图片";
            // 
            // toolStripButtonRImage
            // 
            this.toolStripButtonRImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRImage.Image = global::Properties.Resources.toolStripButtonRImage_Image;
            this.toolStripButtonRImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRImage.Name = "toolStripButtonRImage";
            this.toolStripButtonRImage.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRImage.Text = "普通图片";
            // 
            // toolStripButtonRDBLabel
            // 
            this.toolStripButtonRDBLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRDBLabel.Image = global::Properties.Resources.toolStripButtonRDBLabel_Image;
            this.toolStripButtonRDBLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRDBLabel.Name = "toolStripButtonRDBLabel";
            this.toolStripButtonRDBLabel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRDBLabel.Text = "数据字段";
            // 
            // toolStripButtonRLabel
            // 
            this.toolStripButtonRLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRLabel.Image = global::Properties.Resources.toolStripButtonRLabel_Image;
            this.toolStripButtonRLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRLabel.Name = "toolStripButtonRLabel";
            this.toolStripButtonRLabel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRLabel.Text = "普通字段";
            // 
            // toolStripButtonRLine
            // 
            this.toolStripButtonRLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRLine.Image = global::Properties.Resources.toolStripButtonRLine_Image;
            this.toolStripButtonRLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRLine.Name = "toolStripButtonRLine";
            this.toolStripButtonRLine.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRLine.Text = "画线";
            // 
            // toolStripButtonRSharp
            // 
            this.toolStripButtonRSharp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRSharp.Image = global::Properties.Resources.toolStripButtonRSharp_Image;
            this.toolStripButtonRSharp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRSharp.Name = "toolStripButtonRSharp";
            this.toolStripButtonRSharp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRSharp.Text = "矩形";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // comboDataSource
            // 
            this.comboDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDataSource.Name = "comboDataSource";
            this.comboDataSource.Size = new System.Drawing.Size(130, 25);
            // 
            // comboFieldName
            // 
            this.comboFieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFieldName.Name = "comboFieldName";
            this.comboFieldName.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.viewtoolStripButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.deleteToolStripButton,
            this.toolStripSeparator5,
            this.toolStripButtonAlignBottom,
            this.toolStripButtonAlignLeft,
            this.toolStripButtonAlignRight,
            this.toolStripButtonAlignTop,
            this.toolStripSeparator8,
            this.toolStripButtonWidthSame,
            this.toolStripButtonHeightSame,
            this.toolStripSeparator10,
            this.toolStripButtonUndo,
            this.toolStripButtonRedo,
            this.toolStripSeparator9,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1014, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "新建";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Properties.Resources.openToolStripButton_Image;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "打开";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Properties.Resources.saveToolStripButton_Image;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "保存";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = global::Properties.Resources.printToolStripButton_Image;
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "打印";
            this.printToolStripButton.Visible = false;
            // 
            // viewtoolStripButton
            // 
            this.viewtoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewtoolStripButton.Image = global::Properties.Resources.PrintPreviewHS;
            this.viewtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewtoolStripButton.Name = "viewtoolStripButton";
            this.viewtoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.viewtoolStripButton.Text = "预览";
            this.viewtoolStripButton.Click += new System.EventHandler(this.viewtoolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = global::Properties.Resources.cutToolStripButton_Image;
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "剪切";
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = global::Properties.Resources.copyToolStripButton_Image;
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "复制";
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = global::Properties.Resources.pasteToolStripButton_Image;
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "粘贴";
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Image = global::Properties.Resources.DeleteHS;
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolStripButton.Text = "删除";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAlignBottom
            // 
            this.toolStripButtonAlignBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignBottom.Image = global::Properties.Resources.AlignObjectsBottomHS;
            this.toolStripButtonAlignBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignBottom.Name = "toolStripButtonAlignBottom";
            this.toolStripButtonAlignBottom.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignBottom.Text = "底对齐";
            this.toolStripButtonAlignBottom.Click += new System.EventHandler(this.toolStripButtonAlignBottom_Click);
            // 
            // toolStripButtonAlignLeft
            // 
            this.toolStripButtonAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignLeft.Image = global::Properties.Resources.AlignObjectsLeftHS;
            this.toolStripButtonAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignLeft.Name = "toolStripButtonAlignLeft";
            this.toolStripButtonAlignLeft.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignLeft.Text = "左对齐";
            this.toolStripButtonAlignLeft.Click += new System.EventHandler(this.toolStripButtonAlignLeft_Click);
            // 
            // toolStripButtonAlignRight
            // 
            this.toolStripButtonAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignRight.Image = global::Properties.Resources.AlignObjectsRightHS;
            this.toolStripButtonAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignRight.Name = "toolStripButtonAlignRight";
            this.toolStripButtonAlignRight.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignRight.Text = "右对齐";
            this.toolStripButtonAlignRight.Click += new System.EventHandler(this.toolStripButtonAlignRight_Click);
            // 
            // toolStripButtonAlignTop
            // 
            this.toolStripButtonAlignTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAlignTop.Image = global::Properties.Resources.AlignObjectsTopHS;
            this.toolStripButtonAlignTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlignTop.Name = "toolStripButtonAlignTop";
            this.toolStripButtonAlignTop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAlignTop.Text = "顶对齐";
            this.toolStripButtonAlignTop.Click += new System.EventHandler(this.toolStripButtonAlignTop_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonWidthSame
            // 
            this.toolStripButtonWidthSame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonWidthSame.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonWidthSame.Image")));
            this.toolStripButtonWidthSame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonWidthSame.Name = "toolStripButtonWidthSame";
            this.toolStripButtonWidthSame.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonWidthSame.Text = "宽度相同";
            this.toolStripButtonWidthSame.Click += new System.EventHandler(this.toolStripButtonWidthSame_Click);
            // 
            // toolStripButtonHeightSame
            // 
            this.toolStripButtonHeightSame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHeightSame.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHeightSame.Image")));
            this.toolStripButtonHeightSame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHeightSame.Name = "toolStripButtonHeightSame";
            this.toolStripButtonHeightSame.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonHeightSame.Text = "高度相同";
            this.toolStripButtonHeightSame.Click += new System.EventHandler(this.toolStripButtonHeightSame_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndo.Image")));
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUndo.Text = "撤销";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // toolStripButtonRedo
            // 
            this.toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRedo.Image")));
            this.toolStripButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRedo.Name = "toolStripButtonRedo";
            this.toolStripButtonRedo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRedo.Text = "重做";
            this.toolStripButtonRedo.Click += new System.EventHandler(this.toolStripButtonRedo_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = global::Properties.Resources.helpToolStripButton_Image;
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "帮助";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.relatorioToolStripMenuItem,
            this.dadosToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1028, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.salveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.settingsPageToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.arquivoToolStripMenuItem.Text = "文件";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.newToolStripMenuItem.Text = "新建";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.openToolStripMenuItem.Text = "打开";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.saveToolStripMenuItem.Text = "保存";
            // 
            // salveAsToolStripMenuItem
            // 
            this.salveAsToolStripMenuItem.Name = "salveAsToolStripMenuItem";
            this.salveAsToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.salveAsToolStripMenuItem.Text = "另存";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // settingsPageToolStripMenuItem
            // 
            this.settingsPageToolStripMenuItem.Name = "settingsPageToolStripMenuItem";
            this.settingsPageToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.settingsPageToolStripMenuItem.Text = "页面设置";
            this.settingsPageToolStripMenuItem.Visible = false;
            this.settingsPageToolStripMenuItem.Click += new System.EventHandler(this.settingsPageToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.exportToolStripMenuItem.Text = "导出";
            this.exportToolStripMenuItem.Visible = false;
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.printToolStripMenuItem.Text = "打印";
            this.printToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(115, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.closeToolStripMenuItem.Text = "关闭";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // relatorioToolStripMenuItem
            // 
            this.relatorioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tituloToolStripMenuItem,
            this.sumarioToolStripMenuItem,
            this.toolStripSeparator4,
            this.cabecalhoToolStripMenuItem,
            this.rodapeToolStripMenuItem,
            this.toolStripSeparator3,
            this.gruposToolStripMenuItem,
            this.medidasToolStripMenuItem});
            this.relatorioToolStripMenuItem.Name = "relatorioToolStripMenuItem";
            this.relatorioToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.relatorioToolStripMenuItem.Text = "设计";
            // 
            // tituloToolStripMenuItem
            // 
            this.tituloToolStripMenuItem.Name = "tituloToolStripMenuItem";
            this.tituloToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.tituloToolStripMenuItem.Text = "标题区域";
            // 
            // sumarioToolStripMenuItem
            // 
            this.sumarioToolStripMenuItem.Name = "sumarioToolStripMenuItem";
            this.sumarioToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.sumarioToolStripMenuItem.Text = "汇总区域";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(115, 6);
            // 
            // cabecalhoToolStripMenuItem
            // 
            this.cabecalhoToolStripMenuItem.Name = "cabecalhoToolStripMenuItem";
            this.cabecalhoToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.cabecalhoToolStripMenuItem.Text = "页眉";
            // 
            // rodapeToolStripMenuItem
            // 
            this.rodapeToolStripMenuItem.Name = "rodapeToolStripMenuItem";
            this.rodapeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.rodapeToolStripMenuItem.Text = "页尾";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(115, 6);
            // 
            // gruposToolStripMenuItem
            // 
            this.gruposToolStripMenuItem.Name = "gruposToolStripMenuItem";
            this.gruposToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.gruposToolStripMenuItem.Text = "分组统计";
            this.gruposToolStripMenuItem.Click += new System.EventHandler(this.gruposToolStripMenuItem_Click);
            // 
            // medidasToolStripMenuItem
            // 
            this.medidasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.milimetrosToolStripMenuItem,
            this.polegadasToolStripMenuItem,
            this.centimetrosToolStripMenuItem,
            this.pixelToolStripMenuItem});
            this.medidasToolStripMenuItem.Name = "medidasToolStripMenuItem";
            this.medidasToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.medidasToolStripMenuItem.Text = "标尺设置";
            // 
            // milimetrosToolStripMenuItem
            // 
            this.milimetrosToolStripMenuItem.Name = "milimetrosToolStripMenuItem";
            this.milimetrosToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.milimetrosToolStripMenuItem.Text = "毫米";
            this.milimetrosToolStripMenuItem.Click += new System.EventHandler(this.milimetrosToolStripMenuItem_Click);
            // 
            // polegadasToolStripMenuItem
            // 
            this.polegadasToolStripMenuItem.Name = "polegadasToolStripMenuItem";
            this.polegadasToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.polegadasToolStripMenuItem.Text = "英寸";
            this.polegadasToolStripMenuItem.Click += new System.EventHandler(this.polegadasToolStripMenuItem_Click);
            // 
            // centimetrosToolStripMenuItem
            // 
            this.centimetrosToolStripMenuItem.Name = "centimetrosToolStripMenuItem";
            this.centimetrosToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.centimetrosToolStripMenuItem.Text = "分米";
            this.centimetrosToolStripMenuItem.Click += new System.EventHandler(this.centimetrosToolStripMenuItem_Click);
            // 
            // pixelToolStripMenuItem
            // 
            this.pixelToolStripMenuItem.Name = "pixelToolStripMenuItem";
            this.pixelToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.pixelToolStripMenuItem.Text = "像素";
            this.pixelToolStripMenuItem.Click += new System.EventHandler(this.pixelToolStripMenuItem_Click);
            // 
            // dadosToolStripMenuItem
            // 
            this.dadosToolStripMenuItem.Name = "dadosToolStripMenuItem";
            this.dadosToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.dadosToolStripMenuItem.Text = "数据";
            this.dadosToolStripMenuItem.Click += new System.EventHandler(this.dadosToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.aboutToolStripMenuItem.Text = "关于";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // contextMenuStripControls
            // 
            this.contextMenuStripControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStripControls.Name = "contextMenuStrip1";
            this.contextMenuStripControls.Size = new System.Drawing.Size(179, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem1.Text = "Trazer para Frente";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem2.Text = "Enviar para tras";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // DesignerReport
            // 
            this.ClientSize = new System.Drawing.Size(1028, 557);
            this.Controls.Add(this.TbCrlReportDesigner);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DesignerReport";
            this.Text = "Make Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DesignerReport_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Report_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Report_KeyUp);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignerReport_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Report_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.TbCrlReportDesigner.ResumeLayout(false);
            this.TabPageDesigner.ResumeLayout(false);
            this.tabControlReport.ResumeLayout(false);
            this.PnlDesigner.ResumeLayout(false);
            this.scrollBackGround.ResumeLayout(false);
            this.PnlRuleHorizontal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.PnlBrowse.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.PnlProperty.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.toolStripControls.ResumeLayout(false);
            this.toolStripControls.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeDesignerControls()
        {
            if (this.CurrentReport != null)
            {
                foreach (BandBase base2 in this.CurrentReport.Bands)
                {
                    if (base2.DesignerControl == null)
                    {
                        base2.DesignerControl = new BandDesigner();
                        this.PnlAreaDesigner.Controls.Add(base2.DesignerControl);
                        base2.DesignerControl.MouseMove += new MouseEventHandler(this.BandControlMouseMove);
                        base2.DesignerControl.MouseDown += new MouseEventHandler(this.BandControlMouseDown);
                        base2.DesignerControl.MouseUp += new MouseEventHandler(this.BandControlMouseUp);
                        base2.DesignerControl.Paint += new PaintEventHandler(this.BandControlPaint);
                        base2.DesignerControl.Text = base2.Name;
                        base2.DesignerControl.Location = new Point(0, base2.Height);
                        base2.DesignerControl.Tag = base2;
                        base2.DesignerControl.BringToFront();
                        if (base2.Ruler == null)
                        {
                            Ruler ruler = new Ruler(RulerOrientation.Vertical);
                            this.PnlRuleVertical.Controls.Add(ruler);
                            ruler.Top = 0;
                            ruler.Left = 0;
                            ruler.Width = 0x10;
                            ruler.Height = base2.Height;
                            base2.Ruler = ruler;
                        }
                        base2.Unit = this.CurrentReport.Unit;
                        this.InsertDesignerControl(base2);
                    }
                    base2.DesignerControl.Size = new Size(this.PnlAreaDesigner.Size.Width, base2.DesignerControl.Height);
                }
                if (this.RulerHorizontal == null)
                {
                    this.RulerHorizontal = new Ruler(RulerOrientation.Horizontal);
                    this.PnlRuleHorizontal.Controls.Add(this.RulerHorizontal);
                    this.RulerHorizontal.Top = 0;
                }
                this.RulerHorizontal.Left = this.PnlRuleVertical.Width;
                this.RulerHorizontal.Width = this.PnlAreaDesigner.Width + 50;
                this.RulerHorizontal.Height = 0x12;
                this.RulerHorizontal.Escala = this.CurrentReport.Unit;
            }
        }

        protected void InitializeDesignerPage()
        {
            if (this.CurrentReport.PageSetting.LandScape)
            {
                this.PnlAreaDesigner.Width = this.CurrentReport.PageSetting.AreaPrintedHeigth;
                this.PnlAreaDesigner.Height = this.CurrentReport.PageSetting.AreaPrintedWidth;
            }
            else
            {
                this.PnlAreaDesigner.Width = this.CurrentReport.PageSetting.AreaPrintedWidth;
                this.PnlAreaDesigner.Height = this.CurrentReport.PageSetting.AreaPrintedHeigth;
            }
        }

        private void InitializeFonts()
        {
            this.toolStripComboBoxFont.Items.Clear();
            foreach (FontFamily family in FontFamily.Families)
            {
                this.toolStripComboBoxFont.Items.Add(family.Name);
            }
        }

        protected void InitializeReportDesigner()
        {
            this.InitializeDesignerPage();
            this.InitializeDesignerControls();
            this.ArrangeAreaDesigner();
            this.ArrangeBands();
            this.FillTreeViewControl();
            this.FillTreeViewDataSource();
            this.FillComboDataSource();
            this.SincronizeMenuWithReport();
        }

        protected void InitizalizeEventsTool()
        {
            this.salveAsToolStripMenuItem.Click += new EventHandler(this.saveAsReportClick);
            this.saveToolStripMenuItem.Click += new EventHandler(this.saveReportClick);
            this.saveToolStripButton.Click += new EventHandler(this.saveReportClick);
           // this.saveToolStripButton.Text = "Salvar";
            this.newToolStripButton.Click += new EventHandler(this.newReportClick);
            //this.newToolStripButton.Text = "Novo";
            this.newToolStripMenuItem.Click += new EventHandler(this.newReportClick);
            this.openToolStripButton.Click += new EventHandler(this.openReportClick);
           // this.openToolStripButton.Text = "Abrir";
            this.openToolStripMenuItem.Click += new EventHandler(this.openReportClick);
            this.cutToolStripButton.Click += new EventHandler(this.cutClick);
            //this.cutToolStripButton.Text = "Recortar";
            this.copyToolStripButton.Click += new EventHandler(this.copyClick);
            //this.copyToolStripButton.Text = "Copiar";
            this.pasteToolStripButton.Click += new EventHandler(this.pasteClick);
           // this.pasteToolStripButton.Text = "Colar";
            this.deleteToolStripButton.Click += new EventHandler(this.deleteClick);
            //this.deleteToolStripButton.Text = "Excluir";
            this.tituloToolStripMenuItem.Click += new EventHandler(this.tituloToolStripMenuItem_Click);
            this.tituloToolStripMenuItem.Tag = BandType.BandTitle;
            this.cabecalhoToolStripMenuItem.Click += new EventHandler(this.tituloToolStripMenuItem_Click);
            this.cabecalhoToolStripMenuItem.Tag = BandType.BandHeader;
            this.rodapeToolStripMenuItem.Click += new EventHandler(this.tituloToolStripMenuItem_Click);
            this.rodapeToolStripMenuItem.Tag = BandType.BandFooder;
            this.sumarioToolStripMenuItem.Click += new EventHandler(this.tituloToolStripMenuItem_Click);
            this.sumarioToolStripMenuItem.Tag = BandType.BandSummary;
            this.PnlAreaDesigner.MouseMove += new MouseEventHandler(this.PnlAreaDesigner_MouseMove);
            this.PnlAreaDesigner.MouseClick += new MouseEventHandler(this.PnlAreaDesigner_MouseClick);
            this.PnlAreaDesigner.MouseDown += new MouseEventHandler(this.PnlAreaDesigner_MouseDown);
            this.PnlAreaDesigner.MouseUp += new MouseEventHandler(this.PnlAreaDesigner_MouseUp);
            this.PnlAreaDesigner.Paint += new PaintEventHandler(this.BandControlPaint);
            this.PnlAreaDesigner.Resize += new EventHandler(this.PnlAreaDesigner_Resize);

           
        }

        private void InsertDesignerControl(BandBase band)
        {
            band.SortControls();
            foreach (Control control in band.Controls)
            {
                if (!this.PnlAreaDesigner.Controls.Contains(control))
                {
                    this.PnlAreaDesigner.Controls.Add(control);
                    this.propertyGrid.SelectedObject = control;
                    control.MouseDown += new MouseEventHandler(this.ControlMouseDown);
                    control.MouseUp += new MouseEventHandler(this.ControlMouseUp);
                    control.MouseMove += new MouseEventHandler(this.ControlMouseMove);
                    control.MouseClick += new MouseEventHandler(this.ControlMouseClick);
                    control.ContextMenuStrip = this.contextMenuStripControls;
                    this.controlBox.WireControl(control);
                }
                control.BringToFront();
                control.Location = new Point(control.Location.X, band.Top - band.Height);
            }
        }

        private void milimetrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetUnits(Units.Milimetro);
            this.pixelToolStripMenuItem.Checked = false;
            this.polegadasToolStripMenuItem.Checked = false;
            this.centimetrosToolStripMenuItem.Checked = false;
            this.milimetrosToolStripMenuItem.Checked = true;
        }

        private void newReportClick(object sender, EventArgs e)
        {
            Report report = new Report();
            foreach (IReportData data in this.CurrentReport.DataSources)
            {
                report.DataSources.Add(data);
            }
            this.CurrentReport = report;
            this.CurrentReport.CreateBandDefauts();
            this.ResetReportDesigner();
            this.InitializeReportDesigner();
            this.Text = "Make Report - Sem Titulo";
        }

        private void openReportClick(object sender, EventArgs e)
        {
            if (this.CurrentReport != null)
            {
                OpenFileDialog dialog = new OpenFileDialog {
                    Filter = "Todos Arquivos(*.*)|*.*| Report(*.mrpt)|*.mrpt",
                    FilterIndex = 1
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.CurrentReport.FileName = dialog.FileName;
                    this.CurrentReport.Load();
                    this.Text = "Make Report - " + this.CurrentReport.FileName;
                    this.ResetReportDesigner();
                    this.InitializeReportDesigner();
                }
            }
        }

        private void pasteClick(object sender, EventArgs e)
        {
            if ((this.CurrentReport != null) && (this.clone != null))
            {
                ((CustomControl) this.clone).Band = this.CurrentBand;
                this.CurrentReport.SetUniqueName(this.clone as CustomControl);
                this.InsertDesignerControl(this.CurrentBand);
                this.ArrangeBands();
                this.AddTreeViewControl(this.clone as CustomControl);

                OnComponentAdded(this.clone as CustomControl);
            }
        }

        private void pixelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetUnits(Units.Pixel);
            this.pixelToolStripMenuItem.Checked = true;
            this.polegadasToolStripMenuItem.Checked = false;
            this.centimetrosToolStripMenuItem.Checked = false;
            this.milimetrosToolStripMenuItem.Checked = false;
        }

        private void PnlAreaDesigner_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.controlType != null)
            {
                CustomControl control = CustomControl.FactoryControl((ControlType) this.controlType);
                this.CurrentReport.SetUniqueName(control);
                control.Band = this.FindBandPosY(e.Y);
                control.Location = new Point(e.X, e.Y);

               

                control.垂直位置  = control.Location.Y - control.Band.Top;
                this.InsertDesignerControl(control.Band);
                this.ArrangeBands();
                this.UnCheckButtons();
                this.FillTreeViewControl();
                this.SelectNodeOnTreeView(control.Name);
                this.PrepareFormatButton(control);

                OnComponentAdded(control);

                return;
            }
            //this.controlBox.Remove();
            //this.CurrentControl = null;
           // this.propertyGrid.SelectedObject = null;

            //if ( null != this.temcontrolBox)
            //    this.temcontrolBox.Remove();

            foreach (Control cr in PnlAreaDesigner.Controls)
            {
                if ((cr is CustomControl))
                    ((CustomControl)cr).Cs.Remove();

            }

            foreach (Control cr in this.alControl)
            {
                //ControlBox_Select cs = cr as ControlBox_Select
                ((CustomControl)cr).Cs.Remove();
            }

            alControl.Clear();
            //this.CurrentControl.Cs.Remove ();
            //controlBox.ShowHandlesDeny(this .CurrentControl);
            ////alcontrolBox.Clear();
           this . SetObjectAlignButton();

        }

        private void PnlAreaDesigner_MouseDown(object sender, MouseEventArgs e)
        {
            //foreach (Control cr in this.alControl)
            //{
            //    //ControlBox_Select cs = cr as ControlBox_Select
            //    ((CustomControl)cr).Cs.Remove();
            //}
            //alControl.Clear();
            //alcontrolBox.Clear();
          
            
            Last_MouseMoveX = e.X;
            Last_MouseMoveY = e.Y;
        }

        private void PnlAreaDesigner_MouseMove(object sender, MouseEventArgs e)
        {
            this.SetCursorCoordenadas();

            // By Deny
            int ex = e.X;
            int ey = e.Y;

            if (ex < 0) ex = 0;
            if (ey < 0) ey = 0;
            if (ex > PnlAreaDesigner.Width) ex = PnlAreaDesigner.Width;
            if (ey > PnlAreaDesigner.Height) ey = PnlAreaDesigner.Height;

            if (e.Button == MouseButtons.Left)
            {
                //if (MouseArrow == "None")
                //{
                    int x = Last_MouseMoveX;
                    int y = Last_MouseMoveY;

                    int width = ex - Last_MouseMoveX;
                    int height = ey - Last_MouseMoveY;

                    if (width < 0)
                    {
                        x = ex;
                        width = -width;
                    }

                    if (height < 0)
                    {
                        y = ey;
                        height = -height;
                    }

                    //边界限制
                    if (x < 0) x = 0;
                    if (y < 0) y = 0;
                    if (x + width > PnlAreaDesigner.Size.Width) x = PnlAreaDesigner.Size.Width - width;
                    if (y + height > PnlAreaDesigner.Size.Height) y = PnlAreaDesigner.Size.Height - height;
                    DrawRectangle(Rec_Old.X, Rec_Old.Y, Rec_Old.Width, Rec_Old.Height, FrameStyle.Dashed);

                    Rec_Old.X = x;
                    Rec_Old.Y = y;
                    Rec_Old.Width = width;
                    Rec_Old.Height = height;

                    DrawRectangle(x, y, width, height, FrameStyle.Dashed);

                }
                //else
                //{
                //   // ConditionStretchControl(MouseArrow, ex - Last_MouseMoveX, ey - Last_MouseMoveY);
                //}
            //}
            else
            {
                //MouseArrow = ChangeArrow(ex, ey);
            }
            //
        }

        /// <summary>
        /// 在Panel上划出矩形匡
        /// </summary>
        /// <param name="x">矩形框的起点横作标</param>
        /// <param name="y">矩形框的起点纵作标</param>
        /// <param name="width">矩形框的宽度</param>
        /// <param name="height">矩形框的高度</param>
        /// <param name="style">矩形线的风格</param>
        private void DrawRectangle(int x, int y, int width, int height, FrameStyle style)
        {
            //从相对Panel的鼠标位置转化到相对屏幕的鼠标位置
            x = this.Location.X + this.TbCrlReportDesigner.Location.X + this.PnlRuleVertical.Width   + this.PnlDesigner.Location.X + PnlAreaDesigner.Location.X + x + 8;
            y = this.Location.Y + this.TbCrlReportDesigner.Location.Y + this.pnlControls.Height  + this.PnlRuleHorizontal.Height + this.PnlDesigner.Location.Y + PnlAreaDesigner.Location.Y + y - 8;

            ControlPaint.DrawReversibleFrame(new Rectangle(x, y, width, height), Color.Black, style);

        }

        private void PnlAreaDesigner_MouseUp(object sender, MouseEventArgs e)
        {

            int x = Last_MouseMoveX;
            int y = Last_MouseMoveY;

            if (e.X - Last_MouseMoveX < 0)
            {
                x = e.X;
            }

            if (e.Y - Last_MouseMoveY < 0)
            {
                y = e.Y;
                //				height = - height;
            }

            if (x < 6) x = 6;
            if (y < 6) y = 6;

            int width = Math.Abs(e.X - Last_MouseMoveX);
            int height = Math.Abs(e.Y - Last_MouseMoveY);

            if (x + width + 6 > PnlAreaDesigner.Size.Width) width = PnlAreaDesigner.Size.Width - x - 6;
            if (y + height + 6 > PnlAreaDesigner.Size.Height) height = PnlAreaDesigner.Size.Height - y - 6;

            DrawRectangle(Rec_Old.X, Rec_Old.Y, Rec_Old.Width, Rec_Old.Height, FrameStyle.Dashed);

            Rec_Old.Width = 0;
            Rec_Old.Height = 0;

            ControlContainer(x, y, width, height);

            foreach (Control cr in this.alControl)
            {
                //ControlBox_Select cs = cr as ControlBox_Select
                ((CustomControl)cr).Cs  .ShowHandles();
            }
            this.CurrentControl.Cs.HideHandles();
            //this.controlBox.ShowHandlesDeny(this.CurrentControl);
            this.SetObjectAlignButton();
        }

        private void PnlAreaDesigner_Resize(object sender, EventArgs e)
        {
            this.scrollBackGround.VerticalScroll.Maximum = this.PnlAreaDesigner.Height + 50;
            this.PnlBackGroundBottom.Top = this.PnlAreaDesigner.Height;
            this.PnlBackGroundBottom.Width = this.PnlAreaDesigner.Width;
            this.pnlBackGroundRigth.Top = this.PnlAreaDesigner.Top;
            this.pnlBackGroundRigth.Left = this.PnlAreaDesigner.Width;
        }

        private void PnlBackGround_Scroll(object sender, ScrollEventArgs e)
        {
            this.BandControlPaint(this, null);
        }

        private void polegadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetUnits(Units.Inch);
            this.pixelToolStripMenuItem.Checked = false;
            this.polegadasToolStripMenuItem.Checked = true;
            this.centimetrosToolStripMenuItem.Checked = false;
            this.milimetrosToolStripMenuItem.Checked = false;
        }

        private void PrepareFormatButton(CustomControl control)
        {
            this.toolStripSizeFont.ValueChanged -= new EventHandler(this.toolStripSizeFont_ValueChanged);
            this.toolStripSizeFont.Value = (int) control.Font.Size;
            this.toolStripSizeFont.ValueChanged += new EventHandler(this.toolStripSizeFont_ValueChanged);
            this.toolStripComboBoxFont.SelectedIndexChanged -= new EventHandler(this.toolStripSizeFont_ValueChanged);
            this.SelectItemComboFontFamily(control.Font.FontFamily);
            this.toolStripComboBoxFont.SelectedIndexChanged += new EventHandler(this.toolStripSizeFont_ValueChanged);
            this.toolStripButtonItalico.Checked = control.Font.Italic;
            this.toolStripButtonNegrito.Checked = control.Font.Bold;
            if (this.CurrentControl is IRDBControl)
            {
                this.SelectItemComboDataSource();
            }
        }

        private void PrepareMenuContext()
        {
            this.contextMenuStripControls.Items.Clear();
            ToolStripMenuItem item = new ToolStripMenuItem("底层显示");
            item.Click += new EventHandler(this.toolStripMenuItemSendToBackClick);
            this.contextMenuStripControls.Items.Add(item);
            item = new ToolStripMenuItem("顶层显示");
            item.Click += new EventHandler(this.toolStripMenuItemBringToFrontClick);
            this.contextMenuStripControls.Items.Add(item);
            if ((((this.CurrentControl.Type == ControlType.ControlDBCalc) || (this.CurrentControl.Type == ControlType.ControlDBText)) || (this.CurrentControl.Type == ControlType.ControlSystem)) || (this.CurrentControl.Type == ControlType.ControlText))
            {
                this.contextMenuStripControls.Items.Add(new ToolStripSeparator());
                item = new ToolStripMenuItem("显示格式化串");
                item.Click += new EventHandler(this.ShowFormatDialog);
                this.contextMenuStripControls.Items.Add(item);
            }
            if (this.CurrentControl.Type == ControlType.ControlText)
            {
                this.contextMenuStripControls.Items.Add(new ToolStripSeparator());
                item = new ToolStripMenuItem("编辑文本");
                item.Click += new EventHandler(this.ShowEditDialog);
                this.contextMenuStripControls.Items.Add(item);
            }
            if (this.CurrentControl.Type == ControlType.ControlImage)
            {
                this.contextMenuStripControls.Items.Add(new ToolStripSeparator());
                item = new ToolStripMenuItem("选择图片...");
                item.Click += new EventHandler(this.ShowDialogFigura);
                this.contextMenuStripControls.Items.Add(item);
            }
        }

        protected void RefreshWorkArea()
        {
            this.InitializeDesignerControls();
            this.ArrangeAreaDesigner();
            this.ArrangeBands();
            this.SetRulers();
            this.FillTreeViewControl();
        }

        public  bool RemoveBand(BandType bandtype)
        {
            BandBase band = this.CurrentReport.GetBand(bandtype);
            if (band != null)
            {
                if ((band.Controls.Count > 0) && (MessageBox.Show("确定删除当前区域吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel))
                {
                    return false;
                }
                this.PnlAreaDesigner.Controls.Remove(band.DesignerControl);
                this.PnlRuleVertical.Controls.Remove(band.Ruler);
                this.CurrentReport.RemoveBand(band.BandType);
                this.RefreshWorkArea();
            }
            return true;
        }

        public bool RemoveBand(BandGroup band)
        {
           
            if (band != null)
            {
                if ((band.BandFooder.Controls.Count > 0) &&(band.BandHeader.Controls.Count > 0) && (MessageBox.Show("确定删除当前区域吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel))
                {
                    return false;
                }
                this.PnlAreaDesigner.Controls.Remove(band.BandHeader.DesignerControl);
                this.PnlRuleVertical.Controls.Remove(band.BandHeader.Ruler);
                this.PnlAreaDesigner.Controls.Remove(band.BandFooder.DesignerControl);
                this.PnlRuleVertical.Controls.Remove(band.BandFooder.Ruler);
                band.BandHeader.Controls.Clear();
                band.BandFooder.Controls.Clear();
                this.CurrentReport.RemoveBand(band.BandHeader.BandType);
                this.CurrentReport.Bandcollection .Remove (band.BandHeader);
                this.CurrentReport.RemoveBand(band.BandFooder.BandType);
                this.CurrentReport.Bandcollection.Remove(band.BandFooder );
                this.CurrentReport.Bandcollection.Remove(band);
                
                band.Controls.Clear();
                this.CurrentReport.RemoveBand(band.BandType);
                this.CurrentReport.Bandgroupcolletion.Remove(band.BandHeader);
                this.CurrentReport.Bandgroupcolletion.Remove(band.BandFooder);
                this.CurrentReport.Bandgroupcolletion.Remove(band);
                this.RefreshWorkArea();
            }
            return true;
        }

        private void RemoveTreeViewControl(CustomControl control)
        {
            TreeNode[] nodeArray = this.treeViewReport.Nodes.Find(control.Name, true);
            if (nodeArray.Length > 0)
            {
                TreeNode node = nodeArray[0];
                this.treeViewReport.BeginUpdate();
                node.Remove();
                this.treeViewReport.EndUpdate();
                this.treeViewReport.Refresh();
            }
        }

        private void ResetReportDesigner()
        {
            this.LblObject.Text = string.Empty;
            this.propertyGrid.SelectedObject = null;
            this.controlBox.Remove();
            this.PnlAreaDesigner.Controls.Clear();
            this.PnlRuleVertical.Controls.Clear();
        }

        private void saveAsReportClick(object sender, EventArgs e)
        {
            if (this.CurrentReport != null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.CurrentReport.FileName = dialog.FileName;
                    this.CurrentReport.Save();
                    this.Text = "Make Report - " + this.CurrentReport.FileName;
                }
            }
        }

        private void saveReportClick(object sender, EventArgs e)
        {
            if (this.CurrentReport != null)
            {
                if (!string.IsNullOrEmpty(this.CurrentReport.FileName))
                {
                    this.CurrentReport.Save();
                }
                else
                {
                    this.saveAsReportClick(sender, e);
                }
            }
        }

        private void SelectDataSource(object sender, EventArgs e)
        {
            this.comboFieldName.SelectedIndexChanged -= new EventHandler(this.SelectFieldName);
            this.FillComboFielName();
            this.comboFieldName.SelectedIndexChanged += new EventHandler(this.SelectFieldName);
        }

        private void SelectedObjectPropertyGrid(object sender)
        {
            this.LblObject.Text = string.Empty;
            if (sender != null)
            {
                this.LblObject.Text = ((CustomControl) sender).Name + ":" + ((CustomControl) sender).GetType().Name;
                this.propertyGrid.SelectedObject = sender;
                this.propertyGrid.Refresh();
                this.CurrentControl = (CustomControl) sender;
                this.CurrentBand = this.CurrentControl.Band;
                this.SetTextFormatButton();
                this.SetObjectAlignButton();
                this.PrepareFormatButton(this.CurrentControl);
            }
            else
            {
                this.propertyGrid.SelectedObject = null;
            }
        }

        private void SelectFieldName(object sender, EventArgs e)
        {
            (this.CurrentControl as IRDBControl).数据源 = this.comboDataSource.Text;
            (this.CurrentControl as IRDBControl).数据字段 = this.comboFieldName.Text;
        }

        private void SelectItemComboDataSource()
        {
            int num;
            for (num = 0; num < this.comboDataSource.Items.Count; num++)
            {
                if (this.comboDataSource.Items[num].ToString().Equals((this.CurrentControl as IRDBControl).数据源))
                {
                    this.comboDataSource.SelectedIndex = num;
                    break;
                }
            }
            for (num = 0; num < this.comboFieldName.Items.Count; num++)
            {
                if (this.comboFieldName.Items[num].ToString().Equals((this.CurrentControl as IRDBControl).数据字段))
                {
                    this.comboFieldName.SelectedIndex = num;
                    break;
                }
            }
        }

        private void SelectItemComboFontFamily(FontFamily fontfamily)
        {
            for (int i = 0; i < this.toolStripComboBoxFont.Items.Count; i++)
            {
                if (this.toolStripComboBoxFont.Items[i].ToString().Equals(fontfamily.Name))
                {
                    this.toolStripComboBoxFont.SelectedIndex = i;
                    break;
                }
            }
        }

        private void SelectNodeOnTreeView(string controlName)
        {
            TreeNode[] nodeArray = this.treeViewReport.Nodes.Find(controlName, true);
            this.treeViewReport.FullRowSelect = true;
            this.treeViewReport.Focus();
            if (nodeArray.Length > 0)
            {
                this.treeViewReport.SelectedNode = nodeArray[0];
            }
        }

        private void SetCursorControl(int yPosition)
        {
            if ((yPosition < this.TopAreaDesigner) || (yPosition > (this.PnlAreaDesigner.Height - 0x20)))
            {
                Cursor.Current = Cursors.No;
            }
            else
            {
                Cursor.Current = Cursors.SizeAll;
            }
        }

        private void SetCursorCoordenadas()
        {
            Point point = this.PnlAreaDesigner.PointToClient(Cursor.Position);
            this.toolStripStatusPositionX.Text = "X=" + point.X.ToString();
            this.toolStripStatusPositionY.Text = "Y=" + point.Y.ToString();
        }

        private void SetFontControl()
        {
            float emSize = (int) this.toolStripSizeFont.Value;
            string text = this.toolStripComboBoxFont.Text;
            Font font = new Font(text, emSize, FontStyle.Regular);
            if (this.toolStripButtonNegrito.Checked)
            {
                font = new Font(text, emSize, FontStyle.Bold);
            }
            if (this.toolStripButtonItalico.Checked)
            {
                font = new Font(text, emSize, FontStyle.Italic);
            }
            this.CurrentControl.Font = font;
        }

        private void SetObjectAlignButton()
        {
            this.toolStripButtonAlignBottom.Enabled = this.alControl.Count > 1;
            this.toolStripButtonAlignLeft.Enabled = this.alControl.Count > 1;
            this.toolStripButtonAlignRight.Enabled = this.alControl.Count > 1;
            this.toolStripButtonAlignTop.Enabled = this.alControl.Count > 1;
            this.toolStripButtonHeightSame.Enabled = this.alControl.Count > 1;
            this.toolStripButtonWidthSame.Enabled = this.alControl.Count > 1;

            this.deleteToolStripButton.Enabled = this.CurrentControl != null;
        }

        //private void SetRulers()
        public  void SetRulers()
        {
            foreach (BandBase base2 in this.CurrentReport.Bands)
            {
                if (base2.Ruler != null)
                {
                }
            }
            if (this.RulerHorizontal != null)
            {
                this.RulerHorizontal.Top = 0;
                this.RulerHorizontal.Left = this.PnlRuleVertical.Width;
                this.RulerHorizontal.Width = this.PnlAreaDesigner.Width + 50;
                this.RulerHorizontal.Height = 0x12;
            }
        }

        private void SetTextFormatButton()
        {
            if (((this.CurrentControl is RLabel) || (this.CurrentControl is RDBLabel)) || (this.CurrentControl is RDBCalculated))
            {
                this.toolStripButtonItalico.Enabled = true;
                this.toolStripButtonNegrito.Enabled = true;
                this.toolStripComboBoxFont.Enabled = true;
                this.toolStripSizeFont.Enabled = true;
                this.toolStripButtonAlingLeft.Enabled = true;
                this.toolStripButtonAlingCenter.Enabled = true;
                this.toolStripButtonAlingRigth.Enabled = true;
            }
            else
            {
                this.toolStripButtonItalico.Enabled = false;
                this.toolStripButtonNegrito.Enabled = false;
                this.toolStripComboBoxFont.Enabled = false;
                this.toolStripSizeFont.Enabled = false;
                this.toolStripButtonAlingLeft.Enabled = false;
                this.toolStripButtonAlingCenter.Enabled = false;
                this.toolStripButtonAlingRigth.Enabled = false;
            }
            this.comboDataSource.Enabled = this.CurrentControl is IRDBControl;
            this.comboFieldName.Enabled = this.CurrentControl is IRDBControl;
        }

        private void settingsPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPageSetting.ShowDialog(this.CurrentReport.PageSetting);
            this.InitializeDesignerPage();
            this.InitializeDesignerControls();
            this.ArrangeAreaDesigner();
            this.ArrangeBands();
        }

        private void SetUnits(Units unit)
        {
            if (this.CurrentReport != null)
            {
                this.CurrentReport.Unit = unit;
            }
            this.RulerHorizontal.Escala = unit;
        }

        public static bool HaveDesignRight;
        public static DialogResult ShowDesigner(Report report, bool haveDesignright)
        {
            if (designer == null)
            {
                designer = new DesignerReport(report);
            }
            else
            {
                designer.Dispose();
                designer = new DesignerReport(report);
            }

            DesignerReport.HaveDesignRight = haveDesignright;
            return designer.ShowDialog();
        }

        private void ShowDialogFigura(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Todos Arquivos(*.*)|*.*|Imagens(*.bmp;*.gif;*.jpg;*.ico;*.emf;,*.wmf)|*.bmp;*.gif;*.jpg;*.ico;*.emf;*.wmf",
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ((RImage) this.CurrentControl).文件 = dialog.FileName;
            }
        }

        private void ShowEditDialog(object sender, EventArgs e)
        {
            FormEditMemo.ShowDialog(this.CurrentControl as RLabel);
        }

        private void ShowFormatDialog(object sender, EventArgs e)
        {
            FormFormat.ShowDialog(sender as CustomControl);
        }

        private void SincronizeMenuWithReport()
        {
            this.tituloToolStripMenuItem.Checked = this.CurrentReport.BandTitle != null;
            this.sumarioToolStripMenuItem.Checked = this.CurrentReport.BandSummary != null;
            this.rodapeToolStripMenuItem.Checked = this.CurrentReport.BandFooder != null;
            this.cabecalhoToolStripMenuItem.Checked = this.CurrentReport.BandHeader != null;
        }

        private void TbCrlReportDesigner_Selected(object sender, TabControlEventArgs e)
        {
            if (this.TbCrlReportDesigner.SelectedTab == this.TabPageView)
            {
                if (this.controlViewer == null)
                {
                    this.controlViewer = new ControlViewer();
                }
                this.controlViewer.Dock = DockStyle.Fill;
                this.TabPageView.Controls.Add(this.controlViewer);
                this.controlViewer.SetNoprint();
                if (this.CurrentReport != null)
                {
                    this.controlViewer.ShowReport(this.CurrentReport);
                }
            }
        }

        private void tituloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BandType tag = (BandType) (sender as ToolStripMenuItem).Tag;
            if ((sender as ToolStripMenuItem).Checked)
            {
                if (this.RemoveBand(tag))
                {
                    (sender as ToolStripMenuItem).Checked = false;
                }
            }
            else
            {
                this.AddBand(tag);
                (sender as ToolStripMenuItem).Checked = true;
            }
        }

        private void toolstripButtonAlingClick(object sender, EventArgs e)
        {
            if (this.CurrentControl is RLabel)
            {
                (this.CurrentControl as RLabel).对齐方式  = (TextAlignment) (sender as ToolStripButton).Tag;
            }
            if (this.CurrentControl is RDBLabel)
            {
                (this.CurrentControl as RDBLabel).对齐方式 = (TextAlignment)(sender as ToolStripButton).Tag;
            }
        }

        private void toolStripButtonBoldItalicoClick(object sender, EventArgs e)
        {
            (sender as ToolStripButton).Checked = !(sender as ToolStripButton).Checked;
            this.SetFontControl();
        }

        private void ToolStripButtonControlClick(object sender, EventArgs e)
        {
            this.UnCheckButtons();
            if (sender is ToolStripButton)
            {
                (sender as ToolStripButton).Checked = true;
                this.controlType = ((ToolStripButton) sender).Tag;
            }
            if (sender is ToolStripMenuItem)
            {
                (sender as ToolStripMenuItem).Checked = true;
                this.controlType = ((ToolStripMenuItem) sender).Tag;
            }
        }

        private void toolStripButtonCursorClick(object sender, EventArgs e)
        {
            this.UnCheckButtons();
        }

        private void toolStripMenuItemBringToFrontClick(object sender, EventArgs e)
        {
            ((Control) this.propertyGrid.SelectedObject).BringToFront();
            ((CustomControl) this.propertyGrid.SelectedObject).Index = 1;
        }

        private void toolStripMenuItemSendToBackClick(object sender, EventArgs e)
        {
            ((Control) this.propertyGrid.SelectedObject).SendToBack();
            ((CustomControl) this.propertyGrid.SelectedObject).Index = 0;
        }

        private void toolStripSizeFont_ValueChanged(object sender, EventArgs e)
        {
            this.SetFontControl();
        }

        private void treeViewReport_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!keyAssemble)
            {
                if (e.Node.Nodes.Count.Equals(0))
                {
                    Control control = this.CurrentReport.FindControlByName(e.Node.Text);
                    this.controlBox.SelectControl(control, null);
                    this.SelectedObjectPropertyGrid(control);
                }
            }
        }

        private void treeViewReport_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!keyAssemble)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    e.Cancel = true;
                }
            }
        }

        private void treeViewReport_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!keyAssemble)
            {
                if (e.Node.Nodes.Count.Equals(0))
                {
                    this.controlBox.SelectControl(this.CurrentReport.FindControlByName(e.Node.Text), null);
                }
            }
        }

        private void UnCheckButtons()
        {
            foreach (ToolStripItem item in this.toolStripControls.Items)
            {
                if (item is ToolStripButton)
                {
                    ((ToolStripButton) item).Checked = false;
                }
            }
            this.CurrentControl = null;
            this.controlType = null;
        }

        private void viewtoolStripButton_Click(object sender, EventArgs e)
        {
            this.CurrentReport.Show();
        }

        private void toolStripButtonRCalculated_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonAlignBottom_Click(object sender, EventArgs e)
        {
            foreach (Control cr in this.alControl)
            {
                ////ControlBox_Select cs = cr as ControlBox_Select
                //((CustomControl)cr).Cs.ShowHandles();
                //((CustomControl)cr).宽度 = this.CurrentControl.宽度;
                //((CustomControl)cr).Invalidate();
                //((CustomControl)cr).Location = new Point(this.CurrentControl.水平位置, ((CustomControl)cr).垂直位置);
                BackupSelectedComponents();
                
                BandBase base1 = this.FindBandPosY(cr.Location.Y);

                ((CustomControl)cr).Location = new Point(((CustomControl)cr).Location.X, this.CurrentControl.Height + this.CurrentControl.Location.Y - ((CustomControl)cr).Height );
              
                BandBase base2 = this.FindBandPosY(cr.Location.Y);
                if (base2 != null)
                {
                   
                    if (base1 != base2)
                    {
                        this.RemoveTreeViewControl(((CustomControl)cr));
                        ((CustomControl)cr).Band = base2;
                        this.AddTreeViewControl(((CustomControl)cr));
                    }
                    ((CustomControl)cr).垂直位置 = ((CustomControl)cr).Location.Y - base2.Top;
                    ((CustomControl)cr).水平位置 = ((CustomControl)cr).Location.X;
                }
                ((CustomControl)cr).Cs.MoveHandles();
                ChangeSelectComponentsSizeLocation("Location");
                //ChangeSelectComponentsSizeLocation("Size");
                TransactionsCommit();
            }
            this.ArrangeBands();
        }

        private void toolStripButtonAlignLeft_Click(object sender, EventArgs e)
        {
            foreach (Control cr in this.alControl)
            {
                ////ControlBox_Select cs = cr as ControlBox_Select
                //((CustomControl)cr).Cs.ShowHandles();
                //((CustomControl)cr).宽度 = this.CurrentControl.宽度;
                //((CustomControl)cr).Invalidate();
                //((CustomControl)cr).Location = new Point(this.CurrentControl.水平位置, ((CustomControl)cr).垂直位置);
                BackupSelectedComponents();
                BandBase base1 = this.FindBandPosY(cr.Location.Y);
                ((CustomControl)cr).Location = new Point(this.CurrentControl.Location.X, ((CustomControl)cr).Location.Y);

                BandBase base2 = this.FindBandPosY(cr.Location.Y);
                if (base2 != null)
                {
                   
                    if (base1 != base2)
                    {
                        this.RemoveTreeViewControl(((CustomControl)cr));
                        ((CustomControl)cr).Band = base2;
                        this.AddTreeViewControl(((CustomControl)cr));
                    }
                    ((CustomControl)cr).垂直位置 = ((CustomControl)cr).Location.Y - base2.Top;
                    ((CustomControl)cr).水平位置 = this.CurrentControl.Location.X;
                }
                ((CustomControl)cr).Cs.MoveHandles();
                ChangeSelectComponentsSizeLocation("Location");
                //ChangeSelectComponentsSizeLocation("Size");
                TransactionsCommit();
            }
            this.ArrangeBands();
        }

        private void toolStripButtonAlignRight_Click(object sender, EventArgs e)
        {
            foreach (Control cr in this.alControl)
            {
                ////ControlBox_Select cs = cr as ControlBox_Select
                //((CustomControl)cr).Cs.ShowHandles();
                //((CustomControl)cr).宽度 = this.CurrentControl.宽度;
                //((CustomControl)cr).Invalidate();
                //((CustomControl)cr).Location = new Point(this.CurrentControl.水平位置, ((CustomControl)cr).垂直位置);
                BackupSelectedComponents();
                BandBase base1 = this.FindBandPosY(cr.Location.Y);
                ((CustomControl)cr).Location = new Point(this.CurrentControl.Location.X + this.CurrentControl.Width - ((CustomControl)cr).Width , ((CustomControl)cr).Location.Y);

                BandBase base2 = this.FindBandPosY(cr.Location.Y);
                if (base2 != null)
                {
                   
                    if (base1 != base2)
                    {
                        this.RemoveTreeViewControl(((CustomControl)cr));
                        ((CustomControl)cr).Band = base2;
                        this.AddTreeViewControl(((CustomControl)cr));
                    }
                    ((CustomControl)cr).垂直位置 = ((CustomControl)cr).Location.Y - base2.Top;
                    ((CustomControl)cr).水平位置 = ((CustomControl)cr).Location.X;
                }
                ((CustomControl)cr).Cs.MoveHandles();
                ChangeSelectComponentsSizeLocation("Location");
                //ChangeSelectComponentsSizeLocation("Size");
                TransactionsCommit();

            }
            this.ArrangeBands();
        }

        private void toolStripButtonAlignTop_Click(object sender, EventArgs e)
        {
            foreach (Control cr in this.alControl)
            {
                ////ControlBox_Select cs = cr as ControlBox_Select
                //((CustomControl)cr).Cs.ShowHandles();
                //((CustomControl)cr).宽度 = this.CurrentControl.宽度;
                //((CustomControl)cr).Invalidate();
                //((CustomControl)cr).Location = new Point(this.CurrentControl.水平位置, ((CustomControl)cr).垂直位置);
                BackupSelectedComponents();
                BandBase base1 = this.FindBandPosY(cr.Location.Y);

                ((CustomControl)cr).Location = new Point(((CustomControl)cr).Location.X,  this.CurrentControl.Location.Y );

                BandBase base2 = this.FindBandPosY(cr.Location.Y);
                if (base2 != null)
                {
                   
                    if (base1 != base2)
                    {
                        this.RemoveTreeViewControl(((CustomControl)cr));
                        ((CustomControl)cr).Band = base2;
                        this.AddTreeViewControl(((CustomControl)cr));
                    }
                    ((CustomControl)cr).垂直位置 = ((CustomControl)cr).Location.Y - base2.Top;
                    ((CustomControl)cr).水平位置 = ((CustomControl)cr).Location.X;
                }
                ((CustomControl)cr).Cs.MoveHandles();
                ChangeSelectComponentsSizeLocation("Location");
                //ChangeSelectComponentsSizeLocation("Size");
                TransactionsCommit();
            }
            this.ArrangeBands();
        }

        private void toolStripButtonWidthSame_Click(object sender, EventArgs e)
        {
            foreach (Control cr in this.alControl)
            {
                BackupSelectedComponents();
                ((CustomControl)cr).宽度 = this.CurrentControl.宽度;
                ((CustomControl)cr).Cs.MoveHandles();
               // ChangeSelectComponentsSizeLocation("Location");
                ChangeSelectComponentsSizeLocation("Size");
                TransactionsCommit();
            }
        }

        private void toolStripButtonHeightSame_Click(object sender, EventArgs e)
        {
            foreach (Control cr in this.alControl)
            {
                BackupSelectedComponents();
                ((CustomControl)cr).高度 = this.CurrentControl.高度;
                ((CustomControl)cr).Cs.MoveHandles();
            }
            // ChangeSelectComponentsSizeLocation("Location");
            ChangeSelectComponentsSizeLocation("Size");
            TransactionsCommit();
        }

        #region UndoRedo

        private void controlBox_SizeChanging(object sender, MouseEventArgs e)
        {
            BackupSelectedComponents();
        }

        private void controlBox_SizeChanged(object sender, MouseEventArgs e)
        {
            ChangeSelectComponentsSizeLocation("Location");
            ChangeSelectComponentsSizeLocation("Size");
            TransactionsCommit();
        }

        public System.Windows.Forms.Control.ControlCollection Components
        {
            get
            {
                return this.PnlAreaDesigner.Controls;
            }
        }

        public Control ParentControl
        {
            get
            {
                return this.PnlAreaDesigner;
            }
        }

        //public Control PrimarySelection
        //{
        //    get
        //    {
        //       // return PrimaryControl;
        //    }
        //}

        private bool ContainsControl(Control obj)
        {
            foreach (Control con in this.PnlAreaDesigner.Controls)
            {
                if (con == obj)
                {
                    return true;
                }
            }

            return false;
        }

        public Control GetControl(Control obj, string Name)
        {
            if (obj == null) return null;

            Type type = obj.GetType();

            foreach (Control con in this.PnlAreaDesigner.Controls)
            {
                if (con.GetType() == type && con.Name == Name)
                {
                    return con;
                }
            }
            foreach (BandBase  bb in this.CurrentReport.Bands )
            {
                if (bb.DesignerControl.GetType() == type && bb.DesignerControl .Name == Name)
                {
                    return bb .DesignerControl ;
                }
            }

            //foreach (BandBase bb in this.CurrentReport.BandsGroup )
            //{
            //    if (bb.DesignerControl.GetType() == type && bb.DesignerControl.Name == Name)
            //    {
            //        return bb.DesignerControl;
            //    }
            //}

            return null;
        }

        public List<Control> GetSelectedComponents()
        {
            return this.alControl;
        }

        //public void SetSelectedComponents(ArrayList components)
        public void SetSelectedComponents()
        {
            //this.alControl.Clear();

            //if (components.Count == 0)
            //{
            //    this.PnlAreaDesigner.Refresh();
            //    return;
            //}

            //foreach (Control con in components)
            //{
            //    if (ContainsControl(con))
            //    {
            //        this.alControl.Add(con);
            //    }
            //}

            //SetSelectedComponents();

            //this.PnlAreaDesigner.Refresh();

            //if (alControl.Count == 0)
            //{
            //    //if (null == propertyForm)
            //    //    propertyForm = new PropertyForm(this );
            //    propertyForm.Property = null;
            //    return;
            //}
            if (alControl.Count > 0)
            {
                foreach (Control cr in this.alControl)
                {
                    //ControlBox_Select cs = cr as ControlBox_Select
                    ((CustomControl)cr).Cs.ShowHandles();
                }
            }

            //object[] myArray = new object[alControl.Count];
            //int i = 0;

            //foreach (Control control in alControl)
            //{
            //    if (control.GetType() == typeof(System.Windows.Forms.Label))
            //    {
            //        rptLabel rl = new rptLabel(this, (System.Windows.Forms.Label)control, page);
            //        myArray.SetValue(rl, i);
            //    }

            //    if (control.GetType() == typeof(C1.Win.C1TrueDBGrid.C1TrueDBGrid))
            //    {
            //        rptTrueDBGrid rt = new rptTrueDBGrid(this, (C1.Win.C1TrueDBGrid.C1TrueDBGrid)control, page);
            //        myArray.SetValue(rt, i);
            //    }

            //    if (control.GetType() == typeof(Junxian.XReport.ReportLine))
            //    {
            //        rptLine rl = new rptLine(this, (Junxian.XReport.ReportLine)control, this.page);
            //        myArray.SetValue(rl, i);
            //    }

            //    if (control.GetType() == typeof(System.Windows.Forms.CheckBox))
            //    {
            //        rptCheckBox rl = new rptCheckBox(this, (System.Windows.Forms.CheckBox)control, this.page);
            //        myArray.SetValue(rl, i);
            //    }

            //    if (control.GetType() == typeof(Junxian.XReport.ReportRectangle))
            //    {
            //        rptRecangle rc = new rptRecangle(this, (Junxian.XReport.ReportRectangle)control, page);
            //        myArray.SetValue(rc, i);
            //    }

            //    if (control.GetType() == typeof(System.Windows.Forms.PictureBox))
            //    {
            //        rptPictureBox pb = new rptPictureBox(this, (System.Windows.Forms.PictureBox)control, page);
            //        myArray.SetValue(pb, i);
            //    }

            //    i++;
            //}

            //propertyForm.Property = myArray;

        }

        public void SetSelectedComponents(ArrayList components)
        {
            this.alControl.Clear();

            if (components.Count == 0)
            {
                this.PnlAreaDesigner.Refresh();
                return;
            }

            foreach (Control con in components)
            {
                if (ContainsControl(con))
                {
                    this.alControl.Add(con);
                }
            }

            SetSelectedComponents();
        }

        public void SetBandsLocation(ArrayList components)
        {

            foreach (object  ob in components)
            {
                SizeLocation bd = ob as SizeLocation;

                object component = null;
                if (bd.obj  is Control)
                {
                    component = GetControl(bd.obj  as Control, ((Control)bd.obj ).Name);
                    // report.DestroyComponent(oldcomponent as Control);
                    //component = oldcomponent;
                }
                //((BandDesigner)component).Top = bd .Location .Y ;

                CurrentBandControl = (BandDesigner)component;
                CurrentBand = (BandBase)((BandDesigner)component).Tag;


                if (this.CurrentBandControl != null)
                {
                    //Point point = this.CurrentBandControl.Parent.PointToClient(Cursor.Position);
                    //Point point2 = new Point(this.CurrentBandControl.Location.X, bd.Location.Y - this.positionoff.Y);
                    Point point2 = new Point(this.CurrentBandControl.Location.X, bd.Location.Y );
                    this.SetCursorCoordenadas();
                    int index = this.CurrentReport.Bands.IndexOf(this.CurrentBand);
                    if (index == 0)
                    {
                        if (bd.Location.Y < this.TopAreaDesigner)
                        {
                            point2.Y = this.TopAreaDesigner;
                        }
                        foreach (Control control in this.CurrentBand.Controls)
                        {
                            if (point2.Y < (control.Top + control.Height))
                            {
                                point2.Y = control.Top + control.Height;
                            }
                        }
                    }
                    else
                    {
                        if (index > 0)
                        {
                            int num2 = this.CurrentReport.Bands.IndexOf(this.CurrentBand);
                            if (num2 > 0)
                            {
                                BandBase base2 = (BandBase)this.CurrentReport.Bands[num2 - 1];
                                if ((base2.DesignerControl != null) && (point2.Y < (base2.DesignerControl.Top + base2.DesignerControl.Height)))
                                {
                                    point2.Y = base2.DesignerControl.Top + base2.DesignerControl.Height;
                                }
                            }
                        }
                        foreach (Control control in this.CurrentBand.Controls)
                        {
                            if (point2.Y < (control.Top + control.Height))
                            {
                                point2.Y = control.Top + control.Height;
                            }
                        }
                    }
                    CurrentBandControl.Location = point2;
                }
                if (this.CurrentBand != null)
                {
                    this.CurrentBand.Height = this.CurrentBand.DesignerControl.Top - this.CurrentBand.Top;
                }

                ArrangeAreaDesigner();
                SetRulers();
                ArrangeBands();
                BandControlPaint(this, null);

            }
            //report.CurrentBandControl = (BandDesigner)component;
            //report.CurrentBand = (BandBase)((BandDesigner)component).Tag;

            
        }

        private void BackupSelectedComponents()
        {
            if (transactions != null)
            {
                return;
            }

            this.alControlBak.Clear();

            foreach (Control con in this.alControl)
            {
                alControlBak.Add(new SizeLocation(con, con.Location, con.Size));
            }

            transactions = CreateTransaction("移动改变元素--耕耘一方,收获一方,快乐一方");

        }

        private void BackupSelectedBand()
        {
            if (transactions != null)
            {
                return;
            }

            this.alControlBak.Clear();

           

            foreach (BandBase bb in this.CurrentReport.Bands )
            {

                alControlBak.Add(new SizeLocation(bb.DesignerControl, bb.DesignerControl.Location, bb.DesignerControl.Size));
                
            }

            //foreach (BandBase bb in this.CurrentReport.BandsGroup)
            //{
            //    alControlBak.Add(new SizeLocation(bb.DesignerControl, bb.DesignerControl.Location, bb.DesignerControl.Size));
            //}
            transactions = CreateTransaction("移动改变区域--耕耘一方,收获一方,快乐一方");

        }

        private void ChangeSelectComponentsSizeLocation(string PropName)
        {
            foreach (Control con in this.alControl)
            {
                SizeLocation sl = GetControlOldSizeLocation(con);

                if (sl != null)
                {
                    if (PropName == "Size" && sl.Size != con .Size)
                    {
                        KReport.UndoRedo.ComponentChangedUndoAction.FieldName = "Name";
                        KReport.UndoRedo.ComponentChangedUndoAction.ObjectName = con.Name;
                        KReport.UndoRedo.ComponentChangedUndoAction.XmlPropertyName = PropName;

                        OnComponentChanged(con, TypeDescriptor.GetProperties(con)["Size"], sl.Size, con.Size);
                    }

                    if (PropName == "Location" && sl .Location !=con .Location )
                    {
                        KReport.UndoRedo.ComponentChangedUndoAction.FieldName = "Name";
                        KReport.UndoRedo.ComponentChangedUndoAction.ObjectName = con.Name;
                        KReport.UndoRedo.ComponentChangedUndoAction.XmlPropertyName = PropName;

                        OnComponentChanged(con, TypeDescriptor.GetProperties(con)["Location"], sl.Location, con.Location);
                    }
                }
            }
        }

        private void ChangeSelectBandLocation(string PropName)
        {

            ArrayList bandlist = new ArrayList();
            foreach (BandBase bb in this.CurrentReport.Bands)
            {

                SizeLocation sl = GetControlOldSizeLocation(bb .DesignerControl );

                if (sl != null)
                {

                    if (PropName == "Location" && sl.Location != bb.DesignerControl.Location)
                    {
                       
                        
                        bandlist.Add(sl);
                        
                    }
                }
                //KReport.UndoRedo.ComponentChangedUndoAction.FieldName = "Name";
                //KReport.UndoRedo.ComponentChangedUndoAction.ObjectName = this.CurrentBand.DesignerControl.Name;
                //KReport.UndoRedo.ComponentChangedUndoAction.XmlPropertyName = PropName;

                OnBandsChanged(bb.DesignerControl, TypeDescriptor.GetProperties(bb.DesignerControl)["Location"], bandlist, "reportBands");

            }

            //foreach (BandBase bb in this.CurrentReport.BandsGroup)
            //{
            //    SizeLocation sl = GetControlOldSizeLocation(bb.DesignerControl);

            //    if (sl != null)
            //    {

            //        if (PropName == "Location" && sl.Location != bb.DesignerControl.Location)
            //        {
            //            KReport.UndoRedo.ComponentChangedUndoAction.FieldName = "Name";
            //            KReport.UndoRedo.ComponentChangedUndoAction.ObjectName = this.CurrentBand.DesignerControl.Name;
            //            KReport.UndoRedo.ComponentChangedUndoAction.XmlPropertyName = PropName;

            //            OnComponentChanged(bb.DesignerControl, TypeDescriptor.GetProperties(bb.DesignerControl)["Location"], sl.Location, bb.DesignerControl.Location);
            //        }
            //    }
            //}
            
            
           
        }

        private SizeLocation GetControlOldSizeLocation(Control con)
        {
            foreach (SizeLocation sl in this.alControlBak)
            {
                if (sl.obj == con)
                {
                    return sl;
                }
            }

            return null;
        }

        private void TransactionsBegin()
        {
            if (transactions == null)
            {
                transactions = CreateTransaction("移动改变元素--耕耘一方,收获一方,快乐一方");
            }
        }

        public void TransactionsCommit()
        {
            if (transactions != null)
            {
                transactions.Commit();
            }

            transactions = null;
        }

        //		public void ChangeObjectProperties(object obj, string PropName, object OldValue, object newValue)
        //		{
        //			KReport.UndoRedo.ComponentChangedUndoAction.FieldName = null;
        //			KReport.UndoRedo.ComponentChangedUndoAction.ObjectName = null;
        //			KReport.UndoRedo.ComponentChangedUndoAction.XmlPropertyName = null;
        //
        //			TransactionsBegin();
        //
        //			OnComponentChanged(obj, TypeDescriptor.GetProperties(obj)[PropName], OldValue, newValue);
        //		}

        public void ChangeObjectProperties(string FieldName, string objName, string XmlPropertyName, object obj, string PropName, object OldValue, object newValue)
        {
            KReport.UndoRedo.ComponentChangedUndoAction.FieldName = FieldName;
            KReport.UndoRedo.ComponentChangedUndoAction.ObjectName = objName;
            KReport.UndoRedo.ComponentChangedUndoAction.XmlPropertyName = XmlPropertyName;

            TransactionsBegin();

            OnComponentChanged(obj, TypeDescriptor.GetProperties(obj)[PropName], OldValue, newValue);
        }

        private KReport.UndoRedo.UndoHandler undoHandler = new UndoHandler();

        private int transactionCount;          // >0 means we're doing a transaction
        string transactionscription = null;

        private StringStack transactionDescriptions;   // string descriptions of the current transactions
        private DesignerTransaction transactions = null;

        public event DesignerTransactionCloseEventHandler TransactionClosed;
        public event DesignerTransactionCloseEventHandler TransactionClosing;
        public event EventHandler TransactionOpened;
        public event EventHandler TransactionOpening;

        public event ComponentEventHandler ComponentAdded;
        public event ComponentEventHandler ComponentAdding;
        public event ComponentChangedEventHandler ComponentChanged;
        public event ComponentChangingEventHandler ComponentChanging;
        public event ComponentEventHandler ComponentRemoved;
        public event ComponentEventHandler ComponentRemoving;
        public event ComponentRenameEventHandler ComponentRename;

        public event ComponentChangedEventHandler BandsChanged;


        private void OnComponentAdded(ComponentEventArgs ce)
        {
            //this.IsChanged = true;
            if (ComponentAdded != null)
            {
                ComponentAdded(this, ce);
            }
        }

        public void OnComponentAdded(IComponent component)
        {
            //this.IsChanged = true;
            if (ComponentAdded != null)
            {
                ComponentAdded(this, new ComponentEventArgs(component));
            }
        }

        ///     This is called when a component is about to be added to our container.
        private void OnComponentAdding(ComponentEventArgs ce)
        {
            //this.IsChanged = true;
            if (ComponentAdding != null)
            {
                ComponentAdding(this, ce);
            }
        }

        public void OnComponentAdding(IComponent component)
        {
            //this.IsChanged = true;
            if (ComponentAdding != null)
            {
                ComponentAdding(this, new ComponentEventArgs(component));
            }
        }

        public void OnComponentChanged(object component, System.ComponentModel.MemberDescriptor member, object oldValue, object newValue)
        {
            //this.IsChanged = true;
            if (ComponentChanged != null)
            {
                ComponentChanged(this, new ComponentChangedEventArgs(component, member, oldValue, newValue));
            }
        }

        public void OnBandsChanged(object component, System.ComponentModel.MemberDescriptor member, object oldValue, object newValue)
        {
            //this.IsChanged = true;
            if (BandsChanged != null)
            {
                BandsChanged(this, new ComponentChangedEventArgs(component, member, oldValue, newValue));
            }
        }

        public void OnComponentChanging(object component, System.ComponentModel.MemberDescriptor member)
        {
            //this.IsChanged = true;
            if (ComponentChanging != null)
            {
                ComponentChanging(this, new ComponentChangingEventArgs(component, member));
            }
        }


        ///     This is called after a component has been removed from the container, but before
        ///     the component's site has been destroyed.
        private void OnComponentRemoved(ComponentEventArgs ce)
        {
            //this.IsChanged = true;
            if (ComponentRemoved != null)
            {
                ComponentRemoved(this, ce);
            }
        }

        private void OnComponentRemoved(IComponent component)
        {
            //this.IsChanged = true;
            if (ComponentAdded != null)
            {
                ComponentRemoved(this, new ComponentEventArgs(component));
            }
        }

        ///     This is called when a component is about to be removed from our container.
        private void OnComponentRemoving(ComponentEventArgs ce)
        {
            //this.IsChanged = true;
            if (ComponentRemoving != null)
            {
                ComponentRemoving(this, ce);
            }
        }

        ///     This is called when a component has been renamed.
        internal void OnComponentRename(ComponentRenameEventArgs ce)
        {
            //this.IsChanged = true;
            if (ComponentRename != null)
            {
                ComponentRename(this, ce);
            }
        }

        internal void OnTransactionOpened(EventArgs e)
        {
            //			string name = TransactionDescription;
            if (TransactionOpened != null)
            {
                TransactionOpened(this, e);
            }
        }

        internal void OnTransactionOpening(EventArgs e)
        {
            if (TransactionOpening != null)
                TransactionOpening(this, e);
        }

        internal void OnTransactionClosed(DesignerTransactionCloseEventArgs e)
        {
            //			string name = TransactionDescription;
            if (TransactionClosed != null)
            {
                TransactionClosed(this, e);
            }
        }

        internal void OnTransactionClosing(DesignerTransactionCloseEventArgs e)
        {
            if (TransactionClosing != null)
            {
                TransactionClosing(this, e);
            }
        }

        internal DesignerTransaction CreateTransaction(string description)
        {
            if (description == null)
            {
                description = string.Empty;
            }
            transactionscription = description;
            return new ReportDesignerTransaction(this, description);
        }

        // Get descriptions of all of our transactions.
        internal StringStack TransactionDescriptions
        {
            get
            {
                if (transactionDescriptions == null)
                {
                    transactionDescriptions = new StringStack();
                }
                return transactionDescriptions;
            }
        }

        // Get or set the number of transactions we have.
        internal int TransactionCount
        {
            get
            {
                return transactionCount;
            }
            set
            {
                transactionCount = value;
            }
        }

        // Gets a value indicating whether the designer host is currently in a transaction.
        internal bool InTransaction
        {
            get
            {
                return transactionCount > 0;
            }
        }

        internal string TransactionScription
        {
            get
            {
                return transactionscription;
            }
        }
        /// <summary>
        /// 方法，创建组件
        /// </summary>
        /// <param name="componentClass">类型</param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// 

        internal Control CreateComponent(Type type, int cx, int cy, int width, int height)
        {
            Control con = null;

            //CustomControl control = CustomControl.FactoryControl((ControlType)this.controlType);
            //this.CurrentReport.SetUniqueName(control);
            //control.Band = this.FindBandPosY(e.Y);
            //control.Location = new Point(e.X, e.Y);



            //control.垂直位置 = control.Location.Y - control.Band.Top;
            //this.InsertDesignerControl(control.Band);
            //this.ArrangeBands();
            //this.UnCheckButtons();
            //this.FillTreeViewControl();
            //this.SelectNodeOnTreeView(control.Name);
            //this.PrepareFormatButton(control);


            if (type == typeof(System.Windows.Forms.Label))
            {
                //con = CreateLabel(cx, cy, width, height);
            }

            if (type == typeof(System.Windows.Forms.CheckBox))
            {
                //con = CreateCheckBox(cx, cy, width, height);
            }

            if (type == typeof(System.Windows.Forms.PictureBox))
            {
                //con = CreatePictureBox(cx, cy, width, height);
            }

            //if (type == typeof(C1.Win.C1TrueDBGrid.C1TrueDBGrid))
            //{
            //    //con = CreateC1TrueDBGrid(cx, cy, width, height);
            //}

            //if (type == typeof(KReport.UndoRedo.ReportLine))
            //{
            //    con = CreateLine(cx, cy, width, height);
            //}

            //if (type == typeof(KReport.UndoRedo.ReportRectangle))
            //{
            //    con = CreateRectangle(cx, cy, width, height);
            //}

            //if (con != null)
            //{
            //    AddNameToVisibleList(con, con.Visible);
            //    AddNameToRectangle(con.Name, cx, cy, width, height);
            //}

            return con;
        }


        internal void DestroyComponent(Control control)
        {

            CustomControl selectedObject = (CustomControl)control;
          
            selectedObject.Band = null;
            this.propertyGrid.SelectedObject = null;
            this.LblObject.Text = string.Empty;
            this.controlBox.Remove();
            if (this.PnlAreaDesigner.Controls.Contains(selectedObject))
            {
                this.PnlAreaDesigner.Controls.Remove(selectedObject);
            }
            this.RemoveTreeViewControl(selectedObject);

           
            this.alControl.Remove(control);
           // RemoveNameToVisibleList(control);


            //			OnComponentChanged(control, null, null, null);
            OnComponentRemoved(control);

            //Save_Delete_Info(xcSchema, "Name", control.Name);
        }

        internal string CreateName(System.Type type)
        {
            int i = 0;
            string name = type.Name;
            string cname;

            // Increment counter until we find a name that's not in use
            while (true)
            {
                i++;
                cname = name + i.ToString();

                if (IsValidateName(cname))
                {
                    return cname;
                }
            }
        }

        private bool IsValidateName(string name)
        {
            name = name.ToLower();

            foreach (Control control in this.PnlAreaDesigner.Controls)
            {
                if (control.Name.ToLower() == name)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CanUndo
        {
            get
            {
                return undoHandler.EnableUndo;
            }
        }

        private bool CanRedo
        {
            get
            {
                return undoHandler.EnableRedo;
            }
        }

        private void Undo()
        {
            this.CurrentControl.Cs.Remove();
            this.controlBox.Remove();
            
            undoHandler.Undo();
            
            //this.controlBox.WireControl(this.CurrentControl);
           //ArrangeBands();
           
        }

        private void Redo()
        {
            undoHandler.Redo();
            ArrangeBands();
            //this.controlBox.WireControl(this.CurrentControl);
        }

        private void Reset()
        {
            undoHandler.Reset();
        }
        public void SetCurrentControl(Control nowcontrol)
        {
            this.CurrentControl = nowcontrol as CustomControl;
            this.controlBox.Remove();
            this.controlBox.WireControl(this.CurrentControl);
            this.controlBox.ShowHandlesDeny(this.CurrentControl);
           
        
        }

        #endregion
        #region PropertyValue

        private string strPropertyValue = "";
        private ReportTemplateXML xcSchema;
        public string GetPropertyValue(string cName, string pName)
        {
            strPropertyValue = null;
            xcSchema = new ReportTemplateXML(this.CurrentReport);
            Get_PropertyValue(xcSchema, cName, pName);
            return strPropertyValue;
        }

        /// <summary>
        /// 方法,给出控件名属性名球处属性值
        /// </summary>
        /// <param name="cName">控件名称</param>
        /// <param name="pName">所要保存的属性</param>
        /// <param name="pValue">属性值</param>
        private void Get_PropertyValue(ReportTemplateXML xc, string cName, string pName)
        {
            //foreach (string key in xc.Controls.GetKeyList())
            //{
            //    ArrayList alXmlControls = (ArrayList)xc.Controls[key];

            //    foreach (Junxian.XReport.XmlComponentSchema ControlSchema in alXmlControls)
            //    {
            //        if (ControlSchema.Controls.Count != 0)
            //        {
            //            xc = ControlSchema;
            //            Get_PropertyValue(xc, cName, pName);
            //        }

            //        foreach (Junxian.XReport.XmlProperty xp in ControlSchema.Properties)
            //        {
            //            if (xp.Value == cName)
            //            {
            //                foreach (Junxian.XReport.XmlProperty xp1 in ControlSchema.Properties)
            //                {
            //                    if (xp1.Name == pName)
            //                    {
            //                        strPropertyValue = xp1.Value;
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }



       

        private void Add_MenItem_InvisisbleCon(Control con)
        {
            int index = 16;

            if (con is Label)
            {
                index = 16;
            }

            //if (con is C1TrueDBGrid)
            //{
            //    index = 17;
            //}

            //if (con is Junxian.XReport.ReportRectangle)
            //{
            //    index = 18;
            //}

            //if (con is Junxian.XReport.ReportLine)
            //{
            //    index = 19;
            //}

            //if (con is PictureBox)
            {
                index = 22;
            }

            //Junxian.Magic.Menus.MenuCommand mc_invisible = new Junxian.Magic.Menus.MenuCommand(con.Name, this.imageList3, index, new System.EventHandler(this.displayHideControl));
            //mc_displayElement.MenuCommands.Add(mc_invisible);
        }

        /// <summary>
        /// 方法，添加页合计字段
        /// </summary>
        /// <param name="dataField">字段名</param>
        /// <param name="text">字段文本</param>
        public void AddPageFooterText(string dataField, string text)
        {
            //if (!slPageFooter.ContainsKey(dataField))
            //{
            //    slPageFooter.Add(dataField, text);
            //}
            //else
            //{
            //    slPageFooter[dataField] = text;
            //}
        }

        /// <summary>
        /// 方法，删除页合计字段
        /// </summary>
        /// <param name="dataField">字段名</param>
        public void RemovePageFooterText(string dataField)
        {
            //if (slPageFooter.ContainsKey(dataField))
            //{
            //    slPageFooter.Remove(dataField);
            //}
        }

       


        public void ControlLocked()
        {


            foreach (Control control in alControl)
            {
                string cName = control.Name;
                //if (!alControlLocked.Contains(cName))
                //{
                //    alControlLocked.Add(cName);
                //}
                //DrawLockedRectangle(control);
            }
        }

        public void ControlUnlocked()
        {
            foreach (Control control in alControl)
            {
                string cName = control.Name;
                //if (alControlLocked.Contains(cName))
                //{
                //    alControlLocked.Remove(cName);
                //}
            }

            //page.Refresh();
            //DrawGrabHandle_ChooseControlsExtreme();
        }

        public bool IsLocked(string cName)
        {
            bool b = false;
            //if (alControlLocked.Contains(cName))
            //{
            //    b = true;
            //}
            return b;
        }


        #endregion

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            Undo();
            this.toolStripButtonUndo.Enabled = this.CanUndo;
            this.toolStripButtonRedo.Enabled = this.CanRedo;
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            Redo();
            this.toolStripButtonUndo.Enabled = this.CanUndo;
            this.toolStripButtonRedo.Enabled = this.CanRedo;
        }

        private void DesignerReport_Load(object sender, EventArgs e)
        {
            undoHandler.Reset();
            this.TbCrlReportDesigner.SelectedIndex = 1;
            this.TbCrlReportDesigner.SelectedIndex = 0;

            if (!HaveDesignRight)
            {
                SetNoDesign();
            
            }

        }

        public void SetNoDesign()
        {

            this.TbCrlReportDesigner.TabPages.RemoveAt(1);
        }

        private void DesignerReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            undoHandler.Detach();
        }
    }
    internal class SizeLocation
    {
        public Control obj;
        public Point Location;
        public Size Size;

        public SizeLocation(Control c, Point location, Size size)
        {
            this.obj = c;
            this.Location = location;
            this.Size = size;
        }
    }
}

