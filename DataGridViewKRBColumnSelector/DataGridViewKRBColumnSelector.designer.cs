namespace CustomDataGridViewKRBColumnSelector
{
    partial class DataGridViewKRBColumnSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();

                font.Dispose();

                menuItem.Dispose();
                barGradient.Dispose();
                areaCheckBox.Dispose();
                hoverAreaCheckBox.Dispose();
                exitButton.Dispose();

                popup.Closed -= new System.Windows.Forms.ToolStripDropDownClosedEventHandler(popup_Closed);
                popup.Dispose();

                if (dropDownControl != null)
                    dropDownControl.Dispose();
            }
            
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }
}
