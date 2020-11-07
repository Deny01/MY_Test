namespace KReport.Engine
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormPreviewScreen : Form
    {
        private ToolStripButton btnDoublePage;
        private ToolStripButton BtnFirstPage;
        private ToolStripButton BtnLasPage;
        private ToolStripButton BtnNextPage;
        private ToolStripButton BtnPriorPage;
        private ToolStripButton btnQuaterPage;
        private ToolStripButton btnSinglePage;
        private ToolStripComboBox cbxZoom;
        private IContainer components = null;
        private ControlScreen controlScreen1;
        private ToolStripTextBox EdtPage;
        private static FormPreviewScreen form = null;
        private ToolStripButton helpToolStripButton;
        private int indexpage = 1;
        private ToolStripButton printToolStripButton;
        private Report report;
        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripStatusLabel toolStripStatusLblPagesNumerNumber;

        public FormPreviewScreen()
        {
            this.InitializeComponent();
            this.cbxZoom.Items.AddRange(new object[] { "200", "180", "150", "120", "100", "80", "75", "50", "25" });
            this.cbxZoom.SelectedIndexChanged += new EventHandler(this.cbxZoom_SelectedIndexChanged);
            this.cbxZoom.SelectedIndex = 2;
        }

        private void BtnFirstPage_Click(object sender, EventArgs e)
        {
            this.indexpage = 1;
            this.controlScreen1.StartPage = this.indexpage;
            this.controlScreen1.ShowScreen();
        }

        private void BtnLasPage_Click(object sender, EventArgs e)
        {
            int count = this.controlScreen1.DeviceScreen.Pages.Count;
            this.indexpage = count;
            this.controlScreen1.StartPage = this.indexpage;
            this.controlScreen1.ShowScreen();
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            if (this.indexpage < this.controlScreen1.DeviceScreen.Pages.Count)
            {
                this.indexpage++;
            }
            this.controlScreen1.StartPage = this.indexpage;
            this.controlScreen1.ShowScreen();
        }

        private void BtnPriorPage_Click(object sender, EventArgs e)
        {
            if (this.indexpage > 1)
            {
                this.indexpage--;
            }
            this.controlScreen1.StartPage = this.indexpage;
            this.controlScreen1.ShowScreen();
        }

        private void cbxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.controlScreen1.Zoom = Convert.ToDouble(this.cbxZoom.Text) / 100.0;
            }
            catch
            {
                this.controlScreen1.Zoom = 1.0;
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

        private void FormPreviewScreen_Load(object sender, EventArgs e)
        {
            this.ProcessReport();
        }

        private void InitializeComponent()
        {
            this.controlScreen1 = new KReport.Engine.ControlScreen();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbxZoom = new System.Windows.Forms.ToolStripComboBox();
            this.btnDoublePage = new System.Windows.Forms.ToolStripButton();
            this.btnSinglePage = new System.Windows.Forms.ToolStripButton();
            this.btnQuaterPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnFirstPage = new System.Windows.Forms.ToolStripButton();
            this.BtnPriorPage = new System.Windows.Forms.ToolStripButton();
            this.EdtPage = new System.Windows.Forms.ToolStripTextBox();
            this.BtnNextPage = new System.Windows.Forms.ToolStripButton();
            this.BtnLasPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLblPagesNumerNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlScreen1
            // 
            this.controlScreen1.Columns = 1;
            this.controlScreen1.DeviceScreen = null;
            this.controlScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlScreen1.Location = new System.Drawing.Point(0, 25);
            this.controlScreen1.Name = "controlScreen1";
            this.controlScreen1.Rows = 1;
            this.controlScreen1.Size = new System.Drawing.Size(650, 340);
            this.controlScreen1.StartPage = 1;
            this.controlScreen1.TabIndex = 1;
            this.controlScreen1.Zoom = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripButton,
            this.toolStripSeparator,
            this.toolStripLabel1,
            this.cbxZoom,
            this.btnDoublePage,
            this.btnSinglePage,
            this.btnQuaterPage,
            this.toolStripSeparator2,
            this.BtnFirstPage,
            this.BtnPriorPage,
            this.EdtPage,
            this.BtnNextPage,
            this.BtnLasPage,
            this.toolStripSeparator1,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(650, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "Zoom";
            // 
            // cbxZoom
            // 
            this.cbxZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZoom.Name = "cbxZoom";
            this.cbxZoom.Size = new System.Drawing.Size(75, 25);
            this.cbxZoom.SelectedIndexChanged += new System.EventHandler(this.cbxZoom_SelectedIndexChanged);
            // 
            // btnDoublePage
            // 
            this.btnDoublePage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDoublePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDoublePage.Name = "btnDoublePage";
            this.btnDoublePage.Size = new System.Drawing.Size(23, 22);
            this.btnDoublePage.Text = "toolStripButton2";
            // 
            // btnSinglePage
            // 
            this.btnSinglePage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSinglePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSinglePage.Name = "btnSinglePage";
            this.btnSinglePage.Size = new System.Drawing.Size(23, 22);
            this.btnSinglePage.Text = "toolStripButton3";
            // 
            // btnQuaterPage
            // 
            this.btnQuaterPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuaterPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuaterPage.Name = "btnQuaterPage";
            this.btnQuaterPage.Size = new System.Drawing.Size(23, 22);
            this.btnQuaterPage.Text = "toolStripButton1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // BtnFirstPage
            // 
            this.BtnFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnFirstPage.Name = "BtnFirstPage";
            this.BtnFirstPage.Size = new System.Drawing.Size(23, 22);
            this.BtnFirstPage.Click += new System.EventHandler(this.BtnFirstPage_Click);
            // 
            // BtnPriorPage
            // 
            this.BtnPriorPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnPriorPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnPriorPage.Name = "BtnPriorPage";
            this.BtnPriorPage.Size = new System.Drawing.Size(23, 22);
            this.BtnPriorPage.Text = "<";
            this.BtnPriorPage.Click += new System.EventHandler(this.BtnPriorPage_Click);
            // 
            // EdtPage
            // 
            this.EdtPage.Name = "EdtPage";
            this.EdtPage.Size = new System.Drawing.Size(40, 25);
            // 
            // BtnNextPage
            // 
            this.BtnNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnNextPage.Name = "BtnNextPage";
            this.BtnNextPage.Size = new System.Drawing.Size(23, 22);
            this.BtnNextPage.Click += new System.EventHandler(this.BtnNextPage_Click);
            // 
            // BtnLasPage
            // 
            this.BtnLasPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnLasPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnLasPage.Name = "BtnLasPage";
            this.BtnLasPage.Size = new System.Drawing.Size(23, 22);
            this.BtnLasPage.Text = ">>";
            this.BtnLasPage.Click += new System.EventHandler(this.BtnLasPage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLblPagesNumerNumber});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(650, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLblPagesNumerNumber
            // 
            this.toolStripStatusLblPagesNumerNumber.Name = "toolStripStatusLblPagesNumerNumber";
            this.toolStripStatusLblPagesNumerNumber.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLblPagesNumerNumber.Text = "1 de 10";
            // 
            // FormPreviewScreen
            // 
            this.ClientSize = new System.Drawing.Size(650, 387);
            this.Controls.Add(this.controlScreen1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormPreviewScreen";
            this.Text = "Visualização";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPreviewScreen_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ProcessMensage()
        {
            //this.toolStripStatusLblPagesNumerNumber.Text = "Carregando relatorio...";
            this.toolStripStatusLblPagesNumerNumber.Text = "计算中...";
            Application.DoEvents();
        }

        private void ProcessReport()
        {
            this.report.ReportStart += new ReportStartEventHandler(this.ProcessMensage);
            this.report.ReportCompleted += new ReportCompletedEventHandler(this.ProcessTotalPage);
            this.report.PrintToDevive();
            this.controlScreen1.DeviceScreen = (ReportDeviceScreen) this.report.Device;
            this.controlScreen1.ShowScreen();
        }

        private void ProcessTotalPage()
        {
            this.toolStripStatusLblPagesNumerNumber.Text = "第 " + this.indexpage.ToString() + " 页 共 " + this.report.PagesCount.ToString() + " 页";
        }

        public static DialogResult Show(Report report)
        {
            form = new FormPreviewScreen();
            form.report = report;
            return form.ShowDialog();
        }
    }
}

