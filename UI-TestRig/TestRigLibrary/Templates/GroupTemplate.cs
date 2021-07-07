using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRigLibrary.Templates
{
    public class GroupTemplate
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<string> AllowedFunctions { get; set; } = new List<string>();
    }
}
