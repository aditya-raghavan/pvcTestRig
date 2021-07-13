using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace UI_TestRig
{
    /// <summary>
    /// Acts as a controller connecting UI and Backend.
    /// </summary>
    public static class GlobalConfig
    {
        public static string allowedFileTypes = "Text documents (.csv)|*.csv";

        public static string machinedDataFile = "MachineData.csv";
        public static string groups_functions_file { get; set; } = "Groups_Functions.csv";
        public static string UsersFile { get; set; } = "Users.csv";
        public static IUserAdministrationDataConnection userAdministrationConnection { get; set; }
        public static IMachineDataDataConnection machineDataConnection { get; set; }
        public static IProgramParameterDataConnection programParameterConnection { get; set; }



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
            if (!Directory.Exists($"{ ConfigurationManager.AppSettings["userFilesPath"] }"))
            {
                Directory.CreateDirectory($"{ ConfigurationManager.AppSettings["userFilesPath"] }");
            }
            UserAdministrationTextConnector conn = new UserAdministrationTextConnector();
            MachineDataTextConnector mdConn = new MachineDataTextConnector();
            ProgramParameterTextConnector textCon = new ProgramParameterTextConnector();
            userAdministrationConnection = conn;
            machineDataConnection = mdConn;
            programParameterConnection = textCon;
        }

        

        public static void LoadAllData()
        {
            LoadMachineData();
            UserAdministrationGlobalConfig.uAdmin_FunctionsList = userAdministrationConnection.LoadFunctions();
            UserAdministrationGlobalConfig.uAdmin_GroupsList = userAdministrationConnection.LoadGroups();
            UserAdministrationGlobalConfig.uAdmin_UsersList = userAdministrationConnection.LoadUsers();
            UserTemplate.GroupsList = UserAdministrationGlobalConfig.uAdmin_GroupsList;
        }

        public static void LoadMachineData()
        {
            if(machineDataConnection.CheckMachineDataFile() == true)
            {
                MachineDataGlobalConfig.machineDataObject = machineDataConnection.LoadMachineDataFile();
                MachineDataGlobalConfig.isMachineDataFileThere = true;
            }
            else
            {
                MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageHigh = 0;
                MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageHigh = 0;
                MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsHigh = 0;
                MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentHigh = 0;
                MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentHigh = 0;
                MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentHigh = 0;
                MachineDataGlobalConfig.machineDataObject.forwardTestCurrentHigh = 0;
                MachineDataGlobalConfig.machineDataObject.reverseTestVoltageHigh = 0;
                MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageHigh = 0;
                MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceHigh = 0;
                MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceHigh = 0;
                MachineDataGlobalConfig.machineDataObject.contactResistanceHigh = 0;

                MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageLow = 0;
                MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageLow = 0;
                MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsLow = 0;
                MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentLow = 0;
                MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentLow = 0;
                MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentLow = 0;
                MachineDataGlobalConfig.machineDataObject.forwardTestCurrentLow = 0;
                MachineDataGlobalConfig.machineDataObject.reverseTestVoltageLow = 0;
                MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageLow = 0;
                MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceLow = 0;
                MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceLow = 0;
                MachineDataGlobalConfig.machineDataObject.contactResistanceLow = 0;
            }
        }
    }
}
