﻿using System;
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
        private static Dictionary<string, int> GetTypeIndexDict()
        {
            return new Dictionary<string, int>()
            {
            {"TEST CONFIGURATION NAME", 0},
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

        /// <summary>
        /// Returns full file path for a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FullFilePath(this string fileName)
        {

            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{fileName}";

        }

        public static string FullMachineDataPath(this string fileName)
        {

            return $"{ ConfigurationManager.AppSettings["machinedataPath"] }\\{fileName}";

        }

        public static string FullUserDataPath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["userFilesPath"] }\\{fileName}";
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
        /// returns a populated TestConfigurationTemplate Object with values read from the specified test config file.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static TestConfigurationTemplate ConvertToTestConfigurationTemplate(this List<string> lines)
        {
            TestConfigurationTemplate template = new TestConfigurationTemplate();


            string line = ExtractValuesFromTestConfigurationFile(lines);
            if (line == null)
            {
                return null;
            }

            if (line.Length != 0)
            {
                string[] cols = line.Split(',');

                template.testConfigurationName = cols[0];
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

        public static List<GroupTemplate> ConvertToGroupObject(this List<string> lines)
        {
            List<GroupTemplate> GroupsList = new List<GroupTemplate>();
            List<string> FunctionsList = new List<string>();
            List<string> LinesCopy = new List<string>();
            if(lines == null || lines.Count == 0)
            {
                return GroupsList;
            }
            foreach (string line in lines)
            {
                LinesCopy.Add(line);
            }
            foreach (string line in lines)
            {
                if (line == "GROUP DETAILS")
                {
                    LinesCopy.Remove(line);
                    break;
                }
                FunctionsList.Add(line);
                LinesCopy.Remove(line);

            }
            foreach (string line in LinesCopy)
            {
                GroupTemplate template = new GroupTemplate();
                string[] cols = line.Split(',');
                if(cols.Length == 3 && int.TryParse(cols[0],out int x)== true)
                {
                    bool isDuplicateGroup = false;
                    foreach(GroupTemplate group in GroupsList)
                    {
                        if(group.GroupId == x || group.GroupName == cols[1])
                        {
                            isDuplicateGroup = true;
                        }
                        
                    }
                    if (isDuplicateGroup == false)
                    {
                        template.GroupId = int.Parse(cols[0]);
                        template.GroupName = cols[1];
                        string[] groupFunctions = cols[2].Split('|');
                        foreach (string function in groupFunctions)
                        {
                            if (FunctionsList.Contains(function))
                            {
                                template.AllowedFunctions.Add(function);
                            }

                        }
                        GroupsList.Add(template);
                    }
                }
                
            }
            return GroupsList;

        }

        public static List<UserTemplate> ConvertToUserObject(this List<string> lines)
        {
            
            List<UserTemplate> usersList = new List<UserTemplate>();
            if(lines == null || lines.Count == 0)
            {
                return usersList;
            }
            foreach(string line in lines)
            {
                string[] cols = line.Split(',');
                UserTemplate user = new UserTemplate();
                if(cols.Length == 3)
                {
                    user.UserId = cols[0];
                    user.Password = cols[1];
                    if(int.TryParse(cols[2], out int x))
                    {
                        user.Group = GetGroupFromId(cols[2]);
                        if(user.Group != null)
                        {
                            usersList.Add(user);
                        }
                        
                    }
                    
                }
                
            }
            return usersList;
            
        }

        private static GroupTemplate GetGroupFromId(string id)
        {
            bool found = false;
            GroupTemplate obj = new GroupTemplate();
            if(GlobalConfig.GroupsList == null || GlobalConfig.GroupsList.Count == 0)
            {
                return null;
            }
            foreach(GroupTemplate group in GlobalConfig.GroupsList)
            {
                if(group.GroupId == int.Parse(id))
                {
                    obj = group;
                    found = true;
                }
            }
            if (found)
            {
                return obj;
            }
            else
            {
                return null;
            }
        }

        private static string ExtractValuesFromMachineDataFile(List<string> lines)
        {
            string[] valuesArray = new string[24];
            string formattedString = "";
            Dictionary<string, int> readingsIndex = GetMachineDataReadings();
            Dictionary<string, string> units = GetMachineDataUnits();

            foreach(string line in lines)
            {
                string[] cols = line.Split(',');
                for (int i = 0; i < cols.Length; i++)
                {
                    cols[i] = cols[i].Trim();
                }
                if (readingsIndex.ContainsKey(cols[0].ToUpper()))
                {
                    cols[0] = cols[0].ToUpper();
                    if(cols.Length == 3)
                    {
                        if (units[cols[0]] == cols[2] && double.TryParse(cols[1], out double x))
                        {
                            valuesArray[readingsIndex[cols[0]]] = cols[1];
                            readingsIndex.Remove(cols[0]);
                        }
                    }
                }
            }
            if(readingsIndex.Count != 0)
            {
                double d;
                for(int i = 0; i < 24; i++)
                {
                    if(double.TryParse(valuesArray[i], out d) == false)
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

      

        private static string ExtractValuesFromTestConfigurationFile(List<string> lines)
        {
            string[] valuesArray = new string[18];
            string formattedString = "";
            Dictionary<string, int> typeInfoIndex = GetTypeIndexDict();

            Dictionary<string, int> readingsIndex = GetReadingsIndexDict();

            Dictionary<string, string> units = GetUnitsDict();
            


            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                for(int i = 0; i < cols.Length; i++)
                {
                    cols[i] = cols[i].Trim();
                }
                if (typeInfoIndex.ContainsKey(cols[0].ToUpper()))
                {
                    cols[0] = cols[0].ToUpper();
                    if(cols.Length == 2)
                    {
                        cols[1] = cols[1].ToUpper();
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

                }
                else if (readingsIndex.ContainsKey(cols[0].ToUpper()))
                {
                    cols[0] = cols[0].ToUpper();
                    if(cols.Length == 3)
                    {
                        if (units[cols[0]] == cols[2] && double.TryParse(cols[1], out double x))
                        {
                            valuesArray[readingsIndex[cols[0]]] = cols[1];
                            readingsIndex.Remove(cols[0]);
                        }
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
