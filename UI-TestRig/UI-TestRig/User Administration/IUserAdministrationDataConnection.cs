using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestRig
{
    public interface IUserAdministrationDataConnection
    {
        List<GroupTemplate> LoadGroups();
        ObservableCollection<UserTemplate> LoadUsers();

        List<string> LoadFunctions();

        void SaveUsersToFile();
    }
}
