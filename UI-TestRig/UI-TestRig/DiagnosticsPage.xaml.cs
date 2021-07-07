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
using TestRigLibrary;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for DiagnosticsPage.xaml
    /// </summary>
    public partial class DiagnosticsPage : Page,IPage
    {
        public MainPage parent { get; set; }

        public MachineDataPage machineDataPage { get; set; }

        

        public UserAdministration_UsersTabPage userTabPage { get; set; }
        public DiagnosticsPage(MainPage p)
        {
            InitializeComponent();
            parent = p;            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            parent.diagnosticsPage = this;
            ContainerWindow.container.ChangeFrame(parent);
        }

        private void machineDataButton_Click(object sender, RoutedEventArgs e)
        {
            if(machineDataPage == null)
            {
                machineDataPage = new MachineDataPage(this);
            }
            machineDataPage.parent = this;
            machineDataPage.getMachineData();
            machineDataPage.CheckUser();
            ContainerWindow.container.ChangeFrame(machineDataPage);
        }

        public void CheckUser()
        {
            if(GlobalConfig.uAdmin_CurrentUser != null)
            {
                userTextBox.Text = GlobalConfig.uAdmin_CurrentUser.UserId;
            }
            else
            {
                userTextBox.Text = "";
            }
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (GlobalConfig.GroupsList == null || GlobalConfig.GroupsList.Count == 0)
            {
                MessageBox.Show("MASTER FILE FOR USER ADMINISTRATION IS EITHER NOT FOUND OR NOT IN A CORRECT FORMAT. USER ADMINISTRATION FEATURE CANNOT BE USED.", "FILE MISSING", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (userTabPage == null)
            {
                userTabPage = new UserAdministration_UsersTabPage(this);
            }
            userTabPage.parent = this;
            userTabPage.CheckUser();
            userTabPage.RefreshData();
            userTabPage.isAdmin();
            ContainerWindow.container.ChangeFrame(userTabPage);
        }
    }
}
