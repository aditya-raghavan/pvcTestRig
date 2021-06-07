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
using TestRigLibrary;
using TestRigLibrary.Templates;
using Microsoft.Win32;
using System.Configuration;
using System.Text.RegularExpressions;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for ProgramParameterPage.xaml
    /// </summary>
    public partial class ProgramParameterPage : Page
    {
        IContainer parent;
        //Current loaded model which has higher and lower limits.
        
        //DiodeTypes for Combobox
        private static List<String> diodeTypes = TestConfigurationTemplate.DiodeTypes;
        //Barcode options (ENABLED, DISABLED)
        private static List<String> barCodeOptions = TestConfigurationTemplate.BarCodeOptions;

        Dictionary<string, string> validationResults = new Dictionary<string, string>()
        {
            { "diodeCode", "false" },
            { "additionalCode", "false" },
            { "customerCode", "false" },
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
        public ProgramParameterPage(IContainer container)
        {
            InitializeComponent();
            GlobalConfig.InitialiseConnections();
            NewModel();
            parent = container;
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

        private void NewModel()
        {
            modelTextBlock.Text = "";
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
                //If no model is loaded, calls Model name input form
                if (modelTextBlock.Text.Length == 0)
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

                        SaveModel(ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4));
                    }
                    else
                    {
                        statusText.Text = "SAVE OPERATION CANCELLED";
                    }
                }
                else
                {
                    //If model is already loaded, sends the model name to Save the model
                    SaveModel(modelTextBlock.Text);
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
            if (MessageBox.Show("Create New Model?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //Refreshes form
                NewModel();
                statusText.Text = "NEW TEST CONFIGURATION";

            }
        }


        //Retrieved model from the database is copied to loaded model.
        private void CopyModelToTextBox(TestConfigurationTemplate model)
        {
            modelTextBlock.Text = model.modelName;
            diodeCodeText.Text = model.diodeCode;
            additionalCodeText.Text = model.additionalCode;
            customerCodeText.Text = model.customerCode;
            diodeTypeCombo.SelectedIndex = TestConfigurationTemplate.GetIndexFromDiodeType(model.diodeType);
            barCodePrinterCombo.SelectedIndex = TestConfigurationTemplate.GetIndexFromBarcodeOption(model.barCodeOption);
            positiveTolVoltageText.Text = model.positiveTolerenceVoltage.ToString("N3");
            negativeTolVoltageText.Text = model.negativeTolerenceVoltage.ToString("N3");
            nominalFDVText.Text = model.nominalForwardDropVolts.ToString("N3");
            postiveToleranceCurrentText.Text = model.positiveTolerenceCurrent.ToString("N3");
            negativeTolerenceCurrentText.Text = model.negativeTolerenceCurrent.ToString("N3");
            nominalRevCurrentText.Text = model.nominalReverseCurrent.ToString("N3");
            forwardTestCurrentText.Text = model.forwardTestCurrent.ToString("N3");
            ReverseTestVoltageText.Text = model.reverseTestVoltage.ToString("N3");
            forwardMaxVoltageText.Text = model.forwardMaxVoltage.ToString("N3");
            positiveTolResText.Text = model.positiveTolerenceResistance.ToString("N3");
            negativeTolResText.Text = model.negativeTolerenceResistance.ToString("N3");
            contactResistanceText.Text = model.contactResistance.ToString("N3");

            programTextBox.Text = model.modelName;

        }


        //Initiates save operation
        public void SaveModel(string modelName)
        {
            //Utility method which saves Model to specified modelname file.

            TestConfigurationTemplate model = new TestConfigurationTemplate();
            model.modelName = modelName;
            modelTextBlock.Text = model.modelName;

            model.diodeCode = diodeCodeText.Text;
            model.customerCode = customerCodeText.Text;
            model.additionalCode = additionalCodeText.Text;
            model.diodeType = TestConfigurationTemplate.GetDiodeTypeFromIndex(diodeTypeCombo.SelectedIndex);
            model.barCodeOption = TestConfigurationTemplate.GetBarcodeOptionFromIndex(barCodePrinterCombo.SelectedIndex);

            model.positiveTolerenceVoltage = double.Parse(positiveTolVoltageText.Text);
            model.negativeTolerenceVoltage = double.Parse(negativeTolVoltageText.Text);
            model.nominalForwardDropVolts = double.Parse(nominalFDVText.Text);
            model.positiveTolerenceCurrent = double.Parse(postiveToleranceCurrentText.Text);
            model.negativeTolerenceCurrent = double.Parse(negativeTolerenceCurrentText.Text);
            model.nominalReverseCurrent = double.Parse(nominalRevCurrentText.Text);
            model.forwardTestCurrent = double.Parse(forwardTestCurrentText.Text);
            model.reverseTestVoltage = double.Parse(ReverseTestVoltageText.Text);
            model.forwardMaxVoltage = double.Parse(forwardMaxVoltageText.Text);
            model.positiveTolerenceResistance = double.Parse(positiveTolResText.Text);
            model.negativeTolerenceResistance = double.Parse(negativeTolResText.Text);
            model.contactResistance = double.Parse(contactResistanceText.Text);

            try
            {

                GlobalConfig.Connection.SaveModel(model);
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

                    SaveModel(ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4));
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
                    GlobalConfig.Connection.DeleteModel(ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4));
                    statusText.Text = "TEST CONFIGURATION FILE DELETED";
                    if (modelTextBlock.Text == ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4))
                    {
                        NewModel();

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
            parent.ChangeFrame(new MainPage(parent));

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

                //Loads all the model information from text file to ModelTemplate Class.
                try
                {
                    TestConfigurationTemplate model = GlobalConfig.Connection.LoadModel(ofd.SafeFileName);
                    if (model == null)
                    {
                        MessageBox.Show("File has Invalid Fields.", "File Load Failed.", MessageBoxButton.OK, MessageBoxImage.Error);
                        statusText.Text = "COULD NOT OPEN TEST CONGIGURATION FILE";
                        return;
                    }
                    CopyModelToTextBox(model);
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


        private void positiveTolVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = positiveTolVoltageText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.positiveTolerenceVoltageMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.positiveTolerenceVoltageMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.negativeTolerenceVoltageMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.negativeTolerenceVoltageMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.nominalForwardDropVoltsMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.nominalForwardDropVoltsMin)
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
                    || double.Parse(box.Text) > GlobalConfig.machineDataModel.positiveTolerenceCurrentMax ||
                    double.Parse(box.Text) < GlobalConfig.machineDataModel.positiveTolerenceCurrentMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.nominalReverseCurrentMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.nominalReverseCurrentMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.negativeTolerenceCurrentMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.negativeTolerenceCurrentMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.forwardTestCurrentMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.forwardTestCurrentMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.forwardMaxVoltageMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.forwardMaxVoltageMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.reverseTestVoltageMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.reverseTestVoltageMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.positiveTolerenceResistanceMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.positiveTolerenceResistanceMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.contactResistanceMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.contactResistanceMin)
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
                || double.Parse(box.Text) > GlobalConfig.machineDataModel.negativeTolerenceResistanceMax ||
                double.Parse(box.Text) < GlobalConfig.machineDataModel.negativeTolerenceResistanceMin)
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
                    validationResults["diodeCode"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    validationResults["diodeCode"] = "true";
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
                    validationResults["customerCode"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    validationResults["customerCode"] = "true";
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
                    validationResults["additionalCode"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    validationResults["additionalCode"] = "true";
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
