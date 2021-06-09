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

        public static TestConfigurationTemplate machineDataObject { get; set; } = new TestConfigurationTemplate();

        public static bool isMachineDataFileThere = false;

        public static void LoadMachineData()
        {
            if(Connection.CheckMachineDataFile() == true)
            {
                machineDataObject = Connection.LoadMachineDataFile();
                isMachineDataFileThere = true;
            }
            else
            {
                machineDataObject.positiveTolerenceVoltageHigh = 0;
                machineDataObject.negativeTolerenceVoltageHigh = 0;
                machineDataObject.nominalForwardDropVoltsHigh = 0;
                machineDataObject.positiveTolerenceCurrentHigh = 0;
                machineDataObject.negativeTolerenceCurrentHigh = 0;
                machineDataObject.nominalReverseCurrentHigh = 0;
                machineDataObject.forwardTestCurrentHigh = 0;
                machineDataObject.reverseTestVoltageHigh = 0;
                machineDataObject.forwardMaxVoltageHigh = 0;
                machineDataObject.positiveTolerenceResistanceHigh = 0;
                machineDataObject.negativeTolerenceResistanceHigh = 0;
                machineDataObject.contactResistanceHigh = 0;

                machineDataObject.positiveTolerenceVoltageLow = 0;
                machineDataObject.negativeTolerenceVoltageLow = 0;
                machineDataObject.nominalForwardDropVoltsLow = 0;
                machineDataObject.positiveTolerenceCurrentLow = 0;
                machineDataObject.negativeTolerenceCurrentLow = 0;
                machineDataObject.nominalReverseCurrentLow = 0;
                machineDataObject.forwardTestCurrentLow = 0;
                machineDataObject.reverseTestVoltageLow = 0;
                machineDataObject.forwardMaxVoltageLow = 0;
                machineDataObject.positiveTolerenceResistanceLow = 0;
                machineDataObject.negativeTolerenceResistanceLow = 0;
                machineDataObject.contactResistanceLow = 0;
            }
        }
    }
}
