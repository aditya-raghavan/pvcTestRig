using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestRig
{
    public class UserTemplate
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public GroupTemplate Group { get; set; }

        public string PasswordEncrypted
        {
            get
            {
                return new string('*', Password.Length);
            }
        }


        public static List<GroupTemplate> GroupsList { get; set; } = new List<GroupTemplate>();
    }
}
