using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SuperStore.Common
{
    public class ExceptionManager
    {
        public static void WriteExceptionMessageToFile(string message, Exception exception)
        {
            //Code to write file...
            try
            {
                var fileName = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustomExceptionFileName"]);

                var errorMessages = new StringBuilder();
                errorMessages.AppendLine("----------------------------------------------" + "\n");
                //errorMessages.AppendLine("UserName : " + HttpContext.Current.Session[SessionVariables.email] + "\n");
                errorMessages.AppendLine("DateTime : " + DateTime.Now + "\n");
                errorMessages.AppendLine("Message : " + exception.Message + "\n");
                errorMessages.AppendLine("Link : " + exception.HelpLink + "\n");
                errorMessages.AppendLine("InnerException : " + exception.InnerException + "\n");
                errorMessages.AppendLine(HttpContext.Current.User.Identity.Name);
                errorMessages.AppendLine("StackTrace: " + exception.StackTrace);
                errorMessages.AppendLine("----------------------------------------------" + "\n");

                if (File.Exists(fileName) && fileName != null)
                {
                    File.AppendAllText(fileName, errorMessages.ToString());
                }
            }
            catch (Exception)
            {
                               
            }

        }

    }
}