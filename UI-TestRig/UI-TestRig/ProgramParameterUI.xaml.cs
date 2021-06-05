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
    /// 
    /// READ ME
    /// 
    /// Higher and lower limit assigning function in GlobalConfig class.
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class ProgramParameterUI : Window
    {
        //LoadedModel Object which is two way binded to the form.
        TestConfigurationTemplate LoadedModel = GlobalConfig.SetLimits();
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
            { "contactResistanceText", "true" },
        };
        public ProgramParameterUI()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            //Initializes Text Connection
            GlobalConfig.InitialiseConnections();
            NewModel();
            
            

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
            positiveTolVoltageText.Text = "0";
            negativeTolVoltageText.Text = "0";
            nominalFDVText.Text = "0";
            postiveToleranceCurrentText.Text = "0";
            nominalRevCurrentText.Text = "0";
            negativeTolerenceCurrentText.Text = "0";
            forwardMaxVoltageText.Text = "0";
            forwardTestCurrentText.Text = "0";
            ReverseTestVoltageText.Text = "0";
            positiveTolResText.Text = "0";
            contactResistanceText.Text = "0";
            negativeTolResText.Text = "0";
            programTextBox.Text = "";

            RefreshComboBoxes();
            
        }

        //Disabled user from inputting values other than digits.
        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!(Char.IsDigit(ch) || ch.Equals('.') | ch.Equals('-'))) {
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
                if(modelTextBlock.Text.Length == 0)
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
            barCodePrinterCombo.SelectedIndex =TestConfigurationTemplate.GetIndexFromBarcodeOption( model.barCodeOption);
            positiveTolVoltageText.Text = model.positiveTolerenceVoltage.ToString();
            negativeTolVoltageText.Text = model.negativeTolerenceVoltage.ToString();
            nominalFDVText.Text = model.nominalForwardDropVolts.ToString();
            postiveToleranceCurrentText.Text = model.positiveTolerenceCurrent.ToString();
            negativeTolerenceCurrentText.Text = model.negativeTolerenceCurrent.ToString();
            nominalRevCurrentText.Text = model.nominalReverseCurrent.ToString();
            forwardTestCurrentText.Text = model.forwardTestCurrent.ToString();
            ReverseTestVoltageText.Text = model.reverseTestVoltage.ToString();
            forwardMaxVoltageText.Text = model.forwardMaxVoltage.ToString();
            positiveTolResText.Text = model.positiveTolerenceResistance.ToString();
            negativeTolResText.Text = model.negativeTolerenceResistance.ToString();
            contactResistanceText.Text = model.contactResistance.ToString();

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

                    SaveModel(ofd.SafeFileName.Substring(0,ofd.SafeFileName.Length-4));
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
            if(result == true)
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
            if (MessageBox.Show("Close Form?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
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
                
                //Loads all the model information from text file to ModelTemplate Class.
                try
                {
                    TestConfigurationTemplate model = GlobalConfig.Connection.LoadModel(ofd.SafeFileName);
                    if(model == null)
                    {
                        MessageBox.Show("File has Invalid Fields.", "File Load Failed.",MessageBoxButton.OK, MessageBoxImage.Error);
                        statusText.Text = "COULD NOT OPEN TEST CONGIGURATION FILE";
                        return;
                    }
                    CopyModelToTextBox(model);                                
                    statusText.Text = "TEST CONFIGURATION FILE OPENED SUCCESSFULLY";
                }
                catch(Exception)
                {
                    statusText.Text = "COULD NOT OPEN TEST CONFIGURATION FILE";
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
                || double.Parse(box.Text) > LoadedModel.positiveTolerenceVoltageMax ||
                double.Parse(box.Text) < LoadedModel.positiveTolerenceVoltageMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["positiveTolVoltageText"] = "false";
            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["positiveTolVoltageText"] = "true";
                

            }
        }

        private void negativeTolVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = negativeTolVoltageText;


                if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                    || double.Parse(box.Text) > LoadedModel.negativeTolerenceVoltageMax ||
                    double.Parse(box.Text) < LoadedModel.negativeTolerenceVoltageMin)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                    validationResults["negativeTolVoltageText"] = "false";

                }
                else
                {
                    var bc = new BrushConverter();
                    //#ccffff
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                    validationResults["negativeTolVoltageText"] = "true";

                }
            }
        }

        private void nominalFDVText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = nominalFDVText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.nominalForwardDropVoltsMax ||
                double.Parse(box.Text) < LoadedModel.nominalForwardDropVoltsMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["nominalFDVText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["nominalFDVText"] = "true";

            }
            
        }

        private void postiveToleranceCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = postiveToleranceCurrentText;


                if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                    || double.Parse(box.Text) > LoadedModel.positiveTolerenceCurrentMax ||
                    double.Parse(box.Text) < LoadedModel.positiveTolerenceCurrentMin)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                    validationResults["postiveToleranceCurrentText"] = "false";

                }
                else
                {
                    var bc = new BrushConverter();
                    //#ccffff
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                    validationResults["postiveToleranceCurrentText"] = "true";

                }
            }
        }
        private void nominalRevCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = nominalRevCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.nominalReverseCurrentMax ||
                double.Parse(box.Text) < LoadedModel.nominalReverseCurrentMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["nominalRevCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["nominalRevCurrentText"] = "true";

            }
        }

        private void negativeTolerenceCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = negativeTolerenceCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.negativeTolerenceCurrentMax ||
                double.Parse(box.Text) < LoadedModel.negativeTolerenceCurrentMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["negativeTolerenceCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["negativeTolerenceCurrentText"] = "true";

            }
            
        }

        private void forwardTestCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = forwardTestCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.forwardTestCurrentMax ||
                double.Parse(box.Text) < LoadedModel.forwardTestCurrentMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["forwardTestCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["forwardTestCurrentText"] = "true";

            }
            
        }

        private void forwardMaxVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = forwardMaxVoltageText;
            
            
            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.forwardMaxVoltageMax ||
                double.Parse(box.Text) < LoadedModel.forwardMaxVoltageMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["forwardMaxVoltageText"] = "false";
            
            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["forwardMaxVoltageText"] = "true";
            }
            
        }

        private void ReverseTestVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = ReverseTestVoltageText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.reverseTestVoltageMax ||
                double.Parse(box.Text) < LoadedModel.reverseTestVoltageMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["ReverseTestVoltageText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["ReverseTestVoltageText"] = "true";

            }
            
        }

        private void positiveTolResText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = positiveTolResText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.positiveTolerenceResistanceMax ||
                double.Parse(box.Text) < LoadedModel.positiveTolerenceResistanceMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["positiveTolResText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["positiveTolResText"] = "true";

            }
            
        }

        private void contactResistanceText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = contactResistanceText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.contactResistanceMax ||
                double.Parse(box.Text) < LoadedModel.contactResistanceMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["contactResistanceText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                validationResults["contactResistanceText"] = "true";

            }
            
        }

        private void negativeTolResText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = negativeTolResText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.negativeTolerenceResistanceMax ||
                double.Parse(box.Text) < LoadedModel.negativeTolerenceResistanceMin)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                validationResults["negativeTolResText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
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

            if(validationResults[textBox.Name.ToString()] == "true")
            {
                var bc = new BrushConverter();
                textBox.Background = (Brush)bc.ConvertFrom("#ffccff");
            }
        }

        private void Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            
            TextBox textBox = (TextBox)sender;

            if (validationResults[textBox.Name.ToString()] == "true")
            {
                var bc = new BrushConverter();
                textBox.Background = (Brush)bc.ConvertFrom("#ccffff");
            }

            
        }
    }
}
