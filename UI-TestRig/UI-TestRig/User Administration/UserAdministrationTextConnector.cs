using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


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

        public ObservableCollection<UserTemplate> LoadUsers()
        {
            string fileName = GlobalConfig.UsersFile;
            ObservableCollection<UserTemplate> usersList = fileName.FullUserDataPath().LoadFile().ConvertToUserObject();
            return usersList;
        }

        public void SaveUsersToFile()
        {

            List<string> lines = new List<string>();
            string fileName = GlobalConfig.UsersFile;
            foreach (UserTemplate user in UserAdministrationGlobalConfig.uAdmin_UsersList)
            {
                if(user.UserId != null && user.UserId.Length != 0 && user.Password != null && user.Password.Length != 0 && user.Group != null )
                {
                    string line = $"{user.UserId},{user.Password},{user.Group.GroupId}";
                    lines.Add(line);
                }
                
            }
            File.WriteAllLines(fileName.FullUserDataPath(), lines);
        }
    }
}
