using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestRig
{
    public interface IUserAdministrationDataConnection
    {
        List<GroupTemplate> LoadGroups();
        List<UserTemplate> LoadUsers();

        List<string> LoadFunctions();

        void SaveUsersToFile();
    }
}
