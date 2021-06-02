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

            string fileName = model.Name+".csv";

            lines.Add($"{model.Name}");
            lines.Add($"DIODE CODE,{model.DiodeCode}");
            lines.Add($"CUSTOMER CODE,{model.CustomerCode}");
            lines.Add($"ADDITIONAL CODE,{model.AdditionalCode}");
            lines.Add($"DIODE TYPE,{model.DiodeType}");
            lines.Add($"BAR CODE OPTION,{model.BarCodeOption}");

            lines.Add($"POSITIVE TOLERENCE VOLTAGE,{model.PositiveTolerenceVoltage},mV");
            lines.Add($"NEGATIVE TOLERENCE VOLTAGE,{model.NegativeTolerenceVoltage},mV");
            lines.Add($"NOMINAL FORWARD DROP VOLTAGE,{model.NominalForwardDropVolts},mV");
            lines.Add($"POSTIVIE TOLERENCE CURRENT,{model.PositiveTolerenceCurrent},uA");
            lines.Add($"NEGATIVE TOLERENCE CURRENT,{model.NegativeTolerenceCurrent},uA");
            lines.Add($"NOMINAL REVERSE CURRENT,{model.NominalReverseCurrent},uA");
            lines.Add($"FORWARD TEST CURRENT,{model.ForwardTestCurrent},A");
            lines.Add($"REVERSE TEST VOLTAGE,{model.ReverseTestVoltage},V");
            lines.Add($"FORWARD MAX VOLTAGE,{model.ForwardMaxVoltage},V");
            lines.Add($"POSITIVE TOLERENCE RESISTANCE,{model.PositiveTolerenceResistance},Ohms");
            lines.Add($"NEGATIVE TOLERENCE RESISTANCE,{model.NegativeTolerenceResistance},Ohms");
            lines.Add($"CONTACT RESISTANCE,{model.ContactResistance},Ohms");


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
