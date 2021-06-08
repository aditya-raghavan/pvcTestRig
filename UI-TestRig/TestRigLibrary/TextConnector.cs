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
        /// returns a model object with loaded values from its file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public TestConfigurationTemplate LoadModel(string fileName)
        {
            TestConfigurationTemplate model = new TestConfigurationTemplate();
            model = fileName.FullFilePath().LoadFile().ConvertToModelTemplate();
            return model;
        }

        public TestConfigurationTemplate LoadMachineDataFile()
        {
            string fileName = GlobalConfig.machinedDataFile;
            TestConfigurationTemplate model = new TestConfigurationTemplate();
            model = fileName.FullMachineDataPath().LoadFile().ConvertToMachineDataModel();
            return model;
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
        /// Saves a model to its text file.
        /// </summary>
        /// <param name="model">Model object containing information about the model</param>
        public void SaveModel(TestConfigurationTemplate model)
        {
            //TextFile saving format.
            //modelname,diodecode,customercode,additionalcode,diodeindex,barcodeindex,postolvoltage,negtolvol,NFDV,postolcur,negtolcur,NRC,fortestcurrent,revtestvoltage,formaxvoltage,
            //pottolres,negtolres,contactres

            List<string> lines = new List<string>();

            string fileName = model.modelName+".csv";

            lines.Add($"MODEL NAME,{model.modelName}");
            lines.Add($"DIODE CODE,{model.diodeCode}");
            lines.Add($"CUSTOMER CODE,{model.customerCode}");
            lines.Add($"ADDITIONAL CODE,{model.additionalCode}");
            lines.Add($"DIODE TYPE,{model.diodeType}");
            lines.Add($"BAR CODE OPTION,{model.barCodeOption}");

            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE,{model.positiveTolerenceVoltage.ToString("N3")},mV");
            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE,{model.negativeTolerenceVoltage.ToString("N3")},mV");
            lines.Add($"NOMINAL FORWARD DROP VOLTAGE,{model.nominalForwardDropVolts.ToString("N3")},mV");
            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT,{model.positiveTolerenceCurrent.ToString("N3")},uA");
            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT,{model.negativeTolerenceCurrent.ToString("N3")},uA");
            lines.Add($"NOMINAL REVERSE CURRENT,{model.nominalReverseCurrent.ToString("N3")},uA");
            lines.Add($"FORWARD TEST CURRENT,{model.forwardTestCurrent.ToString("N3")},A");
            lines.Add($"REVERSE TEST VOLTAGE,{model.reverseTestVoltage.ToString("N3")},V");
            lines.Add($"FORWARD MAX VOLTAGE,{model.forwardMaxVoltage.ToString("N3")},V");
            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE,{model.positiveTolerenceResistance.ToString("N3")},Ohms");
            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE,{model.negativeTolerenceResistance.ToString("N3")},Ohms");
            lines.Add($"CONTACT RESISTANCE,{model.contactResistance.ToString("N3")},Ohms");


            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public void SaveMachineData()
        {
            List<string> lines = new List<string>();
            string fileName = "MachineData.csv";

            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataModel.positiveTolerenceVoltageHigh.ToString("N3")},mV");
            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE LOW LIMIT,{GlobalConfig.machineDataModel.positiveTolerenceVoltageLow.ToString("N3")},mV");

            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataModel.negativeTolerenceVoltageHigh.ToString("N3")},mV");
            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE LOW LIMIT,{GlobalConfig.machineDataModel.negativeTolerenceVoltageLow.ToString("N3")},mV");

            lines.Add($"NOMINAL FORWARD DROP VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataModel.nominalForwardDropVoltsHigh.ToString("N3")},mV");
            lines.Add($"NOMINAL FORWARD DROP VOLTAGE LOW LIMIT,{GlobalConfig.machineDataModel.nominalForwardDropVoltsLow.ToString("N3")},mV");

            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT HIGH LIMIT,{GlobalConfig.machineDataModel.positiveTolerenceCurrentHigh.ToString("N3")},uA");
            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT LOW LIMIT,{GlobalConfig.machineDataModel.positiveTolerenceCurrentLow.ToString("N3")},uA");

            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT HIGH LIMIT,{GlobalConfig.machineDataModel.negativeTolerenceCurrentHigh.ToString("N3")},uA");
            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT LOW LIMIT,{GlobalConfig.machineDataModel.negativeTolerenceCurrentLow.ToString("N3")},uA");

            lines.Add($"NOMINAL REVERSE CURRENT HIGH LIMIT,{GlobalConfig.machineDataModel.nominalReverseCurrentHigh.ToString("N3")},uA");
            lines.Add($"NOMINAL REVERSE CURRENT LOW LIMIT,{GlobalConfig.machineDataModel.nominalReverseCurrentLow.ToString("N3")},uA");

            lines.Add($"FORWARD TEST CURRENT HIGH LIMIT,{GlobalConfig.machineDataModel.forwardTestCurrentHigh.ToString("N3")},A");
            lines.Add($"FORWARD TEST CURRENT LOW LIMIT,{GlobalConfig.machineDataModel.forwardTestCurrentLow.ToString("N3")},A");

            lines.Add($"REVERSE TEST VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataModel.reverseTestVoltageHigh.ToString("N3")},V");
            lines.Add($"REVERSE TEST VOLTAGE LOW LIMIT,{GlobalConfig.machineDataModel.reverseTestVoltageLow.ToString("N3")},V");

            lines.Add($"FORWARD MAX VOLTAGE HIGH LIMIT,{GlobalConfig.machineDataModel.forwardMaxVoltageHigh.ToString("N3")},V");
            lines.Add($"FORWARD MAX VOLTAGE LOW LIMIT,{GlobalConfig.machineDataModel.forwardMaxVoltageLow.ToString("N3")},V");

            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT,{GlobalConfig.machineDataModel.positiveTolerenceResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT,{GlobalConfig.machineDataModel.positiveTolerenceResistanceLow.ToString("N3")},Ohms");

            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT,{GlobalConfig.machineDataModel.negativeTolerenceResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT,{GlobalConfig.machineDataModel.negativeTolerenceResistanceLow.ToString("N3")},Ohms");

            lines.Add($"CONTACT RESISTANCE HIGH LIMIT,{GlobalConfig.machineDataModel.contactResistanceHigh.ToString("N3")},Ohms");
            lines.Add($"CONTACT RESISTANCE LOW LIMIT,{GlobalConfig.machineDataModel.contactResistanceLow.ToString("N3")},Ohms");

            File.WriteAllLines(fileName.FullMachineDataPath(), lines);

        }

        /// <summary>
        /// Reads all the existing models.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllModelNames()
        {
            
            string rootPath = $"{ ConfigurationManager.AppSettings["filePath"] }";
            List<string> ModelNames = new List<string>();

            var files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);

            foreach(string file in files)
            {
                ModelNames.Add(Path.GetFileNameWithoutExtension(file));
            }

            return ModelNames;
            
        }

        /// <summary>
        /// Deletes Model File.
        /// </summary>
        /// <param name="modelName"></param>
        public void DeleteModel(string modelName)
        {
            string fileName = modelName + ".csv";
            File.Delete(fileName.FullFilePath());
        }


    }
}
