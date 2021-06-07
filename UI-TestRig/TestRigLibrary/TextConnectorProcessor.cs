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
        private static Dictionary<string,int> GetTypeIndexDict()
        {
            return new Dictionary<string, int>()
            {
            {"MODEL NAME", 0},
            {"DIODE CODE", 1},
            {"CUSTOMER CODE", 2},
            {"ADDITIONAL CODE", 3},
            {"DIODE TYPE", 4},
            {"BAR CODE OPTION",5 }
            };
        }
        private static Dictionary<string, int> GetReadingsIndexDict()
        {
            return new Dictionary<string, int>()
            {
            {"POSITIVE TOLERANCE DROP VOLTAGE", 6},
            {"NEGATIVE TOLERANCE DROP VOLTAGE", 7},
            {"NOMINAL FORWARD DROP VOLTAGE", 8},
            {"POSITIVE TOLERANCE REVERSE CURRENT", 9},
            {"NEGATIVE TOLERANCE REVERSE CURRENT", 10},
            {"NOMINAL REVERSE CURRENT", 11},
            {"FORWARD TEST CURRENT", 12},
            {"REVERSE TEST VOLTAGE", 13},
            {"FORWARD MAX VOLTAGE", 14},
            {"POSITIVE TOLERANCE CONTACT RESISTANCE", 15},
            {"NEGATIVE TOLERANCE CONTACT RESISTANCE", 16},
            {"CONTACT RESISTANCE", 17}
            };
        }
        private static Dictionary<string, string> GetUnitsDict()
        {
            return new Dictionary<string, string>()
            {
            {"POSITIVE TOLERANCE DROP VOLTAGE", "mV"},
            {"NEGATIVE TOLERANCE DROP VOLTAGE","mV"},
            {"NOMINAL FORWARD DROP VOLTAGE", "mV"},
            {"POSITIVE TOLERANCE REVERSE CURRENT", "uA"},
            {"NEGATIVE TOLERANCE REVERSE CURRENT", "uA"},
            {"NOMINAL REVERSE CURRENT", "uA"},
            {"FORWARD TEST CURRENT", "A"},
            {"REVERSE TEST VOLTAGE", "V"},
            {"FORWARD MAX VOLTAGE", "V"},
            {"POSITIVE TOLERANCE CONTACT RESISTANCE", "Ohms"},
            {"NEGATIVE TOLERANCE CONTACT RESISTANCE", "Ohms"},
            {"CONTACT RESISTANCE", "Ohms"}
            };
        }

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

                template.modelName = cols[0];
                template.diodeCode = cols[1];
                template.customerCode = cols[2];
                template.additionalCode = cols[3];
                template.diodeType = cols[4];
                template.barCodeOption = cols[5];

                template.positiveTolerenceVoltage = double.Parse(cols[6]);
                template.negativeTolerenceVoltage = double.Parse(cols[7]);
                template.nominalForwardDropVolts = double.Parse(cols[8]);
                template.positiveTolerenceCurrent = double.Parse(cols[9]);
                template.negativeTolerenceCurrent = double.Parse(cols[10]);
                template.nominalReverseCurrent = double.Parse(cols[11]);
                template.forwardTestCurrent = double.Parse(cols[12]);
                template.reverseTestVoltage = double.Parse(cols[13]);
                template.forwardMaxVoltage = double.Parse(cols[14]);
                template.positiveTolerenceResistance = double.Parse(cols[15]);
                template.negativeTolerenceResistance = double.Parse(cols[16]);
                template.contactResistance = double.Parse(cols[17]);

                
            }

            return template;

        }

      

        private static string ExtractValuesFromModelFile(List<string> lines)
        {
            string[] valuesArray = new string[18];
            string formattedString = "";
            Dictionary<string, int> typeInfoIndex = GetTypeIndexDict();

            Dictionary<string, int> readingsIndex = GetReadingsIndexDict();

            Dictionary<string, string> units = GetUnitsDict();
            


            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                for(int i =0; i < cols.Length; i++)
                {
                    cols[i] = cols[i].Trim();
                }
                if (typeInfoIndex.ContainsKey(cols[0]))
                {
                    if (cols[0] == "DIODE TYPE")
                    {
                        if (TestConfigurationTemplate.DiodeTypes.Contains(cols[1]))
                        {
                            valuesArray[typeInfoIndex[cols[0]]] = cols[1];
                            typeInfoIndex.Remove(cols[0]);
                        }
                    }
                    else if (cols[0] == "BAR CODE OPTION")
                    {
                        if (TestConfigurationTemplate.BarCodeOptions.Contains(cols[1]))
                        {
                            valuesArray[typeInfoIndex[cols[0]]] = cols[1];
                            typeInfoIndex.Remove(cols[0]);
                        }
                    }
                    else
                    {
                        if (cols[1].Trim().Length != 0)
                        {
                            valuesArray[typeInfoIndex[cols[0]]] = cols[1];
                            typeInfoIndex.Remove(cols[0]);
                        }
                    }

                }
                else if (readingsIndex.ContainsKey(cols[0]))
                {
                    if (units[cols[0]] == cols[2] && double.TryParse(cols[1], out double x))
                    {
                        valuesArray[readingsIndex[cols[0]]] = cols[1];
                        readingsIndex.Remove(cols[0]);
                    }
                }
            }
            if (typeInfoIndex.Count == 0 && readingsIndex.Count == 0)
            {
                foreach(string value in valuesArray)
                {
                    formattedString += $"{value},";
                }
                formattedString = formattedString.Substring(0, formattedString.Length - 1);
                return formattedString;
            }
            else
            {
                return null;
            }


        }

        
    }
}
