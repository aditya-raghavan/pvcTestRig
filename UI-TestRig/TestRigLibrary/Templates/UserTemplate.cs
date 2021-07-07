using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRigLibrary.Templates
{
    public class UserTemplate
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public GroupTemplate Group { get; set; }
    }
}
