using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestRigLibrary;
using TestRigLibrary.Templates;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private Dictionary<string, string> ValidationResults = new Dictionary<string, string>()
        {
            {"userId","false" },
            {"password","false" },
            {"confirmpassword","false" }
        };

        private void LoadComboBox()
        {
            groupsComboBox.ItemsSource = null;
            groupsComboBox.ItemsSource = GlobalConfig.GroupsList;
            groupsComboBox.DisplayMemberPath = "GroupName";
            groupsComboBox.SelectedIndex = 0;
        }

        UserAdministration_UsersTabPage parent;
        public AddUserWindow(UserAdministration_UsersTabPage p)
        {
            InitializeComponent();
            parent = p;
            LoadComboBox();
            
        }

        private void userIdTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            bool length = true;
            bool valid = true;
            if(userIdTextbox.Text.Length == 0)
            {
                length = false;
            }
            foreach(UserTemplate user in GlobalConfig.UsersList)
            {
                if(userIdTextbox.Text == user.UserId)
                {                    
                    valid = false;
                    break;
                }
            }
            if(length == false || valid == false)
            {
                userIdTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
                ValidationResults["userId"] = "false";
            }
            else
            {
                userIdTextbox.BorderBrush = new SolidColorBrush(Colors.Gray);
                ValidationResults["userId"] = "true";
            }
            if(valid == false)
            {
                userIdInvalidLabel.Visibility = Visibility.Visible;
            }
            else
            {
                userIdInvalidLabel.Visibility = Visibility.Hidden;
            }
        }

        private void passwordTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            bool length = true;
            bool valid = true;
            string pass = passwordTextbox.Password;
            if (passwordTextbox.Password.Length == 0)
            {
                length = false;
            }
            if (pass.Any(char.IsUpper) && pass.Any(char.IsLower) && pass.Any(char.IsDigit) && pass.Length >= 8 && pass.Length <= 16)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            if (length == false || valid == false)
            {
                passwordTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
                ValidationResults["password"] = "false";
            }
            else
            {
                passwordTextbox.BorderBrush = new SolidColorBrush(Colors.Gray);
                ValidationResults["password"] = "true";
            }
            if (valid == false)
            {
                passwordInvalidLabel.Visibility = Visibility.Visible;
            }
            else
            {
                passwordInvalidLabel.Visibility = Visibility.Hidden;
            }
            if(passwordTextbox.Password == confirmPasswordTextbox.Password && passwordTextbox.Password.Length != 0)
            {
                confirmPasswordInvalidLabel.Visibility = Visibility.Hidden;
            }
        }

        private void confirmPasswordTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(passwordTextbox.Password.Length == 0 && confirmPasswordTextbox.Password.Length == 0)
            {
                return;
            }
            bool length = true;
            bool valid = true;

            if (confirmPasswordTextbox.Password.Length == 0)
            {
                length = false;
            }
            if(confirmPasswordTextbox.Password.Length != 0 && confirmPasswordTextbox.Password == passwordTextbox.Password)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            if (length == false || valid == false)
            {
                confirmPasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
                ValidationResults["confirmpassword"] = "false";
            }
            else
            {
                confirmPasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Gray);
                ValidationResults["confirmpassword"] = "true";
            }
            if (valid == false)
            {
                confirmPasswordInvalidLabel.Visibility = Visibility.Visible;
            }
            else
            {
                confirmPasswordInvalidLabel.Visibility = Visibility.Hidden;
            }

        }

        private bool FormValidation()
        {
            bool output = true;


            if (ValidationResults.ContainsValue("false"))
            {
                output = false;

            }

            if(ValidationResults["userId"] == "false")
            {
                userIdTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if (ValidationResults["password"] == "false")
            {
                passwordTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if (ValidationResults["confirmpassword"] == "false")
            {
                confirmPasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            return output;
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {
                UserTemplate user = new UserTemplate();
                user.UserId = userIdTextbox.Text;
                user.Password = passwordTextbox.Password;
                user.Group = (GroupTemplate) groupsComboBox.SelectedItem;
                parent.AddUser(user);
                this.Close();
            }
        }
    }
}
