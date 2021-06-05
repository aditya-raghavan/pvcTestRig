using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRigLibrary.Templates;

namespace TestRigLibrary
{
    /// <summary>
    /// Acts as a controller connecting UI and Backend.
    /// </summary>
    public static class GlobalConfig
    {
        public static string  AllowedFileType = "Text documents (.csv)|*.csv";
        
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

        public static TestConfigurationTemplate SetLimits()
        {
            TestConfigurationTemplate template = new TestConfigurationTemplate();


            template.positiveTolerenceVoltageMax = 1;
            template.negativeTolerenceVoltageMax = 2;
            template.nominalForwardDropVoltsMax = 3;
            template.positiveTolerenceCurrentMax = 4;
            template.negativeTolerenceCurrentMax = 5;
            template.nominalReverseCurrentMax = 6;
            template.forwardTestCurrentMax = 7;
            template.reverseTestVoltageMax = 8;
            template.forwardMaxVoltageMax = 9;
            template.positiveTolerenceResistanceMax = 10;
            template.negativeTolerenceResistanceMax =11;
            template.contactResistanceMax = 12;

            template.positiveTolerenceVoltageMin = -1;
            template.negativeTolerenceVoltageMin = -2;
            template.nominalForwardDropVoltsMin = -3;
            template.positiveTolerenceCurrentMin = -4;
            template.negativeTolerenceCurrentMin = -5;
            template.nominalReverseCurrentMin = -6;
            template.forwardTestCurrentMin = -7;
            template.reverseTestVoltageMin = -8;
            template.forwardMaxVoltageMin = -9;
            template.positiveTolerenceResistanceMin = -10;
            template.negativeTolerenceResistanceMin = -11;
            template.contactResistanceMin = -12;

            return template;
        }
    }
}
