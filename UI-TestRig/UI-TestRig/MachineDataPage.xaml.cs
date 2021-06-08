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
        IContainer parent;
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

        public MachineDataPage(IContainer container)
        {
            InitializeComponent();
            parent = container;
            statusLabel.Text = "READY";
            getMachineData();


        }

        private void getMachineData()
        {
            positiveToleranceVoltageHighText.Text = GlobalConfig.machineDataModel.positiveTolerenceVoltageHigh.ToString("N3");
            positiveToleranceVoltageLowText.Text = GlobalConfig.machineDataModel.positiveTolerenceVoltageLow.ToString("N3");

            negativeToleranceVoltageHighText.Text = GlobalConfig.machineDataModel.negativeTolerenceVoltageHigh.ToString("N3");
            negativeToleranceVoltageLowText.Text = GlobalConfig.machineDataModel.negativeTolerenceVoltageLow.ToString("N3");

            nominalForwardDropVoltageHighText.Text = GlobalConfig.machineDataModel.nominalForwardDropVoltsHigh.ToString("N3");
            nominalForwardDropVoltageLowText.Text = GlobalConfig.machineDataModel.nominalForwardDropVoltsLow.ToString("N3");

            positiveToleranceReverseCurrentHighText.Text = GlobalConfig.machineDataModel.positiveTolerenceCurrentHigh.ToString("N3");
            positiveToleranceReverseCurrentLowText.Text = GlobalConfig.machineDataModel.positiveTolerenceCurrentLow.ToString("N3");

            negativeToleranceReverseCurrentHighText.Text = GlobalConfig.machineDataModel.negativeTolerenceCurrentHigh.ToString("N3");
            negativeToleranceReverseCurrentLowText.Text = GlobalConfig.machineDataModel.negativeTolerenceCurrentLow.ToString("N3");

            nominalReverseCurrentHighText.Text = GlobalConfig.machineDataModel.nominalReverseCurrentHigh.ToString("N3");
            nominalReverseCurrentLowText.Text = GlobalConfig.machineDataModel.nominalReverseCurrentLow.ToString("N3");

            forwardTestCurrentHighText.Text = GlobalConfig.machineDataModel.forwardTestCurrentHigh.ToString("N3");
            forwardTestCurrentLowText.Text = GlobalConfig.machineDataModel.forwardTestCurrentLow.ToString("N3");

            reverseTestVoltageHighText.Text = GlobalConfig.machineDataModel.reverseTestVoltageHigh.ToString("N3");
            reverseTestVoltageLowText.Text = GlobalConfig.machineDataModel.reverseTestVoltageLow.ToString("N3");

            forwardMaxVoltageHighText.Text = GlobalConfig.machineDataModel.forwardMaxVoltageHigh.ToString("N3");
            forwardMaxVoltageLowText.Text = GlobalConfig.machineDataModel.forwardMaxVoltageLow.ToString("N3");

            positiveToleranceContactResistanceHighText.Text = GlobalConfig.machineDataModel.positiveTolerenceResistanceHigh.ToString("N3");
            positiveToleranceContactResistanceLowText.Text = GlobalConfig.machineDataModel.positiveTolerenceResistanceLow.ToString("N3");

            negativeToleranceContactResistanceHighText.Text = GlobalConfig.machineDataModel.negativeTolerenceResistanceHigh.ToString("N3");
            negativeToleranceContactResistanceLowText.Text = GlobalConfig.machineDataModel.negativeTolerenceResistanceLow.ToString("N3");

            contactResistanceHighText.Text = GlobalConfig.machineDataModel.contactResistanceHigh.ToString("N3");
            contactResistanceLowText.Text = GlobalConfig.machineDataModel.contactResistanceLow.ToString("N3");
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
            parent.ChangeFrame(new DiagnosticsPage(parent));
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
            GlobalConfig.machineDataModel.positiveTolerenceVoltageHigh = double.Parse(positiveToleranceVoltageHighText.Text);
            GlobalConfig.machineDataModel.positiveTolerenceVoltageLow = double.Parse(positiveToleranceVoltageLowText.Text);

            GlobalConfig.machineDataModel.negativeTolerenceVoltageHigh = double.Parse(negativeToleranceVoltageHighText.Text);
            GlobalConfig.machineDataModel.negativeTolerenceVoltageLow = double.Parse(negativeToleranceVoltageLowText.Text);

            GlobalConfig.machineDataModel.nominalForwardDropVoltsHigh = double.Parse(nominalForwardDropVoltageHighText.Text);
            GlobalConfig.machineDataModel.nominalForwardDropVoltsLow = double.Parse(nominalForwardDropVoltageLowText.Text);

            GlobalConfig.machineDataModel.positiveTolerenceCurrentHigh = double.Parse(positiveToleranceReverseCurrentHighText.Text);
            GlobalConfig.machineDataModel.positiveTolerenceCurrentLow = double.Parse(positiveToleranceReverseCurrentLowText.Text);

            GlobalConfig.machineDataModel.negativeTolerenceCurrentHigh = double.Parse(negativeToleranceReverseCurrentHighText.Text);
            GlobalConfig.machineDataModel.negativeTolerenceCurrentLow = double.Parse(negativeToleranceReverseCurrentLowText.Text);

            GlobalConfig.machineDataModel.nominalReverseCurrentHigh = double.Parse(nominalReverseCurrentHighText.Text);
            GlobalConfig.machineDataModel.nominalReverseCurrentLow = double.Parse(nominalReverseCurrentLowText.Text);

            GlobalConfig.machineDataModel.forwardTestCurrentHigh = double.Parse(forwardTestCurrentHighText.Text);
            GlobalConfig.machineDataModel.forwardTestCurrentLow = double.Parse(forwardTestCurrentLowText.Text);

            GlobalConfig.machineDataModel.reverseTestVoltageHigh = double.Parse(reverseTestVoltageHighText.Text);
            GlobalConfig.machineDataModel.reverseTestVoltageLow = double.Parse(reverseTestVoltageLowText.Text);

            GlobalConfig.machineDataModel.forwardMaxVoltageHigh = double.Parse(forwardMaxVoltageHighText.Text);
            GlobalConfig.machineDataModel.forwardMaxVoltageLow = double.Parse(forwardMaxVoltageLowText.Text);

            GlobalConfig.machineDataModel.positiveTolerenceResistanceHigh = double.Parse(positiveToleranceContactResistanceHighText.Text);
            GlobalConfig.machineDataModel.positiveTolerenceResistanceLow = double.Parse(positiveToleranceContactResistanceLowText.Text);

            GlobalConfig.machineDataModel.negativeTolerenceResistanceHigh = double.Parse(negativeToleranceContactResistanceHighText.Text);
            GlobalConfig.machineDataModel.negativeTolerenceResistanceLow = double.Parse(negativeToleranceContactResistanceLowText.Text);

            GlobalConfig.machineDataModel.contactResistanceHigh = double.Parse(contactResistanceHighText.Text);
            GlobalConfig.machineDataModel.contactResistanceLow = double.Parse(contactResistanceLowText.Text);

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
    }
}
