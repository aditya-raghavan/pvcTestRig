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

            lines.Add($"{model.modelName}");
            lines.Add($"DIODE CODE,{model.diodeCode}");
            lines.Add($"CUSTOMER CODE,{model.customerCode}");
            lines.Add($"ADDITIONAL CODE,{model.additionalCode}");
            lines.Add($"DIODE TYPE,{model.diodeType}");
            lines.Add($"BAR CODE OPTION,{model.barCodeOption}");

            lines.Add($"POSITIVE TOLERENCE VOLTAGE,{model.positiveTolerenceVoltage},mV");
            lines.Add($"NEGATIVE TOLERENCE VOLTAGE,{model.negativeTolerenceVoltage},mV");
            lines.Add($"NOMINAL FORWARD DROP VOLTAGE,{model.nominalForwardDropVolts},mV");
            lines.Add($"POSTIVIE TOLERENCE CURRENT,{model.positiveTolerenceCurrent},uA");
            lines.Add($"NEGATIVE TOLERENCE CURRENT,{model.negativeTolerenceCurrent},uA");
            lines.Add($"NOMINAL REVERSE CURRENT,{model.nominalReverseCurrent},uA");
            lines.Add($"FORWARD TEST CURRENT,{model.forwardTestCurrent},A");
            lines.Add($"REVERSE TEST VOLTAGE,{model.reverseTestVoltage},V");
            lines.Add($"FORWARD MAX VOLTAGE,{model.forwardMaxVoltage},V");
            lines.Add($"POSITIVE TOLERENCE RESISTANCE,{model.positiveTolerenceResistance},Ohms");
            lines.Add($"NEGATIVE TOLERENCE RESISTANCE,{model.negativeTolerenceResistance},Ohms");
            lines.Add($"CONTACT RESISTANCE,{model.contactResistance},Ohms");


            File.WriteAllLines(fileName.FullFilePath(), lines);
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
