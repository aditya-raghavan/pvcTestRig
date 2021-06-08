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

        public static string machinedDataFile = "MachineData.csv";
        public static IDataConnection Connection { get; set; }

        public static void InitialiseConnections()
        {
            if (!Directory.Exists($"{ ConfigurationManager.AppSettings["dataPath"] }"))
            {
                Directory.CreateDirectory($"{ ConfigurationManager.AppSettings["dataPath"] }");
            }
            if (!Directory.Exists($"{ ConfigurationManager.AppSettings["machinedataPath"] }"))
            {
                Directory.CreateDirectory($"{ ConfigurationManager.AppSettings["machinedataPath"] }");
            }
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
            if(Connection.CheckMachineDataFile() == true)
            {
                machineDataModel = Connection.LoadMachineDataFile();
                isMachineDataFileThere = true;
            }
            else
            {
                machineDataModel.positiveTolerenceVoltageHigh = 0;
                machineDataModel.negativeTolerenceVoltageHigh = 0;
                machineDataModel.nominalForwardDropVoltsHigh = 0;
                machineDataModel.positiveTolerenceCurrentHigh = 0;
                machineDataModel.negativeTolerenceCurrentHigh = 0;
                machineDataModel.nominalReverseCurrentHigh = 0;
                machineDataModel.forwardTestCurrentHigh = 0;
                machineDataModel.reverseTestVoltageHigh = 0;
                machineDataModel.forwardMaxVoltageHigh = 0;
                machineDataModel.positiveTolerenceResistanceHigh = 0;
                machineDataModel.negativeTolerenceResistanceHigh = 0;
                machineDataModel.contactResistanceHigh = 0;

                machineDataModel.positiveTolerenceVoltageLow = 0;
                machineDataModel.negativeTolerenceVoltageLow = 0;
                machineDataModel.nominalForwardDropVoltsLow = 0;
                machineDataModel.positiveTolerenceCurrentLow = 0;
                machineDataModel.negativeTolerenceCurrentLow = 0;
                machineDataModel.nominalReverseCurrentLow = 0;
                machineDataModel.forwardTestCurrentLow = 0;
                machineDataModel.reverseTestVoltageLow = 0;
                machineDataModel.forwardMaxVoltageLow = 0;
                machineDataModel.positiveTolerenceResistanceLow = 0;
                machineDataModel.negativeTolerenceResistanceLow = 0;
                machineDataModel.contactResistanceLow = 0;
            }
        }
    }
}
