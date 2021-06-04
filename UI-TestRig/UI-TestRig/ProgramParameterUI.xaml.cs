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

        Dictionary<string, string> ValidationResults = new Dictionary<string, string>()
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
            ofd.Filter = GlobalConfig.AllowedFileType;

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

            
            if (ValidationResults.ContainsValue("false"))
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
            ofd.Filter = GlobalConfig.AllowedFileType;

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

        




        //Validations of textboxes.
        //private void positiveTolVoltageText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = positiveTolVoltageText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d) 
        //        || double.Parse(box.Text) > LoadedModel.maxPositiveTolerenceVoltage ||
        //        double.Parse(box.Text) < LoadedModel.minPositiveTolerenceVoltage)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["positiveTolerenceVoltage"] = "false";
        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["positiveTolerenceVoltage"] = "true";

        //    }
        //}

        //private void negativeTolVoltageText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = negativeTolVoltageText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxNegativeTolerenceVoltage ||
        //        double.Parse(box.Text) < LoadedModel.minNegativeTolerenceVoltage)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["negativeTolerenceVoltage"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["negativeTolerenceVoltage"] = "true";

        //    }
        //}

        //private void nominalFDVText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = nominalFDVText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxNegativeTolerenceVoltage ||
        //        double.Parse(box.Text) < LoadedModel.minNegativeTolerenceVoltage)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["nominalForwardDropVolts"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["nominalForwardDropVolts"] = "true";

        //    }
        //}

        //private void postiveToleranceCurrentText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = postiveToleranceCurrentText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxPositiveTolerenceCurrent ||
        //        double.Parse(box.Text) < LoadedModel.minPositiveTolerenceCurrent)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["positiveTolerenceCurrent"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["positiveTolerenceCurrent"] = "true";

        //    }
        //}

        //private void nominalRevCurrentText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = nominalRevCurrentText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxNominalReverseCurrent ||
        //        double.Parse(box.Text) < LoadedModel.minNominalReverseCurrent)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["negativeTolerenceCurrent"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["negativeTolerenceCurrent"] = "true";

        //    }
        //}

        //private void negativeTolerenceCurrentText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = negativeTolerenceCurrentText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxNegativeTolerenceCurrent ||
        //        double.Parse(box.Text) < LoadedModel.minNegativeTolerenceCurrent)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["nominalReverseCurrent"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["nominalReverseCurrent"] = "true";

        //    }
        //}

        //private void forwardTestCurrentText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = forwardTestCurrentText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxForwardTestCurrent ||
        //        double.Parse(box.Text) < LoadedModel.minForwardTestCurrent)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["forwardTestCurrent"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["forwardTestCurrent"] = "true";

        //    }
        //}

        //private void forwardMaxVoltageText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = forwardMaxVoltageText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxForwardMaxVoltage ||
        //        double.Parse(box.Text) < LoadedModel.minForwardMaxVoltage)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["reverseTestVoltage"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["reverseTestVoltage"] = "true";
        //    }
        //}

        //private void ReverseTestVoltageText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = ReverseTestVoltageText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxReverseTestVoltage ||
        //        double.Parse(box.Text) < LoadedModel.minReverseTestVoltage)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["forwardMaxVoltage"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["forwardMaxVoltage"] = "true";

        //    }
        //}

        //private void positiveTolResText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = positiveTolResText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxPositiveTolerenceResistance ||
        //        double.Parse(box.Text) < LoadedModel.minPositiveTolerenceResistance)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["positiveTolerenceResistance"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["positiveTolerenceResistance"] = "true";

        //    }
        //}

        //private void contactResistanceText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = contactResistanceText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxContactResistance ||
        //        double.Parse(box.Text) < LoadedModel.minContactResistance)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["negativeTolerenceResistance"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["negativeTolerenceResistance"] = "true";

        //    }
        //}

        //private void negativeTolResText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = negativeTolResText;


        //    if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
        //        || double.Parse(box.Text) > LoadedModel.maxNegativeTolerenceResistance ||
        //        double.Parse(box.Text) < LoadedModel.minNegativeTolerenceResistance)
        //    {
        //        box.Background = new SolidColorBrush(Colors.Yellow);
        //        box.Foreground = new SolidColorBrush(Colors.Red);
        //        ValidationResults["contactResistance"] = "false";

        //    }
        //    else
        //    {
        //        var bc = new BrushConverter();
        //        //#ccffff
        //        box.Background = (Brush)bc.ConvertFrom("#ccffff");
        //        box.Foreground = new SolidColorBrush(Colors.Black);
        //        ValidationResults["contactResistance"] = "true";

        //    }
        //}

        //private void diodeCodeText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = diodeCodeText;
        //    if (box.Text.Length == 0)
        //    {
        //        box.BorderBrush = new SolidColorBrush(Colors.Red);
        //        ValidationResults["diodeCode"] = "false";
        //    }
        //    else
        //    {
        //        box.BorderBrush = new SolidColorBrush(Colors.Black);
        //        ValidationResults["diodeCode"] = "true";
        //    }
        //}

        //private void customerCodeText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = customerCodeText;
        //    if (box.Text.Length == 0)
        //    {
        //        box.BorderBrush = new SolidColorBrush(Colors.Red);
        //        ValidationResults["customerCode"] = "false";
        //    }
        //    else
        //    {
        //        box.BorderBrush = new SolidColorBrush(Colors.Black);
        //        ValidationResults["customerCode"] = "true";
        //    }
        //}

        //private void additionalCodeText_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    var box = additionalCodeText;
        //    if (box.Text.Length == 0)
        //    {
        //        box.BorderBrush = new SolidColorBrush(Colors.Red);
        //        ValidationResults["additionalCode"] = "false";
        //    }
        //    else
        //    {
        //        box.BorderBrush = new SolidColorBrush(Colors.Black);
        //        ValidationResults["additionalCode"] = "true";
        //    }
        //}

        private void positiveTolVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = positiveTolVoltageText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxPositiveTolerenceVoltage ||
                double.Parse(box.Text) < LoadedModel.minPositiveTolerenceVoltage)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["positiveTolVoltageText"] = "false";
            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["positiveTolVoltageText"] = "true";
                

            }
        }

        private void negativeTolVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = negativeTolVoltageText;


                if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                    || double.Parse(box.Text) > LoadedModel.maxNegativeTolerenceVoltage ||
                    double.Parse(box.Text) < LoadedModel.minNegativeTolerenceVoltage)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                    ValidationResults["negativeTolVoltageText"] = "false";

                }
                else
                {
                    var bc = new BrushConverter();
                    //#ccffff
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                    ValidationResults["negativeTolVoltageText"] = "true";

                }
            }
        }

        private void nominalFDVText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = nominalFDVText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxNominalForwardDropVolts ||
                double.Parse(box.Text) < LoadedModel.minNominalForwardDropVolts)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["nominalFDVText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["nominalFDVText"] = "true";

            }
            
        }

        private void postiveToleranceCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = postiveToleranceCurrentText;


                if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                    || double.Parse(box.Text) > LoadedModel.maxPositiveTolerenceCurrent ||
                    double.Parse(box.Text) < LoadedModel.minPositiveTolerenceCurrent)
                {
                    box.Background = new SolidColorBrush(Colors.Yellow);
                    box.Foreground = new SolidColorBrush(Colors.Red);
                    ValidationResults["postiveToleranceCurrentText"] = "false";

                }
                else
                {
                    var bc = new BrushConverter();
                    //#ccffff
                    box.Background = (Brush)bc.ConvertFrom("#ccffff");
                    box.Foreground = new SolidColorBrush(Colors.Black);
                    ValidationResults["postiveToleranceCurrentText"] = "true";

                }
            }
        }
        private void nominalRevCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = nominalRevCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxNominalReverseCurrent ||
                double.Parse(box.Text) < LoadedModel.minNominalReverseCurrent)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["nominalRevCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["nominalRevCurrentText"] = "true";

            }
        }

        private void negativeTolerenceCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = negativeTolerenceCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxNegativeTolerenceCurrent ||
                double.Parse(box.Text) < LoadedModel.minNegativeTolerenceCurrent)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["negativeTolerenceCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["negativeTolerenceCurrentText"] = "true";

            }
            
        }

        private void forwardTestCurrentText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = forwardTestCurrentText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxForwardTestCurrent ||
                double.Parse(box.Text) < LoadedModel.minForwardTestCurrent)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["forwardTestCurrentText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["forwardTestCurrentText"] = "true";

            }
            
        }

        private void forwardMaxVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = forwardMaxVoltageText;
            
            
            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxForwardMaxVoltage ||
                double.Parse(box.Text) < LoadedModel.minForwardMaxVoltage)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["forwardMaxVoltageText"] = "false";
            
            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["forwardMaxVoltageText"] = "true";
            }
            
        }

        private void ReverseTestVoltageText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = ReverseTestVoltageText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxReverseTestVoltage ||
                double.Parse(box.Text) < LoadedModel.minReverseTestVoltage)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["ReverseTestVoltageText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["ReverseTestVoltageText"] = "true";

            }
            
        }

        private void positiveTolResText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = positiveTolResText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxPositiveTolerenceResistance ||
                double.Parse(box.Text) < LoadedModel.minPositiveTolerenceResistance)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["positiveTolResText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["positiveTolResText"] = "true";

            }
            
        }

        private void contactResistanceText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = contactResistanceText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxContactResistance ||
                double.Parse(box.Text) < LoadedModel.minContactResistance)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["contactResistanceText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["contactResistanceText"] = "true";

            }
            
        }

        private void negativeTolResText_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            var box = negativeTolResText;


            if (box.Text.Length == 0 || !double.TryParse(box.Text, out double d)
                || double.Parse(box.Text) > LoadedModel.maxNegativeTolerenceResistance ||
                double.Parse(box.Text) < LoadedModel.minNegativeTolerenceResistance)
            {
                box.Background = new SolidColorBrush(Colors.Yellow);
                box.Foreground = new SolidColorBrush(Colors.Red);
                ValidationResults["negativeTolResText"] = "false";

            }
            else
            {
                var bc = new BrushConverter();
                //#ccffff
                box.Background = (Brush)bc.ConvertFrom("#ccffff");
                box.Foreground = new SolidColorBrush(Colors.Black);
                ValidationResults["negativeTolResText"] = "true";

            }
            
        }

        private void diodeCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {

            {
                var box = diodeCodeText;
                if (box.Text.Trim().Length == 0)
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Red);
                    ValidationResults["diodeCode"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    ValidationResults["diodeCode"] = "true";
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
                    ValidationResults["customerCode"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    ValidationResults["customerCode"] = "true";
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
                    ValidationResults["additionalCode"] = "false";
                }
                else
                {
                    box.BorderBrush = new SolidColorBrush(Colors.Black);
                    ValidationResults["additionalCode"] = "true";
                }
            }
        }

        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if(ValidationResults[textBox.Name.ToString()] == "true")
            {
                var bc = new BrushConverter();
                textBox.Background = (Brush)bc.ConvertFrom("#ffccff");
            }
        }

        private void Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            
            TextBox textBox = (TextBox)sender;

            if (ValidationResults[textBox.Name.ToString()] == "true")
            {
                var bc = new BrushConverter();
                textBox.Background = (Brush)bc.ConvertFrom("#ccffff");
            }

            
        }
    }
}
