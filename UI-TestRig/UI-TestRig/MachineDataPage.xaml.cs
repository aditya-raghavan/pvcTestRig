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

        Dictionary<string, string> textBoxPairs = new Dictionary<string, string>()
        {
            {"positiveToleranceVoltageHighText","positiveToleranceVoltageLowText"},
            {"negativeToleranceVoltageHighText","negativeToleranceVoltageLowText"},
            {"nominalForwardDropVoltageHighText","nominalForwardDropVoltageLowText"},
            {"positiveToleranceReverseCurrentHighText","positiveToleranceReverseCurrentLowText"},
            {"negativeToleranceReverseCurrentHighText","negativeToleranceReverseCurrentLowText"},
            {"nominalReverseCurrentHighText","nominalReverseCurrentLowText"},
            {"forwardTestCurrentHighText","forwardTestCurrentLowText"},
            {"reverseTestVoltageHighText","reverseTestVoltageLowText"},
            {"forwardMaxVoltageHighText","forwardMaxVoltageLowText"},
            {"positiveToleranceContactResistanceHighText","positiveToleranceContactResistanceLowText"},
            {"negativeToleranceContactResistanceHighText","negativeToleranceContactResistanceLowText"},
            {"contactResistanceHighText","contactResistanceLowText"},
            
        };
        Dictionary<string, string> validationTextBox = new Dictionary<string, string>()
        {
            {"positiveToleranceVoltageHighText","false" },
            {"positiveToleranceVoltageLowText","false" },

            {"negativeToleranceVoltageHighText","false" },
            {"negativeToleranceVoltageLowText","false" },

            {"nominalForwardDropVoltageHighText","false" },
            {"nominalForwardDropVoltageLowText","false" },

            {"positiveToleranceReverseCurrentHighText","false" },
            {"positiveToleranceReverseCurrentLowText","false" },

            {"negativeToleranceReverseCurrentHighText","false" },
            {"negativeToleranceReverseCurrentLowText","false" },

            {"nominalReverseCurrentHighText","false" },
            {"nominalReverseCurrentLowText","false" },

            {"forwardTestCurrentHighText","false" },
            {"forwardTestCurrentLowText","false" },

            {"reverseTestVoltageHighText","false" },
            {"reverseTestVoltageLowText","false" },

            {"forwardMaxVoltageHighText","false" },
            {"forwardMaxVoltageLowText","false" },
            
            {"positiveToleranceContactResistanceHighText","false" },
            {"positiveToleranceContactResistanceLowText","false" },
            
            {"negativeToleranceContactResistanceHighText","false" },
            {"negativeToleranceContactResistanceLowText","false" },
            
            {"contactResistanceHighText","false" },
            {"contactResistanceLowText","false" },
                        
        };
        
        Dictionary<string, string> validationRow = new Dictionary<string, string>()
        {
            {"positiveToleranceVoltage","false" },
            {"negativeToleranceVoltage","false" },
            {"nominalForwardDropVoltage","false" },
            {"positiveToleranceReverseCurrent","false" },
            {"negativeToleranceReverseCurrent","false" },
            {"nominalReverseCurrent","false" },
            {"forwardTestCurrent","false" },
            {"reverseTestVoltage","false" },
            {"forwardMaxVoltage","false" },
            {"positiveToleranceContactResistance","false" },
            {"negativeToleranceContactResistance","false" },
            {"contactResistance","false" },
        };

        public MachineDataPage(IContainer container)
        {
            InitializeComponent();
            parent = container;
            if (!GlobalConfig.isMachineDataFileThere)
            {
                statusLabel.Text = "MACHINE DATA FILE NOT FOUND";
            }
            else
            {
                statusLabel.Text = "MACHINE DATA FILE LOADED SUCCESSFULLY";
            }
            getMachineData();


        }

        private void getMachineData()
        {
            positiveToleranceVoltageHighText.Text = GlobalConfig.machineDataModel.positiveTolerenceVoltageMax.ToString("N3");
            positiveToleranceVoltageLowText.Text = GlobalConfig.machineDataModel.positiveTolerenceVoltageMin.ToString("N3");

            negativeToleranceVoltageHighText.Text = GlobalConfig.machineDataModel.negativeTolerenceVoltageMax.ToString("N3");
            negativeToleranceVoltageLowText.Text = GlobalConfig.machineDataModel.negativeTolerenceVoltageMin.ToString("N3");

            nominalForwardDropVoltageHighText.Text = GlobalConfig.machineDataModel.nominalForwardDropVoltsMax.ToString("N3");
            nominalForwardDropVoltageLowText.Text = GlobalConfig.machineDataModel.nominalForwardDropVoltsMin.ToString("N3");

            positiveToleranceReverseCurrentHighText.Text = GlobalConfig.machineDataModel.positiveTolerenceCurrentMax.ToString("N3");
            positiveToleranceReverseCurrentLowText.Text = GlobalConfig.machineDataModel.positiveTolerenceCurrentMin.ToString("N3");

            negativeToleranceReverseCurrentHighText.Text = GlobalConfig.machineDataModel.negativeTolerenceCurrentMax.ToString("N3");
            negativeToleranceReverseCurrentLowText.Text = GlobalConfig.machineDataModel.negativeTolerenceCurrentMin.ToString("N3");

            nominalReverseCurrentHighText.Text = GlobalConfig.machineDataModel.nominalReverseCurrentMax.ToString("N3");
            nominalReverseCurrentLowText.Text = GlobalConfig.machineDataModel.nominalReverseCurrentMin.ToString("N3");

            forwardTestCurrentHighText.Text = GlobalConfig.machineDataModel.forwardTestCurrentMax.ToString("N3");
            forwardTestCurrentLowText.Text = GlobalConfig.machineDataModel.forwardTestCurrentMin.ToString("N3");

            reverseTestVoltageHighText.Text = GlobalConfig.machineDataModel.reverseTestVoltageMax.ToString("N3");
            reverseTestVoltageLowText.Text = GlobalConfig.machineDataModel.reverseTestVoltageMin.ToString("N3");

            forwardMaxVoltageHighText.Text = GlobalConfig.machineDataModel.forwardMaxVoltageMax.ToString("N3");
            forwardMaxVoltageLowText.Text = GlobalConfig.machineDataModel.forwardMaxVoltageMin.ToString("N3");

            positiveToleranceContactResistanceHighText.Text = GlobalConfig.machineDataModel.positiveTolerenceResistanceMax.ToString("N3");
            positiveToleranceContactResistanceLowText.Text = GlobalConfig.machineDataModel.positiveTolerenceResistanceMin.ToString("N3");

            negativeToleranceContactResistanceHighText.Text = GlobalConfig.machineDataModel.negativeTolerenceResistanceMax.ToString("N3");
            negativeToleranceContactResistanceLowText.Text = GlobalConfig.machineDataModel.negativeTolerenceResistanceMin.ToString("N3");

            contactResistanceHighText.Text = GlobalConfig.machineDataModel.contactResistanceMax.ToString("N3");
            contactResistanceLowText.Text = GlobalConfig.machineDataModel.contactResistanceMin.ToString("N3");
        }

        private bool ValidateForm()
        {
            bool result = true;
            if(validationRow.ContainsValue("false") || validationTextBox.ContainsValue("false"))
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
            TextBox high = new TextBox();
            TextBox low = new TextBox();
            TextBox textBox = (TextBox)sender;
            if (validationTextBox[textBox.Name] == "false")
            {
                textBox.Text = "0.000";
            }
            else
            {
                textBox.Text = double.Parse(textBox.Text).ToString("N3");
            }
            if (textBox.Name.Contains("High"))
            {
                high = (TextBox)sender;
                low = (TextBox)this.FindName(textBoxPairs[high.Name]);
            }
            else if (textBox.Name.Contains("Low"))
            {
                high = (TextBox)this.FindName(textBoxPairs.FirstOrDefault(x => x.Value == textBox.Name).Key);
                low = (TextBox)sender;
            }
            if (validationRow[high.Name.Replace("HighText", "")] == "false")
            {

                low.Background = new SolidColorBrush(Colors.Yellow);
                low.Foreground = new SolidColorBrush(Colors.Red);
                high.Background = new SolidColorBrush(Colors.Yellow);
                high.Foreground = new SolidColorBrush(Colors.Red);

            }
            else
            {
                var bc = new BrushConverter();
                low.Background = (Brush)bc.ConvertFrom("#ccffff");
                low.Foreground = new SolidColorBrush(Colors.Black);
                high.Background = (Brush)bc.ConvertFrom("#ccffff");
                high.Foreground = new SolidColorBrush(Colors.Black);
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox high = new TextBox();
            TextBox low = new TextBox();
            TextBox textBox = (TextBox)sender;
            if (double.TryParse(textBox.Text, out double d) == false)
            {
                validationTextBox[textBox.Name] = "false";
            }
            else
            {
                validationTextBox[textBox.Name] = "true";
            }
            if (textBox.Name.Contains("High"))
            {
                high = (TextBox)sender;
                low = (TextBox)this.FindName(textBoxPairs[high.Name]);
            }
            else if (textBox.Name.Contains("Low"))
            {
                high = (TextBox)this.FindName(textBoxPairs.FirstOrDefault(x => x.Value == textBox.Name).Key);
                low = (TextBox)sender;
            }
            if (validationTextBox[high.Name] == "true" && validationTextBox[low.Name] == "true")
            {
                double l = double.Parse(low.Text);
                double h = double.Parse(high.Text);
                if (h >= l)
                {
                    validationRow[high.Name.Replace("HighText", "")] = "true";
                    if (high.IsFocused == false && low.IsFocused == false)
                    {
                        var bc = new BrushConverter();
                        high.Background = (Brush)bc.ConvertFrom("#ccffff");
                        high.Foreground = new SolidColorBrush(Colors.Black);
                        low.Background = (Brush)bc.ConvertFrom("#ccffff");
                        low.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                else
                {
                    validationRow[high.Name.Replace("HighText", "")] = "false";
                    if (high.IsFocused == false && low.IsFocused == false)
                    {
                        high.Background = new SolidColorBrush(Colors.Yellow);
                        high.Foreground = new SolidColorBrush(Colors.Red);
                        low.Background = new SolidColorBrush(Colors.Yellow);
                        low.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
            }
        }

        private void SaveMachineData()
        {
            GlobalConfig.machineDataModel.positiveTolerenceVoltageMax = double.Parse(positiveToleranceVoltageHighText.Text);
            GlobalConfig.machineDataModel.positiveTolerenceVoltageMin = double.Parse(positiveToleranceVoltageLowText.Text);

            GlobalConfig.machineDataModel.negativeTolerenceVoltageMax = double.Parse(negativeToleranceVoltageHighText.Text);
            GlobalConfig.machineDataModel.negativeTolerenceVoltageMin = double.Parse(negativeToleranceVoltageLowText.Text);

            GlobalConfig.machineDataModel.nominalForwardDropVoltsMax = double.Parse(nominalForwardDropVoltageHighText.Text);
            GlobalConfig.machineDataModel.nominalForwardDropVoltsMin = double.Parse(nominalForwardDropVoltageLowText.Text);

            GlobalConfig.machineDataModel.positiveTolerenceCurrentMax = double.Parse(positiveToleranceReverseCurrentHighText.Text);
            GlobalConfig.machineDataModel.positiveTolerenceCurrentMin = double.Parse(positiveToleranceReverseCurrentLowText.Text);

            GlobalConfig.machineDataModel.negativeTolerenceCurrentMax = double.Parse(negativeToleranceReverseCurrentHighText.Text);
            GlobalConfig.machineDataModel.negativeTolerenceCurrentMin = double.Parse(negativeToleranceReverseCurrentLowText.Text);

            GlobalConfig.machineDataModel.nominalReverseCurrentMax = double.Parse(nominalReverseCurrentHighText.Text);
            GlobalConfig.machineDataModel.nominalReverseCurrentMin = double.Parse(nominalReverseCurrentLowText.Text);

            GlobalConfig.machineDataModel.forwardTestCurrentMax = double.Parse(forwardTestCurrentHighText.Text);
            GlobalConfig.machineDataModel.forwardTestCurrentMin = double.Parse(forwardTestCurrentLowText.Text);

            GlobalConfig.machineDataModel.reverseTestVoltageMax = double.Parse(reverseTestVoltageHighText.Text);
            GlobalConfig.machineDataModel.reverseTestVoltageMin = double.Parse(reverseTestVoltageLowText.Text);

            GlobalConfig.machineDataModel.forwardMaxVoltageMax = double.Parse(forwardMaxVoltageHighText.Text);
            GlobalConfig.machineDataModel.forwardMaxVoltageMin = double.Parse(forwardMaxVoltageLowText.Text);

            GlobalConfig.machineDataModel.positiveTolerenceResistanceMax = double.Parse(positiveToleranceContactResistanceHighText.Text);
            GlobalConfig.machineDataModel.positiveTolerenceResistanceMin = double.Parse(positiveToleranceContactResistanceLowText.Text);

            GlobalConfig.machineDataModel.negativeTolerenceResistanceMax = double.Parse(negativeToleranceContactResistanceHighText.Text);
            GlobalConfig.machineDataModel.negativeTolerenceResistanceMin = double.Parse(negativeToleranceContactResistanceLowText.Text);

            GlobalConfig.machineDataModel.contactResistanceMax = double.Parse(contactResistanceHighText.Text);
            GlobalConfig.machineDataModel.contactResistanceMin = double.Parse(contactResistanceLowText.Text);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                SaveMachineData();
                statusLabel.Text = "MACHINE DATA FILE SAVED";
                GlobalConfig.isMachineDataFileThere = true;
            }
            else
            {
                statusLabel.Text = "SAVE FAILED";
            }
        }
    }
}
