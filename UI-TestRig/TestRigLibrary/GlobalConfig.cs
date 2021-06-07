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
        public static string  allowedFileTypes = "Text documents (.csv)|*.csv";
        
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

        public static TestConfigurationTemplate machineDataModel { get; set; } = new TestConfigurationTemplate();

        public static bool isMachineDataFileThere = false;

        public static void LoadMachineData()
        {
            machineDataModel.positiveTolerenceVoltageMax = 0;
            machineDataModel.negativeTolerenceVoltageMax = 0;
            machineDataModel.nominalForwardDropVoltsMax = 0;
            machineDataModel.positiveTolerenceCurrentMax = 0;
            machineDataModel.negativeTolerenceCurrentMax = 0;
            machineDataModel.nominalReverseCurrentMax = 0;
            machineDataModel.forwardTestCurrentMax = 0;
            machineDataModel.reverseTestVoltageMax = 0;
            machineDataModel.forwardMaxVoltageMax = 0;
            machineDataModel.positiveTolerenceResistanceMax = 0;
            machineDataModel.negativeTolerenceResistanceMax = 0;
            machineDataModel.contactResistanceMax = 0;

            machineDataModel.positiveTolerenceVoltageMin = 0;
            machineDataModel.negativeTolerenceVoltageMin = 0;
            machineDataModel.nominalForwardDropVoltsMin = 0;
            machineDataModel.positiveTolerenceCurrentMin = 0;
            machineDataModel.negativeTolerenceCurrentMin = 0;
            machineDataModel.nominalReverseCurrentMin = 0;
            machineDataModel.forwardTestCurrentMin = 0;
            machineDataModel.reverseTestVoltageMin = 0;
            machineDataModel.forwardMaxVoltageMin = 0;
            machineDataModel.positiveTolerenceResistanceMin = 0;
            machineDataModel.negativeTolerenceResistanceMin = 0;
            machineDataModel.contactResistanceMin = 0;
        }
    }
}
