using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestRig
{
    public class GroupTemplate
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<string> AllowedFunctions { get; set; } = new List<string>();

        public Dictionary<string, bool> FunctionsBool { get; set; } = new Dictionary<string, bool>();
    }
}
