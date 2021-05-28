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
        public ModelTemplate LoadModel(string fileName)
        {
            ModelTemplate model = new ModelTemplate();
            model = fileName.FullFilePath().LoadFile().ConvertToModelTemplate();
            return model;
        }

        /// <summary>
        /// Saves a model to its text file.
        /// </summary>
        /// <param name="model">Model object containing information about the model</param>
        public void SaveModel(ModelTemplate model)
        {
            //TextFile saving format.
            //modelname,diodecode,customercode,additionalcode,diodeindex,barcodeindex,postolvoltage,negtolvol,NFDV,postolcur,negtolcur,NRC,fortestcurrent,revtestvoltage,formaxvoltage,pottolres,negtolres,contactres

            List<string> lines = new List<string>();

            string fileName = model.Name+".csv";

            lines.Add($"{model.Name},{model.TypeInformation.DiodeCode},{model.TypeInformation.CustomerCode},{model.TypeInformation.AdditionalCode},{model.TypeInformation.DiodeIndex},{model.TypeInformation.BarCodeIndex}," +
                $"{model.ModelReadings.PositiveTolerenceVoltage},{model.ModelReadings.NegativeTolerenceVoltage},{model.ModelReadings.NominalForwardDropVolts}," +
                $"{model.ModelReadings.PositiveTolerenceCurrent},{model.ModelReadings.NegativeTolerenceCurrent},{model.ModelReadings.NominalReverseCurrent}," +
                $"{model.ModelReadings.ForwardTestCurrent},{model.ModelReadings.ReverseTestVoltage},{model.ModelReadings.ForwardMaxVoltage}," +
                $"{model.ModelReadings.PositiveTolerenceResistance},{model.ModelReadings.NegativeTolerenceResistance},{model.ModelReadings.ContactResistance}");

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
