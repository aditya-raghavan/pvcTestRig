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
            //WindowState = WindowState.Maximized;
            //WindowStyle = WindowStyle.None;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //'Auto' Button Clicked Event
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (validateForm())
            {
                //changes 'no test running' to 'Automatic test running' 
                testStatusTextBlock.Background = new SolidColorBrush(Colors.Green);
                testStatusTextBlock.Text = "Automatic Test Running";

                //Inserts user details to SQL Server
                String user = userTextBox.Text;
                ComboBoxItem typeItem = (ComboBoxItem)modelComboBox.SelectedItem;
                string model = typeItem.Content.ToString();

                String op = operatorTextBox.Text;
                String diodecode = diodecodeTextBox.Text;
                String customercode = customercodeTextBox.Text;
                String additionalcode = additionalcodeTextBox.Text;
                String jbserial = jbserialTextBox.Text;
                String batchno = batchnoTextBox.Text;

                obj = new sqlconnection();
                //insertData in sqlconnection class
                obj.insertData(user, model, op, diodecode, customercode, additionalcode, jbserial, batchno);



                await Task.Delay(4000);


                testStatusTextBlock.Text = "Test Completed";

                testResultPassTextBlock.Background = new SolidColorBrush(Colors.Green);
                testResultPassTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                MessageBox.Show("Please fill all the details. Required Values for the form to be submitted: User, model, operator, diodecode, customercode, additionalcode, jbserial, batchno");
            }







        }

        private bool validateForm()
        {
            bool isFormValid = true;

            if(userTextBox.Text.Trim()=="" || userTextBox.Text == null)
            {
                isFormValid = false;
            }
            if (operatorTextBox.Text.Trim() == "" || operatorTextBox.Text == null)
            {
                isFormValid = false;
            }
            if (diodecodeTextBox.Text.Trim() == "" || diodecodeTextBox.Text == null)
            {
                isFormValid = false;
            }
            if (customercodeTextBox.Text.Trim() == "" || customercodeTextBox.Text == null)
            {
                isFormValid = false;
            }
            if (additionalcodeTextBox.Text.Trim() == "" || additionalcodeTextBox.Text == null)
            {
                isFormValid = false;
            }
            if (jbserialTextBox.Text.Trim() == "" || jbserialTextBox.Text == null)
            {
                isFormValid = false;
            }
            if (batchnoTextBox.Text.Trim() == "" || batchnoTextBox.Text == null)
            {
                isFormValid = false;
            }
            return isFormValid;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void jbserialText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
