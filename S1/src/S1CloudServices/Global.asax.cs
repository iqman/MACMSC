using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Shared;

namespace S1CloudServices
{
    public class Global : System.Web.HttpApplication
    {
        public static string DriveLetter { get; set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            Logger.Initialize();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                DriveLetter = AzureDrive.Mount();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error mounting azure drive: ", ex);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            AzureDrive.UnMount();
        }
    }
}