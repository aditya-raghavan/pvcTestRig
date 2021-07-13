using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestRig
{
    public interface IProgramParameterDataConnection
    {
        void SaveTestConfigurationToFile(TestConfigurationTemplate template);

        TestConfigurationTemplate LoadTestConfigurationFromFile(string testConfigurationName);
        void DeleteTestConfiguration(string testConfigurationName);
    }
}
