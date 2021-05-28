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

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for ModelNameInputForm.xaml
    /// </summary>
    public partial class ModelNameInputForm : Window
    {
        /// <summary>
        /// A Form which has implemented IModelRequestor Interface.
        /// </summary>
        IModelNameRequestor callingForm;
        /// <summary>
        /// List of existing Model names.
        /// </summary>
        List<string> ModelNames;
        public ModelNameInputForm(IModelNameRequestor caller)
        {
            InitializeComponent();
            callingForm = caller;
            ModelNames = GlobalConfig.Connection.GetAllModelNames();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                //Calls the ModelnameComplete method with inputted model name.
                callingForm.ModelNameComplete(modelNameInputText.Text.Trim());
                this.Close();
            }
            
        }

        private bool ValidateForm()
        {
            bool output = true;
            if(modelNameInputText.Text.Length == 0)
            {
                output = false;
                MessageBox.Show("ModelName cannot be empty.");
            }
            if (ModelNames.Contains(modelNameInputText.Text.Trim()))
            {
                if(MessageBox.Show("Overwrite Model?", "ModelName Already Exists", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    output = false;
                }


            }
            return output;
        }
    }
}
