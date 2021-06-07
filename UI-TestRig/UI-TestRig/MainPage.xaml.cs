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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        IContainer parent;
        
        public MainPage(IContainer container)
        {
            InitializeComponent();
            parent = container;
            
            
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
            parent.ChangeFrame(new ProgramParameterPage(parent));
        }

        private void diagnosticsButton_Click(object sender, RoutedEventArgs e)
        {
            parent.ChangeFrame(new DiagnosticsPage(parent));
        }
    }
}
