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
        public ProgramParameterPage programParameterPage;
        public DiagnosticsPage diagnosticsPage { get; set; }

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
            if(programParameterPage == null)
            {
                programParameterPage = new ProgramParameterPage(this);
            }
            programParameterPage.parent = this;
            programParameterPage.refreshTexts();
            ContainerWindow.container.ChangeFrame(programParameterPage);
        }

        private void diagnosticsButton_Click(object sender, RoutedEventArgs e)
        {
            if(diagnosticsPage == null)
            {
                diagnosticsPage = new DiagnosticsPage(this);
            }
            diagnosticsPage.parent = this;
            ContainerWindow.container.ChangeFrame(diagnosticsPage);
        }
    }
}
