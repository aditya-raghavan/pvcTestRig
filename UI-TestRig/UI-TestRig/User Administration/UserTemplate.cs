using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI_TestRig
{
    public class UserTemplate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string UserId { get; set; }

        public string Password { get { return privatePassword; } set { privatePassword = value; OnPropertyChanged(); } }
        public GroupTemplate Group { get { return privateGroup; } set { privateGroup = value; OnPropertyChanged(); } }

        public string PasswordEncrypted
        {
            get
            {
                if (Password != null && Password.Length != 0)
                {
                    return new string('*', Password.Length);
                }
                return null;

            }
            set
            {
                privatePasswordEncrytped = value;
                OnPropertyChanged();
            }
        }
        public bool CanChangeGroup { get { return privateCanChangeGroup; } set { privateCanChangeGroup = value; OnPropertyChanged(); } }
        public static List<GroupTemplate> GroupsList { get; set; } = new List<GroupTemplate>();

        public UserTemplate()
        {
            this.UserId = "";
            this.Password = "";
            this.Group = null;
            this.CanChangeGroup = true;
        }
        private string privatePassword;
        private string privatePasswordEncrytped;
        private GroupTemplate privateGroup;
        private bool privateCanChangeGroup;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
