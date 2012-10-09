using System.IO;
using System.Windows.Forms;

namespace Shared
{
    public static class FileDialogs
    {
        public static string AskUserForFileNameToSaveIn()
        {
            return AskUserForFileNameToSaveIn(null);
        }

        public static string AskUserForFileNameToSaveIn(string extension)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (extension != null && !Path.HasExtension(dialog.FileName))
                {
                    return dialog.FileName + extension;
                }

                return dialog.FileName;
            }

            return null;
        }

        public static string AskUserForFileNameToOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
