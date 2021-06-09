using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRigLibrary.Templates;

namespace TestRigLibrary
{
    /// <summary>
    /// Any class acting as a data connection class must have the following methods.
    /// </summary>
    public interface IDataConnection
    {
        void SaveTestConfigurationToFile(TestConfigurationTemplate template);

        TestConfigurationTemplate LoadTestConfigurationFromFile(string testConfigurationName);

        

        void DeleteTestConfiguration(string testConfigurationName);

        void SaveMachineData();

        bool CheckMachineDataFile();

        TestConfigurationTemplate LoadMachineDataFile();
    }
}
