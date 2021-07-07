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
    /// Interaction logic for ModifyUserWindow.xaml
    /// </summary>
    public partial class ModifyUserWindow : Window
    {
        UserTemplate user;
        UserAdministration_UsersTabPage parent;

        private Dictionary<string, string> ValidationResults = new Dictionary<string, string>()
        {
            {"userId","true" },
            {"password","false" },
            {"confirmpassword","false" }

        };

        bool isNewPasswordFirstTime = true;
        bool isConfirmPasswordFirstTime = true;
        public ModifyUserWindow(UserTemplate u, UserAdministration_UsersTabPage p)
        {
            InitializeComponent();
            parent = p;
            user = u;
            LoadForm();
        }

        public void LoadForm()
        {
            userIdTextbox.Text = user.UserId;
            passwordTextbox.Password = user.Password;
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            groupsComboBox.ItemsSource = null;
            groupsComboBox.ItemsSource = GlobalConfig.GroupsList;
            groupsComboBox.DisplayMemberPath = "GroupName";
            groupsComboBox.SelectedItem = user.Group;
        }

        private void userIdTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            bool length = true;
            bool valid = true;
            if (userIdTextbox.Text.Length == 0)
            {
                length = false;
            }
            foreach (UserTemplate user in GlobalConfig.UsersList)
            {
                if (userIdTextbox.Text == user.UserId)
                {
                    valid = false;
                    break;
                }
            }
            if(userIdTextbox.Text == user.UserId)
            {
                valid = true;
            }
            if (length == false || valid == false)
            {
                userIdTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
                ValidationResults["userId"] = "false";
            }
            else
            {
                userIdTextbox.BorderBrush = new SolidColorBrush(Colors.Gray);
                ValidationResults["userId"] = "true";
            }
            if (valid == false)
            {
                userIdInvalidLabel.Visibility = Visibility.Visible;
            }
            else
            {
                userIdInvalidLabel.Visibility = Visibility.Hidden;
            }
        }

        private bool FormValidation()
        {
            bool output = true;
            if(newpasswordLabel.Visibility == Visibility.Collapsed)
            {
                ValidationResults["confirmpassword"] = "true";
                ValidationResults["password"] = "true";
            }

            if (ValidationResults.ContainsValue("false"))
            {
                output = false;

            }

            if (ValidationResults["password"] == "false")
            {
                this.newpasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (ValidationResults["confirmpassword"] == "false")
            {
                this.confirmPasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            return output;
        }

        private void modifyUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {
                //if (user.Group.GroupId == 1)
                //{
                //    if(parent.WarnUser() == false)
                //    {
                //        return;
                //    }
                //}
                user.UserId = userIdTextbox.Text;
                if (newpasswordLabel.Visibility == Visibility.Visible)
                {
                    user.Password = newpasswordTextbox.Password;
                }
                else
                {
                    user.Password = passwordTextbox.Password;
                }
                
                user.Group = (GroupTemplate)groupsComboBox.SelectedItem;
                GlobalConfig.Connection.SaveUsersToFile();
                parent.RefreshData();
                this.Close();
            }
        }

        private void confirmPasswordTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(newpasswordTextbox.Password.Length == 0 && confirmPasswordTextbox.Password.Length == 0)
            {
                return;
            }
            bool length = true;
            bool valid = true;

            if (confirmPasswordTextbox.Password.Length == 0)
            {
                length = false;
            }
            if (confirmPasswordTextbox.Password.Length != 0 && confirmPasswordTextbox.Password == newpasswordTextbox.Password)
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
                confirmPasswordInvalidLabel.Visibility = Visibility.Collapsed;
            }

        }

        private void passwordTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            bool length = true;
            bool valid = true;
            string pass = newpasswordTextbox.Password;
            if (newpasswordTextbox.Password.Length == 0)
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
                newpasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
                ValidationResults["password"] = "false";
            }
            else
            {
                newpasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Gray);
                ValidationResults["password"] = "true";
            }
            if (valid == false)
            {
                passwordInvalidLabel.Visibility = Visibility.Visible;
            }
            else
            {
                passwordInvalidLabel.Visibility = Visibility.Collapsed;
            }
            if(newpasswordTextbox.Password == confirmPasswordTextbox.Password && newpasswordTextbox.Password.Length != 0)
            {
                confirmPasswordInvalidLabel.Visibility = Visibility.Collapsed;
            }
        }

        private void changePasswordLink_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.newpasswordLabel.Visibility == Visibility.Collapsed)
            {
                this.newpasswordTextbox.Password = "";
                this.confirmPasswordTextbox.Password = "";
                this.newpasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Gray);
                this.confirmPasswordTextbox.BorderBrush = new SolidColorBrush(Colors.Gray);
                this.newpasswordLabel.Visibility = Visibility.Visible;
                this.newpasswordTextbox.Visibility = Visibility.Visible;
                this.confirmPasswordLabel.Visibility = Visibility.Visible;
                this.confirmPasswordTextbox.Visibility = Visibility.Visible;
                
                if (ValidationResults["password"] == "false" && isNewPasswordFirstTime == false)
                {
                    this.passwordInvalidLabel.Visibility = Visibility.Visible;
                }
                if (ValidationResults["confirmpassword"] == "false" && isConfirmPasswordFirstTime == false)
                {
                    this.confirmPasswordInvalidLabel.Visibility = Visibility.Visible;
                }
                this.changePasswordLink.Text = "Cancel";
                this.Height = 580;
            }
            else
            {
                this.newpasswordLabel.Visibility = Visibility.Collapsed;
                this.newpasswordTextbox.Visibility = Visibility.Collapsed;
                this.confirmPasswordLabel.Visibility = Visibility.Collapsed;
                this.confirmPasswordTextbox.Visibility = Visibility.Collapsed;
                this.passwordInvalidLabel.Visibility = Visibility.Collapsed;
                this.confirmPasswordInvalidLabel.Visibility = Visibility.Collapsed;
                this.changePasswordLink.Text = "Change Password";
                isConfirmPasswordFirstTime = true;
                isNewPasswordFirstTime = true;
                this.Height = 400;
            }
            
        }

        private void ChangePasswordWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            changePasswordLink.IsEnabled = true;
        }

        public void ChangePasswordComplete(string password)
        {
            passwordTextbox.Password = password;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            
        }

        private void newpasswordTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            isNewPasswordFirstTime = false;
        }

        private void confirmPasswordTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            isConfirmPasswordFirstTime = false;
        }
    }
}
