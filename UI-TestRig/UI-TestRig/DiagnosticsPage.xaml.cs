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
        IContainer parent;
        public DiagnosticsPage(IContainer container)
        {
            InitializeComponent();
            parent = container;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            parent.ChangeFrame(new MainPage(parent));
        }

        private void machineDataButton_Click(object sender, RoutedEventArgs e)
        {
            parent.ChangeFrame(new MachineDataPage(parent));
        }
    }
}
