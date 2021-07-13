using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

using UI_TestRig;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ContainerWindow : Window,IContainer
    {
        public static ContainerWindow container;
        public ContainerWindow()
        {
            InitializeComponent();
            mainFrame.Content = new MainPage();
            GlobalConfig.InitialiseConnections();
            GlobalConfig.LoadAllData();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            if(MachineDataGlobalConfig.isMachineDataFileThere == false)
            {
                MessageBox.Show("MACHINE DATA FILE NOT FOUND.", "FILE MISSING", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (UserAdministrationGlobalConfig.uAdmin_GroupsList == null || UserAdministrationGlobalConfig.uAdmin_GroupsList.Count == 0)
            {
                MessageBox.Show("MASTER FILE FOR USER ADMINISTRATION IS EITHER NOT FOUND OR NOT IN A CORRECT FORMAT. USER ADMINISTRATION FEATURE CANNOT BE USED.", "FILE MISSING", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            container = this;

        }

        public void ChangeFrame(IPage page)
        {
            page.CheckUser();
            mainFrame.Content = page;
        }


    }
}
