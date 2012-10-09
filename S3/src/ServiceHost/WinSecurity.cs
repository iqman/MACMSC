using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace ServiceHost
{
    /// <summary>
    /// Class which can be used to check if the process is running with vista limited administrator or as real administrator rights.
    /// Can be used to apply the widely known "shield" image to a button which will result in elevation of the process.
    /// </summary>
    public static class WinSecurity
    {
        [DllImport("user32")]
        private static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        internal const int BCM_FIRST = 0x1600;
        internal const int BCM_SETSHIELD = (BCM_FIRST + 0x000C);

        /// <summary>
        /// Determines if the current operation system is vista or higher.
        /// As of 2010 this would be Vista or Win7.
        /// </summary>
        /// <returns>True if vista or higher</returns>
        public static bool IsVistaOrHigher()
        {
            return Environment.OSVersion.Version.Major >= 6;
        }

        /// <summary>
        /// Checks if the process is elevated
        /// </summary>
        /// <returns>True if elevated</returns>
        public static bool IsAdmin()
        {
            WindowsPrincipal p = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return p.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Restart the current process with administrator credentials
        /// </summary>
        /// <param name="arguments">Command line arguments to be passed when restarting process</param>
        /// <returns>True if the user chose to restart as elevated. This should close this process. False if the user cancelled</returns>
        public static bool RestartElevated(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            if (!string.IsNullOrEmpty(arguments))
            {
                startInfo.Arguments = arguments;
            }

            try
            {
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return false; //If cancelled, do nothing
            }

            return true;
        }

        /// <summary>
        /// Restart the current process with administrator credentials
        /// </summary>
        public static void RestartElevated()
        {
            RestartElevated(null);
        }
    }
}
