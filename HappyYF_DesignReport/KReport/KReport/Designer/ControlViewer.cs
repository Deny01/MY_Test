namespace KReport.Designer
{
    using KReport.Engine;
    using KReport.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    internal class ControlViewer : UserControl
    {
        private ToolStripButton btnDoublePage;
        private ToolStripButton BtnFirstPage;
        private ToolStripButton BtnLasPage;
        private ToolStripButton BtnNextPage;
        private ToolStripButton BtnPriorPage;
        private ToolStripButton btnQuaterPage;
        private ToolStripButton btnSinglePage;
        private ToolStripComboBox cbxZoom;
        private ToolStripTextBox EdtPage;
        private int endPage = -1;
        private ToolStripButton helpToolStripButton;
        private int indexPage = 1;
        private PrintDialog printDialog1;
        private PrintPreviewControl printPreviewControl;
        private ToolStripButton printToolStripButton;
        private Report report;
        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonPrintSetup;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripStatusLabel toolStripStatusLblPagesNumerNumber;

        public void SetNoprint()
        {
            this.printToolStripButton.Visible = false;
            this.toolStripButtonPrintSetup.Visible = false;
        }
        public ControlViewer()
        {
            this.InitializeComponent();
            this.cbxZoom.Items.AddRange(new object[] { "200", "150", "100", "75", "50", "25" });
            this.cbxZoom.SelectedIndexChanged += new EventHandler(this.cbxZoom_SelectedIndexChanged);
            this.cbxZoom.SelectedIndex = 2;
            this.printToolStripButton.Click += new EventHandler(this.printClick);
            //this.printToolStripButton.Text = "Imprimir";
           // this.toolStripButtonPrintSetup.Text = "Configurar impress\x00e3o";
            //this.btnDoublePage.Text = "Duas paginas";
            //this.btnSinglePage.Text = "Uma pagina";
            //this.btnQuaterPage.Text = "Quatro paginas";
            //this.BtnFirstPage.Text = "Primeira";
            //this.BtnLasPage.Text = "Ultima";
            //this.BtnNextPage.Text = "Proxima";
            //this.BtnPriorPage.Text = "Anterior";
        }

        private void btnDoublePage_Click(object sender, EventArgs e)
        {
            this.printPreviewControl.Rows = 1;
            this.printPreviewControl.Columns = 2;
        }

        private void BtnFirstPage_Click(object sender, EventArgs e)
        {
            this.GoToFirstPage();
        }

        private void BtnLasPage_Click(object sender, EventArgs e)
        {
            this.GoToLastPage();
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            this.GoToNextPage();
        }

        private void BtnPriorPage_Click(object sender, EventArgs e)
        {
            this.GoToPriorPage();
        }

        private void btnQuaterPage_Click(object sender, EventArgs e)
        {
            this.printPreviewControl.Columns = 2;
            this.printPreviewControl.Rows = 2;
        }

        private void btnSinglePage_Click(object sender, EventArgs e)
        {
            this.printPreviewControl.Columns = 1;
            this.printPreviewControl.Rows = 1;
        }

        private void cbxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.printPreviewControl.Zoom = Convert.ToDouble(this.cbxZoom.Text) / 100.0;
            }
            catch
            {
                this.printPreviewControl.Zoom = 1.0;
            }
        }

        private void GoToFirstPage()
        {
            this.indexPage = 1;
            this.SeekPage();
        }

        private void GoToLastPage()
        {
            this.endPage = -1;
            while (this.indexPage != this.endPage)
            {
                this.indexPage++;
                this.printPreviewControl.StartPage = this.indexPage - 1;
                if (this.printPreviewControl.StartPage != (this.indexPage - 1))
                {
                    this.endPage = this.printPreviewControl.StartPage + 1;
                    this.indexPage = this.endPage;
                }
            }
            this.SeekPage();
        }

        private void GoToNextPage()
        {
            if (this.indexPage != this.endPage)
            {
                this.indexPage++;
            }
            this.SeekPage();
        }

        private void GoToPriorPage()
        {
            this.indexPage--;
            if (this.indexPage <= 0)
            {
                this.indexPage = 1;
            }
            this.SeekPage();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlViewer));
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrintSetup = new System.Windows.Forms.ToolStripButton();
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
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl.Location = new System.Drawing.Point(0, 25);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(570, 321);
            this.printPreviewControl.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripButton,
            this.toolStripButtonPrintSetup,
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
            this.toolStrip1.Size = new System.Drawing.Size(570, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = global::Properties.Resources.printToolStripButton_Image;
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "打印";
            // 
            // toolStripButtonPrintSetup
            // 
            this.toolStripButtonPrintSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrintSetup.Image = global::Properties.Resources.PrintSetupHS;
            this.toolStripButtonPrintSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrintSetup.Name = "toolStripButtonPrintSetup";
            this.toolStripButtonPrintSetup.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPrintSetup.Text = "打印设置";
            this.toolStripButtonPrintSetup.Click += new System.EventHandler(this.toolStripButtonPrintSetup_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel1.Text = "显示比例";
            // 
            // cbxZoom
            // 
            this.cbxZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZoom.Name = "cbxZoom";
            this.cbxZoom.Size = new System.Drawing.Size(75, 25);
            // 
            // btnDoublePage
            // 
            this.btnDoublePage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDoublePage.Image = ((System.Drawing.Image)(resources.GetObject("btnDoublePage.Image")));
            this.btnDoublePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDoublePage.Name = "btnDoublePage";
            this.btnDoublePage.Size = new System.Drawing.Size(23, 22);
            this.btnDoublePage.Text = "双页";
            this.btnDoublePage.Click += new System.EventHandler(this.btnDoublePage_Click);
            // 
            // btnSinglePage
            // 
            this.btnSinglePage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSinglePage.Image = ((System.Drawing.Image)(resources.GetObject("btnSinglePage.Image")));
            this.btnSinglePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSinglePage.Name = "btnSinglePage";
            this.btnSinglePage.Size = new System.Drawing.Size(23, 22);
            this.btnSinglePage.Text = "单页";
            this.btnSinglePage.Click += new System.EventHandler(this.btnSinglePage_Click);
            // 
            // btnQuaterPage
            // 
            this.btnQuaterPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuaterPage.Image = ((System.Drawing.Image)(resources.GetObject("btnQuaterPage.Image")));
            this.btnQuaterPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuaterPage.Name = "btnQuaterPage";
            this.btnQuaterPage.Size = new System.Drawing.Size(23, 22);
            this.btnQuaterPage.Text = "四页";
            this.btnQuaterPage.Click += new System.EventHandler(this.btnQuaterPage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // BtnFirstPage
            // 
            this.BtnFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("BtnFirstPage.Image")));
            this.BtnFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnFirstPage.Name = "BtnFirstPage";
            this.BtnFirstPage.Size = new System.Drawing.Size(23, 22);
            this.BtnFirstPage.Text = "首页";
            this.BtnFirstPage.Click += new System.EventHandler(this.BtnFirstPage_Click);
            // 
            // BtnPriorPage
            // 
            this.BtnPriorPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnPriorPage.Image = ((System.Drawing.Image)(resources.GetObject("BtnPriorPage.Image")));
            this.BtnPriorPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnPriorPage.Name = "BtnPriorPage";
            this.BtnPriorPage.Size = new System.Drawing.Size(23, 22);
            this.BtnPriorPage.Text = "上页";
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
            this.BtnNextPage.Image = ((System.Drawing.Image)(resources.GetObject("BtnNextPage.Image")));
            this.BtnNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnNextPage.Name = "BtnNextPage";
            this.BtnNextPage.Size = new System.Drawing.Size(23, 22);
            this.BtnNextPage.Text = "下页";
            this.BtnNextPage.Click += new System.EventHandler(this.BtnNextPage_Click);
            // 
            // BtnLasPage
            // 
            this.BtnLasPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnLasPage.Image = ((System.Drawing.Image)(resources.GetObject("BtnLasPage.Image")));
            this.BtnLasPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnLasPage.Name = "BtnLasPage";
            this.BtnLasPage.Size = new System.Drawing.Size(23, 22);
            this.BtnLasPage.Text = "尾页";
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
            this.helpToolStripButton.Image = global::Properties.Resources.helpToolStripButton_Image;
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "帮助";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLblPagesNumerNumber});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(570, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLblPagesNumerNumber
            // 
            this.toolStripStatusLblPagesNumerNumber.Name = "toolStripStatusLblPagesNumerNumber";
            this.toolStripStatusLblPagesNumerNumber.Size = new System.Drawing.Size(47, 17);
            this.toolStripStatusLblPagesNumerNumber.Text = "1 de 10";
            // 
            // printDialog1
            // 
            this.printDialog1.AllowCurrentPage = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.PrintToFile = true;
            this.printDialog1.ShowHelp = true;
            this.printDialog1.UseEXDialog = true;
            // 
            // ControlViewer
            // 
            this.Controls.Add(this.printPreviewControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ControlViewer";
            this.Size = new System.Drawing.Size(570, 368);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void printClick(object sender, EventArgs e)
        {
            PrintController printController = this.printPreviewControl.Document.PrintController;
            this.printPreviewControl.Document.PrintController = new PrintControllerWithStatusDialog(new StandardPrintController(), "Imprimindo...");
            this.printPreviewControl.Document.Print();
            this.printPreviewControl.Document.PrintController = printController;
        }

        private void ProcessMensage()
        {
            this.toolStripStatusLblPagesNumerNumber.Text = "Carregando relatorio...";
            Application.DoEvents();
        }

        private void ProcessTotalPage()
        {
            this.toolStripStatusLblPagesNumerNumber.Text = "Pagina " + this.indexPage.ToString() + " de " + this.report.PagesCount.ToString();
        }

        private void SeekPage()
        {
            this.ProcessTotalPage();
            this.printPreviewControl.StartPage = this.indexPage - 1;
            this.EdtPage.Text = this.indexPage.ToString();
        }

        public void ShowReport(Report report)
        {
            this.report = report;
            this.report.DeviceType = ReportTypeDevice.DocumentPrint;
            this.report.PrepareDevice();
            base.Controls.Remove(this.printPreviewControl);
            this.printPreviewControl = new PrintPreviewControl();
            this.printPreviewControl.Dock = DockStyle.Fill;
            base.Controls.Add(this.printPreviewControl);
            this.printPreviewControl.BringToFront();
            this.printPreviewControl.Document = ((ReportDevicePrintDocument) report.Device).Document;
            this.cbxZoom.SelectedIndex = 2;
            this.cbxZoom_SelectedIndexChanged(null, null);
            this.GoToFirstPage();
            this.report.ReportCompleted += new ReportCompletedEventHandler(this.ProcessTotalPage);
            this.report.ReportStart += new ReportStartEventHandler(this.ProcessMensage);
        }

        private void toolStripButtonPrintSetup_Click(object sender, EventArgs e)
        {
            this.printDialog1.Document = this.printPreviewControl.Document;
            this.printDialog1.PrintToFile = false;
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printPreviewControl.Document.PrinterSettings.PrintRange = this.printDialog1.PrinterSettings.PrintRange;
                this.printPreviewControl.Document.Print();
            }
        }

      
    }
}

