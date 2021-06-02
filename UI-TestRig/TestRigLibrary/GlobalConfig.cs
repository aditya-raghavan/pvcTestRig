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


            template.maxPositiveTolerenceVoltage = 5;
            template.maxNegativeTolerenceVoltage = 5;
            template.maxNominalForwardDropVolts = 5;
            template.maxPositiveTolerenceCurrent = 5;
            template.maxNegativeTolerenceCurrent = 5;
            template.maxNominalReverseCurrent = 5;
            template.maxForwardTestCurrent = 5;
            template.maxReverseTestVoltage = 5;
            template.maxForwardMaxVoltage = 5;
            template.maxPositiveTolerenceResistance = 5;
            template.maxNegativeTolerenceResistance = 5;
            template.maxContactResistance = 5;

            template.minPositiveTolerenceVoltage = 0;
            template.minNegativeTolerenceVoltage = 0;
            template.minNominalForwardDropVolts = 0;
            template.minPositiveTolerenceCurrent = 0;
            template.minNegativeTolerenceCurrent = 0;
            template.minNominalReverseCurrent = 0;
            template.minForwardTestCurrent = 0;
            template.minReverseTestVoltage = 0;
            template.minForwardMaxVoltage = 0;
            template.minPositiveTolerenceResistance = 0;
            template.minNegativeTolerenceResistance = 0;
            template.minContactResistance = 0;

            return template;
        }
    }
}
