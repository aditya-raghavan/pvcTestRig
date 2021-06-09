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
    /// Interaction logic for DiagnosticsPage.xaml
    /// </summary>
    public partial class DiagnosticsPage : Page
    {
        public MainPage parent { get; set; }

        public MachineDataPage machineDataPage { get; set; }
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
            ContainerWindow.container.ChangeFrame(machineDataPage);
        }
    }
}
