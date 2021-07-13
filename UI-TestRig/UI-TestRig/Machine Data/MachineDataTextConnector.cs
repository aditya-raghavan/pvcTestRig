using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UI_TestRig
{
    public class MachineDataTextConnector : IMachineDataDataConnection
    {
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

        public void SaveMachineData()
        {
            List<string> lines = new List<string>();
            string fileName = "MachineData.csv";

            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageHigh.ToString("N3")},mV");
            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageLow.ToString("N3")},mV");

            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageHigh.ToString("N3")},mV");
            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageLow.ToString("N3")},mV");

            lines.Add($"NOMINAL FORWARD DROP VOLTAGE HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsHigh.ToString("N3")},mV");
            lines.Add($"NOMINAL FORWARD DROP VOLTAGE LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsLow.ToString("N3")},mV");

            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentHigh.ToString("N3")},uA");
            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentLow.ToString("N3")},uA");

            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentHigh.ToString("N3")},uA");
            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentLow.ToString("N3")},uA");

            lines.Add($"NOMINAL REVERSE CURRENT HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentHigh.ToString("N3")},uA");
            lines.Add($"NOMINAL REVERSE CURRENT LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentLow.ToString("N3")},uA");

            lines.Add($"FORWARD TEST CURRENT HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.forwardTestCurrentHigh.ToString("N3")},A");
            lines.Add($"FORWARD TEST CURRENT LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.forwardTestCurrentLow.ToString("N3")},A");

            lines.Add($"REVERSE TEST VOLTAGE HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.reverseTestVoltageHigh.ToString("N3")},V");
            lines.Add($"REVERSE TEST VOLTAGE LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.reverseTestVoltageLow.ToString("N3")},V");

            lines.Add($"FORWARD MAX VOLTAGE HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageHigh.ToString("N3")},V");
            lines.Add($"FORWARD MAX VOLTAGE LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageLow.ToString("N3")},V");

            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceLow.ToString("N3")},Ohms");

            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceLow.ToString("N3")},Ohms");

            lines.Add($"CONTACT RESISTANCE HIGH LIMIT,{MachineDataGlobalConfig.machineDataObject.contactResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"CONTACT RESISTANCE LOW LIMIT,{MachineDataGlobalConfig.machineDataObject.contactResistanceLow.ToString("N3")},Ohms");

            File.WriteAllLines(fileName.FullMachineDataPath(), lines);

        }
    }
}
