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
    /// Utility class for text connector class.
    /// </summary>
    public static class TextConnectorProcessor
    {
        /// <summary>
        /// Returns full file path for a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FullFilePath(this string fileName)
        {
            
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{fileName}";

        }

        /// <summary>
        /// Loads content from a file path.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return "";
            }
            return File.ReadAllText(file);

        }

        /// <summary>
        /// returns a populated ModelTemplate Object with values read from the specified model file.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static ModelTemplate ConvertToModelTemplate(this string line)
        {
            ModelTemplate output = new ModelTemplate();
            TypeInformationTemplate ti = new TypeInformationTemplate();
            ReadingsTemplate rt = new ReadingsTemplate();

            if(line.Length != 0)
            {
                string[] cols = line.Split(',');

                output.Name = cols[0];
                ti.DiodeCode = cols[1];
                ti.CustomerCode = cols[2];
                ti.AdditionalCode = cols[3];
                ti.DiodeIndex = int.Parse(cols[4]);
                ti.BarCodeIndex = int.Parse(cols[5]);

                rt.PositiveTolerenceVoltage = decimal.Parse(cols[6]);
                rt.NegativeTolerenceVoltage = decimal.Parse(cols[7]);
                rt.NominalForwardDropVolts = decimal.Parse(cols[8]);
                rt.PositiveTolerenceCurrent = decimal.Parse(cols[9]);
                rt.NegativeTolerenceCurrent = decimal.Parse(cols[10]);
                rt.NominalReverseCurrent = decimal.Parse(cols[11]);
                rt.ForwardTestCurrent = decimal.Parse(cols[12]);
                rt.ReverseTestVoltage = decimal.Parse(cols[13]);
                rt.ForwardMaxVoltage = decimal.Parse(cols[14]);
                rt.PositiveTolerenceResistance = decimal.Parse(cols[15]);
                rt.NegativeTolerenceResistance = decimal.Parse(cols[16]);
                rt.ContactResistance = decimal.Parse(cols[17]);

                output.TypeInformation = ti;
                output.ModelReadings = rt;
            }

            return output;

        }

        
    }
}
