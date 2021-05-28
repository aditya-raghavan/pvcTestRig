using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRigLibrary
{
    /// <summary>
    /// Acts as a controller connecting UI and Backend.
    /// </summary>
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; set; }

        public static void InitialiseConnections()
        {
            if (!Directory.Exists($"{ ConfigurationManager.AppSettings["filePath"] }"))
            {
                Directory.CreateDirectory($"{ ConfigurationManager.AppSettings["filePath"] }");
            }
            TextConnector txt = new TextConnector();
            Connection = txt;
        }
    }
}
