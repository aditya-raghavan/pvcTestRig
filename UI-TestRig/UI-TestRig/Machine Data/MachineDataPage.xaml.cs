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



namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for MachineData.xaml
    /// </summary>
    public partial class MachineDataPage : Page,IPage
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
            positiveToleranceVoltageHighText.Text = MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageHigh.ToString("N3");
            positiveToleranceVoltageLowText.Text = MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageLow.ToString("N3");

            negativeToleranceVoltageHighText.Text = MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageHigh.ToString("N3");
            negativeToleranceVoltageLowText.Text = MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageLow.ToString("N3");

            nominalForwardDropVoltageHighText.Text = MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsHigh.ToString("N3");
            nominalForwardDropVoltageLowText.Text = MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsLow.ToString("N3");

            positiveToleranceReverseCurrentHighText.Text = MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentHigh.ToString("N3");
            positiveToleranceReverseCurrentLowText.Text = MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentLow.ToString("N3");

            negativeToleranceReverseCurrentHighText.Text = MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentHigh.ToString("N3");
            negativeToleranceReverseCurrentLowText.Text = MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentLow.ToString("N3");

            nominalReverseCurrentHighText.Text = MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentHigh.ToString("N3");
            nominalReverseCurrentLowText.Text = MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentLow.ToString("N3");

            forwardTestCurrentHighText.Text = MachineDataGlobalConfig.machineDataObject.forwardTestCurrentHigh.ToString("N3");
            forwardTestCurrentLowText.Text = MachineDataGlobalConfig.machineDataObject.forwardTestCurrentLow.ToString("N3");

            reverseTestVoltageHighText.Text = MachineDataGlobalConfig.machineDataObject.reverseTestVoltageHigh.ToString("N3");
            reverseTestVoltageLowText.Text = MachineDataGlobalConfig.machineDataObject.reverseTestVoltageLow.ToString("N3");

            forwardMaxVoltageHighText.Text = MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageHigh.ToString("N3");
            forwardMaxVoltageLowText.Text = MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageLow.ToString("N3");

            positiveToleranceContactResistanceHighText.Text = MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceHigh.ToString("N3");
            positiveToleranceContactResistanceLowText.Text = MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceLow.ToString("N3");

            negativeToleranceContactResistanceHighText.Text = MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceHigh.ToString("N3");
            negativeToleranceContactResistanceLowText.Text = MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceLow.ToString("N3");

            contactResistanceHighText.Text = MachineDataGlobalConfig.machineDataObject.contactResistanceHigh.ToString("N3");
            contactResistanceLowText.Text = MachineDataGlobalConfig.machineDataObject.contactResistanceLow.ToString("N3");
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
            GlobalConfig.machineDataConnection.SaveMachineData();
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
            MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageHigh = double.Parse(positiveToleranceVoltageHighText.Text);
            MachineDataGlobalConfig.machineDataObject.positiveTolerenceVoltageLow = double.Parse(positiveToleranceVoltageLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageHigh = double.Parse(negativeToleranceVoltageHighText.Text);
            MachineDataGlobalConfig.machineDataObject.negativeTolerenceVoltageLow = double.Parse(negativeToleranceVoltageLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsHigh = double.Parse(nominalForwardDropVoltageHighText.Text);
            MachineDataGlobalConfig.machineDataObject.nominalForwardDropVoltsLow = double.Parse(nominalForwardDropVoltageLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentHigh = double.Parse(positiveToleranceReverseCurrentHighText.Text);
            MachineDataGlobalConfig.machineDataObject.positiveTolerenceCurrentLow = double.Parse(positiveToleranceReverseCurrentLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentHigh = double.Parse(negativeToleranceReverseCurrentHighText.Text);
            MachineDataGlobalConfig.machineDataObject.negativeTolerenceCurrentLow = double.Parse(negativeToleranceReverseCurrentLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentHigh = double.Parse(nominalReverseCurrentHighText.Text);
            MachineDataGlobalConfig.machineDataObject.nominalReverseCurrentLow = double.Parse(nominalReverseCurrentLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.forwardTestCurrentHigh = double.Parse(forwardTestCurrentHighText.Text);
            MachineDataGlobalConfig.machineDataObject.forwardTestCurrentLow = double.Parse(forwardTestCurrentLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.reverseTestVoltageHigh = double.Parse(reverseTestVoltageHighText.Text);
            MachineDataGlobalConfig.machineDataObject.reverseTestVoltageLow = double.Parse(reverseTestVoltageLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageHigh = double.Parse(forwardMaxVoltageHighText.Text);
            MachineDataGlobalConfig.machineDataObject.forwardMaxVoltageLow = double.Parse(forwardMaxVoltageLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceHigh = double.Parse(positiveToleranceContactResistanceHighText.Text);
            MachineDataGlobalConfig.machineDataObject.positiveTolerenceResistanceLow = double.Parse(positiveToleranceContactResistanceLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceHigh = double.Parse(negativeToleranceContactResistanceHighText.Text);
            MachineDataGlobalConfig.machineDataObject.negativeTolerenceResistanceLow = double.Parse(negativeToleranceContactResistanceLowText.Text);
            
            MachineDataGlobalConfig.machineDataObject.contactResistanceHigh = double.Parse(contactResistanceHighText.Text);
            MachineDataGlobalConfig.machineDataObject.contactResistanceLow = double.Parse(contactResistanceLowText.Text);
            statusLabel.Text = "APPLIED CHANGES";
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyMachineData();
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
    }
}
