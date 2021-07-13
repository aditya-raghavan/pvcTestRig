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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page,IPage
    {
        public ProgramParameterPage programParameterPage;
        public DiagnosticsPage diagnosticsPage { get; set; }

        public LoginPage loginPage { get; set; }

        bool buttonFlag = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        private void editProcessParameterButton_Click(object sender, RoutedEventArgs e)
        {
            if (programParameterPage == null)
            {
                programParameterPage = new ProgramParameterPage(this);
            }
            programParameterPage.parent = this;
            programParameterPage.refreshTexts();
            programParameterPage.CheckUser();
            ContainerWindow.container.ChangeFrame(programParameterPage);
        }

        private void diagnosticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (diagnosticsPage == null)
            {
                diagnosticsPage = new DiagnosticsPage(this);
            }
            diagnosticsPage.parent = this;
            diagnosticsPage.CheckUser();
            ContainerWindow.container.ChangeFrame(diagnosticsPage);
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserAdministrationGlobalConfig.uAdmin_GroupsList == null || UserAdministrationGlobalConfig.uAdmin_GroupsList.Count == 0)
            {
                MessageBox.Show("MASTER FILE FOR USER ADMINISTRATION IS EITHER NOT FOUND OR NOT IN A CORRECT FORMAT. USER ADMINISTRATION FEATURE CANNOT BE USED.", "FILE MISSING", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (UserAdministrationGlobalConfig.uAdmin_CurrentUser == null)
            {
                if(buttonFlag == false)
                {
                    loginPage = new LoginPage(this);
                    loginPage.Owner = Application.Current.MainWindow;
                    loginPage.Closing += OnLoginClose;
                    buttonFlag = true;
                    loginPage.Show();
                }
            }
            else
            {
                if(MessageBox.Show("CONFIRM LOG OUT?","CONFIRMATION",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    UserAdministrationGlobalConfig.uAdmin_CurrentUser = null;
                    logInButton.Content = "Log In";
                    CheckUser();
                }
                
            }
        }

        private void OnLoginClose(object sender, System.ComponentModel.CancelEventArgs e) 
        {
            buttonFlag = false;
        }

        public void CheckUser()
        {
            if(UserAdministrationGlobalConfig.uAdmin_CurrentUser != null)
            {
                userTextBox.Text = UserAdministrationGlobalConfig.uAdmin_CurrentUser.UserId;
                logInButton.Content = "Log Out";
            }
            else
            {
                userTextBox.Text = "";
                logInButton.Content = "Log In";
            }
        }
    }
}
