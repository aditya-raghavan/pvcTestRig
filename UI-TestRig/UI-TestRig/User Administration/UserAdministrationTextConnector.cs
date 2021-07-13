using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UI_TestRig
{
    public class UserAdministrationTextConnector : IUserAdministrationDataConnection
    {
        public List<GroupTemplate> LoadGroups()
        {
            string fileName = GlobalConfig.groups_functions_file;
            List<GroupTemplate> GroupsList = fileName.FullUserDataPath().LoadFile().ConvertToGroupObject();

            return GroupsList;
        }

        public List<string> LoadFunctions()
        {
            string fileName = GlobalConfig.groups_functions_file;
            List<string> FunctionsList = fileName.FullUserDataPath().LoadFile().ConvertToFunctions();

            return FunctionsList;
        }

        public List<UserTemplate> LoadUsers()
        {
            string fileName = GlobalConfig.UsersFile;
            List<UserTemplate> usersList = fileName.FullUserDataPath().LoadFile().ConvertToUserObject();
            return usersList;
        }

        public void SaveUsersToFile()
        {

            List<string> lines = new List<string>();
            string fileName = GlobalConfig.UsersFile;
            foreach (UserTemplate user in UserAdministrationGlobalConfig.uAdmin_UsersList)
            {
                string line = $"{user.UserId},{user.Password},{user.Group.GroupId}";
                lines.Add(line);
            }
            File.WriteAllLines(fileName.FullUserDataPath(), lines);
        }
    }
}
