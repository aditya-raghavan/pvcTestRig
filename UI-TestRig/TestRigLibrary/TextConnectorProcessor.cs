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


        static List<string> fileFormat = new List<string>()
        {
            "DIODE CODE","CUSTOMER CODE","ADDITIONAL CODE","DIODE TYPE","BAR CODE OPTION",
            "POSITIVE TOLERENCE VOLTAGE","mV",
            "NEGATIVE TOLERENCE VOLTAGE","mV",
            "NOMINAL FORWARD DROP VOLTAGE","mV",
            "POSTIVIE TOLERENCE CURRENT","uA",
            "NEGATIVE TOLERENCE CURRENT","uA",
            "NOMINAL REVERSE CURRENT","uA",
            "FORWARD TEST CURRENT","A",
            "REVERSE TEST VOLTAGE","V",
            "FORWARD MAX VOLTAGE","V",
            "POSITIVE TOLERENCE RESISTANCE","Ohms",
            "NEGATIVE TOLERENCE RESISTANCE","Ohms",
            "CONTACT RESISTANCE","Ohms",
            
        };

        
        
        
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
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }
            return File.ReadAllLines(file).ToList();

        }

        /// <summary>
        /// returns a populated ModelTemplate Object with values read from the specified model file.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static TestConfigurationTemplate ConvertToModelTemplate(this List<string> lines)
        {
            TestConfigurationTemplate template = new TestConfigurationTemplate();
            

            string line = ExtractValuesFromModelFile(lines);
            if(line == null)
            {
                return null;
            }

            if(line.Length != 0)
            {
                string[] cols = line.Split(',');

                template.Name = cols[0];
                template.DiodeCode = cols[1];
                template.CustomerCode = cols[2];
                template.AdditionalCode = cols[3];
                template.DiodeType = cols[4];
                template.BarCodeOption = cols[5];

                template.PositiveTolerenceVoltage = double.Parse(cols[6]);
                template.NegativeTolerenceVoltage = double.Parse(cols[7]);
                template.NominalForwardDropVolts = double.Parse(cols[8]);
                template.PositiveTolerenceCurrent = double.Parse(cols[9]);
                template.NegativeTolerenceCurrent = double.Parse(cols[10]);
                template.NominalReverseCurrent = double.Parse(cols[11]);
                template.ForwardTestCurrent = double.Parse(cols[12]);
                template.ReverseTestVoltage = double.Parse(cols[13]);
                template.ForwardMaxVoltage = double.Parse(cols[14]);
                template.PositiveTolerenceResistance = double.Parse(cols[15]);
                template.NegativeTolerenceResistance = double.Parse(cols[16]);
                template.ContactResistance = double.Parse(cols[17]);

                
            }

            return template;

        }

        private static bool ValidateFile(List<string> lines)
        {

            bool result = true;
            if (lines.Count == 0)
            {
                return false;

            }
            string index = "Name";
            int typeinfoIndex = 0;
            int readingsIndex = 5;
            double x;
            foreach(string line in lines)
            {
                string[] cols = line.Split(',');
                if (String.Equals(index,"Name"))
                {
                    if (cols.Length != 1)
                    {
                        return false;
                    }

                }
                if(string.Equals(index, "TypeInfo"))
                {
                    if(cols.Length != 2)
                    {
                        return false;
                    }
                    else if(cols[0] != fileFormat[typeinfoIndex] || string.IsNullOrEmpty(cols[1]))
                    {
                        return false;
                    }
                    if(typeinfoIndex == 3)
                    {
                        List<string> diodeTypes = TestConfigurationTemplate.GetDiodeTypes();
                        if(!diodeTypes.Contains(cols[1]))
                        {
                            return false;
                        }
                    }
                    if (typeinfoIndex == 4)
                    {
                        List<string> barCodeOptions = TestConfigurationTemplate.GetBarcodeOptions();
                        if (!barCodeOptions.Contains(cols[1]))
                        {
                            return false;
                        }
                    }
                    typeinfoIndex++;
                }
                if(index == "Readings")
                {
                    if(cols.Length != 3)
                    {
                        return false;
                    }
                    if(cols[0] != fileFormat[readingsIndex] || cols[2] != fileFormat[readingsIndex + 1] || double.TryParse(cols[1],out x) == false)
                    {
                        return false;
                    }
                    readingsIndex = readingsIndex + 2;
                }
                if(index == "Name")
                {
                    index = "TypeInfo";
                }
                if(typeinfoIndex == 5)
                {
                    index = "Readings";
                }
            }
            return result;
        }

        private static string ExtractValuesFromModelFile(List<string> lines)
        {
            if (!ValidateFile(lines))
            {
                return null;
            }

            string converted = "";
            int index = 0;
            foreach(string line in lines)
            {
                string[] cols = line.Split(',');
                converted += $"{cols[index]},";
                if(index == 0)
                {
                    index = 1;
                }
            }

            converted = converted.Substring(0, converted.Length - 1);
            return converted;
        }

        
    }
}
