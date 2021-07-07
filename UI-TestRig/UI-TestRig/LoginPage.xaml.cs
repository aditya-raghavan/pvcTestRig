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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        MainPage mainPage;
        public LoginPage(MainPage mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            userIdTextbox.Focus();
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticate())
            {
                string userId = userIdTextbox.Text;
                string password = passwordTextbox.Password;
                bool result = false;
                foreach(UserTemplate user in GlobalConfig.UsersList)
                {
                    if(userId == user.UserId && password == user.Password)
                    {
                        GlobalConfig.uAdmin_CurrentUser = user;
                        this.Close();
                        mainPage.CheckUser();
                        
                        
                        result = true;
                        break;
                    }
                }
                if(result == false)
                {
                    loginFailedLabel.Visibility = Visibility.Visible;
                }

            }
            else
            {
                loginFailedLabel.Visibility = Visibility.Visible;
            }
        }

        private bool Authenticate()
        {
            
            if(userIdTextbox.Text.Length != 0 && passwordTextbox.Password.Length != 0)
            {
                return true;
            }
            return false;
        }

        private void userIdTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (userIdTextbox.Text.Length == 0)
            {
                userIdTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void passwordTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordTextbox.Password.Length == 0)
            {
                passwordTextbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
