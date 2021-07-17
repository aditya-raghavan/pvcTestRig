using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace UI_TestRig
{
    public static class UserAdministrationGlobalConfig
    {
        public static string groups_functions_file { get; set; } = "Groups_Functions.csv";
        public static string UsersFile { get; set; } = "Users.csv";
        
        public static UserTemplate uAdmin_CurrentUser { get; set; } = null;
        public static List<string> uAdmin_FunctionsList { get; set; }
        public static List<GroupTemplate> uAdmin_GroupsList { get; set; }
        public static ObservableCollection<UserTemplate> uAdmin_UsersList { get; set; }     
    }
}
