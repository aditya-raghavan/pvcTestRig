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
    public partial class MainWindow : Window
    {
        sqlconnection obj;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //'Auto' Button Clicked Event
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //changes 'no test running' to 'Automatic test running' 
            testRunningBlock.Background = new SolidColorBrush(Colors.Green);
            testRunningBlock.Text = "Automatic Test Running";

            //Inserts user details to SQL Server
            String user = userText.Text;
            ComboBoxItem typeItem = (ComboBoxItem)ComboBox1.SelectedItem;
            string model = typeItem.Content.ToString();
           
            String op = operatorText.Text;
            String diodecode = diodecodeText.Text;
            String customercode = customercodeText.Text;
            String additionalcode = additionalcodeText.Text;
            String jbserial = jbserialText.Text;
            String batchno = batchnoText.Text;

            obj = new sqlconnection();
            //insertData in sqlconnection class
            obj.insertData(user, model, op, diodecode, customercode, additionalcode, jbserial, batchno);



            await Task.Delay(4000);


            testRunningBlock.Text = "Test Completed";

            passBlock.Background = new SolidColorBrush(Colors.Green);
            passBlock.Foreground = new SolidColorBrush(Colors.Black);






        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
