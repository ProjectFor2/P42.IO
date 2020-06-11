using System.Activities;
using System.Windows;
using System.Windows.Forms;

namespace P42.IO.Directory
{
    // Interaction logic for DeleteFolderDesigner.xaml
    public partial class DeleteFolderDesigner
    {
        private string selectedPath;

        public DeleteFolderDesigner() => InitializeComponent();

        private void BrowseFolder(object sender, RoutedEventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = Properties.Resources.FolderBrowserDialogDescription;
                dlg.SelectedPath = selectedPath;
                dlg.ShowNewFolderButton = true;

                var result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    selectedPath = dlg.SelectedPath;

                    var p = ModelItem.Properties["Path"];
                    p.SetValue(new InArgument<string>(selectedPath));
                    inPath.UpdateLayout();
                }
            }
        }
    }
}
