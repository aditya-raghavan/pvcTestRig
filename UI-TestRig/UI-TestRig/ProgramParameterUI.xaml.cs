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

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for ProgramParameterUI.xaml
    /// </summary>
    public partial class ProgramParameterUI : Window, IModelNameRequestor
    {
        //DiodeTypes for Combobox
        private static List<String> diodeTypes = TypeInformationTemplate.GetDiodeTypes();
        //Barcode options (ENABLED, DISABLED)
        private static List<String> barCodeOptions = TypeInformationTemplate.GetBarcodeOptions();
        public ProgramParameterUI()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            RefreshForm();
            //Initializes Text Connection
            GlobalConfig.InitialiseConnections();
        }

        private void f2Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                //If no model is loaded, calls Model name input form
                if(modelTextBlock.Text.Length == 0)
                {
                    ModelNameInputForm frm = new ModelNameInputForm(this);
                    frm.Show();                    
                }
                else
                {
                    //If model is already loaded, sends the model name to Save the model
                    ModelNameComplete(modelTextBlock.Text);
                }

            }
            else
            {
                MessageBox.Show("Please Enter Valid values.", "", MessageBoxButton.OK);
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
            decimal x;
            
            if(!decimal.TryParse(positiveTolVoltageText.Text, out decimal posTolVoltage))
            {
                output = false;
            }
            if (!decimal.TryParse(negativeTolVoltageText.Text, out decimal posTolCurrent))
            {
                output = false;
            }
            if (!decimal.TryParse(nominalFDVText.Text, out decimal negTolCurrent))
            {
                output = false;
            }
            if (!decimal.TryParse(postiveToleranceCurrentText.Text, out x))
            {
                output = false;
            }
            if (!decimal.TryParse(negativeTolerenceCurrentText.Text, out  x))
            {
                output = false;
            }
            if (!decimal.TryParse(nominalRevCurrentText.Text, out  x))
            {
                output = false;
            }
            if (!decimal.TryParse(forwardTestCurrentText.Text, out  x))
            {
                output = false;
            }
            if (!decimal.TryParse(ReverseTestVoltageText.Text, out  x))
            {
                output = false;
            }
            if (!decimal.TryParse(forwardMaxVoltageText.Text, out  x))
            {
                output = false;
            }
            if (!decimal.TryParse(positiveTolResText.Text, out  x))
            {
                output = false;
            }
            if (!decimal.TryParse(negativeTolResText.Text, out  x))
            {
                output = false;
            }
            if (!decimal.TryParse(contactResistanceText.Text, out  x))
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
            //Open File Dialog
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".csv";
            ofd.Filter = "Text documents (.csv)|*.csv";

            ofd.InitialDirectory = $"{ConfigurationManager.AppSettings["filePath"] }\\";
            ofd.Multiselect = false;
            Nullable<bool> result = ofd.ShowDialog();

            if(result == true)
            {
                
                ModelTemplate model = new ModelTemplate();
                modelTextBlock.Text = ofd.SafeFileName;

                //Loads all the model information from text file to ModelTemplate Class.
                model = GlobalConfig.Connection.LoadModel(ofd.SafeFileName);

                //Initializes all the textboxes with loaded model details.
                modelTextBlock.Text = model.Name;
                diodeCodeText.Text = model.TypeInformation.DiodeCode;
                customerCodeText.Text = model.TypeInformation.CustomerCode;
                additionalCodeText.Text = model.TypeInformation.AdditionalCode;
                diodeTypeCombo.SelectedIndex = model.TypeInformation.DiodeIndex;
                barCodePrinterCombo.SelectedIndex = model.TypeInformation.BarCodeIndex;

                positiveTolVoltageText.Text = model.ModelReadings.PositiveTolerenceVoltage.ToString();
                negativeTolVoltageText.Text = model.ModelReadings.NegativeTolerenceVoltage.ToString();
                nominalFDVText.Text = model.ModelReadings.NominalForwardDropVolts.ToString() ;

                postiveToleranceCurrentText.Text = model.ModelReadings.PositiveTolerenceCurrent.ToString();
                nominalRevCurrentText.Text = model.ModelReadings.NominalReverseCurrent.ToString();
                negativeTolerenceCurrentText.Text = model.ModelReadings.NegativeTolerenceCurrent.ToString();

                forwardMaxVoltageText.Text = model.ModelReadings.ForwardMaxVoltage.ToString();
                forwardTestCurrentText.Text = model.ModelReadings.ForwardTestCurrent.ToString();
                ReverseTestVoltageText.Text = model.ModelReadings.ReverseTestVoltage.ToString();

                positiveTolResText.Text = model.ModelReadings.PositiveTolerenceResistance.ToString();
                contactResistanceText.Text = model.ModelReadings.ContactResistance.ToString();
                negativeTolResText.Text = model.ModelReadings.NegativeTolerenceResistance.ToString();

                programTextBox.Text = model.Name;



            }
        }

        private void RefreshForm()
        {
            
            modelTextBlock.Text = "";
            diodeCodeText.Text = "";
            customerCodeText.Text = "";
            additionalCodeText.Text = "";

            diodeTypeCombo.ItemsSource = null;
            diodeTypeCombo.ItemsSource = diodeTypes;
            diodeTypeCombo.SelectedIndex = 1;

            barCodePrinterCombo.ItemsSource = null;
            barCodePrinterCombo.ItemsSource = barCodeOptions;
            barCodePrinterCombo.SelectedIndex = 1;

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





        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Create New Model?","Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //Refreshes form
                RefreshForm();
            }
            
        }

        public void ModelNameComplete(string modelName)
        {
            //Utility method which saves Model to specified modelname file.
            ModelTemplate model = new ModelTemplate();
            model.Name = modelName;
            modelTextBlock.Text = model.Name;
            TypeInformationTemplate ti = new TypeInformationTemplate();
            ti.DiodeCode = diodeCodeText.Text;
            ti.CustomerCode = customerCodeText.Text;
            ti.AdditionalCode = additionalCodeText.Text;
            ti.DiodeIndex = diodeTypeCombo.SelectedIndex;
            ti.BarCodeIndex = barCodePrinterCombo.SelectedIndex;
            model.TypeInformation = ti;

            ReadingsTemplate rt = new ReadingsTemplate();
            rt.PositiveTolerenceVoltage = decimal.Parse(positiveTolVoltageText.Text);
            rt.NegativeTolerenceVoltage = decimal.Parse(negativeTolVoltageText.Text);
            rt.NominalForwardDropVolts = decimal.Parse(nominalFDVText.Text);
            rt.PositiveTolerenceCurrent = decimal.Parse(postiveToleranceCurrentText.Text);
            rt.NegativeTolerenceCurrent = decimal.Parse(negativeTolerenceCurrentText.Text);
            rt.NominalReverseCurrent = decimal.Parse(nominalRevCurrentText.Text);
            rt.ForwardTestCurrent = decimal.Parse(forwardTestCurrentText.Text);
            rt.ReverseTestVoltage = decimal.Parse(ReverseTestVoltageText.Text);
            rt.ForwardMaxVoltage = decimal.Parse(forwardMaxVoltageText.Text);
            rt.PositiveTolerenceResistance = decimal.Parse(positiveTolResText.Text);
            rt.NegativeTolerenceResistance = decimal.Parse(negativeTolResText.Text);
            rt.ContactResistance = decimal.Parse(contactResistanceText.Text);

            model.ModelReadings = rt;

            
            GlobalConfig.Connection.SaveModel(model);

            MessageBox.Show("Model Saved");






        }

        private void saveAsButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                ModelNameInputForm frm = new ModelNameInputForm(this);
                frm.Show();
            }
        }

        private bool ValidateDeleteButton()
        {
            bool output = true;
            if(modelTextBlock.Text.Length == 0)
            {
                output = false;
            }

            return output;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDeleteButton())
            {
                if(MessageBox.Show($"Delete Model {modelTextBlock.Text}?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes){
                    GlobalConfig.Connection.DeleteModel(modelTextBlock.Text);
                    MessageBox.Show("Model Deleted");
                    RefreshForm();
                }
            }
            else
            {
                MessageBox.Show("Please Load a Model First");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
