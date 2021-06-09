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
    /// Data connection class in TEXT FORMAT.
    /// </summary>
    public class TextConnector : IDataConnection
    {
        /// <summary>
        /// returns a test config object with loaded values from its file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public TestConfigurationTemplate LoadTestConfigurationFromFile(string fileName)
        {
            TestConfigurationTemplate template = new TestConfigurationTemplate();
            template = fileName.FullFilePath().LoadFile().ConvertToTestConfigurationTemplate();
            return template;
        }

        public TestConfigurationTemplate LoadMachineDataFile()
        {
            string fileName = GlobalConfig.machinedDataFile;
            TestConfigurationTemplate template = new TestConfigurationTemplate();
            template = fileName.FullMachineDataPath().LoadFile().ConvertTomachineDataObject();
            return template;
        }

        public bool CheckMachineDataFile()
        {
            if (File.Exists(GlobalConfig.machinedDataFile.FullMachineDataPath()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Saves a test config to its text file.
        /// </summary>
        /// <param name="test config">template object containing information about the test config</param>
        public void SaveTestConfigurationToFile(TestConfigurationTemplate template)
        {
            //TextFile saving format.
            //testconfigname,diodecode,customercode,additionalcode,diodeindex,barcodeindex,postolvoltage,negtolvol,NFDV,postolcur,negtolcur,NRC,fortestcurrent,revtestvoltage,formaxvoltage,
            //pottolres,negtolres,contactres

            List<string> lines = new List<string>();

            string fileName = template.testConfigurationName+".csv";

            lines.Add($"TEST CONFIGURATION NAME,{template.testConfigurationName}");
            lines.Add($"DIODE CODE,{template.diodeCode}");
            lines.Add($"CUSTOMER CODE,{template.customerCode}");
            lines.Add($"ADDITIONAL CODE,{template.additionalCode}");
            lines.Add($"DIODE TYPE,{template.diodeType}");
            lines.Add($"BAR CODE OPTION,{template.barCodeOption}");

            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE,{template.positiveTolerenceVoltage.ToString("N3")},mV");
            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE,{template.negativeTolerenceVoltage.ToString("N3")},mV");
            lines.Add($"NOMINAL FORWARD DROP VOLTAGE,{template.nominalForwardDropVolts.ToString("N3")},mV");
            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT,{template.positiveTolerenceCurrent.ToString("N3")},uA");
            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT,{template.negativeTolerenceCurrent.ToString("N3")},uA");
            lines.Add($"NOMINAL REVERSE CURRENT,{template.nominalReverseCurrent.ToString("N3")},uA");
            lines.Add($"FORWARD TEST CURRENT,{template.forwardTestCurrent.ToString("N3")},A");
            lines.Add($"REVERSE TEST VOLTAGE,{template.reverseTestVoltage.ToString("N3")},V");
            lines.Add($"FORWARD MAX VOLTAGE,{template.forwardMaxVoltage.ToString("N3")},V");
            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE,{template.positiveTolerenceResistance.ToString("N3")},Ohms");
            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE,{template.negativeTolerenceResistance.ToString("N3")},Ohms");
            lines.Add($"CONTACT RESISTANCE,{template.contactResistance.ToString("N3")},Ohms");


            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public void SaveMachineData()
        {
            List<string> lines = new List<string>();
            string fileName = "MachineData.csv";

            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataObject.positiveTolerenceVoltageHigh.ToString("N3")},mV");
            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE LOW LIMIT,{GlobalConfig.machineDataObject.positiveTolerenceVoltageLow.ToString("N3")},mV");

            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataObject.negativeTolerenceVoltageHigh.ToString("N3")},mV");
            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE LOW LIMIT,{GlobalConfig.machineDataObject.negativeTolerenceVoltageLow.ToString("N3")},mV");

            lines.Add($"NOMINAL FORWARD DROP VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataObject.nominalForwardDropVoltsHigh.ToString("N3")},mV");
            lines.Add($"NOMINAL FORWARD DROP VOLTAGE LOW LIMIT,{GlobalConfig.machineDataObject.nominalForwardDropVoltsLow.ToString("N3")},mV");

            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT HIGH LIMIT,{GlobalConfig.machineDataObject.positiveTolerenceCurrentHigh.ToString("N3")},uA");
            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT LOW LIMIT,{GlobalConfig.machineDataObject.positiveTolerenceCurrentLow.ToString("N3")},uA");

            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT HIGH LIMIT,{GlobalConfig.machineDataObject.negativeTolerenceCurrentHigh.ToString("N3")},uA");
            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT LOW LIMIT,{GlobalConfig.machineDataObject.negativeTolerenceCurrentLow.ToString("N3")},uA");

            lines.Add($"NOMINAL REVERSE CURRENT HIGH LIMIT,{GlobalConfig.machineDataObject.nominalReverseCurrentHigh.ToString("N3")},uA");
            lines.Add($"NOMINAL REVERSE CURRENT LOW LIMIT,{GlobalConfig.machineDataObject.nominalReverseCurrentLow.ToString("N3")},uA");

            lines.Add($"FORWARD TEST CURRENT HIGH LIMIT,{GlobalConfig.machineDataObject.forwardTestCurrentHigh.ToString("N3")},A");
            lines.Add($"FORWARD TEST CURRENT LOW LIMIT,{GlobalConfig.machineDataObject.forwardTestCurrentLow.ToString("N3")},A");

            lines.Add($"REVERSE TEST VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataObject.reverseTestVoltageHigh.ToString("N3")},V");
            lines.Add($"REVERSE TEST VOLTAGE LOW LIMIT,{GlobalConfig.machineDataObject.reverseTestVoltageLow.ToString("N3")},V");

            lines.Add($"FORWARD MAX VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataObject.forwardMaxVoltageHigh.ToString("N3")},V");
            lines.Add($"FORWARD MAX VOLTAGE LOW LIMIT,{GlobalConfig.machineDataObject.forwardMaxVoltageLow.ToString("N3")},V");

            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT,{GlobalConfig.machineDataObject.positiveTolerenceResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT,{GlobalConfig.machineDataObject.positiveTolerenceResistanceLow.ToString("N3")},Ohms");

            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT,{GlobalConfig.machineDataObject.negativeTolerenceResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT,{GlobalConfig.machineDataObject.negativeTolerenceResistanceLow.ToString("N3")},Ohms");

            lines.Add($"CONTACT RESISTANCE HIGH LIMIT,{GlobalConfig.machineDataObject.contactResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"CONTACT RESISTANCE LOW LIMIT,{GlobalConfig.machineDataObject.contactResistanceLow.ToString("N3")},Ohms");

            File.WriteAllLines(fileName.FullMachineDataPath(), lines);

        }

        

        /// <summary>
        /// Deletes Test Configuration File.
        /// </summary>
        /// <param name="testConfigurationName"></param>
        public void DeleteTestConfiguration(string testConfigurationName)
        {
            string fileName = testConfigurationName + ".csv";
            File.Delete(fileName.FullFilePath());
        }


    }
}
