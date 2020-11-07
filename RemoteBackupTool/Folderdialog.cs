using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace RemoteBackupTool
{
    public class folderdialog : FolderNameEditor
    {
        FolderNameEditor.FolderBrowser fdialog = new FolderNameEditor.FolderBrowser();
        public folderdialog()
        {
        }
        public DialogResult displaydialog()
        {
            return displaydialog("请选择一个目录");
        }
        public DialogResult displaydialog(string description)
        {
            fdialog.Description = description;
            return fdialog.ShowDialog();
        }
        public string path
        {
            get
            {
                return fdialog.DirectoryPath;
            }
        }
        ~folderdialog()
        {
            fdialog.Dispose();
        }
    }
}
