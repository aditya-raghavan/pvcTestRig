using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UI_TestRig
{
    public static class UserAdministrationTextConnectorProcessor
    {
        public static string FullUserDataPath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["userFilesPath"] }\\{fileName}";
        }

        /// <summary>
        /// Loads content from a file path.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        

        public static List<string> ConvertToFunctions(this List<string> lines)
        {
            List<string> FunctionsList = new List<string>();
            if (lines == null || lines.Count == 0)
            {
                return FunctionsList;
            }

            foreach (string line in lines)
            {
                if (line == "GROUP DETAILS")
                {

                    break;
                }
                if (line.Length != 0)
                {
                    FunctionsList.Add(line);
                }
            }
            return FunctionsList;
        }

        public static List<GroupTemplate> ConvertToGroupObject(this List<string> lines)
        {
            List<GroupTemplate> GroupsList = new List<GroupTemplate>();
            List<string> FunctionsList = new List<string>();
            List<string> LinesCopy = new List<string>();
            if (lines == null || lines.Count == 0)
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
                if (line.Length != 0)
                {
                    FunctionsList.Add(line);
                }
                LinesCopy.Remove(line);

            }

            foreach (string line in LinesCopy)
            {
                GroupTemplate template = new GroupTemplate();
                string[] cols = line.Split(',');
                if ((cols.Length == 3 || cols.Length == 2) && int.TryParse(cols[0], out int x) == true)
                {
                    bool isDuplicateGroup = false;
                    foreach (GroupTemplate group in GroupsList)
                    {
                        if (group.GroupId == x || group.GroupName == cols[1])
                        {
                            isDuplicateGroup = true;
                        }

                    }
                    if (isDuplicateGroup == false)
                    {
                        template.GroupId = int.Parse(cols[0]);
                        template.GroupName = cols[1];
                        if (cols.Length == 3)
                        {
                            string[] groupFunctions = cols[2].Split('|');
                            foreach (string function in groupFunctions)
                            {
                                if (FunctionsList.Contains(function))
                                {
                                    template.AllowedFunctions.Add(function);
                                }
                            }
                        }
                        foreach (string function in UserAdministrationGlobalConfig.uAdmin_FunctionsList)
                        {
                            if (template.AllowedFunctions.Contains(function))
                            {
                                template.FunctionsBool.Add(function, true);
                            }
                            else
                            {
                                template.FunctionsBool.Add(function, false);
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
            if (lines == null || lines.Count == 0)
            {
                return usersList;
            }
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                UserTemplate user = new UserTemplate();
                if (cols.Length == 3)
                {
                    user.UserId = cols[0];
                    user.Password = cols[1];
                    if (int.TryParse(cols[2], out int x))
                    {
                        user.Group = GetGroupFromId(cols[2]);
                        if (user.Group != null)
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
            if (UserAdministrationGlobalConfig.uAdmin_GroupsList == null || UserAdministrationGlobalConfig.uAdmin_GroupsList.Count == 0)
            {
                return null;
            }
            foreach (GroupTemplate group in UserAdministrationGlobalConfig.uAdmin_GroupsList)
            {
                if (group.GroupId == int.Parse(id))
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
    }
}
