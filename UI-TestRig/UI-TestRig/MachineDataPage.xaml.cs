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
    /// Interaction logic for MachineData.xaml
    /// </summary>
    public partial class MachineDataPage : Page
    {

        public DiagnosticsPage parent { get; set; }
        Dictionary<string, string> validationTextBox = new Dictionary<string, string>()
        {
            {"positiveToleranceVoltageHighText","true" },
            {"positiveToleranceVoltageLowText","true" },

            {"negativeToleranceVoltageHighText","true" },
            {"negativeToleranceVoltageLowText","true" },

            {"nominalForwardDropVoltageHighText","true" },
            {"nominalForwardDropVoltageLowText","true" },

            {"positiveToleranceReverseCurrentHighText","true" },
            {"positiveToleranceReverseCurrentLowText","true" },

            {"negativeToleranceReverseCurrentHighText","true" },
            {"negativeToleranceReverseCurrentLowText","true" },

            {"nominalReverseCurrentHighText","true" },
            {"nominalReverseCurrentLowText","true" },

            {"forwardTestCurrentHighText","true" },
            {"forwardTestCurrentLowText","true" },

            {"reverseTestVoltageHighText","true" },
            {"reverseTestVoltageLowText","true" },

            {"forwardMaxVoltageHighText","true" },
            {"forwardMaxVoltageLowText","true" },
            
            {"positiveToleranceContactResistanceHighText","true" },
            {"positiveToleranceContactResistanceLowText","true" },
            
            {"negativeToleranceContactResistanceHighText","true" },
            {"negativeToleranceContactResistanceLowText","true" },
            
            {"contactResistanceHighText","true" },
            {"contactResistanceLowText","true" },
                        
        };

        public MachineDataPage(DiagnosticsPage p)
        {
            InitializeComponent();
            parent = p;
            statusLabel.Text = "READY";
            getMachineData();


        }

        public void getMachineData()
        {
            positiveToleranceVoltageHighText.Text = GlobalConfig.machineDataObject.positiveTolerenceVoltageHigh.ToString("N3");
            positiveToleranceVoltageLowText.Text = GlobalConfig.machineDataObject.positiveTolerenceVoltageLow.ToString("N3");

            negativeToleranceVoltageHighText.Text = GlobalConfig.machineDataObject.negativeTolerenceVoltageHigh.ToString("N3");
            negativeToleranceVoltageLowText.Text = GlobalConfig.machineDataObject.negativeTolerenceVoltageLow.ToString("N3");

            nominalForwardDropVoltageHighText.Text = GlobalConfig.machineDataObject.nominalForwardDropVoltsHigh.ToString("N3");
            nominalForwardDropVoltageLowText.Text = GlobalConfig.machineDataObject.nominalForwardDropVoltsLow.ToString("N3");

            positiveToleranceReverseCurrentHighText.Text = GlobalConfig.machineDataObject.positiveTolerenceCurrentHigh.ToString("N3");
            positiveToleranceReverseCurrentLowText.Text = GlobalConfig.machineDataObject.positiveTolerenceCurrentLow.ToString("N3");

            negativeToleranceReverseCurrentHighText.Text = GlobalConfig.machineDataObject.negativeTolerenceCurrentHigh.ToString("N3");
            negativeToleranceReverseCurrentLowText.Text = GlobalConfig.machineDataObject.negativeTolerenceCurrentLow.ToString("N3");

            nominalReverseCurrentHighText.Text = GlobalConfig.machineDataObject.nominalReverseCurrentHigh.ToString("N3");
            nominalReverseCurrentLowText.Text = GlobalConfig.machineDataObject.nominalReverseCurrentLow.ToString("N3");

            forwardTestCurrentHighText.Text = GlobalConfig.machineDataObject.forwardTestCurrentHigh.ToString("N3");
            forwardTestCurrentLowText.Text = GlobalConfig.machineDataObject.forwardTestCurrentLow.ToString("N3");

            reverseTestVoltageHighText.Text = GlobalConfig.machineDataObject.reverseTestVoltageHigh.ToString("N3");
            reverseTestVoltageLowText.Text = GlobalConfig.machineDataObject.reverseTestVoltageLow.ToString("N3");

            forwardMaxVoltageHighText.Text = GlobalConfig.machineDataObject.forwardMaxVoltageHigh.ToString("N3");
            forwardMaxVoltageLowText.Text = GlobalConfig.machineDataObject.forwardMaxVoltageLow.ToString("N3");

            positiveToleranceContactResistanceHighText.Text = GlobalConfig.machineDataObject.positiveTolerenceResistanceHigh.ToString("N3");
            positiveToleranceContactResistanceLowText.Text = GlobalConfig.machineDataObject.positiveTolerenceResistanceLow.ToString("N3");

            negativeToleranceContactResistanceHighText.Text = GlobalConfig.machineDataObject.negativeTolerenceResistanceHigh.ToString("N3");
            negativeToleranceContactResistanceLowText.Text = GlobalConfig.machineDataObject.negativeTolerenceResistanceLow.ToString("N3");

            contactResistanceHighText.Text = GlobalConfig.machineDataObject.contactResistanceHigh.ToString("N3");
            contactResistanceLowText.Text = GlobalConfig.machineDataObject.contactResistanceLow.ToString("N3");
        }

        private bool ValidateForm()
        {
            bool result = true;
            if(validationTextBox.ContainsValue("false"))
            {
                result = false;
            }
            return result;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            parent.machineDataPage = this;
            ContainerWindow.container.ChangeFrame(parent);
        }

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

        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            var bc = new BrushConverter();
            textBox.Background = (Brush)bc.ConvertFrom("#ffccff");
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (validationTextBox[textBox.Name] == "false")
            {
                textBox.Text = "0.000";
            }
            else
            {
                textBox.Text = double.Parse(textBox.Text).ToString("N3");
            }
            var bc = new BrushConverter();
            textBox.Foreground = new SolidColorBrush(Colors.Black);
            textBox.Background = (Brush)bc.ConvertFrom("#ccffff");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            TextBox textBox = (TextBox)sender;
            if (double.TryParse(textBox.Text, out double d) == false)
            {
                validationTextBox[textBox.Name] = "false";
            }
            else
            {
                validationTextBox[textBox.Name] = "true";
            }            
        }

        private void SaveMachineData()
        {
            ApplyMachineData();
            GlobalConfig.Connection.SaveMachineData();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                try
                {
                    SaveMachineData();
                    statusLabel.Text = "MACHINE DATA FILE SAVED";
                }
                catch(Exception)
                {
                    statusLabel.Text = "SAVE FAILED";
                }
                
                
            }
            else
            {
                statusLabel.Text = "SAVE FAILED";
            }
        }

        private void ApplyMachineData()
        {
            GlobalConfig.machineDataObject.positiveTolerenceVoltageHigh = double.Parse(positiveToleranceVoltageHighText.Text);
            GlobalConfig.machineDataObject.positiveTolerenceVoltageLow = double.Parse(positiveToleranceVoltageLowText.Text);

            GlobalConfig.machineDataObject.negativeTolerenceVoltageHigh = double.Parse(negativeToleranceVoltageHighText.Text);
            GlobalConfig.machineDataObject.negativeTolerenceVoltageLow = double.Parse(negativeToleranceVoltageLowText.Text);

            GlobalConfig.machineDataObject.nominalForwardDropVoltsHigh = double.Parse(nominalForwardDropVoltageHighText.Text);
            GlobalConfig.machineDataObject.nominalForwardDropVoltsLow = double.Parse(nominalForwardDropVoltageLowText.Text);

            GlobalConfig.machineDataObject.positiveTolerenceCurrentHigh = double.Parse(positiveToleranceReverseCurrentHighText.Text);
            GlobalConfig.machineDataObject.positiveTolerenceCurrentLow = double.Parse(positiveToleranceReverseCurrentLowText.Text);

            GlobalConfig.machineDataObject.negativeTolerenceCurrentHigh = double.Parse(negativeToleranceReverseCurrentHighText.Text);
            GlobalConfig.machineDataObject.negativeTolerenceCurrentLow = double.Parse(negativeToleranceReverseCurrentLowText.Text);

            GlobalConfig.machineDataObject.nominalReverseCurrentHigh = double.Parse(nominalReverseCurrentHighText.Text);
            GlobalConfig.machineDataObject.nominalReverseCurrentLow = double.Parse(nominalReverseCurrentLowText.Text);

            GlobalConfig.machineDataObject.forwardTestCurrentHigh = double.Parse(forwardTestCurrentHighText.Text);
            GlobalConfig.machineDataObject.forwardTestCurrentLow = double.Parse(forwardTestCurrentLowText.Text);

            GlobalConfig.machineDataObject.reverseTestVoltageHigh = double.Parse(reverseTestVoltageHighText.Text);
            GlobalConfig.machineDataObject.reverseTestVoltageLow = double.Parse(reverseTestVoltageLowText.Text);

            GlobalConfig.machineDataObject.forwardMaxVoltageHigh = double.Parse(forwardMaxVoltageHighText.Text);
            GlobalConfig.machineDataObject.forwardMaxVoltageLow = double.Parse(forwardMaxVoltageLowText.Text);

            GlobalConfig.machineDataObject.positiveTolerenceResistanceHigh = double.Parse(positiveToleranceContactResistanceHighText.Text);
            GlobalConfig.machineDataObject.positiveTolerenceResistanceLow = double.Parse(positiveToleranceContactResistanceLowText.Text);

            GlobalConfig.machineDataObject.negativeTolerenceResistanceHigh = double.Parse(negativeToleranceContactResistanceHighText.Text);
            GlobalConfig.machineDataObject.negativeTolerenceResistanceLow = double.Parse(negativeToleranceContactResistanceLowText.Text);

            GlobalConfig.machineDataObject.contactResistanceHigh = double.Parse(contactResistanceHighText.Text);
            GlobalConfig.machineDataObject.contactResistanceLow = double.Parse(contactResistanceLowText.Text);
            statusLabel.Text = "APPLIED CHANGES";
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyMachineData();
        }
    }
}
