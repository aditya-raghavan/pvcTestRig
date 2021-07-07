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
using TestRigLibrary.Templates;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        ModifyUserWindow parent;

        private Dictionary<string, string> ValidationResults = new Dictionary<string, string>()
        {
            
            {"password","false" },
            {"confirmpassword","false" }
        };
        public ChangePasswordWindow(ModifyUserWindow p)
        {
            InitializeComponent();
            parent = p;
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
                passwordInvalidLabel.Visibility = Visibility.Hidden;
            }
        }

        private void confirmPasswordTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
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

            return output;
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {
                parent.ChangePasswordComplete(newpasswordTextbox.Password);
                this.Close();
            }
        }
    }
}
