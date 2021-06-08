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
using TestRigLibrary;
using UI_TestRig;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ContainerWindow : Window,IContainer
    {
        
        public ContainerWindow()
        {
            InitializeComponent();
            mainFrame.Content = new MainPage(this);
            GlobalConfig.InitialiseConnections();
            GlobalConfig.LoadMachineData();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            if(GlobalConfig.isMachineDataFileThere == false)
            {
                MessageBox.Show("MACHINE DATA FILE NOT FOUND.", "FILE MISSING", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        public void ChangeFrame(Page page)
        {
            mainFrame.Content = page;
        }
    }
}
