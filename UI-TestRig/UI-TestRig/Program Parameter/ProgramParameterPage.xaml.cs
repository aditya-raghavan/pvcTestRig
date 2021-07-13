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
using System.Windows.Shapes;

using Microsoft.Win32;
using System.Configuration;
using System.Text.RegularExpressions;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for ProgramParameterPage.xaml
    /// </summary>
    public partial class ProgramParameterPage : Page,IPage
    {

        public MainPage parent { get; set; }
        

        //DiodeTypes for Combobox
        private static List<String> diodeTypes = TestConfigurationTemplate.DiodeTypes;
        //Barcode options (ENABLED, DISABLED)
        private static List<String> barCodeOptions = TestConfigurationTemplate.BarCodeOptions;

        Dictionary<string, string> validationResults = new Dictionary<string, string>()
        {
            { "diodeCodeText", "false" },
            { "additionalCodeText", "false" },
            { "customerCodeText", "false" },
            { "positiveTolVoltageText", "true" },
            { "negativeTolVoltageText", "true" },
            { "nominalFDVText", "true" },
            { "postiveToleranceCurrentText", "true" },
            { "negativeTolerenceCurrentText", "true" },
            { "nominalRevCurrentText", "true" },
            { "forwardTestCurrentText", "true" },
            { "ReverseTestVoltageText", "true" },
            { "forwardMaxVoltageText", "true" },
            { "positiveTolResText", "true" },
            { "negativeTolResText", "true" },
            { "contactResistanceText", "true" }
        };
        public ProgramParameterPage(MainPage p)
        {
            InitializeComponent();            
            NewTestConfiguration();           
            parent = p;            
        }

        

        private void RefreshComboBoxes()
        {
            diodeTypeCombo.ItemsSource = null;
            diodeTypeCombo.ItemsSource = diodeTypes;
            diodeTypeCombo.SelectedIndex = 1;

            barCodePrinterCombo.ItemsSource = null;
            barCodePrinterCombo.ItemsSource = barCodeOptions;
            barCodePrinterCombo.SelectedIndex = 1;
        }

        private void NewTestConfiguration()
        {
            testConfigTextBlock.Text = "";
            diodeCodeText.Text = "";
            customerCodeText.Text = "";
            additionalCodeText.Text = "";
            positiveTolVoltageText.Text = double.Parse("0").ToString("N3");
            negativeTolVoltageText.Text = double.Parse("0").ToString("N3"); ;
            nominalFDVText.Text = double.Parse("0").ToString("N3"); ;
            postiveToleranceCurrentText.Text = double.Parse("0").ToString("N3"); ;
            nominalRevCurrentText.Text = double.Parse("0").ToString("N3"); ;
            negativeTolerenceCurrentText.Text = double.Parse("0").ToString("N3"); ;
            forwardMaxVoltageText.Text = double.Parse("0").ToString("N3"); ;
            forwardTestCurrentText.Text = double.Parse("0").ToString("N3"); ;
            ReverseTestVoltageText.Text = double.Parse("0").ToString("N3"); ;
            positiveTolResText.Text = double.Parse("0").ToString("N3"); ;
            contactResistanceText.Text = double.Parse("0").ToString("N3"); ;
            negativeTolResText.Text = double.Parse("0").ToString("N3"); ;
            programTextBox.Text = "";

            RefreshComboBoxes();

        }

        //Disabled user from inputting values other than digits.
        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!(Char.IsDigit(ch) || ch.Equals('.') | ch.Equals('-')))
                {
                    e.Handled = true;

                    break;
                }
            }
            foreach (var ch in e.Text)
            {
                if ((ch == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                    break;
                }

            }
            foreach (var ch in e.Text)
            {
                if ((ch == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
                {
                    e.Handled = true;
                    break;
                }

            }
        }

        private SaveFileDialog GetSaveFileDialog()
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.DefaultExt = ".csv";
            ofd.Filter = GlobalConfig.allowedFileTypes;

            ofd.InitialDirectory = $"{ConfigurationManager.AppSettings["filePath"] }\\";
            return ofd;
        }


        //save Button Event
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                
                if (testConfigTextBlock.Text.Length == 0)
                {
                    SaveFileDialog ofd = GetSaveFileDialog();


                    Nullable<bool> result = ofd.ShowDialog();
                    if (result == true)
                    {
                        while (!ofd.FileName.Contains(ConfigurationManager.AppSettings["filePath"]))
                        {
                            MessageBox.Show("File Directory not allowed. Please choose from default directory.");
                            ofd.FileName = "";
                            if (ofd.ShowDialog() == false)
                            {
                                statusText.Text = "SAVE OPERATION CANCELLED";
                                return;
                            }

                        }

                        SaveTestConfiguration(ofd.FileName);
                    }
                    else
                    {
                        statusText.Text = "SAVE OPERATION CANCELLED";
                    }
                }
                else
                {
                    //If test configuration is already loaded, sends the test configuration name to Save the config
                    SaveTestConfiguration(testConfigTextBlock.Text);
                    statusText.Text = "TEST CONFIGURATION FILE SAVED";
                }

            }
            else
            {
                statusText.Text = "SAVE OPERATION FAILED";
                MessageBox.Show("Incorrect Values Detected. Please Check for missing fields. Inputted values must be within their respective limits.", "Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateForm()
        {
            bool output = true;


            if (validationResults.ContainsValue("false"))
            {
                output = false;

            }

            return output;
        }

        //New Button event
        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Create New Test Configuration?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //Refreshes form
                NewTestConfiguration();
                statusText.Text = "NEW TEST CONFIGURATION";

            }
        }


        //Retrieved Test Configuration from the database is copied to Textboxes.
        private void CopyTestConfigurationToTextBox(TestConfigurationTemplate template)
        {
            testConfigTextBlock.Text = template.testConfigurationName;
            diodeCodeText.Text = template.diodeCode;
            additionalCodeText.Text = template.additionalCode;
            customerCodeText.Text = template.customerCode;
            diodeTypeCombo.SelectedIndex = TestConfigurationTemplate.GetIndexFromDiodeType(template.diodeType);
            barCodePrinterCombo.SelectedIndex = TestConfigurationTemplate.GetIndexFromBarcodeOption(template.barCodeOption);
            positiveTolVoltageText.Text = template.positiveTolerenceVoltage.ToString("N3");
            negativeTolVoltageText.Text = template.negativeTolerenceVoltage.ToString("N3");
            nominalFDVText.Text = template.nominalForwardDropVolts.ToString("N3");
            postiveToleranceCurrentText.Text = template.positiveTolerenceCurrent.ToString("N3");
            negativeTolerenceCurrentText.Text = template.negativeTolerenceCurrent.ToString("N3");
            nominalRevCurrentText.Text = template.nominalReverseCurrent.ToString("N3");
            forwardTestCurrentText.Text = template.forwardTestCurrent.ToString("N3");
            ReverseTestVoltageText.Text = template.reverseTestVoltage.ToString("N3");
            forwardMaxVoltageText.Text = template.forwardMaxVoltage.ToString("N3");
            positiveTolResText.Text = template.positiveTolerenceResistance.ToString("N3");
            negativeTolResText.Text = template.negativeTolerenceResistance.ToString("N3");
            contactResistanceText.Text = template.contactResistance.ToString("N3");

            programTextBox.Text = template.testConfigurationName;

        }


        //Initiates save operation
        public void SaveTestConfiguration(string filePath)
        {
            //Utility method which saves test configuration to specified testconfig name file.
            string testConfigurationName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            TestConfigurationTemplate template = new TestConfigurationTemplate();
            template.testConfigurationName = testConfigurationName;
            testConfigTextBlock.Text = template.testConfigurationName;

            template.diodeCode = diodeCodeText.Text;
            template.customerCode = customerCodeText.Text;
            template.additionalCode = additionalCodeText.Text;
            template.diodeType = TestConfigurationTemplate.GetDiodeTypeFromIndex(diodeTypeCombo.SelectedIndex);
            template.barCodeOption = TestConfigurationTemplate.GetBarcodeOptionFromIndex(barCodePrinterCombo.SelectedIndex);

            template.positiveTolerenceVoltage = double.Parse(positiveTolVoltageText.Text);
            template.negativeTolerenceVoltage = double.Parse(negativeTolVoltageText.Text);
            template.nominalForwardDropVolts = double.Parse(nominalFDVText.Text);
            template.positiveTolerenceCurrent = double.Parse(postiveToleranceCurrentText.Text);
            template.negativeTolerenceCurrent = double.Parse(negativeTolerenceCurrentText.Text);
            template.nominalReverseCurrent = double.Parse(nominalRevCurrentText.Text);
            template.forwardTestCurrent = double.Parse(forwardTestCurrentText.Text);
            template.reverseTestVoltage = double.Parse(ReverseTestVoltageText.Text);
            template.forwardMaxVoltage = double.Parse(forwardMaxVoltageText.Text);
            template.positiveTolerenceResistance = double.Parse(positiveTolResText.Text);
            template.negativeTolerenceResistance = double.Parse(negativeTolResText.Text);
            template.contactResistance = double.Parse(contactResistanceText.Text);

            try
            {

                GlobalConfig.programParameterConnection.SaveTestConfigurationToFile(template);
                statusText.Text = "TEST CONFIGURATION FILE SAVED";

            }
            catch (Exception)
            {
                statusText.Text = "SAVE OPERATION FAILED";
            }

        }

        private void saveAsButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                SaveFileDialog ofd = GetSaveFileDialog();


                Nullable<bool> result = ofd.ShowDialog();
                if (result == true)
                {
                    while (!ofd.FileName.Contains(ConfigurationManager.AppSettings["filePath"]))
                    {
                        MessageBox.Show("File Directory not allowed. Please choose from default directory.");
                        ofd.FileName = "";
                        if (ofd.ShowDialog() == false)
                        {
                            statusText.Text = "SAVE OPERATION CANCELLED";
                            return;
                        }

                    }

                    SaveTestConfiguration(ofd.FileName);
                    statusText.Text = "TEST CONFIGURATION FILE SAVED";
                }
                else
                {
                    statusText.Text = "SAVE OPERATION CANCELLED";
                }

            }
            else
            {
                statusText.Text = "SAVE OPERATION FAILED";
                MessageBox.Show("Incorrect Values Detected. Please Check for missing fields. Inputted values must be within their respective limits.", "Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = GetOpenFileDialog();

            ofd.Title = "Delete";
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                while (!ofd.FileName.Contains(ConfigurationManager.AppSettings["filePath"]))
                {
                    MessageBox.Show("File Directory not allowed. Please choose from default directory.");
                    ofd.FileName = "";
                    if (ofd.ShowDialog() == false)
                    {
                        statusText.Text = "DELETE OPERATION CANCELLED";
                        return;
                    }

                }

                if (MessageBox.Show($"Delete Test Configuration {ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4)}?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GlobalConfig.programParameterConnection.DeleteTestConfiguration(ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4));
                    statusText.Text = "TEST CONFIGURATION FILE DELETED";
                    if (testConfigTextBlock.Text == ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4))
                    {
                        NewTestConfiguration();

                    }
                }
                else
                {
                    statusText.Text = "DELETE OPERATION CANCELLED";
                }

            }
            else
            {
                statusText.Text = "DELETE OPERATION CANCELLED";
            }



        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            parent.programParameterPage = this;
            ContainerWindow.container.ChangeFrame(parent);
        }

        public void CheckUser()
        {
            if (UserAdministrationGlobalConfig.uAdmin_CurrentUser != null)
            {
                userTextBox.Text = UserAdministrationGlobalConfig.uAdmin_CurrentUser.UserId;
            }
            else
            {
                userTextBox.Text = "";
            }
        }
        private OpenFileDialog GetOpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".csv";
            ofd.Filter = GlobalConfig.allowedFileTypes;

            ofd.InitialDirectory = $"{ConfigurationManager.AppSettings["filePath"] }\\";
            ofd.Multiselect = false;
            return ofd;

        }


        //Open Button Event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Open File Dialog
            OpenFileDialog ofd = GetOpenFileDialog();
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                while (!ofd.FileName.Contains(ConfigurationManager.AppSettings["filePath"]))
                {
                    MessageBox.Show("File Directory not allowed. Please choose from default directory.");
                    ofd.FileName = "";
                    if (ofd.ShowDialog() == false)
                    {
                        statusText.Text = "OPEN OPERATION CANCELLED";
                        return;
                    }

                }

                //Loads all the test config information from text file to TestConfigurationTemplate Class.
                try
                {
                    TestConfigurationTemplate template = GlobalConfig.programParameterConnection.LoadTestConfigurationFromFile(ofd.SafeFileName);
                    if (template == null)
                    {
                        MessageBox.Show("File has Invalid Fields.", "File Load Failed.", MessageBoxButton.OK, MessageBoxImage.Error);
                        statusText.Text = "COULD NOT OPEN TEST CONGIGURATION FILE";
                        return;
                    }
                    CopyTestConfigurationToTextBox(template);
                    statusText.Text = "TEST CONFIGURATION FILE OPENED SUCCESSFULLY";
                }
                catch (Exception exception)
                {
                    statusText.Text = exception.Message;
                }
            }
            else
            {
                statusText.Text = "OPEN OPERATION CANCELLED";
            }
        }

        public void refreshTexts()
        {
            List<string> TextBoxNames = validationResults.Keys.ToList();
            foreach(string textBoxName in TextBoxNames)
            {
                TextBox textBox = (TextBox)this.FindName(textBoxName);
                textBox.Text = textBox.Text + '0';
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
            }
        }

        private void positiveTolVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = positiveTolVoltageText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["positiveTolVoltageText"] = "false";
            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }

                validationResults["positiveTolVoltageText"] = "true";


            }
        }

        private void negativeTolVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = negativeTolVoltageText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["negativeTolVoltageText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }

                validationResults["negativeTolVoltageText"] = "true";

            }

        }

        private void nominalFDVText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = nominalFDVText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["nominalFDVText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }

                validationResults["nominalFDVText"] = "true";

            }

        }

        private void postiveToleranceCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = postiveToleranceCurrentText;


                if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                    || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentHigh ||
                    double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentLow)
                {
                    if (box.IsFocused == false)
                    {
                        box.Background = new SolidColorBrush(Colors.Yellow);
                        box.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    validationResults["postiveToleranceCurrentText"] = "false";

                }
                else
                {
                    var bc = new BrushConverter();
                    //#ccffff
                    if (box.IsFocused == false)
                    {
                        box.Background = (Brush)bc.ConvertFrom("#ccffff");
                        box.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    validationResults["postiveToleranceCurrentText"] = "true";

                }
            }
        }
        private void nominalRevCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = nominalRevCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["nominalRevCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }
                validationResults["nominalRevCurrentText"] = "true";

            }
        }

        private void negativeTolerenceCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = negativeTolerenceCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["negativeTolerenceCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }
                validationResults["negativeTolerenceCurrentText"] = "true";

            }

        }

        private void forwardTestCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = forwardTestCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.forwardTestCurrentHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.forwardTestCurrentLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["forwardTestCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }
                validationResults["forwardTestCurrentText"] = "true";

            }

        }

        private void forwardMaxVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = forwardMaxVoltageText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["forwardMaxVoltageText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }
                validationResults["forwardMaxVoltageText"] = "true";
            }

        }

        private void ReverseTestVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = ReverseTestVoltageText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.reverseTestVoltageHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.reverseTestVoltageLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["ReverseTestVoltageText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }
                validationResults["ReverseTestVoltageText"] = "true";

            }

        }

        private void positiveTolResText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = positiveTolResText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["positiveTolResText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }
                validationResults["positiveTolResText"] = "true";

            }

        }

        private void contactResistanceText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = contactResistanceText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.contactResistanceHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.contactResistanceLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["contactResistanceText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }
                validationResults["contactResistanceText"] = "true";

            }

        }

        private void negativeTolResText_TextChanged(object sender, TextChangedEventArgs e)
        {


            var box = negativeTolResText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceHigh ||
                double.Parse(box.Text) < MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceLow)
            {
                if (box.IsFocused == false)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                }
                validationResults["negativeTolResText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                if (box.IsFocused == false)
                {
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                }
                validationResults["negativeTolResText"] = "true";

            }

        }

        private void diodeCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = diodeCodeText;
                if (box.Text.Trim().Length == 0)
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Red);
                    validationResults["diodeCodeText"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    validationResults["diodeCodeText"] = "true";
                }
            }
        }

        private void customerCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = customerCodeText;
                if (box.Text.Trim().Length == 0)
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Red);
                    validationResults["customerCodeText"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    validationResults["customerCodeText"] = "true";
                }
            }
        }

        private void additionalCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = additionalCodeText;
                if (box.Text.Trim().Length == 0)
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Red);
                    validationResults["additionalCodeText"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    validationResults["additionalCodeText"] = "true";
                }
            }
        }

        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            var bc = new BrushConverter();
            textBox.Background = (Brush)bc.ConvertFrom("#ffccff");
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            double d;
            TextBox textBox = (TextBox)sender;
            if (validationResults[textBox.Name.ToString()] == "true")
            {
                var bc = new BrushConverter();
                textBox.Foreground = new SolidColorBrush(Colors.Black);
                textBox.Background = (Brush)bc.ConvertFrom("#ccffff");
            }
            else
            {
                textBox.Background = new SolidColorBrush(Colors.Yellow);
                textBox.Foreground = new SolidColorBrush(Colors.Red);
            }
            if (double.TryParse(textBox.Text, out d) == false)
            {
                textBox.Text = "0.000";
            }
            else
            {
                textBox.Text = d.ToString("N3");
            }


        }

    }
}
