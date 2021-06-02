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
    /// 
    /// 
    /// 
    /// Interaction logic for ProgramParameterUI.xaml
    /// 
    /// This page is powered by an object of TestConfiguration class called LoadedModel.
    /// The textboxes in the form are binded to the values of Loaded Model. 
    /// The binding is in TWO WAY MODE. Changes happening in textboxes will reflect in LoadedModel and vice versa. 
    /// 
    /// 
    /// 
    /// When loading a model, the loaded values are copied to LoadedModel's properties, which will incur a change in form values. 
    /// Similarly, when a save operation is performed, the current values of LoadedModel is passed for saving. 
    /// 
    /// Validations of the textboxes happen on lost focus event through IDataErrorInfo Interface which the testconfigurationTemplate Class would have Implemented. 
    /// The validations are done on the properties of the class, hence any change in textbox would trigger a change in property of the class which would trigger a validation. 
    /// 
    /// The higher and lower limit of the readings are stored in an array in Global.Config class to simulate the idea of having limit values. 
    /// 
    /// Invalid Values in TypeInformation TextBoxes would initiate a red border to the textboxes. 
    /// Invalid values in readings would initiate a yellow background in reading's textboxes.
    /// 
    /// TextFile Validation is done in TextConnectorProcessor class which would check if the file is in the correct format.
    /// 
    /// </summary>
    public partial class ProgramParameterUI : Window
    {
        //LoadedModel Object which is two way binded to the form.
        TestConfigurationTemplate LoadedModel = new TestConfigurationTemplate();
        //DiodeTypes for Combobox
        private static List<String> diodeTypes = TestConfigurationTemplate.GetDiodeTypes();
        //Barcode options (ENABLED, DISABLED)
        private static List<String> barCodeOptions = TestConfigurationTemplate.GetBarcodeOptions();
        public ProgramParameterUI()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            
            //Initializes Text Connection
            GlobalConfig.InitialiseConnections();
            NewModel();
            this.DataContext = LoadedModel;
            

        }

        private void RefreshComboBoxes()
        {
            diodeTypeCombo.ItemsSource = null;
            diodeTypeCombo.ItemsSource = diodeTypes;
            diodeTypeCombo.SelectedIndex = TestConfigurationTemplate.GetIndexFromDiodeType(LoadedModel.DiodeType);

            barCodePrinterCombo.ItemsSource = null;
            barCodePrinterCombo.ItemsSource = barCodeOptions;
            barCodePrinterCombo.SelectedIndex = TestConfigurationTemplate.GetIndexFromBarcodeOption(LoadedModel.BarCodeOption);
        }

        private void NewModel()
        {
            TestConfigurationTemplate model = new TestConfigurationTemplate();
            CopyModelToLoadedModel(model);
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
        private void f2Button_Click(object sender, RoutedEventArgs e)
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

                                return;
                            }

                        }

                        SaveModel(ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4));
                    }
                    else
                    {
                        statusLabel.Content = "Save Cancelled";
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
                MessageBox.Show("Incorrect Values Detected. Please Check for missing fields. Inputted values must be within their respective limits.", "Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateForm()
        {
            bool output = true;

            if(diodeCodeText.Text.Length == 0)
            {
                output = false;
            }
            if(customerCodeText.Text.Length == 0)
            {
                output = false;
            }
            if(additionalCodeText.Text.Length == 0)
            {
                output = false;
            }
            double x;
            
            if(!double.TryParse(positiveTolVoltageText.Text, out x) || double.Parse(positiveTolVoltageText.Text) > GlobalConfig.maxValues[0] || double.Parse(positiveTolVoltageText.Text) < GlobalConfig.minValues[0])
            {
                output = false;
            }
            if (!double.TryParse(negativeTolVoltageText.Text, out x) || double.Parse(negativeTolVoltageText.Text) > GlobalConfig.maxValues[1] || double.Parse(negativeTolVoltageText.Text) < GlobalConfig.minValues[1])
            {
                output = false;
            }
            if (!double.TryParse(nominalFDVText.Text, out x) || double.Parse(nominalFDVText.Text) > GlobalConfig.maxValues[2] || double.Parse(nominalFDVText.Text) < GlobalConfig.minValues[2])
            {
                output = false;
            }
            if (!double.TryParse(postiveToleranceCurrentText.Text, out x) || double.Parse(postiveToleranceCurrentText.Text) > GlobalConfig.maxValues[3] || double.Parse(postiveToleranceCurrentText.Text) < GlobalConfig.minValues[3])
            {
                output = false;
            }
            if (!double.TryParse(negativeTolerenceCurrentText.Text, out  x) || double.Parse(negativeTolerenceCurrentText.Text) > GlobalConfig.maxValues[4] || double.Parse(negativeTolerenceCurrentText.Text) < GlobalConfig.minValues[4])
            {
                output = false;
            }
            if (!double.TryParse(nominalRevCurrentText.Text, out  x) || double.Parse(nominalRevCurrentText.Text) > GlobalConfig.maxValues[5] || double.Parse(nominalRevCurrentText.Text) < GlobalConfig.minValues[5])
            {
                output = false;
            }
            if (!double.TryParse(forwardTestCurrentText.Text, out  x) || double.Parse(forwardTestCurrentText.Text) > GlobalConfig.maxValues[6] || double.Parse(forwardTestCurrentText.Text) < GlobalConfig.minValues[6])
            {
                output = false;
            }
            if (!double.TryParse(ReverseTestVoltageText.Text, out  x) || double.Parse(ReverseTestVoltageText.Text) > GlobalConfig.maxValues[7] || double.Parse(ReverseTestVoltageText.Text) < GlobalConfig.minValues[7])
            {
                output = false;
            }
            if (!double.TryParse(forwardMaxVoltageText.Text, out  x) || double.Parse(forwardMaxVoltageText.Text) > GlobalConfig.maxValues[8] || double.Parse(forwardMaxVoltageText.Text) < GlobalConfig.minValues[8])
            {
                output = false;
            }
            if (!double.TryParse(positiveTolResText.Text, out  x) || double.Parse(positiveTolResText.Text) > GlobalConfig.maxValues[9] || double.Parse(positiveTolResText.Text) < GlobalConfig.minValues[9])
            {
                output = false;
            }
            if (!double.TryParse(negativeTolResText.Text, out  x) || double.Parse(negativeTolResText.Text) > GlobalConfig.maxValues[10] || double.Parse(negativeTolResText.Text) < GlobalConfig.minValues[10])
            {
                output = false;
            }
            if (!double.TryParse(contactResistanceText.Text, out  x) || double.Parse(contactResistanceText.Text) > GlobalConfig.maxValues[11] || double.Parse(contactResistanceText.Text) < GlobalConfig.minValues[11])
            {
                output = false;
            }

            return output;

        }

        private void diagnosticsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ////Open File Dialog
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.DefaultExt = ".csv";
            //ofd.Filter = "Text documents (.csv)|*.csv";

            //ofd.InitialDirectory = $"{ConfigurationManager.AppSettings["filePath"] }\\";
            //ofd.Multiselect = false;
            //Nullable<bool> result = ofd.ShowDialog();

            //if(result == true)
            //{
                
            //    ModelTemplate model = new ModelTemplate();
            //    modelTextBlock.Text = ofd.SafeFileName;

            //    //Loads all the model information from text file to ModelTemplate Class.
            //    model = GlobalConfig.Connection.LoadModel(ofd.SafeFileName);

            //    //Initializes all the textboxes with loaded model details.
            //    modelTextBlock.Text = model.Name;
            //    diodeCodeText.Text = model.DiodeCode;
            //    customerCodeText.Text = model.CustomerCode;
            //    additionalCodeText.Text = model.AdditionalCode;
            //    diodeTypeCombo.SelectedIndex = model.DiodeIndex;
            //    barCodePrinterCombo.SelectedIndex = model.BarCodeIndex;

            //    positiveTolVoltageText.Text = model.PositiveTolerenceVoltage.ToString();
            //    negativeTolVoltageText.Text = model.NegativeTolerenceVoltage.ToString();
            //    nominalFDVText.Text = model.NominalForwardDropVolts.ToString() ;

            //    postiveToleranceCurrentText.Text = model.PositiveTolerenceCurrent.ToString();
            //    nominalRevCurrentText.Text = model.NominalReverseCurrent.ToString();
            //    negativeTolerenceCurrentText.Text = model.NegativeTolerenceCurrent.ToString();

            //    forwardMaxVoltageText.Text = model.ForwardMaxVoltage.ToString();
            //    forwardTestCurrentText.Text = model.ForwardTestCurrent.ToString();
            //    ReverseTestVoltageText.Text = model.ReverseTestVoltage.ToString();

            //    positiveTolResText.Text = model.PositiveTolerenceResistance.ToString();
            //    contactResistanceText.Text = model.ContactResistance.ToString();
            //    negativeTolResText.Text = model.NegativeTolerenceResistance.ToString();

            //    programTextBox.Text = model.Name;



            //}
        }

        
        //New Button event
        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Create New Model?","Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //Refreshes form
                NewModel();
                statusLabel.Content = "New Model";
                
            }
            
        }


        //Retrieved model from the database is copied to loaded model.
        private void CopyModelToLoadedModel(TestConfigurationTemplate model)
        {
            LoadedModel.Name = model.Name;
            LoadedModel.DiodeCode = model.DiodeCode;
            LoadedModel.AdditionalCode = model.AdditionalCode;
            LoadedModel.CustomerCode = model.CustomerCode;
            LoadedModel.DiodeType = model.DiodeType;
            LoadedModel.BarCodeOption = model.BarCodeOption;
            LoadedModel.PositiveTolerenceVoltage = model.PositiveTolerenceVoltage;
            LoadedModel.NegativeTolerenceVoltage = model.NegativeTolerenceVoltage;
            LoadedModel.NominalForwardDropVolts = model.NominalForwardDropVolts;
            LoadedModel.PositiveTolerenceCurrent = model.PositiveTolerenceCurrent;
            LoadedModel.NegativeTolerenceCurrent = model.NegativeTolerenceCurrent;
            LoadedModel.NominalReverseCurrent = model.NominalReverseCurrent;
            LoadedModel.ForwardTestCurrent = model.ForwardTestCurrent;
            LoadedModel.ReverseTestVoltage = model.ReverseTestVoltage;
            LoadedModel.ForwardMaxVoltage = model.ForwardMaxVoltage;
            LoadedModel.PositiveTolerenceResistance = model.PositiveTolerenceResistance;
            LoadedModel.NegativeTolerenceResistance = model.NegativeTolerenceResistance;
            LoadedModel.ContactResistance = model.ContactResistance;
            
            RefreshComboBoxes();

        }


        //Initiates save operation
        public void SaveModel(string modelName)
        {
            //Utility method which saves Model to specified modelname file.

            try
            {
                LoadedModel.Name = modelName;
                LoadedModel.DiodeType = TestConfigurationTemplate.GetDiodeTypeFromIndex(diodeTypeCombo.SelectedIndex);
                LoadedModel.BarCodeOption = TestConfigurationTemplate.GetBarcodeOptionFromIndex(barCodePrinterCombo.SelectedIndex);
                GlobalConfig.Connection.SaveModel(LoadedModel);
                statusLabel.Content = "Model Saved";
            }
            catch (Exception)
            {
                statusLabel.Content = "Could not save model";
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

                            return;
                        }

                    }

                    SaveModel(ofd.SafeFileName.Substring(0,ofd.SafeFileName.Length-4));
                    statusLabel.Content = "Test Configuration Saved.";
                }
                else
                {
                    statusLabel.Content = "Save Cancelled";
                }

            }
            else
            {
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

                        return;
                    }

                }

                if (MessageBox.Show($"Delete Test Configuration {ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4)}?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GlobalConfig.Connection.DeleteModel(ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4));
                    statusLabel.Content = "Test Configuration Deleted";
                    if (modelTextBlock.Text == ofd.SafeFileName.Substring(0, ofd.SafeFileName.Length - 4))
                    {
                        NewModel();
                    
                    }
                }
                else
                {
                    statusLabel.Content = "Delete Cancelled";
                }

            }
            else
            {
                statusLabel.Content = "Delete Cancelled";
            }
            

            
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                        statusLabel.Content = "Test Configuration Load Failed.";
                        return;
                    }
                    CopyModelToLoadedModel(model);

                    //Initializes all the textboxes with loaded model details.

                    statusLabel.Content = "Test Configuration Loaded Successfully.";
                }
                catch(Exception)
                {
                    statusLabel.Content = "Test Configuration Load Failed.";
                }



            }
        }
    }
}
