using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UI_TestRig
{


    public static class MachineDataTextConnectorProcessor
    {
        private static Dictionary<string, int> GetMachineDataReadings()
        {
            return new Dictionary<string, int>()
            {
            {"POSITIVE TOLERANCE DROP VOLTAGE HIGH LIMIT", 0},
            {"POSITIVE TOLERANCE DROP VOLTAGE LOW LIMIT", 1},
            {"NEGATIVE TOLERANCE DROP VOLTAGE HIGH LIMIT", 2},
            {"NEGATIVE TOLERANCE DROP VOLTAGE LOW LIMIT", 3},
            {"NOMINAL FORWARD DROP VOLTAGE HIGH LIMIT", 4},
            {"NOMINAL FORWARD DROP VOLTAGE LOW LIMIT", 5},
            {"POSITIVE TOLERANCE REVERSE CURRENT HIGH LIMIT", 6},
            {"POSITIVE TOLERANCE REVERSE CURRENT LOW LIMIT", 7},
            {"NEGATIVE TOLERANCE REVERSE CURRENT HIGH LIMIT", 8},
            {"NEGATIVE TOLERANCE REVERSE CURRENT LOW LIMIT", 9},
            {"NOMINAL REVERSE CURRENT HIGH LIMIT", 10},
            {"NOMINAL REVERSE CURRENT LOW LIMIT", 11},
            {"FORWARD TEST CURRENT HIGH LIMIT", 12},
            {"FORWARD TEST CURRENT LOW LIMIT", 13},
            {"REVERSE TEST VOLTAGE HIGH LIMIT", 14},
            {"REVERSE TEST VOLTAGE LOW LIMIT", 15},
            {"FORWARD MAX VOLTAGE HIGH LIMIT", 16},
            {"FORWARD MAX VOLTAGE LOW LIMIT", 17},
            {"POSITIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT", 18},
            {"POSITIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT", 19},
            {"NEGATIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT", 20},
            {"NEGATIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT", 21},
            {"CONTACT RESISTANCE HIGH LIMIT", 22},
            {"CONTACT RESISTANCE LOW LIMIT", 23}
            };
        }
        private static Dictionary<string, string> GetMachineDataUnits()
        {
            return new Dictionary<string, string>()
            {
            {"POSITIVE TOLERANCE DROP VOLTAGE HIGH LIMIT", "mV"},
            {"POSITIVE TOLERANCE DROP VOLTAGE LOW LIMIT", "mV"},
            {"NEGATIVE TOLERANCE DROP VOLTAGE HIGH LIMIT", "mV"},
            {"NEGATIVE TOLERANCE DROP VOLTAGE LOW LIMIT", "mV"},
            {"NOMINAL FORWARD DROP VOLTAGE HIGH LIMIT", "mV"},
            {"NOMINAL FORWARD DROP VOLTAGE LOW LIMIT", "mV"},
            {"POSITIVE TOLERANCE REVERSE CURRENT HIGH LIMIT","uA"},
            {"POSITIVE TOLERANCE REVERSE CURRENT LOW LIMIT","uA"},
            {"NEGATIVE TOLERANCE REVERSE CURRENT HIGH LIMIT","uA"},
            {"NEGATIVE TOLERANCE REVERSE CURRENT LOW LIMIT","uA"},
            {"NOMINAL REVERSE CURRENT HIGH LIMIT", "uA"},
            {"NOMINAL REVERSE CURRENT LOW LIMIT", "uA"},
            {"FORWARD TEST CURRENT HIGH LIMIT", "A"},
            {"FORWARD TEST CURRENT LOW LIMIT", "A"},
            {"REVERSE TEST VOLTAGE HIGH LIMIT", "V"},
            {"REVERSE TEST VOLTAGE LOW LIMIT", "V"},
            {"FORWARD MAX VOLTAGE HIGH LIMIT", "V"},
            {"FORWARD MAX VOLTAGE LOW LIMIT", "V"},
            {"POSITIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT", "Ohms"},
            {"POSITIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT", "Ohms"},
            {"NEGATIVE TOLERANCE CONTACT RESISTANCE HIGH LIMIT", "Ohms"},
            {"NEGATIVE TOLERANCE CONTACT RESISTANCE LOW LIMIT", "Ohms"},
            {"CONTACT RESISTANCE HIGH LIMIT", "Ohms"},
            {"CONTACT RESISTANCE LOW LIMIT", "Ohms"}
            };
        }

        public static string FullMachineDataPath(this string fileName)
        {

            return $"{ ConfigurationManager.AppSettings["machinedataPath"] }\\{fileName}";

        }

        public static TestConfigurationTemplate ConvertTomachineDataObject(this List<string> lines)
        {
            TestConfigurationTemplate template = new TestConfigurationTemplate();

            string line = ExtractValuesFromMachineDataFile(lines);
            string[] cols = line.Split(',');

            template.positiveTolerenceVoltageHigh = double.Parse(cols[0]);
            template.negativeTolerenceVoltageHigh = double.Parse(cols[2]);
            template.nominalForwardDropVoltsHigh = double.Parse(cols[4]);
            template.positiveTolerenceCurrentHigh = double.Parse(cols[6]);
            template.negativeTolerenceCurrentHigh = double.Parse(cols[8]);
            template.nominalReverseCurrentHigh = double.Parse(cols[10]);
            template.forwardTestCurrentHigh = double.Parse(cols[12]);
            template.reverseTestVoltageHigh = double.Parse(cols[14]);
            template.forwardMaxVoltageHigh = double.Parse(cols[16]);
            template.positiveTolerenceResistanceHigh = double.Parse(cols[18]);
            template.negativeTolerenceResistanceHigh = double.Parse(cols[20]);
            template.contactResistanceHigh = double.Parse(cols[22]);

            template.positiveTolerenceVoltageLow = double.Parse(cols[1]);
            template.negativeTolerenceVoltageLow = double.Parse(cols[3]);
            template.nominalForwardDropVoltsLow = double.Parse(cols[5]);
            template.positiveTolerenceCurrentLow = double.Parse(cols[7]);
            template.negativeTolerenceCurrentLow = double.Parse(cols[9]);
            template.nominalReverseCurrentLow = double.Parse(cols[11]);
            template.forwardTestCurrentLow = double.Parse(cols[13]);
            template.reverseTestVoltageLow = double.Parse(cols[15]);
            template.forwardMaxVoltageLow = double.Parse(cols[17]);
            template.positiveTolerenceResistanceLow = double.Parse(cols[19]);
            template.negativeTolerenceResistanceLow = double.Parse(cols[21]);
            template.contactResistanceLow = double.Parse(cols[23]);


            return template;
        }

        private static string ExtractValuesFromMachineDataFile(List<string> lines)
        {
            string[] valuesArray = new string[24];
            string formattedString = "";
            Dictionary<string, int> readingsIndex = GetMachineDataReadings();
            Dictionary<string, string> units = GetMachineDataUnits();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                for (int i = 0; i < cols.Length; i++)
                {
                    cols[i] = cols[i].Trim();
                }
                if (readingsIndex.ContainsKey(cols[0].ToUpper()))
                {
                    cols[0] = cols[0].ToUpper();
                    if (cols.Length == 3)
                    {
                        if (units[cols[0]] == cols[2] && double.TryParse(cols[1], out double x))
                        {
                            valuesArray[readingsIndex[cols[0]]] = cols[1];
                            readingsIndex.Remove(cols[0]);
                        }
                    }
                }
            }
            if (readingsIndex.Count != 0)
            {
                double d;
                for (int i = 0; i < 24; i++)
                {
                    if (double.TryParse(valuesArray[i], out d) == false)
                    {
                        valuesArray[i] = "0.000";
                    }
                }
            }
            foreach (string value in valuesArray)
            {
                formattedString += $"{value},";
            }
            formattedString = formattedString.Substring(0, formattedString.Length - 1);
            return formattedString;

        }
    }
}
