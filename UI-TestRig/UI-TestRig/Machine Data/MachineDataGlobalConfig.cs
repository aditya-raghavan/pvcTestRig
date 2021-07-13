using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestRig
{
    public static class MachineDataGlobalConfig
    {
        public static TestConfigurationTemplate machineDataObject { get; set; } = new TestConfigurationTemplate();

        public static bool isMachineDataFileThere = false;
    }
}
